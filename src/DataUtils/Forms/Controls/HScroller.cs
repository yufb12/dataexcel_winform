using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

using Feng.Drawing;

namespace Feng.Forms
{

   [ToolboxItem(false)]
    public class HScroller : System.Windows.Forms.HScrollBar
    {
        protected override void OnScroll(ScrollEventArgs se)
        {
            if (se.Type == ScrollEventType.SmallIncrement)
            {
                base.Maximum = base.Maximum + 1;
            }
            base.OnScroll(se);
        }
        public new int Maximum
        {
            get
            {
                return base.Maximum;
            }
            set 
            {
                base.Maximum = value;
            }
        }
        public new int Value
        {
            get
            { 
                return base.Value;
            }
            set
            {
                if (value < 1)
                {
                    base.Value = 100;
                }
                else
                { 
                    base.Value = value;
                }
            }
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }
    }

//    [ToolboxItem(true)]
//    public class HScroller : System.Windows.Forms.Control
//    {
//        public HScroller()
//        {
//            base.SetStyle(ControlStyles.DoubleBuffer
//                | ControlStyles.ResizeRedraw
//                | ControlStyles.SupportsTransparentBackColor
//                | ControlStyles.AllPaintingInWmPaint, true);

//            base.SetStyle(ControlStyles.ContainerControl, true);
//            base.SetStyle(ControlStyles.StandardClick, true);
//            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

//            base.UpdateStyles();
//            Init();
//        }
//        public static int DeafultVScrollerWidth = 16;
//        public void Init()
//        {
//            this.Height = DeafultVScrollerWidth;
//        }
//        private int _min = 0;
//        private int _max = 100;
//        private int _pagechang = 10;
//        private float _scrollheight = 10;
//        private float _scrolltop = 0;
//        private int Header
//        {
//            get { return (int)this.Height; }
//        }
//        public virtual int Max
//        {
//            get { return this._max; }
//            set
//            {
//                this._max = value;
//                RefreshScrollThumd();
//            }
//        }
//        public virtual int Min
//        {
//            get { return this._min; }
//            set { this._min = value; }
//        }
//        private int _value = 0;

//        public virtual int Value
//        {
//            get { return this._value; }
//            set
//            {
//                if (value == this.Value)
//                {
//                    return;
//                }
//                if (value > this.MaxPosition)
//                {
//                    value = this.MaxPosition;
//                }
//                if (value < this.Min)
//                {
//                    value = this.Min;
//                }
//                if (BeforeValueChanged != null)
//                {
//                    BeforeValueChangedArgs a = new BeforeValueChangedArgs();
//                    a.Value = value;
//                    BeforeValueChanged(this, a);
//                    if (a.Cancel)
//                    {
//                        return;
//                    }
//                }
//                this._value = value;


//                if (ValueChanged != null)
//                {
//                    ValueChanged(this, this._value);
//                }
//                RefreshScrollThumd();
//                PropertyChanged();
//            }
//        }

//        private int _smallchange = 1;
//        public virtual int SmallChange
//        {
//            get { return _smallchange; }
//            set { _smallchange = value; }
//        }
//        private int _largechange = 10;
//        public virtual int LargeChange
//        {
//            get { return _largechange; }
//            set
//            {
//                _largechange = value;
//                RefreshScrollThumd();
//                this.PropertyChanged();
//            }
//        }

//        public virtual void ProvPage()
//        {
//            int value = _value - _pagechang;
//            Value = value < _min ? _min : value;
//        }
//        public virtual void Prov()
//        {
//            int value = _value - _smallchange;
//            Value = value < _min ? _min : value;
//        }
//        public virtual void NextPage()
//        {
//            int value = _value + _pagechang;
//            Value = value > _max ? _max : value;
//        }
//        public virtual void Next()
//        {
//            int value = _value + _smallchange;
//            Value = value > _max ? _max : value;
//        }
//        public virtual void Home()
//        {
//            Value = _min;
//        }
//        public virtual void End()
//        {
//            Value = _max;
//        }
//        public virtual void Clear()
//        {
//            _min = 0;
//            _max = 0;
//            _smallchange = 1;
//            _pagechang = 10;
//            _value = 0;
//            _scrollheight = 10;
//            _scrolltop = 0;
//        }
//        private int MaxPosition
//        {
//            get
//            {
//                return this.Max - this.LargeChange + 1;
//            }
//        }

//        public void RefreshScrollThumd()
//        {
//            int count = this.Count;
//            if (count < 1)
//            {
//                count = 1;
//            }
//            float height = this.BodyArea.Width;
//            int large = this.LargeChange;
//            if (count < large)
//            {
//                count = large;
//            }
//            float scrollthumd = (large * 1f / count * 1f) * height;
//            _scrollheight = scrollthumd;
//            if (_scrollheight < 16)
//            {
//                _scrollheight = 16;
//            }
//            int ncount = count - large + 1;
//            if (ncount < 1)
//            {
//                ncount = 1;
//            }
//            _scrolltop = (height - _scrollheight) / ncount * (this.Value - this.Min);
//        }

//        public virtual bool CheckInTop(Point pf)
//        {
//            RectangleF rect = new RectangleF(0, 0, this.Rect.Width, 10);
//            return rect.Contains(pf);
//        }
//        public virtual bool CheckInBoom(Point pf)
//        {
//            RectangleF rect = new RectangleF(0, this.Rect.Bottom - 10, this.Rect.Width, 10);
//            return rect.Contains(pf);
//        }

//        private Color _backareacolor = System.Drawing.SystemColors.Control;
//        public Color BackAreaColor
//        {
//            get { return _backareacolor; }
//            set { _backareacolor = value; }
//        }
//        private Color _Arrowcolor = System.Drawing.SystemColors.Control;
//        public Color ArrowColor
//        {
//            get { return _Arrowcolor; }
//            set { _Arrowcolor = value; }
//        }
//        private Color _backdirectioncolor = System.Drawing.SystemColors.ControlDarkDark;
//        public Color BackDirectionColor
//        {
//            get { return _backdirectioncolor; }
//            set { _backdirectioncolor = value; }
//        }
//        private Color _ThumdBackcolor = System.Drawing.SystemColors.ControlDark;
//        public Color ThumdBackColor
//        {
//            get { return _ThumdBackcolor; }
//            set { _ThumdBackcolor = value; }
//        }
//        private Color _SelectThumdBackcolor = Color.SkyBlue;
//        public Color SelectThumdBackcolor
//        {
//            get { return _SelectThumdBackcolor; }
//            set { _SelectThumdBackcolor = value; }
//        }
//        private void DrawArrow(Graphics g)
//        {
//            DrawUpArrow(g);
//            DrawDownArrow(g);
//        }
//        private void DrawArea(Graphics g)
//        {
//            RectangleF rect = this.Rect;
//            using (SolidBrush sb = new SolidBrush(BackAreaColor))
//            {
//                g.FillRectangle(sb, rect);
//            }
//        }
//        public virtual void DrawBorder(Graphics g)
//        {
//            return;

//        }
//        private void DrawThumdBack(Graphics g)
//        {
//            if (_scrollheight <= 0)
//                return;
//            Color thumdbackcolor = ThumdBackColor;
//            if (this.MoveSelected)
//            {
//                thumdbackcolor = SelectThumdBackcolor;
//            }
//            RectangleF rects = this.Rect;
//            RectangleF rect = new RectangleF(rects.Left + this.Header + _scrolltop, rects.Top, _scrollheight, this.Rect.Height);
//            GraphicsHelper.FillColorRectangle(g, thumdbackcolor, rect, LinearGradientMode.Vertical, true);

//        }
//        private void DrawThumd(Graphics g)
//        {
//            RectangleF rects = this.Rect;
//            RectangleF rect = new RectangleF(rects.Left + this.Header + _scrolltop, rects.Top, _scrollheight, this.Rect.Height);
//            if (_scrollheight > 15)
//            {
//                Color dc = Color.FromArgb(100, Color.Black);
//                using (Pen p = new Pen(dc))
//                {

//                    int x = -3;
//                    PointF pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
//                    PointF pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
//                    x += 2;
//                    g.DrawLine(p, pt1, pt2);
//                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 3);
//                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 3);
//                    x += 2;
//                    g.DrawLine(p, pt1, pt2);
//                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2);
//                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2);
//                    x += 2;
//                    g.DrawLine(p, pt1, pt2);
//                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 3);
//                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 3);
//                    x += 2;
//                    g.DrawLine(p, pt1, pt2);
//                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
//                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
//                    x += 2;
//                    g.DrawLine(p, pt1, pt2);
//                }
//            }
//            else if (_scrollheight > 6)
//            {
//                Color dc = Color.FromArgb(100, Color.Black);
//                using (Pen p = new Pen(dc))
//                {

//                    int x = -1;
//                    PointF pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
//                    PointF pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
//                    x += 2;
//                    g.DrawLine(p, pt1, pt2);
//                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
//                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
//                    x += 2;
//                    g.DrawLine(p, pt1, pt2);
//                }
//            }
//        }
//        public virtual void DrawUpArrow(Graphics g)
//        {
//            RectangleF rects = this.Rect;

//            RectangleF rectt = new RectangleF(rects.Left, rects.Top, rects.Height, rects.Height);

//            GraphicsHelper.FillColorRectangle(g, BackDirectionColor, rectt,
//                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

//            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//            rectt.Inflate(-3, -4);
//            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
//, Feng.Drawing.Orientation.Right);
//            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
//            GraphicsHelper.FillColorPath(g, ArrowColor, rects, path,
//                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
//            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
//        }
//        public virtual void DrawDownArrow(Graphics g)
//        {
//            RectangleF rects = this.Rect;

//            RectangleF rectt = new RectangleF(rects.Right - rects.Height, rects.Top, rects.Height, rects.Height);
//            GraphicsHelper.FillColorRectangle(g, BackDirectionColor, rectt,
//                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

//            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//            rectt.Inflate(-3, -4);
//            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
//, Feng.Drawing.Orientation.Left);
//            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
//            GraphicsHelper.FillColorPath(g, ArrowColor, rects, path,
//                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);
//            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

//        }
//        public virtual bool OnDraw(Graphics g)
//        {

//            System.Drawing.Drawing2D.GraphicsState gs = g.Save();
//            g.SetClip(this.Rect);
//            DrawArea(g);
//            DrawThumdBack(g);
//            DrawArrow(g);
//            DrawThumd(g);
//            DrawBorder(g);
//            g.Restore(gs);
//            //Rectangle rect = new Rectangle(Point.Round(ThumdArea.Location), new Size(3, this.Height));
//            //g.FillRectangle(Brushes.Blue, rect);
//            return false;
//        }
//        private bool MoveSelected = false;
//        private Point downpoint = Point.Empty;
//        private Size downsize = Size.Empty;
//        public void PropertyChanged()
//        {
//            this.Invalidate();
//        }

//        public virtual event Feng.EventHelper.BeforeValueChangedEventHandler BeforeValueChanged;
//        public virtual event Feng.EventHelper.ValueChangedEventHandler ValueChanged;
//        public virtual event Feng.EventHelper.ClickEventHandler Click;
//        public virtual event Feng.EventHelper.ClickEventHandler ThumdAreaClick;
//        public virtual event Feng.EventHelper.ClickEventHandler DownArrowAreaClick;
//        public virtual event Feng.EventHelper.ClickEventHandler UpArrowAreaClick;
//        public virtual RectangleF BodyArea
//        {
//            get
//            {
//                return new RectangleF(this.Left, this.Top + this.Header, this.Width - this.Header - this.Header, this.Height);
//            }
//        }
//        public virtual int Count
//        {
//            get
//            {
//                return this.Max - this.Min;
//            }
//        }
//        private void PointToIndex(Point pt)
//        {
//            int count = this.Count;
//            float height = this.BodyArea.Width - this.ThumdArea.Width;
//            int large = this.LargeChange;
//            int ncount = count - large + 1;
//            int i = (int)((ncount * (pt.X - this.Header)) / height);
//            this.Value = i;
//        }

//        protected override void OnPaint(PaintEventArgs e)
//        {

//            try
//            {
//                this.OnDraw(e.Graphics);
//                //e.Graphics.DrawString(this.Value.ToString(), this.Font, Brushes.Red, this.Rect.Location);
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//            base.OnPaint(e);
//        }

//        protected override void OnMouseMove(MouseEventArgs e)
//        {
//            Point pt = e.Location;
//            if (e.Button == System.Windows.Forms.MouseButtons.Left)
//            {
//                if (this.MoveSelected)
//                {

//#if DEBUG
//                    if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
//                    {
//                        System.Diagnostics.Debugger.Break();
//                    }
//#endif


//                    Point pd = new Point(pt.X - downsize.Width, 0);
//                    _scrolltop = pt.X - downsize.Width - this.Header;
//                    pd = Point.Round(this.ThumdArea.Location);
//                    PointToIndex(pd);
//                    //RefreshScrollThumd();
//                    this.Invalidate();
//                    return;
//                }
//            }
//            base.OnMouseMove(e);
//        }
//        protected override void OnMouseClick(MouseEventArgs e)
//        {
//            if (this.MoveSelected)
//                return;
//            Point pt = e.Location;
//            if (UpArrowArea.Contains(pt))
//            {
//                Prov();
//                if (UpArrowAreaClick != null)
//                {
//                    UpArrowAreaClick(this, e);
//                }
//                return;
//            }
//            if (DownArrowArea.Contains(pt))
//            {
//                Next();
//                if (DownArrowAreaClick != null)
//                {
//                    DownArrowAreaClick(this, e);
//                }
//                return;
//            }
//            if (this.Rect.Contains(pt))
//            {
//                PointToIndex(pt);
//                if (Click != null)
//                {
//                    Click(this, e);
//                }
//                return;
//            }

//            base.OnMouseClick(e);
//        }
//        protected override void OnMouseDown(MouseEventArgs e)
//        {
//            Point pt = e.Location;
//            if (ThumdArea.Contains(pt))
//            {
//                downpoint = pt;
//                Point pth = Point.Round(ThumdArea.Location);
//                downsize = new Size(pt.X - pth.X, pt.Y - pth.Y);
//                this.MoveSelected = true;
//                if (ThumdAreaClick != null)
//                {
//                    ThumdAreaClick(this, e);
//                }
//                return;
//            }
//            base.OnMouseDown(e);
//        }
//        protected override void OnMouseUp(MouseEventArgs e)
//        {
//            this.MoveSelected = false;
//            base.OnMouseUp(e);
//        }
//        public virtual RectangleF UpArrowArea
//        {
//            get
//            {
//                RectangleF rects = this.Rect;
//                return new RectangleF(rects.Left, rects.Top, rects.Height, rects.Height);
//            }
//        }
//        public virtual RectangleF DownArrowArea
//        {
//            get
//            {
//                RectangleF rects = this.Rect;
//                return new RectangleF(rects.Right - rects.Height, rects.Top, rects.Height, rects.Height);
//            }
//        }
//        public virtual RectangleF ThumdArea
//        {
//            get
//            {
//                RectangleF rects = this.Rect;
//                return new RectangleF(rects.Left + this.Header + _scrolltop, 
//                    rects.Top,_scrollheight, this.Rect.Height  );
//            }
//        }

//        #region IBounds 成员

//        public virtual RectangleF Rect
//        {
//            get
//            {
//                return new RectangleF(0, 0, this.Width, this.Height);
//            }
//        }
//        #endregion

//    }

}
