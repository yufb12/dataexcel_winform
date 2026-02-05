using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Feng.Excel.Print;
using Feng.Excel.Extend;
using Feng.Enums;
using Feng.Excel.Args;
using Feng.Excel.Delegates;
using Feng.Excel.Interfaces;
using Feng.Args;
using Feng.Forms.Interface;
using Feng.Excel.Base;
using Feng.Excel.Collections;
using Feng.Forms.Events;
using Feng.Excel.Actions; 

namespace Feng.Excel
{
    partial class DataExcel
    {

        #region 用户事件;
        public event Feng.EventHelper.ExceptionHandler Exception;

        //public event  RowHeaderClickEventHandler RowHeaderClick;
        //public event  ColumnHeaderClickEventHandler ColumnHeaderClick;
        public event CellClickEventHandler CellClick;
        public event EventHandler TopHeaderClick;

        public event Feng.Forms.Events.EditEvent.CopyHandler Copying;
        public event Feng.Forms.Events.EditEvent.CutHandler Cuting;
        public event Feng.Forms.Events.EditEvent.PasteHandler Pasting;


        //public event  RowHeaderCellMouseMoveEventHandler RowHeaderCellMouseMove;
        //public event  ColumnHeaderCellMouseMoveEventHandler ColumnHeaderCellMouseMove;

        public event BeforColumnWidthChangedEventHandler BeforColumnWidthChanged;
        public event Delegates.ColumnWidthChangedEventHandler ColumnWidthChanged;
        public event BeforRowHeightChangedEventHandler BeforRowHeightChanged;
        public event RowHeightChangedEventHandler RowHeightChanged;

        public event BeforeInsertRowEventHandler BeforeInsertRow;
        public event BeforeInsertColumnEventHandler BeforeInsertColumn;
        public event BeforeDeleteRowEventHandler BeforeDeleteRow;
        public event BeforeDeleteColumnEventHandler BeforeDeleteColumn;

        public event InsertRowEventHandler InsertedRow;
        public event InsertColumnEventHandler InsertedColumn;
        public event DeleteRowEventHandler RowDeleted;
        public event DeleteColumnEventHandler ColumnDeleted;

        public event EventHandler Inited;

        //public event  RowHeaderDoubleClickEventHandler RowHeaderDoubleClick;
        //public event  ColumnHeaderDoubleClickEventHandler ColumnHeaderDoubleClick;

        public event CellAddRectangleClickEventHandler CellAddRectangleClick;
        public event CellAddRectangleMouseMoveEventHandler CellAddRectangleMouseMove;
        public event BeforeCellCheckChangedEventHandler BeforeCellCheckChanged;
        public event CellCheckChangedEventHandler CellCheckChanged;
        public event FirstDisplayRowEventHandler FirstDisplayRowChanged;
        public event FirstDisplayColumnChangedEventHandler FirstDisplayColumnChanged;

        public event BeforUseDefultSettingEventHandler BeforUseDefultSetting;
        public event BeforeCellSelectChangedEventHandler BeforeSelectCellChanged;
        public event BeforeCellTextChangedEventHandler BeforeCellTextChanged;
        public event CellSelectChangedEventHandler SelectCellChanged;
        public event CellSelectChangedEventHandler SelectCellFinished;
        public event RowHeightChangedEventHandler RowHeightChangedFinished;
        public event Feng.Excel.Delegates.ColumnWidthChangedEventHandler ColumnWidthChangedFinished;
        public event CellSelectChangedEventHandler FunctionSelectCellChanged;
        public event FocusedCellChangedEventHandler FocusedCellChanged;
        public event CellValueChangedEventHandler CellValueChanged;
        public event CellEditControlValueChangedEventHandler CellEditControlValueChanged;
        public event BeforeCellValueChangedEventHandler BeforeCellValueChanged;
        public event BeforeExecuteExpressEventHandler BeforeExecuteExpress;
        public event ExecuteExpressCompletedEventHandler ExecuteExpressCompleted;
        public event BeforeInitEventHandler BeforeInit;
        public event CellInitEditEventHandler CellInitEdit;
        public event BeforeCellInitEditEventHandler BeforeCellInitEdit;
        public event CellEndEditEventHandler CellEndEdit;
        public event CellTextChangedEventHandler CellTextChanged;

