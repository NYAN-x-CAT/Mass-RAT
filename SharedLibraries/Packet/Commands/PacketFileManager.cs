using ProtoBuf;
using SharedLibraries.Packet.Interfaces;
using System;
using System.Collections.Generic;

namespace SharedLibraries.Packet.Commands
{

    #region To Get Drviers
    /// <summary>
    /// DriveInfo  alike
    /// </summary>
    [ProtoContract]
    public class PacketFileManager_DriverPacket : IPacket
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string Type { get; set; }

        [ProtoMember(3)]
        public string Size { get; set; }
    }

    /// <summary>
    /// To save list of drivers
    /// </summary>
    [ProtoContract]
    public class PacketFileManager_GetDrivers : IPacket
    {
        [ProtoMember(1)]
        public List<PacketFileManager_DriverPacket> ListDrivers { get; set; }
    }
    #endregion


    #region To Get Files And Folders
    /// <summary>
    /// FileInfo alike
    /// </summary>
    [ProtoContract]
    public class PacketFileManager_FilePacket : IPacket
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string FullPath { get; set; }

        [ProtoMember(3)]
        public byte[] Icon { get; set; }

        [ProtoMember(4)]
        public string Size { get; set; }

        [ProtoMember(5)]
        public DateTime DateCreation { get; set; }
    }

    /// <summary>
    /// DirectoryInfo alike
    /// </summary>
    [ProtoContract]
    public class PacketFileManager_FolderPacket : IPacket
    {
        [ProtoMember(1)]
        public string Name { get; set; }

        [ProtoMember(2)]
        public string FullPath { get; set; }

        [ProtoMember(3)]
        public DateTime DateCreation { get; set; }
    }

    /// <summary>
    /// To save list of files and list of folders
    /// </summary>
    [ProtoContract]
    public class PacketFileManager_GetPath : IPacket
    {
        [ProtoMember(1)]
        public string Path { get; set; }

        [ProtoMember(2)]
        public List<PacketFileManager_FilePacket> ListFiles { get; set; }

        [ProtoMember(3)]
        public List<PacketFileManager_FolderPacket> ListFolders { get; set; }

        [ProtoMember(4)]
        public string Error { get; set; }
    }

    /// <summary>
    /// Create a download profile for the file
    /// </summary>
    [ProtoContract]
    public class PacketFileManager_DownloadFile : IPacket
    {
        [ProtoMember(1)]
        public string FullPath { get; set; }

        [ProtoMember(2)]
        public byte[] ByteArray { get; set; }

        [ProtoMember(3)]
        public string SocketId { get; set; }

        [ProtoMember(4)]
        public string FileId { get; set; }

        [ProtoMember(5)]
        public long Size { get; set; }
    }
    #endregion
}