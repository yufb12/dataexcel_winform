using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Enums;
using Feng.Excel.Styles;
using Feng.Drawing;

namespace Feng.Excel.Chart
{

    public class AxisY : IAxisY
    {
        public AxisY(IDataExcelChart chart)
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
        private int FirstStart = 15;
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
            g.Graphics.DrawLine(this.Border1.GetPen(), this.Left, this.Top, this.Right, this.Top);
            int position = this.Left + FirstStart;
            while (position > this.Left)
            {
                g.Graphics.DrawLine(this.Border3.GetPen(), position, this.Top, position, this.Top + _smallline);
                position = position - MinorWidth;
            }
            position = this.Left + FirstStart;
            int minorcount = MinorCount;
            int index = 0;
            while (position < this.Right)
            {
                if (minorcount == MinorCount)
                {
                    index++;
                    g.Graphics.DrawLine(this.Border3.GetPen(), position, this.Top, position, this.Top + _largeline);
                    string text = string.Format("Y{0}", index);
                    Size sf = Feng.Utils.ConvertHelper.ToSize(g.Graphics.MeasureString(text, this.Font));
                    Rectangle rectf = new Rectangle(position - sf.Width / 2, this.Top + _largeline + 2, sf.Width, sf.Height);
                    if (this.BackColor != Color.Empty)
                    {
                        SolidBrush sb = SolidBrushCache.GetSolidBrush(this.BackColor);
                        Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, sb, rectf.Left, rectf.Top, rectf.Width, rectf.Height);
                         
                    }
                    if (index < this.SeriesValues.Count - 1)
                    {
                        ISeriesValue isv = this.SeriesValues[index];
                        isv.Left = rectf.Left;
                        isv.Width = 26;
                        isv.Height = this.Chart.AxisX.GetHeight(isv.Value);
                        isv.Top = this.Top - isv.Height - 1;
                        isv.OnDraw(this, g);
                        text = isv.Name;
                    }
                    if (this.ForeColor != Color.Empty)
                    {
                        using (SolidBrush sb = new SolidBrush(this.ForeColor))
                        {
                            rectf.Width = MinorWidth * MinorCount;
                            Feng.Drawing.GraphicsHelper.DrawString(g,text, this.Font, sb, rectf);
                        }
                    }
                    minorcount = 0;
                }
                else
                {
                    g.Graphics.DrawLine(this.Border2.GetPen(), position, this.Top, position, this.Top + _smallline);
                }
                minorcount++;
                position = position + MinorWidth;
            }
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
                return this.Chart.AxisX.Right;
            }
            set
            {
            }
        }

        private int _height = 40;
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
        public virtual int Top
        {
            get
            {
                return this.Chart.Bottom - this.Chart.BottomTitles.GetHeight() - this.Height;
            }
            set
            {
            }
        }

        public virtual int Width
        {
            get
            {
                return this.Chart.Legend.Left - this.Left;
            }
            set
            {
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

        #region IAxisY 成员
        private AxisYValueType _ValueType = AxisYValueType.Number;
        public AxisYValueType ValueType
        {
            get
            {
                return _ValueType;
            }
            set
            {
                _ValueType = value;
            }
        }

        #endregion

        #region IAxisY 成员

        private long _max = long.MaxValue;
        public long Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }
        private long _min = long.MinValue;
        public long Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        public DateTime MaxDateTime
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

        public DateTime MinDataTime
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

        #region IAxisY 成员

        private List<ISeriesValue> _SeriesValues = null;
        public List<ISeriesValue> SeriesValues
        {
            get
            {
                if (_SeriesValues == null)
                {
                    _SeriesValues = new List<ISeriesValue>();
                }
                return _SeriesValues;
            }
            set
            {
                _SeriesValues = value;
            }
        }

        #endregion
    }
}
