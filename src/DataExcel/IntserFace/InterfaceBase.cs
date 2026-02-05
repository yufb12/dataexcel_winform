using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Collections;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Feng.Forms.Interface;
using Feng.Data;
using Feng.Enums;
using Feng.Excel.Styles;
namespace Feng.Excel.Interfaces
{
    #region 基础接口
    public interface IDataNode<T>
    {
        T Parent { get; set; }
        T Previous { get; set; }
        T Next { get; set; }
        T First { get; set; }
        T Last { get; set; }

    }

    public interface IToString
    {
        string ToString();
    }

    public interface IOwnMergeCell
    {
        IMergeCell OwnMergeCell { get; set; }
    }

    public interface IOwnBackCell
    {
        IBackCell OwnBackCell { get; set; }
    }

    public interface IStream
    {

    }
    [Guid(Feng.Excel.App.Product.AssemblyComGuid____), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectSafety
    {
        // methods 
        void GetInterfacceSafyOptions(
            System.Int32 riid,
            out System.Int32 pdwSupportedOptions,
            out System.Int32 pdwEnabledOptions);
        void SetInterfaceSafetyOptions(
            System.Int32 riid,
            System.Int32 dwOptionsSetMask,
            System.Int32 dwEnabledOptions);
    }


    public interface ICellRange
    {

        ICell BeginCell { get; set; }

        ICell EndCell { get; set; }


        ICell MinCell { get; }

        ICell MaxCell { get; }
    }

    public interface IGrid
    {
        DataExcel Grid { get; }
    }
 
 
    public interface ISpText : ICellEditControl
    {
        string Text1 { get; set; }
        string Text2 { get; set; }
    }
    #endregion
    public interface ICellBaseCheckBox : ICellEditControl, ICheckOnClick
    {

    }

    public interface ICellRadioCheckBox : ICellBaseCheckBox
    {

    }


    public interface IFalseString
    {
        string FalseString { get; set; }
    }

    public interface ICellCheckBox : ICellBaseCheckBox 
    {

    }

    public interface ICellComboBox : ICellEditControl, IDisplayMember, IValueMember, ISelectIndex
    {
        //void SetValue<T>(T model);
    }


    public interface IDataExcelRead 
    {
        void Read(DataExcel grid, int version, DataStruct data);
    }

    public interface IReadDataStruct
    {
        void ReadDataStruct(DataStruct data);
    }
    public interface IDropDownGrid
    {
        DataExcel GetDropDownGrid();
    }

    public interface IFile : IDataExcelRead, IPlusAssembly, IDataStruct
    {

    }

    public interface ICellColor : IControlColor, ISelectColor, ILineColor
    {

    }

    public interface IWndProc
    {
        void WndProc(ref Message m, ref bool Handled);
    }
 
    public interface ICellTextChanged
    {
        bool FireCellValueChanged(string txt);
    }

    public interface ICellEditControlGetInstance
    {
        ICellEditControl Clone(IBaseCell cell);
    }

    public interface IIsVisibleInGrid
    {
        bool IsVisibleInGrid { get; }
    }

    public interface IRefresh
    {
        void Refresh();
    }



    
    
    public interface ISelectBorderWidth
    {
        float SelectBorderWidth { get; set; }
    }
 
    public interface IExpressionText
    {
        string Expression { get; set; }
        int ExpressionIndex { get; set; }
    }

    public interface IExpression : IExpressionText, IAutoExecuteExpress, IExecuteExpress
    {

    }

    public interface IAutoExecuteExpress
    {
        bool AutoExecuteExpress { get; set; }
    }

    public interface IExecuteExpress
    {
        void ExecuteExpression();
    }

    public interface IValues
    {
        IDictionary<object, object> Values { get; set; }
    }



    public interface ICellType
    {
        CellType CellType { get; set; }
    }


    public interface IBorderSetting
    {
        CellBorderStyle BorderStyle { get; set; }
    }


    public interface IUpdateVersion
    {
        int UpdateVersion { get; set; }
    }

    public interface IOwnEditControl
    {
        ICellEditControl OwnEditControl { get; set; }
    }

    public interface ISetAllDeafultBoarder
    {
        void SetSelectCellBorderBorderOutside();
    }


    public interface IDrawFunctionBorder
    {
        void DrawFunctionBorder(Feng.Drawing.GraphicsObject g, int index);
    }

    public interface ICurrentCell
    {
        ICell Cell { get; set; }
    }

    public interface IOwnCell
    {
        IBaseCell Cell { get; set; }
    }
 

    public interface IGetFocused
    {
        void GetFocused();
    }

    public interface ISizeMode
    {
        ImageLayout SizeMode { get; set; }
    }

    public interface IFunctionBorder
    {
        bool FunctionBorder { get; set; }

    }

    public interface IPermissions
    {
        string Permissions { get; set; }
        Purview Purview { get; set; }
    }


    //public interface ICurrentRowHeader
    //{
    //    IRowHeaderCell RowHeader { get; set; }
    //}

    //public interface ICurrentColumnHeader
    //{
    //    IColumnHeaderCell ColumnHeader { get; set; }
    //}

    public interface IICellCollection
    {
        ICellCollection Cells { get; set; }
    }

    //public interface ISelected
    //{
    //    bool Selected { get; set; }
    //}

    //public interface IFullColumnSelected
    //{
    //    bool Selected { get; set; }
    //}
    public interface ICellSelect
    {
        bool CellSelect { get; }
    }
    public interface IFullRowSelectedColor
    {
        Color FullRowSelectedColor { get; set; }
    }
    public interface IFullColumnSelectedColor
    {
        Color FullColumnSelectedColor { get; set; }
    }


    public interface IDefaultCellEdit
    {
        ICellEditControl DefaultCellEdit { get; set; }
    }




    public interface IIsMergeCell
    {
        bool IsMergeCell { get; }
    }

    public interface IBaseControl : IGrid, IBounds, IViewEvent, IDraw, IDrawBack, IPerformEvent
    {

    }

    public interface IFunctionCells
    {
        List<ICell> FunctionCells { get; }
    }

    public interface IParentFunctionCells
    {
        List<ICell> ParentFunctionCells { get; set; }
        void AddParentFunctionCell(ICell cell);
        void ExecuteParentExpresses();
    }
 
 

    public interface IContensSize : IContensWidth, IContensHeigth
    {

    }

    public interface IFreshContens
    {
        void FreshContens();
    }


    public interface IKeyValue
    {
        Dictionary<object, object> KeyValue { get; set; }
    }
 

    public interface IToMergeCellArray
    {
        IMergeCell[] ToArray();
    }


    public interface IDesignMode
    {
        DataExcelDesignMode DesignMode { get; set; }
    }

    public interface IImageCell : IExtendCell
    {
        Image Image { get; set; }
        IImageCellCollection ImageCellCollection { get; set; }
    }


    public interface ISizeRect
    {

        bool SizeRectContains(Point pt);
    }

    public interface IMouseMove
    {
        bool MouseMove(Point pt);

    }
    public interface IMouseDown
    {
        bool MouseDown(Point pt);
    }

    public interface ITextCell : IExtendCell
    {
        StringBuilder StringBuilder { get; set; }
    }



    public interface IReSetRowColumn
    {
        void ReSetRowColumn(Point pt);
        void FreshLocation();
    }

    public interface IExpandCellRelativePosition
    {

        IColumn LeftColumn { get; set; }
        int LeftDistance { get; set; }
        int TopDistance { get; set; }
        IRow TopRow { get; set; }
    }
    public interface ICanChangedSize
    {
        bool CanChangedSize { get; set; }
    }

    public interface ISizeChangedMode
    {
        SizeChangMode SizeChangMode { get; set; }
    }
    public interface IExtendCellDoubleClick
    {
        void OnExtendCellDoubleClick();
    }



    public interface IMouseDownPoint
    {
        Point MouseDownPoint { get; set; }
    }
    public interface IMouseDownSize
    {
        Size MouseDownSize { get; set; }
    }
    //public interface IFullRowSelected
    //{
    //    bool FullRowSelected { get; set; }
    //}
    public interface ICurrentRow
    {
        IRow Row { get; set; }
    }
    public interface ICurrentIColumnCollection
    {
        IColumnCollection Columns { get; set; }
    }

    public interface ICurrentIRosCollection
    {
        IRowCollection Rows { get; set; }
    }
    public interface ICurrentColumn
    {
        IColumn Column { get; set; }
    }


    public interface IMaxRowIndex
    {
        int MaxRowIndex { get; }
    }
    public interface IMaxColumnIndex
    {
        int MaxColumnIndex { get; }
    }

    public interface IMaxCell : IMaxRowIndex, IMaxColumnIndex
    {

    }


    public interface ITableCellPrinted
    {
        bool IsTableCellPrinted { get; set; }
    }


    public interface IDrawGridLine
    {
        void DrawGridLine(Feng.Drawing.GraphicsObject g);
    }

    public interface ICanPrintBorder
    {
        bool IsPrintBorder { get; set; }
    }

    public interface ICanPrintBackImage
    {
        bool IsPrintBackImage { get; set; }
    }

    public interface ICanPrintText
    {
        bool IsPrintText { get; set; }
    }

    public interface ICanPrintBackColor
    {
        bool IsPrintBackColor { get; set; }
    }

    public interface IPrintSet : ICanPrintText, ICanPrintBackImage, ICanPrintBorder, ICanPrintBackColor
    {

    }



    //public interface IDrawRow
    //{
    //    void DrawRow(Graphics g);
    //}

    //public interface IDrawColumn
    //{
    //    void DrawColumn(Graphics g);
    //}
    //public interface IDrawHeader
    //{
    //    void DrawRowHeader(Graphics g);
    //}


    public interface ISort
    {
        void Sort();
    }

    public interface IContains
    {
        bool Contains(int item);
    }

    //public interface IColumnHeaders
    //{
    //    IColumnHeaderCollection ColumnHeaders { get; set; }
    //}
    //public interface IRowHeaders
    //{
    //    IRowHeaderCollection RowHeaders { get; set; }
    //}

    public interface IContainsSplit
    {
        bool ContainsSplit(Point pt);
    }


    public interface IGuid
    {
        string Guid { get; set; }
    }



    //public interface IFillPanel
    //{
    //    void FillPanel(Graphics g, Rectangle rect);
    //    void FillPanel(Graphics g, Rectangle rectf);
    //    void FillPanel(Graphics g, Rectangle rect, string skinname);
    //    void FillPanel(Graphics g, Rectangle rectf, string skinname);
    //}

    //public interface IDrawBorder
    //{
    //    void DrawBorder(Graphics g, Rectangle rectf);
    //    void DrawBorder(Graphics g, Rectangle rect);
    //    void DrawBorder(Graphics g, Rectangle rectf, int borderwidth);
    //    void DrawBorder(Graphics g, Rectangle rect, int borderwidth);
    //    void DrawBorder(Graphics g, Rectangle rectf, int borderwidth, string style);
    //    void DrawBorder(Graphics g, Rectangle rect, int borderwidth, string style);
    //    void DrawBorder(Graphics g, Rectangle rectf, string style, string skinname);
    //    void DrawBorder(Graphics g, Rectangle rect, string style, string skinname);
    //    void DrawBorder(Graphics g, Rectangle rectf, int borderwidth, string style, string skinname);
    //    void DrawBorder(Graphics g, Rectangle rect, int borderwidth, string style, string skinname);
    //}

    //public interface IDrawText
    //{
    //    void DrawText(Graphics g, Rectangle rect);
    //    void DrawText(Graphics g, Rectangle rectf);
    //}

    //public interface IDrawControl : IDrawBorder, IFillPanel, IDrawText
    //{

    //}

    //public interface IDrawRadioButton : IDrawControl
    //{
    //    void DrawRadioButton(Graphics g, Rectangle rect);
    //    void DrawRadioButton(Graphics g, Rectangle rectf);
    //    void DrawRadioButton(Graphics g, Rectangle rect, string skinname);
    //    void DrawRadioButton(Graphics g, Rectangle rectf, string skinname);
    //}

    //public interface IDrawButton : IDrawControl
    //{
    //    void DrawButton(Graphics g, Rectangle rect);
    //    void DrawButton(Graphics g, Rectangle rectf);
    //    void DrawButton(Graphics g, Rectangle rect, string skinname);
    //    void DrawButton(Graphics g, Rectangle rectf, string skinname);
    //}
}
