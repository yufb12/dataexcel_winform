using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Feng.Excel.Chart
{

    public class Series : ISeries
    {
        public Series(IDataExcelChart chart)
        {
            _Chart = chart;
        }

        #region ISeries 成员
        private int _MinorCount = 0;
        public int MinorCount
        {
            get
            {
                return _MinorCount;
            }
            set
            {
                _MinorCount = value;
            }
        }

        private int _GridSpacing = 0;
        public int GridSpacing
        {
            get
            {
                return _GridSpacing;
            }
            set
            {
                _GridSpacing = value;
            }
        }

        #endregion
 
        #region ISetText 成员

        public void SetText(string text)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IFont 成员

        public Font Font
        {
            get
            {
                return this.Chart.Font;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IForeColor 成员

        private Color _forecolor = Color.RosyBrown;
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

        #region IGrid 成员

        public DataExcel Grid
        {
            get { return this.Chart.Grid; }
        }

        #endregion

        #region IDraw 成员

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {

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

        #region ICaption 成员

        private string _Caption = string.Empty;
        public string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                _Caption = value;
            }
        }

        #endregion
 

        #region IISeriesValues 成员
        private ISeriesValueCollection _seriesValues = null;
        public ISeriesValueCollection SeriesValues
        {
            get
            {
                if (_seriesValues == null)
                {
                    _seriesValues = new SeriesValueCollection(this);
                }
                return _seriesValues;
            }
            set
            {
                _seriesValues = value;
            }
        }

        #endregion

        #region IBackColor 成员
 
        private Color _backcolor = Color.RoyalBlue;
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
    }
}
