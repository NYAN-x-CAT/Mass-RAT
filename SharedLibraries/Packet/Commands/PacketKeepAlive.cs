using ProtoBuf;
using SharedLibraries.Packet.Interfaces;

namespace SharedLibraries.Packet.Commands
{
    [ProtoContract]
    public class PacketKeepAlive : IPacket
    {
    }
}
