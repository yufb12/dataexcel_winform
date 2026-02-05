#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using Feng.Utils;
using Feng.Drawing;
using Feng.Print;
using Feng.Forms.Controls.Designer;
using Feng.Data;
using Feng.Excel.Print;
using Feng.Enums;
using Feng.Excel.Args; 
using Feng.Excel.Interfaces;
using Feng.Excel.Styles;
using Feng.Excel.App;
using Feng.Forms.Base;
using Feng.Forms.Views;

namespace Feng.Excel.Base
{
    [Serializable]
    [Description("功能：作为单无格背景显示。举例：打印法票的时的底图。")]
    public class BackCell : IBackCell
    {

        public BackCell(DataExcel grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// 插入行时进行的操作。
        /// 如果插入的行在此背景表格的行内（大于最小行，小于最大行）则重新设置单元格的背景单元格。
        /// </summary>
        /// <param name="row"></param>
        public void InSertRow(IRow row)
        {
            if (this.BeginCell.Row.Index <= row.Index && this.EndCell.Row.Index >= row.Index)
            {
                ReSetCellParent();
            }
        }

        /// <summary>
        /// 重新设置背景单元格。
        /// </summary>
        public void ReSetCellParent()
        {
            InitAllCells(this._firstcell, this._endcell, false);
        }

        /// <summary>
        /// 删除行时进行的操作。
        /// 如果删除的行在此背景表格的行内（大于最小行，小于最大行）则重新设置单元格的背景单元格。
        /// 如果删除的为第一行，则把当前第一个单元格，向下移动一格。
        /// 如果删除的为最后一行，则把当前最后单元格，向上移动一格。
        /// </summary>
        /// <param name="row"></param>
        public void DeleteRow(IRow row)
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
            ReSetCellParent();
            Refresh();

        }
        /// <summary>
        /// 插入列时进行的操作。
        /// 如果插入的列在此背景表格的列内（大于最小列，小于最大列）则重新设置单元格的背景单元格。
        /// </summary>
        /// <param name="column"></param>
        public void InSertColumn(IColumn column)
        {
            if (this.BeginCell.Column.Index >= column.Index && this.EndCell.Column.Index <= column.Index)
            {
                ReSetCellParent();
            }
        }

        /// <summary>
        /// 删除列时进行的操作。
        /// 如果删除的列在此背景表格的列内（大于最小列，小于最大列）则重新设置单元格的背景单元格。
        /// 如果删除的为第一列，则把当前第一个单元格，向右移动一格。
        /// 如果删除的为最后一列，则把当前最后单元格，向左移动一格。
        /// </summary>
        /// <param name="column"></param>
        public void DeleteColumn(IColumn column)
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
            ReSetCellParent();
            Refresh();
        }

        #region ICellRange 成员

        /// <summary>
        /// 关闭，删除当前背景单元格
        /// </summary>
        public void Close()
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

        private ICell _firstcell = null;
        public ICell BeginCell
        {
            get { return this._firstcell; }
            set
            {
                this._firstcell = value;
                if (this._endcell == null)
                {
                    this._endcell = value;
                }
                ReSetCellParent();
                Refresh();
            }
        }

        private ICell _endcell = null;
        public ICell EndCell
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
                ReSetCellParent();
                Refresh();
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

        #region IBackCell 成员

        private IBackCellCollection _BackCellCollection;

        public IBackCellCollection BackCellCollection
        {
            get
            {
                return _BackCellCollection;
            }
            set
            {
                _BackCellCollection = value;
            }
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        #endregion

        #region IBounds 成员

        private int _left = 0;
        public int Left
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
        public int Height
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

        public int Right
        {
            get
            {

                return _width + _left;
            }

        }
        public int Bottom
        {
            get
            {

                return _top + _height;
            }

        }
        private int _top = 0;
        public int Top
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
        public int Width
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

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(this._left, this._top, this._width, this._height);
            }
        }
        #endregion

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
                RetSelect(_selected);
                if (value)
                {
                    this.Grid.Selecteds.Add(this);
                }
            }
        }

        public void RetSelect(bool selected)
        {
            this.Grid.BeginReFresh();
            for (int i = this.BeginCell.Row.Index; i <= this.EndCell.Row.Index; i++)
            {
                for (int j = this.BeginCell.Column.Index; j <= this.EndCell.Column.Index; j++)
                {
                    this.Grid[i, j].Selected = selected;
                }
            }
            this.Grid.EndReFresh();
        }

        #endregion

        #region ISelectColor 成员
        private Color _SelectBackColor = Color.Empty;
        public Color SelectBackColor
        {
            get
            {
                return _SelectBackColor;
            }
            set
            {
                _SelectBackColor = value;
            }
        }

        #endregion

        #region ISelectColor 成员

        private Color _SelectForceColor = Color.Empty;
        public Color SelectForceColor
        {
            get
            {
                return _SelectForceColor;
            }
            set
            {
                _SelectForceColor = value;
            }
        }

        #endregion

        #region ISelectBorderColor 成员
        private System.Drawing.Color _SelectBorderColor = Color.Empty;
        public virtual System.Drawing.Color SelectBorderColor
        {
            get { return _SelectBorderColor; }
            set { _SelectBorderColor = value; }
        }

        #endregion

        #region ISelectBorderWidth 成员

        private float _SelectBorderWidth = 3f;
        public float SelectBorderWidth
        {
            get { return _SelectBorderWidth; }
            set { _SelectBorderWidth = value; }
        }

        #endregion

        #region ISetSize 成员

        public void Refresh()
        {
            Rectangle bounds = Rectangle.Empty;
            Rectangle rect = DataExcel.GetRect(this.BeginCell, this.EndCell, ref bounds);

            this.Top = rect.Top;
            this.Left = rect.Left;
            this.Width = rect.Width;
            this.Height = rect.Height;
        }


        #endregion

        #region 系统属性

        private bool _AutoMultiline = false;
        [DefaultValue(true)]
        public virtual bool AutoMultiline
        {
            get { return this._AutoMultiline; }

            set { this._AutoMultiline = value; }
        }

        #endregion

        #region IToString 成员
        public override string ToString()
        {
            return this.Text;
        }
