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
    public class HScroller : IDataExcelScroller
    {
        public HScroller()
        {
        }
        public HScroller(DataExcel grid)
        {
            this._grid = grid; 
            this.Left = 0;
            this.Min = 1;
            this.Top = grid.Height - this.Height;
        }
 
        private float _scrollheight = 10;
        private float _scrolltop = 0;
        private float Header
        {
            get { return this._height; }
        }

        public virtual void ReSetScrollHeight()
        {
            if (this.Grid.VisableColumns.Count < 1)
                return;
 
            float area = (this.Width - Header * 2);
            float count = (this.Max - this.Min) * 1f / this.LargeChange * 1f;
            if (count < 1)
            {
                count = 1;
            }
            float scrollheight = (area / count);
            if (scrollheight < 13)
            {
                scrollheight = 13;
            }
            _scrollheight = scrollheight;
            area = (this.Width - Header * 2) - scrollheight;

            count = this.Max - this.Min;
            scrollheight = area / count;
            int postion = this.Position - this.Min;

            _scrolltop = scrollheight * postion; 
        } 
 
        private PointF downpoint = PointF.Empty;
        public virtual bool OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (!this.Visable)
            {
                return false;
            }
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
                this.Grid.Selectmode = SelectMode.HScrollMoveSelected; 
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
            float area = (this.Width - Header * 2);
            float onepointsteup = (area / count);

            float y = pt.X - this.Header;
            int i = (int)(y / onepointsteup);
            if (i < (this.Grid.EndDisplayedColumnIndex))
            {
                this.Position = i;
            }
        }

        public virtual bool OnMouseDoubleClick(MouseEventArgs e)
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

        public virtual bool OnMouseMove(MouseEventArgs e)
        {
            Point pt = e.Location;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.Grid.Selectmode == SelectMode.HScrollMoveSelected)
                {
                    float  d = pt.X - this.Grid.MouseDownPoint.X;
                    int pindex = (int)((d / BodyArea.Width) * this.Count);
                    int iindex = this.Position + pindex;
                    if (iindex < (this.Grid.EndDisplayedColumnIndex))
                    {
                        this.Position = iindex;
                    }
                    //Point pd = Point.Round(new PointF(this.Grid.MouseDownPoint.X + d, this.Top));
                    //PointToIndex(pd);
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

        #region Color
        private Color _firstcolor = Color.White;
        public virtual Color FirstColor
        {
            get { return Color.FromArgb(this._TransApl, _firstcolor); }
            set { _firstcolor = value; }
        }

        private Color _secondcolor = Color.Blue;
        public virtual Color SecondColor
        {
            get { return _secondcolor;/* Color.FromArgb(this._TransApl, _secondcolor);*/ }
            set { _secondcolor = value; }
        }

        private Color _headfirsttcolor = Color.AliceBlue;
        public virtual Color HeaderFirstColor
        {
            get { return _headfirsttcolor; }
            set { _headfirsttcolor = value; }
        }

        private Color _headsecondcolor = Color.BlueViolet;
        public virtual Color HeaderSecondColor
        {
            get { return _headsecondcolor;/* Color.FromArgb(this._TransApl, _secondcolor);*/ }
            set { _headsecondcolor = value; }
        }
        #endregion

        #region TransApl
        private int _TransApl = 170;
        public virtual int TransApl
        {
            get { return _TransApl; }
            set { _TransApl = value; }
        }

        private float[] _Factors = new float[] { 0.5f, 0.4f, 0.3f, 0.4f, 0.5f };
        public virtual float[] Factors
        {
            get { return _Factors; }
            set { _Factors = value; }
        }

        private float[] _Positions = new float[] { 0f, 0.1f, 0.5f, 0.9f, 1f };
        public virtual float[] Positions
        {
            get { return _Positions; }
            set { _Positions = value; }
        }

        #endregion

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

        private float _height = 15;
        public virtual float Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                ReSetScrollHeight();
            }
        }

        public virtual float Right
        {
            get
            {

                return this.Width + _left;
            }

        }
        public virtual float Bottom
        {
            get
            {

                return _top + _height;
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
 
        public virtual float Width
        {
            get
            {
                if (this.Grid.ShowVerticalScroller)
                {
                    return this.Grid.Width - this.Height;
                }

                return this.Grid.Width;
            }
            set
            { 
            }
        }

        public virtual RectangleF Rect
        {
            get
            {
                return new RectangleF(this._left, this._top, this.Width, this._height);
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


        public virtual bool OnClick(EventArgs e)
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
                return this.Grid.ShowHorizontalScroller;
            }
            set
            {
                this.Grid.ShowHorizontalScroller = value;
            }
        }

        #endregion

        #region IDraw 成员
        public virtual void DrawUpArrow(Graphics g)
        {
            RectangleF rects = this.Rect;

            RectangleF rectt = new RectangleF(rects.Left, rects.Top, rects.Height, rects.Height);

            GraphicsHelper.FillColorRectangle(g, System.Drawing.SystemColors.ControlDarkDark, rectt);
 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-4, -3);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, GraphicsHelper.Orientation.Right);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
 
            GraphicsHelper.FillColorPath(g, System.Drawing.SystemColors.Control, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal, GraphicsHelper.Orientation.Right);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }
        public virtual void DrawDownArrow(Graphics g)
        {

            RectangleF rects = this.Rect;

            RectangleF rectt = new RectangleF(rects.Right - rects.Height, rects.Top, rects.Height, rects.Height);

            GraphicsHelper.FillColorRectangle(g, System.Drawing.SystemColors.ControlDarkDark, rectt);
 
            rectt.Inflate(-4, -3);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, GraphicsHelper.Orientation.Left);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
 
            GraphicsHelper.FillColorPath(g, System.Drawing.SystemColors.Control, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal, GraphicsHelper.Orientation.Left);
 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

        }
        public virtual bool OnDraw(Graphics g)
        {
            if (this.Visable)
            {
                DrawArea(g);
                DrawThumdBack(g);
                DrawArrow(g);
                DrawThumd(g);
                DrawBorder(g);
#if DEBUG
                g.DrawString(string.Format("{0},{1},{2}", this.Max, this.Min, this.Position), this.Font, Brushes.Red, this.Rect);
#endif
            }
            return false;
        }
        private void DrawArrow(Graphics g)
        {
            DrawUpArrow(g);
            DrawDownArrow(g);
        }
        private void DrawArea(Graphics g)
        {
            RectangleF rect = this.Rect;
            Color cc = System.Drawing.SystemColors.Control;// Color.FromArgb(100, Color.CornflowerBlue);
            using (SolidBrush sb = new SolidBrush(cc))
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
            RectangleF rect = new RectangleF(this.Left + this.Header + _scrolltop, this.Top, _scrollheight, this.Rect.Height);
            GraphicsHelper.FillColorRectangle(g, System.Drawing.SystemColors.ControlDark, rect, System.Drawing.Drawing2D.LinearGradientMode.Vertical, true);
        }
        private void DrawThumd(Graphics g)
        {
            RectangleF rect = new RectangleF(this.Left + this.Header + _scrolltop, this.Top, _scrollheight, this.Rect.Height);
            if (_scrollheight > 15)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {

                    float h = rect.Left + rect.Width / 2;

                    int x = -3;

                    PointF pt1 = new PointF(h - x, rect.Top + 2.5f);
                    PointF pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 3);
                    pt2 = new PointF(h - x, rect.Bottom - 3);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 2);
                    pt2 = new PointF(h - x, rect.Bottom - 2);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 3);
                    pt2 = new PointF(h - x, rect.Bottom - 3);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 2.5f);
                    pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                }
            }
            else if (_scrollheight > 6)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {
                    float h = rect.Left + rect.Width / 2;
                    int x = -1;
                    PointF pt1 = new PointF(h - x, rect.Top + 2.5f);
                    PointF pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 2.5f);
                    pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                }
            }
        }

        public virtual RectangleF UpArrowArea
        {
            get
            {
                //TODO:          return new RectangleF(rects.Left, rects.Top, rects.Width, rects.Width); }


                RectangleF rects = this.Rect;
                return new RectangleF(
                    rects.Left,
                    rects.Top,
                    rects.Height,
                    rects.Height);
            }
        }
        public virtual RectangleF DownArrowArea
        {
            get
            {

                //TODO: return new RectangleF(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);

                RectangleF rects = this.Rect;
                return new RectangleF(
                    rects.Right - rects.Height,
                    rects.Top,
                    rects.Height,
                    rects.Height);
            }
        }
        public virtual RectangleF ThumdArea
        {
            get
            {
                return new RectangleF(this.Left + this.Header + _scrolltop, this.Top, _scrollheight, this.Rect.Height);
            }
        }
        public virtual RectangleF BodyArea
        {
            get {
                return new RectangleF(this.Left + this.Header, this.Top, this.Width - this.Header - this.Header, this.Height);
            }
        }
        #endregion

        #region INext 成员
        public virtual void ProvPage()
        {
            Position = Position - this.LargeChange;
            Position = Position < Min ? Min : Position;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedColumnIndex=(Position);
        }
        public virtual void Prov()
        {
            Position -= SmallChange;
            Position = Position < Min ? Min : Position;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedColumnIndex=(Position);
        }
        public virtual void NextPage()
        {
            Position = Position + this.LargeChange;
            Position = Position > Max ? Max : Position;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedColumnIndex=(Position);
        }
        public virtual void Next()
        {
            Position += SmallChange;
            Position = Position > Max ? Max : Position;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedColumnIndex = (Position);
        }
        public virtual void Home()
        {
            Position = Min;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedColumnIndex = (Position);
        }
        public virtual void End()
        {
            Position = Max;
            ReSetScrollHeight();
            this.Grid.FirstDisplayedColumnIndex = (Position);
        }

        #endregion

        #region IScroller 成员

        public virtual event Feng.Office.EventHelper.BeforeValueChangedEventHandler BeforeValueChanged;
        public virtual event Feng.Office.EventHelper.ValueChangedEventHandler ValueChanged;
        public virtual event Feng.Office.EventHelper.ClickEventHandler Click;
        public virtual event Feng.Office.EventHelper.ClickEventHandler ThumdAreaClick;
        public virtual event Feng.Office.EventHelper.ClickEventHandler DownArrowAreaClick;
        public virtual event Feng.Office.EventHelper.ClickEventHandler UpArrowAreaClick;

        #endregion

        #region ISmallChange 成员

        private int _SmallChange = 1;
        public virtual int SmallChange
        {
            get { return _SmallChange; }
            set { _SmallChange = value; }
        }

 

        #endregion

        #region ILargeChange 成员
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

        #endregion

        #region IVisableCount 成员

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

        #endregion

        #region IMax 成员
        public virtual int Count
        {
            get {
                return this.Max - this.Min;
            }
        }
        private int _Max = 1;
        public virtual int Max
        {
            get { return this._Max; }
            set
            {
                if (this._Max == value)
                {
                    return;
                }
                this._Max = value;
                ReSetScrollHeight();
            }
        }

        #endregion

        #region IMin 成员
        private int _Min = 0;
        public virtual int Min
        {
            get { return this._Min; }
            set { this._Min = value; }
        }

        #endregion

        #region IClear 成员

        public virtual void Clear()
        {
            this._Min = 0;
            this._Max = 0;
            this._SmallChange = 1;  
            this._Position = 0;
            _scrollheight = 10;
            _scrolltop = 0;
            _largechange = 10;
        }

        #endregion

        #region IGrid 成员
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
        #endregion

        #region IInt32Value 成员

        private int _Position = 0;

        public virtual int Position
        {
            get { return this._Position; }
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
                this._Position = value;
                ReSetScrollHeight();
                if (ValueChanged != null)
                {
                    ValueChanged(this, this._Position);
                }
                this.Grid.EndReFresh(); 
            }
        }

        #endregion
    }
}
