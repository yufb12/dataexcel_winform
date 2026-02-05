using Feng.Data;
using Feng.Drawing;
using Feng.Excel.Actions;
using Feng.Excel.Args;
using Feng.Excel.Delegates;
using Feng.Excel.Drawing;
using Feng.Excel.Interfaces;
using Feng.Forms.Base;
using Feng.Forms.Controls;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Controls.GridControl;
using Feng.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellGridView : Feng.Forms.Controls.GridControl.GridView, ICellEditControl, IActionList
    {
        public CellGridView()
        {
            hscroll = new CellGridViewHScroll(this);
            vscroll = new CellGridViewVScroll(this);
            Init();
        }
        public CellGridView(DataExcel grid)
        {
            hscroll = new CellGridViewHScroll(this);
            vscroll = new CellGridViewVScroll(this);
            _grid = grid;
            Init();
        }
        public CellGridView(ICell cell)
        {
            hscroll = new CellGridViewHScroll(this);
            vscroll = new CellGridViewVScroll(this);
            Cell = cell;
            Init();
            InitEvent();
        }
        public virtual string ShortName { get { return "CellGridView"; } set { } }
        private DataExcel _grid = null;
        public DataExcel Grid
        {
            get
            {
                if (this.Cell != null)
                {
                    return this.Cell.Grid;
                }
                return _grid;
            }
        }

        public override Control Control
        {
            get
            {
                if (this.Cell != null)
                {
                    return this.Cell.Grid.FindControl();
                }
                return base.Control;
            }
        }
        public void InitEvent()
        {
            if (Cell != null)
            {
                Cell.Grid.ColumnWidthChanged -= new Feng.Excel.Delegates.ColumnWidthChangedEventHandler(Grid_ColumnWidthChanged);
                Cell.Grid.RowHeightChanged -= new RowHeightChangedEventHandler(Grid_RowHeightChanged);
                Cell.Grid.ColumnWidthChanged += new Feng.Excel.Delegates.ColumnWidthChangedEventHandler(Grid_ColumnWidthChanged);
                Cell.Grid.RowHeightChanged += new RowHeightChangedEventHandler(Grid_RowHeightChanged);
                Cell.Grid.CellValueChanged -= new CellValueChangedEventHandler(Grid_CellValueChanged);
                Cell.Grid.CellValueChanged += new CellValueChangedEventHandler(Grid_CellValueChanged);
                Cell.Grid.BeforeDrawSelectCell += new BeforeDrawSelectCellHandler(Grid_BeforeDrawSelectCell);
            }
        }

        public override void Init()
        { 
            base.Init();
        }

        CellGridViewHScroll hscroll = null;
        CellGridViewVScroll vscroll = null;
        public override VScrollerView VScroll
        {
            get
            {
                return vscroll;
            }
        }
        public override HScrollerView HScroll
        {
            get
            {
                return hscroll;
            }
        }

        bool Grid_BeforeDrawSelectCell(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this.Cell == null)
                return false;
            if (this.Cell.Grid.SelectCells != null)
            {
                ICell cell = this.Cell.Grid.SelectCells.BeginCell;
                if (cell != null)
                {
                    ICell cel = this.Cell;
                    if (cel.OwnMergeCell != null)
                    {
                        cel = this.Cell.OwnMergeCell;
                    }
                    if (cell == cel)
                    {
                        DrawHelper.DrawRect(g.Graphics, this.Rect, this.BackColor, 3f, System.Drawing.Drawing2D.DashStyle.Solid);
                        return true;
                    }
                }
            }
            return false;
        }

        void Grid_CellValueChanged(object sender, CellValueChangedArgs e)
        {
            try
            {
                if (e.Cell == this.Cell)
                {
                    ICell cell = e.Cell;
                    if (e.Cell.OwnMergeCell != null)
                    {
                        cell = e.Cell.OwnMergeCell;
                    }
                    this.DataSource = e.Cell.Value;
                }
                else
                {
                    ICell cell = e.Cell;
                    if (e.Cell.OwnMergeCell != null)
                    {
                        cell = e.Cell.OwnMergeCell;
                    }
                    if (cell == this.Cell)
                    {
                        this.DataSource = e.Cell.Value;
                    }
                }

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        void Grid_RowHeightChanged(object sender, IRow row)
        {

            try
            {

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        void Grid_ColumnWidthChanged(object sender, IColumn column)
        {

            try
            {

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        private ICell _cell = null;
        [Browsable(false)]
        public ICell Cell
        {
            get
            {
                if (_cell != null)
                {
                    if (_cell.OwnMergeCell != null)
                    {
                        return _cell.OwnMergeCell;
                    }
                }
                if (_cell != null)
                {
                    if (_cell.Row.Index == 0)
                    {

                    }
                }
                return _cell;
            }
            set
            {
                _cell = value;
                if (value.Row.Index == 0)
                {

                } 
            }
        }

        private Font _font = null;
        [Category(CategorySetting.PropertyUI)]
        public override Font Font
        {
            get
            {
                if (this._font == null)
                {
                    return Cell.Font;
                }
                return _font;
            }
            set
            {
                _font = value;
            }
        }
        private Color _forecolor = Color.Empty;
        [Category(CategorySetting.PropertyUI)]
        public override Color ForeColor
        {
            get
            {
                if (this._forecolor == Color.Empty)
                {
                    return Cell.ForeColor;
                }
                return _forecolor;
            }
            set
            {
                _forecolor = value;
            }
        }
        private Color _backcolor = Color.Empty;
        [Category(CategorySetting.PropertyUI)]
        public override Color BackColor
        {
            get
            {
                if (this._backcolor == Color.Empty)
                {
                    return Cell.BackColor;
                }
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }

        private bool _hasChildEdit = true;
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool HasChildEdit
        {
            get
            {
                return _hasChildEdit;
            }
            set
            {
                _hasChildEdit = value;
            }
        }

        public override int Height
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return (int)Cell.Height;
            }
            set
            {
                base.Height = value;
            }
        }

        public override int Width
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return (int)Cell.Width;
            }
            set
            {
                base.Width = value;
            }
        }

        public override int Left
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return (int)Cell.Left;
            }
            set
            {
                base.Left = value;
            }
        }

        public override int Top
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return (int)Cell.Top;
            }
            set
            {
                base.Top = value;
            }
        }

        [Browsable(false)]
        public virtual string Version
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public virtual int VersionIndex
        {
            get { return 0; }
        }

        [Browsable(false)]
        public virtual string DllName
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public virtual string DownLoadUrl
        {
            get { return string.Empty; }
        }

        private int _id = -1;
        [Browsable(false)]
        public virtual int AddressID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public override void AddControl(Control ctl)
        { 
            base.AddControl(ctl);
        }

        public override void RemoveControl(Control ctl)
        { 
            base.RemoveControl(ctl);
        }

        public void Read(DataExcel grid, int version, DataStruct data)
        {
            _grid = grid;
            ReadDataStruct(data);
            if (this.Grid != null)
            {
                this.Grid.LoadCompleted += new SimpleEventHandler(Grid_LoadCompleted);
            }
        }
        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader stream = new IO.BinaryReader(data.Data))
            {
                DataStruct ds = stream.ReadIndex(0, DataStruct.DataStructNull);
                if (ds != null)
                {
                    base.ReadDataStruct(ds);
                }
                this._backcolor = stream.ReadIndex(1, this._backcolor);
                this._font = stream.ReadIndex(2, this._font);
                this._forecolor = stream.ReadIndex(3, this._forecolor);
                this._id = stream.ReadIndex(4, this._id);
                rowindex = stream.ReadIndex(5, 0);
                columnindex = stream.ReadIndex(6, 0);
                this._PropertyCellEndEdit = stream.ReadIndex(7, this._PropertyCellEndEdit);
                this._PropertyCellSelectChanged = stream.ReadIndex(8, this._PropertyCellSelectChanged);
            }
        }
        protected int rowindex = 0;
        protected int columnindex = 0;
        void Grid_LoadCompleted(object sender)
        {

            try
            {
                this.Grid.LoadCompleted -= new SimpleEventHandler(Grid_LoadCompleted);

                InitEvent();
                this.InitDataSource();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
#if DEBUG
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
#endif
            }

        }

        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
                {
                    bw.Write(0, base.Data);
                    bw.Write(1, this._backcolor);
                    bw.Write(2, this._font);
                    bw.Write(3, this._forecolor);
                    bw.Write(4, this._id);
                    bw.Write(5, 0);//this.Cell.Row.Index);
                    bw.Write(6, 0);// this.Cell.Column.Index);
                    bw.Write(7,this._PropertyCellEndEdit);
                    bw.Write(8,this._PropertyCellSelectChanged);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override Point PointToClient(Point pt)
        {
            Point p = (pt);
            return new Point(p.X - this.Left, p.Y - this.Top);
        }

        public override Point PointToScreen(Point pt)
        {
            if (this.Cell == null)
                return Point.Empty;
            return this.Cell.Grid.PointToScreen(pt);
        }
 
        public override void BeginSetCursor(Cursor begincursor)
        {
            Cell.Grid.BeginSetCursor(begincursor);
        }
 
        public virtual bool InitEdit(object obj)
        {
            
            return false;
        }

        public override bool InDesign
        {
            get
            {
                if (this.Cell == null)
                    return false;
                return this.Cell.Grid.InDesign;
            }
            set
            { }
        }

        public bool InEdit
        {
            get { return false; }
        }

        public void TextPress(string text)
        {

        }

        public bool DrawCell(IBaseCell cell, GraphicsObject g)
        {
            this.OnDraw(this, g);
            return true;
        }

        public bool DrawCellBack(IBaseCell cell, GraphicsObject g)
        {
            return false;
        }

        public bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            return false;
        }

        public bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            return false;
        }

        public bool PrintCellBack(IBaseCell cell, PrintArgs e)
        {
            return false;
        }
 
        public override void Invalidate()
        {
            this.Cell.Grid.Invalidate();

        }
        public override void Invalidate(Rectangle rc)
        {
            this.Cell.Grid.Invalidate();
        }
 
        public virtual ICellEditControl Clone(DataExcel grid)
        {
            CellGridView celledit = new CellGridView(grid);
            celledit._backcolor = this._backcolor;
            celledit._font = this._font;
            celledit._forecolor = this._forecolor;
            celledit._hasChildEdit = this._hasChildEdit;
            return celledit;
        }
 
        public override void OnFocusedCellChanged(GridViewCell cell)
        {
            base.OnFocusedCellChanged(cell);
            if (this.Cell != null)
            {
                ActionArgs e = new ActionArgs(PropertyCellSelectChanged, this.Cell.Grid, this.Cell);
                this.Cell.Grid.ExecuteAction(e, PropertyCellSelectChanged, "GridViewCell", cell);
            }
        }
        private bool lockendedit = false;
        public override void EndEdit()
        {
            if (lockendedit)
                return;
            try
            {

                lockendedit = true;
                GridViewCell cell = this.EditCell;
                this.Grid.EndEdit();
                base.EndEdit();
                if (cell != null)
                {
                    ActionArgs e = new ActionArgs(PropertyCellEndEdit, this.Cell.Grid, this.Cell);
                    this.Cell.Grid.ExecuteAction(e, PropertyCellEndEdit, "GridViewCell", cell);
                }
            }
            finally
            {
                lockendedit = false;
            }
        }
        private string _PropertyCellSelectChanged = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyCellSelectChanged
        {
            get
            {
                return _PropertyCellSelectChanged;
            }
            set
            {
                _PropertyCellSelectChanged = value;

            }
        }
        private CellGridViewCellSelectChangedAction propertycellselectchangedaction = null;
        public virtual CellGridViewCellSelectChangedAction CellSelectChangedAction 
        {
            get { 
                if (propertycellselectchangedaction == null)
                {
                    propertycellselectchangedaction = new CellGridViewCellSelectChangedAction(this);
                }
                return propertycellselectchangedaction;
            }
        }
 
        private string _PropertyCellEndEdit = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyCellEndEdit
        {
            get
            {
                return _PropertyCellEndEdit;
            }
            set
            {
                _PropertyCellEndEdit = value;

            }
        }
        private CellGridViewCellEndEditAction cellendeditaction = null;
        public virtual CellGridViewCellEndEditAction CellEndEditAction
        {
            get
            {
                if (cellendeditaction == null)
                {
                    cellendeditaction = new CellGridViewCellEndEditAction(this);
                }
                return cellendeditaction;
            }
        }

        public virtual List<PropertyAction> GetActiones()
        {
            List<PropertyAction> list = new List<PropertyAction>();
            list.Add(CellSelectChangedAction);
            list.Add(CellEndEditAction);
            return list;
        }

        public override void OnCellValueChanged(GridViewCell cell)
        {
            base.OnCellValueChanged(cell);
        }
         
    }

    public interface IActionList
    {
        List<PropertyAction> GetActiones();
    }

    public class CellGridViewCellSelectChangedAction : PropertyAction
    {
        public CellGridView CellGridView { get; set; }
        public CellGridViewCellSelectChangedAction(CellGridView cellgridview) : base()
        {
            CellGridView = cellgridview;
        }
        public override string ActionName { get { return "CellSelectChanged"; } set { } }
        public override string Descript { get { return "内嵌表格单元选中改变"; } set { } }
        public override string ShortName { get { return "CellSelectChanged"; } set { } }
        public override string Script
        {
            get
            {
                return CellGridView.PropertyCellSelectChanged;
            }
            set { CellGridView.PropertyCellSelectChanged = value; }
        }
    }

    public class CellGridViewCellEndEditAction : PropertyAction
    {
        public CellGridView CellGridView { get; set; }
        public CellGridViewCellEndEditAction(CellGridView cellgridview) : base()
        {
            CellGridView = cellgridview;
        }
        private string actionname= "CellEndEdit";
        private string descript = "内嵌表格单元结束编辑";
        private string shortname = "CellEndEdit";
        public override string ActionName { get { return actionname; } set { actionname = value; } }
        public override string Descript { get { return descript; } set { descript = value; } }
        public override string ShortName { get { return shortname; } set { shortname = value; } }
        public override string Script
        {
            get
            {
                return CellGridView.PropertyCellEndEdit;
            }
            set { CellGridView.PropertyCellEndEdit = value; }
        }
    }

    public class CellGridViewHScroll : HScrollerView
    {
        public CellGridViewHScroll(CellGridView grid)
        {
            _Grid = grid;
        }
        private CellGridView _Grid = null;
        public CellGridView Grid
        {
            get
            {
                return _Grid;
            }
        }

        public override int Top
        {
            get
            {
                return Grid.Top + Grid.Height - this.Height;
            }
            set
            {
            }
        }

        public override int Left
        {
            get
            {
                return Grid.Left;
            }
            set
            {
            }
        }

        public override int Width
        {
            get
            {
                return Grid.Width - this.Height;
            }
            set
            {
            }
        }

        public override void OnValueChanged(int value)
        {
            Grid.SetFirstColumn(value);
            Grid.RefreshColumns();
            base.OnValueChanged(value);
        }
    }


    public class CellGridViewVScroll : VScrollerView
    {
        public CellGridViewVScroll(CellGridView grid)
        {
            _Grid = grid;
        }
        private CellGridView _Grid = null;
        public CellGridView Grid
        {
            get
            {
                return _Grid;
            }
        }
        public override int Height
        {
            get
            {
                return Grid.Height - this.Width - this.Grid.ColumnHeaderHeight;
            }
            set
            {
            }
        }

        public override int Left
        {
            get
            {
                return Grid.Width - this.Width;
            }
            set
            {
            }
        }

        public override int Top
        {
            get
            {
                return this.Grid.ColumnHeaderHeight;
            }
            set
            {
            }
        }

        public override void OnValueChanged(int value)
        {
            Grid.SetPosition(value);
            base.OnValueChanged(value);
        }


        public virtual ICellEditControl Clone(DataExcel grid)
        {
            CellGridView celledit = new CellGridView(grid);
            return celledit;
        }
    }
}
