using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Feng.Forms.Drawing;

namespace Feng.Office.Excel
{

    [Serializable]
    public class VScroller : IDataExcelScroller
    {
        public VScroller()
        {
        }
        public VScroller(DataExcel grid)
        {
            this._grid = grid;
            this.Height = grid.Height - this.Width;
            this.Top = 0;
            this.Min = 1;
            this.Left = grid.Width - this.Width;
        }
        private int _min = 0;
        private int _max = 1; 
        private float _scrollheight = 10;
        private float _scrolltop = 0;
        private int Header
        {
            get { return (int)this._width; }
        }
        public virtual int Max
        {
            get { return this._max; }
            set
            {
                this._max = value;
                ReSetScrollHeight();
            }
        }
        public virtual int Min
        {
            get { return this._min; }
            set { this._min = value; }
        }
        private int _position = 0;

        public virtual int Position
        {
            get { return this._position; }
            set
            {
                if (value > this.Max)
                {
                    value = this.Max;
                }
                if (value < this.Min)
                {
                    value = this.Min;
                }
                if (BeforeValueChanged != null)
                {
                    BeforeValueChangedArgs a = new BeforeValueChangedArgs();
                    a.Value = value;
                    BeforeValueChanged(this, a);
                    if (a.Cancel)
                    {
                        return;
                    }
                }
                this.Grid.BeginReFresh();
                this._position = value;
                ReSetScrollHeight();
                if (ValueChanged != null)
                {
                    ValueChanged(this, this._position);
                }

                this.Grid.EndReFresh(); 
             }
        }

        private int _smallchange = 1;
        public virtual int SmallChange
        {
            get { return _smallchange; }
            set { _smallchange = value; }
        }
        private int _largechange = 10;
        public virtual int LargeChange
        {
            get { return _largechange; }
            set
            {
                _largechange = value;
                ReSetScrollHeight();
            }
        }
        private int _visablecount = 0;
        public virtual int VisableCount
        {
            get { return _visablecount; }
            set
            {
                _visablecount = value;
                ReSetScrollHeight();
            }
        }
        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataExcel Grid
        {
            get
            {
                return this._grid;
            }
            set
            {
                _grid = value;
            }
        }

        public virtual RectangleF UpArrowArea
        {
            get
            {
                RectangleF rects = this.Rect;
                return new RectangleF(rects.Left, rects.Top, rects.Width, rects.Width);
            }
        }
        public virtual RectangleF DownArrowArea
        {
            get
            {
                RectangleF rects = this.Rect;
                return new RectangleF(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);
            }
        }
        public virtual RectangleF ThumdArea
        {
            get
            {

                return new RectangleF(this.Left, this.Top + this.Header + _scrolltop, this.Rect.Width, _scrollheight);
            }
        }

        private Color _backareacolor = System.Drawing.SystemColors.Control;
        public Color BackAreaColor
        {
            get { return _backareacolor; }
            set { _backareacolor = value; }
        }
        private Color _Arrowcolor = System.Drawing.SystemColors.Control;
        public Color ArrowColor
        {
            get { return _Arrowcolor; }
            set { _Arrowcolor = value; }
        }
        private Color _backdirectioncolor = System.Drawing.SystemColors.ControlDarkDark;
        public Color BackDirectionColor
        {
            get { return _backdirectioncolor; }
            set { _backdirectioncolor = value; }
        }
        private Color _ThumdBackcolor = System.Drawing.SystemColors.ControlDark;
        public Color ThumdBackColor
        {
            get { return _ThumdBackcolor; }
            set { _ThumdBackcolor = value; }
        }
        private Color _SelectThumdBackcolor = Color.LightSkyBlue;
        public Color SelectThumdBackcolor
        {
            get { return _SelectThumdBackcolor; }
            set { _SelectThumdBackcolor = value; }
        }

        public virtual event Feng.Office.EventHelper.BeforeValueChangedEventHandler BeforeValueChanged;
        public virtual event Feng.Office.EventHelper.ValueChangedEventHandler ValueChanged;
        public virtual event Feng.Office.EventHelper.ClickEventHandler Click;
        public virtual event Feng.Office.EventHelper.ClickEventHandler ThumdAreaClick;
        public virtual event Feng.Office.EventHelper.ClickEventHandler DownArrowAreaClick;
        public virtual event Feng.Office.EventHelper.ClickEventHandler UpArrowAreaClick;

