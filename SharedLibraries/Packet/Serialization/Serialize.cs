using ProtoBuf;
using SharedLibraries.Helper;
using SharedLibraries.Packet.Interfaces;
using System.IO;

namespace SharedLibraries.Packet.Serialization
{
    /// <summary>
    /// Serialization is the process of converting an object into a stream of bytes to store the object
    /// </summary>
    public static class Serialize
    {
        public static byte[] PacketSerialize(IPacket packet)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize(ms, packet);
                return GZip.Compress(ms.ToArray());
            }
        }
    }
}
