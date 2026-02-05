using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Feng.Drawing;
using Feng.Forms.Interface;
using Feng.Args;

namespace Feng.Forms.Controls
{
    [Serializable]
    public class HScrollerBase : IScrollerBase
    {
        public HScrollerBase()
        {
            
        }

        private int _pagechang = 10;
        private int _scrollheight = 10;
        private int _scrolltop = 0;
        private int Header
        {
            get { return this._height; }
        }

        public virtual void ReSetScrollHeight()
        {
            _value = _value > Max ? Max : _value;
            _value = _value < Min ? Min : _value;
            int area = (this.Width - Header * 2);
            int count = this.Max - this.Min;
            if (count < 1)
                count = 1;
            int scrollheight = (area / count);
            _scrollheight = scrollheight < 13 ? 13 : scrollheight;
            scrollheight = (area - _scrollheight) / count;
            _scrolltop = scrollheight * (_value - this._min);
        } 
 
        private PointF downpoint = PointF.Empty;
        public virtual bool OnCellMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (!this.Visible)
            {
                return false;
            }
            Point pt = e.Location;
            if (UpArrowArea.Contains(pt))
            {
                Prov(); 
                if (UpArrowAreaClick != null)
                {
                    UpArrowAreaClick(this, pt);
                }
                return true;
            }
            if (DownArrowArea.Contains(pt))
            {
                Next(); 
                if (DownArrowAreaClick != null)
                {
                    DownArrowAreaClick(this, pt);
                }
                return true;
            }
            if (ThumdArea.Contains(pt))
            {
                downpoint = ThumdArea.Location; 
                if (ThumdAreaClick != null)
                {
                    ThumdAreaClick(this, pt);
                }
                return true;
            }
            if (this.Rect.Contains(pt))
            {
                PointToIndex(pt); 
                if (Click != null)
                {
                    Click(this, pt);
                }
                return true;
            }
            return false;
        }
 
        private void PointToIndex(Point pt)
        { 
            int i = 0;
            int c = this._max;
            int h = this.Width - this.Header * 2;
            int hc = h / c;
            int hp = pt.X - this._left - this.Header;
            i = (int)Math.Ceiling(hp / hc * 1m);
 
        }

        public virtual bool OnCellMouseDoubleClick(MouseEventArgs e)
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

        public virtual bool OnCellMouseMove(MouseEventArgs e)
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
            if (ThumdArea.Contains(pt))
            { 
                return true;
            }
            if (this.Rect.Contains(pt))
            {
                PointToIndex(pt);
                if (Click != null)
                {
                    Click(this, pt);
                }
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

        private int _height = 15;
        public virtual int Height
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

        public virtual int Right
        {
            get
            {

                return this.Width + _left;
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
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public virtual Rectangle Rect
        {
            get
            {
                return new Rectangle(this._left, this._top, this.Width, this._height);
            }
        }
        #endregion

        #region IFont 成员
        private Font _font = null;
        public virtual Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        #endregion

        #region IEvents 成员

        public virtual bool OnCellMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            return false;
        }
 
        public virtual bool OnCellMouseLeave(EventArgs e)
        {
            return false;
        }

        public virtual bool OnCellMouseHover(EventArgs e)
        {
            return false;
        }

        public virtual bool OnCellMouseEnter(EventArgs e)
        {
            return false;
        }
 
        public virtual bool OnCellMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnCellMouseCaptureChanged(EventArgs e)
        {
            return false;
        }

        public virtual bool OnCellMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnCellKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            return false;
        }

        public virtual bool OnCellKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            return false;
        }