        public event BeforeAddMethodEventHandler BeforeAddMethod;
        public event BeforeHeaderVisibleChangeEventHandler BeforeHeaderVisibleChange;
        public event RowHeaderVisibleChangedEventHandler RowHeaderVisibleChanged;
        public event ColumnHeaderVisibleChangedEventHandler ColumnHeaderVisibleChanged;
        public event ExtendCellDoubleClickEventHandler ExtendCellDoubleClick;
        public event BeforeGridRowLineVisibleChanged BeforeGridRowLineVisibleChanged;
        public event BeforeGridColumnLineVisibleChanged BeforeGridColumnLineVisibleChanged;
        public event GridRowLineVisibleChanged GridRowLineVisibleChanged;
        public event GridColumnLineVisibleChanged GridColumnLineVisibleChanged;

        public event DrawGridRowLineEventHandler DrawGridRowLine;
        public event DrawGridColumnLineEventHandler DrawGridColumnLine;
        //public event  DrawRowHeaderEventHandler DrawRowHeader;
        //public event  DrawColumnHeaderEventHandler DrawColumnHeader;
        public event DrawRowEventHandler DrawRow;
        public event DrawRowBackEventHandler DrawRowBack;
        public event DrawColumnEventHandler DrawColumn;
        public event DrawCellEventHandler DrawCell;
        public event DrawCellBackEventHandler DrawCellBack;
        public event DrawCellBorderEventHandler DrawCellBorder;
        public event BeforeSetExpressEventHandler BeforeSetExpress;
        public event SetExpressEventHandler SetExpress;
        public event Feng.EventHelper.FiguresChangedEventHandler FiguresChanged;

        public event BeforeDrawCellEventHandler BeforeDrawCell;
        public event BeforeDrawCellBackEventHandler BeforeDrawCellBack;

        //public event  CellMouseMoveEventHandler CellMouseMove;
        //public event  CellDoubleClickEventHandler CellDoubleClick;
        public event CellMouseUpHandler CellMouseUp;
        public event CellMouseMoveHandler CellMouseMove;
        public event CellMouseLeaveHandler CellMouseLeave;
        public event CellMouseHoverHandler CellMouseHover;
        public event CellMouseEnterHandler CellMouseEnter;
        public event CellMouseDownHandler CellMouseDown;
        public event CellMouseDoubleClickHandler CellMouseDoubleClick;
        public event CellMouseClickHandler CellMouseClick;
        public event CellMouseCaptureChangedHandler CellMouseCaptureChanged;
        public event CellKeyDownHandler CellKeyDown;
        public event CellKeyPressHandler CellKeyPress;
        public event CellKeyUpHandler CellKeyUp;
        public event CellPreviewKeyDownHandler CellPreviewKeyDown;
        public event CellDoubleClickHandler CellDoubleClick;
        public event CellMouseWheelHandler CellMouseWheel;

        public event ExtendCellClickHandler ExtendCellClick;
        public event BeforeSetFirstDisplayColumnHandler BeforeFirstDisplayColumnChanged;
        public event BeforeSetFirstDisplayRowHandler BeforeFirstDisplayRowChanged;
        public event CellBackColorChangedHandler CellBackColorChanged;


        public event BeforeSaveFileHandler BeforeSaveFile;
        public event SaveFileHandler SaveFile;

        public event BeforeOpenFileHandler BeforeOpenFile;
        public event OpenFileHandler OpenFile;
        public event SimpleEventHandler NewFile;
        public event CalcRowHeightHandler CalcRowHeight;
        public event CalcColumnWidthHandler CalcColumnWidth;

