#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Feng.Excel.Chart;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using System.Drawing.Design;
using Feng.Excel.Designer;
using Feng.Utils;
using Feng.Drawing;
using Feng.Excel.Extend;
using Feng.Forms.Views;

namespace Feng.Excel.Extend
{
    [Serializable]
    public class DataExcelChart : ExtendCell, IDataExcelChart
    {
        public DataExcelChart(DataExcel grid)
            : base(grid)
        {
            this.BackColor = Color.White;
            Chart.Series series = new Series(this);
            series.Caption = "分类名称";
            this.AddSeries(series);
            series = new Series(this);
            series.Caption = "分类名称2"; 
            this.AddSeries(series);

            series = new Series(this);
            series.Caption = "分类名称3"; 
            this.AddSeries(series);

            this.AxisY = new AxisY(this);
            this.AxisX = new AxisX(this);
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            this.Grid.OnExtendCellClick(this);
            return base.OnMouseClick(sender,e, ve);
        }

        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            GraphicsState gs = g.Graphics.Save();
            bool result = base.OnDraw(this, g);
            g.Graphics.SetClip(this.Rect);
            this.DrawBackImage(g);
            this.Legend.OnDraw(this, g);
            this.AxisX.OnDraw(this, g);
            this.AxisY.OnDraw(this, g);
            this.LeftTitles.OnDraw(this, g);
            this.RightTitles.OnDraw(this, g);
            this.TopTitles.OnDraw(this, g);
            this.BottomTitles.OnDraw(this, g);
            this.Series.OnDraw(this, g);
            g.Graphics.Restore(gs);
            return result;
        }

        private bool DrawBackImage(Feng.Drawing.GraphicsObject g)
        {
            Bitmap backimage = null;
            Rectangle rect = this.Rect;
            bool result = false;
            if (backimage == null)
            {
                backimage = this.BackImage;
                rect = ImageHelper.ImageRectangleFromSizeMode(BackImgeSizeMode, backimage, rect);
            }
            if (backimage != null)
            {
                g.Graphics.DrawImage(backimage, rect); 
                result = true;
            }
 
            return result;
        }
        
        public void AddSeries(ISeries series)
        {
            this.Series.Add(series);
        } 

        #region IDataExcelChart 成员

        private ILegend _Legend = null;
        public ILegend Legend
        {
            get
            {
                if (_Legend == null)
                {
                    _Legend = new Legend(this);
                }
                return _Legend;
            }
            set
            {
                _Legend = value;
            }
        }

        private string _PaletteName = string.Empty;
        public string PaletteName
        {
            get
            {
                return _PaletteName;
            }
            set
            {
                _PaletteName = value;
            }
        }

        private int _PaletteBaseColorNumber = 0;
        public int PaletteBaseColorNumber
        {
            get
            {
                return _PaletteBaseColorNumber;
            }
            set
            {
                _PaletteBaseColorNumber = value;
            }
        }

        private IAxisY _AxisY = null;
        public IAxisY AxisY
        {
            get
            {
                if (_AxisY == null)
                {
                    _AxisY = new AxisY(this);
                }
                return _AxisY;
            }
            set
            {
                _AxisY = value;
            }
        }

        private IAxisX _AxisX = null;
        public IAxisX AxisX
        {
            get
            {
                if (_AxisX == null)
                {
                    _AxisX = new AxisX(this);
                }
                return _AxisX;
            }
            set
            {
                _AxisX = value;
            }
        }

        private ISeriesCollection _Series = null;
        public ISeriesCollection Series
        {
            get
            {
                if (_Series == null)
                {
                    _Series = new SeriesCollection(this);
                }
                return _Series;
            }
            set
            {
                _Series = value;
            }
        }

        #endregion

        #region IDataSource 成员
        private object _datasource = null;
        [AttributeProvider(typeof(IListSource))]
        public object DataSource
        {
            get
            {
                return _datasource;
            }
            set
            {
                _datasource = value;
                ReFreshDataSource();
            }
        }

        #endregion

        #region IDisplayMember 成员
        private string _DisplayMember = string.Empty;
        [Editor(typeof(DataColumnDesigner), typeof(UITypeEditor))]
        public string DisplayMember
        {
            get
            {
                return _DisplayMember;
            }
            set
            {
                _DisplayMember = value;
                ReFreshDataSource();
            }
        }

        #endregion

        #region IValueMember 成员
        private string _ValueMember = string.Empty;
        [Editor(typeof(DataColumnDesigner), typeof(UITypeEditor))]
        public string ValueMember
        {
            get
            {
                return _ValueMember;
            }
            set
            {
                _ValueMember = value;
                ReFreshDataSource();
            }
        }

        #endregion

        #region IBackImage 成员
        private Bitmap _backimage = null;
        public virtual Bitmap BackImage
        {
            get
            {
                return this._backimage;
            }
            set
            {
                this._backimage = value;
            }
        }

        private ImageLayout _BackImgeSizeMode = ImageLayout.None;

        public ImageLayout BackImgeSizeMode
        {
            get
            {
                return this._BackImgeSizeMode;
            }
            set
            {
                this._BackImgeSizeMode = value;
            }
        }

        #endregion
 
        #region IDataMember 成员
        private string _datamember = string.Empty;
        [Editor(typeof(DataColumnDesigner), typeof(UITypeEditor))]
        public string DataMember
        {
            get
            {
                return _datamember;
            }
            set
            {
                _datamember = value;
                ReFreshDataSource();
            }
        }

