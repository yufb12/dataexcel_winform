using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Interface;
using Feng.Forms.Views;

namespace Feng.Forms.Controls.GridControl
{
    public partial class GridView : DivView, Feng.Forms.Interface.IDataStruct, Feng.Print.IPrint, IShowCheck
    {
        public GridView()
        {

        }
        public GridView(Control control)
        {
            mcontrol = control;
            InitParentEvent();
        }
        private void InitParentEvent()
        {
            if (mcontrol != null)
            {
                mcontrol.SizeChanged += mcontrol_SizeChanged;
            }
        }

        void mcontrol_SizeChanged(object sender, EventArgs e)
        {
            this.OnSizeChanged(e);
        }

        #region 构造方法
         
        public virtual GridViewCell this[int rowindex, int columnindex]
        {
            get
            {
                GridViewRow row = this.Rows[rowindex];
                if (row != null)
                {
                    GridViewColumn column = this.Columns[columnindex];
                    if (column != null)
                    {
                        GridViewCell cell = row.Cells[column];
                        return cell;
                    }
                }
                return null;
            }
        }
        public virtual GridViewCell this[int rowindex, string columnindex]
        {
            get
            {
                GridViewRow row = this.Rows[rowindex];
                if (row != null)
                {
                    GridViewColumn column = this.Columns[columnindex];
                    if (column != null)
                    {
                        GridViewCell cell = row.Cells[column];
                        return cell;
                    }
                }
                return null;
            }
        }
        #endregion
 
        #region 初始化方法
        public virtual void RefreshVisible()
        {

        }
        public virtual void RefreshSort()
        {
            if (this.SortData != null)
            {
                this.SortData.Source = this.DataSource;
                this.SortData.Sort();
            }
        }
        public virtual void InitDataSource()
        {
            _SortInfo = null;
            this.Rows.Clear();
            if (this.AutoGenerateColumns)
            {
                this.Columns.Clear();
            }
            this._position = 0;
            GenerateColumns(this.DataSource);
            RefreshColumns();
            RefreshRows();
            RefreshVisible();
            ReSetRowHeight();
            RefreshSort();
            RefreshRowValue();
            RefreshColumnWidth();
            RefreshRowHeight();
            //ReSetRowHeight();
            ReFreshTotalCount();
            this.VScroll.Min = 0;
            this.HScroll.Min = 0;
            this.VScroll.Max = Count - this.Rows.Count;
            this.HScroll.Max = this.Columns.Count;

        }

        public override void Init()
        {

        }
 
        public void BeginSetPosition()
        {

        }

        #endregion

        #region EditControl
        public virtual void AddControl(Control ctl)
        {

        }
        public virtual void RemoveControl(Control ctl)
        {

        }
        #endregion

        #region Columns
        public virtual void GenerateColumns(object datasource)
        {
            if (AutoGenerateColumns)
            {
                if (datasource is DataSet)
                {
                    if (string.IsNullOrEmpty(this._datamember))
                    {
                        InitDefaultColumnFields(((DataSet)datasource).Tables[0]);
                    }
                    else
                    {
                        InitDefaultColumnFields(((DataSet)datasource).Tables[this._datamember]);
                    }
                }
                else if (datasource is DataTable)
                {
                    InitDefaultColumnFields((DataTable)datasource);
                }
                else if (IsIlistDataSource(datasource))
                {
                    InitDefaultColumnFields(datasource as System.Collections.IList);
                }
            }
        }

        public virtual void InitDefaultColumnFields(DataTable table)
        {
            int left = RowHeaderWidth;
            int DefaultColumnWidth = 72;
            GridViewColumn col = null;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                int columnindex = i + 1;
                col = new GridViewColumn(this);
                this.Columns.Add(col);
                col.Caption = table.Columns[i].Caption;
                if (string.IsNullOrWhiteSpace(col.Caption))
                {
                    col.Caption = table.Columns[i].ColumnName;
                }
                col.FieldName = table.Columns[i].ColumnName;
                col.Width = DefaultColumnWidth;
                col.Left = left;
                left = left + DefaultColumnWidth;
                col.DataType = table.Columns[i].DataType.FullName;

                if (table.Columns[i].DataType == typeof(decimal))
                {
                    col.TotalMode = TotalMode.Sum;
                }
                if (table.Columns[i].DataType == typeof(int))
                {
                    col.TotalMode = TotalMode.Sum;
                }
            }

