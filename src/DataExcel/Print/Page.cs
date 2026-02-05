using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Feng.Forms.Interface;
using Feng.Print;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Print
{



    [Serializable]
    public class Page : PrintPageBase
    {
        public Page(DataExcel grid, int index)
        {
            this._pageindex = index;
            this._grid = grid;
        }
        private int _startrowindex = 0;
        public int StartRowIndex
        {
            get { return _startrowindex; }
        }
        private int _startcolumnindex = 0;
        public int StartColumnIndex
        {
            get
            {
                return _startcolumnindex;
            }
        }

        private int _endrowindex = 0;
        public int EndRowIndex { get { return _endrowindex; } }

        private int _endcolumnindex = 0;
        public int EndColumnIndex
        {
            get
            {
                return _endcolumnindex;
            }

        }

        public List<IPrint> PrintList { get; set; }

        public List<IPrintBorder> PrintBorderList
        {
            get;
            set;
        }

        private Point _startposition = Point.Empty;
        public Point StartPosition
        {
            get
            {
                return _startposition;
            }
        }

        private int _TableTop = 0;
        public int TableTop
        {
            get
            {
                return _TableTop;
            }
            set
            {
                if (_TableTop < value)
                {
                    _TableTop = value;
                }

            }
        }

        private int _pageindex = 0;
        public int PageIndex { get { return _pageindex; } }
 

        private bool _hasmorepage = false;
        public virtual bool HasMorePage
        {
            get { return _hasmorepage; }
            set { _hasmorepage = value; }
        }


        private IList<object> _list;
        public IList<object> List
        {

            get { return _list; }
            set { _list = value; }
        }

        public Dictionary<object, object> _dict = null;
        public Dictionary<object, object> Dictionary
        {
            get
            {
                if (_dict == null)
                {
                    _dict = new Dictionary<object, object>();
                }
                return _dict;
            }
            set
            {
                _dict = value;
            }
        }

        #region IPrint 成员

        public override bool Print(PrintArgs e)
        {
            Page page = e.CurrentPage as Page;
            if (this.Grid.CurrentPrintSetting.PrintHeader != null)
            {
                this.Grid.CurrentPrintSetting.PrintHeader.Print(e);
            }

            System.Drawing.Graphics g = e.PrintPageEventArgs.Graphics;
            if (e.BeginRowIndex < 1)
            {
                e.BeginRowIndex = e.MinRowIndex;
            }
            int rindex = e.BeginRowIndex;
            if (e.BeginColumnIndex < 1)
            {
                e.BeginColumnIndex = e.MinColumnIndex;
            }
            if (System.Diagnostics.Debugger.IsAttached)
            {
                //g.DrawRectangle(Pens.Red, Rectangle.Round(this.Rect));
            }
            this._startrowindex = e.BeginRowIndex;
            this._startcolumnindex = e.BeginColumnIndex;
            int left = this.Rect.Left;
            int top = this.Rect.Top;
            int rowindex = this._startrowindex;
            int columnindex = this._startcolumnindex;
            int width = 0;
            int height = 0;
            for (; rowindex <= e.MaxRowIndex; rowindex++)
            { 
                IRow row = this.Grid.GetRow(rowindex);
                if ((height + row.Height )> this.Rect.Height)
                {
                    break;
                } 
                height = height + row.Height;
            }
            for (; columnindex <= e.MaxColumnIndex; columnindex++)
            {
                IColumn column = this.Grid.GetColumn(columnindex);
                if ((width + column.Width) > this.Rect.Width)
                {
                    break;
                } 
                width = width + column.Width;
            }
            Rectangle clip = new Rectangle(this.Rect.Left, this.Rect.Top, width, height);
            e.Clip = clip;
            g.SetClip(clip);
            if (System.Diagnostics.Debugger.IsAttached)
            {
                //g.FillRectangle(Brushes.Red, Rectangle.Round(clip));
            }
            this._endrowindex = rowindex;
            this._endcolumnindex = columnindex;

            rowindex = this._startrowindex;
            columnindex = this._startcolumnindex;
            left = this.Rect.Left;
            top = this.Rect.Top;
            for (; rowindex <= e.MaxRowIndex; rowindex++)
            {
                left = this.Rect.Left;
                IRow row = this.Grid.Rows[rowindex];
                if (top + row.Height <= this.Rect.Bottom)
                {
                    columnindex = e.BeginColumnIndex;
                    for (; columnindex <= e.MaxColumnIndex; columnindex++)
                    {
                        IColumn column = this.Grid.Columns[columnindex];
                        if (left + column.Width > this.Rect.Right)
                        {
                            break;
                        }
                        ICell cell = row[column];
                        e.CurrentLocation = new Point(left, top);
                        cell.PrintBack(e);
                        left = left + column.Width;
                    }
                }
                else
                {
                    break;
                }
                top = top + row.Height;
            }

            rowindex = e.BeginRowIndex;
            columnindex = e.BeginColumnIndex;
            left = this.Rect.Left;
            top = this.Rect.Top;
            for (; rowindex <= e.MaxRowIndex; rowindex++)
            {
                left = this.Rect.Left;
                IRow row = this.Grid.Rows[rowindex];
                if (top + row.Height <= this.Rect.Bottom)
                {
                    columnindex = e.BeginColumnIndex;
                    for (; columnindex <= e.MaxColumnIndex; columnindex++)
                    {
                        IColumn column = this.Grid.Columns[columnindex];
                        if (left + column.Width > this.Rect.Right)
                        {
                            break;
                        }
                        ICell cell = row[column];
                        e.CurrentLocation = new Point(left, top);
                        cell.Print(e);
                        left = left + column.Width;
                    }
                }
                else
                {
                    break;
                }
                top = top + row.Height;
            }

            rowindex = e.BeginRowIndex;
            columnindex = e.BeginColumnIndex;
            left = this.Rect.Left;
            top = this.Rect.Top;
            for (; rowindex <= e.MaxRowIndex; rowindex++)
            {
                left = this.Rect.Left;
                IRow row = this.Grid.Rows[rowindex];
                if (top + row.Height <= this.Rect.Bottom)
                {
                    columnindex = e.BeginColumnIndex;
                    for (; columnindex <= e.MaxColumnIndex; columnindex++)
                    {
                        IColumn column = this.Grid.Columns[columnindex];
                        if (left + column.Width > this.Rect.Right)
                        {
                            break;
                        }
                        ICell cell = row[column];
                        e.CurrentLocation = new Point(left, top);
                        cell.PrintBorder(e);
                        left = left + column.Width;
                    }
                }
                else
                {
                    break;
                }
                top = top + row.Height;
            }
            if (rowindex >= e.MaxRowIndex)
            {
                e.BeginRowIndex = e.MinRowIndex;
                e.BeginColumnIndex = columnindex;
            }
            else
            {
                e.BeginRowIndex = rowindex;
            }
            if (columnindex < e.MaxColumnIndex || rowindex < e.MaxRowIndex)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            if (this.Grid.CurrentPrintSetting.PrintFooter != null)
            {
                this.Grid.CurrentPrintSetting.PrintFooter.Print(e);
            }

            return false;
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        #endregion

    }
}
