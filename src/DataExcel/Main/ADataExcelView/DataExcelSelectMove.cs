using Feng.Enums;
using Feng.Excel.Actions;
using Feng.Excel.Args;
using Feng.Excel.Collections;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Forms;
using Feng.Forms.Command;
using Feng.Forms.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    { 
 
        #region 系统重载
         
         
        public void Focus()
        { 
        }
 
         

        private static System.Windows.Forms.Cursor _DefaultCross = null;
        public static System.Windows.Forms.Cursor DefaultCross
        {
            get
            {
                if (_DefaultCross == null)
                {

                    Assembly assembly = typeof(DataExcel).Assembly;

                    Stream fs = assembly.GetManifestResourceStream(@"Feng.Excel.Image.cross.png");

                    System.Drawing.Bitmap d = new Bitmap(fs);
                    System.Windows.Forms.Cursor dc = new System.Windows.Forms.Cursor(d.GetHicon());

                    if (dc == null)
                    {
                        _DefaultCross = System.Windows.Forms.Cursors.Cross;
                    }
                    else
                    {
                        _DefaultCross = dc;
                    }
                }
                return _DefaultCross;
            }
        }
        private Point MousePoint = Point.Empty;
 
        public virtual bool OnMouseDownScroll(MouseEventArgs e)
        {
            bool res = false;
            res = this.HScroll.OnMouseDown(this, e);
            if (res)
            {
                return res;
            }
            res = this.VScroll.OnMouseDown(this, e);
            if (res)
            {
                return res;
            }
            return false;
        }
        public virtual bool OnMouseUpScroll(MouseEventArgs e)
        {
            bool res = false;
            res = this.HScroll.OnMouseUp(this, e);
            if (res)
            {
                return res;
            }
            res = this.VScroll.OnMouseUp(this, e);
            if (res)
            {
                return res;
            }
            return false;
        }
        public virtual bool OnMouseMoveScroll(MouseEventArgs e)
        {
            bool res = false;
            res = this.HScroll.OnMouseMove(this, e);
            if (res)
            {
                return res;
            }
            res = this.VScroll.OnMouseMove(this, e);
            if (res)
            {
                return res;
            }
            return false;
        }
        public virtual void ChangedSize(MouseEventArgs e)
        {


            Point location = this.PointToClient(System.Windows.Forms.Control.MousePosition);// e.Location;
            Size sf = new Size(location.X - MouseDownPoint.X, location.Y - MouseDownPoint.Y);
 
            switch (this.SizeChangMode)
            {
                case SizeChangMode.Null:
                    break;
                case SizeChangMode.TopLeft:
                    this.Width = this.MouseDownSize.Width - sf.Width;
                    this.Height = this.MouseDownSize.Height - sf.Height;
                    this.Top = location.Y;// this.MouseDownPoint.Y + sf.Height;
                    this.Left = location.X;// this.MouseDownPoint.X + sf.Width;

                    break;
                case SizeChangMode.TopRight:
                    this.Width = this.MouseDownSize.Width + sf.Width;
                    this.Height = this.MouseDownSize.Height - sf.Height;
                    this.Top = location.Y;// this.MouseDownPoint.Y + sf.Height;

                    break;
                case SizeChangMode.MidLeft:
                    this.Width = this.MouseDownSize.Width - sf.Width;
                    this.Left = this.MouseDownPoint.X + sf.Width;
                    break;
                case SizeChangMode.MidRight:
                    this.Width = this.MouseDownSize.Width + sf.Width;
                    break;
                case SizeChangMode.BoomLeft:
                    this.Width = this.MouseDownSize.Width - sf.Width;
                    this.Height = this.MouseDownSize.Height + sf.Height;
                    this.Left = this.MouseDownPoint.X + sf.Width;

                    break;
                case SizeChangMode.BoomRight:

                    this.Width = this.MouseDownSize.Width + sf.Width;
                    this.Height = this.MouseDownSize.Height + sf.Height;
                    break;
                case SizeChangMode.MidTop:
                    this.Height = this.MouseDownSize.Height - sf.Height;
                    this.Top = this.MouseDownPoint.Y + sf.Height;
                    break;
                case SizeChangMode.MidBoom:
                    this.Height = this.MouseDownSize.Height + sf.Height;
                    break;
                default:
                    break;
            }
        }
         
         
        public void DoubleClear()
        {
            this.CopyCells = null;
        }
 
         
 
        public void AutoShowScroll()
        {
            if (this.AutoShowScroller)
            {
                if (this.ShowHorizontalScroller)
                {
                    AutoShowHScroll();
                }
                if (this.ShowVerticalScroller)
                {
                    AutoShowVScroll();
                }
            }
        }

        public void AutoShowVScroll()
        {
            if (this.BackGroundMode)
                return;
            int height = 0;
            foreach (IRow row in this.Rows)
            {
                if (row.Index > this.MaxRow)
                {
                    continue;
                }
                if (row.Visible)
                {
                    height = height + row.Height;
                }
                if (height > this.Height)
                {
                    break;
                }
            } 
        }

        public void AutoShowHScroll()
        {
            if (this.BackGroundMode)
                return;
            int width = 0; 
            foreach (IColumn col in this.Columns)
            {
                if (col.Index > this.MaxColumn)
                {
                    continue;
                }
                width = width + col.Width;
                if (width > this.Width)
                {
                    break;
                }
            } 
        }

        public void ScrollerSizeChanged()
        {
            if (this.BackGroundMode)
                return; 
            //if (this.HScroller.Visible)
            //{
            //    this.VScroller.Location = new Point(this.Width - this.VScroller.Width - 1, 0);
            //    this.VScroller.Height = this.Height - this.VScroller.Width;
            //}
            //else
            //{
            //    this.VScroller.Location = new Point(this.Width - this.VScroller.Width - 1, 0);
            //    this.VScroller.Height = this.Height;
            //}
            //if (this.VScroller.Visible)
            //{
            //    this.HScroller.Location = new Point(0, this.Height - this.HScroller.Height - 1);
            //    this.HScroller.Width = this.Width - this.VScroller.Width;
            //}
            //else
            //{
            //    this.HScroller.Location = new Point(0, this.Height - this.HScroller.Height - 1);
            //    this.HScroller.Width = this.Width;
            //}
        }

 
         
        
        public virtual void Dispose(bool disposing)
        {
 
        }
 
        private static Feng.Forms.Caret Caret = null;
        public virtual void ShowCaret(int heigth , int x , int y)
        {
            if (Caret == null)
            {
                Caret = new Feng.Forms.Caret();
                Caret.Handle = this.Handle;
            }
            if (Caret != null)
            {
                MousePoint.X = x;
                MousePoint.Y = y;
                Point ptt = MousePoint;
                Caret.Show(this.Handle, 1, heigth, ptt.X, ptt.Y);
            }
        }
         
 
        public void InsertText(string text)
        {
            if (this.FocusedCell != null)
            {
                if (this.FocusedCell.OwnEditControl != null)
                {
                    this.FocusedCell.OwnEditControl.TextPress(text);
                }
            }
        }
 
        private Feng.Forms.Command.CompositeKey2 compositekey2 = null;
        public Feng.Forms.Command.CompositeKey2 CompositeKeys2
        {
            get
            {
                if (compositekey2 == null)
                {
                    compositekey2 = new Feng.Forms.Command.CompositeKey2();
                }
                return compositekey2;
            }
        }
        private Keys lastkey = Keys.None;
        public bool CompositeKeyEvents(Keys KeyData)
        {  
            if (KeyData == Keys.Shift)
            {
                return false;
            }
            if (KeyData == Keys.Alt)
            {
                return false;
            }
            if (KeyData == Keys.ControlKey)
            {
                return false;
            }
            if (KeyData == (Keys.Control| Keys.ControlKey))
            {
                return false;
            }
            if (KeyData == (System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control))
            {
                return false;
            }
            if (KeyData == (System.Windows.Forms.Keys.ShiftKey | System.Windows.Forms.Keys.Control))
            {
                return false;
            }
            if (KeyData == (System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control))
            {
                return false;
            }
            if (KeyData == (System.Windows.Forms.Keys.Capital))
            {
                return false;
            }
            int c = (int)KeyData;
            if (c >= (int)Keys.D0 && c <= (int)Keys.Z)
            {

            }
            Keys ckey = (KeyData & Keys.Control); 
            Keys ckey2 = (KeyData & Keys.ControlKey);
            ////Feng.Utils.TraceHelper.TraceShow("CompositeKeyEvents");
            //Feng.Utils.TraceHelper.WriteTrace("DataExcel", "CompositeKeyEvents", "KeyData:", KeyData.ToString());
            //Feng.Utils.TraceHelper.WriteTrace("DataExcel", "CompositeKeyEvents", "ckey   :", ckey.ToString());
            //Feng.Utils.TraceHelper.WriteTrace("DataExcel", "CompositeKeyEvents", "ckey2  :", ckey2.ToString());
            //if (KeyData.ToString() == "C")
            //{

            //}

            CommandObject cmd = null;
            if (lastkey == Keys.None)
            {
                lastkey = KeyData;
                cmd = CompositeKeys2.GetCommand(KeyData);
            }
            else
            {
                cmd = CompositeKeys2.GetCommand(lastkey, KeyData);
            }
            if (cmd == null)
            {
                lastkey = KeyData;
                cmd = CompositeKeys2.GetCommand(KeyData);
            }
            if (cmd != null)
            {
                CommandExcute(cmd);
                //lastkey = KeyData;
                return true;
            }
            else
            {
                //CompositeKeys.LastCompsite.Key = null;
                //CompositeKeys.LastCompsite.Value = null;
                lastkey = KeyData;
            }
            return false;

        }
 
        public void PasteSelectCells(SelectCellCollection cells, int rowindex, int columnindex)
        {
            int minrow = cells.MinRow();
            int maxrow = cells.MaxRow();
            int mincol = cells.MinColumn();
            int maxcol = cells.MaxColumn();


        }

        public void PasteCell(ICell cell, int rowindextarget, int columnindextarget)
        {
            IMergeCell mcell = cell as IMergeCell;
            if (mcell != null)
            {
                ICell cellcurrent = this[rowindextarget, columnindextarget];
                if (cellcurrent != null)
                {
                    cellcurrent.OwnMergeCell = mcell;
                }
            }
        }

        public void ClearSelectCells(ISelectCellCollection cells)
        {
            List<ICell> list = cells.GetAllCells();
            foreach (ICell cell in list)
            {
                RemoveCell(cell);
            }
        }
 
        public void RemoveCell(ICell cell)
        {
            if (cell != null)
            {
                cell.Row.Cells.Remove(cell);
            }
        }
 
        public void SetSelectCellsRowIndex(ISelectCellCollection tagetsels, int index)
        {
            int minrow = tagetsels.MinRow();
            int maxrow = tagetsels.MaxRow();


            if (index > minrow)
            {
                minrow = index;
            }
            if (index < maxrow)
            {
                maxrow = index;
            }

            ICell cell1 = this[minrow, tagetsels.MinColumn()];
            ICell cell2 = this[maxrow, tagetsels.MaxColumn()];
            this.SelectCells.BeginCell = cell1;
            this.SelectCells.EndCell = cell2;
        }


        public ICell GetAboveCell(ISelectCellCollection selcells, int index)
        {
            ICell abovecell = null;
            int minrow = selcells.MinRow();
            int mincolumn = selcells.MinColumn();
            if ((minrow - index) > 0)
            {
                abovecell = this[minrow - index, mincolumn];
            }
            return abovecell;
        }
        public ICell GetBottomCell(ISelectCellCollection selcells, int index)
        {
            ICell abovecell = null;
            int row = selcells.MaxRow();
            int mincolumn = selcells.MinColumn();

            abovecell = this[row + index, mincolumn];

            return abovecell;
        }
        public ICell GetLeftCell(ISelectCellCollection selcells, int index)
        {
            ICell abovecell = null;
            int row = selcells.MaxRow();
            int mincolumn = selcells.MinColumn();

            abovecell = this[index, mincolumn - index];

            return abovecell;
        }
        public ICell GetRightCell(ISelectCellCollection selcells, int index)
        {
            ICell abovecell = null;
            int row = selcells.MinRow();
            int column = selcells.MaxColumn();

            abovecell = this[row, column + index];

            return abovecell;
        }

        public ICell GetRightCell(ICell cell)
        {
            ICell cellresult = null;
            int row = cell.Row.Index;
            int column = cell.Column.Index;
            column = column + 1;
            if (column < 1)
            {
                return null;
            }

            cellresult = this[row, column];

            return cellresult;
        }
        public ICell GetLeftCell(ICell cell)
        {
            ICell cellresult = null;
            int row = cell.Row.Index;
            int column = cell.Column.Index;
            column = column - 1;
            if (column < 1)
            {
                return null;
            }

            cellresult = this[row, column];
            if (cellresult.OwnMergeCell!=null)
            {
                return cellresult.OwnMergeCell;
            }

            return cellresult;
        }

        public ICell GetAboveCell(ICell cell)
        {
            ICell abovecell = null;
            int minrow = cell.Row.Index;
            int mincolumn = cell.Column.Index;
            if (minrow > 0)
            {
                abovecell = this[minrow - 1, mincolumn];
            }
            return abovecell;
        }

        public ICell GetDownCell(ICell cell)
        {
            ICell abovecell = null;
            int minrow = cell.Row.Index;
            int mincolumn = cell.Column.Index;
            if (minrow > 0)
            {
                abovecell = this[minrow + 1, mincolumn];
            }
            return abovecell;
        }
        #endregion

        #region 鼠标移动
        private void SCellRangeFunctionCellSelectedMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point location = viewloaction;
            if (this.FunctionSelectCells != null)
            {
                if (this.FunctionSelectCells.BeginCell.Row.Index > 0 && this.FunctionSelectCells.BeginCell.Column.Index > 0)
                {
                    if (!this.ClientBounds.Contains(viewloaction))
                    {
                        if (this.AllowChangedFirstDisplayRow)
                        {
                            location = SetFirstShowIndex(viewloaction);
                        }
                    }
                }
            }

            foreach (IRow r in this.AllVisibleRows)
            {
                if (r.Rect.Contains(location))
                {
                    foreach (IColumn c in this.AllVisibleColumns)
                    {
                        if (c.Rect.Contains(location))
                        {
                            ICell cell = null;
                            if (r != null)
                            {
                                cell = r.Cells[c.Index];
                            }
                            if (cell == null)
                            {
                                cell = this.ClassFactory.CreateDefaultCell(this, r.Index, c.Index);
                            }
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            if (this.FunctionSelectCells.EndCell != cell)
                            {
                                this.FunctionSelectCells.EndCell = cell;
                            }
                            this.OnFunctionSelectCellChanged(this.FunctionSelectCells);
                            return;
                        }
                    }
                    break;
                }
            }
        }

        private void SelectModeCellSeletedMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point location = viewloaction;
            if (this.SelectCells != null)
            {
                if (this.SelectCells.BeginCell.Row.Index > 0 && this.SelectCells.BeginCell.Column.Index > 0)
                {
                    if (!this.ClientBounds.Contains(location))
                    {
                        if (this.AllowChangedFirstDisplayRow)
                        {
                            location = SetFirstShowIndex(location);
                        }
                    }
                }
            }

            foreach (IRow r in this.AllVisibleRows)
            {
                if (r.Rect.Contains(location))
                {
                    foreach (IColumn c in this.AllVisibleColumns)
                    {
                        if (c.Rect.Contains(location))
                        {
                            ICell cell = null;
                            if (r != null)
                            {
                                cell = r.Cells[c.Index];
                            }
                            if (cell == null)
                            {
                                cell = this.ClassFactory.CreateDefaultCell(this, r.Index, c.Index);
                            }
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            if (this.SelectCells.EndCell != cell)
                            {
                                this.SelectCells.EndCell = cell;
                            }
                            //需要优化
                            this.OnCellSelectChanged(this.SelectCells);
                            OnCellMouseMove(cell, e);

                            return;
                        }
                    }
                    break;
                }
            }
        }
 
        private void SetSelectCellRowSelect()
        {
            this.SelectRows.Clear();
            this.SelectColumns.Clear();
            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinRow();
                int maxrow = this.SelectCells.MaxRow();
                int mincolumn = this.SelectCells.MinColumn();
                int maxcolumn = this.SelectCells.MaxColumn();
                if (minrow == 0 && maxrow == 0)
                {
                    for (int i = mincolumn; i <= maxcolumn; i++)
                    {
                        this.Columns[i].Selected = true;
                    }
                }
                if (mincolumn == 0 && maxcolumn == 0)
                {
                    for (int i = minrow; i <= maxrow; i++)
                    {
                        this.Rows[i].Selected = true;
                    }
                }
            }
        }

        private void SelectModeColumnHeaderSelectedMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point pt = new Point(viewloaction.X, this.TopSideHeight / 2);
            #region SelectMode=Null
            Rectangle rectf = new Rectangle(this.LeftSideWidth, 0, this.Width, this.TopSideHeight);
            if (rectf.Contains(pt))
            {
                foreach (IColumn rhc in this.AllVisibleColumns)
                {
                    if (rhc.Rect.Contains(pt))
                    {
                        return;
                    }
                }
            }


            #endregion

        }

        private void SelectModeRowHeaderSelectedMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point pt = new Point(this.LeftSideWidth / 2, viewloaction.Y);
            #region SelectMode=Null
            Rectangle rectf = new Rectangle(0, this.TopSideHeight, this.LeftSideWidth, this.Height);
            if (rectf.Contains(pt))
            {
                foreach (IRow rhc in this.AllVisibleRows)
                {
                    if (rhc.Rect.Contains(pt))
                    {
                        return;
                    }
                }
            }


            #endregion

        }

        private void SelectModeMergeCellAddSelectedMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point location = SetFirstShowIndex(viewloaction);
            ICell cell = GetCellByPoint(location);
            this._MergeCellCollectionAddRect.EndCell = cell;
        } 

        private void SelectModeMergeCellSelectedMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            if (this._MergeCellCollectionRect.FirstMergeCell.Rect.Contains(viewloaction))
            {
                this._MergeCellCollectionRect.FirstMergeCell = this._MergeCellCollectionRect.FirstMergeCell;
                return;
            }
            Point location = SetFirstShowIndex(viewloaction);
            ICell cell = GetCellByPoint(location);

            this._MergeCellCollectionRect.EndCell = cell;
        }

        private void TextCellSizeRectSelected(MouseEventArgs e)
        {
#if MouseMoveControl
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
            {
                Feng.Utils.TraceHelper.WriteTrace("SelectModeImageCellSeletedMouseMoveKeysControl");
            }
#endif
            Point viewloaction = this.PointControlToView(e.Location);
            Point location = viewloaction;
            Size sf = new Size(location.X - MouseDownPoint.X, location.Y - MouseDownPoint.Y);
            switch (ExtendCell.SizeChangMode)
            {
                case SizeChangMode.Null:
                    break;
                case SizeChangMode.TopLeft:
                    ExtendCell.Top = ExtendCell.MouseDownPoint.Y + sf.Height;
                    ExtendCell.Left = ExtendCell.MouseDownPoint.X + sf.Width;
                    ExtendCell.Width = ExtendCell.MouseDownSize.Width - sf.Width;
                    ExtendCell.Height = ExtendCell.MouseDownSize.Height - sf.Height;
                    break;
                case SizeChangMode.TopRight:
                    ExtendCell.Top = ExtendCell.MouseDownPoint.Y + sf.Height;
                    ExtendCell.Width = ExtendCell.MouseDownSize.Width + sf.Width;
                    ExtendCell.Height = ExtendCell.MouseDownSize.Height - sf.Height;
                    break;
                case SizeChangMode.MidLeft:
                    ExtendCell.Left = ExtendCell.MouseDownPoint.X + sf.Width;
                    ExtendCell.Width = ExtendCell.MouseDownSize.Width - sf.Width;
                    break;
                case SizeChangMode.MidRight:
                    ExtendCell.Width = ExtendCell.MouseDownSize.Width + sf.Width;
                    break;
                case SizeChangMode.BoomLeft:
                    ExtendCell.Left = ExtendCell.MouseDownPoint.X + sf.Width;
                    ExtendCell.Width = ExtendCell.MouseDownSize.Width - sf.Width;
                    ExtendCell.Height = ExtendCell.MouseDownSize.Height + sf.Height;
                    break;
                case SizeChangMode.BoomRight:

                    ExtendCell.Width = ExtendCell.MouseDownSize.Width + sf.Width;
                    ExtendCell.Height = ExtendCell.MouseDownSize.Height + sf.Height;
                    break;
                case SizeChangMode.MidTop:
                    ExtendCell.Top = ExtendCell.MouseDownPoint.Y + sf.Height;
                    ExtendCell.Height = ExtendCell.MouseDownSize.Height - sf.Height;
                    break;
                case SizeChangMode.MidBoom:
                    ExtendCell.Height = ExtendCell.MouseDownSize.Height + sf.Height;
                    break;
                default:
                    break;
            }



        }

        private void ImageCellSizeRectSelected(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point location = viewloaction;
            Size sf = new Size(MouseDownPoint.X - location.X, MouseDownPoint.Y - location.Y);
            switch (ExtendCell.SizeChangMode)
            {
                case SizeChangMode.Null:
                    break;
                case SizeChangMode.TopLeft:
                    ExtendCell.Top = ExtendCell.Top + sf.Height;
                    ExtendCell.Left = ExtendCell.Left + sf.Width;
                    break;
                case SizeChangMode.TopRight:
                    break;
                case SizeChangMode.MidLeft:
                    break;
                case SizeChangMode.MidRight:
                    break;
                case SizeChangMode.BoomLeft:
                    break;
                case SizeChangMode.BoomRight:
                    break;
                case SizeChangMode.MidTop:
                    break;
                case SizeChangMode.MidBoom:
                    break;
                default:
                    break;
            }
        }

        private void SelectModeTextMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point location = viewloaction;
            ExtendCell.Left = location.X - ExtendCell.MouseDownPoint.X;
            ExtendCell.Top = location.Y - ExtendCell.MouseDownPoint.Y;

        }

        private void SelectModeImageMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point location = viewloaction;
            ExtendCell.Left = location.X - ExtendCell.MouseDownPoint.X;
            ExtendCell.Top = location.Y - ExtendCell.MouseDownPoint.Y;


        }

        public void SelectModeCellAddSelectedMouseMove(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point pt = viewloaction;
            Point location = SetFirstShowIndex(pt);
#if DEBUG
            if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
            {

            }
#endif
            #region SelectMode=Null
            ICell endcell = null;
            foreach (IRow r in this.VisibleRows)
            {
                if (r.Rect.Contains(location))
                {
                    foreach (IColumn c in this.VisibleColumns)
                    {
                        if (c.Rect.Contains(location))
                        {
                            ICell cell = null;

                            cell = r.Cells[c.Index];
                            if (cell == null)
                            {
                                cell = this.ClassFactory.CreateDefaultCell(this, r.Index, c.Index);
                            }

                            endcell = cell;
                            break;
                        }
                    }
                    break;
                }
            }
            if (endcell != null)
            {
                this._SelectAddRectCollection.EndCell = endcell;
                if (CellAddRectangleMouseMove != null)
                {
                    CellAddRectangleMouseMove(this, viewloaction, endcell, this._SelectCells);
                }
            }
            else
            {
                this._SelectAddRectCollection.EndCell = this._SelectAddRectCollection.EndCell;
            }
            #endregion

        }

        #endregion

        #region 鼠标点击
        public virtual bool SetScrollerMouseDown(MouseEventArgs e)
        { 
            return false;
        }

        public virtual bool ScrollerMouseMove(MouseEventArgs e)
        { 
            return false;
        }

        public virtual bool ScrollerDoubleMouseClick(MouseEventArgs e)
        { 
            return false;
        }

        public virtual bool SetExtendCellMouseDown(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point pt = viewloaction;
            if (this.ListExtendCells != null)
            {
                for (int i = 0; i < this.ListExtendCells.Count; i++)
                {
                    IExtendCell itc = this.ListExtendCells[i];

                    if (itc.MouseDown(pt))
                    {
                        this.BeginReFresh();
                        ClearSelect();
                        ExtendCell = itc;
                        Selectmode = SelectMode.ExtendCellSizeRectSelected;
                        this.EndReFresh();
                        return true;
                    }
                    if (itc.Rect.Contains(pt))
                    {
                        this.BeginReFresh();
                        ClearSelect();
                        Selectmode = SelectMode.TextCellSelected;
                        itc.Selected = true;
                        ExtendCell = itc;
                        this.CellEvent = itc;
                        itc.MouseDownPoint = new Point(pt.X - itc.Left, pt.Y - itc.Top);
                        this.EndReFresh();
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual bool SetDataExcelMouseDown(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            _MouseDownPoint = viewloaction;
            Point pt = viewloaction;
            _MouseDownsize = new Size(this.Width, this.Height);


            bool result = false;
            if (this.TopLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.TopLeft;
                result = true;
                goto exitfunction;
            }
            if (this.TopRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.TopRight;
                result = true;
                goto exitfunction;
            }
            if (this.BottomLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.BoomLeft;
                result = true;
                goto exitfunction;
            }
            if (this.BottomRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.BoomRight;
                result = true;
                goto exitfunction;
            }
            if (this.MidTop.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidTop;
                result = true;
                goto exitfunction;
            }
            if (this.MidBottom.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidBoom;
                result = true;
                goto exitfunction;
            }
            if (this.MidLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidLeft;
                result = true;
                goto exitfunction;
            }
            if (this.MidRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidRight;
                result = true;
                goto exitfunction;
            }
        exitfunction:
            if (result)
            {
                this.Selectmode = SelectMode.ChangedSize;

            }
            return result;
        }

        public virtual bool SetMergeCellCollectionRectMouseDown(MouseEventArgs mes)
        { 
 
            return false;
        }

        public virtual bool SetSelectCellCollectionMouseDown(MouseEventArgs e)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point pt = viewloaction;
            if (this.ShowSelectAddRect)
            {
                if (this._SelectCells != null)
                {
                    if (this._SelectCells.AddRectContains(pt))
                    {
                        this.BeginReFresh();
                        this.BeginSetCursor(DataExcel.DefaultCross);
                        if (_SelectAddRectCollection == null)
                        {
                            _SelectAddRectCollection = new SelectAddRectCollection(this);
                        }
                        this._SelectAddRectCollection.SelectCellCollection = this._SelectCells;

                        Selectmode = SelectMode.CellAddSelected;

                        if (this.CellAddRectangleClick != null)
                        {
                            this.CellAddRectangleClick(this, _SelectCells);
                        }
                        this.EndReFresh();
                        return true;
                    }
                }
            }
            return false;
        }
         
        public virtual bool SetCellMouseDown(MouseEventArgs e, EventViewArgs ve)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            Point pt = viewloaction;
            pt.X = pt.X / this.Zoom;
            pt.Y = pt.Y / this.Zoom;


            ICell cell = GetCellByPoint(pt);
            if (cell != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (this.SelectCells != null)
                    {
                        if (this.SelectCells.Rect.Contains(pt))
                        {
                            return true;
                        }
                    }
                }

                if (BeforeSelectCellChanged != null)
                {
                    CellChangedArgs args = new CellChangedArgs();
                    args.Cell = cell;
                    BeforeSelectCellChanged(this, args);
                    if (args.Cancel)
                    {
                        return true;
                    }
                }

                if (Selectmode != SelectMode.Null)
                {

                    if (Selectmode == SelectMode.CellRangeFunctionCellSelected)
                    {
                        this.BeginReFresh();
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                        {
                            if (FunctionSelectCells == null)
                            {
                                FunctionSelectCells = new CellRangeCollection();
                                FunctionSelectCells.BeginCell = cell;
                                OnFunctionSelectCellChanged(FunctionSelectCells);
                                this.Invalidate();
                            }
                        }
                        else
                        {
                            this.OnFunctionSelectedFinish();
                        }
                        this.EndReFresh();
                    }
                    return true;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Selectmode = SelectMode.CellSelected;
                }
                this.BeginReFresh();
                ClearSelect();
                if (cell.Row.Index == 0 && cell.Column.Index == 0)
                {
                    SetSelectCells(this.GetCell(1,1),this.GetCell(this.Rows.MaxHasValueIndex,this.MaxHasValueColumn));
                }
                else
                {
                    SetSelectCells(cell);
                }
                cell.Selected = true;
                
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                {
                    this.SelectRange.Add(this.FocusedCell);
                    this.SelectRange.Add(cell);
                }
                else
                {
                    this.SelectRange.Clear();
                }
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    this.SelectCell(this.FocusedCell, cell);
                } 
                this.FocusedCell = cell;

                //this.CellMouseState = CellMouseState.Down;
                this.MouseDownCell = cell;
                this.AddFocsedCellMark(cell);
                if (this.FocusedCell != null)
                {
                    this.FocusedCell.OnMouseDown(this, e, ve);
                }
                if (this.SelectRange.Count > 0)
                {
                    OnSelectRangeChanged(this.SelectRange);
                }
                this.EndReFresh();
                return true;
            }
            return false;
        }
        public void SetSelectCells(ICell begin)
        {
            SetSelectCells(begin, null);
        }
        public void SetSelectCells(ICell begin, ICell end)
        {
            if (_SelectCells == null)
            {
                _SelectCells = new SelectCellCollection();
            }
            this._SelectCells.BeginCell = begin;
            if (end != null)
            {
                this._SelectCells.EndCell = end;
            }
            this.OnCellSelectChanged(this._SelectCells);
        }
        #endregion

        #region MoveFocusedCellToNext
        public virtual ICell GetNextEditCell(ICell cell, NextCellType nct,int len)
        {
            int rindex = cell.Row.Index;
            int cindex = cell.Column.Index;
            ICell next = null;
            ICell result = null;
            switch (nct)
            {
                case NextCellType.Left:
                    rindex = cell.Row.Index;
                    cindex = cell.Column.Index;
                    for (; cindex > 0;)
                    {
                        cindex = cindex - len;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;
                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                case NextCellType.Up:
                    rindex = cell.Row.Index;
                    cindex = cell.Column.Index;

                    for (; rindex > 0;)
                    {
                        rindex = rindex - len;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;

                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                case NextCellType.Right:
                    rindex = cell.Row.Index;
                    cindex = cell.MaxColumnIndex;

                    for (; cindex < ReSetHasValue().Y;)
                    {
                        cindex = cindex + len;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;
                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                case NextCellType.Down:
                    rindex = cell.MaxRowIndex;
                    cindex = cell.Column.Index;

                    for (; rindex < ReSetHasValue().X;)
                    {
                        rindex = rindex + len;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;

                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            if (result != null)
            {
                if (result.OwnMergeCell != null)
                {
                    result = cell.OwnMergeCell;
                }
            }
            return result;
        }
        public virtual ICell GetNextEditCell(ICell cell, NextCellType nct)
        {
            int rindex = cell.Row.Index;
            int cindex = cell.Column.Index;
            ICell next = null;
            ICell result = null;
            switch (nct)
            {
                case NextCellType.Left:
                    rindex = cell.Row.Index;
                    cindex = cell.Column.Index;
                    for (; cindex > 0; )
                    {
                        cindex = cindex - 1;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;
                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                case NextCellType.Up:
                    rindex = cell.Row.Index;
                    cindex = cell.Column.Index;

                    for (; rindex > 0; )
                    {
                        rindex = rindex - 1;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;

                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                case NextCellType.Right:
                    rindex = cell.Row.Index;
                    cindex = cell.MaxColumnIndex;

                    for (; cindex < ReSetHasValue().Y; )
                    {
                        cindex = cindex + 1;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;
                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                case NextCellType.Down:
                    rindex = cell.MaxRowIndex;
                    cindex = cell.Column.Index;

                    for (; rindex < ReSetHasValue().X; )
                    {
                        rindex = rindex + 1;
                        next = this[rindex, cindex];
                        if (next == null)
                            continue;

                        if (next.OwnMergeCell != null)
                        {
                            next = next.OwnMergeCell;
                        }
                        if (!next.ReadOnly)
                        {
                            result = next;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            if (result != null)
            {
                if (result.OwnMergeCell != null)
                {
                    result = cell.OwnMergeCell;
                }
            }
            return result;
        }
        public virtual void MoveFocusedCellToNextCell(NextCellType nct, bool initedit)
        {
            this.Focus();
            ICell cell = this.FocusedCell;
            if (this.FocusedCell == null)
            {
                cell = this[1, 1];
            }
            BeforeCellCancelArgs e = new BeforeCellCancelArgs()
            {
                Cell = cell
            };
            this.OnBeforeMoveToNextCell(e, nct);
            if (e.Cancel)
            {
                if (this.FocusedCell == e.Cell)
                    return;
                this.FocusedCell = e.Cell;
                if (initedit)
                {
                    this.FocusedCell.InitEdit(this);
                }
                return;
            }
            int rindex = cell.Row.Index;
            int cindex = cell.Column.Index;
            this.ClearSelect();
            bool res = this.CloseEdit();
            if (res)
            {
                this.ReFreshColumnHeaderWidth(false);
            }
            if (this.FocusedCell != null)
            {
                switch (nct)
                {
                    case NextCellType.Left:
                        rindex = cell.Row.Index;
                        cindex = cell.Column.Index;
                        if (cindex > 1)
                        {
                            cindex = cindex - 1;
                            cell = this[rindex, cindex];
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            } 
                        }
                        break;
                    case NextCellType.Up:
                        rindex = cell.Row.Index;
                        cindex = cell.Column.Index;
                        if (rindex > 1)
                        {
                            rindex = rindex - 1;
                            cell = this[rindex, cindex];
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            } 
                        }
                        break;
                    case NextCellType.Right:
                        rindex = cell.Row.Index;
                        cindex = cell.MaxColumnIndex;
                        cindex = cindex + 1;
                        cell = this[rindex, cindex];
                        if (cell.OwnMergeCell != null)
                        {
                            cell = cell.OwnMergeCell;
                        } 
                        break;
                    case NextCellType.Down:
                        rindex = cell.MaxRowIndex;
                        cindex = cell.Column.Index;
                        rindex = rindex + 1;
                        cell = this[rindex, cindex];
                        if (cell.OwnMergeCell != null)
                        {
                            cell = cell.OwnMergeCell;
                        }
                        break;
                    default:
                        break;
                }
                this.ShowAndFocusedCell(cell);
                //this.FocusedCell = cell;
                if (initedit)
                {
                    this.FocusedCell.InitEdit(this);
                }
            }
        }
 
        public virtual void MoveFocusedCellToLeftCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Left, false);
        }
        public virtual void MoveFocusedCellToUpCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Up, false);
        }
        public virtual void MoveFocusedCellToRightCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Right, false);
        }
        public virtual void MoveFocusedCellToDownCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Down, false);
        }
        public virtual void MoveFocusedCellToNextTabCell()
        {
            if (this.FocusedCell != null)
            {
                if (this.FocusedCell.TabStop)
                {
                    ICell cell = this.TabList.Next(this.FocusedCell);
                    if (cell != null)
                    {
                        this.FocusedCell = cell;
                    }
                }
                else
                {
                    if (this[this.FocusedCell.Row.Index, this.FocusedCell.Column.Index + 1] != null)
                    {
                        this.MoveFocusedCellToRightCell();
                    }
                    else if (this[this.FocusedCell.Row.Index + 1, this.FocusedCell.Column.Index + 1] != null)
                    {
                        this.MoveFocusedCellToLeftCell();
                    }
                }
            }
            else
            {
                this.FocusedCell = this.TabList.First;
            }
        }

        public virtual void MoveFocusedCellToLeftCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Left, true);
        }
        public virtual void MoveFocusedCellToTopCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Up, true);
        }
        public virtual void MoveFocusedCellToRightCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Right, true);
        }
        public virtual void MoveFocusedCellToBottomCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Down, true);
        }
        #endregion
    }
}
