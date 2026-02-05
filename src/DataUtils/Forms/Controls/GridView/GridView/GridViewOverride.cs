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
using Feng.Forms.Events;
using Feng.Forms.Views;

namespace Feng.Forms.Controls.GridControl
{ 
    public partial class GridView  
    {
        
        #region 事件
        [Browsable(false)]
        public Rectangle RowHeaderWidthChangedRect
        {
            get
            {
                return new Rectangle(RowHeaderWidth - sizechangedwidth, 0, sizechangedwidth, this.ColumnHeaderHeight);
            }
        }

        [Browsable(false)]
        public Rectangle ColumnHeaderHeightChangedRect
        {
            get
            {
                return new Rectangle(0, this.ColumnHeaderHeight, RowHeaderWidth, sizechangedwidth);
            }
        }
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            Point pt = PointToClient(e.Location);
            if (this.FocusedCell != null)
            {
                bool has = this.FocusedCell.OnMouseDown(this, e, ve);
                if (!has)
                {
                    this.FocusedCell.EndEdit();
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (this.Rect.Contains(e.Location))
                {
                    bool res = false;
                    if (OnMouseDownColumnSplitSelect(pt))
                    {
                        res = true;
                    }
                    else if (this.ShowRowHeader && RowHeaderWidthChangedRect.Contains(pt))
                    {
                        this.SelectMode = SelectModel_RowHeaderWidthChanged;
                        MouseDownRect = new Rectangle(pt, new Size(RowHeaderWidth, this.ColumnHeaderHeight));
                    }
                    else if (this.ShowColumnHeader && ColumnHeaderHeightChangedRect.Contains(pt))
                    {
                        this.SelectMode = SelectModel_ColumnHeaderHeightChanged;
                        MouseDownRect = new Rectangle(pt, new Size(RowHeaderWidth, this.ColumnHeaderHeight));
                    }
                    else if (this.OperationRect.Contains(pt))
                    {

                        if (this.InDesign)
                        {
                            GridViewColumnDialog frm = new GridViewColumnDialog();
                            frm.Init(this, this.Columns);
                            frm.ShowDialog();
                            RefreshColumnWidth();
                            RefreshColumns();
                            this.RefreshRowValue();
                            RefreshRowHeight();
                            this.Invalidate();
                            return true;
                        }

                    }
                    else if (this.VScroll.Rect.Contains(pt))
                    {
                        this.EndEdit();
                        res = this.VScroll.OnMouseDown(pt);
                    }
                    else if (this.HScroll.Rect.Contains(pt))
                    {
                        this.EndEdit();
                        res = this.HScroll.OnMouseDown(pt);
                    }
                    else if (OnMouseDownCellSelect(sender, e, pt))
                    {
                        res = true;
                    }
                    if (res)
                    {
                        this.Invalidate();
                    }
                    return res;
                }
                else if (OnMouseDownCellSelect(sender, e, pt))
                {
                    this.Invalidate();
                    return true;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (this.RightButtonClickSelect)
                {
                    if (OnMouseDownCellSelect(sender, e, pt))
                    {
                        this.Invalidate();
                        return true;
                    }
                }
            }
            this.EndEdit();
            return false;
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            this.MouseUp();
            this.VScroll.OnMouseUp();
            this.HScroll.OnMouseUp();
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnMouseUp(this, e, ve);
            }
            return false;
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            this.BeginSetCursor(Cursors.Default);
            Point pt = PointToClient(e.Location);
#if DEBUG
            this.tempMoveRect = new Rectangle(pt, new Size(10, 10));
#endif
            bool res = this.VScroll.OnMouseMove(pt);
            if (res)
            {
                this.Invalidate();
                return true;
            }
            res = this.HScroll.OnMouseMove(pt);
            if (res)
            {
                this.Invalidate();
                return true;
            }
            if (e.Button == MouseButtons.Left)
            {
                switch (this.SelectMode)
                {
                    case SelectModel_Null:
                        break;
                    case SelectModel_ColumnSizeChanged:
                        this.BeginSetCursor(Cursors.VSplit);
                        OnMouseMoveColumnSplitWidth(pt);
                        this.Invalidate();
                        return true;
                    case SelectModel_RowHeaderWidthChanged:
                        int width = pt.X - MouseDownRect.X;
                        this.RowHeaderWidth = this.MouseDownRect.Width + width;
                        this.RefreshColumns();
                        this.Invalidate();
                        break;
                    case SelectModel_ColumnHeaderHeightChanged:
                        int heigth = pt.Y - MouseDownRect.Y;
                        this.ColumnHeaderHeight = this.MouseDownRect.Height + heigth;
                        this.RefreshRows();
                        this.ReSetRowHeight();
                        this.Invalidate();
                        break;
                    default:
                        break;
                }
            }
            else if (this.Rect.Contains(e.Location))
            {
                if (this.OperationRect.Contains(pt))
                {
                    this.Invalidate(this.OperationRect);
                    return false;
                }
                else if (OnMouseMoveColumnSplitSelect(pt))
                {
                    this.Invalidate();
                    return true;
                }
                if (this.ShowRowHeader)
                {
                    if (RowHeaderWidthChangedRect.Contains(pt))
                    {

                        this.BeginSetCursor(Cursors.VSplit);
                    }
                }
                if (this.ShowColumnHeader)
                {
                    if (ColumnHeaderHeightChangedRect.Contains(pt))
                    {
                        this.BeginSetCursor(Cursors.HSplit);
                    }
                }

            }

            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnMouseMove(this, e, ve);
            }

            return false;
        }