        #endregion

        public void ReFreshDataSource()
        {
            if (this.DataSource != null)
            {
                if (this.ValueMember != string.Empty)
                {
                    if (this.DisplayMember != string.Empty)
                    {
                        if (this.SeriesName != string.Empty)
                        {
                            ReFreshDataSource(this._datasource);
                            return;
                        }
                    }
                }
            }
            this.Series.Clear();
        }
 
        #region BindingDataSource

        public void ReFreshDataSource(object datasource)
        { 
            if (datasource is DataSet)
            {
                BindingDatatableValues(((DataSet)datasource).Tables[0]);
            }
            else if (datasource is DataTable)
            {
                BindingDatatableValues((DataTable)datasource);
            }
            else if (datasource is System.Collections.IList)
            {
                BindingIlistValues(datasource as System.Collections.IList);
            } 
        }
        
        public void BindingDatatableValues(DataTable table)
        {

#if DEBUG
            try
            {
                 
#endif
                int position = _DataPosition;
                int colorindex = 1;
                this.Series.Clear();
                this.AxisX.MaxValue = null;
                this.AxisX.MinValue = null;

                if (table.Columns.Contains(this.DisplayMember))
                {    if (table.Columns.Contains(this.SeriesName))
                    {
                        if (table.Columns.Contains(this.ValueMember))
                        {
                            List<ISeriesValue> values = new List<ISeriesValue>();
                            Dictionary<string, Chart.Series> list = new Dictionary<string, Series>();
                            decimal min = decimal.MaxValue ;
                            decimal max = decimal.MinValue ;
                            foreach (DataRow row in table.Rows)
                            {
                                string sername = row[this.SeriesName].ToString();
                                Chart.Series ser = null;
                                if (!list.ContainsKey(sername))
                                {
                                    ser = new Series(this);
                                    ser.Caption = sername;
                                    this.Series.Add(ser);
                                    list.Add(sername, ser);
                                    Random rand = new Random(colorindex++);

                                    ser.BackColor = Color.FromArgb(rand.Next(128, 192), rand.Next(128, 192), rand.Next(128, 192));
                                }
                                else
                                {
                                    ser = list[sername];
                                }
                                SeriesValue seri = new SeriesValue(ser);

                                object rowvalue = row[this.ValueMember];
                                object disvalue = row[this.DisplayMember];

                                seri.Name = disvalue.ToString();
                                seri.DisplayObject = disvalue;
                                seri.Value = rowvalue;
                                ser.SeriesValues.Add(seri);
                                values.Add(seri);
                                this.AxisY.SeriesValues = values;
                                decimal dvalue=0;
                                decimal.TryParse(rowvalue.ToString(), out dvalue);
                                if (min > dvalue)
                                {
                                    min = dvalue;
                                }
                                if (max < dvalue)
                                {
                                    max = dvalue;
                                }

                            }

                            this.AxisX.MaxValue = max;
                            this.AxisX.MinValue = min;
                        }
                    } 
                }
#if DEBUG 

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
#endif

        }

        public void BindingIlistValues(System.Collections.IList ilist)
        {

//#if DEBUG
//            try
//            {
//#endif

#warning Type t = ilist.GetType().GetGenericArguments()[0];

                //for (int i = 0; i < ilist.Count; i++)
                //{
                //    PropertyInfo pt = t.GetProperty(this.DataMember);
                //    object value = pt.GetValue(ilist[i], null);
                //}


//#if DEBUG

//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//#endif



        }

        #endregion

        #region IPosition 成员
        private int _DataPosition = 0;
        public int Position
        {
            get
            {
                return _DataPosition;
            }
            set
            {
                _DataPosition = value;
            }
        }

        #endregion

        #region IDataExcelChart 成员


        private string _SeriesName = string.Empty;
        [Editor(typeof(DataColumnDesigner), typeof(UITypeEditor))]
        public string SeriesName
        {
            get
            {
                return _SeriesName;
            }
            set
            {
                _SeriesName = value;
                ReFreshDataSource();
            }
        }

        #endregion

        #region IDataExcelChart 成员 
        private ITitleCollection _LeftTitles = null;
        public ITitleCollection LeftTitles
        {
            get
            {
                if (_LeftTitles == null)
                {
                    _LeftTitles = new TitleCollection(this);
                }
                return _LeftTitles;
            }
            set
            {
                _LeftTitles = value;
            }
        }

        private ITitleCollection _RightTitles = null;
        public ITitleCollection RightTitles
        {
            get
            {
                if (_RightTitles == null)
                {
                    _RightTitles = new TitleCollection(this);
                }
                return _RightTitles;
            }
            set
            {
                _RightTitles = value;
            }
        }

        private ITitleCollection _TopTitles = null;
        public ITitleCollection TopTitles
        {
            get
            {
                if (_TopTitles == null)
                {
                    _TopTitles = new TitleCollection(this);
                }
                return _TopTitles;
            }
            set
            {
                _TopTitles = value;
            }
        }

        private ITitleCollection _BottomTitles = null;
        public ITitleCollection BottomTitles
        {
            get
            {
                if (_BottomTitles == null)
                {
                    _BottomTitles = new TitleCollection(this);
                }
                return _BottomTitles;
            }
            set
            {
                _BottomTitles = value;
            }
        }

        #endregion
    }
}