            //col = new GridViewColumn(this);
            //this.Columns.Insert(0, col);
            //col.Caption = "ColumnName";
            //col.Width = DefaultColumnWidth;
            //col.Left = left;
            //left = left + DefaultColumnWidth;

        }

        public virtual void InitDefaultColumnFields(System.Collections.IList datasource)
        {
            Type t = this._DataSource.GetType().GetGenericArguments()[0];
            System.Reflection.PropertyInfo[] ps = t.GetProperties();

            for (int i = 0; i < ps.Length; i++)
            {
                int columnindex = i + 1;
                GridViewColumn col = new GridViewColumn(this);
                col.Caption = ps[i].Name;
                col.FieldName = ps[i].Name;
            }

        }
        public virtual void RefreshRowValue()
        {
            object datasource = this.DataSource;
            if (datasource is DataSet)
            {
                if (string.IsNullOrEmpty(this._datamember))
                {
                    RefreshRowValue(((DataSet)datasource).Tables[0]);
                }
                else
                {
                    RefreshRowValue(((DataSet)datasource).Tables[this._datamember]);
                }
            }
            else if (datasource is DataTable)
            {
                RefreshRowValue((DataTable)datasource);
            }
            else if (IsIlistDataSource(datasource))
            {
                RefreshRowValue(datasource as System.Collections.IList);
            }
        }
        public virtual object GetDataBingdingItem(int index)
        {
            object item = null;
            object datasource = this.DataSource;
            if (datasource is DataSet)
            {
                if (string.IsNullOrEmpty(this._datamember))
                {
                    item = GetDataRow((((DataSet)datasource).Tables[0]), this.SortData as SortDataTable, index);
                }
                else
                {
                    item = GetDataRow((((DataSet)datasource).Tables[this._datamember]), this.SortData as SortDataTable, index);
                }
            }
            else if (datasource is DataTable)
            {
                item = GetDataRow(((DataTable)datasource), this.SortData as SortDataTable, index);
            }
            else if (IsIlistDataSource(datasource))
            {
                item = GetDataRow((datasource as System.Collections.IList), this.SortData as SortModel, index);
            }
            return item;
        }
        public virtual object GetDataBingdingValue(int index,string field)
        {
            object item = GetDataBingdingItem(index);
            if (item is DataRow)
            {
                DataRow dr = item as DataRow;
                return dr[field];
            }
            else
            {
                return Feng.Utils.ReflectionHelper.GetValue(item, field);
            } 
        }
        public virtual object GetDataBingdingValue(int index, int field)
        {
            object item = GetDataBingdingItem(index);
            if (item is DataRow)
            {
                DataRow dr = item as DataRow;
                return dr[field];
            }
            return null;
        }

        public virtual void SetDataBingdingValue(int index, string field,object value)
        {
            object item = GetDataBingdingItem(index);
            if (item is DataRow)
            {
                DataRow dr = item as DataRow;
                dr[field] = value;
            }
            else
            {
                Feng.Utils.ReflectionHelper.SetValue(item, field,value);
            }
        }
 
        public virtual void RefreshRowValue(System.Collections.IList datasource)
        {
            int rowindex = 0;
            SortModel sortdatatable = this.SortData as SortModel;
            for (int i = Position; i < datasource.Count; i++)
            {
                object datarow = GetDataRow(datasource, sortdatatable, i);
                if (datarow == null)
                {
                    continue;
                }
                if (this.Rows.Count > rowindex)
                {
                    GridViewRow row = this.Rows[rowindex];
                    row.Index = Position + rowindex + 1;
                    row.InitDataBoundItem(datarow);
                    RefreshCellValue(datarow, rowindex);
                }
                else
                {
                    break;
                }
                rowindex++;
            }
        }

