#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Feng.Drawing;
using Feng.Print;
using Feng.Enums;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Extend
{
    [Serializable]
    public class ExtendCell : BaseExtendCell
    {
        public ExtendCell(DataExcel grid)
        {
            this.Width = 300;
            this.Height = 200;
            _grid = grid;
        }

        #region IExpandCellRelativePosition 成员
        private IRow _TopRow = null;
        private IColumn _LeftColumn = null;
        public virtual IRow TopRow
        {
            get { return _TopRow; }
            set { _TopRow = value; }
        }
        public virtual IColumn LeftColumn
        {
            get { return _LeftColumn; }
            set { this._LeftColumn = value; }
        }

        private IRow _bottomRow = null;

        public virtual IRow BottomRow
        {
            get { return _bottomRow; }
            set { _bottomRow = value; }
        }
        public virtual IColumn RightColumn
        {
            get { return _LeftColumn; }
            set { this._LeftColumn = value; }
        }


        private int _topdistance = 0;
        private int _leftdistance = 0;
        public virtual int TopDistance
        {
            get { return _topdistance; }
            set { _topdistance = value; }
        }
        public virtual int LeftDistance
        {
            get { return _leftdistance; }
            set { _leftdistance = value; }
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid;

        public override DataExcel Grid
        {
            get { return _grid; }
        }

        #endregion

        #region IBounds 成员

        private int _left = 0;
        public override int Left
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
        public override int Height
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

        public override int Right
        {
            get
            {

                return _width + _left;
            }

        }
        public override int Bottom
        {
            get
            {
                return _top + _height;
            }

        }
        private int _top = 0;
        public override int Top
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
        public override int Width
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

        public override Rectangle Rect
        {
            get
            {
                return new Rectangle(this._left, this._top, this._width, this._height);
            }
        }

        public enum EnumPosition
        {
            RELATIVE,
            FIXED,
            ABSOLUTE,
            STICKY,
        }

        private EnumPosition positiontype = EnumPosition.FIXED;
        private EnumPosition PositionType { get; set; }
        #endregion

        #region IMergeCell 成员

        private IExtendCellCollection _TextCellCollection;

        public virtual IExtendCellCollection TextCellCollection
        {
            get
            {
                return _TextCellCollection;
            }
            set
            {
                _TextCellCollection = value;
            }
        }

        #endregion

        #region IDraw 成员

        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (!this.IsVisibleInGrid)
            {
                return false;
            }
            if (this.BackColor != Color.Empty)
            {
                GraphicsHelper.FillRectangleLinearGradient(g.Graphics, this.BackColor, this.Rect, true, 1);
                //using (SolidBrush sb = new SolidBrush(this.BackColor))
                //{
                //    g.FillRectangle(sb, this.Left, this.Top, this.Width, this.Height);
                //}
            }
#if DEBUG2
       
            string str = System.IO.File.ReadAllText(@"..\..\ExceptionString.cs");
            if (this.BaseText != null)
            {
                this.BaseText.OnDraw(g);
            }
            else
            {
                g.DrawString(str, this.Grid.Font, Brushes.HotPink, this.Rect);
            }
#endif
            Rectangle rectf = this.Rect;
            if (this.Selected || _SizeChangMode != SizeChangMode.Null)
            {
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddRectangle(rectf);
                    rectf.Inflate(this.SelectBorderWidth, this.SelectBorderWidth);
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
            return false;
        }
        public virtual Size SizeRect
        {
            get { return new Size(6, 6); }
        }

        #endregion

        #region ITextCell 成员
        //StringBuilder _StringBuilder;
        //public StringBuilder StringBuilder
        //{
        //    get
        //    {
        //        return _StringBuilder;
        //    }
        //    set
        //    {
        //        _StringBuilder = value;
        //    }
        //}

        #endregion

        #region ISelected 成员
        private bool _selected = false;
        public override bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        #endregion

        #region IMouseDownPoint 成员
        private Point _MouseDownPoint = Point.Empty;
        public override Point MouseDownPoint
        {
            get
            {
                return _MouseDownPoint;
            }
            set
            {
                _MouseDownPoint = value;
            }
        }

        #endregion

        #region IMouseDownSize 成员
        private Size _MouseDownSize = Size.Empty;
        public override Size MouseDownSize
        {
            get
            {
                return _MouseDownSize;
            }
            set
            {
                _MouseDownSize = value;
            }
        }

        #endregion

        #region IReSetRowColumn 成员

        private int ReSetColumnDistance(ref IColumn col, ref int dleft, Point pt)
        {
            int distance = 0;
            int left = pt.X - dleft;
            if (left > 0)
            {
                distance = left;
            }
            else
            {
                if (col.Index == 1)
                {
                    distance = left;
                    return distance;
                }
                col = this.Grid.Columns[col.Index - 1];
                if (col != null)
                {
                    dleft = dleft - col.Width;
                    distance = ReSetColumnDistance(ref col, ref dleft, pt);
                }
            }
            return distance;

        }

        private int ReSeRowDistance(ref IRow col, ref int dtop, Point pt)
        {
            int distance = 0;
            int top = pt.Y - dtop;
            if (top > 0)
            {
                distance = top;
            }
            else
            {
                if (col.Index == 1)
                {
                    distance = top;
                    return distance;
                }

                col = this.Grid.Rows[col.Index - 1];
                dtop = dtop - col.Height;
                distance = ReSeRowDistance(ref col, ref dtop, pt);
            }
            return distance;

        }

        public override void ReSetRowColumn(Point pt)
        {
            Point temppt = pt;
            for (int i = 0; i < this.Grid.AllVisibleColumnCount; i++)
            {
                int index = this.Grid.FirstDisplayedColumnIndex + i;

                IColumn col = this.Grid.Columns[index];
                if (col != null)
                {
                    if (pt.X < this.Grid.LeftSideWidth)
                    {
                        int dleft = this.Grid.LeftSideWidth;

                        int dis = ReSetColumnDistance(ref  col, ref dleft, pt);
                        this.LeftColumn = col;
                        this.LeftDistance = dis;
                        break;
                    }
                    else
                    {

                        if (col.Left <= temppt.X && col.Right > temppt.X)
                        {
                            this.LeftColumn = col;
                            this.LeftDistance = pt.X - col.Left;
                            break;
                        }
                    }
                }
            }

            temppt = pt;

            for (int i = 0; i < this.Grid.AllVisibleRowCount; i++)
            {
                int index = this.Grid.FirstDisplayedRowIndex + i;
                IRow col = this.Grid.Rows[i]; 
                if (pt.Y < this.Grid.TopSideHeight)
                {
                    int dtop = this.Grid.TopSideHeight;
                    
                    int dis = ReSeRowDistance(ref col, ref dtop, pt);
                    this.TopRow = col;
                    this.TopDistance = dis;
                    break;
                }
                else
                {
                    if (col.Top <= temppt.Y && col.Bottom > temppt.Y)
                    {
                        this.TopRow = col;
                        this.TopDistance = pt.Y - col.Top;
                        break;
                    }
                }
            }
        }

        public virtual int GetTopRowTop(int index, int toprowindex)
        {
            int dtop = 0;

            for (int i = toprowindex; i < index; i++)
            {
                IRow col = this.Grid.Rows[i];
                dtop = dtop - col.Height;
            }

            return dtop;

        }

        public virtual int GetLeftColumnLeft(int index, int leftcolumnindex)
        {
            int dleft = 0;

            for (int i = leftcolumnindex; i < index; i++)
            {
                IColumn col = this.Grid.Columns[i];
                if (col != null)
                {
                    dleft = dleft - col.Width;
                }
            }

            return dleft;

        }

        public override void FreshLocation()
        {
            if (this.Grid.FirstDisplayedRowIndex + this.Grid.AllVisibleRowCount < this._TopRow.Index)
            {
                _IsVisibleInGrid = false;
                return;
            }
            if (this._LeftColumn.Index > this.Grid.FirstDisplayedColumnIndex + this.Grid.AllVisibleColumnCount)
            {
                _IsVisibleInGrid = false;
                return;
            }

            if (this.Grid.FirstDisplayedRowIndex <= this._TopRow.Index
                && this.Grid.FirstDisplayedRowIndex + this.Grid.AllVisibleRowCount >= this._TopRow.Index)
            {
                this._top = this._TopRow.Top + this._topdistance;
            }
            else if (this.Grid.FirstDisplayedRowIndex > this._TopRow.Index)
            {
                this._top = this.Grid.TopSideHeight + GetTopRowTop(this.Grid.FirstDisplayedRowIndex, this._TopRow.Index) + this._topdistance;
            }

            if (this.Grid.FirstDisplayedColumnIndex <= this._LeftColumn.Index
    && this._LeftColumn.Index <= this.Grid.FirstDisplayedColumnIndex + this.Grid.AllVisibleColumnCount)
            {
                this._left = this._LeftColumn.Left + this._leftdistance;
            }
            else if (this.Grid.FirstDisplayedColumnIndex > this._LeftColumn.Index)
            {
                this._left = this.Grid.LeftSideWidth + GetLeftColumnLeft(this.Grid.FirstDisplayedColumnIndex, this._LeftColumn.Index) + this._leftdistance;
            }


            _IsVisibleInGrid = true;
        }

        #endregion

        #region IIsVisibleInGrid 成员
        private bool _IsVisibleInGrid = false;
        public virtual bool IsVisibleInGrid
        {
            get { return _IsVisibleInGrid; }
            set { _IsVisibleInGrid = value; }
        }
        #endregion

        #region IDesignMode 成员
        DataExcelDesignMode _DesignMode = DataExcelDesignMode.Design;
        public virtual DataExcelDesignMode DesignMode
        {
            get
            {
                return _DesignMode;
            }
            set
            {
                _DesignMode = value;
            }
        }

        #endregion

        #region ICanChangedSize 成员
        bool _CanChangedSize = true;
        public virtual bool CanChangedSize
        {
            get
            {
                return _CanChangedSize;
            }
            set
            {
                _CanChangedSize = value;
            }
        }

        #endregion

        #region ISizeRect 成员

        public override bool SizeRectContains(Point pt)
        {
            if (!this.CanChangedSize)
                return false;
            bool result = false;

            if (this.TopLeft.Contains(pt))
            {
                return true;
            }
            if (this.TopRight.Contains(pt))
            {
                return true;
            }
            if (this.BottomLeft.Contains(pt))
            {
                return true;
            }
            if (this.BottomRight.Contains(pt))
            {
                return true;
            }
            if (this.MidBottom.Contains(pt))
            {
                return true;
            }
            if (this.MidLeft.Contains(pt))
            {
                return true;
            }
            if (this.MidRight.Contains(pt))
            {
                return true;
            }
            if (this.MidTop.Contains(pt))
            {
                return true;
            }
            return result;
        }

        #endregion

        #region ISizeChangedMode 成员
        private SizeChangMode _SizeChangMode = SizeChangMode.Null;
        public override SizeChangMode SizeChangMode
        {
            get
            {
                return _SizeChangMode;
            }
            set
            {
                _SizeChangMode = value;
            }
        }

        #endregion

        #region ISizeRect 成员

        public override bool MouseDown(Point pt)
        {
            if (!this.CanChangedSize)
                return false;
            bool result = false;

            _MouseDownPoint = pt;
            _MouseDownSize = new Size(this.Width, this.Height);
            if (this.TopLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.TopLeft;
                return true;
            }
            if (this.TopRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.TopRight;
                return true;
            }
            if (this.BottomLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.BoomLeft;
                return true;
            }
            if (this.BottomRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.BoomRight;
                return true;
            }
            if (this.MidTop.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidTop;
                return true;
            }
            if (this.MidBottom.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidBoom;
                return true;
            }
            if (this.MidLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidLeft;
                return true;
            }
            if (this.MidRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidRight;
                return true;
            }
            return result;
        }

        public virtual bool MouseMove(Point pt)
        {
            return true;
        }

        #endregion

        #region IExtendCellDoubleClick 成员

        public virtual void OnExtendCellDoubleClick()
        {

        }

        #endregion

        #region IPrint 成员

        public override bool Print(PrintArgs e)
        { 
            return false;
        }

        #endregion

        #region IText 成员
        private string _text = string.Empty;
        public override string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        #endregion

        #region IFont 成员
        private Font _font = null;
        public override Font Font
        {
            get
            {
                if (this._font == null)
                {
                    return this.Grid.Font;
                }
                return _font;
            }
            set
            {
                _font = value;

            }
        }

        #endregion


        private int _SelectBorderWidth = 6;
        [Browsable(false)]
        public virtual int SelectBorderWidth
        {
            get
            {
                return _SelectBorderWidth;
            }
            set
            {
                _SelectBorderWidth = value;
            }
        }

        [Browsable(false)]
        public virtual Rectangle TopLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left - _SelectBorderWidth, rectf.Top - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public virtual Rectangle TopRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right, rectf.Top - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public virtual Rectangle BottomLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left - _SelectBorderWidth, rectf.Bottom, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public virtual Rectangle BottomRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right, rectf.Bottom, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;


            }
        }
        [Browsable(false)]
        public virtual Rectangle MidTop
        {
            get
            {
                Rectangle rectf = this.Rect;


                rectf = new Rectangle(rectf.Left + rectf.Width / 2 - _SelectBorderWidth / 2, rectf.Top - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;


            }
        }
        [Browsable(false)]
        public virtual Rectangle MidBottom
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left + rectf.Width / 2 - _SelectBorderWidth / 2, rectf.Bottom, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }
        [Browsable(false)]
        public virtual Rectangle MidLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left - _SelectBorderWidth, rectf.Top + rectf.Height / 2 - _SelectBorderWidth / 2, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }
        [Browsable(false)]
        public virtual Rectangle MidRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right, rectf.Top + rectf.Height / 2 - _SelectBorderWidth / 2, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }

    }
}