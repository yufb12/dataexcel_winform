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
using Feng.Utils;
using Feng.Forms.Interface;

namespace Feng.Forms.Controls.GridControl
{
    public class FooterCell : IDraw, IRect
    {
        #region IFormat 成员
        private FormatType _FormatType = FormatType.Null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public FormatType FormatType
        {
            get
            {
                return _FormatType;
            }
            set
            {
                _FormatType = value;
            }
        }
        private string _FormatString = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string FormatString
        {
            get
            {
                return _FormatString;
            }
            set
            {
                _FormatString = value;
            }
        } 
        public GridView Grid
        {
            get
            {
                return this.Column.Grid;
            }
        }
        #endregion
        private GridViewColumn _column = null;
        [Browsable(false)]
        public virtual GridViewColumn Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }

        private Footer _footer = null;
        public virtual Footer Footer
        {
            get
            {
                return _footer;
            }
            set
            {
                _footer = value;
            }
        }

        private Color _forecolor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color ForeColor
        {
            get
            {
                if (_forecolor == Color.Empty)
                {
                    return this.Grid.ForeColor;
                }
                return _forecolor;
            }
            set
            {
                _forecolor = value;
            }
        }
        public bool OnDraw(object sender, GraphicsObject g)
        {
            Feng.Drawing.GraphicsHelper.DrawText(g.Graphics, this.Grid.Font, this.Column.TotalText, this.ForeColor, this.Rect);
            return false;
        }
 

        public int Left
        {
            get
            {
                return this.Column.Left;
            }
            set
            {
            }
        }

        public int Height
        {
            get
            {
                return this.Footer.Height;
            }
            set
            {
            }
        }

        public int Right
        {
            get { return this.Left + this.Width; }
        }

        public int Bottom
        {
            get { return this.Top + this.Height; }
        }

        public int Top
        {
            get
            {
                return this.Footer.Top;
            }
            set
            {
            }
        }

        public int Width
        {
            get
            {
                return this.Column.Width;
            }
            set
            {
            }
        }

        public Rectangle Rect
        {
            get { return new Rectangle(this.Left, this.Top, this.Width, this.Height); }
        }
    }

    public class Footer : IDraw
    {
        public Footer(GridView grid)
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

        #region IBounds 成员

        [Browsable(false)]
        public virtual int Height
        {
            get
            {
                return this.Grid.RowHeight + 2;
            }
            set
            {
            }
        }
        [Browsable(false)]
        public virtual int Right
        {
            get { return this.Left + this.Width; }
        }
        [Browsable(false)]
        public virtual int Bottom
        {
            get { return this.Top + this.Height; }
        }

        [Browsable(false)]
        public virtual int Left
        {
            get
            {
                return 0;
            }
            set { }

        }
        [Browsable(false)]
        public virtual int Top
        {
            get
            {
                return this.Grid.Height - this.Height - (this.Grid.HScroll.Visible ? this.Grid.HScroll.Height : 0);
            }
            set { }
        }

        [Browsable(false)]
        public virtual int Width
        {
            get
            {
                return this.Grid.Width;
            }
            set
            {
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

        public bool OnDraw(object sender, GraphicsObject g)
        {
#if DEBUG3
            g.Graphics.FillRectangle(Brushes.Red, this.Rect);
#endif
            GraphicsHelper.FillRectangle(g.Graphics, Brushes.LightGray, this.Rect);
            Rectangle rectHeader = new Rectangle(this.Left, this.Top, this.Grid.RowHeaderWidth, this.Height);
            Feng.Drawing.GraphicsHelper.DrawString(g,this.Grid.Count.ToString(), this.Grid.Font, Brushes.Black, rectHeader, Feng.Drawing.StringFormatCache.GetStringFormat());
 
            return false;
        }
    }
}

