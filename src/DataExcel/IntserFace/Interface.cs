using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Forms.Interface;
using Feng.Print;
using Feng.Enums; 

namespace Feng.Excel.Interfaces
{
    public interface IFunctionCell : IGrid, System.Collections.IEnumerable, IClear
    {
        void Add(ICell cell);
        void Remove(ICell cell);
        bool Contains(ICell cell);


    }

    public interface ILockVersion
    {
        Feng.Forms.Base.LockVersion LockVersion { get; set; }
    }
 
    public interface IRow : IBounds, IGrid, IReadOnly, IInhertReadOnly, IControlColor,
        ISelectBorderWidth, ISelectColor, IDraw, IDrawBack, IToString, IIndex, IVisible, IFrozen, IFont,
    ICaption, IICellCollection, ICellSelect, ISelected, ICurrentIRosCollection, IFullRowSelectedColor,
        ILineColor, IName, IClear, IAutoHeight, IPrint, IFile, ITag, ICommandID
        , IAllowChangedSize, IPrintBorder, IFocusForeColor, IFocusBackColor, IDrawGridLine,IChecked
        , IHeaderCell, ILockVersion,IReadDataStruct
    {
        ICell GetCellByIndex(int columnindex);
        ICell GetCellByName(string column);
        ICell this[int columnindex] { get; }
        ICell this[string columnindex] { get; }
        ICell this[IColumn column] { get; }
        bool RowHasValue { get; set; }
    }

    public interface IFixedWidth
    {
        int FixedWidth { get; set; }
    }
    public interface IAllowChangedSize
    {
        bool AllowChangedSize { get; set; }
    }
    public interface IFixedHeight
    {
        int FixedHeight { get; set; }
    }
    public interface IDataType
    {
        Type DataType { get; set; }
    }
    public interface IAliases
    {
        string Aliases { get; set; }
    }

    public interface IHeaderCell
    {
        ICell DefaultStyleCell { get; set; } 
    }
    public interface IDeleted
    {
        bool Deleted { get; set; }
    }
    public interface IColumn : IBounds, IGrid, IReadOnly, IInhertReadOnly, IControlColor,
         ISelectBorderWidth, ISelectColor, IDraw, IToString, IIndex, IVisible,
         ICaption, IFrozen, IFont, ISelected, ICellSelect, ITag, IFormat,
        ICurrentIColumnCollection, ILineColor, IFullColumnSelectedColor, IName, IClear, IAutoWidth,
        IPrint, IFile, IDataType, ISortOrder, IAllowChangedSize, ICommandID, IPerformEvent
        , IHeaderCell, IDeleted, IReadDataStruct
    {

    }

    public interface ISortOrder
    {
        Feng.Forms.ComponentModel.SortOrder Order { get; set; }
    }


    public interface IBingValue
    {
        object BingValue { get; set; }
    }

    public interface ISendMessage
    {
        void SendMessage(System.Windows.Forms.Message m);

    }

    public interface IDefaultValue
    {
        string DefaultValue { get; set; }
    }
    public interface IAllowCopy
    {
        bool AllowCopy { get; set; }
    }
    public interface ICell : IOwnBackCell, IOwnMergeCell, IBaseCell, ITab, IHotKey,
          IPrintValue, IPrintBack, IPrintBorder, IPrintSet, IShowSelectBorder, IPermissions, IDrawBorder, IFreshContens,
        ILocation, IBingValue, ICommandID, ITag, IFieldName, IDrawGridLine, IDefaultValue, IAllowCopy
        , IToolTip, IAllowInputExpress,IReadDataStruct
    {

    }

    public interface IEdit : IInitEdit, IEndEdit
    {

    }

    public interface IImeCharChanged
    {
        bool ImeCharChanged(IBaseCell cell, char c);
    }
    public interface IControlBase : IEdit, IInEdit, IOwnEditControl, IText, IForeColor, IGrid
        , IBackColor, IHorizontalAlignment, IVerticalAlignment, ITextDirection, IAutoMultiline,
          IReadOnly, IInhertReadOnly, IDraw, ISelected, ISelectColor, IValue, IPrint, IFont, IBounds, IFormat
    {

    }

    public interface IHandle
    {
        IntPtr Handle { get; }
    }


