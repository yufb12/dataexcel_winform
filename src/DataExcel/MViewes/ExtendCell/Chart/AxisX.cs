using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Enums;
using Feng.Excel.Styles;
using Feng.Drawing;

namespace Feng.Excel.Chart
{

    public class AxisX : IAxisX
    {
        public AxisX(IDataExcelChart chart)
        {
            _Chart = chart;
        }

        #region IAxis 成员

        private int _minorcount = 4;
        public int MinorCount
        {
            get
            {
                return _minorcount;
            }
            set
            {
                _minorcount = value;
            }
        }

        private int MinorWidth = 10;

        public int GridSpacing
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

        public virtual bool GridSpacingAuto
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

        public virtual bool Reverse
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

        public virtual bool Interlaced
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

        public Color InterlacedColor
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

        public Alignment Alignment
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

        #region IVisible 成员

        public virtual bool Visible
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

        #region IColor 成员

        public Color Color
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

        #region IDraw 成员

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {


#if DEBUG
            try
            {
#endif
                ///////////////////////////代码加在中间

                resetvalue();
                g.Graphics.DrawLine(this.Border1.GetPen(), this.Right, this.Top, this.Right, this.Bottom);
                int position = this.Bottom;

                decimal currentvalue = this._mindecimal;
                int minorcount = MinorCount;
                int index = 1;
                while (position > this.Top)
                {
                    if (minorcount == MinorCount)
                    {
                        index++;
                        g.Graphics.DrawLine(this.Border3.GetPen(), this.Right - _largeline, position, this.Right, position);
                        string text = string.Format("{0:#0}", currentvalue);
                        Size sf = Feng.Utils.ConvertHelper.ToSize(g.Graphics.MeasureString(text, this.Font));
                        currentvalue += this._agvdecimal;
                        Rectangle rectf = new Rectangle(this.Right - sf.Width - _largeline, position - sf.Height / 2, sf.Width, sf.Height);
                        if (this.BackColor != Color.Empty)
                        {
                            SolidBrush sb = SolidBrushCache.GetSolidBrush(this.BackColor);
                            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, sb, rectf.Left, rectf.Top, rectf.Width, rectf.Height);
                            
                        }
                        if (this.ForeColor != Color.Empty)
                        {
                            SolidBrush sb = SolidBrushCache.GetSolidBrush(this.ForeColor);
                            Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Font, sb, rectf); 
                        }
                        minorcount = 0;
                    }
                    else
                    {
                        g.Graphics.DrawLine(this.Border2.GetPen(), this.Right - _smallline, position, this.Right, position);
                    }
                    minorcount++;
                    position = position - MinorWidth;
                }
                return false;
                ///////////////////////////

#if DEBUG
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
#endif
            return false;

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

        #region IBounds 成员

        public virtual int Left
        {
            get
            {
                return this.Chart.Left + this.Chart.LeftTitles.GetWidth();
            }
            set
            {
            }
        }

        public virtual int Height
        {
            get
            {
                return this.Bottom - this.Top;
            }
            set
            {
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
                return this.Chart.AxisY.Top;
            }

        }
        public virtual int Top
        {
            get
            {
                return this.Chart.Top + this.Chart.TopTitles.GetHeight();
            }
            set
            {
            }
        }

        private int _width = 40;
        public virtual int Width
        {
            get
            {
                return this._width;
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
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }

        #endregion

        #region IAxis 成员
        private LineStyle _border1 = null;
        public LineStyle Border1
        {
            get
            {
                if (_border1 == null)
                {
                    _border1 = new LineStyle();
                }
                return _border1;
            }
            set
            {
                _border1 = value;
            }
        }
        private LineStyle _border2 = null;
        public LineStyle Border2
        {
            get
            {
                if (_border2 == null)
                {
                    _border2 = new LineStyle();
                }
                return _border2;
            }
            set
            {
                _border2 = value;
            }
        }
        private LineStyle _border3 = null;
        public LineStyle Border3
        {
            get
            {
                if (_border3 == null)
                {
                    _border3 = new LineStyle();
                }
                return _border3;
            }
            set
            {
                _border3 = value;
            }
        }

        #endregion

        #region IAxis 成员
        private int _smallline = 3;

        public int SmallLine
        {
            get
            {
                return _smallline;
            }
            set
            {
                _smallline = value;
            }
        }

        private int _largeline = 7;
        public int LargeLine
        {
            get
            {
                return _largeline;
            }
            set
            {
                _largeline = value;
            }
        }

        #endregion

        #region IFont 成员
        private Font _font = null;
        public Font Font
        {
            get
            {
                if (this._font == null)
                {
                    return this.Chart.Font;
                }
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        #endregion

        #region IForeColor 成员
        private Color _forecolor = Color.Black;
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

        #endregion

        #region IBackColor 成员
        private Color _backcolor = Color.Empty;
        public Color BackColor
        {
            get
            {
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }

        #endregion

        #region IAxisX 成员

        private decimal _maxdecimal = decimal.MaxValue;
        private decimal _mindecimal = decimal.MinValue;
        private decimal _agvdecimal = decimal.One;

        private void resetvalue()
        {
            decimal max = 100;

            if (this.MaxValue is decimal)
            {
                max = (decimal)this.MaxValue;
            }
            decimal min = 0;
            if (this.MinValue is decimal)
            {
                min = (decimal)this.MinValue;
            }

            int minorcount = MinorCount;
            decimal all = max - min;
            int i = Feng.Utils.ConvertHelper.ToInt32(System.Math.Floor(this.Height / (this.MinorWidth * this.MinorCount * 1m)) - 2);
            if (i < 1)
            {
                i = 1;
            }
            decimal agv = all / i;
            decimal startvalue = min - agv;
            if (min > 0 && startvalue < 0)
            {
                min = 0;
                max = max + startvalue;
                startvalue = 0;
            }
            else
            {
                min = startvalue;
            }
            this._maxdecimal = max;
            this._mindecimal = min;
            this._agvdecimal = agv;
        }

        private object _maxvalue = null;
        public object MaxValue
        {
            get
            {
                return _maxvalue;
            }
            set
            {
                _maxdecimal = decimal.MaxValue;
                _mindecimal = decimal.MinValue;
                _agvdecimal = decimal.One;
                _maxvalue = value;

            }
        }
        private object _minvalue = null;
        public object MinValue
        {
            get
            {
                return _minvalue;
            }
            set
            {
                _maxdecimal = decimal.MaxValue;
                _mindecimal = decimal.MinValue;
                _agvdecimal = decimal.One;
                _minvalue = value;
            }
        }

        #endregion

        public void GetLabel(decimal min, decimal max)
        {

        }

        public void GetCount(decimal min, decimal max)
        {
            decimal all = max - min;
            int i = Feng.Utils.ConvertHelper.ToInt32(System.Math.Floor(this.Height / this.MinorWidth * this.MinorCount * 1m));
            decimal agv = all / i;

        }

        #region IAxisX 成员


        public int GetHeight(object value)
        {
            decimal dvalue2 = Convert.ToDecimal(value) - this._mindecimal;
            decimal dvalue = this._agvdecimal / (decimal)(this.MinorWidth * this.MinorCount);
            int dv = (int)(dvalue2 / dvalue);
            return dv;
        }

        #endregion
    }
}
