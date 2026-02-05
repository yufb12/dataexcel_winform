using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Feng.Enums;
using Feng.Excel.Interfaces;
using Feng.Forms;

namespace Feng.Excel.Args
{
    public class DrawArgs : EventArgs
    {
        public DrawArgs(Feng.Drawing.GraphicsObject g)
        {
            _Graphics = g;
        }
        private Feng.Drawing.GraphicsObject _Graphics = null;
        public Feng.Drawing.GraphicsObject Graphics
        {
            get { return _Graphics; }
        }
        private bool _handled = false;
        public virtual bool Handled
        {
            get { return _handled; }
            set { _handled = value; }
        }
    }

    public class DrawGridRowLineArgs : DrawArgs
    {
        public DrawGridRowLineArgs(Feng.Drawing.GraphicsObject g, IRow row)
            : base(g)
        {
            _Row = row;
        }
        private IRow _Row = null;
        public IRow Row
        {
            get { return _Row; }
        }
    }

    public class DrawGridColumnLineArgs : DrawArgs
    {
        public DrawGridColumnLineArgs(Feng.Drawing.GraphicsObject g, IColumn column)
            : base(g)
        {
            _Column = column;
        }
        private IColumn _Column = null;
        public IColumn Column
        {
            get { return _Column; }
        }
    }

    public class HandleEventArgs : EventArgs
    {
        private bool _handled = false;
        public virtual bool Handled
        {
            get { return _handled; }
            set { _handled = value; }
        }
    }
    public class BeforeCommandExcuteArgs : DataExcelCancelEventArgs
    {
        public object Sender { get; set; }
        public object Value { get; set; }
        public string CommandText { get; set; }
        public string Arg { get; set; }
    }
    public class CommandExcutedArgs
    {
        public object Sender { get; set; }
        public object Value { get; set; }
        public string CommandText { get; set; }
        public string Arg { get; set; }
        public Feng.Forms.Command.CommandObject Command { get; set; }
    }
    //public class MouseHandledEventArgs : HandleEventArgs
    //{
    //    private MouseHandledEventArgs()
    //    {
    //    }

    //    private MouseHandledEventArgs(MouseEventArgs e)
    //    {
    //        _meargs = e;
    //        this._pint = e.Location;
    //    }

    //    public static MouseHandledEventArgs GetInstans(DataExcel grid, MouseEventArgs e)
    //    {

    //        //if (grid.mheas == null)
    //            grid._mheas = new MouseHandledEventArgs(e);
    //        grid._mheas.Handled = false;
    //        grid._mheas.Point = e.Location;
    //        grid._mheas.MouseEventArgs = e;
    //        return grid._mheas;
    //    }
    //    public static MouseHandledEventArgs GetInstans(DataExcel grid, MouseEventArgs e,bool handled)
    //    {
    //        //if (grid.mheas == null)
    //            grid._mheas = new MouseHandledEventArgs(e);
    //        grid._mheas.Handled = handled;
    //        grid._mheas.Point = e.Location;
    //        grid._mheas.MouseEventArgs = e;
    //        return grid._mheas;
    //    }
    //    private MouseEventArgs _meargs = null;
    //    public MouseEventArgs MouseEventArgs
    //    {
    //        get
    //        {
    //            return this._meargs;
    //        }
    //        set
    //        {
    //            this._meargs = value;
    //        }
    //    }

    //    private  Point _pint = Point.Empty;
    //    public virtual Point Point
    //    {
    //        get
    //        {
    //            return this._pint;
    //        }
    //        set
    //        {
    //            this._pint = value;
    //        }
    //    }


    //}


    public class BeforeColumnWidthCancelArgs : CancelEventArgs
    {
        public BeforeColumnWidthCancelArgs()
        {

        }
    }

    public class CellPaintingEventArgs : CancelEventArgs
    {
        private ICell _cell = null;
        private Feng.Drawing.GraphicsObject _graphics = null;
        public ICell Cell { get { return this._cell; } }
        public Feng.Drawing.GraphicsObject Graphics { get { return _graphics; } }
        public CellPaintingEventArgs(ICell cell, Feng.Drawing.GraphicsObject graphics)
        {
            _cell = cell;
            _graphics = graphics;
        }
    }