    public interface IDrawCell
    {
        bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g);
    }
 
    public interface ICellEdit : IInitEdit, IEndEdit
    {

    }

    public interface IDrawCellBack
    {
        bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g);
    }
    public interface IPrintCell
    {
        bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect);
        bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value);
    }
    public interface IPrintCellBack
    {
        bool PrintCellBack(IBaseCell cell, PrintArgs e);
    }

    public interface IAddressID
    {
        int AddressID { get; set; }
    }

    public interface ITextPress
    {
        void TextPress(string text);
    }


 

    public interface IHasChild
    {
        bool HasChildEdit { get; set; }
    }
    public interface ICloneCellEdit
    {
        ICellEditControl Clone(DataExcel grid);
    }

    public interface ICellEditControl : IFile, IViewEvent, ICellEdit,
         IInEdit, IDrawCell, IDrawCellBack, IPrintCell, IPrintCellBack,
           IAddressID, ITextPress, ICurrentCell, ICloneCellEdit, IHasChild
        , IShortName, IReadDataStruct
    {

    }

    //执行事件
    public interface IPerformEvent
    {

        string PropertyOnCellInitEdit { get; set; }

        string PropertyOnCellEndEdit { get; set; }

        string PropertyOnCellValueChanged { get; set; }

        string PropertyOnMouseUp { get; set; }

        string PropertyOnMouseMove { get; set; }

        string PropertyOnMouseLeave { get; set; }

        string PropertyOnMouseHover { get; set; }

        string PropertyOnMouseEnter { get; set; }

        string PropertyOnMouseDown { get; set; }

        string PropertyOnMouseDoubleClick { get; set; }

        string PropertyOnMouseClick { get; set; }

        string PropertyOnMouseCaptureChanged { get; set; }

        string PropertyOnMouseWheel { get; set; }

        string PropertyOnClick { get; set; }

        string PropertyOnKeyDown { get; set; }

        string PropertyOnKeyPress { get; set; }

        string PropertyOnKeyUp { get; set; }

        string PropertyOnPreviewKeyDown { get; set; }

        string PropertyOnDoubleClick { get; set; }

        string PropertyOnDrawBack { get; set; }

        string PropertyOnDrawCell { get; set; }
    }

    public interface IBaseCell : IBaseControl, IControlBase, IContensSize, ITextRect,IEditMode,
        IControlColor, IAutoMultiline, ICurrentRow, ICurrentColumn, IToString,
          IExpression, IAlignment, ICellType, IBorderSetting, IDisplayMember, IValueMember,
        IFormat, IUpdateVersion, ICaption, IFunctionCells, IMaxCell,
        IParentFunctionCells, IClear, ISetAllDeafultBoarder, IDrawFunctionBorder,
        IBackImage, ITextDirection, IIsMergeCell, ITableCellPrinted,
          IEventImageColorSet, IVisible,  IRemark, IExtend,
         IFile, IKeyValue, IFunctionBorder, IName,IUrl,ITable, IText3
    {

    }


    public interface IMergeCell : ICellRange, IRefresh, ICell, IClose,IReadDataStruct
    {
        IMergeCellCollection MergeCellCollection { get; set; }
        void DeleteColumn(IColumn column);
        void DeleteRow(IRow row);
        void InSertColumn(IColumn column);
        void InSertRow(IRow row);
    }

    public interface IBackCell : ICellRange, IRefresh, ISelectBorderWidth, ICell, IClose, IDisposable
    {
        IBackCellCollection BackCellCollection { get; set; }
        void DeleteColumn(IColumn column);
        void DeleteRow(IRow row);
        void InSertColumn(IColumn column);
        void InSertRow(IRow row);
    }


    public interface IExtendCell : IBaseControl, IControlBase, IMouseDownPoint, IMouseDownSize, IViewEvent
    {
        void FreshLocation();
        void ReSetRowColumn(Point pt);
        SizeChangMode SizeChangMode
        {
            get;
            set;
        }
        bool SizeRectContains(Point pt);
        bool MouseDown(Point pt);
    }


    public interface IScrollerBase : IDraw, IFont, IBounds, IVisible,
    IInt32Value, IMaxMin, IClear, IChange, INext, IVisibleCount, IViewEvent
    {
        event Feng.EventHelper.BeforePositionChangedEventHandler BeforeValueChanged;
        event Feng.EventHelper.ValueChangedEventHandler ValueChanged;
        event Feng.EventHelper.ClickEventHandler Click;
        event Feng.EventHelper.ClickEventHandler ThumdAreaClick;
        event Feng.EventHelper.ClickEventHandler DownArrowAreaClick;
        event Feng.EventHelper.ClickEventHandler UpArrowAreaClick;
    }

    public interface IDataExcelScroller : IScrollerBase, IGrid
    {
    }

}
