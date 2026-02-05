using Feng.Data;
using Feng.Drawing;
using Feng.Enums;
using Feng.Excel.Actions;
using Feng.Excel.App;
using Feng.Excel.Args;
using Feng.Excel.Designer;
using Feng.Excel.Edits;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Excel.Script;
using Feng.Excel.Styles;
using Feng.Forms.Base;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Views;
using Feng.Print;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace Feng.Excel.Base
{
    [Serializable]
    [DefaultProperty("Value")]
    public partial class Cell : ICell
    {

        #region 系统属性
#warning [CompilerGenerated]
        private bool _AutoMultiline = false;
        /// <summary>
        /// 是否自动绘制多行。True时绘制多行，False时不绘制多行。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AutoMultiline
        {
            get 
            {
                if (this._AutoMultiline == EmptyCell._AutoMultiline)
                {
                    if (headercell != null)
                    {
                        return headercell.AutoMultiline;
                    }
                }
                return this._AutoMultiline; 
            }

            set { this._AutoMultiline = value; }
        }

        #endregion

        #region 构造函数

        private Cell()
        {

        }

        /// <summary>
        /// 单元格有：行，列属性对应相应的行与列。
        /// 有文本(text)与值(value)两个属性分别对应的是文本与相应的值。
        /// 
        /// </summary>
        /// <param name="grid"></param>
        public Cell(DataExcel grid)
        {
            this._grid = grid;
        }

        public Cell(IRow row, IColumn col)
            : this(row.Grid, row.Index, col.Index)
        {

        }

        public Cell(DataExcel grid, int rowindex, int columnindex)
        {

#if DEBUG

            if (rowindex == 0)
            {

            }
#endif
            this.Grid = grid;
            IRow row = grid.Rows[rowindex];
            if (row == null)
            {
                IRow r = this.Grid.ClassFactory.CreateDefaultRow(this.Grid, rowindex);
                this._row = r;
                this.Grid.Rows.Add(r);
            }
            else
            {
                this._row = row;
            }
            IColumn col = this.Grid.Columns[columnindex];
            if (col == null)
            {
                col = this.Grid.ClassFactory.CreateDefaultColumn(this.Grid, columnindex);
                this.Grid.Columns.Add(col);
            }
            this.Column = col;
            this.Row.Cells.Add(this);
            this.init();

        }

        private void init()
        {
            if (this.Row == null)
                return;
            if (this.Row.Index < 1 && this.Column.Index > 0)
            {
                if (this.Row.Index == 0)
                {
                    this.OwnEditControl = CellColumnHeader.Instance(this.Grid);
                }
                else if (this.Row.Index > -100)
                {
                    this.OwnEditControl = CellColumnHeader.Instance(this.Grid);
                } 
            }
            else if (this.Column.Index < 1)
            {
                this.OwnEditControl = CellRowHeader.Instance(this.Grid);
            }
        }

        #endregion

        #region 界面绘制

        #region IDraw 成员

        public virtual bool OnDrawBack(object sender, Feng.Drawing.GraphicsObject g)
        {
            CellPaintingEventArgs e = new CellPaintingEventArgs(this, g);
            this.Grid.OnCellPainting(e);
            if (e.Cancel)
            {
                return false;
            }
            try
            {
                DrawRectBack(g, this.Rect, this.Text, false, false, null);
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
            return false;
        }

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
#if DEBUG2
            if (Feng.Script.FunctionContainer.DebugFunctionContainer.DebugSate)
            {
                if (this.Row.Index == 0)
                {
                    g.Graphics.DrawString(string.Format("{0},{1}", this.Row.Name, this.Column.Name), this.Font, Brushes.Red, this.Rect);
                }

            }
#endif
#if DEBUG2
            if (this.Row.Index == 3 && (this.Column.Index < 9 && this.Column.Index > 1))
            {

            }
            //Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, Brushes.Blue, this.Rect);
#endif
            if (this.OwnMergeCell != null)
            {
                return false;
            }
            CellPaintingEventArgs e = new CellPaintingEventArgs(this, g);
            this.Grid.OnCellPainting(e);
            if (e.Cancel)
            {
                return false;
            }
            try
            {
                DrawRect(g, this.Rect, this.Text, false, false, null);
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Cell", "OnDraw", ex);
            }
#if DEBUG2
            if (this.Row.Index == 3 && (this.Column.Index < 9 && this.Column.Index > 1))
            {
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, Brushes.Blue, this.Rect);
                g.Graphics.DrawString(this.Text, this.Font, Brushes.Red, this.Rect);
            }
            //Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, Brushes.Blue, this.Rect);
#endif
            return false;
        }

        public virtual void DrawBorder(Feng.Drawing.GraphicsObject g)
        {
            if (this.OwnMergeCell != null)
            {

                this.OwnMergeCell.DrawBorder(g);
            }
            else
            {
                DrawBorder(g, this.Rect, false);
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
            try
            {
                BeforeDrawCellBackArgs e = new BeforeDrawCellBackArgs(g, this);
                this.Grid.OnBeforeDrawCellBack(this, e);
                if (e.Cancel)
                {
                    goto LabelEnd;
                }

                if (print)
                {
                    DrawRectBackPrint(g, bounds, value, printbindingvalue, printArgs);
                }
                else
                {
                    DrawRectBackForm(g, bounds, value, printbindingvalue, printArgs);
                }

                DrawCellBackArgs DrawCellArgs = new DrawCellBackArgs(g, this);
                this.Grid.OnDrawCellBack(DrawCellArgs);

            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
            finally
            {
                g.Graphics.Restore(gs);
            }
            LabelEnd:
            return;
        }
        public void DrawRectBackForm(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool printbindingvalue, PrintArgs printArgs)
        {
            if (this.OwnMergeCell != null)
            {
                //this.OwnMergeCell.OnDrawBack(this, g);
                return;
            }
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
        public void DrawRectBackPrint(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool printbindingvalue, PrintArgs printArgs)
        {
            if (this.OwnBackCell != null)
            {
                this.OwnBackCell.PrintBack(printArgs);
            }
            if (this.OwnMergeCell != null)
            {
                this.OwnMergeCell.PrintBack(printArgs);
            }

            if (this.OwnEditControl != null)
            {
                if (!this.OwnEditControl.PrintCellBack(this, printArgs))
                {
                    if (this.IsPrintBackColor)
                    {
                        DrawBackColor(g, bounds);
                    }
                    if (this.IsPrintBackImage)
                    {
                        DrawBackImage(g, bounds);
                    }
                }
            }
            else
            {
                DrawBackColor(g, bounds);
                DrawBackImage(g, bounds);
            }
        }

        public void DrawRect(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool print, bool printbindingvalue, PrintArgs printArgs)
        {
            if (!this.Visible)
            {
                if (!(this.Grid.InDesign))
                {
                    return;
                }
            }

            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            try
            {
                BeforeDrawCellArgs e = new BeforeDrawCellArgs(g, this);
                this.Grid.OnBeforeDrawCell(this, e);
                if (e.Cancel)
                {
                    return;
                }
#if DEBUG2
                if (this.Row.Index == 13 && this.Column.Index == 4)
                {

                }
#endif
                if (print)
                {
                    DrawRectPrint(g, bounds, value, printbindingvalue, printArgs);
                }
                else
                {
                    DrawRectForm(g, bounds, value, printbindingvalue, printArgs);
                }
                DrawCellArgs DrawCellArgs = new DrawCellArgs(g, this);
                this.Grid.OnDrawCell(DrawCellArgs);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
            finally
            {
                g.Graphics.Restore(gs);
            }
        }
        public void DrawRectForm(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool printbindingvalue, PrintArgs printArgs)
        {
            if (this.OwnBackCell != null)
            {
                if (!g.Items.Contains(this.OwnBackCell))
                {
                    g.Items.Add(this.OwnBackCell);
                    this.OwnBackCell.OnDraw(this, g);
                }
            }

            if (this.OwnMergeCell != null)
            {
                ////if (!g.Items.Contains(this.OwnMergeCell))
                ////{
                ////    g.Items.Add(this.OwnMergeCell);
                ////    this.OwnMergeCell.OnDraw(this, g);
                ////}
                return;
            }
            else
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
                DrawCellArgs DrawCellArgs = new DrawCellArgs(g, this);
                this.Grid.OnDrawCell(DrawCellArgs);
            }
        }
        public void DrawRectPrint(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool printbindingvalue, PrintArgs printArgs)
        {

            if (this.OwnBackCell != null)
            {
                this.OwnBackCell.Print(printArgs);
            }

            if (this.OwnMergeCell != null)
            {
                this.OwnMergeCell.PrintValue(printArgs, value);
            }
            else
            {
                if (this.OwnEditControl != null)
                {
                    if (printbindingvalue)
                    {
                        if (!this.OwnEditControl.PrintValue(this, printArgs, bounds, this.Value))
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
        }

        private bool _FunctionBorder = false;

        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public virtual bool FunctionBorder
        {
            get { return _FunctionBorder; }
            set { _FunctionBorder = value; }
        }

        public void DrawFunctionBorder(Feng.Drawing.GraphicsObject g, int index)
        {
            if (!this.Grid.VisibleRows.Contains(this.Row))
            {
                return;
            }
            if (index < 0)
            {
                if (this.InEdit || FunctionBorder)
                {
                    if (this.FunctionCells != null)
                    {
                        for (int i = 0; i < this.FunctionCells.Count; i++)
                        {
                            ICell cell = this.FunctionCells[i];
                            cell.DrawFunctionBorder(g, i);
                        }
                    }
                }
            }
            else
            {
                Rectangle rect = this.Rect;
                if (index < this.Grid.FunctionCellDeafultColors.Count)
                {
                    g.Graphics.DrawRectangle(this.Grid.FunctionCellDeafultColors[index], rect.Left, rect.Top, rect.Width, rect.Height);

                }
                else
                {
                    Random rand = new Random(200);
                    int r = rand.Next(0, 255);
                    int gg = rand.Next(0, 255);
                    int b = rand.Next(0, 255);
                    Color c = Color.FromArgb(r, gg, b);
                    using (Pen pen = new Pen(c))
                    {
                        g.Graphics.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
                    }
                }
            }

        }

        private bool DrawBackImage(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            Image backimage = null;
            Rectangle rect = bounds;
            if (this.Grid.FocusedCell == this)
            {
                backimage = this._FocusImage;
                rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(FocusImageSizeMode, backimage, bounds);
            }

            if (this.ReadOnly)
            {
                backimage = this._ReadOnlyImage;
                rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(ReadOnlyImageSizeMode, backimage, bounds);
            }
            if (this.Rect.Contains(g.ClientPoint))
            {
                backimage = this._MouseOverImage;
                rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(MouseOverImageSizeMode, backimage, bounds);
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell == this)
            {
                backimage = this._MouseDownImage;
                rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(MouseDownImageSizeMode, backimage, bounds);
            }
            if (backimage == null)
            {
                backimage = this.BackImage;
            }
            if (backimage != null)
            {
                rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(BackImgeSizeMode, backimage, bounds);
                g.Graphics.DrawImage(backimage, rect);
                return true;
            }
            return false;
        }

        private bool DrawBackColor(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            Color backcolor = this.BackColor;
            if (this.Grid.FocusedCell == this)
            {
                if (this.FocusBackColor != Color.Empty)
                {
                    backcolor = this.FocusBackColor;
                }
            }
            if (this.Rect.Contains(g.ClientPoint))
            {
                if (this.MouseOverBackColor != Color.Empty)
                {
                    backcolor = this.MouseOverBackColor;
                }
            }
            if (this.Grid.FocusedCell == this)
            {
                if (this.FocusBackColor != Color.Empty)
                {
                    backcolor = this.FocusBackColor;
                }
            }
            if (this.Grid.FocusedCell == this)
            {
                if (g.MouseButtons == MouseButtons.Left)
                {
                    if (this.MouseDownBackColor != Color.Empty)
                    {
                        backcolor = this.MouseDownBackColor;
                    }
                }
            }
            if (this.Selected)
            {
                if (this.SelectBackColor != Color.Empty)
                {
                    backcolor = this.SelectBackColor;
                }
            }

            if (backcolor != Color.Empty)
            {
                SolidBrush sb = SolidBrushCache.GetSolidBrush(backcolor);
                Rectangle rect = bounds;
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, sb, rect);
                return true;
            }
            return false;
        }

        private void DrawCell(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            //if (this.InEdit)
            //{
            //    return;
            //}
            string text = Feng.Utils.ConvertHelper.ToString(value);
            //text = string.Format("Row={0};Column={1}", this.Row.Index, this.Column.Index);
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, this.DirectionVertical);

            bounds.Offset(1, 1);
            bounds.Inflate(-1, -1);
#if DEBUG
            if (text == "单击获取")
            {

            }
#endif
            Rectangle rect = bounds;

            Color forecolor = this.ForeColor;
            if (this.Grid.FocusedCell == this)
            {
                if (this.FocusForeColor != Color.Empty)
                {
                    forecolor = this.FocusForeColor;
                }
            }

            if (this.Rect.Contains(g.ClientPoint))
            {
                if (this.MouseOverForeColor != Color.Empty)
                {
                    forecolor = this.MouseOverForeColor;
                }
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell == this)
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
            SolidBrush sb = SolidBrushCache.GetSolidBrush(forecolor);

            if (this.AutoMultiline)
            {
                Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Font, sb, rect, sf);
            }
            else
            {
                sf.FormatFlags = StringFormatFlags.NoWrap;
                Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Font, sb, rect, sf);
            }
        }

        public virtual void DrawBack(Feng.Drawing.GraphicsObject g)
        {

        }

        public virtual void DrawBorder(Feng.Drawing.GraphicsObject g, Rectangle bounds, bool print)
        {
            if (BorderStyle != null)
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
                DrawCellBorderArgs DrawCellBorderArgs = new DrawCellBorderArgs(g, this);
                this.Grid.OnDrawCellBorder(DrawCellBorderArgs);
                if (!DrawCellBorderArgs.Handled)
                {
                    DrawLine(g, bounds, print);
                }
            }

        }

        public void DrawGridLine(Feng.Drawing.GraphicsObject g)
        {
            if (this.OwnMergeCell != null)
            {
                this.OwnMergeCell.DrawGridLine(g);
                return;
            }
            if (this.Grid.ShowGridColumnLine)
            {
                DrawGridRightLine(g);
            }
            if (this.Grid.ShowGridRowLine)
            {
                DrawGridBottomLine(g);
            }
        }

        private void DrawLine(Feng.Drawing.GraphicsObject g, Rectangle bounds, bool print)
        {
            //if (BorderStyle.LeftLineStyle.Visible)
            //{
            //    Pen pen = BorderStyle.LeftLineStyle.GetPen();

            //    g.Graphics.DrawLine(pen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
            //}

            //if (BorderStyle.TopLineStyle.Visible)
            //{
            //    Pen pen = BorderStyle.TopLineStyle.GetPen();
            //    g.Graphics.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Top);
            //}

            //if (BorderStyle.RightLineStyle.Visible)
            //{
            //    Pen pen = BorderStyle.RightLineStyle.GetPen();
            //    g.Graphics.DrawLine(pen, bounds.Right, bounds.Top, bounds.Right, bounds.Bottom);
            //}


            //if (BorderStyle.BottomLineStyle.Visible)
            //{
            //    Pen pen = BorderStyle.BottomLineStyle.GetPen();
            //    g.Graphics.DrawLine(pen, bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom);
            //}


            //if (BorderStyle.LeftTopToRightBottomLineStyle.Visible)
            //{
            //    Pen pen = BorderStyle.LeftTopToRightBottomLineStyle.GetPen();
            //    g.Graphics.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
            //}

            //if (BorderStyle.LeftBottomToRightTopLineStyle.Visible)
            //{
            //    Pen pen = BorderStyle.LeftBottomToRightTopLineStyle.GetPen();
            //    g.Graphics.DrawLine(pen, bounds.Left, bounds.Bottom, bounds.Right, bounds.Top);
            //}
        }

        public void PrintBorder(PrintArgs e)
        {
            if (this.OwnMergeCell != null)
            {
                this.OwnMergeCell.PrintBorder(e);
            }
            else
            {
                Rectangle rect = this.Rect;
                rect.Location = e.CurrentLocation;
                Feng.Drawing.GraphicsObject gob = e.Graphic;
                DrawBorder(gob, rect, true);
            }
        }

        public virtual bool OnDrawBack(Graphics g)
        {
            return false;
        }

        public virtual bool Print(PrintArgs e)
        {

            if (this.Column.ID != string.Empty)
            {
                if (this.Grid.DataSource != null)
                {
                    printcell2(e, this);
                    return false;
                }
            }
            if (this.OwnMergeCell != null)
            {
                return this.OwnMergeCell.Print(e);
            }
            else
            {
                Rectangle rect = this.Rect;
                rect.Location = e.CurrentLocation;
                if (this.OwnEditControl != null)
                {
                    this.OwnEditControl.PrintCell(this, e, rect);
                }
                PrintCellArgs pe = new PrintCellArgs();
                pe.Cell = this;
                pe.CurrentPage = e.Index;
                pe.Rect = this.Rect;
                pe.TotalPage = e.Total;
                pe.Value = this.Value;
                this.Grid.OnPrintCell(pe);
                if (pe.Cancel)
                {
                    return false;
                }
                Feng.Drawing.GraphicsObject gob = e.Graphic;
                this.DrawRectBack(gob, rect, this.Text, true, false, e);
                this.DrawRect(gob, rect, this.Text, true, false, e);
            }
            return false;
        }
        public virtual bool PrintBack(PrintArgs e)
        {

            if (this.Column.ID != string.Empty)
            {
                if (this.Grid.DataSource != null)
                {
                    printcell2(e, this);
                    return false;
                }
            }
            PrintCellBackArgs pe = new PrintCellBackArgs();
            pe.Cell = this;
            pe.CurrentPage = e.Index; ;
            pe.Rect = this.Rect;
            pe.TotalPage = e.Total;
            this.Grid.OnPrintCellBack(pe);
            if (pe.Cancel)
            {
                return false;
            }
            Rectangle rect = this.Rect;
            rect.Location = e.CurrentLocation;
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            this.DrawRectBack(gob, rect, this.Text, true, false, e);
            return false;
        }
        /// <summary>
        /// 获取打印数值，进行打印。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cell"></param>
        public void printcell2(PrintArgs e, ICell cell)
        {
            if (cell.Column.ID == string.Empty)
            {
                return;
            }
            if (this.Grid.DataSource is DataSet)
            {
                DataTable ilist = (this.Grid.DataSource as DataSet).Tables[0];
                int i = cell.Row.Index - 1;
                if (i >= ilist.Rows.Count)
                {
                    return;
                }
                object value = ilist.Rows[i][cell.Column.ID];

                cell.IsTableCellPrinted = true;
                cell.PrintValue(e, value.ToString());
                cell.IsTableCellPrinted = false;
            }
            else if (this.Grid.DataSource is DataTable)
            {
                DataTable ilist = this.Grid.DataSource as DataTable;
                int i = cell.Row.Index - 1;
                if (i >= ilist.Rows.Count)
                {
                    return;
                }
                object value = ilist.Rows[i][cell.Column.ID];

                cell.IsTableCellPrinted = true;
                cell.PrintValue(e, value.ToString());
                cell.IsTableCellPrinted = false;
            }
            else if (this.Grid.DataSource is System.Collections.IList)
            {
                System.Collections.IList ilist = this.Grid.DataSource as System.Collections.IList;
                int i = cell.Row.Index - 1;
                if (i >= ilist.Count)
                {
                    return;
                }
                string fieldname = cell.Column.ID;

                if (i < ilist.Count)
                {
                    object value = ReflectionHelper.GetValue(ilist[i], fieldname);
                    if (value == null)
                    {
                        cell.IsTableCellPrinted = true;
                        cell.Value = null;
                        cell.Text = (string.Empty);
                        cell.IsTableCellPrinted = false;
                        return;
                    }

                    cell.IsTableCellPrinted = true;
                    cell.PrintValue(e, value.ToString());
                    cell.IsTableCellPrinted = false;

                }

            }
        }
        #endregion

        #region IPrint 成员

        /// <summary>
        /// 在此单元格内，打印数据。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool PrintValue(PrintArgs e, object value)
        {
            PrintCellArgs pe = new PrintCellArgs();
            pe.Cell = this;
            pe.CurrentPage = e.Index; ;
            pe.Rect = this.Rect;
            pe.TotalPage = e.Total;
            pe.Value = value;
            this.Grid.OnPrintCell(pe);
            if (pe.Cancel)
            {
                return false;
            }

            Rectangle rect = this.Rect;
            rect.Location = e.CurrentLocation;
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawRect(gob, rect, value, true, true, e);

            return false;
        }


        #endregion

        #region IPrintText 成员

        private bool _printtext = true;
        /// <summary>
        /// 套打，指定是否打印文字内容。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintText
        {
            get
            {
                return this._printtext;
            }
            set
            {
                this._printtext = value;
            }
        }

        #endregion

        #region IPrintBackImage 成员
        private bool _PrintBackImage = true;

        /// <summary>
        /// 套打，指定是否打印图象内容。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintBackImage
        {
            get
            {
                return this._PrintBackImage;
            }
            set
            {
                this._PrintBackImage = value;
            }
        }

        #endregion

        #region IPrintBorder 成员
        private bool _PrintBorder = true;

        /// <summary>
        /// 套打，指定是否打印边框。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintBorder
        {
            get
            {
                return this._PrintBorder;
            }
            set
            {
                this._PrintBorder = value;
            }
        }

        #endregion

        #region IPrintBackColor 成员
        private bool _PrintBackColor = true;

        /// <summary>
        /// 套打，指定是否打印背景颜色。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual bool IsPrintBackColor
        {
            get
            {
                return this._PrintBackColor;
            }
            set
            {
                this._PrintBackColor = value;
            }
        }

        #endregion

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

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public DataExcel Grid
        {
            get { return this._grid; }

            set { this._grid = value; }
        }
        #endregion

        #region IBounds 成员

        [Browsable(false)]
        public int Top
        {
            get
            {
                //if (this.OwnMergeCell != null)
                //{
                //    return this.OwnMergeCell.Top;
                //}
                return this.Row.Top;
            }

            set
            {
                throw new CellSizeSetException(ExceptionString.SetCellTop);
            }
        }
        [Browsable(false)]
        public int Left
        {
            get
            {
                //if (this.OwnMergeCell != null)
                //{
                //    return this.OwnMergeCell.Left;
                //}
                //if (this.Column.Index < this.Grid.FirstDisplayedColumnIndex)
                //{
                //    if (this.Column.Index <= this.Grid.FrozenColumn)
                //    {
                //        return this.Column.Left;
                //    }
                //    else
                //    {
                //        return this.Row.Grid.LeftSideWidth;
                //    }
                //}
                //if (this.Column.Index >= this.Grid.FirstDisplayedColumnIndex + this.Grid.AllVisibleColumnCount)
                //{
                //    return this.Grid.Width;
                //}
                return this.Column.Left;
            }
            set
            {
                throw new CellSizeSetException(ExceptionString.SetCellLeft);
            }
        }
        [Browsable(false)]
        public int Bottom
        {
            get { return Top + this.Height; }
        }
        [Browsable(false)]
        public int Right
        {
            get { return Left + this.Width; }
        }
        [Browsable(false)]
        public int Width
        {
            get
            {
                int width = this.Column.Width;
                if (this.OwnMergeCell != null)
                {
                    width = this.OwnMergeCell.Width;
                }

                return width;
            }
            set { throw new CellSizeSetException(ExceptionString.SetCellWidth); }
        }
        [Browsable(false)]
        public int Height
        {
            get
            {
                if (this.OwnMergeCell != null)
                {
                    return this.OwnMergeCell.Height;
                }
                return this.Row.Height;
            }
            set { throw new CellSizeSetException(ExceptionString.SetCellHeight); }
        }

        [Browsable(true)]
        public Rectangle Rect
        {
            get
            {
                Rectangle rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                return rect;
            }
        }
        #endregion

        #region ISelected 成员
        private bool _selected = false;
        [Browsable(false)]
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

        private Color _selectcolor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color SelectBackColor
        {
            get
            {
                if (this._selectcolor == EmptyCell._selectcolor)
                {
                    if (headercell != null)
                    {
                        return headercell.SelectBackColor;
                    }
                }
                return _selectcolor;
            }
            set
            {
                _selectcolor = value;
            }
        }

        #endregion

        #region ISelectColor 成员

        private Color _SelectForceColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color SelectForceColor
        {
            get
            {
                if (this._SelectForceColor == EmptyCell._SelectForceColor)
                {
                    if (headercell != null)
                    {
                        return headercell.SelectForceColor;
                    }
                }
                return _SelectForceColor;
            }
            set
            {
                _SelectForceColor = value;
            }
        }

        #endregion

        #region ISelectBorderColor 成员

        private System.Drawing.Color _SelectBorderColor = Color.Gray;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public System.Drawing.Color SelectBorderColor
        {
            get
            {
                if (this._SelectBorderColor == EmptyCell._SelectBorderColor)
                {
                    if (headercell != null)
                    {
                        return headercell.SelectBorderColor;
                    }
                }
                return _SelectBorderColor;

            }
            set
            {
                _SelectBorderColor = value;
            }
        }

        #endregion

        #region IToString 成员
        public override string ToString()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                return string.Format("R={0},C={1},V={2},N={3}", this.Row.Index, this.Column.Index, this.Value, this.Name);
            }
#endif

            return this.Name;

        }
