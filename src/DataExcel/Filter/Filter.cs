using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Fillter
{
    public class FilterExcel:IReadDataStruct, IDataStruct
    {
        public DataExcel Grid { get; set; }
        public int BeginRowIndex { get; set; }
        public int EndRowIndex { get; set; }
        public int BeginColumnIndex { get; set; }
        public int EndColumnIndex { get; set; }
        public int FilterRowIndex { get; set; }

        public string SumField { get; set; }
        public int SumColumn { get; set; }
        public FilterExcel()
        {
            filterrows = new Feng.Collections.ListEx<IRow>();
            filtercolumns = new Feng.Collections.ListEx<FilterColumn>();
            filtercells = new Feng.Collections.ListEx<ICell>();
            BeginColumnIndex = int.MaxValue;
        }

        private FillterDropDownForm dropdownpopupform = null;

        public void InitDropDownForm()
        {
            dropdownpopupform = new FillterDropDownForm();
            dropdownpopupform.ParentEditForm = this.Grid.FindForm();
            dropdownpopupform.ParentEditForm.VisibleChanged += ParentEditForm_VisibleChanged;
            dropdownpopupform.ParentEditForm.FormClosed += ParentEditForm_FormClosed;


        }

        private void ParentEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dropdownpopupform.ParentEditForm != null)
            {
                if (!dropdownpopupform.ParentEditForm.Visible)
                {
                    dropdownpopupform.Cancel();
                }
            }
        }

        private void ParentEditForm_VisibleChanged(object sender, EventArgs e)
        {
            if (dropdownpopupform.ParentEditForm != null)
            {
                if (!dropdownpopupform.ParentEditForm.Visible)
                {
                    dropdownpopupform.Cancel();
                }
            }
        }
 
        public void Init(DataExcel grid, ISelectCellCollection selectCelles)
        {
            this.Grid = grid;

            grid.CellPainting -= Grid_CellPainting;
            grid.CellMouseClick -= Grid_CellMouseClick;
            grid.CalcRowHeight -= Grid_CalcRowHeight;
            grid.CellPainting += Grid_CellPainting;
            grid.CellMouseClick += Grid_CellMouseClick;
            grid.CalcRowHeight += Grid_CalcRowHeight;
            System.Collections.Generic.List<ICell> list = selectCelles.GetAllCells();
            int rowindex = 0;
            foreach (ICell cell in list)
            {
                if (rowindex == 0)
                {
                    rowindex = cell.Row.Index;
                }
                if (rowindex == cell.Row.Index)
                {
                    filtercells.Add(cell);
                }
                if (BeginColumnIndex > cell.Column.Index)
                {
                    BeginColumnIndex = cell.Column.Index;
                }
                if (EndColumnIndex < cell.Column.Index)
                {
                    EndColumnIndex = cell.Column.Index;
                }
            }
            FilterRowIndex = rowindex;
        }

        public void Init(DataExcel grid)
        {
            this.Grid = grid;
            grid.CellPainting -= Grid_CellPainting;
            grid.CellMouseClick -= Grid_CellMouseClick;
            grid.CalcRowHeight -= Grid_CalcRowHeight;
            grid.CellPainting += Grid_CellPainting;
            grid.CellMouseClick += Grid_CellMouseClick;
            grid.CalcRowHeight += Grid_CalcRowHeight;
            for (int i = BeginColumnIndex; i <= this.EndColumnIndex; i++)
            {
                ICell cell = this.Grid[this.FilterRowIndex, i];
                filtercells.Add(cell);
            }
        }

        private bool Grid_CalcRowHeight(object sender, IRow row)
        {
            try
            {
                if (templistrow.Count > 0)
                {
                    foreach (int item in templistrow)
                    {
                        this.FilterRows.Add(this.Grid.GetRow(item));
                    }
                    templistrow.Clear();
                } 
                if (FilterRows.Contains(row))
                {
                    return true;
                }
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
            return false;
        }

        private void Grid_CellMouseClick(object sender, ICell cell, MouseEventArgs e)
        {
            try
            {
                if (filtercells.Contains(cell))
                {
                    Rectangle rect = new Rectangle(cell.Right - 18, cell.Bottom - 18, 16, 16);
                    if (rect.Contains(e.Location))
                    {
                        ShowPopup(cell);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("FilterExcel", "FilterExcel", "Grid_CellMouseClick", ex);
            }
        }

        private void Grid_CellPainting(object sender, Args.CellPaintingEventArgs e)
        {
            try
            {
                if (filtercells.Contains(e.Cell))
                {
                    ICell cell = e.Cell;
                    Rectangle rect = new Rectangle(cell.Right - 18, cell.Bottom - 18, 16, 16);
                    FilterColumn filterColumn = GetFilterColumn(e.Cell.Column);
                    if (filterColumn != null)
                    {
                        if (filterColumn.FilterRows.Count > 0)
                        {
                            e.Graphics.Graphics.FillRectangle(Brushes.BlueViolet, rect);
                        }
                    }
                    e.Graphics.Graphics.DrawImage(Feng.Drawing.Images.celleditdropdown, rect);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("FilterExcel", "FilterExcel", "Grid_CellMouseClick", ex);
            }
        }

        public void ShowPopup(ICell cell)
        {
            if (dropdownpopupform == null)
            {
                InitDropDownForm();
            }
            Point point = this.Grid.PointToScreen(new Point(cell.Left, cell.Bottom));
            dropdownpopupform.Grid = cell.Grid;
            dropdownpopupform.cell = cell;
            dropdownpopupform.InitPopup(this);
            BeginRowIndex = cell.Row.Index + 1;
            EndRowIndex = this.Grid.Rows.MaxHasValueIndex;
            FilterColumn filterColumn = GetFilterColumn(cell.Column);
            if (filterColumn == null)
            {
                filterColumn = new FilterColumn();
                filterColumn.Column = cell.Column;
                FilterColumns.Add(filterColumn);
            }
            dropdownpopupform.InitFilterColumn(filterColumn);
            dropdownpopupform.InitData();  
            dropdownpopupform.Popup(point);
        }
        private FilterColumn GetFilterColumn(IColumn column)
        {
            foreach (var item in FilterColumns)
            {
                if (item.Column == column)
                {
                    return item;
                }
            }
            return null;
        }

        private Feng.Collections.ListEx<ICell> filtercells = null;
        public Feng.Collections.ListEx<ICell> FilterCells
        {
            get { return filtercells; }
            set { filtercells = value; }
        }

        private Feng.Collections.ListEx<IRow> filterrows = null;
        public Feng.Collections.ListEx<IRow> FilterRows
        {
            get { return filterrows; }
            set { filterrows = value; }
        }

        private Feng.Collections.ListEx<FilterColumn> filtercolumns = null;
        public Feng.Collections.ListEx<FilterColumn> FilterColumns
        {
            get { return filtercolumns; }
            set { filtercolumns = value; }
        }


        public void Clear()
        {
            filterrows.Clear();
            filtercolumns.Clear(); 
        }

        public void AddFilter(IColumn column, Feng.Collections.ListEx<string> value, string op)
        {
            FilterColumn filterColumn = new FilterColumn();
            filterColumn.Column = column;
            filterColumn.Value = value;
            filterColumn.Operator = op;
            filtercolumns.Add(filterColumn);
            Filter(filterColumn);
        }

        public void Filter()
        {
            foreach (FilterColumn column in FilterColumns)
            {
                Filter(column);
            }
        }

        public void Filter(FilterColumn column)
        {
            column.FilterRows.Clear();
            if (column.Value.Count > 0)
            {
                for (int i = this.BeginRowIndex; i <= this.EndRowIndex; i++)
                {
                    IRow row = this.Grid.Rows[i];
                    bool res = FilterRow(row, column);
                    if (!res)
                    {
                        filterrows.Add(row);
                        column.FilterRows.Add(row);
                    }
                }
            }
        }

        public bool FilterRow(IRow row, FilterColumn column)
        {
            bool res = false;
            if (row == null)
                return false;
            ICell cell = row[column.Column];
            if (cell == null)
                return false;
            string orgtext = cell.Text;
            res = FilterValue(orgtext, column.Value, column.Operator);
            return res;
        } 

        public bool FilterValue(string orgtext, Feng.Collections.ListEx<string> Value, string op)
        {
            bool res = false;
            foreach (string item in Value)
            {
                if (FilterValue(orgtext, item, op))
                {
                    return true;
                }
            }
            return res;
        }

        public bool FilterValue(string orgtext, string targetvalue, string op)
        {
            bool res = false;
            switch (op)
            {
                case "Equals":
                    if (orgtext.Equals(targetvalue))
                    {
                        return true;
                    }
                    break;
                case "Contains":
                    if (orgtext.Contains(targetvalue))
                    {
                        return true;
                    }
                    break;
                case "StartsWith":
                    if (orgtext.StartsWith(targetvalue))
                    {
                        return true;
                    }
                    break;
                case "EndsWith":
                    if (orgtext.EndsWith(targetvalue))
                    {
                        return true;
                    }
                    break;

                case "NotEquals":
                    if (!orgtext.Equals(targetvalue))
                    {
                        return true;
                    }
                    break;
                case "NotContains":
                    if (!orgtext.Contains(targetvalue))
                    {
                        return true;
                    }
                    break;
                case "NotStartsWith":
                    if (!orgtext.StartsWith(targetvalue))
                    {
                        return true;
                    }
                    break;
                case "NotEndsWith":
                    if (!orgtext.EndsWith(targetvalue))
                    {
                        return true;
                    }
                    break;
                default:
                    if (orgtext.Equals(targetvalue))
                    {
                        return true;
                    }
                    break;
            }
            return res;
        }

        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                this.BeginRowIndex = reader.ReadIndex(1, BeginRowIndex);
                this.EndRowIndex = reader.ReadIndex(2, EndRowIndex);
                this.SumField = reader.ReadIndex(3, SumField);
                this.SumColumn = reader.ReadIndex(4, SumColumn);
                this.BeginColumnIndex = reader.ReadIndex(6, this.BeginColumnIndex);
                this.EndColumnIndex = reader.ReadIndex(7, this.EndColumnIndex);
                this.FilterRowIndex = reader.ReadIndex(8, this.FilterRowIndex);
                int count = reader.ReadIndex(9, 0);
                for (int i = 0; i < count; i++)
                {
                    int rowindex = reader.ReadInt();
                    templistrow.Add(rowindex);
                }
            }
            Init(this.Grid);
        }

        private List<int> templistrow = new List<int>();
        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    bw.Write(1, BeginRowIndex);
                    bw.Write(2, EndRowIndex);
                    bw.Write(3, SumField);
                    bw.Write(4, SumColumn);
                    bw.Write(6, this.BeginColumnIndex);
                    bw.Write(7, this.EndColumnIndex);
                    bw.Write(8, this.FilterRowIndex);
                    bw.Write(9, this.filterrows.Count);
                    foreach (IRow item in this.filterrows)
                    {
                        bw.Write(item.Index);
                    }

                    data.Data = bw.GetData();
                }
                return data;
            }
        }
        public class FilterColumn
        {
            public FilterColumn()
            {
                Value = new Feng.Collections.ListEx<string>();
                FilterRows = new Feng.Collections.ListEx<IRow>();
            }
            public FilterColumn Next { get; set; }
            public FilterColumn Prev { get; set; }
            public IColumn Column { get; set; }
            private Feng.Collections.ListEx<IRow> filterrows = null;
            public Feng.Collections.ListEx<IRow> FilterRows
            {
                get { return filterrows; }
                set { filterrows = value; }
            }

            public Feng.Collections.ListEx<string> Value { get; set; }
            public string Operator { get; set; }
        }
    }

}
