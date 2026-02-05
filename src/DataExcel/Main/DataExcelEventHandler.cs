
using Feng.Enums; 
using Feng.Excel.Args;
using Feng.Excel.Base;
using Feng.Excel.Collections;
using Feng.Excel.Extend;
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Delegates
{
    public delegate void PaintedEventHandler(object sender, Feng.Drawing.GraphicsObject graphicsobject);
    public delegate bool MouseDownHandled(MouseEventArgs mes);
    public delegate void DrawSpeedHandler(Feng.Drawing.GraphicsObject g);
    public delegate void BeforeAddNewBindingDataHandler(object sender, object value, DataExcelCancelEventArgs e);
    public delegate object ExecFunctionEventHandler(object sender, HandledEventArgs e);
    public delegate void NewDocumentEventHandler(object sender, EventArgs e);

    public delegate void CellValueChangedEventHandler(object sender, CellValueChangedArgs e);
    public delegate void CellEditControlValueChangedEventHandler(object sender, CellValueChangedArgs e, object value);
    public delegate void CellTextChangedEventHandler(object sender, ICell cell);
    public delegate void BeforeCellTextChangedEventHandler(object sender, BeforeCellTextChangedArgs e);
    public delegate void BeforeCellValueChangedEventHandler(object sender, BeforeCellValueChangedArgs e);
    public delegate void BeforeMoveToNextCellEventHandler(object sender, BeforeCellCancelArgs e, NextCellType next);

    //public delegate void RowHeaderClickEventHandler(object sender, IRowHeaderCell item);
    //public delegate void ColumnHeaderClickEventHandler(object sender, IColumnHeaderCell item);
    public delegate void BeforeInsertRowEventHandler(object sender, BeforeInsertRowCancelArgs e);
    public delegate void BeforeInsertColumnEventHandler(object sender, BeforeInsertColumnCancelArgs e);
    public delegate void BeforeDeleteRowEventHandler(object sender, BeforeDeleteRowCancelArgs e);
    public delegate void BeforeDeleteColumnEventHandler(object sender, BeforeDeleteColumnCancelArgs e);
    public delegate void BeforeSetExpressEventHandler(object sender, BeforeSetExpressCancelArgs e);

    public delegate void BeforeCellInitEditEventHandler(object sender, BeforeInitEditCancelArgs e);
    public delegate void CellEndEditEventHandler(object sender, ICell cell);
    public delegate void InsertRowEventHandler(object sender, IRow row);
    public delegate void InsertColumnEventHandler(object sender, IColumn column);
    public delegate void DeleteRowEventHandler(object sender, IRow row);
    public delegate void DeleteColumnEventHandler(object sender, IColumn column);
    public delegate void SetExpressEventHandler(object sender, ICell cell);

    public delegate void CellClickEventHandler(object sender, ICell cell);
    public delegate void CellBackColorChangedHandler(object sender, ICell cell, Color color);
    public delegate void BeforeCellCheckChangedEventHandler(object sender, BeforeCellCheckChangedArgs e);
    public delegate void CellCheckChangedEventHandler(object sender, CellCheckChangedArgs cell);
    public delegate void CellDoubleClickEventHandler(object sender, ICell cell);
    //public delegate void RowHeaderDoubleClickEventHandler(object sender, IRowHeaderCell cell);
    //public delegate void ColumnHeaderDoubleClickEventHandler(object sender, IColumnHeaderCell cell);
    public delegate void CellAddRectangleClickEventHandler(object sender, ISelectCellCollection selecedcells);
    public delegate void CellAddRectangleMouseMoveEventHandler(object sender, System.Drawing.Point point, ICell cell, ISelectCellCollection selecedcells);
    public delegate void CellMouseMoveEventHandler(object sender, ICell cell, MouseEventArgs e);
    //public delegate void RowHeaderCellMouseMoveEventHandler(object sender, IRowHeaderCell rowhead, MouseEventArgs e);
    //public delegate void ColumnHeaderCellMouseMoveEventHandler(object sender, IColumnHeaderCell columnhead, MouseEventArgs e);

    public delegate void FirstDisplayRowEventHandler(object sender, int index);
    public delegate void FirstDisplayColumnChangedEventHandler(object sender, int index);

    public delegate void CellInitEditEventHandler(object sender, ICell cell);



    public delegate void BeforCellSelectedChangedEventHandler(ICell cell, BeforeCellValueChangedArgs e);
    public delegate void CellSelectedChangedEventHandler(object sender, ICell cell);
    public delegate void CalcRowHeightEventHandler(object sender, RowHeightEventArgs e);

    public delegate void BeforColumnWidthChangedEventHandler(object sender, BeforeColumnWidthChangedArgs e);
    public delegate void ColumnWidthChangedEventHandler(object sender, IColumn column);
    public delegate void BeforRowHeightChangedEventHandler(object sender, BeforRowHeightChangedArgs e);
    public delegate void RowHeightChangedEventHandler(object sender, IRow row);

    public delegate void ExpressionChangedEventHandler();
    public delegate void BeforUseDefultSettingEventHandler(object sender, DataExcelBaseCancelArgs e);


    public delegate void BeforeCellSelectChangedEventHandler(object sender, CellChangedArgs e);
    public delegate void BeforTextEditTextChangedEventHandler(object sender, TextChangedCancelArgs e);
    public delegate void CellSelectChangedEventHandler(object sender, ISelectCellCollection selectcells);
    public delegate void FocusedCellChangedEventHandler(object sender, ICell cell);
    public delegate void BeforeInitEventHandler(object sender, CancelEventArgs e);
    public delegate void BeforeExecuteExpressEventHandler(object sender, BeforeExecuteExpressArgs e);
    public delegate void ExecuteExpressCompletedEventHandler(object sender, ExecuteExpressArgs cell);
    public delegate void BeforeAddMethodEventHandler(object sender, BeforeAddMethodArgs e);
    public delegate void BeforeHeaderVisibleChangeEventHandler(object sender, BeforeHeaderVisibleChangedArgs e);
    public delegate void RowHeaderVisibleChangedEventHandler(object sender, EventArgs e);
    public delegate void ColumnHeaderVisibleChangedEventHandler(object sender, EventArgs e);
    public delegate void ExtendCellDoubleClickEventHandler(object sender, IExtendCell cell);

    public delegate void BeforeGridRowLineVisibleChanged(object sender, BeforeGridRowLineVisibleChangedArgs e);
    public delegate void GridRowLineVisibleChanged(object sender, EventArgs e);
    public delegate void BeforeGridColumnLineVisibleChanged(object sender, BeforeGridColumnLineVisibleChangedArgs e);
    public delegate void GridColumnLineVisibleChanged(object sender, EventArgs e);

    public delegate void BeforeDrawCellEventHandler(object sender, BeforeDrawCellArgs e);
    public delegate void BeforeDrawCellBackEventHandler(object sender, BeforeDrawCellBackArgs e);

    public delegate void DrawGridRowLineEventHandler(object sender, DrawGridRowLineArgs e);
    public delegate void DrawGridColumnLineEventHandler(object sender, DrawGridColumnLineArgs e);
    //public delegate void DrawRowHeaderEventHandler(object sender, DrawRowHeaderArgs e);
    //public delegate void DrawColumnHeaderEventHandler(object sender, DrawColumnHeaderArgs e);
    public delegate void DrawRowEventHandler(object sender, DrawRowArgs e);
    public delegate void DrawRowBackEventHandler(object sender, DrawRowBackArgs e);
    public delegate void DrawColumnEventHandler(object sender, DrawColumnArgs e);
    public delegate void DrawCellEventHandler(object sender, DrawCellArgs e);
    public delegate void DrawCellBackEventHandler(object sender, DrawCellBackArgs e);
    public delegate void DrawCellBorderEventHandler(object sender, DrawCellBorderArgs e);


    public delegate void CellPaintingEventHandler(object sender, CellPaintingEventArgs e);



    public delegate void CellMouseUpHandler(object sender, ICell cell, MouseEventArgs e);
    public delegate void CellMouseMoveHandler(object sender, ICell cell, MouseEventArgs e);
    public delegate void CellMouseLeaveHandler(object sender, ICell cell);
    public delegate void CellMouseHoverHandler(object sender, ICell cell);
    public delegate void CellMouseEnterHandler(object sender, ICell cell);
    public delegate void CellMouseDownHandler(object sender, ICell cell, MouseEventArgs e);
    public delegate void CellMouseDoubleClickHandler(object sender, ICell cell, MouseEventArgs e);
    public delegate void CellMouseClickHandler(object sender, ICell cell, MouseEventArgs e);
    public delegate void CellMouseCaptureChangedHandler(object sender, ICell cell);
    public delegate void CellKeyDownHandler(object sender, ICell cell, KeyEventArgs e);
    public delegate void CellKeyPressHandler(object sender, ICell cell, KeyPressEventArgs e);
    public delegate void CellKeyUpHandler(object sender, ICell cell, KeyEventArgs e);
    public delegate void CellPreviewKeyDownHandler(object sender, ICell cell, PreviewKeyDownEventArgs e);
    public delegate void CellDoubleClickHandler(object sender, ICell cell);
    public delegate void CellMouseWheelHandler(object sender, ICell cell, MouseEventArgs e);

    public delegate void CellInitEditHandler(object sender, ICell cell);
    public delegate void CellEndEditHandler(object sender, ICell cell);

    public delegate void BeforePasteHandler(object sender, BeforeCellValueCancelArgs e);
    public delegate void BeforeCutHandler(object sender, BeforeCellValueCancelArgs e);
    public delegate void BeforeCopyHandler(object sender, BeforeCellValueCancelArgs e);
    public delegate void BeforeSetFirstDisplayColumnHandler(object sender, BeforeFirstDisplayColumnChangedArgs e);
    public delegate void BeforeSetFirstDisplayRowHandler(object sender, BeforeFirstDisplayRowChangedArgs e);

    public delegate void ExtendCellClickHandler(object sender, ExtendCell extencell);


    public delegate void BeforeSaveFileHandler(object sender, bool handled, string file);
    public delegate void SaveFileHandler(object sender, string file);

    public delegate void BeforeOpenFileHandler(object sender, bool handled, string file);
    public delegate void OpenFileHandler(object sender, string file);
    public delegate bool CalcRowHeightHandler(object sender, IRow row);
    public delegate void CalcColumnWidthHandler(object sender, IColumn column);

    public delegate void PrintCellHandler(object sender, PrintCellArgs e);
    public delegate void PrintCellBackHandler(object sender, PrintCellBackArgs e);

    public delegate bool BeforeDrawSelectCellHandler(object sender, Feng.Drawing.GraphicsObject g);
    public delegate void SimpleEventHandler(object sender);
    public delegate void ActionChangedEventHandler(object sender,string action);
    public delegate void CellEditEeventHandler(object sender, ICellEditControl edit, Type handler, params object[] args);


    public delegate void BeforeCommandExcuteHandler(object sender, BeforeCommandExcuteArgs e);
    public delegate void CommandExcutedHandler(object sender, CommandExcutedArgs e);
    public delegate void MouseCaptureChangedHandler(object sender, IBounds bound, Point pt);

    public delegate void FormatDisplayHandler(object sender, Cell cell);
    public delegate void SelectRangeChangedEventHandler(object sender, SelectRangeCollection range);
    

#if DEBUG
    public delegate void DoTest(object sender, EventArgs e);
#endif


}
