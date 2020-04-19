using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;

using ClientAndroid.Project.PacketHandler;
using SharedLibraries.Packet.Commands;
using SharedLibraries.Packet.Enums;
using SharedLibraries.Packet.Interfaces;
using SharedLibraries.Packet.Serialization;

namespace ClientAndroid.Project
{
    [Service]
    public class SocketClient : Service
    {
        private static bool IsRunning { get; set; }
        /// <summary>
        /// The socket used for communication.
        /// </summary>
        public static Socket Socket { get; private set; }

        /// <summary>
        /// send a ping to check if other socket is alive or not every x second
        /// </summary>
        private static Timer KeepAlivePacket;

        /// <summary>
        /// determinate the socket status
        /// </summary>
        public static bool IsConnected { get; private set; }

        /// <summary>
        /// sync send
        /// </summary>
        private static readonly object LockSend = new object();


        /// <summary>
        /// initialize your activity
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
        }

        /// <summary>
        /// Called by the system every time a client explicitly starts the service
        /// </summary>
        /// <param name="intent">The Intent supplied to StartService(Intent), as given.</param>
        /// <param name="flags">Additional data about this start request</param>
        /// <param name="startId">A unique integer representing this specific request to start</param>
        /// <returns></returns>
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                new Thread(() =>
                {
                    ReceiveHeader();
                }).Start();
            }
            return StartCommandResult.NotSticky;
        }

        /// <summary>
        /// Read incoming headers
        /// </summary>
        public static void ReceiveHeader()
        {
            while (true)
            {
                while (IsConnected)
                {
                    try
                    {
                        byte[] header = ReceiveData(4);
                        if (header.Length != 4)
                        {
                            IsConnected = false;
                            break;
                        }
                        else
                        {
                            int packetSize = BitConverter.ToInt32(header, 0);
                            if (packetSize > 0)
                            {
                                Console.WriteLine($"Android: packet size is {packetSize}");
                                byte[] payload = ReceiveData(packetSize);
                                if (payload.Length != packetSize)
                                {
                                    IsConnected = false;
                                    break;
                                }
                                else
                                {
                                    IPacket packet = Desirialize.PacketDesirialize(payload);
                                    Console.WriteLine($"Android: packet received is {packet.GetType().Name}");
                                    new Thread(delegate ()
                                    {
                                        new ReadPacket(packet);
                                    }).Start();
                                }
                            }
                        }
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine(se.SocketErrorCode);
                        IsConnected = false;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        IsConnected = false;
                        break;
                    }
                }

                while (!IsConnected)
                {
                    try
                    {
                        Thread.Sleep(2000);
                        Socket?.Dispose();
                        KeepAlivePacket?.Dispose();

                        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                        {
                            ReceiveBufferSize = 50 * 1000,
                            SendBufferSize = 50 * 1000,
                        };
                        Socket.Connect(Configuration.Host, Configuration.Port);
                        IsConnected = true;

                        Send(IdSender());
                        KeepAlivePacket = new Timer(Ping, null, new Random().Next(5000, 30000), new Random().Next(5000, 30000)); //random interval
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine(se.SocketErrorCode);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
        }

        /// <summary>
        /// Read incoming packets | Synchronous
        /// docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.receive?view=netframework-4.8
        /// </summary>
        /// <param name="size">size of incoming payload</param>
        /// <returns>full payload as byte array</returns>
        private static byte[] ReceiveData(int size)
        {
            try
            {
                Socket.ReceiveBufferSize = size;
                byte[] data = new byte[size];
                int offset = 0;

                while (size > 0)
                {
                    int received = Socket.Receive(data, offset, size, SocketFlags.None);

                    if (received <= 0)
                        return new byte[0];

                    offset += received;
                    size -= received;
                    //Console.WriteLine($"Client: received[{received}] offset[{offset}]  size[{size}]");
                }
                return data;
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.SocketErrorCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new byte[0];
        }

        /// <summary>
        /// Send packet
        /// docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.send?view=netframework-4.5
        /// </summary>
        /// <param name="packet">payload to be sent</param>
        public static void Send(IPacket packet)
        {
            lock (LockSend)
            {
                try
                {
                    if (!Socket.Connected || !IsConnected || packet == null)
                    {
                        IsConnected = false;
                        return;
                    }
                    else
                    {
                        byte[] buffer = Serialize.PacketSerialize(packet);
                        byte[] bufferSize = BitConverter.GetBytes(buffer.Length); // convert to 4 bytes Length
                        Console.WriteLine($"Android: Sending {buffer.Length}");

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
                                Console.WriteLine($"Android: Sent {sent}");
                            }
                        }

                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine(se.SocketErrorCode);
                    IsConnected = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsConnected = false;
                }
            }
        }

        private static void Ping(object o)
        {
            Send(new PacketKeepAlive()); // useful to send current active window
            GC.Collect();
        }

        private static IPacket IdSender()
        {
            return new PacketIdentification
            {
                Type = ClientType.Android,
                Username = $"{Build.Manufacturer} {Build.Model}",
                OperatingSystem = Helper.GetAndroidVersion(),
                ID = Configuration.Id,
            };
        }

        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.
            return null;
        }
    }

}