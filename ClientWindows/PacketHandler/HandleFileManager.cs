using ClientWindows.Networking;
using ClientWindows.Settings;
using SharedLibraries.Helper;
using SharedLibraries.Packet.Commands;
using SharedLibraries.Packet.Interfaces;
using SharedLibraries.Packet.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ClientWindows.PacketHandler
{
    public class HandleFileManager
    {
        /// <summary>
        /// get all ready drivers and send them back to file manager form
        /// </summary>
        public void GetDrivers()
        {
            // a new list to store all drivers
            PacketFileManager_GetDrivers packetDrivers = new PacketFileManager_GetDrivers { ListDrivers = new List<PacketFileManager_DriverPacket>() };
            foreach (DriveInfo driver in DriveInfo.GetDrives())
            {
                if (driver.IsReady)
                {
                    packetDrivers.ListDrivers.Add(new PacketFileManager_DriverPacket
                    {
                        Name = driver.Name,
                        Type = driver.DriveType.ToString(),
                        Size = $"{Convertor.IntegerToUnitData(driver.AvailableFreeSpace)} free of {Convertor.IntegerToUnitData(driver.TotalSize)}",
                    });
                }
            }
            ClientSocket.Send(packetDrivers);
        }

        /// <summary>
        /// Get all files and folder and send them back to file manager form
        /// </summary>
        /// <param name="packet">the path to get files and folder 'packet.Path'</param>
        public void GetPath(PacketFileManager_GetPath packet)
        {
            try
            {
                // two new list to add files and folders
                PacketFileManager_GetPath packetGetPath = new PacketFileManager_GetPath
                {
                    Path = packet.Path,
                    ListFiles = new List<PacketFileManager_FilePacket>(),
                    ListFolders = new List<PacketFileManager_FolderPacket>(),
                };

                foreach (DirectoryInfo folder in new DirectoryInfo(packet.Path).GetDirectories())
                {
                    packetGetPath.ListFolders.Add(new PacketFileManager_FolderPacket
                    {
                        FullPath = folder.FullName,
                        Name = folder.Name,
                        DateCreation = folder.CreationTime.ToUniversalTime(),
                    });
                }

                foreach (FileInfo file in new DirectoryInfo(packet.Path).GetFiles())
                {
                    packetGetPath.ListFiles.Add(new PacketFileManager_FilePacket
                    {
                        FullPath = file.FullName,
                        Name = file.Name,
                        Icon = GetIcon(file.FullName),
                        Size = Convertor.IntegerToUnitData(file.Length),
                        DateCreation = file.CreationTime.ToUniversalTime(),
                    });
                }
                ClientSocket.Send(packetGetPath);
            }
            catch (Exception ex)
            {
                ClientSocket.Send(new PacketFileManager_GetPath { Error = ex.Message });
            }
        }

        /// <summary>
        /// extract icons from any file
        /// if the file is an image, it will resized as thumbnail
        /// </summary>
        /// <param name="file"></param>
        /// <returns>image converted to byte[]</returns>
        private byte[] GetIcon(string file)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    // images
                    if (file.EndsWith("jpg") || file.EndsWith("jpeg") || file.EndsWith("gif") || file.EndsWith("png") || file.EndsWith("bmp") || file.EndsWith("wmf") || file.EndsWith("tiff"))
                    {
                        using (Bitmap originalBitmap = new Bitmap(file))
                        using (Bitmap ResizedBitmap = new Bitmap(originalBitmap, new Size(48, 48)))
                        {
                            if (file.EndsWith("jpg") || file.EndsWith("jpeg"))
                                ResizedBitmap.Save(stream, ImageFormat.Jpeg);

                            else if (file.EndsWith("png"))
                                ResizedBitmap.Save(stream, ImageFormat.Png);

                            else if (file.EndsWith("gif"))
                                ResizedBitmap.Save(stream, ImageFormat.Gif);

                            else if (file.EndsWith("png"))
                                ResizedBitmap.Save(stream, ImageFormat.Png);

                            else if (file.EndsWith("bmp"))
                                ResizedBitmap.Save(stream, ImageFormat.Bmp);

                            else if (file.EndsWith("wmf"))
                                ResizedBitmap.Save(stream, ImageFormat.Wmf);

                            else if (file.EndsWith("tiff"))
                                ResizedBitmap.Save(stream, ImageFormat.Tiff);
                        }
                    }
                    // system icon
                    else
                        using (Icon icon = Icon.ExtractAssociatedIcon(file))
                        {
                            icon.ToBitmap().Save(stream, ImageFormat.Png);
                        }
                    return stream.ToArray();
                }
                catch
                {
                    using (Bitmap bitmap = new Bitmap(48, 48))
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                        return stream.ToArray();
                    }
                }
            }
        }

        public void DownloadFile(PacketFileManager_DownloadFile packet)
        {
            FileInfo fileInfo = new FileInfo(packet.FullPath);
            if (fileInfo.Exists)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(Configuration.Host, Configuration.Port);
                    socket.SendBufferSize = 50 * 1000;
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    socket.Dispose();
                }
            }
        }

        private void SendSocket(IPacket packet, Socket socket)
        {
            try
            {
                byte[] buffer = Serialize.PacketSerialize(packet);
                byte[] bufferSize = BitConverter.GetBytes(buffer.Length);
                Debug.WriteLine($"DownloadFile: Sending {buffer.Length}");

                socket.Poll(-1, SelectMode.SelectWrite);
                socket.Send(bufferSize, 0, bufferSize.Length, SocketFlags.None);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    int read = 0;
                    memoryStream.Position = 0;
                    byte[] chunk = new byte[150 * 1000];
                    while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                    {
                        socket.Poll(-1, SelectMode.SelectWrite);
                        int sent = socket.Send(chunk, 0, read, SocketFlags.None);
                        Debug.WriteLine($"DownloadFile: Sent {sent}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