        public event CellPaintingEventHandler CellPainting;
        public event BeforeMoveToNextCellEventHandler BeforeMoveToNextCell;
        public event BeforeDrawSelectCellHandler BeforeDrawSelectCell;
        public event SimpleEventHandler FunctionSelectedFinish;
        public event SimpleEventHandler LoadCompleted;
        public event ExecFunctionEventHandler ExecFunction;
         
        public event CellEditEeventHandler EditEvented;

        public event BeforeCommandExcuteHandler BeforeCommandExcute;
        public event CommandExcutedHandler CommandExcuted;
        public event MouseCaptureChangedHandler MouseLeave;
        public event MouseCaptureChangedHandler MouseEnter;
        public event FormatDisplayHandler FormatDisplayCell;
        public event SelectRangeChangedEventHandler SelectRangeChanged;
        public event EventHandler EndCellValueEdit;
        #endregion

        #region 触发事件
        public virtual void OnEndCellValueEdit()
        {
            if (EndCellValueEdit != null)
            {
                BaseEventArgs e = EventArgsCache.Pool.Pop();
                if (e == null)
                {
                    e = new BaseEventArgs();
                }
                EndCellValueEdit(this, e);
                EventArgsCache.Pool.Push(e);
            }
        }
        public virtual void OnSelectRangeChanged(SelectRangeCollection range)
        { 
            if (SelectRangeChanged != null)
            {
                SelectRangeChanged(this, range);
            }
        }
        public virtual void OnFormatDisplay(Cell cell)
        {
            if (FormatDisplayCell != null)
            {
                FormatDisplayCell(this, cell);
            }
        }
        public virtual void OnMouseLeave(IBounds bound, Point pt)
        {
            if (MouseLeave != null)
            {
                MouseLeave(this, bound, pt);
            }
        }
        public virtual void OnMouseEnter(IBounds bound, Point pt)
        {
            if (MouseEnter != null)
            {
                MouseEnter(this, bound, pt);
            }
        }

        public virtual void OnBeforeCommandExcute(BeforeCommandExcuteArgs e)
        {
            if (BeforeCommandExcute != null)
            {
                BeforeCommandExcute(this, e);
            }
        }
        public virtual void OnCommandExcuted(CommandExcutedArgs e)
        {
            if (CommandExcuted != null)
            {
                CommandExcuted(this, e);
            }
        }
        public virtual string OnCopying(string text)
        {
            if (Copying != null)
            {
                return Copying(this, text);
            }
            return text;
        }
        public virtual string OnCuting(string text)
        {
            if (Cuting != null)
            {
                return Cuting(this, text);
            }
            return text;
        }
        public virtual string OnPasting(string text)
        {
            if (Pasting != null)
            {
                return Pasting(this, text);
            }
            return text;
        }
        public virtual void OnEditEvented(ICellEditControl edit, Type handler, params object[] args)
        {
            if (EditEvented != null)
            {
                EditEvented(this, edit, handler, args);
            }
        } 
        public virtual object OnExecFunction(HandledEventArgs e)
        {
            object value = null;
            if (ExecFunction != null)
            {
                value = ExecFunction(this, e);
            }
            return value;
        }
        public virtual bool OnBeforeDrawSelectCell(Feng.Drawing.GraphicsObject g)
        {
            if (BeforeDrawSelectCell != null)
            {
                return BeforeDrawSelectCell(this, g);
            }
            return false;
        }

        public virtual void OnBeforeMoveToNextCell(BeforeCellCancelArgs e, NextCellType next)
        {
            if (BeforeMoveToNextCell != null)
            {
                BeforeMoveToNextCell(this, e, next);
            }
        }

        public virtual void OnCellPainting(CellPaintingEventArgs e)
        {
            if (CellPainting != null)
            {
                CellPainting(this, e);
            }
        }

        public virtual void OnSaveFile(string filename)
        {
            if (SaveFile != null)
            {
                SaveFile(this, filename);
            }
        }

        public virtual void OnBeforeSaveFile(bool handled, string file)
        {
            if (BeforeSaveFile != null)
            {
                BeforeSaveFile(this, handled, file);
            }
        }