#if Test
        public string AText
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


        #endregion

        #region IControlColor 成员
        private Color _forecolor = Color.Empty;
        public virtual Color ForeColor
        {
            get
            {
                return _forecolor;
            }
            set
            {
                _forecolor = value;
            }
        }
        private Color _backcolor = Color.Empty;
        public virtual Color BackColor
        {
            get
            {
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }

        #endregion

        #region IText 成员
        private string _text = string.Empty;
        public virtual string Text
        {
            get { return this._text; }

            set
            {
                this._text = value;
            }
        }

        #endregion

        #region ICurrentRow 成员

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

        #region IDraw 成员
        private int _lastdrawinde = -1;
        public virtual int LastDrawIndex { get; set; }
        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        { 
            return false;
        }

        public void DrawRect(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool print, PrintArgs printe)
        {

            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            if (bounds != Rectangle.Empty)
            {
                Rectangle clip = bounds;
                if (print)
                {
                    if (printe != null)
                    {
                        if (printe.Clip != Rectangle.Empty)
                        {
                            clip = Rectangle.Intersect(bounds, printe.Clip);
                        }
                    }
                }
                else
                {
                    clip = Rectangle.Intersect(bounds, Rectangle.Round(g.Graphics.ClipBounds));
                }
                g.Graphics.SetClip(clip);

            }
            BeforeDrawCellBackArgs e = new BeforeDrawCellBackArgs(g, this);
            this.Grid.OnBeforeDrawCellBack(this, e);
            if (e.Cancel)
            {
                goto LabelEnd;
            }

            if (this.OwnBackCell != null)
            {
                if (print)
                {
                    this.OwnBackCell.PrintValue(printe, value);
                    goto LabelEnd;

                }
                else
                {
                    if (this.OwnBackCell.OnDraw(this, g))
                    {
                        goto LabelEnd;
                    }
                }
            }

            if (this.OwnEditControl != null)
            {
                if (print)
                {
                    if (this.OwnEditControl.PrintCellBack(this, printe))
                    {
                        goto LableOnDraw;
                    }
                }
                else
                {
                    if (this.OwnEditControl.DrawCellBack(this, g))
                    {
                        goto LableOnDraw;
                    }
                }
            }
            if (print)
            {
                if (!this.IsPrintBackColor)
                {
                    goto LableDrawBackColor;
                }
            }
            DrawBackColor(g, bounds);
        LableDrawBackColor:
            if (print)
            {
                if (!this.IsPrintBackImage)
                {
                    goto LableDrawBackImage;
                }
            }
        LableDrawBackImage:
            DrawBackImage(g, bounds);

        LableOnDraw:
            if (this.OwnEditControl != null)
            {
                if (print)
                {
                    if (this.OwnEditControl.PrintValue(this, printe, bounds, value))
                    {
                        goto LabelEnd;
                    }
                }
                else
                {
                    if (this.OwnEditControl.DrawCell(this, g))
                    {
                        goto LabelEnd;
                    }
                }
            }
            DrawCell(g, bounds, value);
            DrawBorder(g, bounds);

            DrawCellArgs DrawCellArgs = new DrawCellArgs(g, this);
            this.Grid.OnDrawCell(DrawCellArgs);
        LabelEnd:
            g.Graphics.Restore(gs);
        }

        private void DrawCell(Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
 
        }
        private void DrawCellBack(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            DrawBackColor(g, bounds);
            DrawBackImage(g, bounds);
        }
        private bool DrawBackImage(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            Image backimage = null;
            Rectangle rect = bounds;
            if (this.Grid.FocusedCell == this)
            {
                backimage = this.FocusImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(FocusImageSizeMode, backimage, bounds);
            }

            if (this.ReadOnly)
            {
                backimage = this.ReadOnlyImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(ReadOnlyImageSizeMode, backimage, bounds);
            }
            if (this.Rect.Contains (g.ClientPoint))
            {
                backimage = this.MouseOverImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(MouseOverImageSizeMode, backimage, bounds);
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell ==this)
            {
                backimage = this.MouseDownImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(MouseDownImageSizeMode, backimage, bounds);
            }
            if (backimage == null)
            {
                backimage = this.BackImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(BackImgeSizeMode, backimage, bounds);
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
                backcolor = this.BeginCell.FocusBackColor;
            }
            
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell ==this)
            {
                backcolor = this.BeginCell.MouseDownBackColor;
            }
            if (this.Rect.Contains (g.ClientPoint))
            {
                backcolor = this.BeginCell.MouseOverBackColor;
            }
            if (backcolor == Color.Empty)
            {
                backcolor = this.BackColor;
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

        private void DrawCell(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            StringFormat sf = StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, this.DirectionVertical);
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

        public virtual void DrawBorder(Feng.Drawing.GraphicsObject g)
        {
            DrawBorder(g, this.Rect);
        }

        public virtual void DrawBorder(Feng.Drawing.GraphicsObject g, Rectangle bounds)
        {
            if (BorderStyle != null)
            {

                DrawCellBorderArgs DrawCellBorderArgs = new DrawCellBorderArgs(g, this);
                this.Grid.OnDrawCellBorder(DrawCellBorderArgs);
                if (!DrawCellBorderArgs.Handled)
                {
                    //System.Drawing.Drawing2D.GraphicsState gs = g.Save();
                    //g.SetClip(bounds);
                    DrawLine(g, bounds);
                    //g.Restore(gs);
                }
            }
        }

        private void DrawLine(Feng.Drawing.GraphicsObject g, Rectangle bounds)
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



        #region IPrintBorder 成员

        public void PrintBorder(PrintArgs e)
        {
            Page page = e.CurrentPage as Page;
            if (!page.PrintBorderList.Contains(this))
            {
                Rectangle bounds = Rectangle.Empty;
                Rectangle rect = DataExcel.GetPrintBounds(page.StartPosition
                    , this.BeginCell, this.EndCell
                    , ref bounds
                    , page.StartColumnIndex, page.EndColumnIndex
                    , page.StartRowIndex, page.EndRowIndex);
                Feng.Drawing.GraphicsObject gob = e.Graphic;
                DrawBorder(gob, rect);
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
                //Rectangle bounds = Rectangle.Empty;
                //Rectangle rect = this.Rect;
                //rect.Location = e.CurrentLocation;
                //rect = DataExcel.GetPrintBounds(page.StartPosition
                //    , this.BeginCell, this.EndCell
                //    , ref bounds
                //    , page.StartColumnIndex, page.EndColumnIndex
                //    , page.StartRowIndex, page.EndRowIndex);
                ////bounds.Location = e.CurrentLocation;

                Rectangle rect = this.Rect;
                rect.Location = e.CurrentLocation;
                rect = DataExcel.GetPrintBounds(e.CurrentLocation
                    , this.BeginCell, this.EndCell

                    , page.StartColumnIndex, page.EndColumnIndex
                    , page.StartRowIndex, page.EndRowIndex);

                Feng.Drawing.GraphicsObject gob = e.Graphic;
                this.DrawRect(gob, rect, this.Value, true, e);


                page.PrintList.Add(this);
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
                //Rectangle clip = Rectangle.Empty;
                //Rectangle bounds = this.Rect;
                //bounds.Location = e.CurrentLocation;
                //bounds = DataExcel.GetPrintBounds(page.StartPosition
                //    , this.BeginCell, this.EndCell
                //    , ref clip
                //    , page.StartColumnIndex, page.EndColumnIndex
                //    , page.StartRowIndex, page.EndRowIndex);
                ////bounds.Location = e.CurrentLocation;

                Rectangle rect = this.Rect;
                rect.Location = e.CurrentLocation;
                rect = DataExcel.GetPrintBounds(e.CurrentLocation
                    , this.BeginCell, this.EndCell

                    , page.StartColumnIndex, page.EndColumnIndex
                    , page.StartRowIndex, page.EndRowIndex);

                Feng.Drawing.GraphicsObject gob = e.Graphic;

                this.DrawRect(gob, rect, value, true, e);
                page.PrintList.Add(this);
            }
            return true;
        }

        #endregion

        #region IValue 成员
        private object _value = null;
        public virtual object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        #endregion

        #region IExpressionText 成员
        private string _Expression = string.Empty;
        public virtual string Expression
        {
            get
            {
                return this._Expression;
            }
            set
            {
                this._Expression = value;
            }
        }

        #endregion

        #region IAutoExecuteExpress 成员
        private bool _AutoExecuteExpress = true;
        public virtual bool AutoExecuteExpress
        {
            get
            {
                return this._AutoExecuteExpress;
            }
            set
            {
                this._AutoExecuteExpress = value;
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
        private StringAlignment _HorizontalAlignment = StringAlignment.Center;
        public virtual StringAlignment HorizontalAlignment
        {
            get
            {
                return this._HorizontalAlignment;
            }
            set
            {
                this._HorizontalAlignment = value;
            }
        }

        #endregion

        #region IVerticalAlignment 成员
        private StringAlignment _VerticalAlignment = StringAlignment.Center;
        public virtual StringAlignment VerticalAlignment
        {
            get
            {
                return this._VerticalAlignment;
            }
            set
            {
                this._VerticalAlignment = value;
            }
        }

        #endregion

        #region ICellType 成员
        private CellType _CellType = CellType.Default;
        public virtual CellType CellType
        {
            get
            {
                return this._CellType;
            }
            set
            {
                this._CellType = value;
            }
        }

        #endregion

        #region IFont 成员
        private Font _font = null;
        public virtual Font Font
        {
            get
            {
                if (_font == null)
                {
                    return this.BeginCell.Font;
                }
                return _font;
            }
            set
            {
                this._font = value;
            }
        }

        #endregion

        #region IBorderSetting 成员

        private CellBorderStyle _borderStyle;
        public virtual CellBorderStyle BorderStyle
        {
            get
            {
                if (_borderStyle == null)
                {
                    return this.BeginCell.BorderStyle;
                }
                return _borderStyle;
            }
            set
            {
                this._borderStyle = value;
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
        private FormatType _FormatType = FormatType.Null;
        public virtual FormatType FormatType
        {
            get
            {
                return this._FormatType;
            }
            set
            {
                this._FormatType = value;
            }
        }
        private string _FormatString = string.Empty;
        public virtual string FormatString
        {
            get
            {
                return this._FormatString;
            }
            set
            {
                this._FormatString = value;
            }
        }

        #endregion

        #region IUpdateVersion 成员
        private int _UpdateVersion = 0;
        public virtual int UpdateVersion
        {
            get
            {
                return this._UpdateVersion;
            }
            set
            {
                this._UpdateVersion = value;
            }
        }

        #endregion

        #region IOwnEditControl 成员
        private ICellEditControl _OwnEditControl = null;
        public virtual ICellEditControl OwnEditControl
        {
            get
            {
                return this._OwnEditControl;
            }
            set
            {
                this._OwnEditControl = value;
            }
        }

        #endregion

        #region IEndable 成员
        private bool _Enable = true;
        public virtual bool Enable
        {
            get
            {
                return this._Enable;
            }
            set
            {
                this._Enable = value;
            }
        }

        #endregion

        #region IInhertReadOnly 成员
        private bool _inhertreadonly = true;
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
        private bool _ReadOnly = true;
        public virtual bool ReadOnly
        {
            get
            {
                return this._ReadOnly;
            }
            set
            {
                this._ReadOnly = value;
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
            return false;
        }

        public virtual bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseEnter(this, e, ve))
                {
                    return true;
                }
            }
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
            return false;
        }

        public virtual bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {

            if (!this.InEdit)
            {
                this.InitEdit(this);
            }
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseDoubleClick(this, e, ve))
                {
                    return true;
                }
            }
            return true;
        }

        public virtual bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnMouseClick(this, e, ve))
                {
                    return true;
                }
            }
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
            return false;
        }

        public virtual bool OnClick(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnClick(this, e, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            if (e.Alt)
            {
                return false;
            }
            if (e.Control)
            {
                return false;
            }
            if (e.Shift)
            {
                return false;
            }
            if (!this.InEdit)
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
            return true;

        }

        public virtual bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.InEdit)
                {
                    if (this.OwnEditControl.OnKeyPress(this, e, ve))
                    {
                        return true;
                    }
                    char s = e.KeyChar;
                    if (char.IsUpper(s))
                    {
                        SendKey.Send("{CAPSLOCK}(" + s + ")");
                    }
                    else
                    {
                        SendKey.Send(s.ToString());
                    }
                }
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
            return false;
        }

        public virtual bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnDoubleClick(this, e, ve))
                {
                    return true;
                }
            }
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
                if (this.OwnEditControl.OnProcessKeyEventArgs(this, ref   m, ve))
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
                if (this.OwnEditControl.OnProcessKeyMessage(this, ref   m, ve))
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
                if (this.OwnEditControl.OnProcessKeyPreview(this, ref   m, ve))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                if (this.OwnEditControl.OnWndProc(this, ref   m, ve))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region IChecked 成员
        private bool _Checked = false;
        public virtual bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
            }
        }

        #endregion

        #region ICaption 成员
        private string _Caption = string.Empty;
        public virtual string Caption
        {
            get
            {
                return this._Caption;
            }
            set
            {
                this._Caption = value;
            }
        }

        #endregion

        #region IInitEdit 成员

        public virtual bool InitEdit(object obj)
        {
            if (this._indedit)
            {
                return false;
            }
            this.Grid.AddEdit(this);
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
            if (!this.Enable)
            {
                return false;
            }
            this.Grid.BeginReFresh();
            if (this.OwnEditControl == null)
            {
                this.OwnEditControl = this.Grid.DefaultEdit;
            }

            this.OwnEditControl.InitEdit(this);

            _indedit = true;

            this.Grid.EndReFresh();
            return _indedit;
        }

        #endregion

        #region IClear 成员

        public virtual void Clear()
        {

        }

        #endregion

        #region ISetAllDeafultBoarder 成员

        public virtual void SetSelectCellBorderBorderOutside()
        {
            this.BeginCell.SetSelectCellBorderBorderOutside();
        }

        #endregion

        #region IEndEdit 成员

        public virtual void EndEdit()
        { 
            if (!_indedit)
            {
                return;
            }
            _indedit = false;
            this.Grid.BeginReFresh();

            if (this.OwnEditControl != null)
            {
                this.OwnEditControl.EndEdit();
            }

            this.Grid.EndReFresh();
        }

        #endregion

        #region IInEdit 成员
        private bool _indedit = false;
        public virtual bool InEdit
        {
            get
            {
                return this._indedit;
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

        public virtual IMergeCell OwnMergeCell
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

        #endregion

        #region IBackImage 成员
        private Bitmap _BackImge = null;
        public virtual Bitmap BackImage
        {
            get
            {
                return this._BackImge;
            }
            set
            {
                this._BackImge = value;
            }
        }

        #endregion

        #region ITextDirection 成员
        private bool _DirectionVertical = false;
        public virtual bool DirectionVertical
        {
            get
            {
                return this._DirectionVertical;
            }
            set
            {
                this._DirectionVertical = value;
            }
        }

        #endregion

        #region IIsMergeCell 成员

        public virtual bool IsMergeCell
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region IDateTime 成员
        private DateTime _DateTime = DateTime.MinValue;
        public virtual DateTime DateTime
        {
            get
            {
                return this._DateTime;
            }
            set
            {
                this._DateTime = value;
            }
        }

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

        #region IIsDataTableCell 成员

        public virtual bool IsDataTableCell
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        #endregion

        #region IDrawGridLine 成员

        public virtual void DrawGridLine(Feng.Drawing.GraphicsObject g)
        {

        }

        #endregion

        #region IMouseOverImage 成员
        private Bitmap _MouseOverImage = null;
        public virtual Bitmap MouseOverImage
        {
            get
            {
                return this._MouseOverImage;
            }
            set
            {
                this._MouseOverImage = value;
            }
        }

        #endregion

        #region IMouseDownImage 成员
        private Bitmap _MouseDownImage = null;
        public virtual Bitmap MouseDownImage
        {
            get
            {
                return this._MouseDownImage;
            }
            set
            {
                this._MouseDownImage = value;
            }
        }

        private Bitmap _MouseUpImage = null;
        public virtual Bitmap MouseUpImage
        {
            get
            {
                return this._MouseUpImage;
            }
            set
            {
                this._MouseUpImage = value;
            }
        }
        #endregion

        #region IDisableImage 成员
        private Bitmap _DisableImage = null;
        public virtual Bitmap DisableImage
        {
            get
            {
                return this._DisableImage;
            }
            set
            {
                this._DisableImage = value;
            }
        }

        #endregion

        #region IReadOnlyImage 成员
        private Bitmap _ReadOnlyImage = null;
        public virtual Bitmap ReadOnlyImage
        {
            get
            {
                return this._ReadOnlyImage;
            }
            set
            {
                this._ReadOnlyImage = value;
            }
        }

        #endregion

        #region IFocusImage 成员
        private Bitmap _FocusImage = null;
        public virtual Bitmap FocusImage
        {
            get
            {
                return this._FocusImage;
            }
            set
            {
                this._FocusImage = value;
            }
        }

        #endregion

        #region IMouseOverBackColor 成员
        private Color _MouseOverBackColor = Color.Empty;
        public virtual Color MouseOverBackColor
        {
            get
            {
                return this._MouseOverBackColor;
            }
            set
            {
                this._MouseOverBackColor = value;
            }
        }

        #endregion

        #region IMouseDownBackColor 成员
        private Color _MouseDownBackColor = Color.Empty;
        public virtual Color MouseDownBackColor
        {
            get
            {
                return this._MouseDownBackColor;
            }
            set
            {
                this._MouseDownBackColor = value;
            }
        }
        private Color _MouseUpBackColor = Color.Empty;
        public virtual Color MouseUpBackColor
        {
            get
            {
                return this._MouseUpBackColor;
            }
            set
            {
                this._MouseUpBackColor = value;
            }
        }
        #endregion

        #region IDisableBackColor 成员
        private Color _DisableBackColor = Color.Empty;
        public virtual Color DisableBackColor
        {
            get
            {
                return this._DisableBackColor;
            }
            set
            {
                this._DisableBackColor = value;
            }
        }

        #endregion

        #region IReadOnlyBackColor 成员
        private Color _ReadOnlyBackColor = Color.Empty;
        public virtual Color ReadOnlyBackColor
        {
            get
            {
                return this._ReadOnlyBackColor;
            }
            set
            {
                this._ReadOnlyBackColor = value;
            }
        }

        #endregion

        #region IFocusBackColor 成员
        private Color _FocusBackColor = Color.Empty;
        public virtual Color FocusBackColor
        {
            get
            {
                return this._FocusBackColor;
            }
            set
            {
                this._FocusBackColor = value;
            }
        }

        #endregion

        #region IMouseOverForeColor 成员
        private Color _MouseOverForeColor = Color.Empty;
        public virtual Color MouseOverForeColor
        {
            get
            {
                return this._MouseOverForeColor;
            }
            set
            {
                this._MouseOverForeColor = value;
            }
        }

        #endregion

        #region IMouseDownForeColor 成员
        private Color _MouseDownForeColor = Color.Empty;
        public virtual Color MouseDownForeColor
        {
            get
            {
                return this._MouseDownForeColor;
            }
            set
            {
                this._MouseDownForeColor = value;
            }
        }


        private Color _MouseUpForeColor = Color.Empty;
        public virtual Color MouseUpForeColor
        {
            get
            {
                return this._MouseUpForeColor;
            }
            set
            {
                this._MouseUpForeColor = value;
            }
        }
        #endregion

        #region IDisableForeColor 成员
        private Color _DisableForeColor = Color.Empty;
        public virtual Color DisableForeColor
        {
            get
            {
                return this._DisableForeColor;
            }
            set
            {
                this._DisableForeColor = value;
            }
        }

        #endregion

        #region IReadOnlyForeColor 成员
        private Color _ReadOnlyForeColor = Color.Empty;
        public virtual Color ReadOnlyForeColor
        {
            get
            {
                return this._ReadOnlyForeColor;
            }
            set
            {
                this._ReadOnlyForeColor = value;
            }
        }

        #endregion

        #region IFocusForeColor 成员
        private Color _FocusForeColor = Color.Empty;
        public virtual Color FocusForeColor
        {
            get
            {
                return this._FocusForeColor;
            }
            set
            {
                this._FocusForeColor = value;
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
            _ContensWidth = Feng.Utils.ConvertHelper.ToInt32(Size.Width);
            _ContensHeigth = Feng.Utils.ConvertHelper.ToInt32(Size.Height);

        }

        #endregion

        #region IPrintFooter 成员

        public virtual void PrintFooter(PrintArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBindingItem 成员

        public virtual string BindingItem
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ITableCellPrinted 成员
        private bool _IsTableCellPrinted = false;
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

        #region IPrintHeader 成员

        public virtual void PrintHeader(PrintArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISave 成员

        public virtual void Save(Feng.Excel.IO.BinaryWriter stream)
        {
            stream.Write(this.Data);
        }

        #endregion

        #region IRead 成员

        public virtual void Read(Feng.Excel.IO.BinaryReader stream, out int count)
        {
            count = 0;
            while (true)
            {
                if (stream.IsEnd())
                {
                    return;
                }

                ushort i = stream.ReadUInt16();
                byte btype = stream.ReadByte();
                switch (i)
                {
                    case 1:
                        this._AutoExecuteExpress = stream.ReadBoolean();
                        break;
                    case 2:
                        stream.ReadBoolean();
                        break;
                    case 3:
                        this._AutoMultiline = stream.ReadBoolean();
                        break;
                    case 5:
                        this._backcolor = stream.ReadColor();
                        break;
                    case 6:
                        this._BackImge = stream.ReadBitmap();
                        break;
                    //case 7:
                    //    this.BorderStyle = stream.ReadBorderStyle();
                    //    break;
                    case 8:
                        this._Caption = stream.ReadString();
                        break;
                    case 12:
                        this._Checked = stream.ReadBoolean();
                        break;
                    case 13:
                        this._ContensHeigth = stream.ReadInt32();
                        break;
                    case 14:
                        this._ContensWidth = stream.ReadInt32();
                        break;
                    case 15:
                        this._DateTime = stream.ReadDateTime();
                        break;
                    case 16:
                        this._DirectionVertical = stream.ReadBoolean();
                        break;
                    case 17:
                        this._DisableBackColor = stream.ReadColor();
                        break;
                    case 18:
                        this._DisableForeColor = stream.ReadColor();
                        break;
                    case 19:
                        this._DisableImage = stream.ReadBitmap();
                        break;
                    case 20:
                        this._displaymember = stream.ReadString();
                        break;
                    case 21:
                        this._Enable = stream.ReadBoolean();
                        break;
                    case 23:
                        this._Expression = stream.ReadString();
                        break;
                    case 24:
                        int frowindex = stream.ReadInt32();
                        int fcolumnindex = stream.ReadInt32();
                        this.BeginCell = this.Grid[frowindex, fcolumnindex];
                        break;
                    case 22:
                        int erowindex = stream.ReadInt32();
                        int ecolumnindex = stream.ReadInt32();
                        this.EndCell = this.Grid[erowindex, ecolumnindex];
                        break;
                    case 25:
                        this._font = stream.ReadFont();
                        break;
                    case 26:
                        this._forecolor = stream.ReadColor();
                        break;
                    case 27:
                        this._FormatString = stream.ReadString();
                        break;
                    case 29:
                        this._FocusBackColor = stream.ReadColor();
                        break;
                    case 30:
                        this._FocusForeColor = stream.ReadColor();
                        break;
                    case 31:
                        this._FocusImage = stream.ReadBitmap();
                        break;
                    case 32:
                        this._freshversion = stream.ReadInt32();
                        break;
                    case 34:
                        this._height = stream.ReadInt32();
                        break;
                    case 36:
                        this._indedit = stream.ReadBoolean();
                        break;
                    case 37:
                        this._inhertreadonly = stream.ReadBoolean();
                        break;
                    case 38:
                        this._IsTableCellPrinted = stream.ReadBoolean();
                        break;
                    case 41:
                        this._lastdrawinde = stream.ReadInt32();
                        break;
                    case 42:
                        this._left = stream.ReadInt32();
                        break;
                    case 43:
                        this._MouseDownBackColor = stream.ReadColor();
                        break;
                    case 44:
                        this._MouseDownForeColor = stream.ReadColor();
                        break;
                    case 45:
                        this._MouseDownImage = stream.ReadBitmap();
                        break;
                    case 46:
                        this._MouseOverBackColor = stream.ReadColor();
                        break;
                    case 47:
                        this._MouseOverForeColor = stream.ReadColor();
                        break;
                    case 48:
                        this._MouseOverImage = stream.ReadBitmap();
                        break;
                    case 51:
                        this._ReadOnly = stream.ReadBoolean();
                        break;
                    case 52:
                        this._ReadOnlyBackColor = stream.ReadColor();
                        break;
                    case 53:
                        this._ReadOnlyForeColor = stream.ReadColor();
                        break;
                    case 54:
                        this._ReadOnlyImage = stream.ReadBitmap();
                        break;
                    case 55:
                        this._SelectBackColor = stream.ReadColor();
                        break;
                    case 56:
                        this._SelectBorderColor = stream.ReadColor();
                        break;
                    case 57:
                        this._SelectBorderWidth = stream.ReadSingle();
                        break;
                    case 58:
                        this._selected = stream.ReadBoolean();
                        break;
                    case 59:
                        this._SelectForceColor = stream.ReadColor();
                        break;
                    case 60:
                        this._text = stream.ReadString();
                        break;
                    case 61:
                        this._top = stream.ReadInt32();
                        break;
                    case 62:
                        this._UpdateVersion = stream.ReadInt32();
                        break;
                    case 64:
                        this._valuemember = stream.ReadString();
                        break;
                    case 66:
                        this._width = stream.ReadInt32();
                        break;
                    case 67:
                        this._BackImgeSizeMode = (ImageLayout)stream.ReadInt32();
                        break;

                    default:
                        DataExcel.Read(stream, btype);
                        break;
                }

            }
        }
        public virtual void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        #endregion

        #region IDataStruct 成员
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

                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {

                    bw.Write(1, this._AutoExecuteExpress);
                    bw.Write(2, false);
                    bw.Write(3, this._AutoMultiline);
                    bw.Write(5, this._backcolor);
                    bw.Write(6, this._BackImge);
                    //bw.Write(7, this.BorderStyle);
                    bw.Write(8, this._Caption);
                    bw.Write(12, this._Checked);
                    bw.Write(13, this._ContensHeigth);
                    bw.Write(14, this._ContensWidth);
                    bw.Write(15, this._DateTime);
                    bw.Write(16, this._DirectionVertical);
                    bw.Write(17, this._DisableBackColor);
                    bw.Write(18, this._DisableForeColor);
                    bw.Write(19, this._DisableImage);
                    bw.Write(20, this._displaymember);
                    bw.Write(21, this._Enable);
                    bw.Write(23, this._Expression);
                    bw.Write(24, this._firstcell.Row.Index);
                    bw.Write(this._firstcell.Column.Index);
                    bw.Write(22, this._endcell.Row.Index);
                    bw.Write(this._endcell.Column.Index);
                    bw.Write(25, this._font);
                    bw.Write(26, this._forecolor);
                    bw.Write(27, this._FormatString);
                    bw.Write(29, this._FocusBackColor);
                    bw.Write(30, this._FocusForeColor);
                    bw.Write(31, this._FocusImage);
                    bw.Write(32, this._freshversion);
                    bw.Write(34, this._height);
                    bw.Write(36, this._indedit);
                    bw.Write(37, this._inhertreadonly);
                    bw.Write(38, this._IsTableCellPrinted);
                    bw.Write(41, this._lastdrawinde);
                    bw.Write(42, this._left);
                    bw.Write(43, this._MouseDownBackColor);
                    bw.Write(44, this._MouseDownForeColor);
                    bw.Write(45, this._MouseDownImage);
                    bw.Write(46, this._MouseOverBackColor);
                    bw.Write(47, this._MouseOverForeColor);
                    bw.Write(48, this._MouseOverImage);
                    bw.Write(51, this._ReadOnly);
                    bw.Write(52, this._ReadOnlyBackColor);
                    bw.Write(53, this._ReadOnlyForeColor);
                    bw.Write(54, this._ReadOnlyImage);
                    bw.Write(55, this._SelectBackColor);
                    bw.Write(56, this._SelectBorderColor);
                    bw.Write(57, this._SelectBorderWidth);
                    bw.Write(58, this._selected);
                    bw.Write(59, this._SelectForceColor);
                    bw.Write(60, this._text);
                    bw.Write(61, this._top);
                    bw.Write(62, this._UpdateVersion);
                    bw.Write(64, this._valuemember);
                    bw.Write(66, this._width);
                    bw.Write(67, (int)this._BackImgeSizeMode);
                    data.Data = bw.GetData();
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

        #region IOwnBackCell 成员

        public virtual IBackCell OwnBackCell
        {
            get;
            set;
        }

        #endregion

        #region IShowSelectBorder 成员
        private bool _ShowSelectBorder = false;
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
            this._text = text;
        }

        #endregion

        #region IGetFocused 成员

        public virtual void GetFocused()
        {
            //if ((this.EditMode & EditMode.Focused) == EditMode.Focused)
            //{
            //    if ((this.EditMode & EditMode.DoubleClick) == EditMode.DoubleClick)
            //    {
            //        if (!this.InEdit)
            //        {
            //            this.InitEdit();
            //        }
            //    }
            //}
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

        private ImageLayout _BackImgeSizeMode = ImageLayout.Stretch;
        public ImageLayout BackImgeSizeMode
        {
            get
            {
                return _BackImgeSizeMode;
            }
            set
            {
                this._BackImgeSizeMode = value;
            }
        }

        #endregion

        #region IFunctionBorder 成员

        public virtual bool FunctionBorder
        {
            get
            {
                return false;
            }
            set
            {

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

        #region IDisposable 成员

        public void InitAllCells(ICell fcell, ICell ecell, bool clear)
        {
            int start = System.Math.Min(fcell.Row.Index, ecell.Row.Index);
            int count = System.Math.Max(fcell.Row.Index, ecell.Row.Index);

            int cstart = System.Math.Min(fcell.Column.Index, ecell.Column.Index);
            int ccount = System.Math.Max(fcell.Column.Index, ecell.Column.Index);
            for (int i = start; i <= count; i++)
            {
                for (int j = cstart; j <= ccount; j++)
                {
                    if (clear)
                    {
                        this.Grid[i, j].OwnBackCell = null;
                    }
                    else
                    {
                        this.Grid[i, j].OwnBackCell = this;
                    }
                }
            }
        }
        public void Dispose()
        {
            InitAllCells(this._firstcell, this._endcell, true);
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
        private string _id = string.Empty;
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

        private object _tag = null;

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

        public void SendMessage(Message m)
        {

        }

        public string DefaultValue
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
 
        public virtual void ReadDataStruct(DataStruct data)
        {
            int count = 0;

            using (Feng.Excel.IO.BinaryReader br = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                Read(br, out count);
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
        public bool AllowCopy
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

        public string ToolTip { get; set; }

        public bool OnDrawBack(object sender, GraphicsObject g)
        {
            try
            {
                ///////////////////////////代码加在中间 
                if (_lastdrawinde == this.Grid.FreshVersion)
                {
                    return false;
                }
                _lastdrawinde = this.Grid.FreshVersion;
                DrawRect(g, this.Rect, this.Text, false, null);
                ///////////////////////////

            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except",ex); 
            }
            return false;
        }

        public bool PrintBack(PrintArgs e)
        {
            return false;
        }


        public virtual string Url
        {
            get
            {
                return string.Empty;
            }
            set
            { 
            }
        }

        public int ExpressionIndex { get ; set ; }


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

        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(YesNoInhert.Inherit)]
        public virtual YesNoInhert AllowInputExpress
        {
            get { return this.BeginCell.AllowInputExpress; }
            set { this.BeginCell.AllowInputExpress = value; }
        }

        public string Text1 { get ; set ; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
    }
}