#if DEBUG
        public string AText
        {
            get
            {
                string str = string.Format("Name:{6}Value:{5} Row Index:{0};Column Index:{1};Text:{2} Point({3},{4})"
                    , this.Row.Index
                    , this.Column.Index
                    , this.Text, this.Rect.Location.X
                    , this.Rect.Location.Y
                    , this._value
                    , this.Name);
                return str;
            }

        }
#endif

        #endregion

        #region IControlColor 成员
        private Color _ForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color ForeColor
        {
            get
            {
                if (this._ForeColor == EmptyCell._ForeColor)
                {
                    if (headercell != null)
                    {
                        return headercell.ForeColor;
                    }
                }
                if (this._ForeColor == Color.Empty)
                {
                    return Color.Black;
                }
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }

        private Color _BackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color BackColor
        {
            get
            {
                if (this._BackColor == EmptyCell._BackColor)
                {
                    if (headercell != null)
                    {
                        return headercell.BackColor;
                    }
                }
                return _BackColor;
            }
            set
            {
                _BackColor = value;
                this.Grid.OnCellBackColorChanged(this, _BackColor);
            }
        }

        #endregion

        #region IText 成员
        private string _text = string.Empty;
        /// <summary>
        /// Text为单元格显示内容。但单元格的值未必就是此值。
        /// 例如：123.345678。在只显示两位小数时，就是123.46。
        /// </summary>
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                SetText(value);

            }
        }

        private string _text1 = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string Text1 { get { return _text1; } set { _text1 = value; } }
        private string _text2 = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string Text2 { get { return _text2; } set { _text2 = value; } }
        private string _text3 = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string Text3 { get { return _text3; } set { _text3 = value; } }

        /// <summary>
        /// 触发单元格，文本更改前的事件。
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public virtual bool OnBeforeTextChaned(string txt)
        {
            BeforeCellTextChangedArgs e = new BeforeCellTextChangedArgs();
            e.Cell = this;
            this.Grid.OnBeforeCellTextChanged(e);
            if (e.Cancel)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 触发单元格值更改事件。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool OnBeforeValueChanged(object value)
        {
            BeforeCellValueChangedArgs e = new BeforeCellValueChangedArgs()
            {
                Cell = this,
                NewValue = value
            };
            if (this.Grid == null)
                return true;
            this.Grid.OnBeforeCellValueChanged(e);
            if (e.Cancel)
            {
                return false;
            }
            return true;
        }

        public virtual void OnValueChanged(object value)
        {
            CellValueChangedArgs callvaluechangedargs = new CellValueChangedArgs(this);
            if (this.Grid.DataSource != null)
            {
                if (this.Row.Index > 0)
                {
                    if (this.Column.ID != string.Empty)
                    {
                        this.Grid.UpateBingValue(this, value);
                    }
                }
                else
                {
                    return;
                }
            }

            this._value = value;
            //this.Grid.CellChangeds.Add(this);
            this._text = FormatText(this._value);
            if (value != null)
            {
                if (this.Row != null)
                {
                    if (this.Row.Index > this.Grid.Rows.MaxHasValueIndex)
                    {
                        this.Grid.Rows.MaxHasValueIndex = this.Row.Index;
                    }
                }
                if (this.Row != null)
                {
                    if (this.Column.Index > this.Grid.MaxHasValueColumn)
                    {
                        this.Grid.MaxHasValueColumn = this.Column.Index;
                    }
                }
            }
            OnFormatDisplay();
            if (!string.IsNullOrWhiteSpace(this.PropertyOnCellValueChanged))
            {
                ActionArgs ae = new ActionArgs(this.PropertyOnCellValueChanged, this.Grid, this);
                this.Grid.ExecuteAction(ae, this.PropertyOnCellValueChanged);
            }

            this.Grid.BeginReFresh();

            if (this.AutoExecuteExpress)
            {
                this.ExecuteParentExpresses();
            }
            this.Grid.OnCellValueChanged(callvaluechangedargs);
            this.Grid.EndReFresh();
        }

        public string FormatText(object value)
        {
            try
            {
                if (value == null)
                    return string.Empty;
                string text = value.ToString();
                if (this._value != null)
                {
                    Type type = value.GetType();
                    if (Feng.Utils.ConvertHelper.IsNumberType(type))
                    {
                        if (!Feng.Utils.ConvertHelper.IsDecimal(type))
                        {
                            string format = "#0";
                            if (this.FormatString != string.Empty)
                            {
                                format = this.FormatString;
                            }
                            text = Feng.Utils.ConvertHelper.ToInt64(value).ToString(format);
                        }
                        else
                        {
                            string format = string.Empty;
                            if (this.FormatString != string.Empty)
                            {
                                format = this.FormatString;
                            }
                            text = Feng.Utils.ConvertHelper.ToDecimal(value).ToString(format);
                        }
                    }
                    else if (type == typeof(DateTime))
                    {
                        string format = "yyyy-MM-dd";
                        if (this.FormatString != string.Empty)
                        {
                            format = this.FormatString;
                        }
                        text = Feng.Utils.ConvertHelper.ToDateTime(value).ToString(format);
                    }
                    else if (type == typeof(bool))
                    {
                        if (this.OwnEditControl == null)
                        {
                            text = this.Value.ToString();
                        }
                    }
                    else
                    {
                        text = this.Value != null ? this.Value.ToString() : string.Empty;
                    }
                }
                return text;
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        public virtual void OnFormatDisplay()
        {
            this.Grid.OnFormatDisplay(this);
        }

        #endregion

        #region ICurrentRow 成员

        private IRow _row = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [Editor(typeof(EditNull), typeof(UITypeEditor))]
        [ReadOnly(true)]
        public IRow Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        #endregion

        #region ICurrentColumn 成员

        private IColumn _column = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [Editor(typeof(EditNull), typeof(UITypeEditor))]
        [ReadOnly(true)]
        public IColumn Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }

        #endregion

        #region ICurrentIMergeCell 成员

        private IMergeCell _OwnMergeCell = null;

        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public IMergeCell OwnMergeCell
        {
            get { return _OwnMergeCell; }
            set { _OwnMergeCell = value; }
        }

        #endregion

        #region IValue 成员
        private object _value = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (this._value != value)
                {
                    if (this.OnBeforeValueChanged(value))
                    {
                        this.OnValueChanged(value);
                    }
                }
            }
        }

        #endregion

        #region IExpressionText 成员
        private int _expressionindex = 100;
        public virtual int ExpressionIndex
        {
            get { return _expressionindex; }
            set { _expressionindex = value; }
        }
        private string _Expression = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string Expression
        {
            get
            {
                return this._Expression;
            }
            set
            {
                this._Expression = value;
                if (!string.IsNullOrEmpty(this._Expression))
                {
                    this.Grid.ExpressionCells.Add(this);
                }
                else
                {
                    this.Grid.ExpressionCells.Remove(this);
                } 
            }
        }

        #endregion

        #region IAutoExecuteExpress 成员
        private bool _AutoExecuteExpress = true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AutoExecuteExpress
        {
            get
            {
                return _AutoExecuteExpress;
            }
            set
            {
                _AutoExecuteExpress = value;
            }
        }

        #endregion

        #region IExecuteExpress 成员

        private bool _startexecuteexpress = false;
        public void ExecuteExpression()
        {
            if (_startexecuteexpress)
            {
                this._value = null;
                return;
            }
            else
            {
                _startexecuteexpress = true;

                try
                {
                    string express = this.Expression;
                    List<ICell> list = new List<ICell>();
                    bool blexec = false;
                    object res = Function.RunExpress(this.Grid, this, express, list, out blexec);
                    BeforeSetExpressCancelArgs e = new BeforeSetExpressCancelArgs(this);
                    e.Error = blexec;
                    this.Grid.OnBeforeSetExpress(e);
                    if (e.Cancel)
                    {
                        return;
                    }

                    ICell cellres = res as ICell;
                    object objvalue = null;
                    if (cellres != null)
                    {
                        this._value = cellres.Value;
                        this._text = FormatText(this._value);
                        objvalue = cellres.Value;
                    }
                    else
                    {
                        this._value =  res;
                        this._text = FormatText(this._value);
                    }
                }
                finally
                {
                    _startexecuteexpress = false;
                }
            }
        }
        #endregion

        #region IHorizontalAlignment 成员
        private StringAlignment _HorizontalAlignment = StringAlignment.Near;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public StringAlignment HorizontalAlignment
        {
            get
            {
                if (this._HorizontalAlignment == EmptyCell._HorizontalAlignment)
                {
                    if (headercell != null)
                    {
                        return headercell.HorizontalAlignment;
                    }
                }
                return _HorizontalAlignment;
            }
            set
            {
                _HorizontalAlignment = value;
            }
        }

        #endregion

        #region IVerticalAlignment 成员
        private StringAlignment _VerticalAlignment = StringAlignment.Center;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public StringAlignment VerticalAlignment
        {
            get
            {
                if (this._VerticalAlignment == EmptyCell._VerticalAlignment)
                {
                    if (headercell != null)
                    {
                        return headercell.VerticalAlignment;
                    }
                }
                return _VerticalAlignment;
            }
            set
            {
                _VerticalAlignment = value;
            }
        }

        #endregion

        #region ICellType 成员
        private CellType _celltype = CellType.Default;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public CellType CellType
        {
            get
            {
                return _celltype;
            }
            set
            {
                _celltype = value;
            }
        }

        #endregion

        #region IFont 成员
        private Font _font = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Font Font
        {
            get
            {
                if (this._font == null)
                {
                    if (this.Row.Font != null)
                    {
                        return this.Row.Font;
                    }
                    if (this.Column.Font != null)
                    {
                        return this.Column.Font;
                    }
                    if (this._font == EmptyCell._font)
                    {
                        if (headercell != null)
                        {
                            return headercell.Font;
                        }
                    }
                }
                return _font;
            }
            set
            {
                this.Grid.BeginReFresh();

                _font = value.Clone() as Font;
                this.Grid.EndReFresh();
            }
        }

        #endregion

        #region IBorderSetting 成员
        private CellBorderStyle _borderStyle = null;
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public CellBorderStyle BorderStyle
        {
            get
            {
                if (_borderStyle == null)
                {
                    _borderStyle = new CellBorderStyle();
                }
                return _borderStyle;
            }
            set
            {
                _borderStyle = value;
            }
        }

        #endregion

        #region IName 成员
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string Name
        {
            get
            {
                if (this.Column == null)
                    return string.Empty;
                return this.Column.Name + this.Row.Name;
            }
            set { }
        }

        #endregion

        #region IFormat 成员
        private FormatType _FormatType = FormatType.Null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public FormatType FormatType
        {
            get
            {
                if (this._FormatType == EmptyCell._FormatType)
                {
                    if (headercell != null)
                    {
                        return headercell.FormatType;
                    }
                }
                return _FormatType;
            }
            set
            {
                _FormatType = value;
            }
        }
        private string _FormatString = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string FormatString
        {
            get
            {
                if (this._FormatString == EmptyCell._FormatString)
                {
                    if (headercell != null)
                    {
                        return headercell.FormatString;
                    }
                }
                return _FormatString;
            }
            set
            {
                _FormatString = value;
            }
        }

        #endregion

        #region IUpdateVersion 成员
        private int _UpdateVersion = 0;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public int UpdateVersion
        {
            get
            {
                return _UpdateVersion;
            }
            set
            {
                _UpdateVersion = value;
            }
        }

        #endregion

        #region IOwnEditControl 成员
        private ICellEditControl _OwnEditControl = null;
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public ICellEditControl OwnEditControl
        {
            get
            {
                if (_OwnEditControl != null)
                {
                    return _OwnEditControl;
                }
                if (this.headercell != null)
                {
                    if (this.headercell.OwnEditControl != null)
                    {
                        return this.headercell.OwnEditControl;
                    }
                }
                if (this._OwnEditControl == null)
                {
                    return this.Grid.DefaultEdit;
                }
                return _OwnEditControl;
            }
            set
            {
                _OwnEditControl = value;
                if (!this.Grid.CellEdits.Contains(value))
                {
                    this.Grid.CellEdits.Add(value);
                }

            }
        }

        #endregion

        #region IInhertReadOnly 成员
        private bool _inhertreadonly = true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool InhertReadOnly
        {
            get
            {
                return _inhertreadonly;
            }
            set
            {
                _inhertreadonly = value;
            }
        }
        #endregion

        #region IReadOnly 成员
        private bool _readonly = false;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [CompilerGenerated]
        public virtual bool ReadOnly
        {
            get
            {
                if (_inhertreadonly)
                {
                    if (this.Column != null)
                    {
                        if (this.Column.ReadOnly)
                        {
                            return this.Column.ReadOnly;
                        }
                    }
                    if (this.Row != null)
                    {
                        if (this.Row.ReadOnly)
                        {
                            return this.Row.ReadOnly;
                        }
                    }
                    if (this._readonly == EmptyCell._readonly)
                    {
                        if (headercell != null)
                        {
                            return headercell.ReadOnly;
                        }
                    }
                }
                return _readonly;
            }
            set
            {
                _readonly = value;
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
            if (this.MouseUpBackColor != Color.Empty ||
    this.MouseUpForeColor != Color.Empty ||
    this.MouseUpImage != null)
            {
                this.Grid.ReFresh();
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
#if DEBUG
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
            {

            }
#endif

            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseMove(this, e, ve))
                {
                    return true;
                }
            }

            if (this.MouseOverBackColor != Color.Empty || this.MouseOverForeColor != Color.Empty || this.MouseOverImage != null)
            {
                Point viewloaction = this.Grid.PointControlToView(e.Location);
                if (this.Rect.Contains(viewloaction))
                {
                    this.Grid.Invalidate(Rectangle.Round(this.Rect));
                    if (!this.Grid.MousesMoveEvents.Contains(this))
                    {
                        this.Grid.MousesMoveEvents.Add(this);
                    }
                }
                else
                {
                    if (this.Grid.MousesMoveEvents.Contains(this))
                    {
                        this.Grid.MousesMoveEvents.Remove(this);
                        this.Grid.Invalidate(Rectangle.Round(this.Rect));
                    }
                }
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

        private Point lastmousedown = Point.Empty;
        private DateTime lastmousedownclick = DateTime.MinValue;
        public virtual bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (!this.InEdit)
            {
                if ((this.EditMode & EditMode.Focused) == EditMode.Click)
                {
                    this.InitEdit(this);
                }
            }
            if (!this.InEdit)
            {
                Point viewloaction = this.Grid.PointControlToView(e.Location);
                double TotalMilliseconds = (DateTime.Now - lastmousedownclick).TotalMilliseconds;
                if (TotalMilliseconds < 300 && TotalMilliseconds > 150)
                {
                    if (viewloaction.X - lastmousedown.X < 3 && viewloaction.Y - lastmousedown.Y < 3)
                    {
                        if ((this.EditMode & EditMode.DoubleClick) == EditMode.DoubleClick)
                        {
                            bool res = this.OnMouseDoubleClick(sender, e, ve);
                            if (res)
                            {
                                return true;
                            }
                            this.InitEdit(this);
                        }
                    }
                }
                lastmousedownclick = DateTime.Now;
                lastmousedown = viewloaction;
            }
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseDown(this, e, ve))
                {
                    return true;
                }
            }

            if (this.MouseDownBackColor != Color.Empty ||
                this.MouseDownForeColor != Color.Empty ||
                this.MouseDownImage != null)
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
                this.OwnEditControl.Cell = this;
                if (this.OwnEditControl.OnMouseDoubleClick(this, e, ve))
                {
                    return true;
                }
            }
            bool res = false;
            if ((this.EditMode & EditMode.Default) == EditMode.Default)
            {
                this.InitEdit(this);
                res = true;
            }
            if ((this.EditMode & EditMode.DoubleClick) == EditMode.DoubleClick)
            {
                this.InitEdit(this);
                res = true;
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
            return res;
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
            Point viewloaction = this.Grid.PointControlToView(e.Location);
            if (this.Rect.Contains(viewloaction))
            {
                this.Grid.OnCellMouseClick(this, e);
                this.Grid.OnCellClick(this);
            }
            return false;
        }
        public virtual Point PointToView(Point pt)
        {
            return pt;
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

            if ((this.EditMode & EditMode.KeyDown) == EditMode.KeyDown)
            {
                this.InitEdit(this);
            }
            else if (e.KeyCode == Keys.F2 && (this.EditMode & EditMode.F2) == EditMode.F2)
            {
                this.InitEdit(this);
            }
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnKeyDown(this, e, ve))
                {
                    return true;
                }
            }
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
            return false;

        }
        bool lckedit = false;
        public virtual bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            if (lckedit)
                return false;
            lckedit = true;
            try
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

            }
            finally
            {
                lckedit = false;
            }
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
        public const int WM_KEYDOWN = 0x100;
        public virtual bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnPreProcessMessage(this, ref msg, ve))
                {
                    return true;
                }
            }
            //KeyEventArgs e = new KeyEventArgs(((Keys)((int)msg.WParam)));

            //if ((e.KeyCode & Keys.Control) == Keys.Control)
            //{
            //    if (msg.Msg == WM_KEYDOWN)
            //    {
            //        this.InitEdit();
            //    }
            //}

            return false;
        }

        public virtual bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnProcessCmdKey(this, ref msg, keyData, ve))
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

        public virtual bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_CHAR)
            {
                if ((this.EditMode & Enums.EditMode.IME) == Enums.EditMode.IME)
                {
                    this.InitEdit(this);
                }
            }
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
        private bool _checked = false;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (this.OnBeforeValueChanged(value))
                {
                    _checked = value;
                    this.OnValueChanged(value);
                }
            }
        }

        #endregion

        #region ICaption 成员
        private string _caption = string.Empty;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
            }
        }

        #endregion

        #region IInitEdit 成员

        int cellinitcout = 0;

        protected virtual bool OnBeforeCellInitEdit()
        {
            BeforeInitEditCancelArgs e = new BeforeInitEditCancelArgs(this);
            this.Grid.OnBeforeCellInitEdit(e);
            if (e.Cancel)
            {
                return true;
            }
            return false;
        }

        public virtual bool InitEdit(object obj)
        {
            if (this.InEdit)
            {
                return false;
            }

            if (OnBeforeCellInitEdit())
            {
                return false;
            }
            if (this.ReadOnly)
            {
                return false;
            }


            this.Grid.SetFoucsedCellSelect(this);
            this.Grid.InitEdit();
            this.Grid.AddEdit(this);
            this.Grid.BeginReFresh();
            ICellEditControl editcontrol = this.OwnEditControl;
            if (editcontrol != null)
            {
                editcontrol.InitEdit(this);
                this.Grid.EditCell = this;
            }
            this.Grid.OnCellInitEdit(this);
            this.Grid.EndReFresh();
            return this.InEdit;

        }

        #endregion

        #region IClear 成员

        public void Clear()
        {
            if (this.FunctionCells != null)
            {
                this.FunctionCells.Clear();
            }
        }

        #endregion

        #region ISetAllDeafultBoarder 成员

        public void SetSelectCellBorderBorderOutside()
        {
            if (BorderStyle == null)
            {
                BorderStyle = this.Grid.ClassFactory.CreateBorderStyle();
            }
            this.BorderStyle.LeftLineStyle.Visible = true;
            this.BorderStyle.TopLineStyle.Visible = true;
            this.BorderStyle.RightLineStyle.Visible = true;
            this.BorderStyle.BottomLineStyle.Visible = true;
        }

        #endregion

        #region IEndEdit 成员

        int cellendcout = 0;

        public void EndEdit()
        {
            if (!this.InEdit)
            {
                return;
            }

            if (this.OwnEditControl != null)
            {
                this.OwnEditControl.EndEdit();
            }

            this.Grid.OnCellEndEdit(this);

        }

        #endregion

        #region IInEdit 成员
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public virtual bool InEdit
        {
            get
            {
                return (this.Grid.EditCell == this);
            }
        }

        #endregion

        #region IBackImage 成员
        private Bitmap _backimage = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Bitmap BackImage
        {
            get
            {
                return _backimage;
            }
            set
            {

                this.Grid.BeginReFresh();
                _backimage = value;
                this.Grid.EndReFresh();
            }
        }

        #endregion

        #region ITextDirection 成员
        private bool _DirectionVertical = false;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool DirectionVertical
        {
            get
            {
                return _DirectionVertical;
            }
            set
            {
                this.Grid.BeginReFresh();

                _DirectionVertical = value;
                this.Grid.EndReFresh();
            }
        }

        #endregion

        #region IIsMergeCell 成员
        [Browsable(false)]
        public virtual bool IsMergeCell
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region IDateTime 成员
        //private DateTime _datetime = DateTime.MinValue;
        //public DateTime DateTime
        //{
        //    get
        //    {
        //        return _datetime;
        //    }
        //    set
        //    {
        //        string text = string.Empty;

        //        if (this.OnBeforeValueChanged(value))
        //        {
        //            _datetime = value;
        //            this.OnValueChanged(value);
        //        }
        //        if (this._datetime != DateTime.MinValue)
        //        {
        //            string format = DataExcelSetting.DateTimeDeafultFormat;
        //            if (this.FormatType == FormatType.DateTime)
        //            {
        //                if (this.FormatString == string.Empty)
        //                {
        //                    format = this.FormatString;
        //                }

        //            }
        //            text = _datetime.ToString(format);
        //        }
        //        if (this.OnBeforeTextChaned(text))
        //        {
        //            this.OnTextChanged(text);
        //        }

        //    }
        //}

        #endregion

        #region IDisplayMember 成员
        private string _displaymember = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string DisplayMember
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
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string ValueMember
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
        private List<ICell> _functioncells = null;
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public List<ICell> FunctionCells
        {
            get { return _functioncells; }
        }

        #endregion

        #region IParentFunctionCells 成员
        private List<ICell> _ParentFunctionCells = null;
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public List<ICell> ParentFunctionCells
        {
            get
            {
                return _ParentFunctionCells;
            }
            set
            {
                _ParentFunctionCells = value;
            }
        }

        public void AddParentFunctionCell(ICell cell)
        {
            if (ParentFunctionCells == null)
            {
                ParentFunctionCells = new List<ICell>();
            }
            if (!ParentFunctionCells.Contains(cell))
            {
                ParentFunctionCells.Add(cell);
            }
        }

        #endregion

        #region IParentFunctionCells 成员

        public void ExecuteParentExpresses()
        {
            if (this.ParentFunctionCells == null)
                return;
            foreach (ICell cell in ParentFunctionCells)
            {
                cell.ExecuteExpression();
            }
        }

        #endregion

        #region IParentFunctionCells 成员

        public void AddParent()
        {
            if (this.FunctionCells == null)
                return;
            foreach (ICell cell in FunctionCells)
            {
                cell.AddParentFunctionCell(this);
            }

        }

        public void ClearParent()
        {
            if (FunctionCells == null)
                return;
            foreach (ICell cell in FunctionCells)
            {
                if (cell != null)
                {
                    cell.ParentFunctionCells.Remove(this);
                }
            }
        }

        #endregion

        #region IDrawGridLine 成员

        //public void DrawGridLine(Graphics g)
        //{
        //if (this.OwnBackCell != null)
        //{
        //    this.OwnBackCell.DrawGridLine(g);
        //}
        //if (this.OwnMergeCell != null)
        //{
        //    this.OwnMergeCell.DrawGridLine(g);
        //    return;
        //}
        //if (_BorderStyle != null)
        //{
        //    if (_BorderStyle.BorderAnchor != BorderAnchor.Null)
        //    {
        //        if ((_BorderStyle.BorderAnchor & BorderAnchor.Left) == BorderAnchor.Left)
        //        {
        //            Pen pen = _BorderStyle.LeftLineStyle.GetPen();
        //            g.DrawLine(pen, this.Left, this.Top, this.Left, this.Bottom);
        //        }

        //        if ((_BorderStyle.BorderAnchor & BorderAnchor.Top) == BorderAnchor.Top)
        //        {
        //            Pen pen = _BorderStyle.TopLineStyle.GetPen();
        //            g.DrawLine(pen, this.Left, this.Top, this.Right, this.Top);
        //        }

        //        if ((_BorderStyle.BorderAnchor & BorderAnchor.Right) != BorderAnchor.Right)
        //        {
        //            this.Grid.DrawGridLine(g, this.Right, this.Top, this.Right, this.Bottom);
        //        }

        //        if ((_BorderStyle.BorderAnchor & BorderAnchor.Bottom) != BorderAnchor.Bottom)
        //        {
        //            this.Grid.DrawGridLine(g, this.Left, this.Bottom, this.Right, this.Bottom);
        //        }
        //    }
        //    else
        //    {
        //        this.Grid.DrawGridLine(g, this.Right, this.Top, this.Right, this.Bottom);
        //        this.Grid.DrawGridLine(g, this.Left, this.Bottom, this.Right, this.Bottom);
        //    }
        //}
        //else
        //{
        //    this.Grid.DrawGridLine(g, this.Right, this.Top, this.Right, this.Bottom);
        //    this.Grid.DrawGridLine(g, this.Left, this.Bottom, this.Right, this.Bottom);
        //}
        //}

        #endregion

        #region IMouseOverImage 成员
        private Bitmap _MouseOverImage = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Bitmap MouseOverImage
        {
            get
            {
                if (this._MouseOverImage == EmptyCell._MouseOverImage)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseOverImage;
                    }
                }
                return _MouseOverImage;
            }
            set
            {
                _MouseOverImage = value;
            }
        }

        #endregion

        #region IMouseDownImage 成员
        private Bitmap _MouseDownImage = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Bitmap MouseDownImage
        {
            get
            {
                if (this._MouseDownImage == EmptyCell._MouseDownImage)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseDownImage;
                    }
                }
                return _MouseDownImage;
            }
            set
            {
                _MouseDownImage = value;
            }
        }

        private Bitmap _MouseUpImage = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Bitmap MouseUpImage
        {
            get
            {
                if (this._MouseUpImage == EmptyCell._MouseUpImage)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseUpImage;
                    }
                }
                return _MouseUpImage;
            }
            set
            {
                _MouseUpImage = value;
            }
        }
        #endregion

        #region IDisableImage 成员
        private Bitmap _DisableImage = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Bitmap DisableImage
        {
            get
            {
                if (this._DisableImage == EmptyCell._DisableImage)
                {
                    if (headercell != null)
                    {
                        return headercell.DisableImage;
                    }
                }
                return _DisableImage;
            }
            set
            {
                _DisableImage = value;
            }
        }

        #endregion

        #region IReadOnlyImage 成员
        private Bitmap _ReadOnlyImage = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Bitmap ReadOnlyImage
        {
            get
            {
                if (this._ReadOnlyImage == EmptyCell._ReadOnlyImage)
                {
                    if (headercell != null)
                    {
                        return headercell.ReadOnlyImage;
                    }
                }
                return _ReadOnlyImage;
            }
            set
            {
                _ReadOnlyImage = value;
            }
        }

        #endregion

        #region IFocusImage 成员
        private Bitmap _FocusImage = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Bitmap FocusImage
        {
            get
            {
                if (this._FocusImage == EmptyCell._FocusImage)
                {
                    if (headercell != null)
                    {
                        return headercell.FocusImage;
                    }
                }
                return _FocusImage;
            }
            set
            {
                _FocusImage = value;
            }
        }

        #endregion

        #region IMouseOverBackColor 成员
        private Color _MouseOverBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseOverBackColor
        {
            get
            {
                if (this._MouseOverBackColor == EmptyCell._MouseOverBackColor)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseOverBackColor;
                    }
                }
                return _MouseOverBackColor;
            }
            set
            {
                _MouseOverBackColor = value;
            }
        }

        #endregion

        #region IMouseDownBackColor 成员
        private Color _MouseDownBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseDownBackColor
        {
            get
            {
                if (this._MouseDownBackColor == EmptyCell._MouseDownBackColor)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseDownBackColor;
                    }
                }
                return _MouseDownBackColor;
            }
            set
            {
                _MouseDownBackColor = value;
            }
        }



        private Color _MouseUpBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseUpBackColor
        {
            get
            {
                if (this._MouseUpBackColor == EmptyCell._MouseUpBackColor)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseUpBackColor;
                    }
                }
                return _MouseUpBackColor;
            }
            set
            {
                _MouseUpBackColor = value;
            }
        }
        #endregion

        #region IFocusBackColor 成员
        private Color _FocusBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color FocusBackColor
        {
            get
            {
                if (this._FocusBackColor == EmptyCell._FocusBackColor)
                {
                    if (headercell != null)
                    {
                        return headercell.FocusBackColor;
                    }
                }
                return _FocusBackColor;
            }
            set
            {
                _FocusBackColor = value;
            }
        }

        #endregion

        #region IMouseOverForeColor 成员
        private Color _MouseOverForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseOverForeColor
        {
            get
            {
                if (this._MouseOverForeColor == EmptyCell._MouseOverForeColor)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseOverForeColor;
                    }
                }
                return _MouseOverForeColor;
            }
            set
            {
                _MouseOverForeColor = value;
            }
        }

        #endregion

        #region IMouseDownForeColor 成员
        private Color _MouseDownForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseDownForeColor
        {
            get
            {
                if (this._MouseDownForeColor == EmptyCell._MouseDownForeColor)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseDownForeColor;
                    }
                }
                return _MouseDownForeColor;
            }
            set
            {
                _MouseDownForeColor = value;
            }
        }


        private Color _MouseUpForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseUpForeColor
        {
            get
            {
                if (this._MouseUpForeColor == EmptyCell._MouseUpForeColor)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseUpForeColor;
                    }
                }
                return _MouseUpForeColor;
            }
            set
            {
                _MouseUpForeColor = value;
            }
        }
        #endregion

        #region IFocusForeColor 成员
        private Color _FocusForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color FocusForeColor
        {
            get
            {
                if (this._FocusForeColor == EmptyCell._FocusForeColor)
                {
                    if (headercell != null)
                    {
                        return headercell.FocusForeColor;
                    }
                }
                return _FocusForeColor;
            }
            set
            {
                _FocusForeColor = value;
            }
        }

        #endregion


        #region IContensWidth 成员
        private int _ContensWidth = 0;
        [Browsable(false)]
        public int ContensWidth
        {
            get
            {
                return _ContensWidth;
            }
            set
            {
                _ContensWidth = value;
                if (this.Column.AutoWidth)
                {
                    if (_ContensWidth < this.Grid.Width / 2)
                    {
                        if (_ContensWidth > this.Width)
                        {
                            this.Column.Width = _ContensWidth;
                        }
                    }
                }
            }
        }

        #endregion

        #region IContensHeigth 成员
        private int _ContensHeigth = 0;
        [Browsable(false)]
        public int ContensHeigth
        {
            get
            {
                return _ContensHeigth;
            }
            set
            {
                _ContensHeigth = value;
                if (this.Row.AutoHeight)
                {
                    if (_ContensHeigth < this.Grid.Height / 2)
                    {
                        if (_ContensHeigth > this.Height)
                        {
                            this.Row.Height = _ContensHeigth;
                        }
                    }
                }
            }
        }

        #endregion

        #region IFreshContens 成员

        public virtual void FreshContens()
        {
            if (this.OwnMergeCell != null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Text))
                return;
            Graphics g = this.Grid.GetGraphics();
            if (g == null)
                return;
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            if (this.DirectionVertical)
            {
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionVertical;
            }
            Size Size = Feng.Utils.ConvertHelper.ToSize(g.MeasureString(this.Text + "A", this.Font, Point.Empty, sf));
            _ContensWidth = Size.Width;
            _ContensHeigth = Size.Height;
        }

        #endregion

        #region IFreshContens 成员
        private bool _AutoFreshContens = true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AutoFreshContens
        {
            get
            {
                return _AutoFreshContens;
            }
            set
            {
                _AutoFreshContens = value;
            }
        }

        #endregion

        #region ITableCellPrinted 成员
        private bool _IsTableCellPrinted = false;
        [Browsable(false)]
        public virtual bool IsTableCellPrinted
        {
            get
            {
                return _IsTableCellPrinted;
            }
            set
            {
                _IsTableCellPrinted = value;
            }
        }

        #endregion

        #region ISave 成员

        public void Save(Feng.Excel.IO.BinaryWriter stream)
        {
            stream.Write(this.Data);

        }

        #endregion

        #region IRead 成员

        public void Read(out int count, ref string owncontrolname)
        {
            count = 0;

            try
            {

                if (!string.IsNullOrEmpty(this._Expression))
                {
                    this.Grid.ExpressionCells.Add(this);
                }
                if (this._id != string.Empty)
                {
                    this.Grid.IDCells[this._id] = this;
                }
                if (this._tabstop)
                {
                    this.Grid.TabList.Add(this);
                }
                if (this._fieldname != string.Empty)
                {
                    this.Grid.FieldCells[this._fieldname] = this;
                }
                if (celleditid > 0)
                {
                    ICellEditControl cellEditControl = this.Grid.CellSaveEdits.GetItemByID(celleditid);

                    if (cellEditControl != null)
                    {
                        this.OwnEditControl = cellEditControl;
                        this.OwnEditControl.Cell = this;
                    }
                }

            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }

        }

        public void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }
        private int celleditid = 0;
        private string owncontrolname = string.Empty;
        private int columnindex = 0;
        private int rowindex = 0;
        public void ReadDataStruct(DataStruct data)
        {
            if (data == null)
                return;

            int count = 0;
            string strowncontrolsname = string.Empty;
            using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                stream.ReadCache();
                this._AutoExecuteExpress = stream.ReadIndex(1, this._AutoExecuteExpress);// stream.ReadBoolean();
                this._AutoFreshContens = stream.ReadIndex(3, this._AutoFreshContens);
                this._AutoMultiline = stream.ReadIndex(4, this._AutoMultiline);
                this._BackColor = stream.ReadIndex(5, this._BackColor);// stream.ReadColor();
                this._backimage = stream.ReadIndex(6, this._backimage);



                this._caption = stream.ReadIndex(8, this._caption);
                this.cellendcout = stream.ReadIndex(9, this.cellendcout);
                this.cellinitcout = stream.ReadIndex(10, this.cellinitcout);
                this._celltype = (CellType)stream.ReadIndex(11, (int)this._celltype);
                this._checked = stream.ReadIndex(12, this._checked);
                columnindex = stream.ReadIndex(13, 0);
                this._ContensHeigth = stream.ReadIndex(14, this._ContensHeigth);
                this._ContensWidth = stream.ReadIndex(15, this._ContensWidth);
                this._DirectionVertical = stream.ReadIndex(17, this._DirectionVertical);
                Color _DisableBackColor = stream.ReadIndex(18, Color.Empty);
                Color _DisableForeColor = stream.ReadIndex(19, Color.Empty);
                this._DisableImage = stream.ReadIndex(20, this._DisableImage);
                stream.ReadIndex(21, false);
                this._Expression = stream.ReadIndex(22, this._Expression);
                this._font = stream.ReadIndex(23, this._font);
                this._ForeColor = stream.ReadIndex(24, this._ForeColor);
                this._FormatString = stream.ReadIndex(25, this._FormatString);
                this._FormatType = (FormatType)stream.ReadIndex(26, (int)this._FormatType);
                this._FocusBackColor = stream.ReadIndex(27, this._FocusBackColor);
                this._FocusForeColor = stream.ReadIndex(28, this._FocusForeColor);
                this._FocusImage = stream.ReadIndex(29, this._FocusImage);
                this._HorizontalAlignment = (StringAlignment)stream.ReadIndex(31, (int)this._HorizontalAlignment);
                bool inedit = stream.ReadIndex(32, false);
                stream.ReadIndex(33, false);
                this._IsTableCellPrinted = stream.ReadIndex(34, this._IsTableCellPrinted);
                this._MouseDownBackColor = stream.ReadIndex(36, this._MouseDownBackColor);
                this._MouseDownForeColor = stream.ReadIndex(37, this._MouseDownForeColor);
                this._MouseDownImage = stream.ReadIndex(38, this._MouseDownImage);
                this._MouseOverBackColor = stream.ReadIndex(39, this._MouseOverBackColor);
                this._MouseOverForeColor = stream.ReadIndex(40, this._MouseOverForeColor);
                this._MouseOverImage = stream.ReadIndex(41, this._MouseOverImage);
                this.owncontrolname = stream.ReadIndex(44, owncontrolname);
                this._readonly = stream.ReadIndex(46, this._readonly);
                Color _ReadOnlyBackColor = stream.ReadIndex(47, Color.Empty);
                Color _ReadOnlyForeColor = stream.ReadIndex(48, Color.Empty);
                this._ReadOnlyImage = stream.ReadIndex(49, this._ReadOnlyImage);
                rowindex = stream.ReadIndex(50, 0);

                this._SelectBorderColor = stream.ReadIndex(51, this._SelectBorderColor);
                this._selectcolor = stream.ReadIndex(52, this._selectcolor);
                this._selected = stream.ReadIndex(53, this._selected);
                this._SelectForceColor = stream.ReadIndex(54, this._SelectForceColor);
                this._startexecuteexpress = stream.ReadIndex(55, this._startexecuteexpress);
                if (data.ReadValue)
                {
                    this._text = stream.ReadIndex(56, string.Empty);
                }
                else
                {
                    stream.ReadIndex(56, string.Empty);
                }
                this._UpdateVersion = stream.ReadIndex(57, this._UpdateVersion);
                if (data.ReadValue)
                {
                    this._value = stream.ReadBaseValueIndex(58, this._value);
                }
                else
                {
                    stream.ReadBaseValueIndex(58, this._value);
                }
                this._VerticalAlignment = (StringAlignment)stream.ReadIndex(59, (int)this._VerticalAlignment);
                this._inhertreadonly = stream.ReadIndex(60, this._inhertreadonly);
                this._ShowSelectBorder = stream.ReadIndex(61, this._ShowSelectBorder);
                this._EditMode = (EditMode)stream.ReadIndex(62, (int)this._EditMode);
                this._printtext = stream.ReadIndex(63, this._printtext);
                this._PrintBackImage = stream.ReadIndex(64, this._PrintBackImage);
                this._PrintBorder = stream.ReadIndex(65, this._PrintBorder);
                this._PrintBackColor = stream.ReadIndex(66, this._PrintBackColor);
                this._MouseOverImageSizeMode = (ImageLayout)stream.ReadIndex(67, (int)this._MouseOverImageSizeMode);
                this._MouseDownImageSizeMode = (ImageLayout)stream.ReadIndex(68, (int)this._MouseDownImageSizeMode);
                this._DisableImageSizeMode = (ImageLayout)stream.ReadIndex(69, (int)this._DisableImageSizeMode);
                this._ReadOnlyImageSizeMode = (ImageLayout)stream.ReadIndex(70, (int)this._ReadOnlyImageSizeMode);
                this._FocusImageSizeMode = (ImageLayout)stream.ReadIndex(71, (int)this._FocusImageSizeMode);
                this._BackImgeSizeMode = (ImageLayout)stream.ReadIndex(72, (int)this._BackImgeSizeMode);
                this._id = stream.ReadIndex(73, this._id);
                this._fieldname = stream.ReadIndex(74, string.Empty);
                this.celleditid = stream.ReadIndex(75, 0);
                this._defaultvalue = stream.ReadIndex(76, string.Empty);
                this._visible = stream.ReadIndex(77, this._visible);
                stream.ReadIndex(78, false);
                this._PropertyOnCellInitEdit = stream.ReadIndex(79, this._PropertyOnCellInitEdit);
                this._PropertyOnCellEndEdit = stream.ReadIndex(80, this._PropertyOnCellEndEdit);
                this._PropertyOnCellValueChanged = stream.ReadIndex(81, this._PropertyOnCellValueChanged);
                this._PropertyOnClick = stream.ReadIndex(82, this._PropertyOnClick);
                this._PropertyOnDoubleClick = stream.ReadIndex(83, this._PropertyOnDoubleClick);
                this._PropertyOnKeyDown = stream.ReadIndex(84, this._PropertyOnKeyDown);
                this._PropertyOnKeyPress = stream.ReadIndex(85, this._PropertyOnKeyPress);
                this._PropertyOnKeyUp = stream.ReadIndex(86, this._PropertyOnKeyUp);
                this._PropertyOnMouseCaptureChanged = stream.ReadIndex(87, this._PropertyOnMouseCaptureChanged);
                this._PropertyOnMouseClick = stream.ReadIndex(88, this._PropertyOnMouseClick);
                this._PropertyOnMouseDoubleClick = stream.ReadIndex(89, this._PropertyOnMouseDoubleClick);
                this._PropertyOnMouseDown = stream.ReadIndex(90, this._PropertyOnMouseDown);
                this._PropertyOnMouseEnter = stream.ReadIndex(91, this._PropertyOnMouseEnter);
                this._PropertyOnMouseHover = stream.ReadIndex(92, this._PropertyOnMouseHover);
                this._PropertyOnMouseLeave = stream.ReadIndex(93, this._PropertyOnMouseLeave);
                this._PropertyOnMouseMove = stream.ReadIndex(94, this._PropertyOnMouseMove);
                this._PropertyOnMouseUp = stream.ReadIndex(95, this._PropertyOnMouseUp);
                this._PropertyOnMouseWheel = stream.ReadIndex(96, this._PropertyOnMouseWheel);
                this._PropertyOnPreviewKeyDown = stream.ReadIndex(97, this._PropertyOnPreviewKeyDown);
                this._remark = stream.ReadIndex(98, this._remark);
                this._extend = stream.ReadIndex(99, this._extend);
                this._allowcopy = stream.ReadIndex(100, this._allowcopy);
                this._tooltip = stream.ReadIndex(101, this._tooltip);
                this._tabstop = stream.ReadIndex(102, this._tabstop);
                this._tabindex = stream.ReadIndex(103, this._tabindex);
                this._url = stream.ReadIndex(104, this._url);
                this._expressionindex = stream.ReadIndex(105, this._expressionindex);
                this._tablecolumnname = stream.ReadIndex(106, this._tablecolumnname);
                this._tablename = stream.ReadIndex(107, this._tablename);
                this._tablerowindex = stream.ReadIndex(108, this._tablerowindex);
                this._AllowInputExpress = (YesNoInhert)stream.ReadIndex(109, (int)this._AllowInputExpress);



                this._MouseUpBackColor = stream.ReadIndex(110, this._MouseUpBackColor);
                this._MouseUpForeColor = stream.ReadIndex(111, this._MouseUpForeColor);
                this._MouseUpImage = stream.ReadIndex(112, this._MouseUpImage);
                this._MouseUpImageSizeMode = (ImageLayout)stream.ReadIndex(113, (int)this._MouseUpImageSizeMode);
                this._borderStyle = CellBorderStyle.GetLineStyle(stream.ReadIndex(114, DataStruct.DataStructNull));

                rowindex = stream.ReadIndex(115, rowindex);
                columnindex = stream.ReadIndex(116, columnindex);
                this._text1 = stream.ReadIndex(117, _text1);
                this._text2 = stream.ReadIndex(118, _text2);
                this._text3 = stream.ReadIndex(119, _text3);
                this._PropertyOnDrawBack = stream.ReadIndex(120, _PropertyOnDrawBack);
                this._PropertyOnDrawCell = stream.ReadIndex(121, _PropertyOnDrawCell);
#if DEBUG
                if (rowindex == 8 && columnindex == 2)
                {

                }
#endif
#if DEBUG
                if (_text1 == "123")
                {
                    string str = this.owncontrolname;
                }
#endif


            }

            if (this.Grid != null)
            {
                this._column = this.Grid.Columns[columnindex];
                this._row = this.Grid.Rows[rowindex];
            }
            Read(out count, ref strowncontrolsname);

            this.init();
        }
        #endregion

        #region IDataStruct 成员
        public int RowIndex
        {
            get
            {
                if (this._row == null)
                    return 0;
                return this._row.Index;
            }
        }
        public int ColumnIndex
        {
            get
            {
                if (this._column == null)
                    return 0;
                return this._column.Index;
            }
        }
        public static readonly Cell EmptyCell = new Cell();


        [Browsable(false)]
        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = string.Empty,
                    Name = string.Empty,
                };
                try
                {
                    Type t = this.GetType();

                    //if (this.Text == "test")
                    //{

                    //}

                    //if (this.Row.Index < 1 && this.Column.Index > 0)
                    //{
                    //    this.BackColor = DataColors.HeaderColor;
                    //    this.HorizontalAlignment = StringAlignment.Center;
                    //    this.VerticalAlignment = StringAlignment.Center;

                    //}
                    //else if (this.Column.Index < 1)
                    //{
                    //    this.BackColor = DataColors.HeaderColor;
                    //    this.HorizontalAlignment = StringAlignment.Center;
                    //    this.VerticalAlignment = StringAlignment.Center;
                    //    this.OwnEditControl = CellRowHeader.Instance(this.Grid);
                    //}

                    using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                    {
                        bw.Write(1, this._AutoExecuteExpress, EmptyCell._AutoExecuteExpress);
                        bw.Write(3, this._AutoFreshContens, EmptyCell._AutoFreshContens);
                        bw.Write(4, this._AutoMultiline, EmptyCell._AutoMultiline);

                        if (this.Row != null && this.Column != null)
                        {
                            if (this.Row.Index < 1 && this.Column.Index > 0)
                            {
                                //bw.Write(5, this._BackColor, DataColors.HeaderColor);

                            }
                            else if (this.Column.Index < 1)
                            {
                                //bw.Write(5, this._BackColor, DataColors.HeaderColor);
                            }
                            else
                            {
                                bw.Write(5, this._BackColor, EmptyCell._BackColor);
                            }
                        }
                        else
                        {
                            bw.Write(5, this._BackColor, EmptyCell._BackColor);
                        }
                        bw.Write(6, this._backimage, EmptyCell._backimage);
                        bw.Write(7, false, false);
                        bw.Write(8, this._caption, EmptyCell._caption);
                        bw.Write(9, this.cellendcout, EmptyCell.cellendcout);
                        bw.Write(10, this.cellinitcout, EmptyCell.cellinitcout);
                        bw.Write(11, (int)this._celltype, (int)EmptyCell._celltype);
                        bw.Write(12, this._checked, EmptyCell._checked);
                        //bw.Write(13, this.ColumnIndex, EmptyCell.ColumnIndex);
                        bw.Write(14, this._ContensHeigth, EmptyCell._ContensHeigth);
                        bw.Write(15, this._ContensWidth, EmptyCell._ContensWidth);
                        bw.Write(17, this._DirectionVertical, EmptyCell._DirectionVertical);
                        bw.Write(18, Color.Empty, Color.Empty);
                        bw.Write(19, Color.Empty, Color.Empty);
                        bw.Write(20, this._DisableImage, EmptyCell._DisableImage);
                        bw.Write(21, false, false);
                        bw.Write(22, this._Expression, EmptyCell._Expression);
                        bw.Write(23, this._font, EmptyCell._font);
                        bw.Write(24, this._ForeColor, EmptyCell._ForeColor);
                        bw.Write(25, this._FormatString, EmptyCell._FormatString);
                        bw.Write(26, (int)this._FormatType, (int)EmptyCell._FormatType);
                        bw.Write(27, this._FocusBackColor, EmptyCell._FocusBackColor);
                        bw.Write(28, this._FocusForeColor, EmptyCell._FocusForeColor);
                        bw.Write(29, this._FocusImage, EmptyCell._FocusImage);
                        //bw.Write(30, this._functioncells);
#warning 需要优化
                        if (this.Row != null && this.Column != null)
                        {
                            if (this.Row.Index < 1 && this.Column.Index > 0)
                            {
                                //bw.Write(31, (int)this._HorizontalAlignment, (int)StringAlignment.Center);

                            }
                            else if (this.Column.Index < 1)
                            {
                                //bw.Write(31, (int)this._HorizontalAlignment, (int)StringAlignment.Center);
                            }
                            else
                            {
                                bw.Write(31, (int)this._HorizontalAlignment, (int)EmptyCell._HorizontalAlignment);
                            }
                        }
                        else
                        {
                            bw.Write(31, (int)this._HorizontalAlignment, (int)EmptyCell._HorizontalAlignment);
                        }
                        bw.Write(32, false, false);
                        bw.Write(33, false, false);
                        bw.Write(34, this._IsTableCellPrinted, EmptyCell._IsTableCellPrinted);
                        //bw.Write(35, this._MergeCell);
                        bw.Write(36, this._MouseDownBackColor, EmptyCell._MouseDownBackColor);
                        bw.Write(37, this._MouseDownForeColor, EmptyCell._MouseDownForeColor);
                        bw.Write(38, this._MouseDownImage, EmptyCell._MouseDownImage);
                        bw.Write(39, this._MouseOverBackColor, EmptyCell._MouseOverBackColor);
                        bw.Write(40, this._MouseOverForeColor, EmptyCell._MouseOverForeColor);
                        bw.Write(41, this._MouseOverImage, EmptyCell._MouseOverImage);
                        //bw.Write(42, this._OwnDataTable);
                        //bw.Write(43, this._OwnDataTableCell);
                        if (this._OwnEditControl != null)
                        {
                            if (this.Row != null && this.Column != null)
                            {
                                Type type = this._OwnEditControl.GetType();
                                if (this.Row.Index < 1 && this.Column.Index > 0)
                                {
                                    //bw.Write(44, type == typeof(CellColumnHeader) ? string.Empty : type.FullName, string.Empty);

                                }
                                else if (this.Column.Index < 1)
                                {
                                    //bw.Write(44, type == typeof(CellRowHeader) ? string.Empty : type.FullName, string.Empty);
                                }
                                else
                                {
                                    bw.Write(44, this._OwnEditControl == null ? string.Empty : this._OwnEditControl.GetType().FullName, string.Empty);
                                }
                            }
                            else
                            {
                                bw.Write(44, this._OwnEditControl == null ? string.Empty : this._OwnEditControl.GetType().FullName, string.Empty);
                            }
                        }
                        //bw.Write(45, this._ParentFunctionCells);
                        bw.Write(46, this._readonly, EmptyCell._readonly);
                        bw.Write(47, Color.Empty, Color.Empty);
                        bw.Write(48, Color.Empty, Color.Empty);
                        bw.Write(49, this._ReadOnlyImage, EmptyCell._ReadOnlyImage);
                        //bw.Write(50, this.RowIndex, EmptyCell.RowIndex);
                        bw.Write(51, this._SelectBorderColor, EmptyCell._SelectBorderColor);
                        bw.Write(52, this._selectcolor, EmptyCell._selectcolor);
                        bw.Write(53, false, false);
                        bw.Write(54, this._SelectForceColor, EmptyCell._SelectForceColor);
                        bw.Write(55, this._startexecuteexpress, EmptyCell._startexecuteexpress);
                        bw.Write(56, this._text, EmptyCell._text);
                        bw.Write(57, this._UpdateVersion, EmptyCell._UpdateVersion);
                        object value = this._value;
                        //if (!string.IsNullOrWhiteSpace(this._text))
                        //{
                        //    System.Diagnostics.Debugger.Break();
                        //}

                        bw.WriteBaseValue(58, this._value, null);
                        bw.Write(59, (int)this._VerticalAlignment, (int)EmptyCell._VerticalAlignment);
                        bw.Write(60, this._inhertreadonly, EmptyCell._inhertreadonly);
                        bw.Write(61, this._ShowSelectBorder, EmptyCell._ShowSelectBorder);
                        bw.Write(62, (int)this.EditMode, (int)EmptyCell.EditMode);
                        bw.Write(63, this._printtext, EmptyCell._printtext);
                        bw.Write(64, this._PrintBackImage, EmptyCell._PrintBackImage);
                        bw.Write(65, this._PrintBorder, EmptyCell._PrintBorder);
                        bw.Write(66, this._PrintBackColor, EmptyCell._PrintBackColor);
                        bw.Write(67, (int)this._MouseOverImageSizeMode, (int)EmptyCell._MouseOverImageSizeMode);
                        bw.Write(68, (int)this._MouseDownImageSizeMode, (int)EmptyCell._MouseDownImageSizeMode);
                        bw.Write(69, (int)this._DisableImageSizeMode, (int)EmptyCell._DisableImageSizeMode);
                        bw.Write(70, (int)this._ReadOnlyImageSizeMode, (int)EmptyCell._ReadOnlyImageSizeMode);
                        bw.Write(71, (int)this._FocusImageSizeMode, (int)EmptyCell._FocusImageSizeMode);
                        bw.Write(72, (int)this._BackImgeSizeMode, (int)EmptyCell._BackImgeSizeMode);
                        bw.Write(73, this._id, EmptyCell._id);
                        bw.Write(74, this._fieldname, EmptyCell._fieldname);
                        int celleditid = 0;
                        if (this.OwnEditControl != null)
                        {
                            if (this.Row != null && this.Column != null)
                            {
                                if (this.Row.Index < 1 && this.Column.Index > 0)
                                {
                                    //if (this.OwnEditControl.GetType() != typeof(CellColumnHeader))
                                    //{
                                    //    celleditid = this.OwnEditControl.AddressID;
                                    //}

                                }
                                else if (this.Column.Index < 1)
                                {
                                    //if (this.OwnEditControl.GetType() != typeof(CellRowHeader))
                                    //{
                                    //    celleditid = this.OwnEditControl.AddressID;
                                    //}
                                }
                                else
                                {
                                    if (this.OwnEditControl.GetType() != this.Grid.DefaultEdit.GetType())
                                    {
                                        celleditid = this.OwnEditControl.AddressID;
                                    }
                                }
                            }
                            else
                            {
                                if (this.OwnEditControl.GetType() != this.Grid.DefaultEdit.GetType())
                                {
                                    celleditid = this.OwnEditControl.AddressID;
                                }
                            }
                        }

                        bw.Write(75, celleditid, 0);
                        bw.Write(76, this._defaultvalue, EmptyCell._defaultvalue);
                        bw.Write(77, this._visible, EmptyCell._visible);
                        bw.Write(78, false, false);
                        bw.Write(79, this._PropertyOnCellInitEdit, EmptyCell._PropertyOnCellInitEdit);
                        bw.Write(80, this._PropertyOnCellEndEdit, EmptyCell._PropertyOnCellEndEdit);
                        bw.Write(81, this._PropertyOnCellValueChanged, EmptyCell._PropertyOnCellValueChanged);
                        bw.Write(82, this._PropertyOnClick, EmptyCell._PropertyOnClick);
                        bw.Write(83, this._PropertyOnDoubleClick, EmptyCell._PropertyOnDoubleClick);
                        bw.Write(84, this._PropertyOnKeyDown, EmptyCell._PropertyOnKeyDown);
                        bw.Write(85, this._PropertyOnKeyPress, EmptyCell._PropertyOnKeyPress);
                        bw.Write(86, this._PropertyOnKeyUp, EmptyCell._PropertyOnKeyUp);
                        bw.Write(87, this._PropertyOnMouseCaptureChanged, EmptyCell._PropertyOnMouseCaptureChanged);
                        bw.Write(88, this._PropertyOnMouseClick, EmptyCell._PropertyOnMouseClick);
                        bw.Write(89, this._PropertyOnMouseDoubleClick, EmptyCell._PropertyOnMouseDoubleClick);
                        bw.Write(90, this._PropertyOnMouseDown, EmptyCell._PropertyOnMouseDown);
                        bw.Write(91, this._PropertyOnMouseEnter, EmptyCell._PropertyOnMouseEnter);
                        bw.Write(92, this._PropertyOnMouseHover, EmptyCell._PropertyOnMouseHover);
                        bw.Write(93, this._PropertyOnMouseLeave, EmptyCell._PropertyOnMouseLeave);
                        bw.Write(94, this._PropertyOnMouseMove, EmptyCell._PropertyOnMouseMove);
                        bw.Write(95, this._PropertyOnMouseUp, EmptyCell._PropertyOnMouseUp);
                        bw.Write(96, this._PropertyOnMouseWheel, EmptyCell._PropertyOnMouseWheel);
                        bw.Write(97, this._PropertyOnPreviewKeyDown, EmptyCell._PropertyOnPreviewKeyDown);
                        bw.Write(98, this._remark, EmptyCell._remark);
                        bw.Write(99, this._extend, EmptyCell._extend);
                        bw.Write(100, this._allowcopy, EmptyCell._allowcopy);
                        bw.Write(101, this._tooltip, EmptyCell._tooltip);
                        bw.Write(102, this._tabstop, EmptyCell._tabstop);
                        bw.Write(103, this._tabindex, EmptyCell._tabindex);
                        bw.Write(104, this._url, EmptyCell._url);
                        bw.Write(105, this._expressionindex, EmptyCell._expressionindex);
                        bw.Write(106, this._tablecolumnname, EmptyCell._tablecolumnname);
                        bw.Write(107, this._tablename, EmptyCell._tablename);
                        bw.Write(108, this._tablerowindex, EmptyCell._tablerowindex);
                        bw.Write(109, (int)this._AllowInputExpress, (int)EmptyCell._AllowInputExpress);

                        bw.Write(110, this._MouseUpBackColor, EmptyCell._MouseUpBackColor);
                        bw.Write(111, this._MouseUpForeColor, EmptyCell._MouseUpForeColor);
                        bw.Write(112, this._MouseUpImage, EmptyCell._MouseUpImage);
                        bw.Write(113, (int)this._MouseUpImageSizeMode, (int)EmptyCell._MouseUpImageSizeMode);
                        bw.Write(114, this._borderStyle, EmptyCell._borderStyle);
                        byte[] ds = bw.GetData();
                        if (ds.Length < 1)
                        {
                            return null;
                        }
                        bw.Write(115, this.RowIndex, EmptyCell.RowIndex);
                        bw.Write(116, this.ColumnIndex, EmptyCell.ColumnIndex);
                        bw.Write(117, this._text1, EmptyCell._text1);
                        bw.Write(118, this._text2, EmptyCell._text2);
                        bw.Write(119, this._text3, EmptyCell._text3);
                        bw.Write(120, this._PropertyOnDrawBack, EmptyCell._PropertyOnDrawBack);
                        bw.Write(121, this._PropertyOnDrawCell, EmptyCell._PropertyOnDrawCell);

                        data.Data = bw.GetData();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Row={0},Column={1}", this.Row.Index, this.Column.Index), ex);
                }
                //#warning 必须删除
                //                using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
                //                {
                //                    stream.ReadCache();
                //                }
                return data;

            }
        }

        #endregion

        #region IVersion 成员
        [Browsable(false)]
        public string Version
        {
            get { return Feng.DataUtlis.SmallVersion.AssemblySecondVersion; }
        }

        #endregion

        #region IAssembly 成员
        [Browsable(false)]
        public string DllName
        {
            get { return string.Empty; }
        }

        #endregion

        #region IDownLoadUrl 成员
        [Browsable(false)]
        public string DownLoadUrl
        {
            get { return string.Empty; }
        }

        #endregion

        #region IKeyValue 成员
        private Dictionary<object, object> _keyvalue = null;
        [Browsable(false)]
        [Category(CategorySetting.Design)]
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

        #region IOwnBackCell 成员
        private IBackCell _OwnBackCell = null;
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public virtual IBackCell OwnBackCell
        {
            get
            {
                return _OwnBackCell;
            }
            set
            {
                _OwnBackCell = value;
            }
        }

        #endregion

        #region IShowSelectBorder 成员
        private bool _ShowSelectBorder = false;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool ShowFocusedSelectBorder
        {
            get
            {
                return _ShowSelectBorder;
            }
            set
            {
                _ShowSelectBorder = value;
            }
        }

        #endregion
        private ICell headercell
        {
            get
            {
                if (rowheadercell1 != null)
                {
                    return rowheadercell1;
                }
                if (columnheadercell1 != null)
                {
                    return columnheadercell1;
                }
                return null;
            }
        }
        private ICell rowheadercell1
        {
            get
            {
                if (this.Row == null)
                    return null;
                if (this.Row.DefaultStyleCell == null)
                    return null;
                if (this.Row.DefaultStyleCell == this)
                    return null;
                if (this.Row.DefaultStyleCell.Row == null)
                    return null; 
                if (this.Row.Index < this.Row.DefaultStyleCell.Row.Index)
                    return null;
                return this.Row.DefaultStyleCell;
            }
        }
        private ICell columnheadercell1
        {
            get
            {
                if (this.Column == null)
                    return null;
                if (this.Column.DefaultStyleCell == null)
                    return null;
                if (this.Column.DefaultStyleCell == this)
                    return null;
                if (this.Column.DefaultStyleCell.Column == null)
                    return null;
                if (this.Column.Index< this.Column.DefaultStyleCell.Column.Index)
                    return null;
                return this.Column.DefaultStyleCell;
            }
        }
        #region IEditMode 成员
        private EditMode _EditMode = EditMode.Default;
        [DefaultValue(EditMode.KeyDown)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual EditMode EditMode
        {
            get
            {
                if (this._EditMode == EmptyCell._EditMode)
                {
                    if (headercell != null)
                    {
                        return headercell.EditMode;
                    }
                }
                return _EditMode;
            }
            set
            {
                _EditMode = value;
            }
        }

        #endregion

        #region ISetText 成员

        public virtual void SetText(string text)
        {
            if (!OnBeforeTextChaned(text))
            {
                return;
            }
            this._text = text;
            this.Grid.OnCellTextChanged(this);
        }


        #endregion

        #region IMouseOverImage 成员

        private ImageLayout _MouseOverImageSizeMode = ImageLayout.None;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ImageLayout MouseOverImageSizeMode
        {
            get
            {
                if (this._MouseOverImageSizeMode == EmptyCell._MouseOverImageSizeMode)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseOverImageSizeMode;
                    }
                }
                return _MouseOverImageSizeMode;
            }
            set
            {
                _MouseOverImageSizeMode = value;
            }
        }

        #endregion

        #region IMouseDownImage 成员

        private ImageLayout _MouseDownImageSizeMode = ImageLayout.None;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ImageLayout MouseDownImageSizeMode
        {
            get
            {
                if (this._MouseDownImageSizeMode == EmptyCell._MouseDownImageSizeMode)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseDownImageSizeMode;
                    }
                }
                return _MouseDownImageSizeMode;
            }
            set
            {
                _MouseDownImageSizeMode = value;
            }
        }


        private ImageLayout _MouseUpImageSizeMode = ImageLayout.None;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ImageLayout MouseUpImageSizeMode
        {
            get
            {
                if (this._MouseUpImageSizeMode == EmptyCell._MouseUpImageSizeMode)
                {
                    if (headercell != null)
                    {
                        return headercell.MouseUpImageSizeMode;
                    }
                }
                return _MouseUpImageSizeMode;
            }
            set
            {
                _MouseUpImageSizeMode = value;
            }
        }
        #endregion

        #region IDisableImage 成员
        private ImageLayout _DisableImageSizeMode = ImageLayout.None;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ImageLayout DisableImageSizeMode
        {
            get
            {
                if (this._DisableImageSizeMode == EmptyCell._DisableImageSizeMode)
                {
                    if (headercell != null)
                    {
                        return headercell.DisableImageSizeMode;
                    }
                }
                return _DisableImageSizeMode;
            }
            set
            {
                _DisableImageSizeMode = value;
            }
        }

        #endregion

        #region IReadOnlyImage 成员

        private ImageLayout _ReadOnlyImageSizeMode = ImageLayout.None;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ImageLayout ReadOnlyImageSizeMode
        {
            get
            {
                if (this._ReadOnlyImageSizeMode == EmptyCell._ReadOnlyImageSizeMode)
                {
                    if (headercell != null)
                    {
                        return headercell.ReadOnlyImageSizeMode;
                    }
                }
                return _ReadOnlyImageSizeMode;
            }
            set
            {
                _ReadOnlyImageSizeMode = value;
            }
        }

        #endregion

        #region IFocusImage 成员

        private ImageLayout _FocusImageSizeMode = ImageLayout.None;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ImageLayout FocusImageSizeMode
        {
            get
            {
                if (this._FocusImageSizeMode == EmptyCell._FocusImageSizeMode)
                {
                    if (headercell != null)
                    {
                        return headercell.FocusImageSizeMode;
                    }
                }
                return _FocusImageSizeMode;
            }
            set
            {
                _FocusImageSizeMode = value;
            }
        }

        #endregion

        #region IBackImage 成员

        private ImageLayout _BackImgeSizeMode = ImageLayout.None;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ImageLayout BackImgeSizeMode
        {
            get
            {
                if (this._BackImgeSizeMode == EmptyCell._BackImgeSizeMode)
                {
                    if (headercell != null)
                    {
                        return headercell.BackImgeSizeMode;
                    }
                }
                return _BackImgeSizeMode;
            }
            set
            {
                _BackImgeSizeMode = value;
            }
        }

        #endregion

        #region IMaxRowIndex 成员

        [Browsable(false)]
        public int MaxRowIndex
        {
            get { return this.Row.Index; }
        }

        #endregion

        #region IMaxColumnIndex 成员

        [Browsable(false)]
        public int MaxColumnIndex
        {
            get { return this.Column.Index; }
        }

        #endregion

        #region IPermissions 成员
        private string _permissions = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual string Permissions
        {
            get
            {
                return this._permissions;
            }
            set
            {
                this._permissions = value;
            }
        }
        private string _url = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue("")]
        public virtual string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }
        private Purview _purview = Purview.Equal;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual Purview Purview
        {
            get
            {
                return this._purview;
            }
            set
            {
                this._purview = value;
            }
        }
        #endregion

        #region ITableStop 成员
        private bool _tabstop = false;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool TabStop
        {
            get
            {
                return _tabstop;
            }
            set
            {
                _tabstop = value;
                if (_tabstop && _tabindex >= 0)
                {
                    this.Grid.TabList.Add(this);
                }
                else
                {
                    this.Grid.TabList.Remove(this);
                }
            }
        }

        #endregion

        #region ITableIndex 成员
        private int _tabindex = -1;
        [DefaultValue(-1)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public int TabIndex
        {
            get
            {
                return _tabindex;
            }
            set
            {
                _tabindex = value;
                if (_tabstop && _tabindex >= 0)
                {
                    this.Grid.TabList.Add(this);
                }
                else
                {
                    this.Grid.TabList.Remove(this);
                }
            }
        }

        #endregion

        #region IHotKeyEnable 成员
        private bool _hotkeyenable = true;
        public virtual bool HotKeyEnable
        {
            get
            {
                return _hotkeyenable;
            }
            set
            {
                _hotkeyenable = value;
                if (_hotkeyenable && _hotkeydata != Keys.None)
                {
                    this.Grid.HotKeyList.Add(this);
                }
                else
                {
                    this.Grid.HotKeyList.Remove(this);
                }
            }
        }

        #endregion

        #region IHotKeyData 成员
        private Keys _hotkeydata = Keys.None;
        public virtual Keys HotKeyData
        {
            get
            {
                return _hotkeydata;
            }
            set
            {
                _hotkeydata = value;
                if (_hotkeyenable && _hotkeydata != Keys.None)
                {
                    this.Grid.HotKeyList.Add(this);
                }
                else
                {
                    this.Grid.HotKeyList.Remove(this);
                }
            }
        }

        #endregion

        #region ILocation 成员

        public Point Location
        {
            get { return Point.Round(this.Rect.Location); }
        }

        #endregion

        #region IBingValue 成员
        [Browsable(false)]
        public object BingValue
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                string format = this.FormatString;
                FormatType ft = this.FormatType;
                if (!string.IsNullOrEmpty(this.Column.FormatString))
                {
                    ft = this.Column.FormatType;
                    format = this.Column.FormatString;
                }
                if (!string.IsNullOrWhiteSpace(this.Text))
                {
                    Size sf = Feng.Drawing.GraphicsHelper.Sizeof(this.Text, this.Font, this.Grid.FindControl());
                    this._ContensHeigth = sf.Height;
                    this._ContensWidth = sf.Width;
                }
            }
        }

        #endregion

        #region ICommandID 成员
        private string _id = string.Empty;

        public virtual string ID
        {
            get
            {
                return _id;
            }
            set
            {

                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    this.Grid.IDCells.Remove(_id);
                    _id = string.Empty;
                    return;
                }
                if (this.Grid.CheckID(value))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(_id))
                {
                    this.Grid.IDCells.Remove(_id);
                }
                _id = value.Trim();
                this.Grid.IDCells[_id] = this;
            }
        }

        #endregion

        #region ITag 成员

        private object _tag = null;
        [Browsable(false)]
        public virtual object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        #endregion

        #region IToolTip 成员

        private string _tooltip = null;
        [Browsable(false)]
        public virtual string ToolTip
        {
            get
            {
                return _tooltip;
            }
            set
            {
                _tooltip = value;
            }
        }

        #endregion
        private string _fieldname = string.Empty;
        public virtual string FieldName
        {
            get
            {
                return _fieldname;
            }
            set
            {
                _fieldname = value;
                this.Grid.FieldCells.Add(this);
            }
        }


        private string _defaultvalue = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual string DefaultValue
        {
            get
            {
                return _defaultvalue;
            }
            set
            {
                _defaultvalue = value;
            }
        }

        private bool _visible = true;
        [Browsable(true)]
        [DefaultValue(true)]
        [Category(CategorySetting.Design)]
        public virtual bool Visible
        {
            get
            {
                return this._visible;
            }
            set
            {
                this._visible = value;
            }
        }



        private string _remark = string.Empty;
        [Category(CategorySetting.Design)]
        public virtual string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
            }
        }
        private string _extend = string.Empty;
        [Category(CategorySetting.Design)]
        public virtual string Extend
        {
            get
            {
                return _extend;
            }
            set
            {
                _extend = value;
            }
        }

        private bool _allowcopy = true;
        [DefaultValue(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AllowCopy
        {
            get
            {
                return _allowcopy;
            }
            set
            {
                _allowcopy = value;
            }
        }

        private string _tablename = string.Empty;
        [Category(CategorySetting.PropertyTable)]
        public virtual string TableName { get { return _tablename; } set { _tablename = value; } }


        private string _tablecolumnname = string.Empty;
        [Category(CategorySetting.PropertyTable)]
        public string TableColumnName { get { return _tablecolumnname; } set { _tablecolumnname = value; } }

        private int _tablerowindex = -1;
        [DefaultValue(-1)]
        [Category(CategorySetting.PropertyTable)]
        public int TableRowIndex { get { return _tablerowindex; } set { _tablerowindex = value; } }

        private YesNoInhert _AllowInputExpress = YesNoInhert.Inherit;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(YesNoInhert.Inherit)]
        public virtual YesNoInhert AllowInputExpress
        {
            get
            {
                if (this._AllowInputExpress == EmptyCell._AllowInputExpress)
                {
                    if (headercell != null)
                    {
                        return headercell.AllowInputExpress;
                    }
                }
                if (_AllowInputExpress == YesNoInhert.Inherit)
                {
                    return this.Grid.AllowInputExpress;
                }
                return _AllowInputExpress;
            }
            set { _AllowInputExpress = value; }
        }

        private EditVersionCollection editVersions = null;
        public EditVersionCollection EditVersions
        {
            get
            {
                if (editVersions == null)
                {
                    editVersions = new EditVersionCollection();
                }
                return editVersions;
            }
        }

        private EditVersion currentEditVersion = null;
        public EditVersion CurrentEditVersion
        {
            get
            {
                if (currentEditVersion == null)
                {
                    currentEditVersion = new EditVersion();
                }
                return currentEditVersion;
            }
        }

        public EditVersion AddVersion()
        {
            EditVersion editVersion = new EditVersion();
            editVersion.EditID = 0;
            editVersion.EditTime = DateTime.Now;
            editVersion.EditUserID = this.Grid.UserID;
            editVersion.EditUserName = this.Grid.UserName;
            return editVersion;
        }
    }
}
