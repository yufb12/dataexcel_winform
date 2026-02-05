
using Feng.Data;
using Feng.Excel.Collections;
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Feng.Excel.Table
{

    public class CellTableTools
    {
        public static void SetSelectCellsDataTable(DataExcel grid, ISelectCellCollection selectCell)
        {
            int minrow = selectCell.MinRow();
            int maxrow = selectCell.MaxRow();
            int mincolumn = selectCell.MinColumn();
            int maxcolumn = selectCell.MaxColumn();
            CellTable table = new CellTable(grid.CellDataBase);
            grid.CellDataBase.MainTable = table;
            grid.CellDataBase.Tables.Clear();
            grid.CellDataBase.Tables.Add(table);
            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                ICell cell = grid[minrow, i];
                if (cell != null)
                {
                    CellTableColumn cellTableColumn = new CellTableColumn(table);
                    cellTableColumn.Index = i;
                    if (!string.IsNullOrEmpty(cell.ID))
                    {
                        cellTableColumn.Name = cell.ID;
                    }
                    else
                    {
                        cellTableColumn.Name = cell.Text;
                    }
                    table.Colums.Add(i, cellTableColumn);
                    if (cell.OwnMergeCell != null)
                    {
                        i = cell.MaxColumnIndex;
                    }
                }
            }
            for (int i = minrow + 1; i <= maxrow; i++)
            {
                CellTableRow cellTableRow = new CellTableRow(table);
                table.Rows.Add(cellTableRow);
                for (int j = mincolumn; j <= maxcolumn; j++)
                {
                    CellTableColumn column = table.Get(j);
                    if (column == null)
                        continue;
                    ICell cell = grid[i, j];
                    if (cell != null)
                    {
                        CellTableCell cellTableCell = new CellTableCell(cellTableRow);
                        cellTableCell.Column = column;
                        cellTableCell.Row = cellTableRow;
                        cellTableRow.Add(cellTableCell);
                        cellTableCell.Cell = cell;
                    }
                }
            }
        }

        public static DataTable GetDataTable(CellTable cellTable)
        {
            if (cellTable == null)
                return null;
            DataTable table = new DataTable();

            foreach (var item in cellTable.Colums)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.Value.Name))
                    {
                        continue;
                    }
                    table.Columns.Add(item.Value.Name);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            foreach (var item in cellTable.Rows)
            {

#if DEBUG
                try
                {
#endif
                    DataRow row = table.NewRow(); 
                    int spacecount = 0;
                    foreach (var itemcell in item.Cells)
                    {
                        if (string.IsNullOrWhiteSpace(itemcell.Key.Name))
                        {
                            continue;
                        }
                        string txt = Feng.Utils.ConvertHelper.ToString(itemcell.Value.Value);
                        if (string.IsNullOrWhiteSpace(txt))
                        {
                            spacecount++;
                        }
                        row[itemcell.Key.Name] = itemcell.Value.Value;
                    }
                    if (item.Cells.Count > 5)
                    {
                        if (spacecount > 2)
                        {
                            break;
                        }
                    }
                    table.Rows.Add(row);
#if DEBUG
                }
                catch (Exception ex)
                {
                    Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
                }
#endif
            }
            return table;
        }
        public static DataTable GetDataTable(DataExcel grid)
        {
            if (grid == null)
                return null;
            DataTable table = new DataTable();
            List<ICell> cells = grid.IDCells.GetCells();
            foreach (var item in cells)
            {
                try
                {
                    table.Columns.Add(item.ID);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            DataRow row = table.NewRow();
            foreach (var item in cells)
            {
                row[item.ID] = item.Value;
            }
            table.Rows.Add(row);
            return table;
        }
        public static void SetDataTable(CellTable cellTable, DataTable dataTable)
        {
            if (cellTable == null)
                return;
            for (int i = 0; i < cellTable.Rows.Count; i++)
            {
                CellTableRow item = cellTable.Rows[i];
                if (item == null)
                    continue;
                DataRow row = null;
                if (i < dataTable.Rows.Count)
                {
                    row = dataTable.Rows[i];
                }
                foreach (var itemcell in item.Cells)
                {
                    string columnname = itemcell.Key.Name;
                    if (!string.IsNullOrWhiteSpace(columnname))
                    {
                        if (row != null)
                        {
                            if (dataTable.Columns.Contains(columnname))
                            {
                                itemcell.Value.Value = row[columnname];
                                continue;
                            }
                        }
                    }
                    itemcell.Value.Value = null;
                }
            }
        }
        public static void SetDataTable(DataExcel grid, DataTable dataTable)
        {
            if (grid == null)
                return;
            List<ICell> cells = grid.IDCells.GetCells();
            DataRow row = null;
            if (dataTable.Rows.Count > 0)
            {
                row = dataTable.Rows[0];
            }
            foreach (ICell item in cells)
            {
                if (dataTable.Columns.Contains(item.ID))
                {
                    item.Value = row[item.ID];
                }
                else
                {
                    item.Value = null;
                }
            }
        }
        public static DataTable GetGirdDataTable(DataExcel grid)
        {
            DataTable dataTableid = GetDataTable(grid);

            DataTable dataTablecelltable = GetDataTable(grid.CellDataBase.MainTable);
            if (dataTableid == null)
            {
                return dataTablecelltable;
            }
            if (dataTablecelltable == null)
            {
                return dataTableid;
            }
            DataTable tablemain = null;
            if (dataTablecelltable.Rows.Count >= dataTableid.Rows.Count)
            {
                tablemain = dataTablecelltable.Copy();
                foreach (DataColumn column in dataTableid.Columns)
                {
                    tablemain.Columns.Add(column.ColumnName, column.DataType);
                }
                if (dataTableid.Rows.Count < 1)
                    return tablemain;
                DataRow rowid = dataTableid.Rows[0];
                foreach (DataRow row in tablemain.Rows)
                {
                    foreach (DataColumn column in dataTableid.Columns)
                    {
                        row[column.ColumnName] = rowid[column.ColumnName];
                    }
                }
            }
            else
            {
                tablemain = dataTableid.Copy();
                foreach (DataColumn column in dataTablecelltable.Columns)
                {
                    tablemain.Columns.Add(column.ColumnName, column.DataType);
                }
            }
            return tablemain;
        }
        public static void SetGridDataTable(DataExcel grid, DataTable dataTable)
        {
            SetDataTable(grid, dataTable);
            SetDataTable(grid.CellDataBase.MainTable, dataTable);
        }
        public static DataTable ConverSelectesToDataTable(DataExcel grid,
    ICell begin, ICell end, List<int> numcolumns, List<int> timecolumns)
        {
            ISelectCellCollection selectCell = new SelectCellCollection();
            selectCell.BeginCell = begin;
            selectCell.EndCell = end;
            return ConverSelectesToDataTable(grid, selectCell, numcolumns, timecolumns);
        }
        public static DataTable ConverSelectesToDataTable(DataExcel grid,
        ISelectCellCollection selectCell, List<int> numcolumns, List<int> timecolumns)
        {
            DataTable table = new DataTable();
            int headerrow = selectCell.MinRow();
            int minrow = headerrow + 1;
            int maxrow = selectCell.MaxRow();
            int mincolumn = selectCell.MinColumn();
            int maxcolumn = selectCell.MaxColumn();
            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                ICell cell = grid[headerrow, i];
                string txt = cell.Text;
                table.Columns.Add(txt);
            }
            for (int rowindex = minrow; rowindex <= maxrow; rowindex++)
            {
                DataRow dataRow = table.NewRow();
                int i = 0;
                for (int columnindex = mincolumn; columnindex <= maxcolumn; columnindex++, i++)
                {
                    ICell cell = grid[rowindex, columnindex];
                    if (numcolumns.Contains(columnindex))
                    {
                        dataRow[i] = Feng.Utils.ConvertHelper.ToDecimal(cell.Value);
                    }
                    else if (timecolumns.Contains(columnindex))
                    {
                        dataRow[i] = Feng.Utils.ConvertHelper.ToDateTime(cell.Value);
                    }
                    else
                    {
                        dataRow[i] = cell.Value;
                    }

                }
                table.Rows.Add(dataRow);
            }
            return table;
        }
    }

    public class CellDataBase : IDataStruct, IReadData, IGrid
    {
        private DataExcel mgrid = null;
        public DataExcel Grid
        {
            get
            {
                return mgrid;
            }
        }
        public CellDataBase(DataExcel grid)
        {
            mgrid = grid;
            Tables = new List<CellTable>();
        }
        public List<CellTable> Tables { get; set; }
        public virtual CellTable MainTable { get; set; }
        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    bw.Write(1, this.Tables.Count);
                    string maintablename = string.Empty;
                    if (this.MainTable != null)
                    {
                        maintablename = this.MainTable.Name;
                    }
                    bw.Write(2, maintablename);
                    foreach (CellTable item in Tables)
                    {
                        bw.Write(item.Data);
                    }
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                int rowcount = reader.ReadIndex(1, 0);
                string maintablename = reader.ReadIndex(2, string.Empty);
                for (int i = 0; i < rowcount; i++)
                {
                    DataStruct dataitem = reader.ReadDataStruct();
                    CellTable item = new CellTable(this);
                    item.Read(dataitem);
                    this.Tables.Add(item);
                    if (item.Name == maintablename)
                    {
                        this.MainTable = item;
                    }
                }
            }

        }
    }

    public class CellTable : IName, IDataStruct, IReadData
    {
        private CellDataBase database = null;
        public CellDataBase DataBase
        {
            get
            {
                return database;
            }
        }
        public CellTable(CellDataBase celldatabase)
        {
            database = celldatabase;
            Rows = new List<CellTableRow>();
            Colums = new Feng.Collections.DictionaryEx<int, CellTableColumn>();
        }

        public string Name { get; set; }

        public List<CellTableRow> Rows { get; set; }
        public Feng.Collections.DictionaryEx<int, CellTableColumn> Colums { get; set; }

        public CellTableRow NewRow()
        {
            CellTableRow row = new CellTableRow(this);
            return row;
        }
        public virtual void AddRow(CellTableRow row)
        {
            Rows.Add(row);
        }
        public virtual void AddColumn(CellTableColumn column)
        {
            Colums.Add(column.Index, column);
        }
        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    bw.Write(1, this.Name);
                    bw.Write(2, this.Colums.Count);
                    foreach (var item in Colums)
                    {
                        bw.Write(item.Value.Data);
                    }
                    bw.Write(3, this.Rows.Count);
                    foreach (CellTableRow item in Rows)
                    {
                        bw.Write(item.Data);
                    }
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                this.Name = reader.ReadIndex(1, this.Name);
                int columncount = reader.ReadIndex(2, 0);

                for (int i = 0; i < columncount; i++)
                {
                    DataStruct dataitem = reader.ReadDataStruct();
                    CellTableColumn item = new CellTableColumn(this);
                    item.Read(dataitem);
                    this.AddColumn(item);
                }
                int rowcount = reader.ReadIndex(3, 0);
                for (int i = 0; i < rowcount; i++)
                {
                    DataStruct dataitem = reader.ReadDataStruct();
                    CellTableRow item = new CellTableRow(this);
                    item.Read(dataitem);
                    this.AddRow(item);
                }
            }

        }

        public virtual CellTableColumn Get(int columnindex)
        {
            return this.Colums.Get(columnindex);
        }
        public virtual CellTableColumn Get(string columnname)
        {
            foreach (var item in Colums)
            {
                if (item.Value.Name == columnname)
                {
                    return item.Value;
                }
            }
            return null;
        }
    }

    public class CellTableRow : IIndex, IDataStruct, IReadData
    {
        private CellTable table = null;
        public CellTable Table
        {
            get
            {
                return table;
            }
        }
        public CellTableRow(CellTable celltable)
        {
            table = celltable;
            Cells = new Feng.Collections.DictionaryEx<CellTableColumn, CellTableCell>();
        }

        public int Index { get; set; }
        public Feng.Collections.DictionaryEx<CellTableColumn, CellTableCell> Cells { get; set; }

        public virtual void Add(CellTableColumn column, CellTableCell cell)
        {
            if (Cells.ContainsKey(column))
                return;
            Cells.Add(column, cell);
        }

        public virtual void Add(CellTableCell cell)
        {
            if (Cells.ContainsKey(cell.Column))
                return;
            Cells.Add(cell.Column, cell);
        }
        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    bw.Write(1, this.Index);
                    bw.Write(2, this.Cells.Count);
                    foreach (var item in Cells)
                    {
                        bw.Write(item.Value.Data);
                    }
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                this.Index = reader.ReadIndex(1, this.Index);
                int rowcount = reader.ReadIndex(2, 0);
                for (int i = 0; i < rowcount; i++)
                {
                    DataStruct dataitem = reader.ReadDataStruct();
                    CellTableCell item = new CellTableCell(this);
                    item.Row = this;
                    item.Read(dataitem);
                    if (item.Column != null)
                    {
                        this.Add(item);
                    }
                }
            }

        }

        public override string ToString()
        {
            return Index.ToString();
        }
    }

    public class CellTableCell : IDataStruct, IReadData
    {
        public CellTableCell(CellTableRow row)
        {
            Row = row;
        }
        public CellTableRow Row { get; set; }
        public CellTableColumn Column { get; set; }
        public ICell Cell { get; set; }
        private object _value = null;
        public object Value
        {
            get
            {
                if (this.Cell != null)
                    return this.Cell.Value;
                return _value;
            }
            set { _value = value; }
        }
        private int icellrowindex = 0;
        private int icellcolumnindex = 0;
#warning 文件加载完成后，必须刷新ICELL，添加删除行时会不一致
        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    if (this.Cell != null)
                    {
                        bw.WriteBaseValue(1, this.Cell.Value);
                        bw.Write(2, this.Cell.Row.Index);
                        bw.Write(3, this.Cell.Column.Index);
                    }
                    else
                    {
                        bw.WriteBaseValue(1, null);
                        bw.WriteBaseValue(2, 0);
                        bw.WriteBaseValue(3, 0);
                    }
                    bw.Write(4, this.Column.Name);
                    data.Data = bw.GetData();

                }
                return data;
            }
        }

        public void Refresh()
        {
            DataExcel grid = this.Row.Table.DataBase.Grid;
            this.Cell = grid[icellrowindex, icellcolumnindex];
        }

        public void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                this.Value = reader.ReadBaseValueIndex(1, null);
                icellrowindex = reader.ReadIndex(2, 0);
                icellcolumnindex = reader.ReadIndex(3, 0);
                string columnname = reader.ReadIndex(4, string.Empty);
                this.Column = this.Row.Table.Get(columnname);
            }

        }

        public override string ToString()
        {
            return string.Format("{0}", Value);
        }
    }
    public class CellTableColumn : IName, IDataStruct, IReadData
    {
        private CellTable table = null;
        public CellTable Table
        {
            get
            {
                return table;
            }
        }
        public CellTableColumn(CellTable celltable)
        {
            table = celltable;
        }
        public string Name { get; set; }
        public int Index { get; set; }

        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    bw.Write(1, this.Name);
                    bw.Write(2, this.Index);
                    data.Data = bw.GetData();

                }
                return data;
            }
        }

        public void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                this.Name = reader.ReadIndex(1, this.Name);
                this.Index = reader.ReadIndex(2, this.Index);
            }

        }
        public override string ToString()
        {
            return Name;
        }
    }



}