        public override bool OnMouseLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnMouseLeave(this, e, ve);
            }
            return false;
        }

        public override bool OnMouseHover(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnMouseHover(this, e, ve);
            }
            return false;
        }

        public override bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnMouseEnter(this, e, ve);
            }
            return false;
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.Rect.Contains(e.Location))
            {
                if (this.FocusedCell != null)
                {
                    Point pt = PointToClient(e.Location);
                    if (this.FocusedCell.Rect.Contains(pt))
                    {
                        return this.FocusedCell.OnMouseDoubleClick(this, e, ve);
                    }
                }
            }
            return false;
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.Rect.Contains(e.Location))
            {
                Point pt = PointToClient(e.Location);
                if (this.VScroll.Rect.Contains(pt))
                {
                    this.Invalidate();
                    return this.VScroll.OnMouseClick(pt);
                }
                else if (this.HScroll.Rect.Contains(pt))
                {
                    this.Invalidate();
                    return this.HScroll.OnMouseClick(pt);
                }
                else if (this.ShowColumnHeader && this.ColumnHeaderRect.Contains(pt))
                {
                    if (this.SelectMode == SelectModel_Null)
                    {
                        foreach (GridViewColumn col in this.Columns)
                        {
                            if (col.ColumnHeader.Contains(pt))
                            {
                                ColumnHeaderClick(col);
                                this.RefreshRowValue();
                                RefreshRowHeight();
                                this.Invalidate();
                                return true;
                            }
                        }
                    }
                }
                else if (this.Footer.Rect.Contains(pt))
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        foreach (GridViewColumn col in this.visibleColumns)
                        {
                            if (col.FooterCell.Rect.Contains(pt))
                            {
                                System.Windows.Forms.ContextMenuStrip menu = new ContextMenuStrip();

                                ToolStripItem item = menu.Items.Add("计数");
                                item.Tag = new DataTag(col, TotalMode.Count);
                                item = menu.Items.Add("合计");
                                item.Tag = new DataTag(col, TotalMode.Sum);
                                item = menu.Items.Add("平均");
                                item.Tag = new DataTag(col, TotalMode.Avg);
                                item = menu.Items.Add("最大");
                                item.Tag = new DataTag(col, TotalMode.Max);
                                item = menu.Items.Add("最小");
                                item.Tag = new DataTag(col, TotalMode.Min);
                                menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
                                Point p = this.PointToScreen(e.Location);
                                menu.Show(p);
                            }
                        }
                    }
                }
                else if (this.FocusedCell != null)
                {
                    if (this.FocusedCell.Rect.Contains(pt))
                    {
                        return this.FocusedCell.OnMouseClick(this, e, ve);
                    }
                }

            }
            return false;
        }

        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            try
            {
                DataTag dt = e.ClickedItem.Tag as DataTag;
                if (dt != null)
                {
                    GridViewColumn col = dt.Obj1 as GridViewColumn;
                    if (col != null)
                    {
                        TotalMode tm = (TotalMode)dt.Obj2;
                        switch (tm)
                        {
                            case TotalMode.Null:
                                col.TotalMode = TotalMode.Null;
                                break;
                            case TotalMode.Sum:
                                col.TotalMode = TotalMode.Sum;
                                break;
                            case TotalMode.Avg:
                                col.TotalMode = TotalMode.Avg;
                                break;
                            case TotalMode.Count:
                                col.TotalMode = TotalMode.Count;
                                break;
                            case TotalMode.Max:
                                col.TotalMode = TotalMode.Max;
                                break;
                            case TotalMode.Min:
                                col.TotalMode = TotalMode.Min;
                                break;
                            default:
                                break;
                        }
                        this.Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        public override bool OnMouseCaptureChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnMouseCaptureChanged(this, e, ve);
            }
            return false;
        }

        public override bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                bool has = this.FocusedCell.OnMouseWheel(this, e, ve);
                if (!has)
                {
                    this.FocusedCell.EndEdit();
                }
            }
            if (this.Rect.Contains(e.Location))
            {
                EndEdit();
                int numberOfTextLinesToMove = -1 * e.Delta * this.ScrollStep / 120;

                this.Position = (this.Position + numberOfTextLinesToMove);
                this.Invalidate();
                return true;
            }
            return false;
        }

        public override bool OnClick(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnClick(this, e, ve);
            }
            return false;
        }

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnKeyDown(this, e, ve);
            }
            return false;
        }

        public override bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnKeyPress(this, e, ve);
            }
            return false;
        }

        public override bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnKeyUp(this, e, ve);
            }
            return false;
        }

        public override bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnPreviewKeyDown(this, e, ve);
            }
            return false;
        }

        public override bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.CellDoubleClick != null)
            {
                return CellDoubleClick(this, this.FocusedCell);
            }
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnDoubleClick(this, e, ve);
            }
            return false;
        }

        public override bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnPreProcessMessage(this, ref msg, ve);
            }
            return false;
        }

        public override bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnProcessCmdKey(this, ref msg, keyData, ve);
            }
            return false;
        }

        public override bool OnProcessDialogChar(object sender, char charCode, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnProcessDialogChar(this, charCode, ve);
            }
            return false;
        }

        public override bool OnProcessDialogKey(object sender, Keys keyData, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnProcessDialogKey(this, keyData, ve);
            }
            return false;
        }

        public override bool OnProcessKeyEventArgs(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnProcessKeyEventArgs(this, ref m, ve);
            }
            return false;
        }

        public override bool OnProcessKeyMessage(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnProcessKeyMessage(this, ref m, ve);
            }
            return false;
        }

        public override bool OnProcessKeyPreview(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.OnProcessKeyPreview(this, ref m, ve);
            }
            return false;
        }


        #endregion

    }
 
}