    public class DataExcelBaseCancelArgs : CancelEventArgs
    {
        public DataExcelBaseCancelArgs()
        {

        }

    }

    public class BeforeFirstDisplayRowChangedArgs : CancelEventArgs
    {
        public BeforeFirstDisplayRowChangedArgs(int index)
        {
            _index = index;
        }
        private int _index = 0;
        public int Index { get { return _index; } }
    }
    public class BeforeFirstDisplayColumnChangedArgs : CancelEventArgs
    {
        public BeforeFirstDisplayColumnChangedArgs(int index)
        {
            _index = index;
        }
        private int _index = 0;
        public int Index { get { return _index; } }
    }
    public class BeforeCellCancelArgs : CancelEventArgs
    {
        public BeforeCellCancelArgs()
        {

        }
        public ICell Cell
        {
            get;
            set;
        }
    }

    public class BeforeCellValueCancelArgs : CancelEventArgs
    {
        public BeforeCellValueCancelArgs()
        {

        }
        public ICell Cell
        {
            get;
            set;
        }
        public object Value
        {
            get;
            set;
        }
    }

    public class BeforeInitEditCancelArgs : DataExcelBaseCancelArgs
    {
        public BeforeInitEditCancelArgs(ICell cell)
        {
            _cell = cell;
        }

        private ICell _cell = null;
        public ICell Cell
        {
            get { return _cell; }
            set { _cell = value; }
        }
    }

    public class BeforeSetExpressCancelArgs : DataExcelBaseCancelArgs
    {
        public BeforeSetExpressCancelArgs(ICell cell)
        {
            _cell = cell;
        }

        private bool _error = false;

        public virtual bool Error
        {
            get { return _error; }
            set { _error = value; }
        }

        private ICell _cell = null;
        public ICell Cell
        {
            get { return _cell; }
            set { _cell = value; }
        }
    }

    public class BeforeInsertRowCancelArgs : DataExcelBaseCancelArgs
    {
        public BeforeInsertRowCancelArgs(IRow row)
        {
            _row = row;
        }
        private IRow _row = null;
        public IRow Row
        {
            get { return _row; }
            set { _row = value; }
        }
    }
    public class BeforeInsertColumnCancelArgs : DataExcelBaseCancelArgs
    {
        public BeforeInsertColumnCancelArgs(IColumn column)
        {
            _column = column;
        }
        private IColumn _column = null;
        public IColumn Column
        {
            get { return _column; }
            set { _column = value; }
        }
    }

    public class BeforeDeleteRowCancelArgs : DataExcelBaseCancelArgs
    {
        public BeforeDeleteRowCancelArgs(IRow row)
        {
            _row = row;
        }
        private IRow _row = null;
        public IRow Row
        {
            get { return _row; }
            set { _row = value; }
        }
    }
    public class BeforeDeleteColumnCancelArgs : DataExcelBaseCancelArgs
    {
        public BeforeDeleteColumnCancelArgs(IColumn column)
        {
            _column = column;
        }
        private IColumn _column = null;
        public IColumn Column
        {
            get { return _column; }
            set { _column = value; }
        }
    }
    public class BeforeCellInitEditArgs : DataExcelBaseCancelArgs
    {
        public BeforeCellInitEditArgs()
            : base()
        {
        }
    }

    public class BeforeAddMethodArgs : DataExcelBaseCancelArgs
    {
        public BeforeAddMethodArgs()
        {

        }

    }



    //public class DrawRowHeaderArgs : DrawArgs
    //{
    //    public DrawRowHeaderArgs(Graphics g, IRowHeaderCell rowheader)
    //        : base(g)
    //    {
    //        _rowheader = rowheader;
    //    }
    //    private IRowHeaderCell _rowheader = null;
    //    public IRowHeaderCell RowHeader
    //    {
    //        get { return _rowheader; }
    //    }
    //}

