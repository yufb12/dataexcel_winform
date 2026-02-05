using System;
using System.Collections;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace Feng.Forms.Controls
{
    public class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            return Compare(x as ListViewItem, y as ListViewItem);
        }
        public int Compare(ListViewItem x, ListViewItem y)
        {
            int returnVal = -1;
            if (x.SubItems.Count != y.SubItems.Count)
            {
                return 0;
            }
            ListViewSubItem subItemx = x.SubItems[col];
            ListViewSubItem subItemy = y.SubItems[col];
            if (order == SortOrder.Ascending)
            {
                if (subItemx.Tag is DateTime)
                {
                    returnVal = DateTime.Compare(((DateTime)subItemx.Tag), ((DateTime)subItemy.Tag));
                }
                else if (subItemx.Tag is int)
                {
                    returnVal = ((int)subItemx.Tag).CompareTo((int)subItemy.Tag);
                }
                else
                {
                    returnVal = String.Compare(subItemx.Text, subItemy.Text);
                }
            }
            else if (order == SortOrder.Descending)
            {
                if (subItemx.Tag is DateTime)
                {
                    returnVal = DateTime.Compare(((DateTime)subItemy.Tag), ((DateTime)subItemx.Tag));
                }
                else if (subItemx.Tag is int)
                {
                    returnVal = ((int)subItemy .Tag).CompareTo((int)subItemx.Tag);
                }
                else
                {
                    returnVal = String.Compare(subItemy.Text, subItemx.Text);
                }
            }
            return returnVal;
        }
    }
}