        public virtual void ProvPage()
        {
            _position = _position - this.LargeChange;
            _position = _position < _min ? _min : _position;
            ReSetScrollHeight();
        }
        public virtual void Prov()
        {
            _position -= _smallchange;
            _position = _position < _min ? _min : _position;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedRowIndex = (_position);
        }
        public virtual void NextPage()
        {
            _position = _position + this.LargeChange;
            _position = _position > _max ? _max : _position;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedRowIndex = (_position);
        }
        public virtual void Next()
        {
            _position += _smallchange;
            _position = _position > _max ? _max : _position;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedRowIndex = (_position);
        }
        public virtual void Home()
        {
            _position = _min;
            ReSetScrollHeight();
        }
        public virtual void End()
        {
            _position = _max;
            ReSetScrollHeight();
        }
        public virtual void Clear()
        {
            this._min = 0;
            this._max = 0;
            this._smallchange = 1;
            this._position = 0;
            this._scrollheight = 10;
            this._scrolltop = 0;
            this._largechange = 10;
        }

        public virtual void ReSetScrollHeight()
        {
            if (this.Grid.VisableRows.Count < 1)
                return;
            float area = (this.Height - Header * 2);
            float count = (this.Max - this.Min) * 1f / this.LargeChange;
            if (count < 1)
            {
                count = 1;
            }
            float scrollheight = area / count;
            if (scrollheight < 13)
            {
                scrollheight = 13;
            }
            _scrollheight = scrollheight;
            area = (this.Height - Header * 2) - scrollheight;
            count = this.Max - this.Min;
            scrollheight = area / count;
            int postion = this.Position - this.Min;
            _scrolltop = scrollheight * (postion); 
        }

        public virtual bool CheckInTop(Point pf)
        {
            RectangleF rect = new RectangleF(0, 0, this.Rect.Width, 10);
            return rect.Contains(pf);
        }
        public virtual bool CheckInBoom(Point pf)
        {
            RectangleF rect = new RectangleF(0, this.Rect.Bottom - 10, this.Rect.Width, 10);
            return rect.Contains(pf);
        }

