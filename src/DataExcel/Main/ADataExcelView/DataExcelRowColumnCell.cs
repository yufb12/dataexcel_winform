using Feng.Drawing;
using Feng.Enums;
using Feng.Excel.App;
using Feng.Excel.Args;
using Feng.Excel.Base;
using Feng.Excel.Chart;
using Feng.Excel.Collections;
using Feng.Excel.Extend;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        #region 行列操作
        public virtual void InsertRow()
        {
            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinRow();
                int maxrow = this.SelectCells.MaxRow();
                for (int index = minrow; index <= maxrow; index++)
                {
                    IRow row = this.ClassFactory.CreateDefaultRow(this, index);
                    this.Rows.Insert(index, row);
                }
                this.Rows.Refresh();
            }
        }
        public virtual void InsertRow(SelectCellCollection cells)
        {
            if (cells != null)
            {
                int minrow = this.SelectCells.MinRow();
                int maxrow = this.SelectCells.MaxRow();
                for (int index = minrow; index <= maxrow; index++)
                {
                    IRow row = this.ClassFactory.CreateDefaultRow(this, index);
                    this.Rows.Insert(index, row);
                }
            }
        }
        public virtual void InsertColumn()
        {
            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinColumn();
                int maxrow = this.SelectCells.MaxColumn();
                for (int index = minrow; index <= maxrow; index++)
                {
                    IColumn row = this.ClassFactory.CreateDefaultColumn(this, index);
                    this.Columns.Insert(index, row);
                }
            }
        }
        public virtual void DeleteColumn(List<ICell> list)
        {
   
            foreach (ICell cell in list)
            { 

                int minrow = cell.MaxColumnIndex;
                int maxrow = cell.Column.Index;

                DeleteColumnMerge(minrow, maxrow);
                DeleteColumnBackCell(minrow, maxrow);
                int[] values = new int[maxrow - minrow + 1];
                for (int i = maxrow; i >= minrow; i--)
                {
                    this.Columns.RemoveAt(i);
                } 
            }
            this.Columns.Refresh();
            this.ReSetMergeCellSize();
            this.ReFreshFirstDisplayColumnIndex();
        }
        public virtual void DeleteColumn()
        {

            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinColumn();
                int maxrow = this.SelectCells.MaxColumn();

                DeleteColumnMerge(minrow, maxrow);
                DeleteColumnBackCell(minrow, maxrow);
                int[] values = new int[maxrow - minrow + 1];
                for (int i = maxrow; i >= minrow; i--)
                {
                    this.Columns.RemoveAt(i);
                }
                this.Columns.Refresh();

                this.ReSetMergeCellSize(); 
            }
        }
        public virtual void HideColumn()
        {

            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinColumn();
                int maxrow = this.SelectCells.MaxColumn();

                for (int i = maxrow; i >= minrow; i--)
                {
                    IColumn column = this.Columns[i];
                    if (column != null)
                    {
                        column.Visible = false;
                    }
                }

                this.ReSetMergeCellSize();
            }
        }
        public virtual void HideColumn(List<ICell> list)
        {

            foreach (ICell cell in list)
            {

                int minrow = cell.MaxColumnIndex;
                int maxrow = cell.Column.Index;
                for (int i = maxrow; i >= minrow; i--)
                {
                    IColumn column = this.Columns[i];
                    if (column != null)
                    {
                        column.Visible = false;
                    }
                }
            }
            this.ReSetMergeCellSize();
        }
        public virtual void ShowColumn(List<ICell> list)
        {

            foreach (ICell cell in list)
            {

                int minrow = cell.MaxColumnIndex;
                int maxrow = cell.Column.Index;
                for (int i = maxrow; i >= minrow; i--)
                {
                    IColumn column = this.Columns[i];
                    if (column != null)
                    {
                        column.Visible = true;
                    }
                }
            }
            this.ReSetMergeCellSize();
        }
        public virtual void ShowColumn()
        {

            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinColumn();
                int maxrow = this.SelectCells.MaxColumn();

                for (int i = maxrow; i >= minrow; i--)
                {
                    IColumn column = this.Columns[i];
                    if (column != null)
                    {
                        column.Visible = true;
                    }
                }

                this.ReSetMergeCellSize();
            }
        }


        public virtual void HideRow()
        {

            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinRow();
                int maxrow = this.SelectCells.MaxRow();

                for (int i = maxrow; i >= minrow; i--)
                {
                    IRow column = this.Rows[i];
                    if (column != null)
                    {
                        column.Visible = false;
                    }
                }

                this.ReSetMergeCellSize();
            }
        }
        public virtual void HideRow(List<ICell> list)
        {

            foreach (ICell cell in list)
            {

                int minrow = cell.MaxRowIndex;
                int maxrow = cell.Row.Index;
                for (int i = maxrow; i >= minrow; i--)
                {
                    IRow column = this.Rows[i];
                    if (column != null)
                    {
                        column.Visible = false;
                    }
                }
            }
            this.ReSetMergeCellSize();
        }
        public virtual void ShowRow(List<ICell> list)
        {

            foreach (ICell cell in list)
            {

                int minrow = cell.MaxRowIndex;
                int maxrow = cell.Row.Index;
                for (int i = maxrow; i >= minrow; i--)
                {
                    IRow column = this.Rows[i];
                    if (column != null)
                    {
                        column.Visible = true;
                    }
                }
            }
            this.ReSetMergeCellSize();
        }
        public virtual void ShowRow()
        {

            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinRow();
                int maxrow = this.SelectCells.MaxRow();

                for (int i = maxrow; i >= minrow; i--)
                {
                    IRow column = this.Rows[i];
                    if (column != null)
                    {
                        column.Visible = true;
                    }
                }

                this.ReSetMergeCellSize();
            }
        }

        public virtual void DeleteRow()
        {

            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinRow();
                int maxrow = this.SelectCells.MaxRow();

                DeleteRowMerge(minrow, maxrow);
                DeleteRowBackCell(minrow, maxrow);
                for (int index = maxrow; index >= minrow; index--)
                {
                    this.Rows.RemoveAt(index);
                }
                this.Rows.Refresh();
                this.ReSetMergeCellSize();
            }
        }      
        public virtual void DeleteEmptyRow()
        {
            List<IRow> delrows = new List<IRow>();
            foreach (IRow row in this.Rows)
            {
                if (row.Index < 1)
                    continue;
                bool hasvalue = false;
                foreach (IColumn  column in this.Columns)
                {
                    if (column.Index < 1)
                        continue;
                    ICell cell = row[column];
                    if (cell == null)
                    {
                        continue;
                    }
                    if (cell.Value != null)
                    {
                        hasvalue = true;
                    }
                    if (cell.OwnEditControl != null)
                    {
                        hasvalue = true;
                    }
                    if (cell.OwnMergeCell != null)
                    {
                        hasvalue = true;
                    }
                    if (cell.BackColor != Color.Empty)
                    {
                        hasvalue = true;
                    }
                    if (cell.BackImage != null)
                    {
                        hasvalue = true;
                    }
                }
                if (!hasvalue)
                {
                    delrows.Add(row);
                }
            }
            delrows.Sort(Compare);
            foreach (IRow item in delrows)
            {
                this.Rows.Remove(item);
            }
        }

        public int Compare(IRow row1, IRow row2)
        {
            if (row1.Index > row2.Index)
            {
                return -1;
            }

            if (row1.Index < row2.Index)
            {
                return 1;
            }
            return 0;
        }


        public virtual void DeleteRow(List<ICell> list)
        {
            foreach (ICell cell in list)
            {
                int minrow = cell.Row.Index;
                int maxrow = cell.MaxRowIndex;
                DeleteColumnMerge(minrow, maxrow);
                DeleteColumnBackCell(minrow, maxrow);
                for (int index = maxrow; index >= minrow; index--)
                {
                    this.Rows.RemoveAt(index);
                }
            }
            this.Rows.Refresh();

        }
        public virtual void ClearCell()
        {
            this.ClearCell(this.GetSelectCells());
        }
        public virtual void ClearCell(List<ICell> list)
        {

            foreach (ICell cell in list)
            {
                if (cell != null)
                {
                    if (cell.Row != null)
                    {
                        cell.Row.Cells.Remove(cell);
                    }
                }
            }
        }

        public void DeleteColumnMerge(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                foreach (IRow item in this.Rows)
                {
                    ICell cell = item.Cells[i];
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            if (cell.OwnMergeCell.BeginCell.Column.Index >= min && cell.OwnMergeCell.EndCell.Column.Index <= max)
                            {
                                this.UnMergeCell(cell.OwnMergeCell);
                            }
                        }
                    }
                }
            }
        }

        public void DeleteColumnBackCell(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                foreach (IRow item in this.Rows)
                {
                    ICell cell = item.Cells[i];
                    if (cell != null)
                    {
                        if (cell.OwnBackCell != null)
                        {
                            if (cell.OwnBackCell.BeginCell.Column.Index >= min && cell.OwnBackCell.EndCell.Column.Index <= max)
                            {
                                this.UnBackCell(cell.OwnBackCell);
                            }
                        }
                    }
                }
            }
        }


        public void DeleteRowMerge(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                foreach (IRow item in this.Rows)
                {
                    ICell cell = item.Cells[i];
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            if (cell.OwnMergeCell.BeginCell.Row.Index >= min && cell.OwnMergeCell.EndCell.Row.Index <= max)
                            {
                                this.UnMergeCell(cell.OwnMergeCell);
                            }
                        }
                    }
                }
            }
        }

        public void DeleteRowBackCell(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                foreach (IRow item in this.Rows)
                {
                    ICell cell = item.Cells[i];
                    if (cell != null)
                    {
                        if (cell.OwnBackCell != null)
                        {
                            if (cell.OwnBackCell.BeginCell.Row.Index >= min && cell.OwnBackCell.EndCell.Row.Index <= max)
                            {
                                this.UnBackCell(cell.OwnBackCell);
                            }
                        }
                    }
                }
            }
        }

        public virtual void InsertColumn(SelectCellCollection cells)
        {
            if (cells != null)
            {
                int minrow = this.SelectCells.MinColumn();
                int maxrow = this.SelectCells.MaxColumn();
                for (int index = minrow; index <= maxrow; index++)
                {
                    IColumn row = this.ClassFactory.CreateDefaultColumn(this, index);
                    this.Columns.Insert(index, row);
                }
            }
        }
        public virtual void InsertRow(int index)
        {
            IRow row = this.ClassFactory.CreateDefaultRow(this, index);
            this.Rows.Insert(index, row);
        }

        public virtual void InsertColumn(int index)
        {
            IColumn row = this.ClassFactory.CreateDefaultColumn(this, index);
            this.Columns.Insert(index, row);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="direction">Direction.Top</param>
        public virtual void InsertCell(ICell cell, int direction)
        {
            int rowmaxindex = cell.MaxRowIndex;
            int columnmaxindex = cell.MaxColumnIndex;
            int rowminindex = cell.Row.Index;
            int columnminindex = cell.Column.Index;
            switch (direction)
            {
                case Direction.Top:
                    InsertCellMoveDown(cell, 1);
                    this.Rows.Refresh();
                    break;
                case Direction.Bottom:
                    InsertCellMoveUp(cell);
                    this.Rows.Refresh();
                    break;
                case Direction.Right:
                    InsertCellMoveRight(cell, 1);
                    this.Columns.Refresh();
                    break;
                case Direction.Left:
                    InsertCellMoveLeft(cell);
                    this.Columns.Refresh();
                    break;
                default:
                    return;
            }
            foreach (IMergeCell mc in this.MergeCells)
            {

                mc.Refresh();
            }
            //this.ReFreshFirstDisplayRowIndex();
            //this.ReFreshFirstDisplayColumnIndex();
        }
        public virtual void InsertCellMoveUp(ICell cell)
        {
            int rowminindex = cell.Row.Index;
            int columnminindex = cell.Column.Index;
            int maxrow = ReSetHasValue().X;

            IRow row = this.Rows[rowminindex - 1];
            if (row != null)
            {
                row.Cells.RemoveAt(columnminindex);
            }
            ClearMergeAndBackCell(cell);
            for (int i = rowminindex; i <= maxrow; i++)
            {
                int rowindex = i;
                ICell celltemp = this[rowindex, columnminindex];
                row = this.Rows[i - 1];
                if (row != null && celltemp != null)
                {
                    celltemp.Row = row;
                    row.Cells.Add(celltemp);
                }
                ClearMergeAndBackCell(celltemp);
                IRow rowtemp = this.Rows[rowindex];
                if (rowtemp != null)
                {
                    rowtemp.Cells.Remove(celltemp);
                }
            }
        }
        public virtual void InsertCellMoveDown()
        {
            if (this.SelectCells == null)
                return;
            int count = this.SelectCells.MaxRow() - this.SelectCells.MinRow() + 1;
            int maxcolumn = this.SelectCells.MaxColumn();
            int mincolumn = this.SelectCells.MinColumn();
            int minrow = this.SelectCells.MinRow();
            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                ICell cell = this[minrow, i];
                InsertCellMoveDown(cell, count);
            }
        }
        public virtual void InsertCellMoveDown(SelectCellCollection cells)
        {
            if (cells == null)
                return;
            int count = this.SelectCells.MaxRow() - this.SelectCells.MinRow() + 1;
            int maxcolumn = this.SelectCells.MaxColumn();
            int mincolumn = this.SelectCells.MinColumn();
            int minrow = this.SelectCells.MinRow();
            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                ICell cell = this[minrow, i];
                InsertCellMoveDown(cell, count);
            }
        }
        public virtual void InsertCellMoveRight()
        {
            if (this.SelectCells == null)
                return;
            int count = this.SelectCells.MaxColumn() - this.SelectCells.MinColumn() + 1;
            int maxrow = this.SelectCells.MaxRow();
            int minrow = this.SelectCells.MinRow();
            int mincolumn = this.SelectCells.MinColumn();
            for (int i = minrow; i <= maxrow; i++)
            {
                ICell cell = this[i, mincolumn];
                InsertCellMoveRight(cell, count);
            }
        }
        public virtual void InsertCellMoveRight(SelectCellCollection cells)
        {
            if (cells == null)
                return;
            int count = this.SelectCells.MaxColumn() - this.SelectCells.MinColumn() + 1;
            int maxrow = this.SelectCells.MaxRow();
            int minrow = this.SelectCells.MinRow();
            int mincolumn = this.SelectCells.MinColumn();
            for (int i = minrow; i <= maxrow; i++)
            {
                ICell cell = this[i, mincolumn];
                InsertCellMoveRight(cell, count);
            }
        }
        public virtual void InsertCellMoveDown(ICell cell, int count)
        {
            int rowminindex = cell.Row.Index;
            int columnminindex = cell.Column.Index;
            int maxrow = ReSetHasValue().X;
            for (int i = maxrow; i >= rowminindex; i--)
            {
                ICell celltemp = this[i, columnminindex];
                ClearMergeAndBackCell(celltemp);
                IRow row = this.Rows[i];
                if (row != null && celltemp != null)
                {
                    row.Cells.Remove(celltemp);
                }
                int rowindex = i + count;
                IRow rowtemp = this.Rows[rowindex];
                if (rowtemp == null)
                {
                    rowtemp = this.ClassFactory.CreateDefaultRow(this, rowindex);
                    this.Rows.Add(rowtemp);
                }
                celltemp.Row = rowtemp;
                rowtemp.Cells.Add(celltemp);
            }
        }

        public virtual void InsertCellMoveLeft(ICell cell)
        {
            int rowminindex = cell.Row.Index;
            int columnminindex = cell.Column.Index;
            int maxcolumn = ReSetHasValue().Y;

            IRow row = this.Rows[rowminindex];
            if (row != null)
            {
                row.Cells.RemoveAt(columnminindex - 1);
            }
            for (int i = columnminindex; i <= maxcolumn; i++)
            {
                ICell celltemp = this[rowminindex, i];
                ClearMergeAndBackCell(celltemp);
                if (row != null && celltemp != null)
                {
                    row.Cells.Remove(celltemp);
                }
                int columnindex = i - 1;
                IColumn columntemp = this.Columns[columnindex];
                if (columntemp == null)
                {
                    columntemp = this.ClassFactory.CreateDefaultColumn(this, columnindex);
                    this.Columns.Add(columntemp);
                }
                celltemp.Column = columntemp;
                row.Cells.Add(celltemp);
            }
        }
        public virtual void InsertCellMoveRight(ICell cell, int count)
        {
            int rowminindex = cell.Row.Index;
            int columnminindex = cell.Column.Index;
            int maxcolumn = ReSetHasValue().Y;
            for (int i = maxcolumn; i >= columnminindex; i--)
            {
                ICell celltemp = this[rowminindex, i];
                ClearMergeAndBackCell(celltemp);
                IRow row = this.Rows[rowminindex];
                if (row != null && celltemp != null)
                {
                    row.Cells.Remove(celltemp);
                }
                int columnindex = i + count;
                IColumn columntemp = this.Columns[columnindex];
                if (columntemp == null)
                {
                    columntemp = this.ClassFactory.CreateDefaultColumn(this, columnindex);
                    this.Columns.Add(columntemp);
                }
                celltemp.Column = columntemp;
                row.Cells.Add(celltemp);
            }
        }

        public virtual void DeleteCellMoveUp()
        {
            DeleteCellMoveUp(this.SelectCells);
        }
        public virtual void DeleteCellMoveUp(ISelectCellCollection cells)
        {
            if (cells == null)
                return;
            int maxcolumn = this.SelectCells.MaxColumn();
            int mincolumn = this.SelectCells.MinColumn();
            int minrow = this.SelectCells.MinRow();
            int maxrow = this.SelectCells.MaxRow();
            for (int rowindex = minrow; rowindex <= maxrow; rowindex++)
            {
                for (int columnindex = mincolumn; columnindex <= maxcolumn; columnindex++)
                {
                    IRow row = this.Rows[rowindex];
                    if (row == null)
                    {
                        continue;
                    }
                    IColumn column = this.Columns[columnindex];
                    if (column == null)
                    {
                        continue;
                    }
                    ICell cell = row[column];
                    row.Cells.Remove(cell);
                }
            }
            int count = maxrow - minrow + 1;
            this.Rows.Sort();
            foreach (IRow row in this.Rows)
            {
                if (row.Index <= maxrow)
                {
                    continue;
                }
                IRow targetrow = this.Rows[row.Index - count];
                if (targetrow == null)
                    continue;
                for (int columnindex = mincolumn; columnindex <= maxcolumn; columnindex++)
                {
                    ICell cell = row[columnindex];
                    if (cell == null)
                        continue;
                    cell.Row = targetrow;
                    targetrow.Cells.Add(cell);
                    row.Cells.Remove(cell);
                }
            }
        }
        public virtual void DeleteCellMoveLeft()
        {
            DeleteCellMoveLeft(this.SelectCells);
        }
        public virtual void DeleteCellMoveLeft(ISelectCellCollection cells)
        {
            if (cells == null)
                return;
            int maxcolumn = this.SelectCells.MaxColumn();
            int mincolumn = this.SelectCells.MinColumn();
            int minrow = this.SelectCells.MinRow();
            int maxrow = this.SelectCells.MaxRow();
            for (int rowindex = minrow; rowindex <= maxrow; rowindex++)
            {
                for (int columnindex = mincolumn; columnindex <= maxcolumn; columnindex++)
                {
                    IRow row = this.Rows[rowindex];
                    if (row == null)
                    {
                        continue;
                    }
                    IColumn column = this.Columns[columnindex];
                    if (column == null)
                    {
                        continue;
                    }
                    ICell cell = row[column];
                    row.Cells.Remove(cell);
                }
            }
            int count = maxcolumn - mincolumn + 1;
            foreach (IColumn column in this.Columns)
            {
                if (column.Index <= maxcolumn)
                {
                    continue;
                }
                IColumn targetcolumn = this.Columns[column.Index - count];
                if (targetcolumn == null)
                    continue;
                for (int rowindex = minrow; rowindex <= maxrow; rowindex++)
                {
                    IRow row = this.Rows[rowindex];
                    ICell cell = row[column];
                    if (cell == null)
                        continue;
                    cell.Column = targetcolumn;
                    row.Cells[targetcolumn] = cell;
                }
            }
        }
        protected void ClearMergeAndBackCell(ICell cell)
        {
            if (cell == null)
                return;
            UnMergeCell(cell.OwnMergeCell);
            UnBackCell(cell.OwnBackCell);
        }
        public void UnMergeCell(IMergeCell mc)
        {
            if (mc != null)
            {
                this.MergeCells.Remove(mc);
                for (int i = mc.MinCell.Row.Index; i <= mc.MaxCell.MaxRowIndex; i++)
                {
                    for (int j = mc.MinCell.Column.Index; j <= mc.MaxCell.MaxColumnIndex; j++)
                    {
                        ICell cell = this[i, j];
                        if (cell != null)
                        {
                            cell.OwnMergeCell = null;
                        }
                    }
                }
            }
        }
        public void UnBackCell(IBackCell mc)
        {
            if (mc != null)
            {
                for (int i = mc.MinCell.Row.Index; i <= mc.MaxCell.MaxRowIndex; i++)
                {
                    for (int j = mc.MinCell.Column.Index; j <= mc.MaxCell.MaxColumnIndex; j++)
                    {
                        ICell cell = this[i, j];
                        if (cell != null)
                        {
                            cell.OwnBackCell = null;
                        }
                    }
                }
            }
        }
        public virtual void CopyAdd(IRow row)
        {

        }
        public void CopyInsert()
        {

        }

        #endregion

        #region 第一显示
        private int _BeginSetFirstDisplayRowIndex = 0;
        public void BeginSetFirstDisplayRowIndex()
        {
            _BeginSetFirstDisplayRowIndex++;
        }
        private int _EndSetFirstDisplayRowIndex = 0;
        public void EndSetFirstDisplayRowIndex()
        {
            _EndSetFirstDisplayRowIndex++;
            if (_EndSetFirstDisplayRowIndex >= _BeginSetFirstDisplayRowIndex)
            {
                this.ReFreshFirstDisplayRowIndex();
            }
        }
        public void ReFreshFirstDisplayRowIndex()
        { 
#if DEBUG
            try
            {
#endif
                ///////////////////////////代码加在中间
                this.BeginReFresh();
                if (_EndSetFirstDisplayRowIndex == _BeginSetFirstDisplayRowIndex)
                {
                    SetFirstRowShowIndex(this._FirstDisplayedRowIndex);
                }
                ///////////////////////////
                this.EndReFresh();
#if DEBUG
            }
            catch (Exception ex)
            {           
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex); 
            }
