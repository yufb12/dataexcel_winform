using Feng.Drawing;
using Feng.Excel.Collections;
using Feng.Excel.Delegates;
using Feng.Excel.Interfaces;
using Feng.Forms.Skin;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        public static string DebugText = string.Empty;
        public event PaintedEventHandler Painted;
        public virtual void OnPainted(Feng.Drawing.GraphicsObject graphicsobject)
        {
            if (Painted != null)
            {
                Painted(this, graphicsobject);
            }
        }


        #region 界面绘制

        public void Do(DrawSpeedHandler handler, Feng.Drawing.GraphicsObject g)
        {
            handler(g);
        }
        public void DrawBackCellLayer(object sender, GraphicsObject g)
        {

        }
        public virtual void OnDrawExcel(Feng.Drawing.GraphicsObject g)
        {
            _freshversion++;
            if (_freshversion > ushort.MaxValue)
            {
                _freshversion = 0;
            }
            this._EndReFresh = this._BeginReFresh = 0;
            //graphicscache = g.Graphics;
#if DEBUG
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch swall = new System.Diagnostics.Stopwatch();
            sw.Start();
            swall.Start();
#endif
            celldrawEdit = null;
            celldrawEdit2 = null;
            double d1 = 0.05d;
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawLayerBottom:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawColumn(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawColumn:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawBackCell(g);
            OnDrawBack(g);
            OnDrawMergeCellBack(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawBack:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawRow(g);
            OnDrawMergeCell(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawRow:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawRowHeader(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawRowHeader:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawColumnHeader(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawColumnHeader:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart(); 
#endif
            if (this.ShowGridRowLine)
            {
                OnDrawGridLine(g);
            }
            else
            {
                OnHeaderGridLine(g);
            }
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawGridLine:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            g.CurrentLayer = null;
            OnDrawBorder(g);

#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawBorder:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawListExtendCells(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawListExtendCells:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawSelectCells(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawSelectCells:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            if (this.FunctionSelectCells != null)
            {
                this.FunctionSelectCells.OnDraw(this, g);
            }
            this.VerticalRuler.OnDraw(this, g);
            this.HorizontalRuler.OnDraw(this, g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "Ruler.OnDraw:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawMoveBorder(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawMoveBorder:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawDragDropCell(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawDragDropCell:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawTempRect(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawTempRect:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawCopyCellRect(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawCopyCellRect:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnShowMultSelectCells(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "ShowMultSelectCells:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            OnDrawShowFocusedCellBorder(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawShowFocusedCellBorder:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Restart();
#endif
            if (this.ShowBorder)
            {
                using (Pen pen = new Pen(this.BorderColor))
                {
                    g.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                }
            }
            else
            {
                //this.DrawGridLine(g, 0, 0, 0, this.Height);
            }
            OnDrawFunctionBorder(g);
            OnDrawHScroll(g);
            DrawEdit(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "ShowBorder:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
#endif
            OnDrawToolTip(g);
#if DEBUG
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "OnDrawToolTip:" + sw.Elapsed.TotalSeconds.ToString("0.###"));
            sw.Stop();
            swall.Stop();
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeedsAll", swall.Elapsed.TotalSeconds > d1, "DrawAll:" + swall.Elapsed.TotalSeconds.ToString("0.###"));
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "DrawSpeed", sw.Elapsed.TotalSeconds > d1, "DrawAll:" + swall.Elapsed.TotalSeconds.ToString("0.###"));
#endif
            //g.Graphics.ResetClip();
            //g.Graphics.ResetTransform();
            //g.Graphics.DrawString(Feng.Drawing.GraphicsObject.DebugText,
            //    g.DefaultFont, Brushes.Red, new Rectangle(this.Left, this.Top, this.Width, this.Height));
        }
        public IBaseCell celldrawEdit = null;
        public Feng.Excel.Edits.CellEdit celldrawEdit2 = null;
        private void DrawEdit(GraphicsObject g)
        {
            if (celldrawEdit2 != null)
            {
                celldrawEdit2.DrawCell(celldrawEdit, g);
            }
        }
        private void OnDrawHScroll(GraphicsObject g)
        {
            HScroll.OnDraw(g.Graphics);
            VScroll.OnDraw(g.Graphics);
        }
        private void OnDrawFunctionBorder(GraphicsObject g)
        {
            if (this.FocusedCell != null)
            {
                if (this.InEdit)
                {
                    this.FocusedCell.DrawFunctionBorder(g, -1);
                }
            }
        }
        public void OnDrawListExtendCells(GraphicsObject g)
        {
            if (ListExtendCells != null)
            {
                foreach (IExtendCell extendcell in ListExtendCells)
                {
                    extendcell.OnDraw(this, g);
                }
            }
        }
        public void OnShowMultSelectCells(GraphicsObject g)
        {
            foreach (ICell cell in SelectRange)
            {
                if (this.FocusedCell == cell)
                {
                    continue;
                }
                if (this.InVisible(cell))
                {

                    if (cell.ShowFocusedSelectBorder)
                    {
                        Pen p = PenCache.GetPen(this.FocusedCell.SelectBorderColor);
                        g.Graphics.DrawRectangle(p, Rectangle.Round(cell.Rect));
                    }

                    if (this.ShowFocusedCellBorder)
                    {
                        Pen p = PenCache.GetPen(this.ShowFocusedCellBorderColor);
                        g.Graphics.DrawRectangle(p, Rectangle.Round(cell.Rect));

                    }
                }
            }
        }
        public void OnDrawShowFocusedCellBorder(GraphicsObject g)
        {
            if (this.FocusedCell != null)
            {
                if (this.InVisible(this.FocusedCell))
                {

                    if (this.ShowFocusedCellBorder)
                    {
                        using (Pen p = new Pen(this.ShowFocusedCellBorderColor))
                        {
                            g.Graphics.DrawRectangle(p, Rectangle.Round(this.FocusedCell.Rect));
                        }
                    }
                }
            }
        }
        private void OnDrawSelectCells(GraphicsObject g)
        {
            if (this.ShowSelectBorder)
            {
                if (!this.InEdit)
                {
                    if (this.SelectCells != null)
                    {
                        bool drawselectcells = true;
                        if (this.SelectCells.BeginCell == this.SelectCells.EndCell)
                        {
                            ICell cell = this.SelectCells.BeginCell;
                            if (cell.OwnEditControl != null)
                            {
                                if (cell.OwnEditControl.HasChildEdit)
                                {
                                    drawselectcells = false;
                                }
                            }
                        }
                        if (drawselectcells)
                        {
                            this.SelectCells.OnDraw(this, g);
                        }
                    }
                    if (this._SelectAddRectCollection != null)
                    {
                        this._SelectAddRectCollection.OnDraw(this, g);
                    }

                    if (this.MergeCellCollectionRect != null)
                    {
                        this.MergeCellCollectionRect.OnDraw(g);
                    }
                    if (this.MergeCellCollectionAddRect != null)
                    {
                        this._MergeCellCollectionAddRect.OnDraw(g);
                    }
                }
            }
            if (this.FocusedCell != null)
            {
                if (this.InVisible(this.FocusedCell))
                {

                    if (this.FocusedCell.ShowFocusedSelectBorder)
                    {
                        Pen p = PenCache.GetPen(this.FocusedCell.SelectBorderColor);
                        g.Graphics.DrawRectangle(p, Rectangle.Round(this.FocusedCell.Rect));
                    }
                }
            }

        }

        public virtual void OnDrawDragDropCell(Feng.Drawing.GraphicsObject g)
        {
            if (this.DrawDragDropCell)
            {
                SelectCellCollection cells = DragDropCells;
                if (cells != null)
                {
                    Rectangle rect = Rectangle.Round(cells.Rect);
                    Pen p = new Pen(Color.Brown, 3);
                    g.Graphics.DrawRectangle(p, rect);
                }
            }
        }
        public virtual void OnDrawTempRect(Feng.Drawing.GraphicsObject g)
        {
            if (this.TempSelectRect != null)
            {
                Rectangle rect = Rectangle.Round(this.TempSelectRect.Rect);
                Pen p = new Pen(Color.Brown, 3);
                g.Graphics.DrawRectangle(p, rect);
            }
        }

        public virtual void OnHeaderGridLine(Feng.Drawing.GraphicsObject g)
        {
            if (this.ShowColumnHeader)
            {
                IRow row = this.GetRow(0);
                foreach (IColumn column in this.AllVisibleColumns)
                {
                    ICell cell = row[column];
                    if (cell == null)
                    {
                        cell = this.ClassFactory.CreateDefaultCell(row, column);
                    }
                    cell.DrawGridLine(g);
                }
            }

            if (this.ShowRowHeader)
            {
                IColumn column = this.GetColumn(0);
           
                foreach (IRow row in this.AllVisibleRows)
                {
                    ICell cell = row[column];
                    if (cell == null)
                    {
                        cell = this.ClassFactory.CreateDefaultCell(row, column);
                    }
                    cell.DrawGridLine(g);
                }
            }
        }

        public virtual void OnDrawGridLine(Feng.Drawing.GraphicsObject g)
        {

            try
            {
                for (int i = this.AllVisibleRows.Count - 1; i >= 0; i--)
                {
                    IRow row = null;
                    try
                    {
                        row = this.AllVisibleRows[i];
                        if (this.EndRow == row)
                            continue;
                    }
                    catch (Exception ex)
                    {
                        Feng.Utils.BugReport.Log(ex);
                        Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
                    }
                    row.DrawGridLine(g);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
        }
        public virtual void OnDrawRow(Feng.Drawing.GraphicsObject g)
        {
            foreach (IRow rh in this.VisibleRows)
            {
                rh.OnDraw(this, g);
            }
        }
        public virtual void OnDrawRowHeader(Feng.Drawing.GraphicsObject g)
        {
            if (this.ShowRowHeader)
            {
                IColumn cl = this.GetColumn(0);
                foreach (IRow rh in this.AllVisibleRows)
                {
                    if (!rh.Visible)
                    {
                        continue;
                    }
                    if (rh.Height > 0)
                    {
                        ICell cell = rh.Cells[cl];
                        if (cell == null)
                        {
                            cell = this.ClassFactory.CreateDefaultCell(rh, cl);
                        }
                        if (cell != null)
                        {
                            cell.OnDraw(this, g);
                        }

                    }
                }
            }
        }
        public virtual void OnDrawColumn(Feng.Drawing.GraphicsObject g)
        {
            foreach (IColumn ch in this.AllVisibleColumns)
            {
                if (ch.Index > 0)
                {
                    ch.OnDraw(this, g);
                }
            }
        }
        public virtual void OnDrawColumnHeader(Feng.Drawing.GraphicsObject g)
        { 
            if (this.ShowRowHeader)
            { 
                IRow rh = this.GetRow(0); 
                if (rh.Visible)
                {
                    if (rh.Height > 0)
                    {
                        foreach (IColumn cl in this.AllVisibleColumns)
                        {
                            ICell cell = rh.Cells[cl];
                            if (cell == null)
                            {
                                cell = this.ClassFactory.CreateDefaultCell(rh, cl);
                            }
                            if (cell != null)
                            {
                                cell.OnDraw(this, g);
                            }

                        }
                    }
                }
            }
        }
        public virtual void OnDrawBorder(Feng.Drawing.GraphicsObject g)
        {
            foreach (IRow rh in this.AllVisibleRows)
            {
                foreach (IColumn ch in this.AllVisibleColumns)
                {
                    ICell cell = this[rh.Index, ch.Index];
                    if (cell != null)
                    {
                        cell.DrawBorder(g);
                    }
                }
            }
        }
        public virtual void OnDrawBack(Feng.Drawing.GraphicsObject g)
        {
            foreach (IRow rh in this.VisibleRows)
            {
                rh.OnDrawBack(this, g);
            }
        }
        public virtual void OnDrawBackCell(Feng.Drawing.GraphicsObject g)
        {
            foreach (IRow row in this.VisibleRows)
            {
                foreach (IColumn column in this.VisibleColumns)
                {
                    ICell cell = row.Cells[column];
                    if (cell != null)
                    {
                        if (cell.OwnBackCell != null)
                        {
                            cell.OwnBackCell.OnDrawBack(this, g);
                        }
                    }
                }
            }
        }
        public virtual void OnDrawMergeCellBack(Feng.Drawing.GraphicsObject g)
        {
            foreach (IRow row in this.VisibleRows)
            {
                foreach (IColumn column in this.VisibleColumns)
                {
                    ICell cell = row.Cells[column];
                    if (cell != null)
                    {
                        if (this.EndRow == row)
                            continue;
                        if (cell.OwnMergeCell != null)
                        {
                            cell.OwnMergeCell.OnDrawBack(this, g);
                        }
                    }
                }
            }
        }
        public virtual void OnDrawMergeCell(Feng.Drawing.GraphicsObject g)
        {
            foreach (IRow row in this.VisibleRows)
            {
                foreach (IColumn column in this.VisibleColumns)
                {
                    ICell cell = row.Cells[column];
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            //if (cell.OwnMergeCell.BeginCell.Row.Index == 6 && cell.OwnMergeCell.BeginCell.Column.Index == 4)
                            //{
                            if (this.EndRow == row)
                                continue;
                            //}
                            cell.OwnMergeCell.OnDraw(this, g);
                        }
                    }
                }
            }
        }
        public virtual void OnDrawCopyCellRect(Feng.Drawing.GraphicsObject g)
        {
            if (this.CopyCells != null)
            {
                BaseRectDrawing.Instanc.DrawDashPattern(g.Graphics, this.CopyCells.Rect);
            }
        }
        public virtual void OnDrawMoveBorder(Feng.Drawing.GraphicsObject g)
        {
            if (this.AllowChangedSize)
            {
                if (this.SelectChangedBorder)
                {
                    Rectangle rectf = this.Rect;
                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddRectangle(rectf);
                        rectf.Inflate(this.SelectBorderWidth * -1, this.SelectBorderWidth * -1);
                        path.AddRectangle(rectf);
                        using (System.Drawing.Drawing2D.HatchBrush hb = new HatchBrush(HatchStyle.Percent20, Color.Gray, Color.White))
                        {
                            g.Graphics.FillPath(hb, path);
                        }
                    }
                    Brush brush = Brushes.Blue;

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.TopLeft);

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidTop);

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidLeft);

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.TopRight);

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidRight);

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.BottomLeft);

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidBottom);

                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.BottomRight);
                }
            }
        }
        public virtual void OnDrawToolTip(Feng.Drawing.GraphicsObject g)
        {
            //g.Graphics.DrawString(this._FirstDisplayedRowIndex.ToString(), this.Font, Brushes.Red, new Rectangle(70, 70, 70, 70));
            if (this.ToolTipVisible)
            {
                Size size = Feng.Drawing.GraphicsHelper.Sizeof(this.ToolTip, this.Font, g.Graphics);
                Rectangle rect = new Rectangle(this.Width / 2 - size.Width / 2 - 20, this.Height / 2 - size.Height / 2 - 20,
                    size.Width + 40, size.Height + 20);
                Color color = ColorHelper.Opacity(Color.Blue, 80);
                GraphicsHelper.FillRectangle(g.Graphics, rect, color);
                Feng.Drawing.GraphicsHelper.DrawText(g.Graphics, this.Font, this.ToolTip, Color.Wheat, rect);
            }
        }

        private Cursor DefaultCursor { get { return Cursors.Default; } }
        private Cursor _begincursor = null;
        public override void BeginSetCursor(Cursor begincursor)
        {
            this.Control.BeginSetCursor(begincursor);
        }


        private int _BeginReFresh = 100;
        public override void BeginReFresh()
        {
            _BeginReFresh++;
        }
        private int _EndReFresh = 0;
        public override void EndReFresh()
        {
            _EndReFresh++;
            this.ReFresh();
        }

        public virtual void BeginReFresh(Rectangle rect)
        {
            _region.Union(rect);
            _BeginReFresh++;
        }
        public virtual void EndReFresh(Rectangle rect)
        {
            _EndReFresh++;
            this.RePaint(rect);
        }
        private System.Drawing.Region _region = new Region();


        public bool DebugStep = false;
        public override void Invalidate()
        {
            if (DebugStep)
            {
                Feng.Utils.TraceHelper.DebuggerBreak();
            }
            if (this.Control != null)
            {
                this.Control.Invalidate();
            }
        }

        public override void Invalidate(Rectangle rc)
        {
            if (DebugStep)
            {
                Feng.Utils.TraceHelper.DebuggerBreak();
            }
            if (this.Control != null)
            {
                this.Control.Invalidate(rc);
            }
        }

        public override void Invalidate(Region region)
        {
            if (DebugStep)
            {
                Feng.Utils.TraceHelper.DebuggerBreak();
            }
            if (this.Control != null)
            {
                this.Control.Invalidate(region);
            }
        }

        public virtual void Invalidate(Rectangle rc, bool invalidateChildren)
        {
            if (DebugStep)
            {
                Feng.Utils.TraceHelper.DebuggerBreak();
            }
            base.Invalidate(rc);
        }

        public virtual void Invalidate(Region region, bool invalidateChildren)
        {
            if (DebugStep)
            {
                Feng.Utils.TraceHelper.DebuggerBreak();
            }
            base.Invalidate(region);
        }
        public virtual void RePaint(Rectangle rect)
        {

            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate();
                this.Invalidate(_region);
                _region.MakeEmpty();
                _BeginReFresh = _EndReFresh = 0;
            }

        }
        int refreshtimes = 0;
        public override void ReFresh()
        {
            if (this._BeginReFresh == _EndReFresh)
            {
                if (this.BackGroundMode)
                    return;
                this.Invalidate();
                refreshtimes++;
                _BeginReFresh = _EndReFresh = 0;
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "RePaint", false, "Invalidate:" + refreshtimes);
            }
        }

        public void DrawGridLine(Feng.Drawing.GraphicsObject g, int x1, int y1, int x2, int y2)
        {
            //g.CurrentLayer.Path.AddLine(x1, y1, x2, y2);
            //g.CurrentLayer.Path.CloseFigure();
            g.Graphics.DrawLine(this.GridLinePen, x1, y1, x2, y2);
        }

        public void DrawGridRectangle(Feng.Drawing.GraphicsObject g, int x, int y, int width, int height)
        {
            if (this.ShowGridRowLine)
            {
                g.Graphics.DrawRectangle(this.GridLinePen, x, y, width, height);
            }
        }

        public void DrawGridRectangle(Feng.Drawing.GraphicsObject g, Rectangle rect)
        {
            if (this.ShowGridRowLine)
            {
                Pen br = this.GridLinePen;
                g.Graphics.DrawRectangle(br, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        public void DoMergeCellCollectionAddRectDown(MergeCellCollectionAddRect mca)
        {
            if (mca.EndCell == null)
                return;
            int x = mca.MergeCellCollectionRect.FirstMergeCell.EndCell.Column.Index
                - mca.MergeCellCollectionRect.FirstMergeCell.BeginCell.Column.Index;
            int y = mca.MergeCellCollectionRect.FirstMergeCell.EndCell.Row.Index
                - mca.MergeCellCollectionRect.FirstMergeCell.BeginCell.Row.Index;

            int w = mca.EndCell.Column.Index -
                mca.MergeCellCollectionRect.FirstMergeCell.EndCell.Column.Index;

            int h = mca.EndCell.Row.Index - mca.MergeCellCollectionRect.FirstMergeCell.EndCell.Row.Index;

            if (mca.EndCell.Row.Index > mca.MergeCellCollectionRect.FirstMergeCell.EndCell.Row.Index)
            {
                SetMergeCellToDown(mca);
                return;
            }

            if (mca.EndCell.Row.Index < mca.MergeCellCollectionRect.FirstMergeCell.BeginCell.Row.Index)
            {
                SetMergeCellToTop(mca);
                return;
            }

            if (mca.EndCell.Column.Index > mca.MergeCellCollectionRect.FirstMergeCell.EndCell.Column.Index)
            {
                SetMergeCellToRight(mca);
                return;
            }

            if (mca.EndCell.Column.Index < mca.MergeCellCollectionRect.FirstMergeCell.BeginCell.Column.Index)
            {
                SetMergeCellToLeft(mca);
                return;
            }
        }

        public static void DrawCellText(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle rect, string text)
        {
            if (cell == null)
                return;
            if (string.IsNullOrEmpty(text))
                return;
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(cell.HorizontalAlignment, cell.VerticalAlignment, cell.DirectionVertical);

            Color forecolor = Color.Empty;
            if (cell.Grid.FocusedCell == cell)
            {
                forecolor = cell.FocusForeColor;
            }

            if (cell.Rect.Contains(g.ClientPoint))
            {
                forecolor = cell.MouseOverForeColor;
            }
            if (g.MouseButtons == MouseButtons.Left && cell.Grid.FocusedCell == cell)
            {
                forecolor = cell.MouseDownForeColor;
            }
            if (cell.Selected)
            {
                forecolor = cell.SelectForceColor;
            }
            if (forecolor == Color.Empty)
            {
                forecolor = cell.ForeColor;
            }

            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                rect.Location = new Point(rect.Location.X, rect.Location.Y + 2);
                if (cell.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, cell.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, cell.Font, sb, rect, sf);
                }
            }
        }
#endregion
    }
}
