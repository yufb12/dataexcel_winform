using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Drawing;
using System.ComponentModel;
using Feng.Excel.Styles;

namespace Feng.Excel.Chart
{

    public class Legend : ILegend 
    {
        public Legend(IDataExcelChart chart)
        {
            _Chart = chart;
#if DEBUG
            this.Width = 100;
            this.Height = 72;
#endif
        }

        #region IDraw 成员

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (_Visible)
            {

                int left = this.Left + this.Margin.Left;
                int top = this.Top + this.Margin.Top;
                int height = this.Margin.Top;
                for (int i = 0; i < this.Chart.Series.Count; i++)
                {
                    Rectangle rectf = new Rectangle();
                    rectf.Location = new Point(left, top);
                    rectf.Width = 20;
                    rectf.Height = 16;

                    GraphicsHelper.FillRectangleLinearGradient(g.Graphics, this.Chart.Series[i].BackColor, rectf, true);

                    rectf = new Rectangle(rectf.Right + this.Margin.Right, rectf.Top,
                        this.Width - rectf.Width - this.Margin.Right, rectf.Height);
                    using (SolidBrush sb = new SolidBrush(this.Chart.Series[i].BackColor))
                    {
                        Feng.Drawing.GraphicsHelper.DrawString(g,this.Chart.Series[i].Caption, this.Chart.Font, sb, rectf);
                    }
                    top = top + 2 + rectf.Height;
                    height = height + rectf.Height + 2;
                }
                height = height + this.Margin.Bottom;

                g.Graphics.DrawRectangle(Border.GetPen(), this.Left, this.Top, this.Width, height); 
            }
            return false;
        }

        #endregion

        #region IGrid 成员

        [Browsable(false)]
        public DataExcel Grid
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IForeColor 成员

        public Color ForeColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IBorderColor 成员

        public Color BorderColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IBackColor 成员

        public Color BackColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IBackImage 成员

        public Bitmap BackImage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Windows.Forms.ImageLayout BackImgeSizeMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IThickNess 成员

        public int ThickNess
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
 
        #region IChart 成员
        private IDataExcelChart _Chart = null;
        public virtual IDataExcelChart Chart
        {
            get
            {
                return _Chart;
            }
        }

        #endregion

        #region IVisible 成员
        private bool _Visible = true;
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

        #endregion

        #region IBorder 成员
        private LineStyle _border = null;
        public LineStyle Border
        {
            get
            {
                if (_border == null)
                {
                    _border = new LineStyle();
                }
                return _border;
            }
            set
            {
                _border = value;
            }
        }

        #endregion
 
        #region IBounds 成员

        private int _left = 0;
        public virtual int Left
        {
            get
            {
                return this.Chart.Rect.Right - this.Margin.Right - this.Width;
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

                return _width + _left;
            }

        }
        public virtual int Bottom
        {
            get
            {
                return _top + _height;
            }

        }
        private int _top = 0;
        public virtual int Top
        {
            get
            {
                return this.Chart.Rect.Top + this.Margin.Top;
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
                return new Rectangle(this._left, this._top, this._width, this._height);
            }
        }

        #endregion

        #region IMargin 成员
        private System.Windows.Forms.Padding _Margin = new System.Windows.Forms.Padding(4);

        public System.Windows.Forms.Padding Margin
        {
            get
            {
                return _Margin;
            }
            set
            {
                _Margin = value;
            }
        }

        #endregion
    }
}
