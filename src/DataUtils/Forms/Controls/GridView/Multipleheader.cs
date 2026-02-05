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
using System.Security.Permissions;

namespace Feng.Forms.Controls.GridControl
{
    public class Multipleheader 
    {
        public Multipleheader(GridView grid)
        {
            _grid = grid;
        }
        private GridView _grid = null;
        [Category(CategorySetting.PropertyDesign)]
        [Browsable(false)]
        public GridView Grid
        {
            get
            {
                return _grid;
            }
        }

        private string _caption = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public string Caption
        {
            get {
                return _caption;
            }
            set
            {
                _caption = value;
            }
        }

        private GridViewColumn _begin = null;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public GridViewColumn Begin 
        {
            get {
                return _begin;
            }
            set
            {
                _begin = value;
            }
        }
        private GridViewColumn _end = null;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public GridViewColumn End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }
 
        #region IBounds 成员

        [Browsable(false)]
        public virtual int Height
        {
            get
            {
                return this.Grid.Height;
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
                return 0;
            }
            set { }
        }

        private int _width = 72;
        [Browsable(false)]
        [DefaultValue(72)]
        public virtual int Width
        {
            get
            {
                return _width;
            }
            set
            {
                this._width = value;
            }
        }
        [Browsable(true)]
        public virtual RectangleF Rect
        {
            get
            {
                return new RectangleF(this.Left, this.Top, this.Width, this.Height);
            }
        }

        #endregion
    }
}

