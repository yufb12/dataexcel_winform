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
    [DesignTimeVisible(false)]
    public class ToolBarItem
    {
        public ToolBarItem()
        {
        }
        public ToolBarItem(string text)
        {
            this._text = text;
        }
        public ToolBarItem(string text, string id)
        {
            this._text = text;
            this._id = id;
        }
        public ToolBarItem(string text, Image img)
        {
            this._text = text;
            this._image = img;
        }
        public ToolBarItem(string text, string id, Image img)
        {
            if (img != null)
            {

            }
            this._text = text;
            this._image = img;
            this._id = id;
        }
        public ToolBarItem(string text, string id, Image img, bool showtext, bool showimage)
        {
            this._text = text;
            this._image = img;
            this._id = id;
            this._showimage = showimage;
            this._showtext = showtext;

        }
        public ToolBarItem(string text, Image img, bool showtext, bool showimage)
        {
            this._text = text;
            this._image = img;
            this._showimage = showimage;
            this._showtext = showtext;

        }

        public ToolBarItem(ToolBar toolbar)
        {
            _toolbar = toolbar;
            this.Height = toolbar.Height;
        }
        public ToolBarItem(string text, ToolBarItemClickHandler toolBarItemClick)
        {
            this._text = text;
            this.ItemClick = toolBarItemClick;
        }
        public ToolBarItem(string text, Image img, ToolBarItemClickHandler toolBarItemClick)
        {
            this._text = text;
            this._image = img;
            this.ItemClick = toolBarItemClick;
        }

        private ToolBarItemCollection _items = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        public ToolBarItemCollection Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ToolBarItemCollection(this.ToolBar);
                }
                return _items;
            }
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {

        }
        public virtual void OnMouseMove(MouseEventArgs e)
        {

        }
        public virtual void OnMouseLeave(EventArgs e)
        {

        }
        public virtual void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {

        }

        public virtual void OnSizeChanged(EventArgs e)
        {

        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
        }
        private ToolBar _toolbar = null;
        public ToolBar ToolBar
        {
            get
            {
                return _toolbar;
            }
            set
            {
                _toolbar = value;
                this.Height = value.Height;
            }
        }
        private int _left = 0;
        private int _top = 0;
        private int _width = 52;
        private int _height = 25;
        private int _textwidth = 40;
        private bool _showtooltip = true;
        private string _tooltip = string.Empty;
        public virtual string ToolTip
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_tooltip))
                {
                    return this.Text;
                }
                return this._tooltip;
            }
            set {
                this._tooltip = value;
            }
        }
        public virtual bool ShowToolTip
        {
            get { return _showtooltip; }
            set { _showtooltip = value; }
        }
        public virtual int TextWidth
        {
            get { return _textwidth; }
            set { _textwidth = value; }
        }
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

        public Rectangle Rect
        {
            get
            {
                Rectangle rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                return rect;
            }
        }

        private int _minwidth = 20;
        public virtual int MinWidth
        {
            get
            {
                return _minwidth;
            }
            set
            {
                _minwidth = value;
            }
        }

        public Rectangle GetCloseRect()
        {
            if (!this.ShowClose)
                return Rectangle.Empty;
            if (this.Rect.Width > this.ToolBar.ImageSize * 3)
            {

                Rectangle rect = new Rectangle();
                rect.X = this.Left + this.Width - this.ToolBar.ImageSize - 10;
                rect.Y = rect.Top + 4;
                rect.Width = this.ToolBar.ImageSize - 4;
                rect.Height = this.ToolBar.ImageSize - 4;
                if (this.ToolBar.ImageSize < this.ToolBar.ToolBarHeight)
                {
                    rect.Y = this.Rect.Top + (this.ToolBar.ToolBarHeight - this.Image.Height) / 2 + 4;
                }
                return rect;

            }
            return Rectangle.Empty;

        }


        public virtual void OnDrawText(Feng.Drawing.GraphicsObject g)
        {
            if (this.ShowText)
            {
                Rectangle rect = this.Rect;
                int imageleft = 0;
                int closewidth = 0;
                if (this.ShowImage)
                {
                    imageleft = this.ToolBar.ImageSize;
                }
                if (this.ShowClose)
                {
                    closewidth = this.ToolBar.ImageSize ;
                }
                int w = 2;
                //if (this.ToolBar.FocusItem == this)
                //{
                //    w = 6;
                //}
                rect.X = rect.Left + imageleft + w;
                rect.Y = rect.Top + 1;
                rect.Width = rect.Width - imageleft - closewidth - w;
                if (this.ToolBar != null)
                {
                    this.ToolBar.Skin.DrawText(g.Graphics, rect, this);
                }
            }
        }
        public virtual void OnDraw(Feng.Drawing.GraphicsObject g)
        { 
            OnDrawBack(g);

            OnDrawImage(g);
            OnDrawCloseImage(g);
            OnDrawText(g);
        }
        public virtual void OnDrawBack(Feng.Drawing.GraphicsObject g)
        {
            Point pt = g.ClientPoint;
            Rectangle rect = this.Rect;
            //rect.X = rect.X + 2;
            //rect.Y = rect.Y + 2;
            //rect.Width = rect.Width - 4;
            //rect.Height = rect.Height - 4;
            if (this.ToolBar != null)
            {
                this.ToolBar.Skin.DrawBack(g.Graphics, rect, rect.Contains(pt), false);
            }
 
        }
        public virtual void OnDrawImage(Feng.Drawing.GraphicsObject g)
        {
            if (this.ShowImage)
            {
                if (this.Image == null)
                    return;
                Rectangle rect = this.Rect;
                int w = 2;
                //if (this.ToolBar.FocusItem == this)
                //{
                //    w = 6;
                //}
                rect.X = rect.Left + w;
                rect.Y = rect.Top + 4;
                rect.Width = this.ToolBar.ImageSize - 4;
                rect.Height = this.ToolBar.ImageSize - 4;
                if (this.ToolBar.ImageSize < this.ToolBar.ToolBarHeight)
                {
                    rect.Y = this.Rect.Top + (this.ToolBar.ToolBarHeight - this.Image.Height) / 2+4;
                }
                if (this.ToolBar != null)
                {
                    this.ToolBar.Skin.DrawImage(g.Graphics, rect, this);
                }
            }
        }
        public virtual void OnDrawCloseImage(Feng.Drawing.GraphicsObject g)
        {
            if (this.ShowClose)
            {
                if (this.CloseImage == null)
                    return;
                Point pt = g.ClientPoint;
                if (this.ToolBar.FocusItem == this)
                {
                    Rectangle rect = this.GetCloseRect();
                    //Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, Brushes.Red, rect);
                    if (this.ToolBar != null)
                    {
                        if (rect.Contains(pt))
                        {
                            g.Graphics.FillRectangle(Brushes.LightGray, rect);
                        }
                        this.ToolBar.Skin.DrawCloseImage(g.Graphics, rect, this);
                    }
                }
                else
                {
                    Rectangle rect = this.GetCloseRect();
                    Rectangle rectm = this.Rect;
              
                    if (rect.Contains(pt))
                    {
                        g.Graphics.FillRectangle(Brushes.LightGray, rect);
                        this.ToolBar.Skin.DrawCloseImage(g.Graphics, rect, this);
                    }
                }
            }
        }
        private string _text = string.Empty;


        [Browsable(true)]
        public virtual string Text
        {
            get
            {
                return this._text;
            }

            set
            {
                this._text = value;
                if (this.ToolBar != null)
                {
                    this.ToolBar.OnToolBarItemChanged(this, Feng.Forms.Controls.GridControl.ChangedReason.ValueChanged);
                }
            }
        }
        private string _id = string.Empty;


        [Browsable(true)]
        public virtual string ID
        {
            get
            {
                return this._id;
            }

            set
            {
                this._id = value; 
            }
        }

        private Image _image = null;
        public virtual Image Image
        {
            get
            {
                return this._image;
            }
            set
            {
                this._image = value;
            }
        }

        private Image _closeimage = null;
        public virtual Image CloseImage
        {
            get
            {
                return this._closeimage;
            }
            set
            {
                this._closeimage = value;
            }
        }

        private bool _showimage = true;
        public virtual bool ShowImage
        {
            get { return _showimage; }
            set { _showimage = value; }
        }

        private bool _showtext = true;
        public virtual bool ShowText
        {
            get { return _showtext; }
            set { _showtext = value; }
        }


        private bool _showclose = false;
        public virtual bool ShowClose
        {
            get { return _showclose; }
            set { _showclose = value; }
        }

        public override string ToString()
        {
            return this.Text;
        }

        private bool _alignright = false;
        public virtual bool AlignRight {
            get {
                return _alignright;
            }
            set
            {
                _alignright = value;
            }
        }

        private bool _visable = true;
        public virtual bool Visable
        {
            get
            {
                return _visable;
            }
            set
            {
                _visable = value;
            }
        }

        public virtual void ReSzie(Graphics g)
        {
            Feng.Forms.Skins.ToolBarItemSkin.Default.SizeItem(g, this);
            if (this.Width < MinWidth)
            {
                this.Width = MinWidth;
            }
        }

        public event ToolBarItemClickHandler ItemClick;
        public virtual void OnToolBarItemClick(ToolBarItem item)
        {
            if (ItemClick != null)
            {
                ItemClick(this.ToolBar, item);
            }
        }

        private object _tag = null;
        public virtual object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
         
    }

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemLabel : ToolBarItem
    {
        public ToolBarItemLabel(string text, string id, Image img, bool showtext, bool showimage) :
            base(text, id, img, showtext, showimage)
        {

        }
 
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = this.Rect; 
            Feng.Forms.Skins.ToolBarItemSkin.Default.DrawText(g.Graphics, rect,this);
        }
         
    }

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemUp : ToolBarItem
    {
        public ToolBarItemUp()
        {
        }
 
        public override void OnDrawBack(Feng.Drawing.GraphicsObject g)
        {
            Point pt = g.ClientPoint;
            Rectangle rect = this.Rect;

            this.ToolBar.Skin.DrawBack(g.Graphics, rect, rect.Contains(pt));


        }
        public override void OnDrawImage(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = new Rectangle(this.Rect.Left + this.Rect.Width / 2 - this.ToolBar.ImageSize, this.Rect.Top, this.ToolBar.ImageSize, this.ToolBar.ImageSize);

            g.Graphics.DrawImage(Feng.Utils.Properties.Resources.Sortup, rect);

        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            OnDrawBack(g);
            OnDrawImage(g);
        }

    }
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemDown : ToolBarItem
    {
        public ToolBarItemDown()
        {
        }
        public override void OnDrawBack(Feng.Drawing.GraphicsObject g)
        {
            Point pt = g.ClientPoint;
            Rectangle rect = this.Rect;

            this.ToolBar.Skin.DrawBack(g.Graphics, rect, rect.Contains(pt));

        }
        public override void OnDrawImage(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = new Rectangle(this.Rect.Left + this.Rect.Width / 2 - this.ToolBar.ImageSize, this.Rect.Top, this.ToolBar.ImageSize, this.ToolBar.ImageSize);

            g.Graphics.DrawImage(Feng.Utils.Properties.Resources.Sortdown, rect);

        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            OnDrawBack(g);
            OnDrawImage(g);
        }

    }

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemDrop : ToolBarItem
    {
        public ToolBarItemDrop(string text, string id, Image img, bool showtext, bool showimage) :
            base(text, id, img, showtext, showimage)
        {

        }

        public override void OnDrawText(Feng.Drawing.GraphicsObject g)
        {
            if (this.ShowText)
            {
                Rectangle rect = this.Rect;
                int imageleft = 0;
                int closewidth = 0;  
                rect.X = rect.Left + imageleft + 2;
                rect.Y = rect.Top + 1;
                rect.Width = rect.Width - imageleft - closewidth - 2;
                if (this.ToolBar != null)
                {
                    this.ToolBar.Skin.DrawText(g.Graphics, rect, this);
                }
            }
        }
        public override void OnDrawImage(Feng.Drawing.GraphicsObject g)
        {
            if (this.ShowImage)
            {
                if (this.Image == null)
                    return;
                Rectangle rect = this.Rect;
                rect.X = rect.Right - this.ToolBar.ImageSize - 4;
                rect.Y = rect.Top + 4;
                rect.Width = this.ToolBar.ImageSize - 4;
                rect.Height = this.ToolBar.ImageSize - 4;
                if (this.ToolBar.ImageSize < this.ToolBar.ToolBarHeight)
                {
                    rect.Y = this.Rect.Top + (this.ToolBar.ToolBarHeight - this.Image.Height) / 2 + 4;
                }
                if (this.ToolBar != null)
                {
                    this.ToolBar.Skin.DrawImage(g.Graphics, rect, this);
                }
            }
        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            OnDrawBack(g);
            OnDrawImage(g); 
            OnDrawText(g);
        }
    }

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemVSplit : ToolBarItem
    {
        public override int Width
        {
            get
            {
                return 8;
            }
            set
            {
                base.Width = value;
            }
        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = this.Rect;
            rect.X = rect.X + 3;
            rect.Y = rect.Y + 4;
            rect.Width = rect.Width - 6;
            rect.Height = rect.Height - 8;
            Feng.Forms.Skins.ToolBarItemSkin.Default.DrawEmptyVSplit(g.Graphics, rect); 
        }
    }
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemHSplit : ToolBarItem
    {
        public override int Height
        {
            get
            {
                return 8;
            }
            set
            {
                base.Height = value;
            }
        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rect = this.Rect;
            rect.X = rect.X + 4;
            rect.Y = rect.Y + 3;
            rect.Width = rect.Width - 8;
            rect.Height = rect.Height - 6;
            Feng.Forms.Skins.ToolBarItemSkin.Default.DrawEmptyHSplit(g.Graphics, rect);
        }
    }

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemHeader : ToolBarItem
    {
        public override int Width
        {
            get
            {
                if (!this.Visable)
                    return 0;
                return 13;
            }
            set
            {
                base.Width = value;
            }
        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            if (this.Visable)
            {
                Rectangle rect = this.Rect;
                rect.X = rect.X + 3;
                rect.Y = rect.Y + 4;
                rect.Height = rect.Height - 8;
                rect.Width = 6;
                Feng.Forms.Skins.ToolBarItemSkin.Default.DrawHeader(g.Graphics, rect);
            }
        }
    }

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemFooter : ToolBarItem
    {
        public override int Width
        {
            get
            {
                if (!this.Visable)
                    return 0;
                return 16;
            }
            set
            {
                base.Width = value;
            }
        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            if (this.Visable)
            {
                base.OnDraw(g);
                Rectangle rect = this.Rect;
                rect.X = rect.X + 2;
                rect.Y = rect.Y + 2;
                rect.Width = rect.Width - 4;
                rect.Height = rect.Height - 4;
                Feng.Forms.Skins.ToolBarItemSkin.Default.DrawFooter(g.Graphics, rect);
            }
        }
    }


    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemUper : ToolBarItem
    {
        public override int Width
        {
            get
            {
                return 13;
            }
            set
            {
                base.Width = value;
            }
        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {

            Rectangle rect = this.Rect;
            rect.X = rect.X + 3;
            rect.Y = rect.Y + 4;
            rect.Height = rect.Height - 8;
            rect.Width = 6;
            Feng.Forms.Skins.ToolBarItemSkin.Default.DrawHeader(g.Graphics, rect);
        }
    }

    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class ToolBarItemBottomer : ToolBarItem
    {
        public override int Width
        {
            get
            {
                return 16;
            }
            set
            {
                base.Width = value;
            }
        }
        public override void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            base.OnDraw(g);
            Rectangle rect = this.Rect;
            rect.X = rect.X + 2;
            rect.Y = rect.Y + 2;
            rect.Width = rect.Width - 4;
            rect.Height = rect.Height - 4;
            Feng.Forms.Skins.ToolBarItemSkin.Default.DrawFooter(g.Graphics, rect);
        }
    }
}