        public virtual void OnOpenFile(string filename)
        {
            if (OpenFile != null)
            {
                OpenFile(this, filename);
            }
        }
        public virtual void OnNew()
        {
            if (NewFile != null)
            {
                NewFile(this);
            }
        }
        public virtual void OnBeforeOpenFile(bool handled, string file)
        {
            if (BeforeOpenFile != null)
            {
                BeforeOpenFile(this, handled, file);
            }
        }

        public virtual void OnBeforeFirstDisplayColumnChanged(BeforeFirstDisplayColumnChangedArgs e)
        {
            if (BeforeFirstDisplayColumnChanged != null)
            {
                BeforeFirstDisplayColumnChanged(this, e);
            }
        }

        public virtual void OnBeforeFirstDisplayRowChanged(BeforeFirstDisplayRowChangedArgs e)
        {
            if (BeforeFirstDisplayRowChanged != null)
            {
                BeforeFirstDisplayRowChanged(this, e);
            }
        }

        public virtual void OnFirstDisplayColumnChanged(int index)
        {
            this.CloseEdit();
            if (FirstDisplayColumnChanged != null)
            {
                FirstDisplayColumnChanged(this, index);
            }
        }

        public virtual void OnFirstDisplayRowChanged(int index)
        {
            this.CloseEdit();
            if (FirstDisplayRowChanged != null)
            {
                FirstDisplayRowChanged(this, index);
            }
        }

        public virtual void OnExtendCellClick(ExtendCell extendcell)
        {
            if (ExtendCellClick != null)
            {
                ExtendCellClick(this, extendcell);
            }
        }

        public virtual void OnCellBackColorChanged(ICell cell, Color color)
        {
            if (CellBackColorChanged != null)
            {
                CellBackColorChanged(this, cell, color);
            }
        }

        public virtual void OnCellClick(ICell cell)
        {
            if (CellClick != null)
            {
                CellClick(this, cell);
            }
        }

        public virtual void OnCellMouseUp(ICell cell, MouseEventArgs e)
        {
            if (CellMouseUp != null)
            {
                CellMouseUp(this, cell, e);
            }
        }

        public virtual void OnCellMouseMove(ICell cell, MouseEventArgs e)
        {
            if (CellMouseMove != null)
            {
                CellMouseMove(this, cell, e);
            }
        }

        public virtual void OnCellMouseLeave(ICell cell)
        {
            if (CellMouseLeave != null)
            {
                CellMouseLeave(this, cell);
            }
        }

        public virtual void OnCellMouseHover(ICell cell)
        {
            if (CellMouseHover != null)
            {
                CellMouseHover(this, cell);
            }
        }

        public virtual void OnCellMouseEnter(ICell cell)
        {
            if (CellMouseEnter != null)
            {
                CellMouseEnter(this, cell);
            }
        }

        public virtual void OnCellMouseDown(ICell cell, MouseEventArgs e)
        {
            if (CellMouseDown != null)
            {
                CellMouseDown(this, cell, e);
            }
        }

        public virtual void OnCellMouseDoubleClick(ICell cell, MouseEventArgs e)
        {
            if (CellMouseDoubleClick != null)
            {
                CellMouseDoubleClick(this, cell, e);
            }
        }

        public virtual void OnCellMouseClick(ICell cell, MouseEventArgs e)
        {
            if (CellMouseClick != null)
            {
                CellMouseClick(this, cell, e);
            }
        }

        public virtual void OnCellMouseCaptureChanged(ICell cell)
        {
            if (CellMouseCaptureChanged != null)
            {
                CellMouseCaptureChanged(this, cell);
            }
        }

        public virtual void OnCellKeyDown(ICell cell, KeyEventArgs e)
        {
            if (CellKeyDown != null)
            {
                CellKeyDown(this, cell, e);
            }
        }

        public virtual void OnCellKeyPress(ICell cell, KeyPressEventArgs e)
        {
            if (CellKeyPress != null)
            {
                CellKeyPress(this, cell, e);
            }
        }

