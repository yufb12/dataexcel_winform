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
using Feng.Forms.Interface;

namespace Feng.Forms.Controls
{
      [ToolboxItem(false)]
    public class Input : System.Windows.Forms.Control 
    {
        public Input()
        {
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles(); 
        }
 
        protected override void WndProc(ref Message m)
        {

            OnWndProc(ref m);
            base.WndProc(ref m);
        }
        public bool OnWndProc(ref Message m)
        {

            if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_IME_SETCONTEXT && m.WParam.ToInt32() == 1)
            {
                Feng.Utils.UnsafeNativeMethods.ImmAssociateContext(this.Handle, this.Handle);
            }

            switch (m.Msg)
            {
                case Feng.Utils.UnsafeNativeMethods.WM_CHAR:
                    KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) 
                        | System.Windows.Forms.Control.ModifierKeys);
                    if (e.Modifiers == Keys.Control || e.Modifiers == Keys.Alt)
                    {
                        break;
                    }
                    char text = (char)e.KeyData;
                    this.BeginReFresh();
                    InsertText(e, text);
                    this.EndReFresh();
                    break;
                case Feng.Utils.UnsafeNativeMethods.WM_PASTE:
                    this.Paste();
                    break;
                case Feng.Utils.UnsafeNativeMethods.WM_IME_CHAR:
                    if (m.WParam.ToInt32() == Feng.Utils.UnsafeNativeMethods.PM_REMOVE) //如果不做这个判断.会打印出重复的中文 
                    {
                        StringBuilder str = new StringBuilder();
                        MessageBox.Show(str.ToString());
                    }
                    break; 
            }
            return false;
        }

        public int StartIndex { get; set; }
        public int SelectLength { get; set; }

        public void InsertText(KeyEventArgs e, char text)
        {

        }

        private int _BeginReFresh = 100;
        public virtual void BeginReFresh()
        {
            _BeginReFresh++;
        }
        private int _EndReFresh = 0;
        public virtual void EndReFresh()
        {
            _EndReFresh++;
            this.RePaint();
        }

        public virtual void BeginReFresh(RectangleF rect)
        {
            _region.Union(rect);
            _BeginReFresh++;
        }
        public virtual void EndReFresh(RectangleF rect)
        {
            _EndReFresh++;
            this.RePaint(rect);
        }
        private System.Drawing.Region _region = new Region();
        public virtual void RePaint(RectangleF rect)
        { 
            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate(_region);
                _region.MakeEmpty();
                _BeginReFresh = _EndReFresh = 0;
            }

        }

        public virtual void RePaint()
        {
            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate(true);
                _BeginReFresh = _EndReFresh = 0;
            }
        }
        public void Paste()
        {

        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            try
            { 
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                OnDraw(g);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnPaint(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseDown(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnSizeChanged(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        { 
            base.OnMouseWheel(e);
        }
 
        public void OnDraw(Graphics g)
        {
            if (System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                GraphicsHelper.FillRectangleLinearGradient(g, Color2, Color1, 0, 0, this.Width, this.Height, GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
            }
            else
            {
                Point pt = this.PointToClient(System.Windows.Forms.Control.MousePosition);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                if (rect.Contains(pt))
                {
                    GraphicsHelper.FillRectangleLinearGradient(g, Color.FromArgb(Color1.A, Color1.R, Color1.G, (byte)(Color1.B + 128)), Color2, 0, 0, this.Width, this.Height, GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
                }
                else
                {
                    GraphicsHelper.FillRectangleLinearGradient(g, Color1, Color2, 0, 0, this.Width, this.Height, GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
                }
            }

            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), g.ClipBounds, GenericDefault);
        }
        public void PropertyChanged()
        {
        } 
        private StringAlignment _alignment = StringAlignment.Near;
        public virtual StringAlignment Alignment
        {
            get { return _alignment; }
            set
            {
                _alignment = value;
                 GenericDefault.Alignment = this.Alignment;
                PropertyChanged();
            }
        }
        private StringAlignment _lineAlignment = StringAlignment.Center;
        public virtual StringAlignment LineAlignment
        {
            get { return _lineAlignment; }

            set
            {
                _lineAlignment = value;
                 GenericDefault.LineAlignment = this.LineAlignment;
                PropertyChanged();
            }
        }
        private StringFormat _GenericDefault;
        private StringFormat GenericDefault
        {
            get
            {
                if (_GenericDefault == null)
                {
                    _GenericDefault = StringFormat.GenericDefault.Clone() as StringFormat;
                    _GenericDefault.Alignment = this.Alignment;
                    _GenericDefault.LineAlignment = this.LineAlignment;
                }
                return _GenericDefault;
            }
        }
        private Color _color1 = Color.White;
        [Category(CategorySetting.PropertyDesign)]
        public Color Color1
        {
            get { return _color1; }
            set { _color1 = value; }
        }
        private Color _color2 = Color.Lavender;
        [Category(CategorySetting.PropertyDesign)]

        public Color Color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }
        private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
        [Category(CategorySetting.PropertyDesign)]
        public LinearGradientMode GradientMode
        {
            get { return _GradientMode; }
            set { _GradientMode = value; }
        }
        private bool _drawborder = true;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(true)]
        public bool DrawBorder
        {
            get { return _drawborder; }
            set { _drawborder = value; }
        }
        private int _borderwidth = 1;
        [DefaultValue(1)]
        [Category(CategorySetting.PropertyDesign)]
        public int BorderWidth
        {
            get { return _borderwidth; }
            set { _borderwidth = value; }
        }
        private Color _bordercolor = Color.DarkGray;
        [Category(CategorySetting.PropertyDesign)]
        public Color BorderColor
        {
            get { return _bordercolor; }
            set { _bordercolor = value; }
        }
        private int _radius = 6;

        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(6)]
        public int Radius
        {

            get { return _radius; }
            set { _radius = value; }
        }

        private CharViewCollection _Chars = null;
        private CharViewCollection Chars {
            get {
                if (_Chars == null)
                {
                    _Chars = new CharViewCollection();
                }
                return _Chars;
            }
        }
        
    }


    public class CharViewCollection : IList<CharView>
    {
        private List<CharView> list = new List<CharView>();
        public int IndexOf(CharView item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, CharView item)
        {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        public CharView this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
            }
        }

        public void Add(CharView item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(CharView item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(CharView[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get {
                return this.list.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CharView item)
        {
            return this.Remove(item);
        }

        public IEnumerator<CharView> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class CharView : IDraw
    {
        public bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            return true;
        }
    } 

    public class TextLineViewCollection : IList<TextLineView>
    {
        private List<TextLineView> list = new List<TextLineView>();
        public int IndexOf(TextLineView item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TextLineView item)
        {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        public TextLineView this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
            }
        }

        public void Add(TextLineView item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(TextLineView item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(TextLineView[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TextLineView item)
        {
            return this.Remove(item);
        }

        public IEnumerator<TextLineView> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class TextLineView : IDraw
    {

        public bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            return true;
        }
    }
 
}
