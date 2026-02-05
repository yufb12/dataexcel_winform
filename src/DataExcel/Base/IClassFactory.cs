using System; 
using Feng.Excel.Interfaces;
using Feng.Excel.Styles;
using Feng.Script.CBEexpress;

namespace Feng.Excel.Interfaces
{
    public interface IClassFactory
    {
        IColumn CreateDefaultColumn(DataExcel grid, int index);
        IColumn CreateDefaultColumn(DataExcel grid);
        IColumnCollection CreateDefaultColumns(DataExcel grid);
        IFunctionCell CreateDefaultFunctionCells(DataExcel grid);
        IMethodCollection CreateDefaultMethods(DataExcel grid);
        IRowCollection CreateDefaultRows(DataExcel grid);
        ICell CreateDefaultCell(DataExcel grid, int rowindex, int columnindex);
        ICell CreateDefaultCell(IRow row, IColumn column);
        ICell CreateDefaultCell(DataExcel grid);
        IFunctionCell CreateDefaultFunctionCellection(DataExcel grid);
        IRow CreateDefaultRow(DataExcel grid, int index);
        IRow CreateDefaultRow(DataExcel grid);
        IMethodCollection CreateDefaultRunMethodHelperCollection(DataExcel grid);
        IMergeCellCollection CreateDefaultMergeCells(DataExcel grid);
        IBackCellCollection CreateDefaultBackCells(DataExcel grid);

        IO.BinaryWriter CreateBinaryWriter(System.IO.Stream stream);
        IO.BinaryWriter CreateBinaryWriter(byte[] data);
        IO.BinaryReader CreateBinaryReader(System.IO.Stream stream);
        IO.BinaryReader CreateBinaryReader(byte[] data);
        IBackCell CreateDefaultBackCell(DataExcel grid);
        ICellCollection CreateDefaultCells(IRow row);
        LineStyle CreateLineStyle();
        CellBorderStyle CreateBorderStyle();
    }
}