        public virtual void OnCellKeyUp(ICell cell, KeyEventArgs e)
        {
            if (CellKeyUp != null)
            {
                CellKeyUp(this, cell, e);
            }
        }

        public virtual void OnCellPreviewKeyDown(ICell cell, PreviewKeyDownEventArgs e)
        {
            if (CellPreviewKeyDown != null)
            {
                CellPreviewKeyDown(this, cell, e);
            }
        }
        public virtual void OnCellDoubleClick(ICell cell)
        {
            if (CellDoubleClick != null)
            {
                CellDoubleClick(this, cell);
            }
        }
        public virtual void OnCellMouseWheel(ICell cell, MouseEventArgs e)
        {
            if (CellMouseWheel != null)
            {
                CellMouseWheel(this, cell, e);
            }
        }
        public virtual void OnException(Exception ex)
        {
            Feng.Utils.BugReport.Log(ex);
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex); 
            if (this.Exception != null)
            {
                Exception(this, ex);
            }

        }
        public virtual void OnBeforColumnWidthChanged(BeforeColumnWidthChangedArgs e)
        {
            if (BeforColumnWidthChanged != null)
            {
                BeforColumnWidthChanged(this, e);
            }
        }
        public virtual void OnColumnWidthChanged(IColumn column)
        {

            if (ColumnWidthChanged != null)
            {
                ColumnWidthChanged(this, column);
            }

        }
        public virtual void OnBeforRowHeightChanged(BeforRowHeightChangedArgs e)
        {
            if (BeforRowHeightChanged != null)
            {
                BeforRowHeightChanged(this, e);
            }
        }
        public virtual void OnRowHeightChanged(IRow row)
        {
            if (RowHeightChanged != null)
            {
                RowHeightChanged(this, row);
            }
        }
        public virtual void OnBeforeInsertRow(BeforeInsertRowCancelArgs e)
        {
            if (this.BeforeInsertRow != null)
            {
                this.BeforeInsertRow(this, e);
            }
        }
        public virtual void OnBeforeInsertColumn(BeforeInsertColumnCancelArgs e)
        {
            if (this.BeforeInsertColumn != null)
            {
                this.BeforeInsertColumn(this, e);
            }
        }
        public virtual void OnBeforeDeleteRow(BeforeDeleteRowCancelArgs e)
        {
            if (this.BeforeDeleteRow != null)
            {
                this.BeforeDeleteRow(this, e);
            }
        }
        public virtual void OnBeforeDeleteColumn(BeforeDeleteColumnCancelArgs e)
        {
            if (this.BeforeDeleteColumn != null)
            {
                this.BeforeDeleteColumn(this, e);
            }
        }

        public virtual void OnInsertRow(IRow row)
        {
            if (this.InsertedRow != null)
            {
                this.InsertedRow(this, row);
            }
        }
        public virtual void OnInsertColumn(IColumn column)
        {
            if (this.InsertedColumn != null)
            {
                this.InsertedColumn(this, column);
            }
        }
        public virtual void OnDeleteRow(IRow row)
        {
            if (this.RowDeleted != null)
            {
                this.RowDeleted(this, row);
            }
        }
        public virtual void OnDeleteColumn(IColumn column)
        {
            if (this.ColumnDeleted != null)
            {
                this.ColumnDeleted(this, column);
            }
        }