    //public class DrawColumnHeaderArgs : DrawArgs
    //{
    //    public DrawColumnHeaderArgs(Graphics g, IColumnHeaderCell columnheader)
    //        : base(g)
    //    {
    //        _columnheader = columnheader;
    //    }
    //    private IColumnHeaderCell _columnheader = null;
    //    public IColumnHeaderCell ColumnHeader
    //    {
    //        get { return _columnheader; }
    //    }
    //}

    public class BeforeDrawArgs : DataExcelBaseCancelArgs
    {
        public BeforeDrawArgs(Feng.Drawing.GraphicsObject g)
        {
            _Graphics = g;
        }
        private Feng.Drawing.GraphicsObject _Graphics = null;
        public Feng.Drawing.GraphicsObject Graphics
        {
            get { return _Graphics; }
        }
        private bool _handled = false;
        public virtual bool Handled
        {
            get { return _handled; }
            set { _handled = value; }
        }
    }

    public class BeforeDrawCellArgs : BeforeDrawArgs
    {
        public BeforeDrawCellArgs(Feng.Drawing.GraphicsObject g, ICell cell)
            : base(g)
        {
            _Cell = cell;
        }
        private ICell _Cell = null;
        public ICell Cell
        {
            get { return _Cell; }
        }
    }

    public class BeforeDrawCellBackArgs : BeforeDrawArgs
    {
        public BeforeDrawCellBackArgs(Feng.Drawing.GraphicsObject g, ICell cell)
            : base(g)
        {
            _Cell = cell;
        }
        private ICell _Cell = null;
        public ICell Cell
        {
            get { return _Cell; }
        }
    }

    public class DrawRowArgs : DrawArgs
    {
        public DrawRowArgs(Feng.Drawing.GraphicsObject g, IRow row)
            : base(g)
        {
            _Row = row;
        }
        private IRow _Row = null;
        public IRow Row
        {
            get { return _Row; }
        }
    }
    public class DrawRowBackArgs : DrawArgs
    {
        public DrawRowBackArgs(Feng.Drawing.GraphicsObject g, IRow row)
            : base(g)
        {
            _Row = row;
        }
        private IRow _Row = null;
        public IRow Row
        {
            get { return _Row; }
        }
    }
    public class DrawColumnArgs : DrawArgs
    {
        public DrawColumnArgs(Feng.Drawing.GraphicsObject g, IColumn column)
            : base(g)
        {
            _Column = column;
        }
        private IColumn _Column = null;
        public IColumn Column
        {
            get { return _Column; }
        }
    }

    public class DrawCellBackArgs : DrawArgs
    {
        public DrawCellBackArgs(Feng.Drawing.GraphicsObject g, ICell cell)
            : base(g)
        {
            _Cell = cell;
        }
        private ICell _Cell = null;
        public ICell Cell
        {
            get { return _Cell; }
        }
    }
    public class DrawCellArgs : DrawArgs
    {
        public DrawCellArgs(Feng.Drawing.GraphicsObject g, ICell cell)
            : base(g)
        {
            _Cell = cell;
        }
        private ICell _Cell = null;
        public ICell Cell
        {
            get { return _Cell; }
        }
    }

    public class DrawMergeCellArgs : DrawArgs
    {
        public DrawMergeCellArgs(Feng.Drawing.GraphicsObject g, IMergeCell cell)
            : base(g)
        {
            _Cell = cell;
        }
        private IMergeCell _Cell = null;
        public IMergeCell Cell
        {
            get { return _Cell; }
        }
    }

    public class DrawCellBorderArgs : DrawArgs
    {
        public DrawCellBorderArgs(Feng.Drawing.GraphicsObject g, IBaseCell cell)
            : base(g)
        {
            _Cell = cell;
        }
        private IBaseCell _Cell = null;
        public IBaseCell Cell
        {
            get { return _Cell; }
        }
    }

    public class DrawMergeCellBorderArgs : DrawArgs
    {
        public DrawMergeCellBorderArgs(Feng.Drawing.GraphicsObject g, IMergeCell cell)
            : base(g)
        {
            _Cell = cell;
        }
        private IMergeCell _Cell = null;
        public IMergeCell Cell
        {
            get { return _Cell; }
        }
    }

