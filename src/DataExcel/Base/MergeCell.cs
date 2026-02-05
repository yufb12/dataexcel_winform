#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using Feng.Utils;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using Feng.Print;
using Feng.Excel.Designer;
using System.Drawing.Design;
using Feng.Data;
using Feng.Excel.Print;
using Feng.Enums;
using Feng.Excel.Args; 
using Feng.Excel.Interfaces;
using Feng.Excel.Styles;
using Feng.Excel.App;
using Feng.Excel.Actions;
using Feng.Forms.Base;
using Feng.Forms.Views;

namespace Feng.Excel.Base
{
    [Serializable]
    public class MergeCell : IMergeCell
    { 
        private MergeCell()
        { 
        }

        public MergeCell(DataExcel grid)
        { 
            _grid = grid;
        }

        public MergeCell(ICell firstcell, ICell endcell)
        {
            _grid = firstcell.Grid;
            int rmin = System.Math.Min(firstcell.Row.Index, endcell.Row.Index);
            int cmin = System.Math.Min(firstcell.Column.Index, endcell.Column.Index);
            int rmax = System.Math.Max(firstcell.Row.Index, endcell.Row.Index);
            int cmax = System.Math.Max(firstcell.Column.Index, endcell.Column.Index);
            _firstcell = firstcell.Grid[rmin, cmin];
            _firstcell.VerticalAlignment = StringAlignment.Center;
            _firstcell.HorizontalAlignment = StringAlignment.Center;
            _endcell = firstcell.Grid[rmax, cmax];

            Refresh();
        }

        public virtual void InSertRow(IRow row)
        {
            if (this.BeginCell.Row.Index <= row.Index && this.EndCell.Row.Index >= row.Index)
            {
                ReSetCellParent();
            }
        }

        public virtual void DeleteRow(IRow row)
        {
            if (this.BeginCell.Row.Index == row.Index)
            {
                this._firstcell = this.Grid[this.BeginCell.Row.Index + 1, this.BeginCell.Column.Index];
            }
            else if (this.EndCell.Row.Index == row.Index)
            {
                this._endcell = this.Grid[this.EndCell.Row.Index - 1, this.EndCell.Column.Index];
            }
            else
            {
                return;
            }
            Refresh();

        }

        public virtual void InSertColumn(IColumn column)
        {
            if (this.BeginCell.Column.Index >= column.Index && this.EndCell.Column.Index <= column.Index)
            {
                ReSetCellParent();
            }
        }

        public virtual void DeleteColumn(IColumn column)
        {
            if (this.BeginCell.Column.Index == column.Index)
            {
                this._firstcell = this.Grid[this.BeginCell.Row.Index, this.BeginCell.Column.Index + 1];
            }
            else if (this.EndCell.Column.Index == column.Index)
            {
                this._endcell = this.Grid[this.EndCell.Row.Index, this.EndCell.Column.Index - 1];
            }
            else
            {
                return;
            }
            Refresh();
        }

        #region IDraw 成员
        private int lastdrawinde = -1;
        public virtual int LastDrawIndex { get; set; }
        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        { 
            try
            { 
                if (lastdrawinde == this.Grid.FreshVersion)
                {
                    return false;
                }
                string text = this.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    text = Feng.Utils.ConvertHelper.ToString(this.Value);
                }
                lastdrawinde = this.Grid.FreshVersion;
                DrawFront(g, this.Rect, this.ClipBounds(), this.Text, false, false, null);
                //g.Graphics.DrawRectangle(Pens.Red, this.ClipBounds());
                //g.Graphics.FillRectangle(Brushes.Yellow, this.ClipBounds());
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
#if DEBUG2
            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, Brushes.Blue, this.Rect);
#endif
            return false;
        }

        public void DrawFront(Feng.Drawing.GraphicsObject g, Rectangle bounds, Rectangle clipbounds, object value, bool print, bool printbindingvalue, PrintArgs printArgs)
        {
            if (!this.Visible)
            {
                if (!(this.Grid.InDesign))
                {
                    return;
                }
            }
            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            Rectangle clip = Rectangle.Intersect(Rectangle.Round(g.Graphics.ClipBounds), clipbounds);
            g.Graphics.SetClip(clip);
 
            BeforeDrawCellArgs e = new BeforeDrawCellArgs(g, this);
            this.Grid.OnBeforeDrawCell(this, e);
            if (e.Cancel)
            {
                goto LabelEnd;
            }
            if (print)
            {
                DrawRectPrint(g, bounds, value, printbindingvalue, printArgs);
            }
            else
            {
                DrawRectForm(g, bounds, value);
            }
            DrawCellArgs DrawCellArgs = new DrawCellArgs(g, this);
            this.Grid.OnDrawCell(DrawCellArgs);
        LabelEnd:
            g.Graphics.Restore(gs);
        }

        public void DrawRectForm(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
 
            if (this.OwnEditControl != null)
            {
                if (!this.OwnEditControl.DrawCell(this, g))
                {
                    DrawCell(g, bounds, value);
                }
            }
            else
            {
                DrawCell(g, bounds, value);
            }
        }

        public void DrawRectPrint(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool printbindingvalue, PrintArgs printArgs)
        { 

            if (this.OwnBackCell != null)
            {
                this.OwnBackCell.PrintValue(printArgs, value); 
            }

            if (this.OwnEditControl != null)
            {
                if (printbindingvalue)
                {
                    if (!this.OwnEditControl.PrintValue(this, printArgs, bounds, value))
                    {
                        DrawCell(g, bounds, value);
                    }
                }
                else
                {
                    if (!this.OwnEditControl.PrintCell(this, printArgs, bounds))
                    {
                        DrawCell(g, bounds, value);
                    }
                } 
            }
            else
            {
                DrawCell(g, bounds, value);
            }
        }

        public void DrawRectBack(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool print, bool printbindingvalue, PrintArgs printArgs)
        {
            if (!this.Visible)
            {
                if (!(this.Grid.InDesign))
                {
                    return;
                }
            }
            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            Rectangle clip = Rectangle.Intersect(Rectangle.Round(g.Graphics.ClipBounds), bounds);
            g.Graphics.SetClip(clip);
            try
            {
                BeforeDrawCellBackArgs e = new BeforeDrawCellBackArgs(g, this);
                this.Grid.OnBeforeDrawCellBack(this, e);
                if (e.Cancel)
                {
                    return;
                }
                if (print)
                {
                    DrawRectBackPrint(g, bounds, printArgs);
                }
                else
                {
                    DrawRectBackForm(g, bounds);
                }

                DrawCellBackArgs DrawCellArgs = new DrawCellBackArgs(g, this);
                this.Grid.OnDrawCellBack(DrawCellArgs);
            }
            finally
            {
                g.Graphics.Restore(gs);
            }

        }

        public void DrawRectBackForm(Feng.Drawing.GraphicsObject g, Rectangle bounds )
        {
           
            if (this.OwnBackCell != null)
            {
                this.OwnBackCell.OnDrawBack(this, g); 
            }

            if (this.OwnEditControl != null)
            {
                if (!this.OwnEditControl.DrawCellBack(this, g))
                {
                    DrawBackColor(g, bounds);
                    DrawBackImage(g, bounds);
                }
            }
            else
            {
                DrawBackColor(g, bounds);
                DrawBackImage(g, bounds);
            }
 

        }

        public void DrawRectBackPrint(Feng.Drawing.GraphicsObject g, Rectangle bounds, PrintArgs printArgs)
        {
           

            if (this.OwnBackCell != null)
            {
                this.OwnBackCell.PrintBack(printArgs); 
            }

            if (this.OwnEditControl != null)
            {
                if (!this.OwnEditControl.PrintCellBack(this, printArgs))
                {
                    if (this.IsPrintBackImage)
                    {
                        DrawBackImage(g, bounds);
                    } if (this.IsPrintBackColor)
                    {
                        DrawBackColor(g, bounds);
                    }
                } 
            }  

        }

