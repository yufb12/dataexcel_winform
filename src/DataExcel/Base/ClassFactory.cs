using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Feng.Excel.Interfaces;
using Feng.Excel.Styles;
using Feng.Excel.Base;
using Feng.Excel.Collections;
using Feng.Excel.View;
using Feng.Script.CBEexpress;
using Feng.Script.Method;

namespace Feng.Excel.Base
{
    [Serializable]
    public class DefultClassFactory : IClassFactory
    {
        public DefultClassFactory(DataExcel grid)
        {
            _grid = grid;
        }

        private DataExcel _grid = null;
        public DataExcel Grid
        {
            get
            {
                return _grid;
            }
        }

        public virtual ICell CreateDefaultCell(IRow row, IColumn column)
        {
            Cell cell = new Cell(row, column);
            return cell;
        }

        public virtual ICell CreateDefaultCell(DataExcel grid, int rowindex, int columnindex)
        {
            IColumn column = grid.Columns[columnindex];
            if (column == null)
            {
                column = this.Grid.ClassFactory.CreateDefaultColumn(this.Grid, columnindex);
                this.Grid.Columns.Add(column);
            }
            if (column != null)
            {
                Cell cell = new Cell(grid, rowindex, columnindex);
                return cell;
            }
            return null;
        }

        public virtual ICell CreateDefaultCell(DataExcel grid)
        {
            Cell cell = new Cell(grid);
            return cell;
        }

        //public virtual ICursorManage CreateDefaultCursorManage()
        //{ 
        //    return new CursorManage(); 
        //}

        public virtual IRow CreateDefaultRow(DataExcel grid, int index)
        {
            return new Row(grid, index);
        }
        public virtual IColumn CreateDefaultColumn(DataExcel grid, int index)
        {
            return new Column(grid, index);
        }

        public virtual IColumn CreateDefaultColumn(DataExcel grid)
        {
            return new Column(grid);
        }
        public virtual IFunctionCell CreateDefaultFunctionCellection(DataExcel grid)
        {
            return new FunctionCell(grid);
        }

        public virtual IMethodCollection CreateDefaultRunMethodHelperCollection(DataExcel grid)
        {
            return new MethodCollection();
        }

        public virtual IRowCollection CreateDefaultRows(DataExcel grid)
        {
            return new RowCollection(grid);
        }

        public virtual IColumnCollection CreateDefaultColumns(DataExcel grid)
        {
            return new ColumnCollection(grid);
        }

        public virtual IFunctionCell CreateDefaultFunctionCells(DataExcel grid)
        {
            return new FunctionCell(grid);
        }

        public virtual IMethodCollection CreateDefaultMethods(DataExcel grid)
        {
            return new MethodCollection();
        }

        //public virtual IDataExcelScroller CreateDefaultVScroller(DataExcel grid)
        //{
        //    return new VScroller(grid);
        //} 

        //public virtual IDataExcelScroller CreateDefaultHScroller(DataExcel grid)
        //{
        //    return new HScroller(grid);
        //}
 
        public virtual IMergeCellCollection CreateDefaultMergeCells(DataExcel grid)
        {
            return new MergeCellCollection(grid);
        }

        public virtual IBackCellCollection CreateDefaultBackCells(DataExcel grid)
        {
            return new BackCellCollection(grid);
        }

        public virtual Feng.Excel.IO.BinaryWriter CreateBinaryWriter(System.IO.Stream stream)
        {
            return new Feng.Excel.IO.BinaryWriter(stream);
        }

        public virtual Feng.Excel.IO.BinaryWriter CreateBinaryWriter(byte[] data)
        {
            return new Feng.Excel.IO.BinaryWriter(data);
        }

        public virtual Feng.Excel.IO.BinaryReader CreateBinaryReader(System.IO.Stream stream)
        {
            return new Feng.Excel.IO.BinaryReader(stream);
        }

        public virtual Feng.Excel.IO.BinaryReader CreateBinaryReader(byte[] data)
        {
            return new Feng.Excel.IO.BinaryReader(data);
        }

        public virtual LineStyle CreateLineStyle()
        {
            return new LineStyle();
        }

        public virtual CellBorderStyle CreateBorderStyle()
        {
            return new CellBorderStyle();
        }

        private LineStyle _DeafultLineStyle = null;

        public virtual LineStyle DeafultLineStyle
        {
            get
            {
                if (_DeafultLineStyle == null)
                {
                    _DeafultLineStyle = this.CreateLineStyle();
                }
                return _DeafultLineStyle;
            }
            set
            {
                _DeafultLineStyle = value;
            }
        }

        public virtual IBackCell CreateDefaultBackCell(DataExcel grid)
        {
            return new BackCell(grid);
        }

        #region IClassFactory 成员

        public ICellCollection CreateDefaultCells(IRow row)
        {
            return new CellCollection(row);
        }

        #endregion

        #region IClassFactory 成员

        public IRow CreateDefaultRow(DataExcel grid)
        {
            return new Row(grid);
        }

        #endregion
    }


}
