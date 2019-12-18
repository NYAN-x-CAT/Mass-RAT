using ProtoBuf;
using SharedLibraries.Packet.Interfaces;

namespace SharedLibraries.Packet.Commands
{
    [ProtoContract]
    public class PacketIdentification : IPacket 
    {
        [ProtoMember(1)]
        public string Username { get; set; }

        [ProtoMember(2)]
        public string OperatingSystem { get; set; }

        [ProtoMember(3)]
        public string ID { get; set; }

        [ProtoMember(4)]
        public Enums.ClientType Type { get; set; }
    }
}
