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

namespace Feng.Forms.Controls
{
      [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class Series : Component 
    {
        public Series()
        { 
        }
        public Series(string text)
        {
            this.Text = text;
        }
        public Series(Chart toolbar)
        {
            _toolbar = toolbar;
        }
 
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
 
        }
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
 
        }
        protected virtual void OnMouseLeave(EventArgs e)
        {
 
        }
        protected virtual void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
 
        }

        protected virtual void OnSizeChanged(EventArgs e)
        {
 
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {  
        }
        private Chart _toolbar = null;
        public Chart ToolBar
        {
            get {
                return _toolbar;
            }
            set {
                _toolbar = value;
            }
        }
        private int _left = 0;
        private int _top = 0;
        private int _width = 72;
        private int _height = 40;

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

        public int Left {
            get {
                return _left;
            }
            set {
                _left = value;
            }
        }
 
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
        public Rectangle Rect {
            get {
                Rectangle rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                return rect;
            }
        }
        public virtual void OnDrawText(Feng.Drawing.GraphicsObject g)
        {
            GraphicsHelper.DrawText(g.Graphics, this.ToolBar.Font, this.Text, this.ForeColor, this.Rect, 4);
        }
        public virtual void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            OnDrawBack(g);
            OnDrawText(g);
        }
        public virtual void OnDrawBack(Feng.Drawing.GraphicsObject g)
        {
            Point pt = this.ToolBar.PointToClient(System.Windows.Forms.Control.MousePosition);
            Rectangle rect = this.Rect;
            if (rect.Contains(pt))
            {
                GraphicsHelper.FillRectangleLinearGradient(g.Graphics, Color.FromArgb(Color1.A, Color1.R, Color1.G, (byte)(Color1.B + 128)), Color2, 0, 0, this.Width, this.Height, GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
            }
            else
            {
                GraphicsHelper.FillRectangleLinearGradient(g.Graphics, Color1, Color2, this.Left, this.Top, this.Width, this.Height, GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
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
        private int _radius = 0;

        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(6)]
        public int Radius
        {

            get { return _radius; }
            set { _radius = value; }
        }

        private Color _forecolor = Color.Black;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(typeof(Color), "Black")]
        public Color ForeColor
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

        private string _text = string.Empty;
 
        [Browsable(true)] 
        public string Text
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
    }
 
}
