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
using Feng.Data;
using Feng.Forms.Interface;

namespace Feng.Forms.Views
{  

    public class ToolBarItemView : IDraw, IText, IRect
    {
        public ToolBarItemView(ToolBarView toolbar)
        {
            _ToolBar = toolbar;
        }
        private ToolBarView _ToolBar = null;
        public ToolBarView ToolBar
        {
            get
            {
                return _ToolBar;
            }
        }
        #region IBounds 成员
        [Browsable(false)]
        public virtual int Height
        {
            get
            {
                return this.ToolBar.Height;
            }
            set
            {
            }
        }
        [Browsable(false)]
        public virtual int Right
        {
            get { return this._left + this.Width; }
        }
        [Browsable(false)]
        public virtual int Bottom
        {
            get { return this.Top + this.Height; }
        }

        private int _left = 0;
        [Browsable(false)]
        public virtual int Left
        {
            get
            {
                return _left;
            }
            set { _left = value; }

        }

        [Browsable(false)]
        public virtual int Top
        {
            get
            {
                return this.ToolBar.Top;
            }
            set
            {
            }
        }

        private int _width = 0;
        [Browsable(false)]
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
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual Rectangle Rect
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }

        #endregion

        public int ItemImageWidth = 20;
        public void DrawFocuseBack(GraphicsObject g)
        {
            if (this.ToolBar.FocusedItem == this)
            {
                Rectangle imageRect = new Rectangle(this.Left, this.Top, this.Height, this.Height);
                //g.Graphics.FillRectangle(SolidBrushCache.GetSolidBrush(this.ToolBar.FocusBackColor), Rect);
                using (Brush brush = new LinearGradientBrush(Rect, ColorHelper.Light(this.ToolBar.FocusBackColor), this.ToolBar.FocusBackColor, LinearGradientMode.Horizontal))
                {
                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, Rect);
                }
            }
        }
        public virtual bool OnDraw(object sender, GraphicsObject g)
        {
 
            int textwidth = this.Width;
            int textleft = this.Left;
            DrawFocuseBack(g);
            if (this.Image != null)
            {
                Rectangle imageRect = new Rectangle(this.Left, this.Top, this.Height, this.Height);
                int imageheight = ItemImageWidth;
                if (imageheight > this.Image.Height)
                {
                    imageheight = this.Image.Height;
                }
                int imagewidth = ItemImageWidth;
                if (imagewidth > this.Image.Width)
                {
                    imagewidth = this.Image.Width;
                }
                imageRect = new Rectangle(this.Left + 5, (this.Height - imageheight) / 2, imagewidth, this.Image.Height);
                //g.Graphics.DrawImage(this.Image, imageRect);
                GraphicsHelper.DrawImage(g.Graphics, this.Image, imageRect, ImageLayout.Stretch);
                textwidth = this.Width - imageRect.Width - 2;
                textleft = textleft + imageRect.Width + 5 * 2;
            }
            if (ShowCaption)
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    Rectangle textRect = new Rectangle(textleft, this.Top, textwidth - 5, this.Height);
                    Color forcecolor = Color.Black;// this.ToolBar.ForeColor;
                    if (this.ToolBar.FocusedItem == this)
                    {
                        forcecolor = Color.White;// GraphicsHelper.GetColorInstead(Color.White);
                    }
                    Feng.Drawing.GraphicsHelper.DrawString(g,this.Text, this.ToolBar.Font, SolidBrushCache.GetSolidBrush(forcecolor), textRect, StringFormatCache.GetStringFormatAlignLeftNoWrap());
                }
            }
            if (ShowSplitLine)
            {
                GraphicsHelper.DrawBorderRight(g.Graphics, this.Rect);
            }
            return true;
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

        private Image _Image = null;
        public virtual Image Image
        {
            get
            {
                return _Image;
            }

            set
            {
                _Image = value;
            }
        }

        private bool _showcatipin = true;
        public virtual bool ShowCaption
        {
            get
            {
                return _showcatipin;
            }
            set { _showcatipin = value; }
        }
        private bool _showsplitline = true;
        public virtual bool ShowSplitLine
        {
            get
            {
                return _showsplitline;
            }
            set { _showsplitline = value; }
        }

        public int CalculateWidth()
        {
            Size size = GraphicsHelper.Sizeof(this.Text, this.ToolBar.Font, this.ToolBar.Control.CreateGraphics());
            int width = 0;
            if (this.Image != null)
            {
                width = this.Height;
            }
            width = width + size.Width;
            return width;
        }
    }
}
