using ClientWindows.Networking;
using SharedLibraries.Packet.Commands;
using SharedLibraries.Packet.Interfaces;

namespace ClientWindows.PacketHandler
{
    public class ReadPacket
    {
        public ReadPacket(IPacket packet)
        {
            switch (packet)
            {
                case PacketFileManager_GetPath _packet:
                    {
                        new HandleFileManager().GetPath(_packet);
                        break;
                    }

                case PacketFileManager_GetDrivers _packet:
                    {
                        new HandleFileManager().GetDrivers();
                        break;
                    }

                case PacketFileManager_DownloadFile _packet:
                    {
                        new HandleFileManager().DownloadFile(_packet);
                        break;
                    }
            }
        }
    }
}
