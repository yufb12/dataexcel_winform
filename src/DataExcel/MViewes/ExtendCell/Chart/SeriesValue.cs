using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Drawing;
using Feng.Forms.Interface;

namespace Feng.Excel.Chart
{
    public interface IOwnerSeries
    {
        ISeries OwnerSeries { get; set; }
    }

    public interface ISeriesValue : IValue, IDraw, IOwnerSeries, IBounds, IName 
    {

    }
    public class SeriesValue : ISeriesValue 
    {
        public SeriesValue(ISeries ownerSeries)
        {
            _ownerSeries = ownerSeries;
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private DateTime _datetime = DateTime.MinValue;
        public DateTime DataTime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }

        private decimal _decimalvalue = decimal.MinValue;
        public decimal DecimalValue
        {
            get { return _decimalvalue; }
            set { _decimalvalue = value; }
        }

        private double _doublevalue = double.MinValue;
        public double DoubleValue
        {
            get { return _doublevalue; }
            set { _doublevalue = value; }
        }

        private long _longvalue = long.MinValue;
        public long LongValue
        {
            get { return _longvalue; }
            set { _longvalue = value; }
        }

        private object _displayvalue = null;
        public object DisplayObject
        {
            get { return _displayvalue; }
            set { _displayvalue = value; }
        }

        private object _value = null;
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private Type _type = null;
        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #region IDraw 成员

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            Rectangle rectt = new Rectangle(this.Left - 20, this.Top - 20, 72, 20);
            Feng.Drawing.GraphicsHelper.DrawString(g,this.Value.ToString(), this.OwnerSeries.Font, Brushes.Red, rectt);
            GraphicsHelper.FillRectangleLinearGradient(g.Graphics, this.OwnerSeries.BackColor, this.Rect, true);
            return false;
        }

        #endregion

        #region IOwnerSeries 成员
        private ISeries _ownerSeries = null;
        public ISeries OwnerSeries
        {
            get
            { 
                return _ownerSeries;
            }
            set
            {
                _ownerSeries = value;
            }
        }

        #endregion
 
        #region IBounds 成员

        private int _left = 0;
        public virtual int Left
        {
            get
            {
                return _left ;
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

                return this.Left + this.Width;
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
                return new Rectangle(this._left, this._top, this._width, this._height);
            }
        }

        #endregion
    }
}