        private void DrawCell(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            if (this.InEdit)
            {
                return;
            }
            if (value == null)
            {
                return;
            }

            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(
                this.HorizontalAlignment, this.VerticalAlignment, this.DirectionVertical);
            string text = value.ToString();
            //text = this.BeginCell.ToString() + "   " + this.EndCell.ToString();
            Rectangle rect = bounds;

            Color forecolor = this.ForeColor;
            if (this.Grid.FocusedCell == this)
            {
                if (this.FocusForeColor != Color.Empty)
                {
                    forecolor = this.FocusForeColor;
                }
            }

            if (this.Rect.Contains (g.ClientPoint))
            {
                if (this.MouseOverForeColor != Color.Empty)
                {
                    forecolor = this.MouseOverForeColor;
                }
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell ==this)
            {
                if (this.MouseDownForeColor != Color.Empty)
                {
                    forecolor = this.MouseDownForeColor;
                }
            }
            if (this.Selected)
            {
                if (this.SelectForceColor != Color.Empty)
                {
                    forecolor = this.SelectForceColor;
                }
            }
            if (forecolor == Color.Empty)
            {
                forecolor = Color.Black;
            }

            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                //if (text == "千分尺15")
                //{

                //}
                //if (this.BeginCell.Row.Index == 6 && this.BeginCell.Column.Index == 4)
                //{

                //}
                rect.Location = new Point(rect.Location.X, rect.Location.Y);
                if (this.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Font, sb, rect, sf);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Font, sb, rect, sf);

                }
            }
        }
        int lastdrawbackindex = 0;
        public virtual bool OnDrawBack(object sender, GraphicsObject g)
        {
            try
            { 
                if (lastdrawbackindex == this.Grid.FreshVersion)
                {
                    return false;
                }
                lastdrawbackindex = this.Grid.FreshVersion;
                DrawRectBack(g, this.Rect, this.Text, false, false, null);

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return false;
        }

        public virtual bool PrintBack(PrintArgs e)
        {
            try
            {
                if (lastdrawinde == this.Grid.FreshVersion)
                {
                    return false;
                }
                lastdrawinde = this.Grid.FreshVersion;
                DrawRectBack(e.Graphic, this.Rect, this.Text, true, false, e);

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return false;
        }
         
        private bool DrawBackImage(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            Bitmap backimage = null;
            Rectangle rect = bounds;
            if (this.Grid.FocusedCell == this)
            {
                backimage = this.FocusImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(FocusImageSizeMode, backimage, rect);
            }

            if (this.ReadOnly)
            {
                backimage = this.ReadOnlyImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(ReadOnlyImageSizeMode, backimage, rect);
            }
            if (this.Rect.Contains (g.ClientPoint))
            {
                backimage = this.MouseOverImage;
                rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(MouseOverImageSizeMode, backimage, rect);
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell ==this)
            {
                backimage = this.MouseDownImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(MouseDownImageSizeMode, backimage, rect);
            }
            if (backimage == null)
            {
                backimage = this.BackImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(BackImgeSizeMode, backimage, rect);
            }
            if (backimage != null)
            {
                g.Graphics.DrawImage(backimage, rect);
                return true;
            }
            return false;
        }

#warning DrawBackColorEvent
        private bool DrawBackColor(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            Color backcolor = Color.Empty;
            if (this.Grid.FocusedCell == this)
            {
                if (this.FocusBackColor != Color.Empty)
                {
                    backcolor = this.FocusBackColor;
                }
            }

            if (this.Rect.Contains (g.ClientPoint))
            {
                if (this.MouseOverBackColor != Color.Empty)
                {
                    backcolor = this.MouseOverBackColor;
                }
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell ==this)
            {
                if (this.MouseDownBackColor != Color.Empty)
                {
                    backcolor = this.MouseDownBackColor;
                }
            }
            if (backcolor == Color.Empty)
            {
                backcolor = this.BackColor;
            }
            if (backcolor != this.Grid.BackColor)
            {
                SolidBrush sb = SolidBrushCache.GetSolidBrush(backcolor);
                Rectangle rect = bounds;
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, sb, rect);
                return true;
            }
            return false;
        }

        private void DrawCell(Feng.Drawing.GraphicsObject g, Rectangle bounds, bool print)
        {
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(
                  this.HorizontalAlignment, this.VerticalAlignment, this.DirectionVertical);
            string text = this.Text;

            Rectangle rect = bounds;

            Color forecolor = Color.Empty;
            if (this.Grid.FocusedCell == this)
            {
                forecolor = this.BeginCell.FocusForeColor;
            }

            if (this.Rect.Contains (g.ClientPoint))
            {
                forecolor = this.BeginCell.MouseOverForeColor;
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell ==this)
            {
                forecolor = this.BeginCell.MouseDownForeColor;
            }
            if (forecolor == Color.Empty)
            {
                forecolor = this.ForeColor;
            }

            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                if (this.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Font, sb, rect, sf);

                }
            }
        }

        private int lastdrawborderindex = 0;
        public virtual void DrawBorder(Feng.Drawing.GraphicsObject g)
        {
            if (lastdrawborderindex == this.Grid.FreshVersion)
            {
                return;
            }
            lastdrawborderindex = this.Grid.FreshVersion;
            DrawBorder(g, this.Rect, false);
        }

        public virtual void DrawBorder(Feng.Drawing.GraphicsObject g, Rectangle bounds, bool print)
        {
            if (BorderStyle != null)
            {
                //DrawCellBorderArgs DrawCellBorderArgs = new DrawCellBorderArgs(g, this);
                //this.Grid.OnDrawCellBorder(DrawCellBorderArgs);
                //if (!DrawCellBorderArgs.Handled)
                //{
                    DrawLine(g, bounds, print);
                //    return;
                //}
            }

        }

        private void DrawLine(Feng.Drawing.GraphicsObject g, Rectangle bounds, bool print)
        {
            if (BorderStyle.LeftLineStyle.Visible)
            {
                Pen pen = BorderStyle.LeftLineStyle.GetPen();
                g.Graphics.DrawLine(pen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
            }

            if (BorderStyle.TopLineStyle.Visible)
            {
                Pen pen = BorderStyle.TopLineStyle.GetPen();
                g.Graphics.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Top);
            }

            if (BorderStyle.RightLineStyle.Visible)
            {
                Pen pen = BorderStyle.RightLineStyle.GetPen();
                g.Graphics.DrawLine(pen, bounds.Right, bounds.Top, bounds.Right, bounds.Bottom);
            }


            if (BorderStyle.BottomLineStyle.Visible)
            {
                Pen pen = BorderStyle.BottomLineStyle.GetPen();
                g.Graphics.DrawLine(pen, bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
            }


            if (BorderStyle.LeftTopToRightBottomLineStyle.Visible)
            {
                Pen pen = BorderStyle.LeftTopToRightBottomLineStyle.GetPen();
                g.Graphics.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
            }

            if (BorderStyle.LeftBottomToRightTopLineStyle.Visible)
            {
                Pen pen = BorderStyle.LeftBottomToRightTopLineStyle.GetPen();
                g.Graphics.DrawLine(pen, bounds.Left, bounds.Bottom, bounds.Right, bounds.Top);
            }
        }

        public void DrawGridLine(Feng.Drawing.GraphicsObject g)
        {
            if (this.Grid.ShowGridColumnLine)
            {
                DrawGridRightLine(g);
            }
            if (this.Grid.ShowGridRowLine)
            {
                DrawGridBottomLine(g);
            }
        }
 
        public virtual void DrawGridRightLine(Feng.Drawing.GraphicsObject g)
        {
            if (this.BorderStyle != null)
            {
                if (!this.BorderStyle.RightLineStyle.Visible)
                {
                    if (this.Grid.ShowGridColumnLine)
                    {
                        this.Grid.DrawGridLine(g, this.Right, this.Top, this.Right, this.Bottom);
                    }
                }
            }
        }

        public virtual void DrawGridBottomLine(Feng.Drawing.GraphicsObject g)
        {
            if (this.BorderStyle != null)
            {
                if (!this.BorderStyle.BottomLineStyle.Visible)
                {
                    this.Grid.DrawGridLine(g, this.Left, this.Bottom, this.Right, this.Bottom);
                }
            }
        }

        #region IPrintBorder 成员

        public void PrintBorder(PrintArgs e)
        {
            Page page = e.CurrentPage as Page;
            if (!page.PrintBorderList.Contains(this))
            {
                Rectangle bounds = Rectangle.Empty;
                Rectangle rect = DataExcel.GetPrintBounds(e.CurrentLocation
                    , this.BeginCell, this.EndCell
                    , ref bounds
                    , page.StartColumnIndex, page.EndColumnIndex
                    , page.StartRowIndex, page.EndRowIndex);
                Feng.Drawing.GraphicsObject gob = e.Graphic;
                DrawBorder(gob, rect, true);
                page.PrintBorderList.Add(this);
            }

        }

        #endregion
        #endregion

        #region IPrint 成员

        #region IPrint 成员

        public virtual bool Print(PrintArgs e)
        {
            Page page = e.CurrentPage as Page;
            if (!page.PrintList.Contains(this))
            {
                Rectangle rect = this.Rect;
                rect.Location = e.CurrentLocation;
                rect = DataExcel.GetPrintBounds(e.CurrentLocation
                    , this.BeginCell, this.EndCell

                    , page.StartColumnIndex, page.EndColumnIndex
                    , page.StartRowIndex, page.EndRowIndex);
                Feng.Drawing.GraphicsObject gob = e.Graphic;
                page.PrintList.Add(this);

                PrintCellArgs pe = new PrintCellArgs();
                pe.Cell = this;
                pe.CurrentPage = e.Index; ;
                pe.Rect = this.Rect;
                pe.TotalPage = e.Total;
                pe.Value = this.Value;
                this.Grid.OnPrintCell(pe);
                if (pe.Cancel)
                {
                    return false;
                }
                this.DrawRectBack(gob, rect, this.Value, true, false, e);
                this.DrawFront(gob, rect, rect, this.Value, true, false, e);

            }
            return false;
        }


        #endregion

        #endregion

        #region PrintValue 成员

        public virtual bool PrintValue(PrintArgs e, object value)
        {
            Page page = e.CurrentPage as Page;
            if (!page.PrintList.Contains(this))
            {
                Rectangle bounds = this.Rect;
                bounds.Location = e.CurrentLocation;
                bounds = DataExcel.GetPrintMergeCellRect(e.CurrentLocation, this
                    , this.BeginCell, this.EndCell

                    , page.StartColumnIndex, page.EndColumnIndex
                    , page.StartRowIndex, page.EndRowIndex);
                Feng.Drawing.GraphicsObject gob = e.Graphic;
                page.PrintList.Add(this);
                PrintCellArgs pe = new PrintCellArgs();
                pe.Cell = this;
                pe.CurrentPage = e.Index; ;
                pe.Rect = this.Rect;
                pe.TotalPage = e.Total;
                pe.Value = Value;
                this.Grid.OnPrintCell(pe);
                if (pe.Cancel)
                {
                    return false;
                }
                this.DrawFront(gob, bounds, bounds, value, true, true, e);
            }
            return true;
        }

        #endregion

        #region ICellRange 成员
        public virtual void ReSetCellParent()
        {
            if (this._firstcell == null)
            {
                return;
            }
            if (this._endcell == null)
            {
                return;
            }
            for (int i = this._firstcell.Row.Index; i <= this._endcell.Row.Index; i++)
            {
                for (int j = this._firstcell.Column.Index; j <= this._endcell.Column.Index; j++)
                {
                    ICell cell = this.Grid[i, j];
                    cell.OwnMergeCell = this;
                }
            }
        }

        public virtual void Close()
        {
            this.Grid.BeginReFresh();
            if (this._firstcell == null)
            {
                return;
            }
            if (this._endcell == null)
            {
                return;
            }
            for (int i = this._firstcell.Row.Index; i <= this._endcell.Row.Index; i++)
            {
                for (int j = this._firstcell.Column.Index; j <= this._endcell.Column.Index; j++)
                {
                    ICell cell = this.Grid[i, j];
                    cell.OwnMergeCell = null;
                }
            }
            this.Grid.EndReFresh();
        }

        //           public virtual  void SetSize()
        //        {
        //#if ControlStop
        //            if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
        //            {
        //                System.Diagnostics.Debugger.Break();
        //            }
        //#endif

        //            if (this._firstcell.Top > this._endcell.Top)
        //            {
        //                _top = this._endcell.Top;
        //                int boom = this._firstcell.Boom;
        //                _height = boom - _top;

        //                if (this.EndCell.Row.Index < this.Grid.FirstDisplayedScrollingRowIndex)
        //                {
        //                    this.Top = this.Grid.ColumnHeaderHeight;
        //                    int h = 0;
        //                    for (int i = this.Grid.FirstDisplayedScrollingRowIndex; i <= this.FirstCell.Row.Index; i++)
        //                    {
        //                        IRow r = this.Grid.Rows[i];

        //                        if (r == null)
        //                        {
        //                            h = h + this.Grid.RowHeight;
        //                        }
        //                        else
        //                        {
        //                            if (r.Visible)
        //                            {
        //                                h = h + r.Height;
        //                            }
        //                        }
        //                    }
        //                    this.Height = h;
        //                }
        //                else
        //                {
        //                    this.Top = this.EndCell.Row.Top;
        //                }
        //            }
        //            else
        //            {
        //                _top = this._firstcell.Top;
        //                int boom = this._endcell.Boom;
        //                _height = boom - _top;
        //                if (this.FirstCell.Row.Index < this.Grid.FirstDisplayedScrollingRowIndex)
        //                {
        //                    this.Top = this.Grid.ColumnHeaderHeight;
        //                    int h = 0;
        //                    for (int i = this.Grid.FirstDisplayedScrollingRowIndex; i <= this.EndCell.Row.Index; i++)
        //                    {
        //                        IRow r = this.Grid.Rows[i];

        //                        if (r == null)
        //                        {
        //                            h = h + this.Grid.RowHeight;
        //                        }
        //                        else
        //                        {
        //                            if (r.Visible)
        //                            {
        //                                h = h + r.Height;
        //                            }
        //                        }
        //                    }
        //                    this.Height = h;
        //                }
        //                else
        //                {
        //                    this.Top = this.FirstCell.Row.Top;
        //                }
        //            }

        //            if (this._firstcell.Left > this._endcell.Left)
        //            {
        //                _left = this._endcell.Left;
        //                int right = this._firstcell.Right;
        //                _width = right - _left;
        //                if (this.EndCell.Row.Index < this.Grid.FirstDisplayedScrollingColumnIndex)
        //                {
        //                    this.Left = this.Grid.RowHeaderWidth;
        //                    int w = 0;
        //                    for (int i = this.Grid.FirstDisplayedScrollingColumnIndex; i <= this.FirstCell.Column.Index; i++)
        //                    {
        //                        IColumn r = this.Grid.Columns[i];
        //                        if (r == null)
        //                        {
        //                            w = w + this.Grid.ColumnWidth;
        //                        }
        //                        else
        //                        {
        //                            if (r.Visible)
        //                            {
        //                                w = w + r.Width;
        //                            }
        //                        }
        //                    }
        //                    this.Width = w;
        //                }
        //                else
        //                {
        //                    this.Left = this.EndCell.Column.Left;
        //                }
        //            }
        //            else
        //            {
        //                _left = this._firstcell.Left;
        //                int right = this._endcell.Right;
        //                _width = right - _left;
        //                if (this.FirstCell.Column.Index < this.Grid.FirstDisplayedScrollingColumnIndex)
        //                {
        //                    this.Left = this.Grid.RowHeaderWidth;
        //                    int w = 0;
        //                    for (int i = this.Grid.FirstDisplayedScrollingColumnIndex; i <= this.EndCell.Column.Index; i++)
        //                    {
        //                        IColumn r = this.Grid.Columns[i];
        //                        if (r == null)
        //                        {
        //                            w = w + this.Grid.ColumnWidth;
        //                        }
        //                        else
        //                        {
        //                            if (r.Visible)
        //                            {
        //                                w = w + r.Width;
        //                            }
        //                        }
        //                    }
        //                    this.Width = w;
        //                }
        //                else
        //                {
        //                    this.Left = this.FirstCell.Column.Left;
        //                }
        //            }


        //#if TestReSetSize

        //            Console.WriteLine("ReSetSize：" + this.Rect.ToString());
        //#endif
        //        }
        private ICell _firstcell = null;
        public virtual ICell BeginCell
        {
            get { return this._firstcell; }
            set
            {
                this._firstcell = value;
                if (this._endcell == null)
                {
                    this._endcell = value;
                }
                Refresh();
            }
        }

        private ICell _endcell = null;
        public virtual ICell EndCell
        {
            get
            {
                if (this._endcell == null)
                {
                    return this._firstcell;
                }
                return this._endcell;
            }

            set
            {
                this._endcell = value;
                Refresh();
                this.ReSetCellParent();
            }
        }

        public ICell MinCell
        {
            get
            {

                int minr = System.Math.Min(_firstcell.Row.Index, _endcell.Row.Index);
                int minc = System.Math.Min(_firstcell.Column.Index, _endcell.Column.Index);
                return this.Grid[minr, minc];
            }
        }

        public ICell MaxCell
        {
            get
            {
                int maxr = System.Math.Max(_firstcell.Row.Index, _endcell.Row.Index);
                int maxc = System.Math.Max(_firstcell.Column.Index, _endcell.Column.Index);
                return this.Grid[maxr, maxc];
            }
        }

        #endregion

        #region IMergeCell 成员

        private IMergeCellCollection _MergeCellCollection;

        public virtual IMergeCellCollection MergeCellCollection
        {
            get
            {
                return _MergeCellCollection;
            }
            set
            {
                _MergeCellCollection = value;
            }
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataExcel Grid
        {
            get { return this._grid; }
        }

        #endregion

        #region IBounds 成员

        private int _left = 0;
        public virtual int Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        private int _height = 0;
        public virtual int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public virtual int Right
        {
            get
            {

                return _width + _left;
            }

        }
        public virtual int Bottom
        {
            get
            {

                return _top + _height;
            }

        }
        private int _top = 0;
        public virtual int Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }

        private int _width = 0;
        public virtual int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public virtual Rectangle Rect
        {
            get
            {
                return new Rectangle(this._left, this._top, this._width, this._height);
            }
        }
        private Rectangle _ClipBounds = Rectangle.Empty;
        public virtual Rectangle ClipBounds()
        {
            if (_ClipBounds == Rectangle.Empty)
            {
                return this.Rect;
            }
            return this._ClipBounds;
        }
        #endregion

        //#region IClipRectangle 成员
        //private Rectangle _ClipRectangle = Rectangle.Empty;
        //public virtual Rectangle ClipRectangle
        //{
        //    get { return _ClipRectangle; }
        //}

        //#endregion

        #region IDraw 成员
        private int _freshversion = 0;

        #endregion

        #region ISelected 成员
        private bool _selected = false;
        public virtual bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                if (value)
                {
                    this.Grid.Selecteds.Add(this);
                }
            }
        }


        #endregion

        #region ISelectColor 成员 
        public virtual Color SelectBackColor
        {
            get { return this.BeginCell.SelectBackColor; }
            set { this.BeginCell.SelectBackColor = value; }
        }

        #endregion

        #region ISelectColor 成员
         
        public virtual Color SelectForceColor
        {
            get { return this.BeginCell.SelectForceColor; }
            set { this.BeginCell.SelectForceColor = value; }
        }

        #endregion

        #region ISelectBorderColor 成员

        public virtual System.Drawing.Color SelectBorderColor
        {
            get { return this.BeginCell.SelectBorderColor; }
            set { this.BeginCell.SelectBorderColor = value; }
        }

        #endregion

 

        #region ISetSize 成员

        public virtual void Refresh()
        {
            //ReSetCellParent();
            Rectangle clipbounds = Rectangle.Empty;
            Rectangle rect = DataExcel.GetRect(this.BeginCell, this.EndCell, ref clipbounds);
            //this._ClipRectangle = bounds;
            this.Top = rect.Top;
            this.Left = rect.Left;
            this.Width = rect.Width;
            this.Height = rect.Height;
        }


        #endregion

        #region 系统属性
         
        [DefaultValue(true)]
        public virtual bool AutoMultiline
        {
            get
            {
                return this.BeginCell.AutoMultiline;
            }
            set
            {
                this.BeginCell.AutoMultiline = value;
            }
        }

        #endregion

        #region IToString 成员
        public override string ToString()
        {
#if DEBUG
            return string.Format("BeginCell:{0} EndCell:{1} Text:{2}", this.BeginCell.Name, this.EndCell.Name, this.Text);
#endif
            return this.Text;
        }
