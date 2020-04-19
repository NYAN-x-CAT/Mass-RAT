namespace Server.Forms
{
    partial class FormMain
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betterListView1 = new Server.Controls.BetterListView();
            this.ipHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.usernameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.osHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileManagerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 36);
            // 
            // fileManagerToolStripMenuItem
            // 
            this.fileManagerToolStripMenuItem.Name = "fileManagerToolStripMenuItem";
            this.fileManagerToolStripMenuItem.Size = new System.Drawing.Size(185, 32);
            this.fileManagerToolStripMenuItem.Text = "File Manager";
            this.fileManagerToolStripMenuItem.Click += new System.EventHandler(this.FileManagerToolStripMenuItem_Click);
            // 
            // betterListView1
            // 
            this.betterListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.betterListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ipHeader,
            this.columnHeader1,
            this.usernameHeader,
            this.osHeader});
            this.betterListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.betterListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.betterListView1.FullRowSelect = true;
            this.betterListView1.HideSelection = false;
            this.betterListView1.Location = new System.Drawing.Point(0, 0);
            this.betterListView1.Name = "betterListView1";
            this.betterListView1.Size = new System.Drawing.Size(897, 439);
            this.betterListView1.TabIndex = 0;
            this.betterListView1.UseCompatibleStateImageBehavior = false;
            this.betterListView1.View = System.Windows.Forms.View.Details;
            // 
            // ipHeader
            // 
            this.ipHeader.Text = "IP Address";
            this.ipHeader.Width = 196;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 130;
            // 
            // usernameHeader
            // 
            this.usernameHeader.Text = "Username";
            this.usernameHeader.Width = 191;
            // 
            // osHeader
            // 
            this.osHeader.Text = "Operating system";
            this.osHeader.Width = 299;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 439);
            this.Controls.Add(this.betterListView1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MassRAT | By NYAN CAT";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader ipHeader;
        private System.Windows.Forms.ColumnHeader usernameHeader;
        private System.Windows.Forms.ColumnHeader osHeader;
        public Controls.BetterListView betterListView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileManagerToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

