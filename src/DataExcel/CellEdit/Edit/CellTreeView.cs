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
using Feng.Forms.Controls.TreeView;
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
    public class CellTreeView : Feng.Forms.Controls.TreeView.DataTreeViewBase, ICellEditControl, IActionList
    {

        public CellTreeView(DataExcel grid)
        {
            hscroll = new CellTreeViewHScroll(this);
            vscroll = new CellTreeViewVScroll(this); 
            this.grid = grid;
            Init();
        }

        public virtual string ShortName { get { return "CellTreeView"; } set { } }
        private DataExcel grid = null;
        [Browsable(false)]
        public DataExcel Grid
        {
            get
            {
                return grid;
            }
        }
        public CellTreeView(ICell cell)
        {
            hscroll = new CellTreeViewHScroll(this);
            vscroll = new CellTreeViewVScroll(this);
            Cell = cell;
            Init();
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
                Cell.Grid.CellValueChanged -= new CellValueChangedEventHandler(Grid_CellValueChanged);
                Cell.Grid.ColumnWidthChanged -= new Feng.Excel.Delegates.ColumnWidthChangedEventHandler(Grid_ColumnWidthChanged);
                Cell.Grid.RowHeightChanged -= new RowHeightChangedEventHandler(Grid_RowHeightChanged);
                Cell.Grid.BeforeDrawSelectCell -= new BeforeDrawSelectCellHandler(Grid_BeforeDrawSelectCell);

                Cell.Grid.ColumnWidthChanged += new Feng.Excel.Delegates.ColumnWidthChangedEventHandler(Grid_ColumnWidthChanged);
                Cell.Grid.RowHeightChanged += new RowHeightChangedEventHandler(Grid_RowHeightChanged);
                Cell.Grid.CellValueChanged += new CellValueChangedEventHandler(Grid_CellValueChanged);
                Cell.Grid.BeforeDrawSelectCell += new BeforeDrawSelectCellHandler(Grid_BeforeDrawSelectCell);
            }
        }

        public override void Init()
        {
            this.ShowColumnHeader = false;
            this.ShowFooter = false;
            this.ShowRowHeader = false; 
            base.Init();
        }

        CellTreeViewHScroll hscroll = null;
        CellTreeViewVScroll vscroll = null;
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
                return _cell;
            }
            set
            {
                _cell = value;
                InitEvent();
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
        public override int Height
        {
            get
            {
                ICell cell = this.Cell;
                return (int)cell.Height;
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
                return (int)Cell.Top;
            }
            set
            {
                base.Top = value;
            }
        }

        private bool _AutoGenerateColumns = false;
        [Browsable(true)]
        [DefaultValue(true)]
        [Category(CategorySetting.PropertyData)]
        public override bool AutoGenerateColumns
        {
            get
            {
                return _AutoGenerateColumns;
            }
            set
            {
                _AutoGenerateColumns = value;
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
            //if (!this.Cell.Grid.Controls.Contains(ctl))
            //{
            //    this.Cell.Grid.AddEdit(ctl);
            //}
            base.AddControl(ctl);
        }

        public override void RemoveControl(Control ctl)
        {
            //if (this.Cell.Grid.Controls.Contains(ctl))
            //{
            //    this.Cell.Grid.Controls.Remove(ctl);
            //}
            base.RemoveControl(ctl);
        }

        public void Read(DataExcel grid, int version, DataStruct data)
        {
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
                _AutoGenerateColumns = stream.ReadIndex(7, _AutoGenerateColumns);
                _PropertyCellEndEdit = stream.ReadIndex(8, this._PropertyCellEndEdit);
                _PropertyCellSelectChanged = stream.ReadIndex(9, this._PropertyCellSelectChanged);
                _PropertyNodeSelectChanged = stream.ReadIndex(10, this._PropertyNodeSelectChanged);
            }
        }

        protected int rowindex = 0;
        protected int columnindex = 0;
        void Grid_LoadCompleted(object sender)
        {

            try
            {
                //this.VScroll.Value = 1;
                //this.HScroll.Value = 1;
                this.Grid.LoadCompleted -= new SimpleEventHandler(Grid_LoadCompleted);  
                InitEvent();
                this.RefreshColumns();
                this.RefreshRows();
                ReSetRowHeight();
                RefreshRowValue();

            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
#if DEBUG
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except",ex); 
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
                    bw.Write(5, 0);// this.Cell.Row.Index);
                    bw.Write(6, 0);// this.Cell.Column.Index);
                    bw.Write(7, this._AutoGenerateColumns);// this.Cell.Column.Index);
                    bw.Write(8, this._PropertyCellEndEdit);
                    bw.Write(9, this._PropertyCellSelectChanged);
                    bw.Write(10, this._PropertyNodeSelectChanged);
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

        //public virtual void EndEdit()
        //{
        //    this.Grid.EndEdit();
        //}

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
            CellTreeView celledit = new CellTreeView(grid);
            celledit._backcolor = this._backcolor;
            celledit._font = this._font;
            celledit._forecolor = this._forecolor;
            celledit._hasChildEdit = this._hasChildEdit;
            return celledit;
        }
        public override void OnFocusedNodeChanged(DataTreeNode node)
        {
            try
            {
                if (node != null)
                {
                    ActionArgs e = new ActionArgs(PropertyNodeSelectChanged, this.Cell.Grid, this.Cell);
                    this.Cell.Grid.ExecuteAction(e, PropertyCellSelectChanged, "TreeViewNode", node);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "CellTreeView", "OnFocusedNodeChanged", ex);
            }
            base.OnFocusedNodeChanged(node); 
        }
        public override void EndEdit()
        {
            try
            {
                GridViewCell cell = this.EditCell;
                if (cell != null)
                {
                    ActionArgs e = new ActionArgs(PropertyNodeSelectChanged, this.Cell.Grid, this.Cell);
                    this.Cell.Grid.ExecuteAction(e, PropertyCellSelectChanged, "TreeViewCell", cell);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "CellTreeView", "EndEdit", ex);
            }
            base.EndEdit();
            if (this.Grid != null)
            {
                this.Grid.EndEdit();
            }
        }
        public override void OnFocusedCellChanged(GridViewCell cell)
        {
            //if (this.Cell != null)
            //{
            //    ActionArgs e = new ActionArgs(PropertyCellSelectChanged, this.Cell.Grid, this.Cell);
            //    this.Cell.Grid.ExecuteAction(e, PropertyCellSelectChanged, "GridViewCell", cell);
            //}
            base.OnFocusedCellChanged(cell);
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
        private CellTreeViewCellSelectChangedAction propertycellselectchangedaction = null;
        public virtual CellTreeViewCellSelectChangedAction CellSelectChangedAction
        {
            get
            {
                if (propertycellselectchangedaction == null)
                {
                    propertycellselectchangedaction = new CellTreeViewCellSelectChangedAction(this);
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
        private CellTreeViewCellEndEditAction cellendeditaction = null;
        public virtual CellTreeViewCellEndEditAction CellEndEditAction
        {
            get
            {
                if (cellendeditaction == null)
                {
                    cellendeditaction = new CellTreeViewCellEndEditAction(this);
                }
                return cellendeditaction;
            }
        }


        private string _PropertyNodeSelectChanged = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyNodeSelectChanged
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
        private CellTreeViewNodeSelectChangedAction propertynodeselectchangedaction = null;
        public virtual CellTreeViewNodeSelectChangedAction NodeSelectChangedAction
        {
            get
            {
                if (propertynodeselectchangedaction == null)
                {
                    propertynodeselectchangedaction = new CellTreeViewNodeSelectChangedAction(this);
                }
                return propertynodeselectchangedaction;
            }
        }

        public class CellTreeViewCellSelectChangedAction : PropertyAction
        {
            public CellTreeView EditView { get; set; }
            public CellTreeViewCellSelectChangedAction(CellTreeView celltreeview) : base()
            {
                EditView = celltreeview;
            }
            private string actionname = "CellSelectChanged";
            private string descript = "选中单元格改变";
            private string shortname = "CellSelectChanged";
            public override string ActionName { get { return actionname; } set { actionname = value; } }
            public override string Descript { get { return descript; } set { descript = value; } }
            public override string ShortName { get { return shortname; } set { shortname = value; } }
            public override string Script
            {
                get
                {
                    return EditView.PropertyCellSelectChanged;
                }
                set { EditView.PropertyCellSelectChanged = value; }
            }
        }

        public class CellTreeViewNodeSelectChangedAction : PropertyAction
        {
            public CellTreeView EditView { get; set; }
            public CellTreeViewNodeSelectChangedAction(CellTreeView celltreeview) : base()
            {
                EditView = celltreeview;
            }
            private string actionname = "NodeSelectChanged";
            private string descript = "选中节点改";
            private string shortname = "NodeSelectChanged";
            public override string ActionName { get { return actionname; } set { actionname = value; } }
            public override string Descript { get { return descript; } set { descript = value; } }
            public override string ShortName { get { return shortname; } set { shortname = value; } }
            public override string Script
            {
                get
                {
                    return EditView.PropertyNodeSelectChanged;
                }
                set { EditView.PropertyNodeSelectChanged = value; }
            }
        }

        public class CellTreeViewCellEndEditAction : PropertyAction
        {
            public CellTreeView EditView { get; set; }
            public CellTreeViewCellEndEditAction(CellTreeView celltreeview) : base()
            {
                EditView = celltreeview;
            }
            private string actionname = "CellEndEdit";
            private string descript = "内嵌表格单元结束编辑";
            private string shortname = "CellEndEdit";
            public override string ActionName { get { return actionname; } set { actionname = value; } }
            public override string Descript { get { return descript; } set { descript = value; } }
            public override string ShortName { get { return shortname; } set { shortname = value; } }
            public override string Script
            {
                get
                {
                    return EditView.PropertyCellEndEdit;
                }
                set { EditView.PropertyCellEndEdit = value; }
            }
        }

        public virtual List<PropertyAction> GetActiones()
        {
            List<PropertyAction> list = new List<PropertyAction>();
            list.Add(CellSelectChangedAction);
            list.Add(CellEndEditAction);
            list.Add(NodeSelectChangedAction);
            return list;
        }
    }

    public class CellTreeViewHScroll : HScrollerView
    {
        public CellTreeViewHScroll(CellTreeView grid)
        {
            _Grid = grid;
        }
        private CellTreeView _Grid = null;
        public CellTreeView Grid
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
                return Grid.Height - this.Height;
            }
            set
            {
            }
        }

        public override int Left
        {
            get
            {
                return 0;
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


    public class CellTreeViewVScroll : VScrollerView
    {
        public CellTreeViewVScroll(CellTreeView grid)
        {
            _Grid = grid;
        }
        private CellTreeView _Grid = null;
        public CellTreeView Grid
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
    }
}