        public virtual void RefreshRowValue(DataTable table)
        {
            int rowindex = 0; 
            SortDataTable sortdatatable = this.SortData as SortDataTable;
            for (int i = Position; i < table.Rows.Count; i++)
            {
                DataRow datarow = GetDataRow(table, sortdatatable, i);
                if (datarow == null)
                {
                    continue;
                }
                if (this.Rows.Count > rowindex)
                {
                    GridViewRow row = this.Rows[rowindex];
                    row.Index = Position + rowindex + 1;
                    row.InitDataBoundItem(datarow);
                    RefreshCellValue(datarow, rowindex);
                }
                else
                {
                    break;
                }
                rowindex++;
            }
        }
        public virtual DataRow GetDataRow(DataTable table, SortDataTable sortdatatable, int index)
        {
            DataRow datarow = null;
            if (sortdatatable == null)
            {
                if (index < table.Rows.Count)
                {
                    datarow = table.Rows[index];
                }
            }
            else
            {
                datarow = sortdatatable.GetValue(index) as DataRow;
            }
            return datarow;
        }
        public virtual object GetDataRow(System.Collections.IList table, SortModel sortdatatable, int index)
        {
            object datarow = null;
            if (sortdatatable == null)
            {
                if (index < table.Count)
                {
                    datarow = table[index];
                }
            }
            else
            {
                datarow = sortdatatable.GetValue(index);
            }
            return datarow;
        }
        
