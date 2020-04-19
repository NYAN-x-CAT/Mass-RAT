using SharedLibraries.Packet.Commands;
using SharedLibraries.Packet.Interfaces;

namespace ClientAndroid.Project.PacketHandler
{
    public class ReadPacket
    {
        public ReadPacket(IPacket packet)
        {
            switch (packet)
            {
                case PacketFileManager_GetPath _packet:
                    {
                        new HandleFileManager().GetPath(_packet.Path);
                        break;
                    }

                case PacketFileManager_GetDrivers _packet:
                    {
                        new HandleFileManager().GetPath(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
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