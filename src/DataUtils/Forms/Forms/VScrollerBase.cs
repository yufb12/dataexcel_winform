using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using Feng.Drawing;
using Feng.Forms.Interface;
using Feng.Args;
namespace Feng.Forms
{
    [Serializable]
    public class VScrollerBase : IScrollerBase
    {
        public VScrollerBase()
        {
        }

        private int _min = 0;
        private int _max = 1;
        private int _pagechang = 10;
        private int _scrollheight = 10;
        private int _scrolltop = 0;
        private int Header
        {
            get { return (int)this.Width; }
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
                this._position = value;
                ReSetScrollHeight();
                OnValueChanged(this._position);
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
        public virtual void ProvPage()
        {
            Position = _position - _pagechang;

        }
        public virtual void Prov()
        {
            Position = _position - _smallchange;

        }
        public virtual void NextPage()
        {
            Position = _position + _pagechang;

        }
        public virtual void Next()
        {
            Position = _position + _smallchange;
        }
        public virtual void Home()
        {
            Position = _min;
        }
        public virtual void End()
        {
            Position = _max;
        }
        public virtual void Clear()
        {
            _min = 0;
            _max = 100;
            _smallchange = 1;
            _pagechang = 10;
            _position = 0;
            _scrollheight = 10;
            _scrolltop = 0;
        }

        public virtual void OnValueChanged(int value)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, value);
            }
        }

        public virtual void ReSetScrollHeight()
        {
            _position = _position > this.Max ? this.Max : _position;
            _position = _position < this.Min ? this.Min : _position;
            int area = (this.Height - Header * 2);
            int count = this.Max - this.Min;
            if (count < 1)
                count = 1;
            int scrollheight = (area / count);
            _scrollheight = scrollheight < 13 ? 13 : scrollheight;
            scrollheight = (area - _scrollheight) / count;
            _scrolltop = scrollheight * (_position - this._min);
        }

        public virtual bool CheckInTop(Point pf)
        {
            Rectangle rect = new Rectangle(0, 0, this.Rect.Width, 10);
            return rect.Contains(pf);
        }
        public virtual bool CheckInBoom(Point pf)
        {
            Rectangle rect = new Rectangle(0, this.Rect.Bottom - 10, this.Rect.Width, 10);
            return rect.Contains(pf);
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
        private void DrawArrow(Feng.Drawing.GraphicsObject g)
        {
            DrawUpArrow(g);
            DrawPageUpArrow(g);
            DrawDownArrow(g);
            DrawPageDownArrow(g);
        }
        private void DrawArea(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = this.Rect;
            SolidBrush sb = SolidBrushCache.GetSolidBrush(BackAreaColor);
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
            Rectangle rect = new Rectangle(this.Left, this.Top + this.Header + _scrolltop, this.Rect.Width, _scrollheight);
            GraphicsHelper.FillRectangleLinearGradient(g.Graphics, ThumdBackColor, rect, true);

        }
        private void DrawThumd(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = new Rectangle(this.Left, this.Top + this.Header + _scrolltop, this.Rect.Width, _scrollheight);
            if (_scrollheight > 15)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {

                    int x = -3;
                    PointF pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    PointF pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 3, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 3, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 3, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 3, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
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
                    g.Graphics.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.Graphics.DrawLine(p, pt1, pt2);
                }
            }
        }
        public virtual void DrawUpArrow(Feng.Drawing.GraphicsObject g)
        {
            int height = 0;
            if (this.ShowNextPageButton)
            {
                height = this.Width;
            }
            Rectangle rect = new Rectangle(this._left, this._top + height, this.Width, this.Width);
            GraphicsHelper.DrawUpArrowButton(g.Graphics, rect, this.BackDirectionColor, this.ArrowColor); 
        }

        public virtual void DrawDownArrow(Feng.Drawing.GraphicsObject g)
        {
            int height = 0;
            if (this.ShowNextPageButton)
            {
                height = this.Width;
            }
            Rectangle rect = new Rectangle(this._left, this.Bottom - this.Width - height, this.Width, this.Width);
            GraphicsHelper.DrawDownArrowButton(g.Graphics, rect, this.BackDirectionColor, this.ArrowColor);
        }


        public virtual void DrawPageUpArrow(Feng.Drawing.GraphicsObject g)
        {
            GraphicsHelper.DrawPageUpArrowButton(g.Graphics, this.Rect, this.BackDirectionColor, this.ArrowColor);
        }
        public virtual void DrawPageDownArrow(Feng.Drawing.GraphicsObject g)
        {
            GraphicsHelper.DrawPageDownArrowButton(g.Graphics, this.Rect, this.BackDirectionColor, this.ArrowColor);
        }
        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this.Visible)
            {
                System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
                g.Graphics.SetClip(this.Rect);
                DrawArea(g);
                DrawThumdBack(g);
                DrawThumd(g);
                DrawBorder(g);
                DrawArrow(g);
                g.Graphics.Restore(gs);
            }
            return false;
        }


        private PointF downpoint = PointF.Empty;


        public virtual event Feng.EventHelper.BeforePositionChangedEventHandler BeforeValueChanged;
        public virtual event Feng.EventHelper.ValueChangedEventHandler ValueChanged;
        public virtual event Feng.EventHelper.ClickEventHandler Click;
        public virtual event Feng.EventHelper.ClickEventHandler ThumdAreaClick;
        public virtual event Feng.EventHelper.ClickEventHandler DownArrowAreaClick;
        public virtual event Feng.EventHelper.ClickEventHandler UpArrowAreaClick;

        public virtual event Feng.EventHelper.ClickEventHandler DownPageArrowAreaClick;
        public virtual event Feng.EventHelper.ClickEventHandler UpPageArrowAreaClick;
        private void PointToIndex(Point pt)
        {

            int i = 0;
            int c = this._max;// -this._Visiblecount;
            int h = this.Rect.Height - this.Header * 2;
            int hc = h / c;
            int hp = pt.Y - this._top - this.Header;
            i = (int)Math.Ceiling(hp / hc * 1m);

        }


        public virtual Rectangle UpArrowArea
        {
            get
            {
                Rectangle rects = this.Rect;
                int height = 0;
                if (this.ShowNextPageButton)
                {
                    height = rects.Width;
                }
                return new Rectangle(rects.Left, rects.Top + height, rects.Width, rects.Width);
            }
        }
        public virtual Rectangle DownArrowArea
        {
            get
            {
                Rectangle rects = this.Rect;
                int height = 0;
                if (this.ShowNextPageButton)
                {
                    height = rects.Width;
                }
                return new Rectangle(rects.Left, rects.Bottom - rects.Width - height, rects.Width, rects.Width);
            }
        }


        public virtual Rectangle UpPageArrowArea
        {
            get
            {
                Rectangle rects = this.Rect;
                return new Rectangle(rects.Left, rects.Top, rects.Width, rects.Width);
            }
        }
        public virtual Rectangle DownPageArrowArea
        {
            get
            {
                Rectangle rects = this.Rect;
                return new Rectangle(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);
            }
        }

        public virtual Rectangle ThumdArea
        {
            get
            {

                return new Rectangle(this.Left, this.Top + this.Header + _scrolltop, this.Rect.Width, _scrollheight);
            }
        }

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
                return this._height;
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

                return _width + _left;
            }

        }
        public virtual int Bottom
        {
            get
            {

                return _top + this.Height;
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

        private int _width = 15;
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
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
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

        public virtual bool OnCellMouseDown(MouseEventArgs e)
        {
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
            if (UpPageArrowArea.Contains(pt))
            {
                this.ProvPage();
                if (UpPageArrowAreaClick != null)
                {
                    UpPageArrowAreaClick(this, pt);
                }
                return true;
            }
            if (DownPageArrowArea.Contains(pt))
            {
                this.NextPage();
                if (DownPageArrowAreaClick != null)
                {
                    DownPageArrowAreaClick(this, pt);
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

        public virtual bool OnCellMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)
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
            if (UpPageArrowArea.Contains(pt))
            {
                return true;
            }
            if (DownPageArrowArea.Contains(pt))
            {
                return true;
            }
            if (this.Rect.Contains(pt))
            {
                return true;
            }
            return false;
        }

        public virtual bool OnCellMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            Point pt = e.Location;
            if (this.Rect.Contains(pt))
            {
                return true;
            }
            if (UpArrowArea.Contains(pt))
            {
                return true;
            }
            if (DownArrowArea.Contains(pt))
            {
                return true;
            }
            if (UpPageArrowArea.Contains(pt))
            {
                return true;
            }
            if (DownPageArrowArea.Contains(pt))
            {
                return true;
            }
            if (ThumdArea.Contains(pt))
            {
                return true;
            }

            return false;
        }

        private short _SrollStep = 3;
        [DefaultValue(3)]
        [Browsable(true)]
        public virtual short ScrollStep
        {
            get { return _SrollStep; }

            set { this._SrollStep = value; }
        }

        public virtual bool OnCellMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            int numberOfTextLinesToMove = -1 * e.Delta * this.ScrollStep / 120;

            if (numberOfTextLinesToMove > 0)
            {
                numberOfTextLinesToMove = this._smallchange;
            }
            else
            {
                numberOfTextLinesToMove = this._smallchange * -1;
            }
            this.Position = this.Position + numberOfTextLinesToMove;
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


        public bool OnCellClick(EventArgs e)
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


        private bool _shownextpagebutton = false;
        public virtual bool ShowNextPageButton
        {
            get
            {
                return _shownextpagebutton;
            }
            set
            {
                _shownextpagebutton = value;
            }
        }
    }
}
