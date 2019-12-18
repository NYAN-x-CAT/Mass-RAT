using Server.Networking;
using Server.Settings;
using SharedLibraries.Packet.Commands;
using SharedLibraries.Packet.Interfaces;
using System;
using System.Diagnostics;

namespace Server.PacketHandler
{
    public class ReadPacket
    {
        /// <summary>
        /// execute the client command
        /// </summary>
        /// <param name="client">current client</param>
        /// <param name="packet">the full packet that was received</param>
        public ReadPacket(SocketClient client, IPacket packet)
        {
            try
            {
                switch (packet)
                {
                    case PacketIdentification _packet: // to add client to the main form listview
                        {
                            SafeThreading.UIThread.AddClientToListview(client, _packet);
                            break;
                        }

                    case PacketFileManager_GetPath _packet: // list of files and folders
                        {
                            new HandleFileManager().GetPath(client, _packet);
                            break;
                        }

                    case PacketFileManager_GetDrivers _packet: // list of drivers
                        {
                            new HandleFileManager().GetDrivers(client, _packet);
                            break;
                        }

                    case PacketFileManager_DownloadFile _packet:
                        {
                            new HandleFileManager().DownloadFile(client, _packet);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                client.Disconnected();
            }
        }
    }
}
