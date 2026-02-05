using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl;
using Feng.Forms.Views;

namespace Feng.Forms.Controls.TreeView
{
    public class DataTreeCell : GridViewCell
    {
        public DataTreeCell(DataTreeRow row)
            : base(row)
        {
        }
        public override bool OnDraw(GraphicsObject g)
        {

            try
            {
                ///////////////////////////代码加在中间 
                DrawRect(g, this.Rect, this.Text);
                /////////////////////////// 
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }
            return false;
        }
        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        { 
            try
            { 
                if (this.Column.Index == 0)
                {
                    DataTreeRow row = this.Row as DataTreeRow;
                    if (row != null)
                    {
                        if (row.Node != null)
                        {
                            Rectangle rect = Feng.Drawing.GraphicsHelper.GetPaddingRectPositionLeft(Rectangle.Round(this.Rect), new Size(16, 16),
                                new Feng.Drawing.Padding() { Left = 16 * (row.Node.Level - 1), Right = 2, Top = (row.Height - 16) / 2 });
                             
                            if (rect.Contains(e.Location))
                            {  
                                row.Node.IsExpanded = !row.Node.IsExpanded; 
                                row.Node.TreeView.RefreshAll();
                                row.Node.TreeView.Invalidate();
                                return true;
                            }

                            if (row.Node.ShowCheckBox == CheckStates.Yes)
                            {
                                rect.Offset(20, 0);
                                if (rect.Contains(e.Location))
                                {
                                    row.Node.Check = !row.Node.Check;
                                    DataTreeNode node = row.Node;
                                    CheckChild(node);
                                    row.Node.TreeView.Invalidate();
                                    return true;

                                } 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }

            return base.OnMouseClick(sender, e, ve);
        }
        public void CheckChild(DataTreeNode node)
        {
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
            {
                return;
            }
            bool uncheck = false;
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
            {
                uncheck = true;
            }
            if (node.Nodes.Count > 0)
            {
                foreach (DataTreeNode item in node.Nodes)
                {
                    if (uncheck)
                    {
                        item.Check = !item.Check;
                    }
                    else
                    {
                        item.Check = node.Check;
                    }
                    CheckChild(item);
                }
            }
            
        }
        public override void DrawRect(Feng.Drawing.GraphicsObject g, RectangleF bounds, object value)
        {

            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            g.Graphics.Clip.Xor(bounds);
            DataTreeViewBase treeview = this.Grid as DataTreeViewBase;
            if (treeview != null)
            {
                DataTreeRow treerow = this.Row as DataTreeRow;
                if (treerow != null)
                {
                    if (treeview.FocusedNode == treerow.Node)
                    { 
                        Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, this.Grid.CellSelectBrush, this.Rect);
                    }
                }
            }
            if (this.Column.Index == 0)
            {
                DataTreeRow row = this.Row as DataTreeRow;
                if (row != null)
                {
                    if (row.Node != null)
                    {
                        Rectangle rect = Feng.Drawing.GraphicsHelper.GetPaddingRectPositionLeft(Rectangle.Round(bounds), new Size(16, 16),
                            new Feng.Drawing.Padding() { Left = 16 * (row.Node.Level - 1), Right = 2, Top = (row.Height - 16) / 2 });
          
                        if (row.Node.Nodes.Count > 0 || row.Node.HasChild)
                        {
                            if (row.Node.IsExpanded)
                            {
                                g.Graphics.DrawImage(Images.Expanded, rect);
                            }
                            else
                            {
                                g.Graphics.DrawImage(Images.Expandno, rect);
                            }
                 
                        }
                        else
                        {
                            g.Graphics.DrawImage(Images.ARROW_STATE_BLUE_FINISH, rect);
                        }
                        if (row.Node.ShowCheckBox == CheckStates.Yes)
                        {
                            rect.Offset(20, 0);
                            if (row.Node.Check)
                            {
                                g.Graphics.DrawImage(Images.CheckStateYes, rect);
                            }
                            else
                            {
                                g.Graphics.DrawImage(Images.CheckStateNo, rect);
                            }
                        }
                        if (row.Node.Image != null)
                        {

                            Rectangle rectimage = new Rectangle(rect.Right + 4, rect.Top, 16, 16);
                            rect.X = rect.X + 20;
                            g.Graphics.DrawImage(row.Node.Image, rectimage);

                        }
                        rect.X = rect.Right;
                        rect.Y = (int)bounds.Y+1;
                        rect.Height =(int) bounds.Height;
                        rect.Width = (int)bounds.Width - rect.Left;
                         
                        DrawCell(g, rect, value);
                    }
                }

            }
            else
            {
                DrawCell(g, bounds, value);
            }

            g.Graphics.Restore(gs);
        }
        private void DrawCell(Feng.Drawing.GraphicsObject g, RectangleF bounds, object value)
        {
            if (this.InEdit)
            {
                return;
            }
            if (value == null)
            {
                return;
            }
            string text = value.ToString();
            if (string.IsNullOrWhiteSpace(text))
                return;
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(
                 this.Column.HorizontalAlignment, this.Column.VerticalAlignment, this.Column.DirectionVertical);

            bounds.Offset(1, 1);
            bounds.Inflate(-1, -1);
            RectangleF rect = bounds;

            Color forecolor = this.Grid.ForeColor;

            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                rect.Location = new PointF(rect.Location.X, rect.Location.Y);
                if (this.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Column.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Column.Font, sb, rect, sf);

                }
            }
        }
    }

    public class DataTreeCellCollection : CellCollection, IList<GridViewCell>
    {
        private List<GridViewCell> list = new List<GridViewCell>();

        public override GridViewCell this[GridViewColumn col]
        {
            get
            {
                foreach (DataTreeCell cell in list)
                {
                    if (cell.Column == col)
                    {
                        return cell;
                    }
                }
                return null;
            }
        }
        public override GridViewCell this[string field]
        {
            get
            {
                foreach (DataTreeCell cell in list)
                {
                    if (cell.Column.FieldName == field)
                    {
                        return cell;
                    }
                }
                return null;
            }
        }
        public override GridViewCell this[int index]
        {
            get
            {
                if (index >= list.Count)
                {
                    return null;
                }
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public override void Add(GridViewCell item)
        {
            list.Add(item);
        }

        public override void Clear()
        {
            list.Clear();
        }

        public override bool Contains(GridViewCell item)
        {
            return list.Contains(item);
        }

        public override void CopyTo(GridViewCell[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public override int Count
        {
            get
            {
                return list.Count;
            }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override bool Remove(GridViewCell item)
        {
            return list.Remove(item);
        }

        public override IEnumerator<GridViewCell> GetEnumerator()
        {
            return list.GetEnumerator();
        }


        public override int IndexOf(GridViewCell item)
        {
            return list.IndexOf(item);
        }

        public override void Insert(int index, GridViewCell item)
        {
            list.Insert(index, item);
        }

        public override void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }


}
