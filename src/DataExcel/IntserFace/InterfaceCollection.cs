using Feng.Forms.Interface;
using System.Collections.Generic;
namespace Feng.Excel.Interfaces
{



    public interface ICellChangedCollection : IList<ICell>, ICollection<ICell>,
IEnumerable<ICell>, IAddrangle<ICell>, IGrid
    {

    }

    public interface ICellCollection : IList<ICell>, ICollection<ICell>,
IEnumerable<ICell>, IAddrangle<ICell>, IGrid, ICurrentRow
    {
        ICell this[IColumn column] { get; set; }
    }

    public interface IFunctionCellection : IList<IBaseCell>, ICollection<IBaseCell>,
IEnumerable<IBaseCell>, IAddrangle<IBaseCell>, IGrid, IExecuteExpress
    {

    }


    public interface IMergeCellCollection : IList<IMergeCell>, ICollection<IMergeCell>,
    IEnumerable<IMergeCell>, IAddrangle<IMergeCell>, IGrid, IDraw, IRefresh, IFile,IReadDataStruct
    {

    }

    public interface IBackCellCollection : IList<IBackCell>, ICollection<IBackCell>,
    IEnumerable<IBackCell>, IAddrangle<IBackCell>, IGrid, IDraw, IRefresh, IFile,IReadDataStruct
    {

    }


    public interface IImageCellCollection : IList<IExtendCell>, ICollection<IExtendCell>,
    IEnumerable<IExtendCell>, IAddrangle<IExtendCell>, IGrid, IDraw, IRefresh
    {

    }
    public interface IGridViewCollection : IExtendCellCollection
    {

    }

    public interface IExtendCellCollection : IList<IExtendCell>, ICollection<IExtendCell>,
    IEnumerable<IExtendCell>, IAddrangle<IExtendCell>, IGrid, IDraw, IRefresh, IFile
    {

    }


    public interface IDataExcelChartCellCollection : IList<IExtendCell>, ICollection<IExtendCell>,
    IEnumerable<IExtendCell>, IAddrangle<IExtendCell>, IGrid, IDraw, IRefresh
    {

    }



    public interface IColumnCollection : IList<IColumn>, ICollection<IColumn>, IReadOnlyMax,
        IEnumerable<IColumn>, IGrid, Feng.Print.IPrint, IContains, IFile,IReadDataStruct
    {
        IColumn this[string name] { get; }
        void Refresh();
    }

    public interface IRowCollection : IList<IRow>, ICollection<IRow>, IReadOnlyMax,
        IEnumerable<IRow>, IGrid, Feng.Print.IPrint, IContains, IFile, ISort,IReadDataStruct, IRefresh
    {
        //List<IRow> CellSelectRows { get; set; }
        //void InsertRow(int index);
        int MaxHasValueIndex { get; set; } 
    }

}