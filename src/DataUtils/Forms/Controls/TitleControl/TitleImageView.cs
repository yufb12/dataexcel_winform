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

namespace Feng.Forms.Views
{
    public class TitleImageView : IDraw,IFont,IMouseDown
    {
        public TitleImageView()
        {

        }
        public Control Control { get; set; }
        public bool OnDraw(object sender, GraphicsObject g)
        {
            foreach (TitleImageViewItem item in this.Items)
            {
                item.OnDraw(this, g);
            }
            return true;
        }
        public virtual void RefreshItemSize()
        {
            if (this.Items.Count > 0)
            {
                int swidth = this.Width / this.Items.Count;
                int left = this.Left;
                foreach (TitleImageViewItem item in this.Items)
                {
                    item.Left = left;
                    if (swidth > item.MaxWidth)
                    {
                        item.Width = item.MaxWidth;
                    }
                    else
                    {
                        item.Width = swidth;
                    }
                    item.Top = this.Top;
                    item.Height = this.Height;
                    left = left + item.Width;
                }
            }
        }
        private TitleImageViewItemCollection _items = null;
        public TitleImageViewItemCollection Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new TitleImageViewItemCollection();
                }
                return _items;
            }
        }
        public TitleImageViewItem AddItem(string text)
        {
            TitleImageViewItem item = new TitleImageViewItem();
            item.Font = this.Font;
            item.Text = text;
            this.Items.Add(item);
            return item;
        }
        public TitleImageViewItem AddItem(string text,Image image)
        {
            TitleImageViewItem item = new TitleImageViewItem();
            item.Font = this.Font;
            item.Text = text;
            item.Image = image;
            this.Items.Add(item);
            return item;
        }
        private Font _Font = null;
        public Font Font { get { return _Font; } set { _Font = value; } }
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

        public virtual int Right
        {
            get
            {
                return Left + Width;
            }
        }

        public virtual int Bottom
        {
            get
            {
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
                return new Rectangle(Left, Right, Width, Height);
            }
        }

        public virtual void MouseClick()
        {

        }

        public bool MouseDown(object sender, MouseEventArgs e)
        {
            foreach (TitleImageViewItem item in Items)
            {
                if (item.Rect.Contains(e.Location))
                {
                    return true;
                }
            }
            return false;
        }
    }

}