        public virtual void OnCellSelectChanged(ISelectCellCollection selectcells)
        {
            if (SelectCellChanged != null)
            {
                SelectCellChanged(this, selectcells);
            }
        }
        public virtual void OnSelectCellFinished(ISelectCellCollection selectcells)
        {
            if (SelectCellFinished != null)
            {
                SelectCellFinished(this, selectcells);
            }
        }
        public virtual void OnRowHeightChangedFinished(IRow row)
        {
            if (row != null)
            {
                foreach (var item in this.SelectRows)
                {
                    IRow rowa = item as IRow;
                    if (rowa == row)
                    {
                        continue;
                    }
                    rowa.Height = row.Height;
                }
             
            }
            if (RowHeightChangedFinished != null)
            {
                RowHeightChangedFinished(this, row);
            }
        }
        public virtual void OnColumnWidthChangedFinished(IColumn column)
        {
            if (column != null)
            {
                foreach (var item in this.SelectColumns)
                {
                    IColumn columna = item as IColumn;
                    if (columna == column)
                    {
                        continue;
                    }
                    //ICell cell = this[0, column.Index];
                    if (columna.Selected)
                    {
                        columna.Width = column.Width;
                    }
                }

            }
            if (ColumnWidthChangedFinished != null)
            {
                ColumnWidthChangedFinished(this, column);
            }
        }
        public virtual void OnFunctionSelectCellChanged(ISelectCellCollection selectcells)
        {
            if (FunctionSelectCellChanged != null)
            {
                FunctionSelectCellChanged(this, selectcells);
            }
        }
        public virtual void OnFunctionSelectedFinish()
        {
            if (FunctionSelectedFinish != null)
            {
                FunctionSelectedFinish(this);
            }
        }
        public virtual void OnTopHeaderClick(EventArgs e)
        {
            if (this.TopHeaderClick != null)
            {
                this.TopHeaderClick(this, new EventArgs());
            }
        }

        public virtual void OnCellValueChanged(CellValueChangedArgs e)
        {
            if (CellValueChanged != null)
            {
                CellValueChanged(this, e);
            }
            if (PropertyValueChanged != null && !string.IsNullOrWhiteSpace(PropertyValueChanged))
            {
                ExecuteAction(new ActionArgs(PropertyValueChanged, this, this.FocusedCell), PropertyValueChanged);
            }
        }
 
        public virtual void OnCellEditControlValueChanged(ICell cell, object value)
        {
            if (CellEditControlValueChanged != null)
            {
                CellValueChangedArgs e = new CellValueChangedArgs(cell);
                CellEditControlValueChanged(this, e, value);
            }

        }
        public virtual void OnBeforeCellCheckChanged(BeforeCellCheckChangedArgs e)
        {
            if (this.BeforeCellCheckChanged != null)
            {
                BeforeCellCheckChanged(this, e);
            }
        }
        public virtual void OnCellCheckChanged(CellCheckChangedArgs e)
        {
            if (this.CellCheckChanged != null)
            {
                CellCheckChanged(this, e);
            }
        }
        public virtual void OnBeforeCellValueChanged(BeforeCellValueChangedArgs e)
        {
            if (BeforeCellValueChanged != null)
            {
                BeforeCellValueChanged(this, e);
            }
        }
        public virtual void OnCellTextChanged(ICell cell)
        {
            if (CellTextChanged != null)
            {
                CellTextChanged(this, cell);
            }
            if (this.AutoExecuteExpress)
            {
                cell.ExecuteParentExpresses();
            }
        }
        public virtual void OnBeforeExecuteExpress(BeforeExecuteExpressArgs e)
        {
            if (BeforeExecuteExpress != null)
            {
                BeforeExecuteExpress(this, e);
            }
        }
        public virtual void OnExecuteExpressCompleted(ExecuteExpressArgs e)
        {
            if (ExecuteExpressCompleted != null)
            {
                ExecuteExpressCompleted(this, e);
            }
        }
        public virtual void OnBeforeAddMethod(BeforeAddMethodArgs e)
        {
            if (BeforeAddMethod != null)
            {
                BeforeAddMethod(this, e);
            }
        }
        public virtual void OnExtendCellDoubleClick(IExtendCell cell)
        {
            if (ExtendCellDoubleClick != null)
            {
                ExtendCellDoubleClick(this, cell);
            }
        }
        public virtual void OnBeforeCellTextChanged(BeforeCellTextChangedArgs e)
        {
            if (BeforeCellTextChanged != null)
            {
                BeforeCellTextChanged(this, e);
            }
        }
        public virtual void OnDrawGridRowLine(DrawGridRowLineArgs e)
        {
            if (DrawGridRowLine != null)
            {
                DrawGridRowLine(this, e);
            }
        }
        public virtual void OnDrawGridColumnLine(DrawGridColumnLineArgs e)
        {
            if (DrawGridColumnLine != null)
            {
                DrawGridColumnLine(this, e);
            }
        }
        //public virtual void OnDrawRowHeader(DrawRowHeaderArgs e)
        //{