        public virtual void RefreshCellValue(DataRow datarow, int rowindex)
        {
            GridViewRow row = this.Rows[rowindex];
            if (row == null)
            {
                return;
            }
            DataTable table = null;
            if (datarow != null)
            {
                table = datarow.Table;
            }
            for (int colindex = 0; colindex < this.Columns.Count; colindex++)
            {
                GridViewColumn col = this.Columns[colindex];
                GridViewCell cell = row.Cells[col];
                if (cell == null)
                {
                    cell = AddGridViewCell(rowindex, colindex);
                }
                else
                {
                    cell.Column = col;
                }
                if (table == null || datarow == null)
                { 
                    cell.Value = null;
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(col.FieldName))
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName.ToUpper() == col.FieldName.ToUpper())
                        {
                            object value= datarow[col.FieldName];
                            cell.InitValue(value);
                            break;
                        }
                    }
                }
            }
        }
        public virtual void RefreshCellValue(object datarow, int rowindex)
        {
            GridViewRow row = this.Rows[rowindex];
            if (row == null)
            {
                return;
            }
 
            for (int colindex = 0; colindex < this.Columns.Count; colindex++)
            {
                GridViewColumn col = this.Columns[colindex];
                GridViewCell cell = row.Cells[col];
                if (cell == null)
                {
                    cell = AddGridViewCell(rowindex, colindex);
                }
                else
                {
                    cell.Column = col;
                } 
                if (!string.IsNullOrWhiteSpace(col.FieldName))
                { 
                    object value  = Feng.Utils.ReflectionHelper.GetValue(datarow, col.FieldName);
                    cell.InitValue(value);  
                }
            }
        }
        public virtual GridViewCell AddGridViewCell(int rowindex, int colindex)
        {
            GridViewRow row = this.Rows[rowindex];
            GridViewColumn col = this.Columns[colindex];
            GridViewCell cell = new GridViewCell(row);
            cell.Column = col;
            row.Cells.Add(cell);
            return cell;
        }
        public virtual bool IsIlistDataSource(object datasource)
        {
            return datasource is System.Collections.IList;
            //Type t = datasource.GetType().GetGenericArguments()[0];
            //t = datasource.GetType().GetInterface("System.Collections.Generic.IList");
            //if (t != null)
            //{
            //    return true;
            //}
            //return false;
        }
 
        public virtual void RefreshAll()
        {
            this.BeginReFresh();
            this.EndEdit();
            RefreshColumns();
            RefreshVisible();
            RefreshRows();
            RefreshRowHeight();
            RefreshRowValue(); 
            this.EndReFresh();
        }
        public virtual void RefreshRows()
        {
            int footerheight = this.Footer.Height;
            if (!this.ShowFooter)
            {
                footerheight = 2;
            }
  
            int rowcount = (int)Math.Floor((this.Height - this.TopSapce - this.HScroll.Height - footerheight - 2) * 1d / this.RowHeight);
            if (this.Rows.Count > rowcount)
            {
                int count = this.Rows.Count - rowcount;
                for (int i = 0; i < count; i++)
                {
                    int index = this.Rows.Count - 1;
                    if (index > 0)
                    {
                        this.Rows.RemoveAt(index);
                    }
                }
            }
            else
            {
                int count = rowcount - this.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    AddNewRow();
                }
            } 
        }
        public virtual void AddNewRow()
        {
            GridViewRow r = new GridViewRow(this);
            this.Rows.Add(r);
        }
        public List<GridViewColumn> visibleColumns = new List<GridViewColumn>();
        //public void RefreshColumnWidth()
        //{
        //    try
        //    { 
        //        if (this.Control == null)
        //            return;
        //        Graphics g = this.Control.CreateGraphics();
        //        foreach (GridViewColumn col in Columns)
        //        {
        //            SizeF sf = Feng.Drawing.GraphicsHelper.Sizeof(col.Caption, this.Font, g);
        //            if (col.AutoWidth)
        //            {
        //                col.Width = (int)sf.Width;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Feng.Utils.TraceHelper.WriteTrace("GridView", "GridViewColumn", "RefreshColumnWidth", ex);

        //    }
        //}
        public void RefreshColumns()
        {
            int left = LeftSapce; 
            visibleColumns.Clear();
            List<GridViewColumn> listdo = new List<GridViewColumn>();
            for (int i = 0; i < FrozenColumn && i < this.Columns.Count; i++)
            {
                GridViewColumn col = Columns[i];
                listdo.Add(col);
                if (!col.Visible)
                {
                    continue;
                }
                col.Left = left;
                left = left + col.Width;
                visibleColumns.Add(col);
                if (left > this.Width)
                {
                    break;
                }
            }
            for (int i = FirstColumn; i < this.Columns.Count; i++)
            {
                GridViewColumn col = Columns[i];
                if (listdo.Contains(col))
                    continue;
                if (!col.Visible)
                {
                    continue;
                }
                col.Left = left;
                left = left + col.Width;
                visibleColumns.Add(col);
                if (left > this.Width)
                {
                    break;
                }
            }
            RefreshHScrollVisable();

        }
        public virtual void ReSetRowHeight()
        {
            int top = this.TopSapce; 
            for (int i = 0; i < this.Rows.Count; i++)
            {
                GridViewRow r = this.Rows[i]; 
                r.Top = top;
                top = top + r.Height;
            }

        }
        public virtual void CloseEdit()
        {
            if (this.Edit != null)
            {
                this.Edit.EndEdit();
            }
            this.Edit = null;
            this.EditCell = null;
        }
        public virtual void InitEdit()
        {
            if (this.ReadOnly)
            {
                return;
            }
            if (this.FocusedCell != null)
            {
                this.FocusedCell.InitEdit();
                this.Invalidate();
            }
        }
        public virtual void ClearSelect()
        {
            SelectCells.Clear();
        }
        public virtual void RefreshHScrollVisable()
        {
            int width = 0;
            if (this.ShowRowHeader)
            {
                width = this.RowHeaderWidth;
            }
            foreach (GridViewColumn column in this.Columns)
            {
                if (column.Visible)
                {
                    width = width + column.Width;
                }
            }
            if (width > this.Width)
            {
                this.HScroll.Max = this.Columns.Count;
                this.HScroll.Visible = true;
            }
            else
            {
                this.HScroll.Visible = false;
            }
        }
        public virtual void EndEdit()
        {
            GridViewCell cell = this.EditCell;
            CloseEdit();
            if (cell != null)
            {
                this.OnCellEndEdit(cell);
            }
        }
        #endregion

        #region 保存


        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = string.Empty,
                    Name = string.Empty,
                };

                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, this._AutoGenerateColumns);
                    bw.Write(2, this._backcolor);
                    bw.Write(3, this._columnheaderheight);
                    bw.Write(4, this._datamember);
                    bw.WriteSerializeValue(5, this._DataSource);
                    bw.Write(6, (int)this._EditMode);
                    bw.Write(7, this._firstcolumn);
                    bw.Write(8, this._font);
                    bw.Write(9, this._forecolor);
                    bw.Write(10, this._Frozencolumn);
                    bw.Write(11, this._frozenrow);
                    bw.Write(12, this._height);
                    bw.Write(13, this._left);
                    bw.Write(14, this._position);
                    bw.Write(15, this._rowheaderwidth);
                    bw.Write(16, this._rowheight);
                    bw.Write(17, this._showcolumndesign);
                    bw.Write(18, this._SrollStep);
                    bw.Write(19, this._top);
                    bw.Write(20, this._width);
                    DataStructCollection datastructs = new DataStructCollection();
                    foreach (GridViewColumn col in this.Columns)
                    {
                        datastructs.Add(col.Data);
                    }
                    bw.Write(21, datastructs);
                    bw.Write(22, this._showfooter);
                    bw.Write(23, this._readonly);
                    bw.Write(23, this._RightButtonClickSelect);

                    data.Data = bw.GetData();
                    bw.Close();
                }

                return data;
            }
        }


        public virtual void ReadDataStruct(DataStruct data)
        {
            using (Feng.IO.BufferReader stream = new IO.BufferReader(data.Data))
            {
                this._AutoGenerateColumns = stream.ReadIndex(1, this._AutoGenerateColumns);
                this._backcolor = stream.ReadIndex(2, this._backcolor);
                this._columnheaderheight = stream.ReadIndex(3, this._columnheaderheight);
                this._datamember = stream.ReadIndex(4, this._datamember);
                this._DataSource = stream.ReadSerializeValueIndex(5, this._DataSource);
                this._EditMode = (EditMode)stream.ReadIndex(6, (int)this._EditMode);
                this._firstcolumn = stream.ReadIndex(7, this._firstcolumn);
                this._font = stream.ReadIndex(8, this._font);
                this._forecolor = stream.ReadIndex(9, this._forecolor);
                this._Frozencolumn = stream.ReadIndex(10, this._Frozencolumn);
                this._frozenrow = stream.ReadIndex(11, this._frozenrow);
                this._height = stream.ReadIndex(12, this._height);
                this._left = stream.ReadIndex(13, this._left);
                this._position = stream.ReadIndex(14, this._position);
                this._rowheaderwidth = stream.ReadIndex(15, this._rowheaderwidth);
                this._rowheight = stream.ReadIndex(16, this._rowheight);
                this._showcolumndesign = stream.ReadIndex(17, this._showcolumndesign);
                this._SrollStep = stream.ReadIndex(18, this._SrollStep);
                this._top = stream.ReadIndex(19, this._top);
                this._width = stream.ReadIndex(20, this._width);
                DataStructCollection datastructs = null;
                datastructs = stream.ReadIndex(21, datastructs);
                this._showfooter = stream.ReadIndex(22, this._showfooter);
                this._readonly = stream.ReadIndex(22, this._readonly);
                this._RightButtonClickSelect = stream.ReadIndex(23, this._RightButtonClickSelect);

                foreach (DataStruct ds in datastructs)
                {
                    GridViewColumn col = new GridViewColumn(this);
                    col.Read(ds);
                    this.Columns.Add(col);
                }
            }
        }
        #endregion
 
        #region MoveFocuedCell
        public virtual void Focus()
        {

        }
        public virtual void MoveFocusedCellToNextCell(NextCellType nct, bool initedit)
        {
            this.Focus();
            GridViewCell cell = this.FocusedCell;
            if (this.FocusedCell == null)
            {
                cell = this[1, 1];
            }
            if (cell == null)
                return;
            BeforeMoveFocusCellCancelArgs e = new BeforeMoveFocusCellCancelArgs()
            {
                Cell = cell
            };
            this.OnBeforeMoveToNextCell(e, nct);
            if (e.Cancel)
            {
                if (this.FocusedCell == e.Cell)
                    return;
                this.FocusedCell = e.Cell;
                if (initedit)
                {
                    this.FocusedCell.InitEdit();
                }
                return;
            }
            int rindex = cell.Row.Index;
            int cindex = cell.Column.Index;
            this.ClearSelect();
            if (this.FocusedCell != null)
            {
                switch (nct)
                {
                    case NextCellType.Left:
                        rindex = cell.Row.Index;
                        cindex = cell.Column.Index;
                        if (cindex > 1)
                        {
                            cindex = cindex - 1;
                            cell = this[rindex, cindex]; 
                            this.FocusedCell = cell;
                            if (initedit)
                            {
                                this.FocusedCell.InitEdit();
                            }
                        }
                        break;
                    case NextCellType.Up:
                        rindex = cell.Row.Index;
                        cindex = cell.Column.Index;
                        if (rindex > 1)
                        {
                            rindex = rindex - 1;
                            cell = this[rindex, cindex]; 
                            this.FocusedCell = cell;
                            if (initedit)
                            {
                                this.FocusedCell.InitEdit();
                            }
                        }
                        break;
                    case NextCellType.Right:
                        rindex = cell.Row.Index;
                        cindex = cell.Column.Index;
                        cindex = cindex + 1;
                        cell = this[rindex, cindex]; 
                        this.FocusedCell = cell;
                        if (initedit)
                        {
                            this.FocusedCell.InitEdit();
                        }
                        break;
                    case NextCellType.Down:
                        rindex = cell.Row.Index;
                        cindex = cell.Column.Index;
                        rindex = rindex + 1;
                        cell = this[rindex, cindex]; 
                        this.FocusedCell = cell;
                        if (initedit)
                        {
                            this.FocusedCell.InitEdit();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public virtual void MoveFocusedCellToLeftCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Left, false);
        }
        public virtual void MoveFocusedCellToTopCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Up, false);
        }
        public virtual void MoveFocusedCellToRightCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Right, false);
        }
        public virtual void MoveFocusedCellToBottomCell()
        {
            MoveFocusedCellToNextCell(NextCellType.Down, false);
        }

        public virtual void MoveFocusedCellToLeftCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Left, true);
        }
        public virtual void MoveFocusedCellToTopCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Up, true);
        }
        public virtual void MoveFocusedCellToRightCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Right, true);
        }
        public virtual void MoveFocusedCellToBottomCellAndInitEdit()
        {
            MoveFocusedCellToNextCell(NextCellType.Down, true);
        }

        #endregion

        #region COLUMN SIZE CHANGED
         
        private Rectangle MouseDownRect = Rectangle.Empty;
        private int sizechangedwidth = 10;

        private Feng.Forms.Controls.GridControl.GridViewColumn sizechangedcolumn = null;

        public bool OnMouseDownCellSelect(object sender, MouseEventArgs e,Point pt)
        {
            foreach (Feng.Forms.Controls.GridControl.GridViewRow r in this.Rows)
            {
                if (r.Rect.Contains(pt))
                {
                    if (r.OnMouseDown(sender, e, EventViewArgs.GetEventViewArgs(this.mcontrol)))
                        return true;
                    Feng.Forms.Controls.GridControl.GridViewColumn column = null; 
                    foreach (GridViewColumn c in visibleColumns)
                    {
                        if (c.Rect.Contains(pt))
                        {
                            column = c;
                            Feng.Forms.Controls.GridControl.GridViewCell cell = r.Cells[c];
                            if (cell != null)
                            {
                                this.EndEdit();
                                OnFocusedCellChanged(cell);
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public virtual bool OnMouseDownColumnSplitSelect(Point pt)
        {
            Rectangle rect = Rectangle.Empty;
            foreach (GridViewColumn col in this.Columns)
            {
                rect = new Rectangle(col.Right - sizechangedwidth, col.Top, sizechangedwidth, ColumnHeaderHeight);
                if (rect.Contains(pt))
                {
                    this.SelectMode = SelectModel_ColumnSizeChanged;
                    sizechangedcolumn = col;
                    MouseDownRect = new Rectangle(pt, new Size(col.Width, col.Height));
                    return true;
                }
            }
            return false;
        }
        public virtual bool OnMouseMoveColumnSplitSelect(Point pt)
        {
            Rectangle rect = Rectangle.Empty;
            foreach (GridViewColumn col in this.Columns)
            {
                rect = new Rectangle(col.Right - sizechangedwidth, col.Top, sizechangedwidth, ColumnHeaderHeight);
                if (rect.Contains(pt))
                {
                    this.BeginSetCursor(Cursors.VSplit);
                    return true;
                }
            }
            return false;
        }
        public override void BeginSetCursor(Cursor begincursor)
        {

        }
        public virtual void OnMouseMoveColumnSplitWidth(Point pt)
        {
            int width = pt.X - MouseDownRect.X;
            if (sizechangedcolumn != null)
            {
                width = MouseDownRect.Width + width;
                if (width < 0)
                {
                    width = 0;
                }
                else if (width > this.Width * 0.8)
                {
                    width = this.Width * 8 / 10;
                }
                sizechangedcolumn.Width = width;

                this.OnColumnChanged(sizechangedcolumn, ChangedReason.SizeChanged);
                
                RefreshColumns();
            }
        }
        public virtual void SetMouseDownSplit()
        {

        }
 
        public virtual void MouseUp()
        {
            this.SelectMode = SelectModel_Null;
            sizechangedcolumn = null;
        }
        public virtual void ReFreshScroll()
        {
            int rowcount = this.Rows.Count;
            int datacount=0;
            DataTable table = this.DataSource as DataTable;
            if (table != null)
            {
                datacount = table.Rows.Count;
                if (datacount > rowcount)
                {
                    this.VScroll.Visible = true;
                    return;
                } 
            }
            this.VScroll.Visible = false;
        }
        public virtual void RefreshRowHeight()
        { 
            if (this.RowAutoSize)
            {
                if (this.Control != null)
                {
                    foreach (GridViewRow row in this.Rows)
                    {
                        int rowheight = this.RowHeight;
                        if (rowheight > this.RowHeight * triple)
                        {
                            rowheight = this.RowHeight * triple;
                        }
                        row.Height = rowheight;
                    }
                    ReSetRowHeight();
                    ReFreshScroll();
                }
            }
        }
        public virtual void RefreshColumnWidth()
        {
            if (this.ColumnAutoSize)
            {
                if (this.Control != null)
                {
                    int spacewidth = this.ColumnWidth * triple;
                    Graphics g = Control.CreateGraphics();
                    foreach (GridViewColumn col in this.Columns)
                    {
                        if (!col.AutoWidth)
                            continue;
                        int rowheight = this.ColumnWidth;

                        SizeF sf = g.MeasureString(col.Caption, this.Font);
                        if (rowheight < sf.Width)
                        {
                            rowheight = (int)sf.Width + 16;
                        }
                        foreach (GridViewRow row in this.Rows)
                        {
                            GridViewCell cell = row.Cells[col];
                            if (cell == null)
                            {
                                continue;
                            }
                            if (!string.IsNullOrWhiteSpace(cell.Text))
                            {
                                sf = g.MeasureString(cell.Text, this.Font, spacewidth);
                                if (rowheight < sf.Width)
                                {
                                    rowheight = (int)sf.Width+4;
                                }
                            }
                        }
                        if (rowheight > this.ColumnWidth * triple)
                        {
                            rowheight = this.ColumnWidth * triple;
                        }
                        col.Width = rowheight;
                    }
                    RefreshColumns();
                }
            }
        }
        #endregion


        #region 合计行
        public void ReFreshTotalCount()
        {
            ReFreshTotalCount(string.Empty);
        }
        public void ReFreshTotalCount(string field)
        {
            object datasource = this.DataSource;
            if (datasource is DataSet)
            {
                if (string.IsNullOrEmpty(this._datamember))
                {
                    ReFreshTotalCount(((DataSet)datasource).Tables[0], field);
                }
                else
                {
                    ReFreshTotalCount(((DataSet)datasource).Tables[this._datamember], field);
                }
            }
            else if (datasource is DataTable)
            {
                ReFreshTotalCount((DataTable)datasource, field);
            }
            else if (IsIlistDataSource(datasource))
            {
                ReFreshTotalCount(datasource as System.Collections.IList, field);
            }
        }

        public void ReFreshTotalCount(System.Collections.IList list, string field)
        {

        }

        public void ReFreshTotalCount(DataTable table, string field)
        {
            if (!string.IsNullOrEmpty(field))
            {
                foreach (GridViewColumn col in this.Columns)
                {
                    if (field == col.FieldName)
                    {
                        switch (col.TotalMode)
                        {
                            case TotalMode.Sum:
                                col.TotalValue = Feng.Data.DataTableHelper.Sum(table, col.FieldName);
                                break;
                            case TotalMode.Avg:
                                col.TotalValue = Feng.Data.DataTableHelper.Avg(table, col.FieldName);
                                break;
                            case TotalMode.Count:
                                col.TotalValue = Feng.Data.DataTableHelper.Count(table, col.FieldName);
                                break;
                            case TotalMode.Max:
                                col.TotalValue = Feng.Data.DataTableHelper.Max(table, col.FieldName);
                                break;
                            case TotalMode.Min:
                                col.TotalValue = Feng.Data.DataTableHelper.Min(table, col.FieldName);
                                break;
                            default:
                                break;
                        }
                    }
                }
                return;
            }
            foreach (GridViewColumn col in this.Columns)
            {
                if (!string.IsNullOrEmpty(col.FieldName))
                {
                    switch (col.TotalMode)
                    {
                        case TotalMode.Sum:
                            col.TotalValue = Feng.Data.DataTableHelper.Sum(table, col.FieldName);
                            break;
                        case TotalMode.Avg:
                            col.TotalValue = Feng.Data.DataTableHelper.Avg(table, col.FieldName);
                            break;
                        case TotalMode.Count:
                            col.TotalValue = Feng.Data.DataTableHelper.Count(table, col.FieldName);
                            break;
                        case TotalMode.Max:
                            col.TotalValue = Feng.Data.DataTableHelper.Max(table, col.FieldName);
                            break;
                        case TotalMode.Min:
                            col.TotalValue = Feng.Data.DataTableHelper.Min(table, col.FieldName);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion

        #region 多表头
        private MultipleheaderCollection _MultipleHeaders = null;
        [Browsable(false)]
        public virtual MultipleheaderCollection MultipleHeaders
        {
            get
            {
                if (_MultipleHeaders == null)
                {
                    _MultipleHeaders = new MultipleheaderCollection(this);
                }
                return _MultipleHeaders;
            }
            set
            {
                _MultipleHeaders = value;
            }
        }
        #endregion

        #region 编辑控件
        public static Feng.Forms.Interface.IEditView GetCellEdit(DataStruct ds)
        {
            Feng.Forms.Interface.IEditView owncontrol = null;
            if (!string.IsNullOrEmpty(ds.DllName))
            {
                owncontrol = CreateInatance<Feng.Forms.Interface.IEditView>(ds.DllName, ds.FullName,
ds.AessemlyDownLoadUrl, null);
            }
            else
            {
                string fullname = ds.FullName;
                owncontrol = GetCellEdit(fullname);
            }
            return owncontrol;
        }

        public static Feng.Forms.Interface.IEditView GetCellEdit(string fullname)
        {
            Feng.Forms.Interface.IEditView owncontrol = null;
            if (fullname == typeof(CellNumber).FullName)
            {
                owncontrol = new CellNumber();
            }
            return owncontrol;
        }

        public static T CreateInatance<T>(string filename, string fullname, string downloadurl, object[] args) where T : class
        {
            if (!System.IO.File.Exists(filename))
            {
                try
                {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        wc.DownloadFile(downloadurl, filename);
                    }
                }
                catch (Exception)
                {
                    return null;
                }

            }
            if (!System.IO.File.Exists(filename))
            {
                return null;
            }
            Assembly.LoadFrom(filename);
            Type type = Type.GetType(filename, false);
            if (type != null)
            {
                return Activator.CreateInstance(type, args) as T;
            }
            return null;
        }

        #endregion

        #region 获取值

        public virtual DataRow GetFocusDataRow()
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.Row.DataBoundItem as DataRow;
            }
            return null;
        }
        public virtual object GetFocusRow()
        {
            if (this.FocusedCell != null)
            {
                return this.FocusedCell.Row.DataBoundItem;
            }
            return null;
        }

        #endregion
    }
}