#if Test
           public virtual  string AText
        {
            get
            {
                string str = string.Format("Name:{6}Value:{5} Row Index:{0};Column Index:{1};Text:{2} Point({3},{4})"
                    , this.Row.Index
                    , this.Column.Index
                    , this.Text, this.Rect.Location.X
                    , this.Rect.Location.Y
                    , this.Value
                    , this.Name);
                return str;
            }

        }
#endif

        #endregion

        #region 保存文件

        public virtual void Save(IStream filestream)
        {

        }

        public virtual byte[] GetData()
        {
            return this.Grid.Encoding.GetBytes(this.Text);
        }

        #endregion

        #region IControlColor 成员
        public virtual Color ForeColor
        {
            get
            {
                return this.BeginCell.ForeColor;
            }
            set
            {
                this.BeginCell.ForeColor = value;
            }
        }

        public virtual Color BackColor
        {
            get
            {
                return this.BeginCell.BackColor;
            }
            set
            {
                this.BeginCell.BackColor = value;
            }
        }

        #endregion

        #region IText 成员

        public virtual string Text
        {
            get { return this.BeginCell.Text; }

            set
            {
                this.BeginCell.Text = (value);
            }
        }

        public virtual string Text1
        {
            get { return this.BeginCell.Text1; }

            set
            {
                this.BeginCell.Text1 = (value);
            }
        }
        public virtual string Text2
        {
            get { return this.BeginCell.Text2; }

            set
            {
                this.BeginCell.Text2 = (value);
            }
        }
        public virtual string Text3
        {
            get { return this.BeginCell.Text3; }

            set
            {
                this.BeginCell.Text3 = (value);
            }
        }
        #endregion

        #region ICurrentRow 成员
        [ReadOnly(true)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual IRow Row
        {
            get
            {
                return this.BeginCell.Row;
            }
            set
            {
                this.BeginCell.Row = value;
            }
        }

        #endregion

        #region ICurrentColumn 成员
        [ReadOnly(true)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual IColumn Column
        {
            get
            {
                return this.BeginCell.Column;
            }
            set
            {
                this.BeginCell.Column = value;
            }
        }

        #endregion

        #region IValue 成员

        public virtual object Value
        {
            get
            {
                return this.BeginCell.Value;
            }
            set
            {
                this.BeginCell.Value = value;
            }
        }

        #endregion

        #region IExpressionText 成员

        public virtual string Expression
        {
            get
            {
                return this.BeginCell.Expression;
            }
            set
            {
                this.BeginCell.Expression = value;
            }
        }

        #endregion

        #region IAutoExecuteExpress 成员

        public virtual bool AutoExecuteExpress
        {
            get
            {
                return this.BeginCell.AutoExecuteExpress;
            }
            set
            {
                this.BeginCell.AutoExecuteExpress = value;
            }
        }

        #endregion

        #region IExecuteExpress 成员

        public virtual void ExecuteExpression()
        {
            this.BeginCell.ExecuteExpression();

        }
        #endregion

        #region IHorizontalAlignment 成员

        public virtual StringAlignment HorizontalAlignment
        {
            get
            {
                return this.BeginCell.HorizontalAlignment;
            }
            set
            {
                this.BeginCell.HorizontalAlignment = value;
            }
        }

        #endregion

        #region IVerticalAlignment 成员
        public virtual StringAlignment VerticalAlignment
        {
            get
            {
                return this.BeginCell.VerticalAlignment;
            }
            set
            {
                this.BeginCell.VerticalAlignment = value;
            }
        }

        #endregion

        #region ICellType 成员

        public virtual CellType CellType
        {
            get
            {
                return this.BeginCell.CellType;
            }
            set
            {
                this.BeginCell.CellType = value;
            }
        }

        #endregion

        #region IFont 成员

        public virtual Font Font
        {
            get
            {
                return this.BeginCell.Font;
            }
            set
            {
                this.BeginCell.Font = value;
            }
        }

        #endregion

        #region IBorderSetting 成员

        public virtual CellBorderStyle BorderStyle
        {
            get
            {
                return this.BeginCell.BorderStyle;
            }
            set
            {
                this.BeginCell.BorderStyle = value;
            }
        }
        #endregion

        #region IName 成员

        public virtual string Name
        {
            get
            {
                return this.Column.Name + this.Row.Name;
            }
            set { }
        }

        #endregion

        #region IFormat 成员
        public virtual FormatType FormatType
        {
            get
            {
                return this.BeginCell.FormatType;
            }
            set
            {
                this.BeginCell.FormatType = value;
            }
        }

        public virtual string FormatString
        {
            get
            {
                return this.BeginCell.FormatString;
            }
            set
            {
                this.BeginCell.FormatString = value;
            }
        }

        #endregion

        #region IUpdateVersion 成员

        public virtual int UpdateVersion
        {
            get
            {
                return this.BeginCell.UpdateVersion;
            }
            set
            {
                this.BeginCell.UpdateVersion = value;
            }
        }

        #endregion

        #region IOwnEditControl 成员

        public virtual ICellEditControl OwnEditControl
        {
            get
            {
                return this.BeginCell.OwnEditControl;
            }
            set
            {
                this.BeginCell.OwnEditControl = value;
            }
        }

        #endregion

        #region IInhertReadOnly 成员
        public virtual bool InhertReadOnly
        {
            get
            {
                return this.BeginCell.InhertReadOnly;
            }
            set
            {
                this.BeginCell.InhertReadOnly = value;
            }
        }
        #endregion

        #region IReadOnly 成员

        public virtual bool ReadOnly
        {
            get
            {
                return this.BeginCell.ReadOnly;
            }
            set
            {
                this.BeginCell.ReadOnly = value;
            }
        }

        #endregion

        #region ICellEvents 成员

        public virtual bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseUp(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseUp))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseUp, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseUp);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseUp(this, e);
            return false;
        }

        public virtual bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseMove(this, e, ve))
                {
                    return true;
                }
            }

            if (this.MouseOverBackColor != Color.Empty || this.MouseOverForeColor != Color.Empty || this.MouseOverImage != null)
            {
                this.Grid.ReFresh();
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseMove))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseMove, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseMove);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseMove(this, e);
            return false;
        }

        public virtual bool OnMouseLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseLeave(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseLeave))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseLeave, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseLeave);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseLeave(this);
            return false;
        }

        public virtual bool OnMouseHover(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseHover(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseHover))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseHover, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseHover);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseHover(this);
            return false;
        }

        public virtual bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve)
        {
            if ((this.EditMode & EditMode.MouseEnter) == EditMode.MouseEnter)
            {
                this.InitEdit(this);
            }

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseEnter(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseEnter))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseEnter, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseEnter);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseEnter(this);
            return false;
        }

        public virtual bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {


            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseDown(this, e, ve))
                {
                    return true;
                }
            }

            if (this.MouseDownBackColor != Color.Empty || this.MouseDownForeColor != Color.Empty || this.MouseDownImage != null)
            {
                this.Grid.ReFresh();
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseDown))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseDown, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseDown);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseDown(this, e);
            return false;
        }

        public virtual bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseDoubleClick(this, e, ve))
                {
                    return true;
                }
            }

            if ((this.EditMode & EditMode.Default) == EditMode.Default)
            {
                this.InitEdit(this);
            }
            if ((this.EditMode & EditMode.DoubleClick) == EditMode.DoubleClick)
            {
                this.InitEdit(this);
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseDoubleClick))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseDoubleClick, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseDoubleClick);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseDoubleClick(this, e);
            return false;
        }

        public virtual bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if ((this.EditMode & EditMode.Click) == EditMode.Click)
            {
                this.InitEdit(this);
            }

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseClick(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnClick))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnClick, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnClick);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseClick(this, e);
            this.Grid.OnCellClick(this);
            return false;
        }

        public virtual bool OnMouseCaptureChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseCaptureChanged(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseCaptureChanged))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseCaptureChanged, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseCaptureChanged);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseCaptureChanged(this);
            return false;
        }

        public virtual bool OnClick(object sender, EventArgs e, EventViewArgs ve)
        {
            bool result = false;

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnClick(this, e, ve))
                {
                    result = true;
                    goto ExitFuntion;
                }
            }

        ExitFuntion:
            if (!string.IsNullOrWhiteSpace(this.PropertyOnClick))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnClick, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnClick);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellClick(this);
            return result;
        }

        public virtual bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            bool result = false;


            if ((this.EditMode & EditMode.KeyDown) == EditMode.KeyDown)
            {
                this.InitEdit(this);
            }

            else if (e.KeyCode == Keys.F2 && (this.EditMode & EditMode.F2) == EditMode.F2)
            {
                this.InitEdit(this);
            }
            //EditMode editMode = this.EditMode | EditMode.Default;
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnKeyDown(this, e, ve))
                {
                    result = true;
                    goto ExitFuntion;
                }
            }
        ExitFuntion:
            if (!string.IsNullOrWhiteSpace(this.PropertyOnKeyDown))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnKeyDown, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnKeyDown);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellKeyDown(this, e);
            return result;

        }

        public virtual bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl != null)
                {
                    if (this.OwnEditControl.OnKeyPress(this, e, ve))
                    {
                        return true;
                    }
                }
                //if (this.InEdit)
                //{
                //    char s = e.KeyChar;
                //    if (char.IsUpper(s))
                //    {
                //        SendKey.Send("{CAPSLOCK}(" + s + ")");
                //    }
                //    else
                //    {
                //        SendKey.Send(s.ToString());
                //    }
                //}
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnKeyPress))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnKeyPress, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnKeyPress);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellKeyPress(this, e);

            return false;
        }

        public virtual bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnKeyUp(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnKeyUp))
            {

                ActionArgs ae = new ActionArgs(this.PropertyOnKeyUp, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnKeyUp);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellKeyUp(this, e);
            return false;
        }

        public virtual bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnPreviewKeyDown(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnPreviewKeyDown))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnPreviewKeyDown, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnPreviewKeyDown);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellPreviewKeyDown(this, e);
            return false;
        }

        public virtual bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            if ((this.EditMode & EditMode.Default) == EditMode.Default)
            {
                this.InitEdit(this);
            }
            if ((this.EditMode & EditMode.DoubleClick) == EditMode.DoubleClick)
            {
                this.InitEdit(this);
            }

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnDoubleClick(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnDoubleClick))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnDoubleClick, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnDoubleClick);
                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellDoubleClick(this);
            return false;

        }

        public virtual bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseWheel(this, e, ve))
                {
                    return true;
                }
            }
            if (!string.IsNullOrWhiteSpace(this.PropertyOnMouseWheel))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnMouseWheel, this.Grid, this);
                ae.Arg = e;
                this.Grid.ExecuteAction(ae, this.PropertyOnMouseWheel);

                if (ae.Handle)
                {
                    return true;
                }
            }
            this.Grid.OnCellMouseWheel(this, e);
            return false;
        }

        public virtual bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnPreProcessMessage(this, ref   msg, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve)
        {

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnProcessCmdKey(this, ref   msg, keyData, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnProcessDialogChar(object sender, char charCode, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnProcessDialogChar(this, charCode, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnProcessDialogKey(object sender, Keys keyData, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnProcessDialogKey(this, keyData, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnProcessKeyEventArgs(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnProcessKeyEventArgs(this, ref m, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnProcessKeyMessage(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnProcessKeyMessage(this, ref m, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnProcessKeyPreview(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnProcessKeyPreview(this, ref m, ve))
                {
                    return true;
                }
            }
            return false;
        }
        [System.Diagnostics.DebuggerStepThrough()]
        public virtual bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnWndProc(this, ref m, ve))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region IChecked 成员

        //public virtual bool Checked
        //{
        //    get
        //    {
        //        return this.FirstCell.Checked;
        //    }
        //    set
        //    {
        //        this.FirstCell.Checked = value;
        //    }
        //}

        #endregion

        #region ICaption 成员

        public virtual string Caption
        {
            get
            {
                return this.BeginCell.Caption;
            }
            set
            {
                this.BeginCell.Caption = value;
            }
        }

        #endregion

        #region IInitEdit 成员
 
        int cellinitcout = 0;
 
        public virtual bool InitEdit(object obj)
        {
            if (this.InEdit)
            {
                return false;
            }

            BeforeInitEditCancelArgs e = new BeforeInitEditCancelArgs(this);
            this.Grid.OnBeforeCellInitEdit(e);
            if (e.Cancel)
            {
                return false;
            }
            if (this.ReadOnly)
            {
                return false;
            } 

            this.Grid.SetFoucsedCellSelect(this);
            this.Grid.AddEdit(this);
            this.Grid.BeginReFresh();
            ICellEditControl editcontrol = this.OwnEditControl; 
            if (editcontrol != null)
            {
                bool res = editcontrol.InitEdit(this);
                if (res)
                {
                    this.Grid.EditCell = this;
                }
            }
            this.Grid.OnCellInitEdit(this);
            this.Grid.EndReFresh();
            return this.InEdit;
            //if (this.InEdit)
            //{
            //    return false;
            //}

            //BeforeInitEditCancelArgs e = new BeforeInitEditCancelArgs(this);
            //this.Grid.OnBeforeCellInitEdit(e);
            //if (e.Cancel)
            //{
            //    return false;
            //}
            //if (this.ReadOnly)
            //{
            //    return false;
            //}
            //this.Grid.SetFoucsedCellSelect(this);
            ////if (!this.Enable)
            ////{
            ////    return false;
            ////}
            //this.Grid.AddEdit(this);
            //if (this.OwnEditControl == null)
            //{
            //    if (this.Row.DefaultCellEdit != null)
            //    {
            //        this.OwnEditControl = this.Row.DefaultCellEdit;
            //    }
            //    else
            //    {
            //        this.OwnEditControl = this.Grid.DefaultEdit;
            //    }
            //}
            //if (this.OwnEditControl != null)
            //{
            //    this.OwnEditControl.InitEdit(this);
            //    this.Grid.EditCell = this;
            //}
            //this.Grid.OnCellInitEdit(this);
            //return this.InEdit; 
        }

        #endregion

        #region IClear 成员

        public virtual void Clear()
        {
            this.Close();
        }

        #endregion

        #region ISetAllDeafultBoarder 成员

        public virtual void SetSelectCellBorderBorderOutside()
        {
            this.BeginCell.SetSelectCellBorderBorderOutside();
        }

        #endregion

        #region IEndEdit 成员
 
        int cellendcout = 0;
 
        public virtual void EndEdit()
        { 
            if (!this.InEdit)
            {
                return;
            }
            this.Grid.BeginReFresh();

            if (this.OwnEditControl != null)
            {
                this.OwnEditControl.EndEdit();
            }

            this.Grid.EndReFresh();
        }

        #endregion

        #region IInEdit 成员
        public virtual bool InEdit
        {
            get
            {
                return (this.Grid.EditCell == this);
            }
        }

        #endregion

        #region IDrawFunctionBorder 成员

        public virtual void DrawFunctionBorder(Feng.Drawing.GraphicsObject g, int index)
        {
            this.BeginCell.DrawFunctionBorder(g, index);
        }

        #endregion

        #region ICurrentIMergeCell 成员

        //   public virtual  IMergeCell MergeCell
        //{
        //    get
        //    {
        //        return null;
        //    }
        //    set
        //    {

        //    }
        //}

        #endregion

        #region IBackImage 成员

        public virtual Bitmap BackImage
        {
            get
            {
                return this.BeginCell.BackImage;
            }
            set
            {
                this.BeginCell.BackImage = value;
            }
        }

        #endregion

        #region ITextDirection 成员

        public virtual bool DirectionVertical
        {
            get
            {
                return this.BeginCell.DirectionVertical;
            }
            set
            {
                this.BeginCell.DirectionVertical = value;
            }
        }

        #endregion

        #region IIsMergeCell 成员

        public virtual bool IsMergeCell
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region IDateTime 成员

        //public virtual DateTime DateTime
        //{
        //    get
        //    {
        //        return this.FirstCell.DateTime;
        //    }
        //    set
        //    {
        //        this.FirstCell.DateTime = value;
        //    }
        //}

        #endregion


        #region IDisplayMember 成员
        private string _displaymember = string.Empty;
        public virtual string DisplayMember
        {
            get
            {
                return _displaymember;
            }
            set
            {
                _displaymember = value;
            }
        }

        #endregion

        #region IValueMember 成员
        private string _valuemember = string.Empty;
        public virtual string ValueMember
        {
            get
            {
                return _valuemember;
            }
            set
            {
                _valuemember = value;
            }
        }

        #endregion

        #region IFunctionCells 成员

        public virtual List<ICell> FunctionCells
        {
            get { return this.BeginCell.FunctionCells; }
        }

        #endregion

        #region IParentFunctionCells 成员

        public virtual List<ICell> ParentFunctionCells
        {
            get
            {
                return this.BeginCell.ParentFunctionCells;
            }
            set
            {
                this.BeginCell.ParentFunctionCells = value;
            }
        }

        public virtual void AddParentFunctionCell(ICell cell)
        {
            this.BeginCell.AddParentFunctionCell(cell);
        }

        #endregion

        #region IParentFunctionCells 成员


        public virtual void ExecuteParentExpresses()
        {
            this.BeginCell.ExecuteParentExpresses();
        }

        #endregion

        #region IMouseOverImage 成员

        public virtual Bitmap MouseOverImage
        {
            get
            {
                return this.BeginCell.MouseOverImage;
            }
            set
            {
                this.BeginCell.MouseOverImage = value;
            }
        }

        #endregion

        #region IMouseDownImage 成员

        public virtual Bitmap MouseDownImage
        {
            get
            {
                return this.BeginCell.MouseDownImage;
            }
            set
            {
                this.BeginCell.MouseDownImage = value;
            }
        }

        public virtual Bitmap MouseUpImage
        {
            get
            {
                return this.BeginCell.MouseUpImage;
            }
            set
            {
                this.BeginCell.MouseUpImage = value;
            }
        }
        #endregion

        #region IDisableImage 成员

        public virtual Bitmap DisableImage
        {
            get
            {
                return this.BeginCell.DisableImage;
            }
            set
            {
                this.BeginCell.DisableImage = value;
            }
        }

        #endregion

        #region IReadOnlyImage 成员

        public virtual Bitmap ReadOnlyImage
        {
            get
            {
                return this.BeginCell.ReadOnlyImage;
            }
            set
            {
                this.BeginCell.ReadOnlyImage = value;
            }
        }

        #endregion

        #region IFocusImage 成员

        public virtual Bitmap FocusImage
        {
            get
            {
                return this.BeginCell.FocusImage;
            }
            set
            {
                this.BeginCell.FocusImage = value;
            }
        }

        #endregion

        #region IMouseOverBackColor 成员

        public virtual Color MouseOverBackColor
        {
            get
            {
                return this.BeginCell.MouseOverBackColor;
            }
            set
            {
                this.BeginCell.MouseOverBackColor = value;
            }
        }

        #endregion

        #region IMouseDownBackColor 成员

        public virtual Color MouseDownBackColor
        {
            get
            {
                return this.BeginCell.MouseDownBackColor;
            }
            set
            {
                this.BeginCell.MouseDownBackColor = value;
            }
        }


        public virtual Color MouseUpBackColor
        {
            get
            {
                return this.BeginCell.MouseUpBackColor;
            }
            set
            {
                this.BeginCell.MouseUpBackColor = value;
            }
        }

        #endregion

        #region IFocusBackColor 成员

        public virtual Color FocusBackColor
        {
            get
            {
                return this.BeginCell.FocusBackColor;
            }
            set
            {
                this.BeginCell.FocusBackColor = value;
            }
        }

        #endregion

        #region IMouseOverForeColor 成员

        public virtual Color MouseOverForeColor
        {
            get
            {
                return this.BeginCell.MouseOverForeColor;
            }
            set
            {
                this.BeginCell.MouseOverForeColor = value;
            }
        }

        #endregion

        #region IMouseDownForeColor 成员

        public virtual Color MouseDownForeColor
        {
            get
            {
                return this.BeginCell.MouseDownForeColor;
            }
            set
            {
                this.BeginCell.MouseDownForeColor = value;
            }
        }

        public virtual Color MouseUpForeColor
        {
            get
            {
                return this.BeginCell.MouseUpForeColor;
            }
            set
            {
                this.BeginCell.MouseUpForeColor = value;
            }
        }
        #endregion

        #region IFocusForeColor 成员

        public virtual Color FocusForeColor
        {
            get
            {
                return this.BeginCell.FocusForeColor;
            }
            set
            {
                this.BeginCell.FocusForeColor = value;
            }
        }

        #endregion
 

        #region IContensWidth 成员
        private int _ContensWidth = 0;
        public virtual int ContensWidth
        {
            get
            {
                return _ContensWidth;
            }
            set
            {
                _ContensWidth = value;
            }
        }

        #endregion

        #region IContensHeigth 成员
        private int _ContensHeigth = 0;
        public virtual int ContensHeigth
        {
            get
            {
                return _ContensHeigth;
            }
            set
            {
                _ContensHeigth = value;
            }
        }

        #endregion

        #region IFreshContens 成员

        public virtual void FreshContens()
        { 
            Graphics g = this.Grid.GetGraphics();
            if (g == null)
                return;
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            if (this.DirectionVertical)
            {
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionVertical;
            }
            Size Size = Feng.Utils.ConvertHelper.ToSize(g.MeasureString(this.Text, this.Font, Point.Empty, sf));
            _ContensWidth = Size.Width;
            _ContensHeigth = Size.Height;

        }

        #endregion


        #region IPrintFooter 成员

        public virtual void PrintFooter(PrintArgs e)
        { 
        }

        #endregion


        #region ITableCellPrinted 成员

        public virtual bool IsTableCellPrinted
        {
            get
            {
                if (this.BeginCell == null)
                    return false;
                return this.BeginCell.IsTableCellPrinted;
            }
            set
            {
                this.BeginCell.IsTableCellPrinted = value;
            }
        }

        #endregion


        #region IPrintHeader 成员

        public virtual void PrintHeader(PrintArgs e)
        { 
        }

        #endregion

        #region ISave 成员

        public virtual void Save(Feng.Excel.IO.BinaryWriter stream)
        {
            stream.Write(this.Data);
        }

        #endregion

        #region IRead 成员

        public virtual void Read(DataExcel grid, Feng.Excel.IO.BinaryReader stream, out int count)
        {

            count = 1;
            try
            { 


            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
 
        }
        private int frowindex = 0;
        private int fcolumnindex = 0;
        private int erowindex = 0;
        private int ecolumnindex = 0;
        private int celleditid = 0;
        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                stream.ReadCache();
                cellendcout = stream.ReadIndex(3, cellendcout);
                cellinitcout = stream.ReadIndex(4, cellinitcout);
                _ContensHeigth = stream.ReadIndex(5, _ContensHeigth);
                _ContensWidth = stream.ReadIndex(6, _ContensWidth);
                frowindex = stream.ReadIndex(7, 0);
                fcolumnindex = stream.ReadIndex(8, 0);
                erowindex = stream.ReadIndex(9, 0);
                ecolumnindex = stream.ReadIndex(10, 0);
                _freshversion = stream.ReadIndex(12, _freshversion);
                _height = stream.ReadIndex(13, _height);
                //lastdrawinde = stream.ReadIndex(16, lastdrawinde);
                _left = stream.ReadIndex(17, _left);
                _selected = stream.ReadIndex(22, _selected);
                _top = stream.ReadIndex(24, _top);
                _width = stream.ReadIndex(25, _width);
                celleditid = stream.ReadIndex(26, 0);
            }
            this.BeginCell = this.Grid[frowindex, fcolumnindex];
            this.EndCell = this.Grid[erowindex, ecolumnindex];
        }
        public virtual void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        #endregion

        #region IDataStruct 成员
        public static readonly MergeCell Empty = new MergeCell();
        [Browsable(false)]
        public virtual DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = string.Empty,
                    Name = string.Empty,
                };
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (Feng.Excel.IO.BinaryWriter bw = this.Grid.ClassFactory.CreateBinaryWriter(ms))
                    { 
                        bw.Write(3, cellendcout, Empty.cellendcout);
                        bw.Write(4, cellinitcout, Empty.cellinitcout);
                        bw.Write(5, _ContensHeigth, Empty._ContensHeigth);
                        bw.Write(6, _ContensWidth, Empty._ContensWidth);
                        bw.Write(7, _firstcell.Row.Index, 0);
                        bw.Write(8, _firstcell.Column.Index, 0);
                        bw.Write(9, _endcell.Row.Index, 0);
                        bw.Write(10, _endcell.Column.Index, 0);
                        bw.Write(12, _freshversion, Empty._freshversion);
                        bw.Write(13, _height, Empty._height);
                        bw.Write(14, false, false);
                        bw.Write(15, IsTableCellPrinted, Empty.IsTableCellPrinted);
                        //bw.Write(16, lastdrawinde, Empty.lastdrawinde);
                        bw.Write(17, _left, Empty._left); 
                        bw.Write(22, _selected, Empty._selected); 
                        bw.Write(24, _top, Empty._top);
                        bw.Write(25, _width, Empty._width);
                        int celleditid = 0;
                        if (this.OwnEditControl != null)
                        {
                            celleditid = this.OwnEditControl.AddressID;
                        }
                        bw.Write(26, celleditid, 0);
 
                    }
                    data.Data = ms.ToArray();
                }

                return data;
            }
        }

        #endregion

        #region IVersion 成员
        [Browsable(false)]
        public virtual string Version
        {
            get
            {
                return Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
            }
        }

        #endregion

        #region IAssembly 成员

        public virtual string DllName
        {
            get { return string.Empty; }
        }

        #endregion

        #region IDownLoadUrl 成员
        [Browsable(false)]
        public virtual string DownLoadUrl
        {
            get { return string.Empty; }
        }

        #endregion

        #region IPrintText 成员
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintText
        {
            get
            {
                return this.BeginCell.IsPrintText;
            }
            set
            {
                this.BeginCell.IsPrintText = value;
            }
        }

        #endregion

        #region IPrintBackImage 成员
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintBackImage
        {
            get
            {
                return this.BeginCell.IsPrintBackImage;
            }
            set
            {
                this.BeginCell.IsPrintBackImage = value;
            }
        }

        #endregion

        #region IPrintBorder 成员
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintBorder
        {
            get
            {
                return this.BeginCell.IsPrintBorder;
            }
            set
            {
                this.BeginCell.IsPrintBorder = value;
            }
        }

        #endregion

        #region IPrintBackColor 成员
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintBackColor
        {
            get
            {
                return this.BeginCell.IsPrintBackColor;
            }
            set
            {
                this.BeginCell.IsPrintBackColor = value;
            }
        }

        #endregion

        #region IKeyValue 成员
        private Dictionary<object, object> _keyvalue = null;
        public virtual Dictionary<object, object> KeyValue
        {
            get
            {
                if (_keyvalue == null)
                {
                    _keyvalue = new Dictionary<object, object>();
                }
                return _keyvalue;
            }
            set
            {
                _keyvalue = value;
            }
        }

        #endregion

        #region ICurrentIMergeCell 成员

        IMergeCell IOwnMergeCell.OwnMergeCell
        {
            get;
            set;
        }

        #endregion

        #region IOwnBackCell 成员

        public virtual IBackCell OwnBackCell
        {
            get { return this.BeginCell.OwnBackCell; }
            set { this.BeginCell.OwnBackCell = value; }
        }

        #endregion

        #region IShowSelectBorder 成员
        public virtual bool ShowFocusedSelectBorder
        {
            get
            {
                return this.BeginCell.ShowFocusedSelectBorder;
            }
            set
            {
                this.BeginCell.ShowFocusedSelectBorder = value;
            }
        }

        #endregion

        #region IEditMode 成员

        public virtual EditMode EditMode
        {
            get
            {
                return this.BeginCell.EditMode;
            }
            set
            {
                this.BeginCell.EditMode = value;
            }
        }

        #endregion
 

        #region ISetText 成员

        public virtual void SetText(string text)
        {
            this.BeginCell.Text = (text);
        }

        #endregion

        #region IMouseOverImage 成员


        public virtual ImageLayout MouseOverImageSizeMode
        {
            get
            {
                return this.BeginCell.MouseOverImageSizeMode;
            }
            set
            {
                this.BeginCell.MouseOverImageSizeMode = value;
            }
        }

        #endregion

        #region IMouseDownImage 成员


        public virtual ImageLayout MouseDownImageSizeMode
        {
            get
            {
                return this.BeginCell.MouseDownImageSizeMode;
            }
            set
            {
                this.BeginCell.MouseDownImageSizeMode = value;
            }
        }

        public virtual ImageLayout MouseUpImageSizeMode
        {
            get
            {
                return this.BeginCell.MouseUpImageSizeMode;
            }
            set
            {
                this.BeginCell.MouseUpImageSizeMode = value;
            }
        }
        #endregion

        #region IDisableImage 成员


        public virtual ImageLayout DisableImageSizeMode
        {
            get
            {
                return this.BeginCell.DisableImageSizeMode;
            }
            set
            {
                this.BeginCell.DisableImageSizeMode = value;
            }
        }

        #endregion

        #region IReadOnlyImage 成员


        public virtual ImageLayout ReadOnlyImageSizeMode
        {
            get
            {
                return this.BeginCell.ReadOnlyImageSizeMode;
            }
            set
            {
                this.BeginCell.ReadOnlyImageSizeMode = value;
            }
        }

        #endregion

        #region IFocusImage 成员


        public virtual ImageLayout FocusImageSizeMode
        {
            get
            {
                return this.BeginCell.FocusImageSizeMode;
            }
            set
            {
                this.BeginCell.FocusImageSizeMode = value;
            }
        }

        #endregion


        #region IBackImage 成员


        public ImageLayout BackImgeSizeMode
        {
            get
            {
                return this.BeginCell.BackImgeSizeMode;
            }
            set
            {
                this.BeginCell.BackImgeSizeMode = value;
            }
        }

        #endregion

        #region IFunctionBorder 成员

        public virtual bool FunctionBorder
        {
            get
            {
                return this.BeginCell.FunctionBorder;
            }
            set
            {
                this.BeginCell.FunctionBorder = value;
            }
        }

        #endregion

        #region IMaxRowIndex 成员

        public int MaxRowIndex
        {
            get { return System.Math.Max(this.BeginCell.Row.Index, this.EndCell.Row.Index); }
        }

        #endregion

        #region IMaxColumnIndex 成员

        public int MaxColumnIndex
        {
            get { return System.Math.Max(this.BeginCell.Column.Index, this.EndCell.Column.Index); }
        }

        #endregion

        #region IPermissions 成员

        public virtual string Permissions
        {
            get
            {
                return this.BeginCell.Permissions;
            }
            set
            {
                this.BeginCell.Permissions = value;
            }
        }

        public virtual Purview Purview
        {
            get
            {
                return this.BeginCell.Purview;
            }
            set
            {
                this.BeginCell.Purview = value;
            }
        }

        #endregion

        #region ITableStop 成员

        public virtual bool TabStop
        {
            get
            {
                return this.BeginCell.TabStop;
            }
            set
            {
                this.BeginCell.TabStop = value;
            }
        }

        #endregion

        #region ITableIndex 成员

        public int TabIndex
        {
            get
            {
                return this.BeginCell.TabIndex;
            }
            set
            {
                this.BeginCell.TabIndex = value;
            }
        }

        #endregion

        #region IHotKeyEnable 成员

        public virtual bool HotKeyEnable
        {
            get
            {
                return this.BeginCell.HotKeyEnable;
            }
            set
            {
                this.BeginCell.HotKeyEnable = value;
            }
        }

        #endregion

        #region IHotKeyData 成员

        public virtual Keys HotKeyData
        {
            get
            {
                return this.BeginCell.HotKeyData;
            }
            set
            {
                this.BeginCell.HotKeyData = value;
            }
        }

        #endregion


        #region ILocation 成员

        public Point Location
        {
            get { return this.BeginCell.Location; }
        }

        #endregion

        #region IBingValue 成员

        public object BingValue
        {
            get
            {
                return this.BeginCell.BingValue;
            }
            set
            {
                this.BeginCell.BingValue = value;
            }
        }

        #endregion

        #region ICommandID 成员

        public virtual string ID
        {
            get
            {
                return this.BeginCell.ID;
            }
            set
            {
                this.BeginCell.ID = value;
            }
        }

        #endregion

        #region ITag 成员


        public virtual object Tag
        {
            get
            {
                return this.BeginCell.Tag;
            }
            set
            {
                this.BeginCell.Tag = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.BeginCell.ToolTip;
            }
            set
            {
                this.BeginCell.ToolTip = value;
            }
        }
        #endregion


        public virtual string FieldName
        {
            get
            {
                return this.BeginCell.FieldName;
            }
            set
            {
                this.BeginCell.FieldName = value;
            }
        }

        #region PropertyEvent 成员

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseUp
        {
            get
            {
                return this.BeginCell.PropertyOnMouseUp;
            }
            set
            {
                this.BeginCell.PropertyOnMouseUp = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseMove
        {
            get
            {
                return this.BeginCell.PropertyOnMouseMove;
            }
            set
            {
                this.BeginCell.PropertyOnMouseMove = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseLeave
        {
            get
            {
                return this.BeginCell.PropertyOnMouseLeave;
            }
            set
            {
                this.BeginCell.PropertyOnMouseLeave = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseHover
        {
            get
            {
                return this.BeginCell.PropertyOnMouseHover;
            }
            set
            {
                this.BeginCell.PropertyOnMouseHover = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseEnter
        {
            get
            {
                return this.BeginCell.PropertyOnMouseEnter;
            }
            set
            {
                this.BeginCell.PropertyOnMouseEnter = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseDown
        {
            get
            {
                return this.BeginCell.PropertyOnMouseDown;
            }
            set
            {
                this.BeginCell.PropertyOnMouseDown = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseDoubleClick
        {
            get
            {
                return this.BeginCell.PropertyOnMouseDoubleClick;
            }
            set
            {
                this.BeginCell.PropertyOnMouseDoubleClick = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseClick
        {
            get
            {
                return this.BeginCell.PropertyOnMouseClick;
            }
            set
            {
                this.BeginCell.PropertyOnMouseClick = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseCaptureChanged
        {
            get
            {
                return this.BeginCell.PropertyOnMouseCaptureChanged;
            }
            set
            {
                this.BeginCell.PropertyOnMouseCaptureChanged = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseWheel
        {
            get
            {
                return this.BeginCell.PropertyOnMouseWheel;
            }
            set
            {
                this.BeginCell.PropertyOnMouseWheel = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnClick
        {
            get
            {
                return this.BeginCell.PropertyOnClick;
            }
            set
            {
                this.BeginCell.PropertyOnClick = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnKeyDown
        {
            get
            {
                return this.BeginCell.PropertyOnKeyDown;
            }
            set
            {
                this.BeginCell.PropertyOnKeyDown = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnKeyPress
        {
            get
            {
                return this.BeginCell.PropertyOnKeyPress;
            }
            set
            {
                this.BeginCell.PropertyOnKeyPress = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnKeyUp
        {
            get
            {
                return this.BeginCell.PropertyOnKeyUp;
            }
            set
            {
                this.BeginCell.PropertyOnKeyUp = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnPreviewKeyDown
        {
            get
            {
                return this.BeginCell.PropertyOnPreviewKeyDown;
            }
            set
            {
                this.BeginCell.PropertyOnPreviewKeyDown = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnDoubleClick
        {
            get
            {
                return this.BeginCell.PropertyOnDoubleClick;
            }
            set
            {
                this.BeginCell.PropertyOnDoubleClick = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnCellInitEdit
        {
            get
            {
                return this.BeginCell.PropertyOnCellInitEdit;
            }
            set
            {
                this.BeginCell.PropertyOnCellInitEdit = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnCellEndEdit
        {
            get
            {
                return this.BeginCell.PropertyOnCellEndEdit;
            }
            set
            {
                this.BeginCell.PropertyOnCellEndEdit = value;
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
       [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnCellValueChanged
        {
            get
            {
                return this.BeginCell.PropertyOnCellValueChanged;
            }
            set
            {
                this.BeginCell.PropertyOnCellValueChanged = value;
            }
        }
        #endregion
 
        public virtual string DefaultValue
        {
            get
            {
                return this.BeginCell.DefaultValue;
            }
            set
            {
                this.BeginCell.DefaultValue = value;
            }
        }

        public virtual bool Visible
        {
            get
            {
                return this.BeginCell.Visible;
            }
            set
            {
                this.BeginCell.Visible = value;
            }
        }
 
 
        public virtual string Remark
        {
            get
            {
                return this.BeginCell.Remark;
            }
            set
            {
                this.BeginCell.Remark = value;
            }
        }

        public virtual string Extend
        {
            get
            {
                return this.BeginCell.Extend;
            }
            set
            {
                this.BeginCell.Extend = value;
            }
        }

        public string PropertyOnDrawBack
        {
            get
            {
                return this.BeginCell.PropertyOnDrawBack;
            }
            set
            {
                this.BeginCell.PropertyOnDrawBack = value;
            }
        }

        public string PropertyOnDrawCell
        {
            get
            {
                return this.BeginCell.PropertyOnDrawCell;
            }
            set
            {
                this.BeginCell.PropertyOnDrawCell = value;
            }
        }

        public virtual bool AllowCopy
        {
            get
            {
                return this.BeginCell.AllowCopy;
            }
            set
            {
                this.BeginCell.AllowCopy = value;
            }
        }

        public virtual string Url
        {
            get
            {
                return this.BeginCell.Url;
            }
            set
            {
                this.BeginCell.Url = value;
            }
        }

        public virtual int ExpressionIndex
        {
            get
            {
                return this.BeginCell.ExpressionIndex;
            }
            set
            {
                this.BeginCell.ExpressionIndex = value;
            }
        }

         
        [Category(CategorySetting.PropertyTable)]
        public virtual string TableName { get { return this.BeginCell.TableName; } set { this.BeginCell.TableName = value; } }

         
        [Category(CategorySetting.PropertyTable)]
        public string TableColumnName { get { return this.BeginCell.TableColumnName; } set { this.BeginCell.TableColumnName = value; } }
         
        [DefaultValue(-1)]
        [Category(CategorySetting.PropertyTable)]
        public int TableRowIndex { get { return this.BeginCell.TableRowIndex; } set { this.BeginCell.TableRowIndex = value; } }

        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(YesNoInhert.Inherit)]
        public virtual YesNoInhert AllowInputExpress
        {
            get { return this.BeginCell.AllowInputExpress; }
            set { this.BeginCell.AllowInputExpress = value; }
        }
         
    }
}
