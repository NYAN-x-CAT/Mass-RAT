using ProtoBuf;
using SharedLibraries.Helper;
using SharedLibraries.Packet.Interfaces;
using System.IO;

namespace SharedLibraries.Packet.Serialization
{
    /// <summary>
    /// Deserialization is the reverse process where the byte stream is used to recreate the actual object
    /// </summary>
    public static class Desirialize
    {
        public static IPacket PacketDesirialize(byte[] byteArray)
        {
            using (var stream = new MemoryStream(GZip.Decompress(byteArray)))
            {
                stream.Position = 0;
                return Serializer.Deserialize<IPacket>(stream);
            }
        }
    }
}
