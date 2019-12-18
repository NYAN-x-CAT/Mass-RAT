using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Server.Controls
{
    public class BetterListView : ListView
    {
        private ListViewColumnSorter LvwColumnSorter { get; set; }
        public void AddItem(string text)
        {
            this.Items.Add(new ListViewItem(text));
        }

        public BetterListView()
        {
            this.View = View.Details;
            this.FullRowSelect = true;
            this.LvwColumnSorter = new ListViewColumnSorter();
            this.ListViewItemSorter = LvwColumnSorter;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetWindowTheme(this.Handle, "explorer", null);
        }

        //protected override void OnInvalidated(InvalidateEventArgs e)
        //{
        //    if (this.Columns.Count == 1)
        //    {
        //        Columns[0].Width = this.Width - 4;
        //    }
        //    base.OnInvalidated(e);
        //}

        //protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        //{
        //    if (this.Columns.Count == 1)
        //    {
        //        e.Cancel = true;
        //        e.NewWidth = this.Columns[e.ColumnIndex].Width;
        //    }
        //    else

        //        base.OnColumnWidthChanging(e);
        //}

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            base.OnColumnClick(e);

            if (e.Column == this.LvwColumnSorter.SortColumn)
            {
                this.LvwColumnSorter.Order = (this.LvwColumnSorter.Order == SortOrder.Ascending)
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }
            else
            {
                this.LvwColumnSorter.SortColumn = e.Column;
                this.LvwColumnSorter.Order = SortOrder.Ascending;
            }

            if (!this.VirtualMode)
                this.Sort();
        }

        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string textSubAppName, string textSubIdList);
    }
}
