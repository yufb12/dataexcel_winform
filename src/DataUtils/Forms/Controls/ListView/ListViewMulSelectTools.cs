using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms; 

namespace Feng.Forms.Controls
{
    public class ListViewMulSelectTools
    {
        System.Windows.Forms.ListView listViewMain;
        private bool Copy = false;
        public void Init(System.Windows.Forms.ListView listView,bool copy)
        {
            listViewMain = listView;
            InitListEvetn();
            InitSort();
            Copy = copy;
        }

        public void InitListEvetn()
        {
            listViewMain.MouseDown += listView1_MouseDown;
            listViewMain.MouseMove += listView1_MouseMove;
            listViewMain.MouseUp += ListViewMain_MouseUp;
            listViewMain.KeyUp += ListViewMain_KeyUp;

        }

        private void ListViewMain_KeyUp(object sender, KeyEventArgs e)
        {      
            try
            {
                if (Copy)
                {
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        foreach (ListViewItem item in listViewMain.SelectedItems)
                        {
                            foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                            {
                                sb.Append("\t" + subitem.Text);
                            }
                            sb.AppendLine();
                        }
                        Feng.Forms.ClipboardHelper.SetText(sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            } 
        }

        private void ListViewMain_MouseUp(object sender, MouseEventArgs e)
        {
            listViewmulselected = false;
            listViewmulselectedpoint = e.Location;
        }

        private bool listViewmulselected = false;
        private Point listViewmulselectedpoint = Point.Empty;
        private List<ListViewItem> listViewItems = new List<ListViewItem>();
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.listViewMain.MultiSelect)
                {
                    listViewItems.Clear();
                    listViewmulselected = true;
                    listViewmulselectedpoint = e.Location;
                }
            }
            else
            {
                listViewmulselected = false;
            }
        }
        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (listViewmulselected)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point point = e.Location;
                    int x = Math.Min(listViewmulselectedpoint.X, point.X);
                    int y = Math.Min(listViewmulselectedpoint.Y, point.Y);
                    Rectangle rect = new Rectangle(x, y,
                Math.Abs(point.X - listViewmulselectedpoint.X),
                    Math.Abs(point.Y - listViewmulselectedpoint.Y));
                    List<ListViewItem> listtemp = new List<ListViewItem>();
                    listtemp.AddRange(listViewItems.ToArray());
                    listViewItems.Clear();
                    foreach (ListViewItem item3 in listViewMain.Items)
                    {
                        if (item3 != null)
                        {
                            if (rect.IntersectsWith(item3.Bounds))
                            {
                                listViewItems.Add(item3);
                                listtemp.Remove(item3);
                                item3.Selected = true;
                            }
                        }
                    }

                    foreach (var item2 in listtemp)
                    {
                        item2.Selected = false;
                    }
                }
            }
        }

        public void InitSort()
        {
            this.listViewMain.Sorting = SortOrder.Ascending;
            //this.listViewMain.ListViewItemSorter = new ListViewItemComparerByName();
            this.listViewMain.ColumnClick += new ColumnClickEventHandler(listView1_ColumnClick);
            //this.listViewMain.ColumnClick += listView1_ColumnClick;
        }
        // 点击对应的列头时，按该列头升序或降序排列列表项
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView lvw = (ListView)sender;
                if (lvw.Sorting == SortOrder.Ascending)
                {
                    lvw.Sorting = SortOrder.Descending;
                }
                else
                {
                    lvw.Sorting = SortOrder.Ascending;
                }

                lvw.ListViewItemSorter = new ListViewItemComparer(e.Column, lvw.Sorting);
                lvw.Sort();
                lvw.ListViewItemSorter = null;
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }
    }


}
