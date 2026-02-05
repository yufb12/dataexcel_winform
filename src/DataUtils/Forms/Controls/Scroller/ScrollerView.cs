using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;

using Feng.Args;
using Feng.Forms.Views;
using Feng.Data;

namespace Feng.Forms.Controls
{
     
    public class ScrollerView: DivView
    {
        public ScrollerView()
        {
        }
 
        #region 属性
        #region IBounds 成员
 
        #endregion
        private int _min = 0;
        private int _max = 100; 
        private int _thickness = 10;
        public virtual int Thickness
        {
            get {
                return _thickness;
            }
            set
            {
                _thickness = value;
            }
        }
        private int _scrolltop = 0;
        public int ScrollTop
        {
            get {
                return _scrolltop;
            }
            set {
                _scrolltop = value;
            }
        }
 
        public virtual int Header
        {
            get { return (int)this.Width; }
        }
        public virtual int Max
        {
            get { return this._max; }
            set
            {
                this._max = value;
                if (this._max < 1)
                {
                    this._max = 0;
                } 
                RefreshScrollThumd();
            }
        }
        public virtual int Min
        {
            get { return this._min; }
            set { this._min = value; }
        }
        private int _value = 0;

        public virtual int Value
        {
            get { return this._value; }
            set
            {
                if (value == this.Value)
                {
                    return;
                }
                if (value > this.Max )
                {
                    value = this.Max;
                }
                if (value < this.Min)
                {
                    value = this.Min;
                }
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
                this.OnValueChanged(this._value);
                RefreshScrollThumd();
                PropertyChanged();
            }
        }

        private int _smallchange = 1;
        public virtual int SmallChange
        {
            get { return _smallchange; }
            set { _smallchange = value; }
        }
        private int _largechange = 3;
        public virtual int LargeChange
        {
            get { return _largechange; }
            set
            {
                _largechange = value;
                RefreshScrollThumd();
                this.PropertyChanged();
            }
        }
              
        public override Rectangle Rect
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }


        private bool _visible = true;
        public virtual bool Visible
        {
            get {
                return this._visible;
            }
            set {
                this._visible = value;
            }

        }
        #endregion
        public virtual void ProvPage()
        {
            int value = _value - this.LargeChange;
            Value = value < _min ? _min : value;
        }
        public virtual void Prov()
        {
            int value = _value - _smallchange;
            Value = value < _min ? _min : value;
        }
        public virtual void NextPage()
        {
            int value = _value + this.LargeChange;
            Value = value > _max ? _max : value;
        }
        public virtual void Next()
        {
            int value = _value + _smallchange;
            Value = value > _max ? _max : value;
        }
        public virtual void Home()
        {
            Value = _min;
        }
        public virtual void End()
        {
            Value = _max;
        }
        public virtual void Clear()
        {
            _min = 0;
            _max = 0;
            _smallchange = 1; 
            _value = 0;
            _thickness = 10;
            _scrolltop = 0;
        }

        public virtual void RefreshScrollThumd()
        {
            int count = this.Count;
            if (count < 1)
            {
                count = 1;
            }
            int height = this.BodyArea.Height;
            int large = this.LargeChange;
            int thickness = (int)(1f * height * (1f / (count + 1) * 1f));
            float smallwidth = 1f * (height - thickness) / count;
          
            this.Thickness = (int)thickness;
            if (_thickness < 16) 
            {
                _thickness = 16;
            }
            int top= (int)((this.Value - this.Min) * smallwidth);
            if ((top + thickness) > height)
            {
                top = height - thickness;
            }
            this.ScrollTop = top; 
        }

        public virtual bool CheckInTop(Point pf)
        {
            Rectangle rect = new Rectangle(this.Left, this.Top, this.Rect.Width, 10);
            return rect.Contains(pf);
        }
        public virtual bool CheckInBoom(Point pf)
        {
            Rectangle rect = new Rectangle(this.Left, this.Rect.Bottom - 10, this.Rect.Width, 10);
            return rect.Contains(pf);
        }
 
        public virtual bool OnDraw(Graphics g)
        { 
            return false;
        }
        private bool _MoveSelected = false;
        public bool MoveSelected
        {
            get {
                return _MoveSelected && System.Windows.Forms.Control.MouseButtons == MouseButtons.Left;
            }
            set
            {
                _MoveSelected = value;
            }
        }
        private Point downpoint = Point.Empty;
        private  Size downsize = Size.Empty;
        public virtual void PropertyChanged()
        {

        }
 
        public virtual event Feng.EventHelper.BeforePositionChangedEventHandler BeforeValueChanged;
        public virtual event Feng.EventHelper.ValueChangedEventHandler ValueChanged; 
        public virtual event Feng.EventHelper.ClickEventHandler ThumdAreaClick;
        public virtual event Feng.EventHelper.ClickEventHandler DownArrowAreaClick;
        public virtual event Feng.EventHelper.ClickEventHandler UpArrowAreaClick;

        public virtual void OnValueChanged(int value)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, value);
            }
        }
        public virtual Rectangle BodyArea
        {
            get
            {
                return new Rectangle(this.Left, this.Top + this.Header, this.Width, this.Height - this.Header - this.Header);
            }
        }
        public virtual int Count
        {
            get
            {
                return this.Max - this.Min;
            }
        }
        public virtual void PointToIndex(Point pt)
        {
            int count = this.Count;
            int height = this.BodyArea.Height; 
            int large = this.LargeChange;
            int thickness = (int)(1f * height * large / count);
            float smallwidth = 1f * (height - thickness) / count;
            int pheight = pt.Y - this.Top - this.Header;

            int topcount = (int)(pheight / smallwidth);
            int i = topcount;

            this.Value = this.Min + i;

        }

        public virtual bool OnMouseMove(Point pt)
        {

            if (this.MoveSelected)
            {
                Point pd = GetMovePoint(pt, downsize);
                PointToIndex(pd);
                RefreshScrollThumd();
                return true;
            }
            return false;
        }

        public virtual Point GetMovePoint(Point pt, Size sf)
        {
            Point pd = new Point(pt.X, pt.Y - sf.Height); 
            return pd;
        }

        public virtual bool OnMouseClick(Point pt)
        {
            if (this.MoveSelected)
                return false; 
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
         
            return false;
        }
        public virtual bool OnMouseDown(Point pt)
        {
          
            if (this.Rect.Contains(pt))
            {
                if (ThumdArea.Contains(pt))
                {
                    downpoint = pt;
                    Point pth = Point.Round(ThumdArea.Location);
                    downsize = new Size(pt.X - pth.X, pt.Y - pth.Y);
                    this.MoveSelected = true;
                    if (ThumdAreaClick != null)
                    {
                        ThumdAreaClick(this, pt);
                    }
                    return true;
                }
                if (BodyArea.Contains(pt))
                {
                    PointToIndex(pt);
                }
                if (this.DownArrowArea.Contains(pt))
                {
                    this.Next();
                }
                if (this.UpArrowArea.Contains(pt))
                {
                    this.Prov();
                }
                return true;

            }
            return false;
        }
        public virtual bool OnMouseUp()
        {
            this.MoveSelected = false;
            return false;
        }

        public virtual Rectangle UpArrowArea
        {
            get
            {
                Rectangle rects = this.Rect;
                return new Rectangle(rects.Left, rects.Top, rects.Width, rects.Width);
            }
        }
        public virtual Rectangle DownArrowArea
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
                Rectangle rects = this.Rect;
                return new Rectangle(rects.Left, rects.Top + this.Header + _scrolltop, this.Rect.Width, _thickness);
            }
        }
 
    }

}
