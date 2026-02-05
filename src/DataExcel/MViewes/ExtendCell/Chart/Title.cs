using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Feng.Enums;

namespace Feng.Excel.Chart
{

    public class Title : IChartTitle 
    {
        public Title(IDataExcelChart chart)
        {
            _Chart = chart;
        }
 
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
 
        #region IChartTitle 成员

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

        public virtual bool Antialiasing
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

        #region ITtitle 成员

        public string Text
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

        #region IFont 成员

        public Font Font
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

        #region IGrid 成员

        [Browsable(false)]
        public DataExcel Grid
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDock 成员

        public System.Windows.Forms.DockStyle Dock
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
    }
}
