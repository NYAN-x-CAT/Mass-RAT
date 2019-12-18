namespace Server.Forms
{
    partial class FormFileManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFileManager));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Folders", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Files", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Drivers", System.Windows.Forms.HorizontalAlignment.Left);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.iconArrowBack = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.iconArrowForward = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtError = new System.Windows.Forms.ToolStripStatusLabel();
            this.betterListView1 = new Server.Controls.BetterListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.betterListView2 = new Server.Controls.BetterListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerDownloadManager = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1023, 556);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.statusStrip2);
            this.tabPage1.Controls.Add(this.statusStrip1);
            this.tabPage1.Controls.Add(this.betterListView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1015, 523);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browser";
            // 
            // statusStrip2
            // 
            this.statusStrip2.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel5,
            this.iconArrowBack,
            this.toolStripStatusLabel3,
            this.iconArrowForward,
            this.toolStripStatusLabel4,
            this.txtPath});
            this.statusStrip2.Location = new System.Drawing.Point(3, 3);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1009, 32);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 5;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(17, 25);
            this.toolStripStatusLabel5.Text = " ";
            // 
            // iconArrowBack
            // 
            this.iconArrowBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.iconArrowBack.Image = ((System.Drawing.Image)(resources.GetObject("iconArrowBack.Image")));
            this.iconArrowBack.Name = "iconArrowBack";
            this.iconArrowBack.Size = new System.Drawing.Size(24, 25);
            this.iconArrowBack.Text = "toolStripStatusLabel1";
            this.iconArrowBack.Click += new System.EventHandler(this.IconArrowBack_Click);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(17, 25);
            this.toolStripStatusLabel3.Text = " ";
            // 
            // iconArrowForward
            // 
            this.iconArrowForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.iconArrowForward.Image = ((System.Drawing.Image)(resources.GetObject("iconArrowForward.Image")));
            this.iconArrowForward.Name = "iconArrowForward";
            this.iconArrowForward.Size = new System.Drawing.Size(24, 25);
            this.iconArrowForward.Text = "toolStripStatusLabel2";
            this.iconArrowForward.Click += new System.EventHandler(this.IconArrowForward_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(27, 25);
            this.toolStripStatusLabel4.Text = "   ";
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(18, 25);
            this.txtPath.Text = "..";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtError});
            this.statusStrip1.Location = new System.Drawing.Point(3, 492);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1009, 28);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.ForeColor = System.Drawing.Color.Red;
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(18, 21);
            this.txtError.Text = "..";
            // 
            // betterListView1
            // 
            this.betterListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.betterListView1.BackColor = System.Drawing.SystemColors.Control;
            this.betterListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.betterListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.betterListView1.Enabled = false;
            this.betterListView1.FullRowSelect = true;
            listViewGroup1.Header = "Folders";
            listViewGroup1.Name = "Folders";
            listViewGroup2.Header = "Files";
            listViewGroup2.Name = "Files";
            listViewGroup3.Header = "Drivers";
            listViewGroup3.Name = "Drivers";
            this.betterListView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.betterListView1.HideSelection = false;
            this.betterListView1.LargeImageList = this.imageList1;
            this.betterListView1.Location = new System.Drawing.Point(3, 38);
            this.betterListView1.Name = "betterListView1";
            this.betterListView1.Size = new System.Drawing.Size(1008, 449);
            this.betterListView1.SmallImageList = this.imageList1;
            this.betterListView1.TabIndex = 3;
            this.betterListView1.TileSize = new System.Drawing.Size(300, 100);
            this.betterListView1.UseCompatibleStateImageBehavior = false;
            this.betterListView1.View = System.Windows.Forms.View.Tile;
            this.betterListView1.DoubleClick += new System.EventHandler(this.BetterListView1_DoubleClick);
            this.betterListView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BetterListView1_KeyDown);
            this.betterListView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BetterListView1_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.betterListView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1015, 523);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Download Manager";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // betterListView2
            // 
            this.betterListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader11,
            this.columnHeader10});
            this.betterListView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.betterListView2.FullRowSelect = true;
            this.betterListView2.HideSelection = false;
            this.betterListView2.Location = new System.Drawing.Point(3, 3);
            this.betterListView2.Name = "betterListView2";
            this.betterListView2.Size = new System.Drawing.Size(1009, 517);
            this.betterListView2.TabIndex = 0;
            this.betterListView2.UseCompatibleStateImageBehavior = false;
            this.betterListView2.View = System.Windows.Forms.View.Details;
            this.betterListView2.DoubleClick += new System.EventHandler(this.BetterListView2_DoubleClick);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Filename";
            this.columnHeader9.Width = 557;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Type";
            this.columnHeader11.Width = 210;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Size";
            this.columnHeader10.Width = 140;
            // 
            // timerDownloadManager
            // 
            this.timerDownloadManager.Enabled = true;
            this.timerDownloadManager.Interval = 1000;
            this.timerDownloadManager.Tick += new System.EventHandler(this.TimerDownloadManager_Tick);
            // 
            // FormFileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1023, 556);
            this.Controls.Add(this.tabControl1);
            this.Location = new System.Drawing.Point(0, 1);
            this.Name = "FormFileManager";
            this.Text = "FormFileManager ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFileManager_FormClosed);
            this.Load += new System.EventHandler(this.FormFileManager_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel iconArrowBack;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel iconArrowForward;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel txtPath;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtError;
        private Controls.BetterListView betterListView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private Controls.BetterListView betterListView2;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Timer timerDownloadManager;
    }
}