    public class BeforeGridRowLineVisibleChangedArgs : DataExcelBaseCancelArgs
    {
        public BeforeGridRowLineVisibleChangedArgs()
            : base()
        {
        }
    }

    public class BeforeGridColumnLineVisibleChangedArgs : DataExcelBaseCancelArgs
    {
        public BeforeGridColumnLineVisibleChangedArgs()
            : base()
        {
        }
    }

    public class BeforeHeaderVisibleChangedArgs : DataExcelBaseCancelArgs
    {
        public BeforeHeaderVisibleChangedArgs(HeaderMode headermode)
        {
            _HeaderMode = headermode;
        }
        private HeaderMode _HeaderMode = HeaderMode.RowHeader;
        public HeaderMode HeaderMode
        {
            get { return _HeaderMode; }
            set { _HeaderMode = value; }
        }
    }

    public class BeforeExecuteExpressArgs : DataExcelBaseCancelArgs
    {
        public BeforeExecuteExpressArgs(IBaseCell cell)
        {
            this._cell = cell;
        }
        private IBaseCell _cell;
        public IBaseCell Cell
        {
            get { return this._cell; }

            set { this._cell = value; }
        }
    }

    public class TextChangedCancelArgs : DataExcelBaseCancelArgs
    {
        public KeyEventArgs Key { get; set; }
        public TextChangedCancelArgs(KeyEventArgs e)
        {
            Key = e;
        }
    }

    public class BeforeCellTextChangedArgs : DataExcelBaseCancelArgs
    {
        public IBaseCell Cell { get; set; }
    }

    public class CellChangedArgs : DataExcelBaseCancelArgs
    {
        public ICell Cell { get; set; }
    }

    public class BeforeCellCheckChangedArgs : DataExcelBaseCancelArgs
    {
        public IControlBase Cell { get; set; }
    }

    public class CellCheckChangedArgs : EventArgs
    {
        public IControlBase Cell { get; set; }
    }

    public class CellInitEditArgs : EventArgs
    {
        public ICell Cell { get; set; }
    }

    public class CellValueChangedArgs : EventArgs
    {
        public CellValueChangedArgs(ICell cell)
        {
            this._cell = cell;
        }

        private ICell _cell;
        public ICell Cell
        {
            get { return this._cell; }

            set { this._cell = value; }
        }
    }

    public class ExecuteExpressArgs : EventArgs
    {
        public ExecuteExpressArgs(ICell cell)
        {
            this._cell = cell;
        }

        private ICell _cell;
        public ICell Cell
        {
            get { return this._cell; }

            set { this._cell = value; }
        }
    }

    //public class RowHeaderChangArgs : DataExcelBaseCancelArgs
    //{
    //    public RowHeaderCell RowHeader = null;
    //}

    //public class ColumnHeaderChangArgs : DataExcelBaseCancelArgs
    //{
    //    public IColumnHeaderCell ColumnHeader = null;
    //}

    public class RowHeightEventArgs
    {
        private bool _Handled = false;
        public virtual bool Handled
        {
            get { return _Handled; }
            set { _Handled = value; }
        }

    }



    public class DataExcelCancelEventArgs : CancelEventArgs
    {

    }

    public class BeforeCellValueChangedArgs : CancelEventArgs
    {
        private ICell _cell;
        public ICell Cell
        {
            get { return this._cell; }

            set
            {
                this._cell = value;


            }
        }

        public object NewValue
        {
            get;
            set;
        }
    }

    //public class ValueChangedArgs : EventArgs
    //{
    //    private int _value = 0;
    //    public int Value
    //    {
    //        get { return _value; }
    //        set { _value = value; }
    //    }
    //}



    public class BeforeColumnWidthChangedArgs : DataExcelBaseCancelArgs
    {
        public IColumn ColumnHeader
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
    }

    public class BeforRowHeightChangedArgs : DataExcelBaseCancelArgs
    {
        public IRow Row
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }
    }

    public class PrintCellArgs : CancelEventArgs
    {
        public ICell Cell { get; set; }
        public object Value { get; set; }
        public Rectangle Rect { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
    public class PrintCellBackArgs : CancelEventArgs
    {
        public ICell Cell { get; set; }
        public Rectangle Rect { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
