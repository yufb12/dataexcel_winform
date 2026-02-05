using Feng.Drawing;
using Feng.Enums;
using Feng.Excel.Actions;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
#if DEBUG
        private string debugname = string.Empty;
        public string DebugName { get { return debugname; } set { debugname = value; } }
        public override string ToString()
        {
            return "DebugName:" + debugname + " ID:" + this.ID;
        }
#endif
        private DataExcelContentsView ContentsView = new DataExcelContentsView();
        public class DataExcelContentsView : DivView
        {
            public DataExcel Grid { get; set; }
            public override bool OnDraw(object sender, GraphicsObject g)
            {
                return base.OnDraw(sender, g);
            }
        }

        public void InitViews()
        {
            if (!this.Viewes.Contains(this.ContentsView))
            {
                this.ContentsView.Grid = this;
                this.Viewes.Add(this.ContentsView);
            }
            if (this.Viewes.Contains(this.HScroll))
            {
                this.Viewes.Add(this.HScroll);
            }
            if (this.Viewes.Contains(this.VScroll))
            {
                this.Viewes.Add(this.VScroll);
            }
        }

        public override bool OnDrawBack(object sender, GraphicsObject g)
        {
            if (this.BackColor != Color.Empty)
            {
                SolidBrush solidBrush = SolidBrushCache.GetSolidBrush(this.BackColor);
                g.Graphics.FillRectangle(solidBrush, this.Rect);
            }
 
            if (this.BackgroundImage != null)
            {
                Feng.Drawing.GraphicsHelper.DrawImage(g.Graphics, this.BackgroundImage,this.Rect, this.BackgroundImageLayout);
            }
            return base.OnDrawBack(sender, g);
        }

        public override bool OnDraw(object sender, GraphicsObject currentGraphicsObject)
        {
            try
            {

                Graphics g = currentGraphicsObject.Graphics;
                GraphicsState gs = g.Save();
                g.SetClip(new Rectangle(this.Left, this.Top, this.Width, this.Height));
                g.TranslateTransform(this.Left, this.Top);
                g.ScaleTransform(this.Zoom, this.Zoom);


                if (currentGraphicsObject.Items != null)
                {
                    currentGraphicsObject.Items.Clear();
                }
                OnDrawExcel(currentGraphicsObject);
                OnPainted(currentGraphicsObject);
                //currentGraphicsObject.Graphics.DrawString(DebugText, this.Font, Brushes.Red, new Point (1,1));
                base.OnDraw(sender, currentGraphicsObject);
                g.Restore(gs);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
                this.OnException(ex);
            }
            return false;
        }

        public override bool OnHandleCreated(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {
                InitDefault();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
            return base.OnHandleCreated(sender, e, ve);
        }

        private DivView divview = null;
        protected DivView DivView
        {
            get
            {
                if (divview == null)
                {
                    divview = new DivView();
                    divview.AddView(this.VScroll);
                    divview.AddView(this.HScroll);
                    divview.AddView(this.VerticalRuler);
                    divview.AddView(this.HorizontalRuler);
                }
                return divview;
            }
        }

        public override bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            try
            {
                if (this.FocusedCell != null)
                {
                    if (this.FocusedCell.OnPreProcessMessage(this, ref msg, ve))
                    {
                        return true;
                    }
                }
                if (msg.Msg == Feng.Utils.UnsafeNativeMethods.WM_KEYDOWN)
                {
                    Keys keydata = (Keys)((int)((long)msg.WParam));
                    keydata = keydata | System.Windows.Forms.Control.ModifierKeys;
                    System.Collections.Generic.List<ICell> list = this.GetSelectCells();
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            if (item.OnKeyDown(this, new KeyEventArgs(keydata), ve))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (this.CellEvent != null)
                        {
                            if (this.CellEvent.OnKeyDown(this, new KeyEventArgs(keydata), ve))
                            {
                                return true;
                            }
                        }
                    }
                    bool result = CompositeKeyEvents(keydata);
                    if (result)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            return base.OnPreProcessMessage(sender, ref msg, ve);
        }

        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (base.OnMouseDown(sender, e, ve))
                {
                    return true;
                }
                Feng.Utils.MouseHook.Instance.OnMouseDown();
                Point viewloaction = this.PointControlToView(e.Location);
                MouseDownPoint = viewloaction;

                if (!this.Focused)
                {
                    this.Focus();
                }
                if (this.CellEvent != null)
                {
                    if (this.CellEvent.OnMouseDown(this, e, ve))
                    {
                        return true;
                    }
                }

                bool res = CloseEdit();
                if (res)
                {
                    this.ReFreshFirstDisplayRowIndex();
                    this.ReFreshFirstDisplayColumnIndex();
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (SetCellMouseDown(e, ve))
                    {
                        goto LabelEnd;
                    }
                    return false;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (OnMouseDownScroll(e))
                    {
                        return true;
                        goto LabelEnd;
                    }
                    if (SetDataExcelMouseDown(e))
                    {
                        return true;
                        goto LabelEnd;
                    }
                    if (SetScrollerMouseDown(e))
                    {
                        return true;
                        goto LabelEnd;
                    }
                    if (SetExtendCellMouseDown(e))
                    {
                        return true;
                        goto LabelEnd;
                    }
                    if (SetMergeCellCollectionRectMouseDown(e))
                    {
                        return true;
                        goto LabelEnd;
                    }
                    if (SetSelectCellCollectionMouseDown(e))
                    {
                        return true;
                        goto LabelEnd;
                    }
                }

                if (SetCellMouseDown(e, ve))
                {
                    return true;
                    goto LabelEnd;
                }

            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
            }
        LabelEnd:
            return base.OnMouseDown(sender, e, ve);
        }

        public override bool OnDragLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            return base.OnDragLeave(sender, e, ve);
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.None)
                {
                    this.BeginSetCursor(System.Windows.Forms.Cursors.Default);
                }
                if (this.CellEvent != null)
                {
                    if (this.CellEvent.OnMouseMove(this, e, ve))
                    {
                        return true;
                    }
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    return false;
                }

                for (int i = this.MousesMoveEvents.Count - 1; i >= 0; i--)
                {
                    Feng.Forms.Interface.IViewEvent events = this.MousesMoveEvents[i];
                    events.OnMouseMove(this, e, ve);
                }
                if (e.Button == MouseButtons.Left)
                {
                    bool res = OnMouseMoveScroll(e);
                    if (res)
                        return res;
                }
                if (e.Button != System.Windows.Forms.MouseButtons.Left)
                {
                    bool res = SelectModeNullMouseMove(e, ve);
                    if (res)
                    {
                        return res;
                    }
                }
                else
                {
                    //if (System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
                    //{
                    switch (Selectmode)
                    {
                        case SelectMode.Null:
                            bool res = SelectModeNullMouseMove(e, ve);
                            if (res)
                            {
                                return res;
                            }
                            break;
                        case SelectMode.RowHeaderSelected:
                            break;
                        case SelectMode.RowHeaderSplitSelected:
                            break;
                        case SelectMode.FullRowSelected:
                            this.BeginReFresh();
                            SelectModeRowHeaderSelectedMouseMove(e);
                            this.EndReFresh();
                            break;
                        case SelectMode.ColumnHeaderSelected:

                            break;
                        case SelectMode.ColumnHeaderSplitSelected:
                            this.BeginReFresh();
                            this.EndReFresh();
                            break;
                        case SelectMode.FullColumnSelected:
                            this.BeginReFresh();
                            SelectModeColumnHeaderSelectedMouseMove(e);
                            this.EndReFresh();
                            break;
                        case SelectMode.CellSelected:

                            if (this.MultiSelect)
                            {
                                this.BeginReFresh();
                                SelectModeCellSeletedMouseMove(e);
                                SetSelectCellRowSelect();
                                this.EndReFresh();
                            }
                            break;
                        case SelectMode.CellRangeFunctionCellSelected:
                            this.BeginReFresh();
                            SCellRangeFunctionCellSelectedMouseMove(e);
                            this.EndReFresh();
                            break;
                        case SelectMode.MergeCellSelected:
                            this.BeginReFresh();
                            SelectModeMergeCellSelectedMouseMove(e);
                            this.EndReFresh();
                            break;
                        case SelectMode.MergeCellAddSelected:
                            this.BeginReFresh();
                            SelectModeMergeCellAddSelectedMouseMove(e);
                            this.EndReFresh();
                            break;
                        case SelectMode.ImageCellSelected:
                            this.BeginReFresh();
                            SelectModeImageMouseMove(e);
                            this.EndReFresh();
                            break;
                        case SelectMode.CellAddSelected:
                            this.BeginReFresh();
                            SelectModeCellAddSelectedMouseMove(e);
                            this.EndReFresh();
                            break;
                        case SelectMode.TextCellSelected:
                            this.BeginReFresh();
                            SelectModeTextMouseMove(e);
                            this.EndReFresh();
                            break;
                        //case SelectMode.ImageCellSizeRectSelected:
                        //    this.BeginReFresh();
                        //    ImageCellSizeRectSelected(e);
                        //    this.EndReFresh();
                        //    break;
                        case SelectMode.ExtendCellSizeRectSelected:
                            this.BeginReFresh();
                            TextCellSizeRectSelected(e);
                            this.EndReFresh();
                            break;
                        //case SelectMode.HScrollMoveSelected:
                        //    this.BeginReFresh();
                        //    this.HScroller.OnMouseMove(e);
                        //    this.EndReFresh();
                        //    break;
                        //case SelectMode.VScrollMoveSelected:
                        //    this.BeginReFresh();
                        //    this.VScroller.OnMouseMove(e);
                        //    this.EndReFresh();
                        //    break;
                        case SelectMode.ChangedSize:
                            this.BeginReFresh();
                            ChangedSize(e);
                            this.EndReFresh();
                            break;

                        case SelectMode.Drag:
                            this.BeginReFresh();
                            this.Cursor = Cursors.Hand;
                            this.EndReFresh();
                            break;
                        default:
                            break;
                    }
                    //}
                }

            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {

            }
            return base.OnMouseMove(sender, e, ve);
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (this.Selectmode == SelectMode.CellRangeFunctionCellSelected)
                {
                    if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                    {
                        this.FunctionSelectCells = null;
                        this.Invalidate();
                        goto EndLable;
                    }
                    else
                    {
                        this.OnFunctionSelectedFinish();

                    }
                }
                if (this.CellEvent != null)
                {
                    if (this.CellEvent.OnMouseUp(this, e, ve))
                    {
                        goto EndLable;
                    }
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    goto EndLable;
                }
                bool res = OnMouseUpScroll(e);
                if (res)
                    return res;
                switch (this.Selectmode)
                {
                    case SelectMode.RowHeightChangedMode:
                        this.OnRowHeightChangedFinished(this.HeightChangedRow);
                        this.ReFreshFirstDisplayRowIndex();
                        break;
                    case SelectMode.ColumnWidthChangedMode:
                        this.OnColumnWidthChangedFinished(this.WidthChangedColumn);
                        this.ReFreshFirstDisplayColumnIndex();
                        break;
                    case SelectMode.CellSelected:
                        this.OnSelectCellFinished(this.SelectCells);
                        break;
                    case SelectMode.CellAddSelected:
                        this.BeginReFresh();
                        UpdateSelectAddRect();
                        this._SelectAddRectCollection = null;
                        this.EndReFresh();
                        break;
                    case SelectMode.ImageCellSelected:
                        this.BeginReFresh();
                        ExtendCell.ReSetRowColumn(Point.Round(ExtendCell.Rect.Location));
                        ExtendCell.FreshLocation();
                        this.EndReFresh();
                        break;
                    case SelectMode.TextCellSelected:
                        this.BeginReFresh();
                        ExtendCell.ReSetRowColumn(Point.Round(ExtendCell.Rect.Location));
                        ExtendCell.FreshLocation();
                        this.EndReFresh();
                        break;
                    case SelectMode.MergeCellAddSelected:
                        this.BeginReFresh();
                        DoMergeCellCollectionAddRectDown(this.MergeCellCollectionAddRect);
                        this.EndReFresh();
                        break;
                    case SelectMode.ExtendCellSizeRectSelected:
                        this.BeginReFresh();
                        this.ExtendCell.SizeChangMode = SizeChangMode.Null;
                        this.EndReFresh();
                        break;
                    default:
                        this._SelectAddRectCollection = null;
                        this._MergeCellCollectionAddRect = null;

                        break;
                }
                this.Selectmode = SelectMode.Null;

            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        EndLable:
            return base.OnMouseUp(sender, e, ve);
        }
        private int checktimees=0;
        private  void CheckTime()
        {
            if (DateTime.Now > new DateTime(2027, 10, 13))
            {
                if (checktimees % 30==0)
                {
                    System.Windows.Forms.MessageBox.Show("Updates Needed");
                }
                checktimees++;
            }
        }
        [Feng.Data.OperationContractAttribute]
        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                CheckTime();
                Point viewloaction = this.PointControlToView(e.Location);
                Point pf = viewloaction;
                if (this.Selectmode == SelectMode.Null || this.Selectmode == SelectMode.CellSelected)
                {
                    if (CellEvent != null)
                    {
                        Point pt = ve.ControlPoint;
                        ve.ControlPoint.Offset(this.Left, this.Top);
                        bool res = CellEvent.OnMouseClick(this, e, ve);
                        if (res)
                        {
                            return false;
                        }
                        if (PropertyClick != null && !string.IsNullOrWhiteSpace(PropertyClick))
                        {
                            ExecuteAction(new ActionArgs(PropertyClick, this, this.FocusedCell), PropertyClick);
                        }
                    }
                }
                lastkey = Keys.None;
            }
            catch (Exception ex)
            {
                OnException(ex);
            }

            return base.OnMouseClick(sender, e, ve);
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {

                _BeginReFresh = _EndReFresh = 0;
                if (CellEvent != null)
                {
                    bool handle = CellEvent.OnMouseDoubleClick(this, e, ve);
                    if (handle)
                        return true;
                }
                Point viewloaction = this.PointControlToView(e.Location);
                DoubleClear();
                MouseDownPoint = viewloaction;
                this.BeginReFresh();
                if (this.ListExtendCells != null)
                {
                    for (int i = ListExtendCells.Count - 1; i >= 0; i--)
                    {
                        IExtendCell iec = ListExtendCells[i];
                        if (iec.Rect.Contains(viewloaction))
                        {
                            bool res = iec.OnMouseDoubleClick(this, e, ve);
                            OnExtendCellDoubleClick(iec);
                            if (res)
                            {
                                return res;
                            }
                        }
                    }
                }
                MouseEventArgs ee = e;
                if (ScrollerDoubleMouseClick(ee))
                {
                    return true;
                }

                foreach (IRow r in this.AllVisibleRows)
                {
                    if (r.Rect.Contains(viewloaction))
                    {
                        foreach (IColumn c in this.AllVisibleColumns)
                        {
                            if (c.Rect.Contains(viewloaction))
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

                                this._ICellEvents = cell;
                                if (CellDoubleClick != null)
                                {
                                    CellDoubleClick(this, cell);
                                }
                                Selectmode = SelectMode.CellSelected;

                                bool resut = this._ICellEvents.OnMouseDoubleClick(this, e, ve);
                                if (resut)
                                {
                                    return true;
                                }
                                break;
                            }
                        }
                        break;
                    }
                }

                if (PropertyDoubleClick != null && !string.IsNullOrWhiteSpace(PropertyDoubleClick))
                {
                    ExecuteAction(new ActionArgs(PropertyDoubleClick, this, this.FocusedCell), PropertyDoubleClick);
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                this.EndReFresh();
            }
            return base.OnMouseDoubleClick(sender, e, ve);
        }

        DateTime lastwheel = DateTime.Now;
        public override bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if ((DateTime.Now - lastwheel).TotalMilliseconds < 100)
                {
                    if (Math.Abs(e.Delta) < 120)
                    {
                        return false;
                    }
                }
                lastwheel = DateTime.Now;

                this.BeginReFresh();
                try
                {
                    bool handle = false;
                    if (CellEvent != null)
                    {
                        handle = CellEvent.OnMouseWheel(this, e, ve);
                        if (handle)
                            return true;
                    }
                    if (!this.AllowChangedFirstDisplayRow)
                    {
                        return false;
                    }
                    if (!handle)
                    {
                        bool res = CloseEdit();
                        int index = this.FirstDisplayedRowIndex;
                        if (e.Delta < 0)
                        {
                            index = GetNextFilterRow(index);
                            if (e.Delta < -40)
                            {
                                index = GetNextFilterRow(index);
                            }
                            if (e.Delta < -80)
                            {
                                index = GetNextFilterRow(index);
                            }
                        }
                        else
                        {
                            index = GetProevFilterRow(index);
                            if (e.Delta > 80)
                            {
                                index = GetProevFilterRow(index);
                            }
                            if (e.Delta > 40)
                            {
                                index = GetProevFilterRow(index);
                            }
                        }

                        //Feng.Utils.TraceHelper.WriteTrace("OnMouseWheel", index.ToString(), e.Delta.ToString(), index.ToString());
                        if (index < 1)
                            index = 1;
                        this.FirstDisplayedRowIndex = index;
                        RefreshRowHeaderWidth();
                        if (res)
                        {
                            this.ReFreshColumnHeaderWidth(true);
                        }
                        return true;
                    }
                }
                finally
                {
                    this.EndReFresh();
                }

            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            return base.OnMouseWheel(sender, e, ve);
        }
        private int GetProevFilterRow(int rowindex)
        {
            for (int i = 1; i < 1000; i++)
            {
                int index = rowindex - i;
                bool isfilterrow = IsFilterRow(index);
                if (!isfilterrow)
                {
                    return index;
                }
            }
            return rowindex;
        }
        private int GetNextFilterRow(int rowindex)
        {
            for (int i = 1; i < 1000; i++)
            {
                int index = rowindex + i;
                bool isfilterrow = IsFilterRow(index);
                if (!isfilterrow)
                {
                    return index;
                }
            }
            return rowindex;
        }
        private bool IsFilterRow(int rowindex)
        { 
            if (this.FilterExcel != null)
            {
                if (this.FilterExcel.FilterRows.Count > 0)
                { 
                    IRow row = this.GetRow(rowindex);
                    if (this.FilterExcel.FilterRows.Contains(row))
                    {
                        return true;
                    }
                }
            } 
            return false; ;
        }
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {

            try
            {
                this.BeginReFresh();

                this.ReFreshFirstDisplayColumnIndex();
                this.ReFreshFirstDisplayRowIndex();

                ScrollerSizeChanged();

                this.EndReFresh();
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }

            return base.OnSizeChanged(sender, e, ve);
        }

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (CellEvent != null)
                {
                    if (CellEvent.OnKeyDown(this, e, ve))
                    {
                        return false;
                    }
                }
                if (this.InEdit)
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        this.ClearCaretEdit();
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {
                    this.Delete();
                }
                if (PropertyKeyDown != null && !string.IsNullOrWhiteSpace(PropertyKeyDown))
                {
                    ExecuteAction(new ActionArgs(PropertyKeyDown, this, this.FocusedCell), PropertyKeyDown);
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            return base.OnKeyDown(sender, e, ve);

        }

        public override bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (CellEvent != null)
                {
                    this.BeginReFresh();
                    CellEvent.OnKeyPress(this, e, ve);
                    this.EndReFresh();
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            return base.OnKeyPress(sender, e, ve);
        }

        public override bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            try
            {
                bool handle = false;
                if (CellEvent != null)
                {
                    handle = CellEvent.OnKeyUp(this, e, ve);
                }
                if (PropertyKeyUp != null && !string.IsNullOrWhiteSpace(PropertyKeyUp))
                {
                    ExecuteAction(new ActionArgs(PropertyKeyUp, this, this.FocusedCell), PropertyKeyUp);
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            return base.OnKeyUp(sender, e, ve);
        }

        public override bool OnDragEnter(object sender, DragEventArgs drgevent, EventViewArgs ve)
        {
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
            {
                drgevent.Effect = DragDropEffects.All;
            }
            else if (drgevent.Data.GetDataPresent(DataFormats.Text))
            {
                drgevent.Effect = DragDropEffects.All;
            }
            else
            {
                drgevent.Effect = DragDropEffects.None;
            }
            return base.OnDragEnter(sender, drgevent, ve);
        }

        public override bool OnDragDrop(object sender, DragEventArgs drgevent, EventViewArgs ve)
        {
            try
            {
                string[] s = (string[])drgevent.Data.GetData(DataFormats.FileDrop, false);

                if (s != null)
                {
                    if (s.Length > 0)
                    {
                        this.Open(s[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnDragDrop(sender, drgevent, ve);
        }

        public override bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            try
            {
                bool res = base.OnWndProc(sender, ref m, ve);
                if (res)
                {
                    return true;
                }
                if (this.CellEvent != null)
                {
                    if (this.CellEvent.OnWndProc(this, ref m, ve))
                    {
                        return false;
                    }
                }
                int msg = m.Msg;
                if (msg == Feng.Utils.UnsafeNativeMethods.WM_IME_SETCONTEXT) //associated IME with our UserControl
                {
                    //if (!m.WParam.Equals(IntPtr.Zero))
                    //{
                    //    bool flag = Feng.Utils.UnsafeNativeMethods.ImmAssociateContextEx(m.HWnd, IntPtr.Zero, 16);
                    //    IntPtr hIMC = Feng.Utils.UnsafeNativeMethods.ImmGetContext(m.HWnd);
                    //    flag = Feng.Utils.UnsafeNativeMethods.ImmSetOpenStatus(hIMC, true);
                    //    flag = Feng.Utils.UnsafeNativeMethods.ImmReleaseContext(m.HWnd, hIMC); 
                    //}
                }
                else if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_IME_STARTCOMPOSITION) //Intercept Message to get Unicode Char
                {
                    IntPtr hIMC = Feng.Utils.UnsafeNativeMethods.ImmGetContext(m.HWnd);
                    Feng.Utils.UnsafeNativeMethods.COMPOSITIONFORM CompForm = new Feng.Utils.UnsafeNativeMethods.COMPOSITIONFORM();
                    CompForm.dwStyle = Feng.Utils.UnsafeNativeMethods.CFS_POINT;
                    CompForm.dwStyle = Feng.Utils.UnsafeNativeMethods.CFS_POINT;
                    CompForm.ptCurrentPos = new Point();
                    if (!this.InEdit)
                    {
                        MousePoint = PointToClient(System.Windows.Forms.Control.MousePosition);
                    }

                    CompForm.ptCurrentPos.X = MousePoint.X;
                    CompForm.ptCurrentPos.Y = MousePoint.Y;
                    bool flag = Feng.Utils.UnsafeNativeMethods.ImmSetCompositionWindow(hIMC, ref CompForm);
                    flag = Feng.Utils.UnsafeNativeMethods.ImmReleaseContext(m.HWnd, hIMC);
                    return true;
                }
                else if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_CHAR)
                {
                    if (System.Windows.Forms.Control.ModifierKeys != Keys.Control
                        || System.Windows.Forms.Control.ModifierKeys != Keys.Control
                        || System.Windows.Forms.Control.ModifierKeys != Keys.Control)
                    {

                        char m_ImeChar = Convert.ToChar(m.WParam.ToInt32());
                        if (!char.IsControl(m_ImeChar) || m_ImeChar == '\b')
                        {

                            if (this.FocusedCell != null)
                            {
                                this.FocusedCell.InitEdit(this);
                                IImeCharChanged imeCharChanged = this.FocusedCell.OwnEditControl as IImeCharChanged;
                                if (imeCharChanged != null)
                                {
                                    bool resime = imeCharChanged.ImeCharChanged(this.FocusedCell, m_ImeChar);
                                    if (resime)
                                    {
                                        this.Invalidate();
                                    }
                                }
                            }
                        }

                    }
                }
                if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_IME_ENDCOMPOSITION) //Intercept Message to get Unicode Char
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            return base.OnWndProc(sender, ref m, ve);
        }


    }
}
