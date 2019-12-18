using ProtoBuf;
using SharedLibraries.Packet.Commands;

namespace SharedLibraries.Packet.Interfaces
{
    // add your class and number++
    [ProtoContract]
    [ProtoInclude(1, typeof(PacketIdentification))]

    [ProtoInclude(2, typeof(PacketKeepAlive))]

    [ProtoInclude(3, typeof(PacketFileManager_GetDrivers))]
    [ProtoInclude(4, typeof(PacketFileManager_FilePacket))]
    [ProtoInclude(5, typeof(PacketFileManager_GetPath))]
    [ProtoInclude(6, typeof(PacketFileManager_FolderPacket))]
    [ProtoInclude(7, typeof(PacketFileManager_DriverPacket))]
    [ProtoInclude(8, typeof(PacketFileManager_DownloadFile))]

    public class IPacket { }
}