        private void DrawArrow(Graphics g)
        {
            DrawUpArrow(g);
            DrawDownArrow(g);
        }
        private void DrawArea(Graphics g)
        {
            RectangleF rect = this.Rect;
            using (SolidBrush sb = new SolidBrush(BackAreaColor))
            {
                g.FillRectangle(sb, rect);
            }
        }
        public virtual void DrawBorder(Graphics g)
        {
            return;

        }
        private void DrawThumdBack(Graphics g)
        {
            if (_scrollheight <= 0)
                return;
            if (this.Grid.Selectmode != SelectMode.VScrollMoveSelected)
            {
                RectangleF rect = new RectangleF(this.Left, this.Top + this.Header + _scrolltop, this.Rect.Width, _scrollheight);
                GraphicsHelper.FillColorRectangle(g, ThumdBackColor, rect, true);
            }
            else
            {
                RectangleF rect = new RectangleF(this.Left, this.Top + this.Header + _scrolltop, this.Rect.Width, _scrollheight);
                GraphicsHelper.FillColorRectangle(g, SelectThumdBackcolor, rect, true);
            }
        }
        private void DrawThumd(Graphics g)
        {
            RectangleF rect = new RectangleF(this.Left, this.Top + this.Header + _scrolltop, this.Rect.Width, _scrollheight);
            if (_scrollheight > 15)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {

                    int x = -3;
                    PointF pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    PointF pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 3, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 3, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 3, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 3, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                }
            }
            else if (_scrollheight > 6)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {

                    int x = -1;
                    PointF pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    PointF pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                }
            }
        }
        public virtual void DrawUpArrow(Graphics g)
        {
            RectangleF rects = this.Rect;

            RectangleF rectt = new RectangleF(rects.Left, rects.Top, rects.Width, rects.Width);

            GraphicsHelper.FillColorRectangle(g, BackDirectionColor, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, GraphicsHelper.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
            GraphicsHelper.FillColorPath(g, ArrowColor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, GraphicsHelper.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }
        public virtual void DrawDownArrow(Graphics g)
        {
            RectangleF rects = this.Rect;

            RectangleF rectt = new RectangleF(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);
            GraphicsHelper.FillColorRectangle(g, BackDirectionColor, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, GraphicsHelper.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
            GraphicsHelper.FillColorPath(g, ArrowColor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, GraphicsHelper.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

        }
        public virtual bool OnDraw(Graphics g)
        {
            if (this.Visable)
            {
                System.Drawing.Drawing2D.GraphicsState gs = g.Save();
                g.SetClip(this.Rect);
                DrawArea(g);
                DrawThumdBack(g);
                DrawArrow(g);
                DrawThumd(g);
                DrawBorder(g);
                g.Restore(gs);
            }
            return false;
        }

        private PointF downpoint = PointF.Empty;
        public virtual bool OnMouseDown(MouseEventArgs e)
        {
            Point pt = e.Location;
            if (UpArrowArea.Contains(pt))
            {
                Prov();
                if (UpArrowAreaClick != null)
                {
                    UpArrowAreaClick(this, e);
                }
                return true;
            }
            if (DownArrowArea.Contains(pt))
            {
                Next();
                if (DownArrowAreaClick != null)
                {
                    DownArrowAreaClick(this, e);
                }
                return true;
            }
            if (ThumdArea.Contains(pt))
            {
                downpoint = ThumdArea.Location;
                this.Grid.Selectmode = SelectMode.VScrollMoveSelected;
                if (ThumdAreaClick != null)
                {
                    ThumdAreaClick(this, e);
                }
                return true;
            }
            if (this.Rect.Contains(pt))
            {
                PointToIndex(pt);
                if (Click != null)
                {
                    Click(this, e);
                }
                return true;
            }
            return false;
        }


        private void PointToIndex(Point pt)
        {

            int count = this.Max - this.Min;
            float area = (this.Height - Header * 2);
            float onepointsteup = (area / count);
            
            float y = pt.Y - this.Header;
            int i = (int)(y / onepointsteup);
            if (i < (this.Grid.EndDisplayedRowIndex))
            {
                this.Position = i;
            }
         
        }

        public virtual bool OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)
        {
            Point pt = e.Location;
            if (UpArrowArea.Contains(pt))
            {
                return true;
            }
            if (DownArrowArea.Contains(pt))
            {
                return true;
            }
            if (this.Rect.Contains(pt))
            {
                return true;
            }
            return false;
        }

        public virtual bool OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            Point pt = e.Location;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.Grid.Selectmode == SelectMode.VScrollMoveSelected)
                {
                    int d = pt.Y - this.Grid.MouseDownPoint.Y;
                    Point pd = Point.Round(new PointF(this.Left, this.Grid.MouseDownPoint.Y + d));
                    PointToIndex(pd);
                    return true;
                }
            }
            if (UpArrowArea.Contains(pt))
            {
                if (this._grid == null)
                    return false;
                this._grid.BeginSetCursor(Cursors.Default);
                return true;
            }
            if (DownArrowArea.Contains(pt))
            {
                if (this._grid == null)
                    return false;
                this._grid.BeginSetCursor(Cursors.Default);
                return true;
            }
            if (ThumdArea.Contains(pt))
            {
                if (this._grid == null)
                    return false;
                this._grid.BeginSetCursor(Cursors.Default);
                return true;
            }
            if (this.Rect.Contains(pt))
            {
                if (this._grid == null)
                    return false;
                this._grid.BeginSetCursor(Cursors.Default);
                return true;
            }
            return false;
        }


        #region IBounds 成员

        private float _left = 0;
        public virtual float Left
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
 
        public virtual float Height
        {
            get
            {
                if (this.Grid.ShowHorizontalScroller)
                {
                    return this.Grid.Height - this.Width;
                }
                return this.Grid.Height;
            }
            set
            {
                ReSetScrollHeight();
            }
        }

        public virtual float Right
        {
            get
            {

                return _width + _left;
            }

        }
        public virtual float Bottom
        {
            get
            {

                return _top + this.Height;
            }

        }
        private float _top = 0;
        public virtual float Top
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

        private float _width = 15;
        public virtual float Width
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

        public virtual RectangleF Rect
        {
            get
            {
                return new RectangleF(this._left, this._top, this._width, this.Height );
            }
        }
        #endregion

        #region IFont 成员
        private Font _font = null;
        public virtual Font Font
        {
            get
            {
                if (_font != null)
                {
                    return this._font;
                }
                return this.Grid.Font;
            }
            set
            {
                this._font = value;
            }
        }

        #endregion

        #region IEvents 成员

        public virtual bool OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseLeave(EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseHover(EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseEnter(EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseCaptureChanged(EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            return false;
        }
 
        public virtual bool OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            return false;
        }

        public virtual bool OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            return false;
        }

        public virtual bool OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            return false;
        }

        public virtual bool OnPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            return false;
        }

        public virtual bool OnDoubleClick(EventArgs e)
        {
            return false;
        }

        public virtual bool OnPreProcessMessage(ref System.Windows.Forms.Message msg)
        {
            return false;
        }

        public virtual bool OnProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            return false;
        }

        public virtual bool OnProcessDialogChar(char charCode)
        {
            return false;
        }

        public virtual bool OnProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            return false;
        }

        public virtual bool OnProcessKeyEventArgs(ref System.Windows.Forms.Message m)
        {
            return false;
        }

        public virtual bool OnProcessKeyMessage(ref System.Windows.Forms.Message m)
        {
            return false;
        }

        public virtual bool OnProcessKeyPreview(ref System.Windows.Forms.Message m)
        {
            return false;
        }

        public virtual bool OnWndProc(ref System.Windows.Forms.Message m)
        {
            return false;
        }

        #endregion

        #region IEvents 成员


        public bool OnClick(EventArgs e)
        {
            return false;
        }

        #endregion

        #region IVisable 成员

        [DefaultValue(true)]
        public virtual bool Visable
        {
            get
            {
                return this.Grid.ShowVerticalScroller;
            }
            set
            {
                this.Grid.ShowVerticalScroller = value;
            }
        }

        #endregion
    }
}
