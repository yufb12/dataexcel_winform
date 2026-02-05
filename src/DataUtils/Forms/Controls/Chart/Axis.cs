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


namespace Feng.Forms.Controls
{
    [ToolboxItem(false)]
    public abstract class Axis : Component, IVisible, IDraw, ITitle, IBounds, IPadding
    {
        public Axis()
        {

        }

        private bool _Visible = false;
        public virtual bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
            }
        }

        public abstract bool OnDraw(object sender, Feng.Drawing.GraphicsObject g);

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
                this._height = value;
            }
        }

        public virtual int Right
        {
            get
            {
                return this.Left + this.Width;
            }
        }

        public virtual int Bottom
        {
            get
            {
                return this.Top + this.Height;
            }
        }
        private int _top = 0;
        public virtual int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                this._top = value;
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
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }
        private string _title = string.Empty;
        public virtual string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        private Padding _padding;
        public virtual Padding Padding
        {
            get
            {
                return _padding;
            }
            set
            {
                _padding = value;
            }
        }

    }

}
