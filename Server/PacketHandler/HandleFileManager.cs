using Server.Forms;
using Server.Networking;
using SharedLibraries.Packet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.PacketHandler
{
   public class HandleFileManager
    {
        public void GetDrivers(SocketClient client, PacketFileManager_GetDrivers packet)
        {
            if (client.CurrentForms.GetFormFileManager != null)
            {
                client.CurrentForms.GetFormFileManager.AddDrivers(packet);
            }
        }

        public void GetPath(SocketClient client, PacketFileManager_GetPath packet)
        {
            if (client.CurrentForms.GetFormFileManager != null)
            {
                client.CurrentForms.GetFormFileManager.AddFilesFolders(packet);
            }
        }

        public void DownloadFile(SocketClient client, PacketFileManager_DownloadFile packet)
        {
            FormFileManager formFileManager = (FormFileManager)Application.OpenForms[$"FileManager {packet.SocketId}"];
            if (formFileManager != null)
            {
                formFileManager.DownloadFile(client, packet);
            }
            else
            {
                client.Disconnected();
            }
        }
    }
}
