using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SharedLibraries.Packet.Enums
{
    [ProtoContract]
    public enum ClientType
    {
        PC,
        Android,
    }
}