#endif
        }
        private int ContentTop = 0; 

        public bool OnCalcRowHeight(IRow row)
        {
            if (CalcRowHeight != null)
            {
                return CalcRowHeight(this, row);
            }
            return false;
        }

        public void OnCalcColumnWidth(IColumn column)
        {
            if (CalcColumnWidth != null)
            {
                CalcColumnWidth(this, column);
            }
        }
        private List<IRow> _VisibleHeaderRows = new List<IRow>();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<IRow> VisibleHeaderRows
        {
            get { return _VisibleHeaderRows; }
        }
        private List<IRow> _VisibleFooterRows = new List<IRow>();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<IRow> VisibleFooterRows
        {
            get { return _VisibleFooterRows; }
        }
        private bool _BackGroundMode = false;
        [Browsable (false)]
        public bool BackGroundMode { get { return _BackGroundMode; } set { _BackGroundMode = value; } }

        private int CalcDisplayRowHeader(ref int height)
        {
            IRow row = this.Rows[0];
            if (row == null)
            {
                height = this.DefaultRowHeight+5;
                return height;
            }
            this.OnCalcRowHeight(row);
            this.AllVisibleRows.Add(row);
            this.VisibleHeaderRows.Add(row);
            height = row.Height;
            return height;
            //IRow row = null;
            //int h = 0;
            //for (int k = -99; k <= 0; k++)
            //{
            //    if (this.Rows.Contains(k))
            //    {
            //        row = this.Rows[k];
            //        if (row.Visible)
            //        {
            //            this.OnCalcRowHeight(row);
            //            this.AllVisibleRows.Add(row);
            //            this.VisibleHeaderRows.Add(row);
            //            row.Top = height;

            //            height += row.Height;
            //            h = h + row.Height;
            //            if (height > this.Height)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}
            //return h;
        }

         
        private void SetFirstDisplayFrozenFooterIndex(ref int height, ref int i)
        {
            IRow row = null;
            if (this.FrozenRow > 0)
            {
                int colindex = 0;
                while (height < this.Height)
                {
                    colindex = colindex + 1;
                    if (this.FrozenRow < colindex)
                    {
                        break;
                    }
                    row = this.Rows[colindex];
                    if (row != null)
                    {
                        if (row.Visible)
                        {
                            this.OnCalcRowHeight(row);
                            this.AllVisibleRows.Add(row);
                            this.VisibleRows.Add(row);
                            row.Top = height;
                            height = height + row.Height;
                        }
                    }
                    i++;

                }
            }
        }
        private void SetFirstDisplayRow(int index)
        {
            try
            {
                this.BeginReFresh(); 
                if (index > this.MaxRow)
                {
                    index = this.MaxRow;
                }
                if (index < 1)
                {
                    index = 1;
                } 
                int top = this.TopSideHeight;
                this.AllVisibleRows.Clear();
                this.VisibleRows.Clear();
                this.VisibleHeaderRows.Clear();
                this.VisibleFooterRows.Clear();
                IRow row = null;
#if DEBUG2
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "SetFirstDisplayRow", this.ToString());
#endif
                if (this.ShowColumnHeader)
                {
                    _toprowheight = CalcDisplayRowHeader(ref top);
                }
                ContentTop = top;

                int height = 0; 
                List<IRow> listtemp = new List<IRow>(); 
                int i = 0;
                SetFirstDisplayFrozenFooterIndex(ref top, ref i);

                int rowindex = i + _FirstDisplayedRowIndex;
                int h = top;
                int rowsheight = this.Height;
                if (this.ShowEndRow)
                {
                    rowsheight = this.Height - this.DefaultRowHeight - this.RowHeaderHeight - this.HScroll.Height;
                }
                while (top <= (rowsheight - height))
                {
                    rowindex = i + _FirstDisplayedRowIndex;
                    if (this.MaxRow > 0)
                    {
                        if (rowindex > this.MaxRow)
                        {
                            break;
                        }
                    }
                    else if (this.DataSource != null)
                    {
                        break;
                    }
                    row = this.Rows[rowindex];
                    //if (rowindex > 96)
                    //{

                    //}
                    if (row == null)
                    {
                        if (this.AutoGenerateRows)
                        {
                            row = this.ClassFactory.CreateDefaultRow(this, rowindex);
                            this.Rows.Add(row);
                        }
                    }
                    if (row != null)
                    {
                        if (row.Visible)
                        {
                            if (!this.OnCalcRowHeight(row))
                            {
                                this.AllVisibleRows.Add(row);
                                this.VisibleRows.Add(row);
                                row.Top = top;
                                int rindex = i + index;
                                _EndDisplayedRowIndex = rowindex;
                                top += row.Height;
                            }
                            //Feng.Utils.TraceHelper.WriteTrace("row.Height", rowindex.ToString ()+"", row.Index+"", true, row.Height.ToString());
                        }
                    }
                    i++;
                }
                AddEndRow(top);
                this.AllVisibleRows.AddRange(listtemp.ToArray());
         
            }
            finally
            {
                this.AutoShowScroll();
                this.EndReFresh();
            }

        }
        public void AddEndRow(int top)
        {
            this.EndRow = null;
            int rowindex = this.Rows.MaxHasValueIndex;
            if (this.EndDisplayedRowIndex >= rowindex)
            {
                return;
            }
            IRow row = this.Rows[rowindex]; 
            if (row == null)
            {
                if (this.AutoGenerateRows)
                {
                    row = this.ClassFactory.CreateDefaultRow(this, rowindex);
                    this.Rows.Add(row);
                }
            }
            if (row != null)
            {
                if (row.Visible)
                {
                    this.AllVisibleRows.Add(row);
                    this.VisibleRows.Add(row);
                    row.Top = top;
                    this.EndRow = row;
                }
            }
        }
        public void RefreshRowHeaderWidth()
        {
            try
            {
                IColumn col = this.Columns[0];
                if (col != null)
                {
                    Graphics g = this.GetGraphics();
                    if (g == null)
                        return;
                    Size sf = Feng.Drawing.GraphicsHelper.Sizeof(string.Format("{0}", 1 + this.EndDisplayedRowIndex) + "  ", this.Font, g);
                    //Size sf = Feng.Utils.ConvertHelper.ToSize(g.MeasureString(, this.Font,1000, ));
                    if (sf.Width > col.Width)
                    {
                        col.Width = sf.Width + 10;
                    }
                    else
                    {
                        if (col.Width > 40 && sf.Width < 40)
                        {
                            col.Width = 40;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "RefreshRowHeaderWidth", ex);
            }
            
        }
#warning 需要优化
        public void SizeRowHeader()
        {

            //var col = this.Columns[0];
            //if (col != null)
            //{
            //    var g = this.GetGraphics();
            //    if (g == null)
            //        return;
            //    Size sf = Feng.Utils.ConvertHelper.ToSize(g.MeasureString(string.Format("{0}", i + _FirstDisplayedRowIndex), this.Font));
            //    if (sf.Width > col.Width)
            //    {
            //        col.Width = sf.Width;
            //    }
            //    else
            //    {
            //        if (col.Width > 40 && sf.Width < 40)
            //        {
            //            col.Width = 40;
            //        }
            //    }
            //}
        }
        public void SetVScrollerValue(int value)
        {
            //if (value > this.VScroller.Maximum)
            //{
            //    value = this.VScroller.Maximum;
            //}
            //if (value < this.VScroller.Minimum)
            //{
            //    value = this.VScroller.Minimum;
            //}
            //this.VScroller.Value = value;
        }
        bool lockSetFirstRowShowIndex = false;
        private void SetFirstRowShowIndex(int index)
        {
            if (lockSetFirstRowShowIndex)
                return;
            try
            {
                //if (!this.AllowChangedFirstDisplayRow)
                //{
                //    return;
                //}
                lockSetFirstRowShowIndex = true;
                _EndSetFirstDisplayRowIndex = _BeginSetFirstDisplayRowIndex = 0;
                BeforeFirstDisplayRowChangedArgs e = new BeforeFirstDisplayRowChangedArgs(index);
                this.OnBeforeFirstDisplayRowChanged(e);
                if (e.Cancel)
                {
                    return;
                }
                this.BeginReFresh();
                SetFirstDisplayRow(index);
                SetVScrollerByFirstColumnIndex();
                this.OnFirstDisplayRowChanged(index);
                RefreshExtendCells();
            }
            finally
            {
                this.EndReFresh();
                lockSetFirstRowShowIndex = false;
            }

        }


        private int _BeginSetFirstDisplayColumnIndex = 0;
        public void BeginSetFirstDisplayColumnIndex()
        {
            _BeginSetFirstDisplayColumnIndex++;
        }
        private int _EndSetFirstDisplayColumnIndex = 0;
        public void EndSetFirstDisplayColumnIndex()
        {
            _EndSetFirstDisplayColumnIndex++;
            this.ReFreshFirstDisplayColumnIndex();
        }
        public void ReFreshFirstDisplayColumnIndex()
        {
            if (this._BeginSetFirstDisplayColumnIndex == this._EndSetFirstDisplayColumnIndex)
            {
                SetFirstColumnShowIndex(this._FirstDisplayedColumnIndex);
            }
        }

        private int ContentLeft = 0;
        public void CreateRowHeader()
        {
            IRow row = this.ClassFactory.CreateDefaultRow(this, 0);
            if (row != null)
            {
                this.Rows.Add(row);
            }
        }

        public void CreateColumnHeader()
        {
            IColumn column = this.ClassFactory.CreateDefaultColumn(this, 0);
            if (column != null)
            {
                this.Columns.Add(column);
            }
        }
        private void SetRowHeader(ref int width)
        {
            IColumn column = null;
            if (this.ShowRowHeader)
            {
                column = this.Columns[0];
                if (column != null)
                {
                    if (column.Visible)
                    {
                        this.OnCalcColumnWidth(column);
                        this.AllVisibleColumns.Add(column);
                        column.Left = width;
                        width += column.Width;
                    }
                }
            }
        }

        private void SetFrozenColumn(ref int width)
        {
            IColumn column = null;
            if (this.FrozenColumn > 0)
            { 
                for (int ci = 1; ci <= this.FrozenColumn; ci++)
                { 
                    column = this.Columns[ci];
                    if (column != null)
                    {
                        if (column.Visible)
                        {
                            this.OnCalcColumnWidth(column);
                            this.AllVisibleColumns.Add(column); 
                            this.VisibleColumns.Add(column);
                            column.Left = width;
                            width = width + column.Width;
                        }
                    } 
                }
            }
        }

        private void SetColumnWidthByFirstIndex(ref int width)
        {
            IColumn column = null; 
            for (int ci = _FirstDisplayedColumnIndex; ci <= this.MaxColumn; ci++)
            {
                if (ci <= this.FrozenColumn)
                {
                    continue;
                }
                if (width < this.Width)
                { 
                    column = this.Columns[ci];
                    if (column == null)
                    {
                        if (this.AutoGenerateColumns)
                        {
                            column = this.ClassFactory.CreateDefaultColumn(this, ci);
                            this.Columns.Add(column);
                        }
                    }
                    if (column != null)
                    {
                        if (column.Visible)
                        {
                            this.OnCalcColumnWidth(column);
                            this.AllVisibleColumns.Add(column);
                            this.VisibleColumns.Add(column);
                            column.Left = width; 
                            width = width + column.Width;
                            _EndDisplayedColumnIndex = ci;
                        }
                    } 
                }
                else
                {
                    break;
                }
            }
        }

        private void SetFirstDisplayColumn(int index)
        {

            if (index < 1)
            {
                index = 1;
            }

            if (index > this.MaxColumn)
            {
                index = this.MaxColumn;
            }

            this._FirstDisplayedColumnIndex = index;
            int i = 0;
            int width = this.LeftSideWidth;
            this.AllVisibleColumns.Clear();
            this.VisibleColumns.Clear();
            SetRowHeader(ref width);
            ContentLeft = width;
            SetFrozenColumn(ref width);
            SetColumnWidthByFirstIndex(ref width);
            SetHScrollerByFirstColumnIndex();
            this.AutoShowScroll();
        }

        private void SetVScrollerByFirstColumnIndex()
        {
            if (this.VScroll != null)
            {
                this.VScroll.Max = this.Rows.Max;

            }
        }
        private void SetHScrollerByFirstColumnIndex()
        {
            if (this.HScroll != null)
            { 
                this.HScroll.Max = this.Columns.Max; 
                SetHScrollerValue(this.FirstDisplayedColumnIndex);
            }
        }
        public void SetHScrollerValue(int value)
        { 
        }
        bool lockSetFirstColumnShowIndex = false;
        private void SetFirstColumnShowIndex(int index)
        {
            if (lockSetFirstColumnShowIndex)
                return;
            try
            { 
                lockSetFirstColumnShowIndex = true;
                this._BeginSetFirstDisplayColumnIndex = this._EndSetFirstDisplayColumnIndex = 0;
                BeforeFirstDisplayColumnChangedArgs e = new BeforeFirstDisplayColumnChangedArgs(index);
                this.OnBeforeFirstDisplayColumnChanged(e);
                if (e.Cancel)
                {
                    return;
                }
                this.BeginReFresh();

                SetFirstDisplayColumn(index);
                this.OnFirstDisplayColumnChanged(index);
                RefreshExtendCells(); 
                this.EndReFresh();
            }
            finally
            {
                lockSetFirstColumnShowIndex = false;
            }

        }

        public virtual void BeginRefreshExtendCells()
        {

        }

        public virtual void EndRefreshExtendCells()
        {

        }

        public virtual void RefreshExtendCells()
        {
            if (this.BackCells != null)
            {
                this.BackCells.Refresh();
            }
            if (this.MergeCells != null)
            {
                this.MergeCells.Refresh();
            }
            if (this.ListExtendCells != null)
            {
                this.ListExtendCells.Refresh();
            }
            if (this.CopyCells != null)
            {
                this.CopyCells.Refresh();
            }

            if (this.SelectCells != null)
            {
                this.SelectCells.Refresh();
            }
        }

        public void ReFreshRowHeaderHeight(bool containsAutoHeight)
        {
            this.BeginSetFirstDisplayRowIndex();
            foreach (IRow row in this.AllVisibleRows)
            {
                if (row.Index < 0)
                {
                    continue;
                }
                if (!containsAutoHeight)
                {
                    if (!row.AutoHeight)
                    {
                        continue;
                    }
                }
                int height = this.DefaultRowHeight;
                foreach (IColumn column in this.AllVisibleColumns)
                {
                    if (column.Index < 1)
                    {
                        continue;
                    }
                    ICell cell = row[column];
                    if (cell != null)
                    {
                        if (cell.ContensHeigth > height)
                        {
                            height = cell.ContensHeigth;
                        }
                    }
                }
                if (height > 0)
                {
                    row.Height = height;
                }
            }
            this.EndSetFirstDisplayRowIndex();
        }

        /// <summary>
        /// 重新设置列的宽度
        /// </summary>
        public void ReFreshColumnHeaderWidth(bool containsAutoWidth)
        {
            this.BeginSetFirstDisplayColumnIndex();
            foreach (IColumn column in this.AllVisibleColumns)
            {
                if (!column.AllowChangedSize)
                    continue;
                if (column.Index < 1)
                {
                    continue;
                }

                if (!column.AutoWidth)
                {
                    continue;
                }

                ICell cell2 = this[0, column.Index];
                int ContensWidth = 0;
                if (cell2 != null)
                {
                    ContensWidth = cell2.ContensWidth;
                }
                int width = this.DefaultColumnWidth > ContensWidth
                    ? this.DefaultColumnWidth
                    : ContensWidth;
                foreach (IRow row in this.AllVisibleRows)
                {
                    if (row.Index < 1)
                    {
                        continue;
                    }
                    ICell cell = row[column];
                    if (cell != null)
                    {
                        if (cell.ContensWidth > width)
                        {
                            width = cell.ContensWidth;
                        }
                    }
                }
                if (width > 0)
                {
                    if (width < 800)
                    {
                        column.Width = width;
                    }
                }
            }
            this.EndSetFirstDisplayColumnIndex();
        }

        public void SetFocuseddCell(ICell cell)
        {


            this.BeginReFresh(); 
            this.CloseEdit();
            ICell cel = cell;
            if (cell != null)
            {
                if (cell.OwnMergeCell != null)
                {
                    cel = cell.OwnMergeCell;
                }
            } 
            this._focusedcell = cel;

            this._ICellEvents = this._focusedcell; 
            if (FocusedCellChanged != null)
            {
                FocusedCellChanged(this, this._focusedcell);
            }
            this.EndReFresh();
        }

        public ICell GetCellByPoint(int x, int y)
        {
            Point point = new Point(x, y);
            return GetCellByPoint(point);
        }
        public ICell GetCellByPoint(Point point)
        {
            foreach (IRow r in this.AllVisibleRows)
            {
                if (r.Rect.Contains(point))
                {
                    foreach (IColumn c in this.AllVisibleColumns)
                    {
                        if (c.Rect.Contains(point))
                        {
                            ICell cell = null;
                            if (r != null)
                            {
                                cell = r.Cells[c.Index];
                            }
                            if (cell == null)
                            {
                                cell = this.ClassFactory.CreateDefaultCell(this, r.Index, c.Index);
                            }
                            if (cell.OwnMergeCell != null)
                            {
                                return cell.OwnMergeCell;
                            }
                            return cell;
                        }
                    }
                    break;
                }
            }
            return null;
        }

        public int GetEndDisplayRowIndex(ICell cell)
        {
            int heigt = 0;
            for (int i = cell.Row.Index; i >= ConstantValue.MinRowIndex; i--)
            {
                heigt = heigt + this.Rows[i].Height;
                if (heigt >= this.Height)
                {
                    return i;
                }
            }
            return ConstantValue.MinRowIndex;
        }

        private bool SelectModeNullMouseMove(MouseEventArgs e, EventViewArgs ve)
        {
            Point viewloaction = this.PointControlToView(e.Location);
            #region TopLeft
            if (this.AllowChangedSize)
            {
                if (this.TopLeft.Contains(viewloaction)
                       || this.MidTop.Contains(viewloaction)
                       || this.MidLeft.Contains(viewloaction)
                       || this.TopRight.Contains(viewloaction)
                       || this.MidRight.Contains(viewloaction)
                       || this.BottomLeft.Contains(viewloaction)
                       || this.MidBottom.Contains(viewloaction)
                       || this.BottomRight.Contains(viewloaction)
                   )
                {
                    this.SelectChangedBorder = true;
                }
                else
                {
                    this.SelectChangedBorder = false;
                }
            }
            #endregion

            MouseEventArgs ee = e;
#warning 使用 当前事件处理
            if (this.ScrollerMouseMove(ee))
            {
                return true;
            }

            Rectangle rectf = new Rectangle(0, this.TopSideHeight, this.LeftSideWidth, this.Height);

            #region MergeCellCollectionRect
            if (this.ShowSelectAddRect)
            {
                if (this._MergeCellCollectionRect != null)
                {
                    if (this._MergeCellCollectionRect.AddRectContains(viewloaction))
                    {
                        this.BeginSetCursor(DataExcel.DefaultCross);
                        return true;
                    }
                }
            }
            #endregion
            #region SelectCells
            if (this.ShowSelectAddRect)
            {
                if (this._SelectCells != null)
                {
                    if (this._SelectCells.AddRectContains(viewloaction))
                    {
                        this.BeginSetCursor(DataExcel.DefaultCross);
                        return true;
                    }
                }
            }
            #endregion


            #region CellMouseMove
            Rectangle rect = Rectangle.Empty;
            Point viewlocation = this.PointControlToView(e.Location);
            foreach (IRow r in this.AllVisibleRows)
            {
                if (r.Rect.Contains(viewlocation))
                {
                    foreach (IColumn c in this.AllVisibleColumns)
                    {
                        if (c.Rect.Contains(viewlocation))
                        {
                            ICell cell = null;
                            if (r != null)
                            {
                                cell = r.Cells[c.Index];
                            }
                            if (cell == null)
                            {
                                cell = this.ClassFactory.CreateDefaultCell(this, r.Index, c.Index);
                            }
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            this.MouseOverCell = cell;
                            //this.CellMouseState = CellMouseState.Over;
                            if (this.MouseCaptureView != cell)
                            { 
                                this.MouseCaptureView = cell;
                                cell.OnMouseEnter(this, e, ve);
                            }


                            if (cell.OnMouseMove(this, e, ve))
                            {
                                return true;
                            }
                            this.BeginSetCursor(this.DefaultCursor);
                            OnCellMouseMove(cell, e);
                            return false;

                        }
                    }
                    break;
                }
            }
            #endregion
            return false;
        }

        public Point SetFirstShowIndex(Point point)
        { 

            Point location = point;
            int movestep = 1;
            if (point.X <= this.ContentLeft)
            {
                location.X = this.ContentLeft + 1;
            }
            if (point.X >= (this.Width))
            {
                location.X = this.Width - 1;
            }
            if (point.Y <= this.ContentTop)
            {
                location.Y = this.ContentTop + 1;
            }
            if (point.Y >= (this.Height))
            {
                location.Y = this.Height - 1;
            }
            int firstcolumnindex = 0;
            if (point.X <= this.ContentLeft)
            {
                firstcolumnindex = (this.FirstDisplayedColumnIndex - movestep);
                if (firstcolumnindex <= 0)
                {
                    firstcolumnindex = 0;
                }
                if (this.AllowChangedFirstDisplayColumn)
                {
                    SetFirstColumnShowIndex(firstcolumnindex);
                }
                return location;
            }
            else if (point.X >= (this.Width))
            {
                firstcolumnindex = (this.FirstDisplayedColumnIndex + movestep);
                if (firstcolumnindex <= 0)
                {
                    firstcolumnindex = 0;
                }
                if (this.AllowChangedFirstDisplayColumn)
                {
                    SetFirstColumnShowIndex(firstcolumnindex);
                }
                return location;
            }
            if (point.Y <= this.ContentTop)
            {
                firstcolumnindex = (this.FirstDisplayedRowIndex - movestep);
                if (firstcolumnindex <= 0)
                {
                    firstcolumnindex = 0;
                }
                if (this.AllowChangedFirstDisplayRow)
                {
                    SetFirstRowShowIndex(firstcolumnindex);
                }
                return location;
            }
            else if (point.Y >= (this.Height-30))
            {
                firstcolumnindex = (this.FirstDisplayedRowIndex + movestep);
                if (firstcolumnindex <= 0)
                {
                    firstcolumnindex = 0;
                }
                if (this.AllowChangedFirstDisplayRow)
                {
                    SetFirstRowShowIndex(firstcolumnindex);
                }
                return location;
            }
            return location;
        }

        public static int GetColumnIndexByColumnHeader(string columnheader)
        {
            columnheader = columnheader.ToUpper();
            if (string.IsNullOrEmpty(columnheader)) return 0;
            int index = 0;
            for (int i = columnheader.Length - 1, j = 1; i >= 0; i--, j *= 26)
            {
                char c = columnheader[i];
                if (c < 'A' || c > 'Z')
                {
                    return -1;
                }
                index += ((int)c - 64) * j;
            }
            return index;
        }
        public static string GetColumnHeaderByColumnIndex(int index)
        {
            string columnheader = string.Empty;
            while (index > 0)
            {
                int m = index % 26;
                if (m == 0)
                {
                    m = 26;
                }
                columnheader = (char)(m + 64) + columnheader;
                index = (index - m) / 26;
            }
            return columnheader;
        }
        public static string GetCellName(int row, int column)
        {
            string name = GetColumnHeaderByColumnIndex(column);
            name = name + row.ToString();
            return name;
        }
        #endregion

        #region 扩展表格
        public virtual ISelectCellCollection SelectCell(string begin, string end)
        {
            ICell begincell = null;
            ICell endCell = null;

            begincell = this.GetCell(begin);
            if (begincell == null)
                return null;

            endCell = this.GetCell(end);
            if (endCell == null)
                return null;
            return SelectCell(begincell, endCell);
        }
        public virtual ISelectCellCollection SelectCell(ICell begincell)
        {
            SelectCellCollection selectcell = new SelectCellCollection();
            selectcell.BeginCell = begincell;
            selectcell.EndCell = begincell;
            this.SelectCells = selectcell;
            return selectcell;
        }
        public virtual ISelectCellCollection SelectCell(ICell begincell, ICell endCell)
        {
            SelectCellCollection selectcell = new SelectCellCollection();
            selectcell.BeginCell = begincell;
            selectcell.EndCell = endCell;
            this.SelectCells = selectcell;
            return selectcell;
        }
        public virtual ISelectCellCollection SelectCell(int minrow, int mincol, int maxrow, int maxcol)
        {
            ICell begincell = this[minrow, mincol];
            ICell endCell = this[maxrow, maxcol];
            return SelectCell(begincell, endCell); ;
        }

        public virtual ISelectCellCollection SelectCell(string cellreange)
        {
            if (!cellreange.Contains(":"))
            {
                return null;
            }
            string[] cells = cellreange.Split(':');
            if (cells.Length != 2)
            {
                return null;
            }
            ICell begincell = null;
            ICell endCell = null;

            begincell = this.GetCell(cells[0]);
            if (begincell == null)
                return null;

            endCell = this.GetCell(cells[1]);
            if (endCell == null)
            {
                endCell = begincell;
            }
            SelectCellCollection selectcell = new SelectCellCollection();
            selectcell.BeginCell = begincell;
            selectcell.EndCell = endCell;
            return selectcell;
        }
        public virtual IMergeCell MergeCell(int x1, int y1, int x2, int y2)
        {
            ICell begincell = null;
            ICell endCell = null;

            begincell = this[x1, y1];
            if (begincell == null)
                return null;

            endCell = this[x2, y2];
            if (endCell == null)
                return null;
            return SetMergeCell(begincell, endCell);
        }

        public virtual IMergeCell MergeCell(string begin, string end)
        {
            ICell begincell = null;
            ICell endCell = null;

            begincell = this.GetCell(begin);
            if (begincell == null)
                return null;

            endCell = this.GetCell(end);
            if (endCell == null)
                return null;
            return SetMergeCell(begincell, endCell);
        }

        public virtual IMergeCell MergeCell(ICell begincell, ICell endCell)
        {
            return SetMergeCell(begincell, endCell);
        }
        public virtual IMergeCell MergeCell(ISelectCellCollection cells)
        {
            return SetMergeCell(cells.BeginCell, cells.EndCell);
        }
        public virtual IMergeCell MergeCell(string cellreange)
        {
            if (!cellreange.Contains(":"))
            {
                return null;
            }
            string[] cells = cellreange.Split(':');
            if (cells.Length != 2)
            {
                return null;
            }
            ICell begincell = null;
            ICell endCell = null;

            begincell = this.GetCell(cells[0]);
            if (begincell == null)
                return null;

            endCell = this.GetCell(cells[1]);
            if (endCell == null)
                return null;
            return SetMergeCell(begincell, endCell);
        }
        public virtual IMergeCell MergeCell()
        {
            return SetMergeCell();
        }
        public virtual IMergeCell SetMergeCell()
        {
            if (this.SelectCells != null)
            {
                return this.SetMergeCell(this.SelectCells);
            }
            return null;
        }
        public virtual IMergeCell SetMergeCell(ICell begincell, ICell endCell)
        {
            this.BeginReFresh();

            int minrow = Math.Min(begincell.Row.Index, endCell.Row.Index);
            int mincolumn = Math.Min(begincell.Column.Index, endCell.Column.Index);

            int maxrow = Math.Max(begincell.Row.Index, endCell.MaxRowIndex);
            int maxcolumn = Math.Max(begincell.Column.Index, endCell.MaxColumnIndex);
            bool hasmergecell = false;
            for (int i = minrow; i <= maxrow; i++)
            {
                for (int j = mincolumn; j <= maxcolumn; j++)
                {
                    ICell cell = this[i, j];
                    if (cell.OwnMergeCell != null)
                    {
                        hasmergecell = true;
                        break;
                    }
                }
            }
            IMergeCell table = null;
            if (!hasmergecell)
            {
                table = new MergeCell(this);

                table.BeginCell = this[minrow, mincolumn];
                table.EndCell = this[maxrow, maxcolumn];
                if (this.MergeCells == null)
                {
                    this.MergeCells = new MergeCellCollection(this);
                }
                this.MergeCells.Add(table);
            }

            this.EndReFresh();
            return table;
        }
        public virtual IMergeCell SetMergeCell(int beginrowindex, int begincolumnindex, int endrowindex, int endcolumnindex)
        {
            this.BeginReFresh();

            IMergeCell table = new MergeCell(this);

            table.BeginCell = this[beginrowindex, begincolumnindex];
            table.EndCell = this[endrowindex, endcolumnindex];

            if (this.MergeCells == null)
            {
                this.MergeCells = new MergeCellCollection(this);
            }
            this.MergeCells.Add(table);

            this.EndReFresh();
            return table;
        }
        public virtual IMergeCell SetMergeCell(ISelectCellCollection selectcells)
        {
            if (selectcells == null)
                return null;
 
            return SetMergeCell(selectcells.MinCell, selectcells.MaxCell);
        }
        public virtual void EndMergeCell(ISelectCellCollection selectcells)
        {
            if (selectcells == null)
                return;
            this.BeginReFresh();
            List<ICell> list = selectcells.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.OwnMergeCell != null)
                {
                    IMergeCell mcell = cell.OwnMergeCell;
                    if (mcell != null)
                    {
                        this.MergeCells.Remove(mcell);
                    }
                }
            }

            this.EndReFresh();
        }
        public virtual void EndMergeCell(ICell cell)
        {
            if (cell == null)
                return;
            this.BeginReFresh();

            IMergeCell mcell = cell as IMergeCell;
            if (mcell != null)
            {
                this.MergeCells.Remove(mcell);
            }

            this.EndReFresh();
        }
        public virtual void EndMergeCell()
        {
            if (this.FocusedCell == null)
                return;
            this.BeginReFresh();

            EndMergeCell(this.SelectCells);

            this.EndReFresh();
        }

        public IBackCell AddBackCells(ISelectCellCollection selectcells)
        {
            if (selectcells == null)
                return null;
            this.BeginReFresh();
            IBackCell table = this.ClassFactory.CreateDefaultBackCell(this); ;
            table.BeginCell = selectcells.MinCell;
            table.EndCell = selectcells.MaxCell;

            if (this._backcells == null)
            {
                this._backcells = this.ClassFactory.CreateDefaultBackCells(this);
            }
            this._backcells.Add(table);
            this.EndReFresh();
            return table;
        }

        public IBackCell SetBackCells()
        {
            if (this._SelectCells == null)
                return null;
            this.BeginReFresh();

            IBackCell table = this.ClassFactory.CreateDefaultBackCell(this); ;
            table.BeginCell = this._SelectCells.MinCell;
            table.EndCell = this._SelectCells.MaxCell;

            if (this._backcells == null)
            {
                this._backcells = this.ClassFactory.CreateDefaultBackCells(this);
            }
            this._backcells.Add(table);

            this.EndReFresh();
            return table;
        }

        public IBackCell SetBackCells(ISelectCellCollection selectcells)
        {
            if (selectcells == null)
                return null;
            this.BeginReFresh();
            IBackCell table = this.ClassFactory.CreateDefaultBackCell(this); ;
            table.BeginCell = selectcells.MinCell;
            table.EndCell = selectcells.MaxCell;

            if (this._backcells == null)
            {
                this._backcells = this.ClassFactory.CreateDefaultBackCells(this);
            }
            this._backcells.Add(table);
            this.EndReFresh();
            return table;
        }
        public ImageCell AddImageCell()
        {
            Point pt = new Point((this.Left + 72), this.Top + 40);
            if (this.FocusedCell != null)
            {
                pt = this.FocusedCell.Location;
            }
            return AddImageCell(pt);
        }
        public ImageCell AddImageCell(Point pt)
        {
            ImageCell table = new ImageCell(this);
            table.ReSetRowColumn(pt);

            table.FreshLocation();

            ListExtendCells.Add(table);
            return table;
        }

        public void AddTextCell()
        {
            Point pt = new Point((this.Left + 72), this.Top + 40);
            if (this.FocusedCell != null)
            {
                pt = this.FocusedCell.Location;
            }
            AddTextCell(pt);
        }
        public void AddTextCell(Point pt)
        {
            ExtendCell table = new ExtendCell(this);
            table.ReSetRowColumn(pt);

            table.FreshLocation();

            if (ListExtendCells == null)
            {
                ListExtendCells = new ExtendCellCollection(this);
            }
            ListExtendCells.Add(table);
        }

        public IDataExcelChart AddChartCell()
        {
            Point pt = new Point((this.Left + 72), this.Top + 40);
            if (this.FocusedCell != null)
            {
                pt = this.FocusedCell.Location;
            }
            return AddChartCell(pt);
        }
        public IDataExcelChart AddChartCell(Point pt)
        {
            IDataExcelChart table = new DataExcelChart(this);
            table.ReSetRowColumn(pt);
            table.FreshLocation();
            ListExtendCells.Add(table);
            return table;
        }
        #endregion

        #region 合并单元格
        private void SetMergeCellToLeft(MergeCellCollectionAddRect mca)
        {
            IMergeCell mcell = mca.MergeCellCollectionRect.FirstMergeCell;
            ICell endcell = mca.EndCell;
            ICell fcell = mcell.BeginCell;
            ICell ecell = mcell.EndCell;
            int d = fcell.Column.Index - ecell.Column.Index;
            for (int i = ecell.Column.Index + 1; i <= endcell.Column.Index; i++)
            {
                IMergeCell mel = new MergeCell(this[fcell.Row.Index, i], this[ecell.Row.Index, i + d]);
                this.MergeCells.Add(mel);
                i = i + d;
            }
        }
        private void SetMergeCellToRight(MergeCellCollectionAddRect mca)
        {
            IMergeCell mcell = mca.MergeCellCollectionRect.FirstMergeCell;
            ICell endcell = mca.EndCell;
            ICell fcell = mcell.BeginCell;
            ICell ecell = mcell.EndCell;
            int d = ecell.Column.Index - fcell.Column.Index;
            for (int i = ecell.Column.Index + 1; i <= endcell.Column.Index; i++)
            {
                IMergeCell mel = new MergeCell(this[fcell.Row.Index, i], this[ecell.Row.Index, i + d]);
                this.MergeCells.Add(mel);
                i = i + d;
            }
        }

        private void SetMergeCellToTop(MergeCellCollectionAddRect mca)
        {
            IMergeCell mcell = mca.MergeCellCollectionRect.FirstMergeCell;
            ICell endcell = mca.EndCell;
            ICell fcell = mcell.BeginCell;
            ICell ecell = mcell.EndCell;
            int d = ecell.Row.Index - fcell.Row.Index;
            for (int i = endcell.Row.Index + 1; i <= ecell.Row.Index; i++)
            {
                IMergeCell mel = new MergeCell(this[i, fcell.Column.Index], this[i + d, ecell.Column.Index]);
                this.MergeCells.Add(mel);
                i = i + d;
            }
        }
        private void SetMergeCellToDown(MergeCellCollectionAddRect mca)
        {
            IMergeCell mcell = mca.MergeCellCollectionRect.FirstMergeCell;
            ICell endcell = mca.EndCell;
            ICell fcell = mcell.BeginCell;
            ICell ecell = mcell.EndCell;
            int d = ecell.Row.Index - fcell.Row.Index;
            for (int i = ecell.Row.Index + 1; i <= endcell.Row.Index; i++)
            {
                IMergeCell mel = new MergeCell(this[i, fcell.Column.Index], this[i + d, ecell.Column.Index]);
                this.MergeCells.Add(mel);
                i = i + d;
            }
        }
        public void ReSetMergeCellSize()
        {
            for (int i = 1; i <= this.Columns.Max; i++)
            {
                IColumn col = this.Columns[i];
                if (col == null)
                    continue;
                for (int j = 1; j <= this.Rows.Max; j++)
                {
                    IRow row = this.Rows[j];
                    if (row == null)
                        continue;
                    ICell cell = row[col];
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            cell.OwnMergeCell.EndCell = cell.OwnMergeCell.EndCell;
                        }
                        if (cell.OwnBackCell != null)
                        {
                            cell.OwnBackCell.EndCell = cell.OwnBackCell.EndCell;
                        }
                    }
                }
            }
        }

        #endregion

    }
}
