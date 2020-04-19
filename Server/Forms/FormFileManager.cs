using Server.Networking;
using SharedLibraries.Helper;
using SharedLibraries.Packet.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Server.Forms
{
    public partial class FormFileManager : Form
    {
        public SocketClient SocketClient { get; set; }
        public FormFileManager()
        {
            InitializeComponent();
        }

        #region Incoming Commands

        /// <summary>
        /// to add drivers
        /// </summary>
        public void AddDrivers(PacketFileManager_GetDrivers packet)
        {

            #region Drivers
            // a new temp listviewitem to add PacketFileManager_DriverPacket fields
            List<ListViewItem> driversList = new List<ListViewItem>();

            foreach (PacketFileManager_DriverPacket driver in packet.ListDrivers)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = driver.Name,
                    ToolTipText = driver.Name,
                    Group = betterListView1.Groups["Drivers"],
                };
                item.SubItems.AddRange(new string[] { driver.Type, driver.Size });
                driversList.Add(item);
            }
            #endregion

            this.Invoke((MethodInvoker)delegate
            {
                betterListView1.BeginUpdate();
                betterListView1.Items.Clear();
                txtError.Text = string.Empty;
                txtPath.Text = string.Empty;
                betterListView1.Items.AddRange(driversList.ToArray());
                betterListView1.Enabled = true;
                betterListView1.Focus();
                betterListView1.EndUpdate();
            });
        }

        /// <summary>
        /// to add files and folder
        /// </summary>
        public void AddFilesFolders(PacketFileManager_GetPath packet)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(packet.Error)) // check for error
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtError.Text = packet.Error;
                        betterListView1.Enabled = true;
                        betterListView1.Focus();
                    });
                    return;
                }
                // go to ui thread to add items to betterListView1
                this.Invoke((MethodInvoker)delegate
                {
                    //BeginUpdate: Prevents the control from drawing until the EndUpdate() method is called.
                    betterListView1.BeginUpdate();
                    betterListView1.Items.Clear(); // clear old items
                    txtError.Text = string.Empty; // clear error
                    txtPath.Text = packet.Path; // show the new path
                    imageList1.Images.Clear(); // clear old icons
                    // add folder icon
                    imageList1.Images.Add("0_folder", Image.FromStream(new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAACXBIWXMAAAsTAAALEwEAmpwYAAAFHGlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDUgNzkuMTYzNDk5LCAyMDE4LzA4LzEzLTE2OjQwOjIyICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHhtcDpDcmVhdGVEYXRlPSIyMDE5LTEyLTEzVDE5OjIyOjU3KzAzOjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAxOS0xMi0xM1QxOToyNDowMiswMzowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAxOS0xMi0xM1QxOToyNDowMiswMzowMCIgZGM6Zm9ybWF0PSJpbWFnZS9wbmciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpkNDU0NTliZC05ZWI0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"))));
                });


                #region Folders
                List<ListViewItem> folderList = null;
                if (packet.ListFolders != null)
                {
                    folderList = new List<ListViewItem>();
                    foreach (PacketFileManager_FolderPacket folder in packet.ListFolders)
                    {
                        ListViewItem item = new ListViewItem
                        {
                            Text = folder.Name,
                            ToolTipText = folder.FullPath,
                            Group = betterListView1.Groups["Folders"],
                            ImageIndex = 0,
                        };
                        item.SubItems.AddRange(new string[] { folder.DateCreation.ToLocalTime().ToString() });
                        folderList.Add(item);
                    }
                }

                #endregion


                #region Files
                List<ListViewItem> fileList = null;

                if (packet.ListFiles != null)
                {
                    fileList = new List<ListViewItem>();
                    foreach (PacketFileManager_FilePacket file in packet.ListFiles)
                    {
                        if (file.Icon.Length == 0)
                        {
                            file.Icon = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAB5AAAAeQB+hcYWwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAALcSURBVHic7Zu7bhNBFIb/M3u313gV3xKBhJF4gAiJDqXgJtHTUSFeghqJhhZEQxEegQQKWl4gHRXNIm2DZBALS2znskNBFKUI3vXMeGcdz1faZ2b+83vGc3y0Js45VJAkSQDHf+n7/hMV831JD/H40/dSsR8f9BE67OxL3zjnT3tR623RWFYUUIYkSYKcuR8455sq5lPAgIheoER+0gYkSRLktvsehNuycymmFwNuUZCUAafJc9yRmUcnwgYkSRLklru7zMkDggbEceznlrsD4K5iPZUztwFxHPvMa+wCuLcAPZUzlwEnye/ggiQPzGHAmeTvL1BP5ZQy4CT5d7hgyQOAXSKG7KD5inN+BcDnmYHEMjWyqqPQgBjwQj9QUt7WESWl8DJjDNAtQDfGAN0CdGMM0C2gLDYDHl1v4uG1BkjlvArnWhhtl+H5zQibnX/9jRtdF8/2UkyO5dt5td0BbZfBImDYsvFmq3OaPABsbfh4fWsNPd9C6BBcS3xPUFFTNAb8MM3GwitI8DU7wiCw4P8nweyQIz3Icblpnf9+OwyGwGTWGrU+AlfD2fJChxA65ydfltoegaowBugWoBtjgG4BuhG+BTjnyH79VqlFmPBSC0RitYD4Ncg5xn/2hYerJGyFQOUGgIRdV4+4DmEDiBF6GwPhhevCyn8JGgN0C9DNyhsgVQeM9+txDQaNhp46IEvrUQgFQSBcB5gjIDqQGEN3va9SizDExD9HqY4Qk1i4Lix/BpIYA3QL0I3ENQhMpzM7zpXheb7wD0KJQihH+uOn6HCl9Nb7IBLbzOYIiA4kxhB11lRqEUZbHeB6hQ9j156VPwLGAN0CdGMM0C1AN4W3wHQ0cpy8Lv3/+ZiOJg66XbkHJDzbto7HB+pUVYjnuoVPT6z8ETAG6BagG2OAbgG6MQYUBQyjKCOivAoxKiGifBhFhf9hKrMDjiyLtpfJBCLKLYu2ARwVxf4F/k2ZA8gV5fIAAAAASUVORK5CYII=");
                        }
                        imageList1.Images.Add(file.FullPath, Image.FromStream(new MemoryStream(file.Icon)));
                        ListViewItem item = new ListViewItem
                        {
                            Text = file.Name,
                            ToolTipText = file.FullPath,
                            Group = betterListView1.Groups["Files"],
                            ImageKey = file.FullPath,
                        };
                        item.SubItems.AddRange(new string[] { file.Size, file.DateCreation.ToLocalTime().ToString() });
                        fileList.Add(item);
                    }
                }
                #endregion

                // add
                this.Invoke((MethodInvoker)delegate
                {
                    if (folderList != null) betterListView1.Items.AddRange(folderList.ToArray());
                    if (fileList != null) betterListView1.Items.AddRange(fileList.ToArray());
                    betterListView1.Enabled = true;
                    betterListView1.Focus();
                    betterListView1.EndUpdate();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Save incoming file
        /// </summary>
        public async void DownloadFile(SocketClient socket, PacketFileManager_DownloadFile packet)
        {
            if (packet.ByteArray == null) // packet is just a add to listview
            {
                ListViewItem item = new ListViewItem
                {
                    Text = Path.GetFileName(packet.FullPath),
                    Tag = socket, // needed for timer
                    ToolTipText = packet.FileId,
                };
                item.SubItems.AddRange(new string[] { "Download", Convertor.IntegerToUnitData(packet.Size) });

                this.Invoke((MethodInvoker)delegate
                {
                    betterListView2.Items.Add(item);
                });
            }
            else // save byte[]
            {
                using (FileStream stream = new FileStream(Path.GetFileName(packet.FullPath), FileMode.Create))
                   await stream.WriteAsync(packet.ByteArray, 0, packet.ByteArray.Length);

                this.Invoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem item in betterListView2.Items)
                    {
                        if (item.ToolTipText == packet.FileId)
                        {
                            item.SubItems[2].Text = "Finished";
                            break;
                        }
                    }
                });
                socket.Disconnected();
            }
        }
        #endregion


        #region Form Events
        private void FormFileManager_Load(object sender, EventArgs e)
        {
            GetDrivers();
        }

        private void FormFileManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            SocketClient.CurrentForms.GetFormFileManager = null;
            SocketClient = null;
        }

        private void IconArrowBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void IconArrowForward_Click(object sender, EventArgs e)
        {
            GoForward();
        }

        private void BetterListView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (betterListView2.SelectedItems[0].SubItems[2].Text == "Finished" && File.Exists(betterListView2.SelectedItems[0].Text))
                {
                    Process.Start(betterListView2.SelectedItems[0].Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Settings.SafeThreading.UIThread.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// to do
        /// navigate using mouse
        /// </summary>
        private void BetterListView1_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(e.Button);
            if (betterListView1.ContainsFocus)

                switch (e.Button)
                {
                    case MouseButtons.XButton1: //back
                        {
                            GoBack();
                            break;
                        }

                    case MouseButtons.XButton2: //return
                        {
                            GoForward();
                            break;
                        }
                }
        }

        /// <summary>
        /// to do
        /// navigate using keyboard
        /// </summary>
        private void BetterListView1_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.KeyCode);
            if (betterListView1.ContainsFocus)
                switch (e.KeyCode)
                {
                    case Keys.Back:
                        {
                            GoBack();
                            break;
                        }

                    case Keys.Return:
                        {
                            GoForward();
                            break;
                        }

                    case Keys.Delete:
                        {
                            break;
                        }
                }
        }

        private void BetterListView1_DoubleClick(object sender, EventArgs e)
        {
            GoForward();
        }

        private void TimerDownloadManager_Tick(object sender, EventArgs e)
        {
            if (betterListView2.Items.Count > 0)
            {
                foreach (ListViewItem item in betterListView2.Items)
                {
                    if (item.SubItems[2].Text != "Finished")
                    {
                        SocketClient client = (SocketClient)item.Tag;
                        string fileSize = item.SubItems[2].Text.Split('/')[0];
                        item.SubItems[2].Text = $"{fileSize}/{Convertor.IntegerToUnitData(client.BytesReceived)}";
                    }
                }
            }
        }

        #endregion


        #region Helper

        /// <summary>
        /// send path of the folder to browse it
        /// </summary>
        private void GetPath(string path)
        {
            PacketFileManager_GetPath packet = new PacketFileManager_GetPath { Path = path };
            ThreadPool.QueueUserWorkItem((o) =>
            {
                SocketClient.Send(packet);
            });
            betterListView1.Enabled = false;
        }

        private void GetDrivers()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                SocketClient.Send(new PacketFileManager_GetDrivers());
            });
        }

        private void GoBack()
        {
            if (!string.IsNullOrWhiteSpace(txtPath.Text))
                if (txtPath.Text.Length > 3)
                {
                    string fullPath = Path.GetDirectoryName(txtPath.Text);           
                    GetPath(fullPath.Replace("\\", "/")); // valid android path
                }
                else
                    GetDrivers();
            else
                GetDrivers();
        }

        private void GoForward()
        {
            // if folder or driver = browse
            if (betterListView1.SelectedItems.Count > 0)
            {
                if (betterListView1.SelectedItems[0].Group.Name == "Folders" || betterListView1.SelectedItems[0].Group.Name == "Drivers")
                {
                    GetPath(betterListView1.SelectedItems[0].ToolTipText);
                    betterListView1.Enabled = false;
                }
                else if(betterListView1.SelectedItems[0].Group.Name == "Files")
                {
                    foreach (ListViewItem file in betterListView1.SelectedItems)
                    {
                        if (file.Group.Name == "Files")
                        {
                            PacketFileManager_DownloadFile packet = new PacketFileManager_DownloadFile
                            {
                                FullPath = file.ToolTipText,
                            };

                            ThreadPool.QueueUserWorkItem((o) =>
                            {
                                SocketClient.Send(packet);
                            });
                        }
                    }
                    tabControl1.SelectedTab = tabPage2;
                }
            }
        }
        #endregion

    }
}
