using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Server.Networking
{
    public class SocketServer
    {
        /// <summary>
        /// Begins listening for clients
        /// docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket?view=netframework-4.5
        /// </summary>
        /// <param name="port">Port to listen for clients on</param>
        public SocketServer(int port)
        {
            try
            {
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    ReceiveBufferSize = 50 * 1000,
                    SendBufferSize = 50 * 1000,
                };
                server.Bind(new IPEndPoint(IPAddress.Any, port));
                server.Listen(200);
                server.BeginAccept(EndAcceptCall, server);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Accepts incoming client
        /// docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.beginaccept?view=netframework-4.5
        /// </summary>
        /// <param name="ar">The status of the asynchronous operation</param>
        private void EndAcceptCall(IAsyncResult ar)
        {
            Socket server = (Socket)ar.AsyncState;
            try
            {
                Socket newClient = server.EndAccept(ar);
                new SocketClient(newClient);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                server.BeginAccept(new AsyncCallback(EndAcceptCall), server); //~loop
            }
        }
    }
}