        public virtual bool OnCellKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            return false;
        }

        public virtual bool OnCellPreviewKeyDown(System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            return false;
        }

        public virtual bool OnCellDoubleClick(EventArgs e)
        {
            return false;
        }

        public virtual bool OnCellPreProcessMessage(ref System.Windows.Forms.Message msg)
        {
            return false;
        }

        public virtual bool OnCellProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            return false;
        }

        public virtual bool OnCellProcessDialogChar(char charCode)
        {
            return false;
        }

        public virtual bool OnCellProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            return false;
        }

        public virtual bool OnCellProcessKeyEventArgs(ref System.Windows.Forms.Message m)
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

        public virtual bool OnCellWndProc(ref System.Windows.Forms.Message m)
        {
            return false;
        }

        #endregion

        #region IEvents 成员


        public virtual bool OnCellClick(EventArgs e)
        {
            return false;
        }

        #endregion

        #region IVisible 成员

        private bool _Visible = true;
        [DefaultValue(true)]
        public virtual bool Visible
        {
            get
            {
                return this._Visible;
            }
            set
            {
                this._Visible = value;
            }
        }

        #endregion

        #region IDraw 成员
        public virtual void DrawUpArrow(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rects = this.Rect;

            Rectangle rectt = new Rectangle(rects.Left, rects.Top, rects.Height, rects.Height);

            GraphicsHelper.FillRectangleLinearGradient(g.Graphics, System.Drawing.SystemColors.ControlDarkDark, rectt);

            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-4, -3);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Right);
            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            GraphicsHelper.FillColorPath(g.Graphics, System.Drawing.SystemColors.Control, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal, Feng.Drawing.Orientation.Right);

            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }
        public virtual void DrawDownArrow(Feng.Drawing.GraphicsObject g)
        {

            Rectangle rects = this.Rect;

            Rectangle rectt = new Rectangle(rects.Right - rects.Height, rects.Top, rects.Height, rects.Height);

            GraphicsHelper.FillRectangleLinearGradient(g.Graphics, System.Drawing.SystemColors.ControlDarkDark, rectt);
 
            rectt.Inflate(-4, -3);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Left);
            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            GraphicsHelper.FillColorPath(g.Graphics, System.Drawing.SystemColors.Control, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal, Feng.Drawing.Orientation.Left);

            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

        }
        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this.Visible)
            {
                DrawArea(g);
                DrawThumdBack(g);
                DrawArrow(g);
                DrawThumd(g);
                DrawBorder(g);
            }
            return false;
        }
        private void DrawArrow(Feng.Drawing.GraphicsObject g)
        {
            DrawUpArrow(g);
            DrawDownArrow(g);
        }
        private void DrawArea(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = this.Rect;
            Color cc = System.Drawing.SystemColors.Control;// Color.FromArgb(100, Color.CornflowerBlue);
            SolidBrush sb = SolidBrushCache.GetSolidBrush(cc);
            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, sb, rect);
            
        }
        public virtual void DrawBorder(Feng.Drawing.GraphicsObject g)
        {
            return;
        }
        private void DrawThumdBack(Feng.Drawing.GraphicsObject g)
        {
            if (_scrollheight <= 0)
                return;
            Rectangle rect = new Rectangle(this.Left + this.Header + _scrolltop, this.Top, _scrollheight, this.Rect.Height);
            GraphicsHelper.FillRectangleLinearGradient(g.Graphics, System.Drawing.SystemColors.ControlDark, rect, System.Drawing.Drawing2D.LinearGradientMode.Vertical, true);
        }
        private void DrawThumd(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = new Rectangle(this.Left + this.Header + _scrolltop, this.Top, _scrollheight, this.Rect.Height);
            if (_scrollheight > 15)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {

                    int h = rect.Left + rect.Width / 2;

                    int x = -3;

                    PointF pt1 = new PointF(h - x, rect.Top + 2.5f);
                    PointF pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 3);
                    pt2 = new PointF(h - x, rect.Bottom - 3);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 2);
                    pt2 = new PointF(h - x, rect.Bottom - 2);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 3);
                    pt2 = new PointF(h - x, rect.Bottom - 3);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 2.5f);
                    pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                }
            }
            else if (_scrollheight > 6)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {
                    int h = rect.Left + rect.Width / 2;
                    int x = -1;
                    PointF pt1 = new PointF(h - x, rect.Top + 2.5f);
                    PointF pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(h - x, rect.Top + 2.5f);
                    pt2 = new PointF(h - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                }
            }
        }

        public virtual Rectangle UpArrowArea
        {
            get
            {
                //TODO:          return new Rectangle(rects.Left, rects.Top, rects.Width, rects.Width); }


                Rectangle rects = this.Rect;
                return new Rectangle(
                    rects.Left,
                    rects.Top,
                    rects.Height,
                    rects.Height);
            }
        }
        public virtual Rectangle DownArrowArea
        {
            get
            {

                //TODO: return new Rectangle(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);

                Rectangle rects = this.Rect;
                return new Rectangle(
                    rects.Right - rects.Height,
                    rects.Top,
                    rects.Height,
                    rects.Height);
            }
        }
        public virtual Rectangle ThumdArea
        {
            get
            {
                return new Rectangle(this.Left + this.Header + _scrolltop, this.Top, _scrollheight, this.Rect.Height);
            }
        }
 
        public virtual Rectangle UpPageArrowArea
        {
            get
            {
                //TODO:          return new Rectangle(rects.Left, rects.Top, rects.Width, rects.Width); }
                Rectangle rects = this.Rect;
                return new Rectangle(
                    rects.Left,
                    rects.Top,
                    rects.Height,
                    rects.Height);
            }
        }

        public virtual Rectangle DownPageArrowArea
        {
            get
            {
                //TODO: return new Rectangle(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);

                Rectangle rects = this.Rect;
                return new Rectangle(
                    rects.Right - rects.Height,
                    rects.Top,
                    rects.Height,
                    rects.Height);
            }
        }

        #endregion

        #region INext 成员
        public virtual void ProvPage()
        {
            _value = _value - _pagechang;
            _value = _value < _min ? _min : _value;
            ReSetScrollHeight(); 
        }
        public virtual void Prov()
        {
            _value -= _smallchange;
            _value = _value < _min ? _min : _value;
            ReSetScrollHeight(); 
        }
        public virtual void NextPage()
        {
            _value = _value + _pagechang;
            _value = _value > _max ? _max : _value;
            ReSetScrollHeight(); 
        }
        public virtual void Next()
        {
            _value += _smallchange;
            _value = _value > _max ? _max : _value;
            ReSetScrollHeight(); 
        }
        public virtual void Home()
        {
            _value = _min;
            ReSetScrollHeight(); 
        }
        public virtual void End()
        {
            _value = _max;
            ReSetScrollHeight(); 
        }

        #endregion

        #region IScroller 成员

        public virtual event Feng.EventHelper.BeforePositionChangedEventHandler BeforeValueChanged;
        public virtual event Feng.EventHelper.ValueChangedEventHandler ValueChanged;
        public virtual event Feng.EventHelper.ClickEventHandler Click;
        public virtual event Feng.EventHelper.ClickEventHandler ThumdAreaClick;
        public virtual event Feng.EventHelper.ClickEventHandler DownArrowAreaClick;
        public virtual event Feng.EventHelper.ClickEventHandler UpArrowAreaClick;

        #endregion

        #region ISmallChange 成员

        private int _smallchange = 1;
        public virtual int SmallChange
        {
            get { return _smallchange; }
            set { _smallchange = value; }
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

        #region IVisibleCount 成员

        private int _Visiblecount = 0;
        public virtual int VisibleCount
        {
            get { return _Visiblecount; }
            set
            {
                _Visiblecount = value;
                ReSetScrollHeight();
            }
        }

        #endregion

        #region IMax 成员
        
        private int _max = 1;
        public virtual int Max
        {
            get { return this._max; }
            set
            {
                this._max = value;
                ReSetScrollHeight();
            }
        }

        #endregion

        #region IMin 成员
        private int _min = 0;
        public virtual int Min
        {
            get { return this._min; }
            set { this._min = value; }
        }

        #endregion

        #region IClear 成员

        public virtual void Clear()
        {
            _min = 0;
            _max = 0;
            _smallchange = 1;
            _pagechang = 10;
            _value = 0;
            _scrollheight = 10;
            _scrolltop = 0;
        }

        #endregion

        #region IInt32Value 成员

        private int _value = 0;

        public virtual int Position
        {
            get { return this._value; }
            set
            {

                if (BeforeValueChanged != null)
                {
                    BeforePositionChangedArgs a = new BeforePositionChangedArgs();
                    a.Value = value;
                    BeforeValueChanged(this, a);
                    if (a.Cancel)
                    {
                        return;
                    }
                }
                this._value = value; 
                if (ValueChanged != null)
                {
                    ValueChanged(this, this._value);
                }
                ReSetScrollHeight();

            }
        }

        #endregion

        private bool _shownextpagebutton = false;
        public virtual bool ShowNextPageButton
        {
            get {
                return _shownextpagebutton;
            }
            set {
                _shownextpagebutton = value;
            }
        }
    }
}
