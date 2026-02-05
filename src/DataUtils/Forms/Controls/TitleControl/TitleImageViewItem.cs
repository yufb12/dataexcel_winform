using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using Feng.Drawing;

namespace Feng.Forms.Views
{ 
    public class TitleImageViewItem:IDraw,ISelected,IBounds,IText,ITitleImage,IBackColor,ISelectColor,IFont,IForeColor
    {
        public TitleImageViewItem()
        {

        }
 
        public bool OnDraw(object sender, GraphicsObject g)
        {
            DrawBack(g);
            Draw(sender, g);
            return true;
        }
        public void Draw(object sender, GraphicsObject g)
        {
            Color color = this.ForeColor;
            if (this.Selected)
            {
                color = this.SelectForceColor;
            }
            Brush sb = Feng.Drawing.SolidBrushCache.GetSolidBrush(color);
            if (this.Image != null)
            {
                Feng.Drawing.GraphicsHelper.DrawImage(g.Graphics, this.Image, new Rectangle(this.Left + 2, this.Top + 2, this.Height - 4, this.Height),
                     ImageLayout.Center);
            }
            Feng.Drawing.GraphicsHelper.DrawString(g, this.Text, this.Font, sb, 
                new RectangleF(this.Left+this.Height,this.Top,this.Width-this.Height,this.Height)
                );
        }
        public void DrawBack(GraphicsObject g)
        {

            Color backcolor = this.BackColor;
            if (this.Selected)
            {
                backcolor = this.SelectBackColor;
            }
            Brush sb = Feng.Drawing.SolidBrushCache.GetSolidBrush(backcolor);
            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, sb, this.Rect);
        }

        private TitleImageView _parent = null;
        public TitleImageView Parnt
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = value;
            }
        }
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
            }
        }

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
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public virtual int Right { get {
                return Left + Width;
            } }

        public virtual int Bottom {
            get {
                return Top + Height;
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
        public virtual int Width { get {
                return _width;
            } set {
                _width = value;
            }
        }

        private int _MaxWidth = 0;
        public virtual int MaxWidth
        {
            get
            {
                return _MaxWidth;
            }
            set
            {
                _MaxWidth = value;
            }
        }

        public virtual Rectangle Rect {
            get {
                return new Rectangle(Left, Right, Width, Height);
            }
        }

        private string _text = string.Empty;
        public virtual string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        private Image _image = null;
        public virtual Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        private Color _BackColor = Color.White;
        public Color BackColor { get { return _BackColor; } set { _BackColor = value; } }

        private Color _SelectBackColor = Color.AntiqueWhite;
        public Color SelectBackColor { get { return _SelectBackColor; } set { _SelectBackColor = value; } }

        private Color _SelectForceColor = Color.Black;
        public Color SelectForceColor { get { return _SelectForceColor; } set { _SelectForceColor = value; } }

        private Font _Font = null;
        public Font Font { get { return _Font; } set { _Font = value; } }

        private Color _ForeColor = Color.Black;
        public Color ForeColor { get { return _ForeColor; } set { _ForeColor = value; } }
    }

}