        //    if (DrawRowHeader != null)
        //    {
        //        DrawRowHeader(this, e);
        //    }
        //}
        //public virtual void OnDrawColumnHeader(DrawColumnHeaderArgs e)
        //{
        //    if (DrawColumnHeader != null)
        //    {
        //        DrawColumnHeader(this, e);
        //    }
        //}
        public virtual void OnDrawRow(DrawRowArgs e)
        {
            if (DrawRow != null)
            {
                DrawRow(this, e);
            }
        }
        public virtual void OnDrawRowBack(DrawRowBackArgs e)
        {
            if (DrawRowBack != null)
            {
                DrawRowBack(this, e);
            }
        }
        public virtual void OnDrawColumn(DrawColumnArgs e)
        {
            if (DrawColumn != null)
            {
                DrawColumn(this, e);
            }
        }
        public virtual void OnDrawCell(DrawCellArgs e)
        {

            if (DrawCell != null)
            {
                DrawCell(this, e);
            }
        }
        public virtual void OnDrawCellBack(DrawCellBackArgs e)
        {

            if (DrawCellBack != null)
            {
                DrawCellBack(this, e);
            }
        }
        public virtual void OnDrawCellBorder(DrawCellBorderArgs e)
        {
            if (DrawCellBorder != null)
            {
                DrawCellBorder(this, e);
            }
        }
        public virtual void OnBeforeCellInitEdit(BeforeInitEditCancelArgs e)
        {
            if (BeforeCellInitEdit != null)
            {
                BeforeCellInitEdit(this, e);
            }
        }
        public virtual void OnCellInitEdit(ICell cell)
        {
            if (CellInitEdit != null)
            {
                CellInitEdit(this, cell);
            }
        }
        public virtual void OnCellEndEdit(ICell cell)
        {
            if (CellEndEdit != null)
            {
                CellEndEdit(this, cell);
            }
        }
        public virtual void OnBeforeSetExpress(BeforeSetExpressCancelArgs e)
        {
            if (BeforeSetExpress != null)
            {
                BeforeSetExpress(this, e);
            }
        }
        public virtual void OnSetExpress(ICell cell)
        {
            if (SetExpress != null)
            {
                SetExpress(this, cell);
            }
        }
        public virtual void OnFiguresChanged(object sender, FiguresEventArgs e)
        {
            if (this.FiguresChanged != null)
            {
                this.FiguresChanged(this, e);
            }
        }
        public virtual void OnBeforeDrawCell(object sender, BeforeDrawCellArgs e)
        {
            if (this.BeforeDrawCell != null)
            {
                this.BeforeDrawCell(this, e);
            }
        }
        public virtual void OnBeforeDrawCellBack(object sender, BeforeDrawCellBackArgs e)
        {
            if (this.BeforeDrawCellBack != null)
            {
                this.BeforeDrawCellBack(this, e);
            }
        }
        public virtual void OnBeforePaste(BeforeCellValueCancelArgs e)
        {
            if (this.BeforePaste != null)
            {
                this.BeforePaste(this, e);
            }
        }

        public virtual void OnBeforeCut(BeforeCellValueCancelArgs e)
        {
            if (this.BeforeCut != null)
            {
                this.BeforeCut(this, e);
            }
        }

        public virtual void OnBeforeCopy(BeforeCellValueCancelArgs e)
        {
            if (this.BeforeCopy != null)
            {
                this.BeforeCopy(this, e);
            }
        }

        public event BeforePasteHandler BeforePaste;

        public event BeforeCutHandler BeforeCut;

        public event BeforeCopyHandler BeforeCopy;

        #endregion

        #region 事件方法
 

        #endregion

    }
}
