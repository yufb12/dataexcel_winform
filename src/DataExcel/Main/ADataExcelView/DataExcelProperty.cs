using Feng.App;
using Feng.Args;
using Feng.Collections;
using Feng.Commands;
using Feng.Data;
using Feng.Enums;
using Feng.Excel.App;
using Feng.Excel.Args;
using Feng.Excel.Base;
using Feng.Excel.Collections;
using Feng.Excel.Designer;
using Feng.Excel.Edits;
using Feng.Excel.Extend;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Excel.Print;
using Feng.Excel.Styles;
using Feng.Excel.Table;
using Feng.Excel.View;
using Feng.Forms;
using Feng.Forms.Base;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        private string id = string.Empty;
        public string ID { 
            get 
            {
                if (id == string.Empty)
                {
                    id = Guid.NewGuid().ToString();
                }
                return id;
            }
        }
        private DesignerChche designerdata = null;
        [Localizable(true)]
        [Browsable(true)]
        [Editor(typeof(DesignerChcheTypeEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public DesignerChche DesignerData
        {
            get
            {
                return designerdata;
            }
            set
            {
                designerdata = value;
                if (this.designerdata != null)
                {

                    try
                    {
                        this.Open(designerdata.DesignerData);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }
        //private bool designstate = false;
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool DesignState
        //{
        //    get
        //    { 
        //        return designstate;
        //    }
        //    set { designstate = value; }
        //}
        private ImageLayout __BackgroundImageLayout = ImageLayout.Center;
        private Image __BackgroundImage = null;

        private IBounds mousecaptureview = null;
        [Browsable(false)]
        [DefaultValue(null)]
        public virtual IBounds MouseCaptureView
        {
            get
            {
                return mousecaptureview;
            }
            set
            {
                mousecaptureview = value;
            }
        }

        public virtual ICell GetNullCell(int rowindex, int columnindex)
        {
            IRow row = this.Rows[rowindex];
            if (row == null)
            {
                return null;
            }
            ICell cell = row.Cells[columnindex]; 
            return cell;
        }

        public virtual ICell GetCell(int rowindex, int columnindex)
        {
            return this[rowindex, columnindex];
        }
        public virtual ICell FindCell(int rowindex, int columnindex)
        {
            IRow row = this.Rows[rowindex];
            if (row == null)
            {
                return null;
            }
            ICell cell = row.Cells[columnindex];
            if (cell != null)
            {
                if (cell.OwnMergeCell != null)
                {
                    return cell.OwnMergeCell;
                }
            }
            return cell;
        }
        public virtual bool IsNullCell(int rowindex, int columnindex)
        {
            IRow row = this.Rows[rowindex];
            if (row == null)
            {
                return true;
            }
            ICell cell = row.Cells[columnindex];
            if (cell == null)
            {
                return true;
            }
            return false;
        }
        public IRow GetRow(int index)
        {

            IRow row = this.Rows[index];
            if (row == null)
            {
                row = this.ClassFactory.CreateDefaultRow(this, index);
                this.Rows.Add(row);
            }
            return row;
        }

        public IRow FindRow(int index)
        {

            IRow row = this.Rows[index]; 
            return row;
        }
        public IColumn GetColumn(int index)
        {

            IColumn column = this.Columns[index];
            if (column == null)
            {
                column = this.ClassFactory.CreateDefaultColumn(this, index);
                this.Columns.Add(column);
            }
            return column;
        }
        public IColumn GetColumn(string name)
        {
            int index = DataExcel.GetColumnIndexByColumnHeader(name);
            IColumn column = this.Columns[index];
            if (column == null)
            {
                column = this.ClassFactory.CreateDefaultColumn(this, index);
                this.Columns.Add(column);
            }
            return column;
        }
        public IColumn FindColumnByID(string name)
        {
            foreach (IColumn item in this.Columns)
            {
                if (item.ID.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }

        public ICell FindCellByField(int rowindex,string fieldname)
        {
            IRow row = this.GetRow(rowindex);
            if (row == null)
                return null;
            foreach (IColumn item in this.Columns)
            {
                ICell cell = row.Cells[item];
                if (cell.FieldName.Equals(fieldname))
                {
                    return cell;
                }
            }
            return null;
        }

        public IColumn GetColumnByField(string fieldname, int rowindex)
        {
            IRow row = this.GetRow(rowindex);
            if (row == null)
                return null;
            foreach (IColumn item in this.Columns)
            {
                ICell cell = row.Cells[item];
                if (cell.FieldName.Equals(fieldname))
                {
                    return item;
                }
            }
            return null;
        }
        public ICell this[int rowindex, int columnindex]
        {
            get
            {

                IRow row = this.Rows[rowindex];
                if (row == null)
                {
                    row = this.ClassFactory.CreateDefaultRow(this, rowindex);
                    this.Rows.Add(row);
                }
                ICell cell = row.Cells[columnindex];
                if (cell == null)
                {
                    cell = this.ClassFactory.CreateDefaultCell(this, rowindex, columnindex);
                }
                return cell;

            }
        }

        public ICell this[int rowindex, string id]
        {
            get
            {
                IRow row = this.Rows[rowindex];
                if (row == null)
                {
                    row = this.ClassFactory.CreateDefaultRow(this, rowindex);
                    this.Rows.Add(row);
                } 
                ICell cell = null;
                foreach (IColumn column in this.Columns)
                {
                    if (column.ID == id)
                    { 
                        cell = row.Cells[column];
                        if (cell == null)
                        {
                            cell = this.ClassFactory.CreateDefaultCell(this, rowindex, column.Index);
                        }
                        return cell;
                    }
                }
        
                return cell;

            }
        }
        public ICell this[string cellname]
        {
            get
            {
                System.Text.RegularExpressions.MatchCollection mcs = System.Text.RegularExpressions.Regex.Matches(cellname, @"([a-zA-z]+)(\d+)");
                if (mcs.Count != 1)
                {
                    return GetCellByID(cellname);
                }
                int rowindex = int.Parse(mcs[0].Groups[2].Value);
                int columnindex = DataExcel.GetColumnIndexByColumnHeader(mcs[0].Groups[1].Value);
                return this[rowindex, columnindex];
            }
        }

        private IDCellCollection _dicCommandCell = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDCellCollection IDCells
        {
            get
            {
                if (_dicCommandCell == null)
                {
                    _dicCommandCell = new IDCellCollection();
                }
                return _dicCommandCell;
            }
        }


        private SelectRangeCollection selectrange = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectRangeCollection SelectRange
        {
            get
            {
                if (selectrange == null)
                {
                    selectrange = new SelectRangeCollection();
                }
                return selectrange;
            }
        }
        public ICell GetCellByNameAndID(string name)
        {
            ICell cell = this.GetCell(name);
            if (cell == null)
            {
                cell = GetCellByID(name);
            }
            return cell;
        }
        public ICell GetCellFromColumn(string columnid,string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            IColumn column = this.Columns[columnid] ;
            ICell cell = null;
            foreach (IRow item in this.Rows)
            {
                ICell cell2 = item.Cells[column];
                if (cell2 == null)
                    continue;
                string text = Feng.Utils.ConvertHelper.ToString(cell2.Value);
                if (value.Equals(text))
                {
                    cell = cell2;
                    break;
                }
                Feng.Utils.TraceHelper.WriteTrace("", "", "cell2.Value", true, text);
            }
            return cell;
        }
        public ICell GetCellFromColumn(string columnid,int startrowindex,int endrowindex, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            IColumn column = this.GetColumn(columnid);
            ICell cell = null;
            for  (int i=startrowindex;i<=endrowindex;i++)
            {
                IRow item = this.Rows[i];
                if (item == null)
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return this[i, column.Index];
                    }
                    continue;
                }
                ICell cell2 = item.Cells[column];
                if (cell2 == null)
                    continue;
                string text = Feng.Utils.ConvertHelper.ToString(cell2.Value);
                if (value.Equals(text))
                {
                    cell = cell2;
                }
                Feng.Utils.TraceHelper.WriteTrace("", "", "cell2.Value", true, text);
            }
            return cell;
        }
        public ICell GetCellByID(string id)
        {
            if (IDCells.Contains(id))
            {
                return IDCells[id];
            }
            return null;
        }
        public ICell FindCellByID(string id)
        {
            if (IDCells.Contains(id))
            {
                return IDCells[id];
            }
            return null;
        }
        public string GetCellText(string id, string defaultvalue)
        {
            ICell cell = GetCellByNameAndID(id);
            if (cell != null)
            {
                return cell.Text;
            }
            return defaultvalue;
        }
        public bool ExistID(string id)
        {
            ICell cell = GetCellByID(id);
            if (cell != null)
            {
                return true;
            }
            return false;
        }
        public string GetCellText(string name)
        {
            ICell cell = this.GetCellByNameAndID(name);
            if (cell != null)
            {
                return cell.Text;
            }
            return string.Empty;
        }
        public object GetCellValue(string name)
        {
            ICell cell = this.GetCellByNameAndID(name);
            if (cell != null)
            {
                return cell.Value;
            }
            return null;
        }

 
        public List<ICell> GetCellsByID(string id)
        {
            return IDCells.GetCells(id);
        }
        private FieldCellection _dicfieldcells = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FieldCellection FieldCells
        {
            get
            {
                if (_dicfieldcells == null)
                {
                    _dicfieldcells = new FieldCellection();
                }
                return _dicfieldcells;
            }
        }

        private ExpressionCellection _expressioncells = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ExpressionCellection ExpressionCells
        {
            get
            {
                if (_expressioncells == null)
                {
                    _expressioncells = new ExpressionCellection();
                }
                return _expressioncells;
            }
        }

        public ICell GetCellByField(string field)
        {
            foreach (ICell icell in this.FieldCells)
            {
                if (icell.FieldName == field)
                {
                    return icell;
                }
            }
            return null;
        }

        private Color _backcolor = Color.Empty;
        public override Color BackColor
        {
            get
            {
                return _backcolor;
            }
            set
            {

                _backcolor = value;
            }
        }

        private Font _defaultcellfont = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(null)]
        public Font DefaultCellFont
        {
            get
            {
                if (this._defaultcellfont == null)
                {
                    if (this.Font != null)
                    {
                        this._defaultcellfont = this.Font.Clone() as Font;
                    }
                }
                return this._defaultcellfont;
            }
            set
            {
                this._defaultcellfont = value;
            }
        }

        private bool _columnautowidth = true;
        [DefaultValue(true)]
        public virtual bool ColumnAutoWidth
        {
            get { return _columnautowidth; }
            set { _columnautowidth = value; }
        }

        private bool _rowautoheight = true;
        [DefaultValue(true)]
        [Category(CategorySetting.Design)]
        public virtual bool RowAutoHeight
        {
            get { return _rowautoheight; }
            set { _rowautoheight = value; }
        }

        private int _Frozencolumn = -1;
        [DefaultValue(-1)]
        [Category(CategorySetting.Design)]
        public int FrozenColumn
        {
            get { return _Frozencolumn; }
            set { _Frozencolumn = value; }
        }

        private int _frozenrow = -1;
        [DefaultValue(-1)]
        [Category(CategorySetting.Design)]
        public int FrozenRow
        {
            get { return _frozenrow; }
            set { _frozenrow = value; }
        }
        [Browsable(false)]
        public bool Frozen { get { return FrozenRow > 0 || FrozenColumn > 0; } }

        private bool _allowchangedsize = false;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(false)]
        public virtual bool AllowChangedSize
        {
            get { return _allowchangedsize; }
            set { _allowchangedsize = value; }
        }

        private bool _Allowchangedfirstdisplayrow = true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(true)]
        public virtual bool AllowChangedFirstDisplayRow
        {
            get { return _Allowchangedfirstdisplayrow; }
            set { _Allowchangedfirstdisplayrow = value; }
        }

        private bool _Allowchangedfirstdisplaycolumn = true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(true)]
        public virtual bool AllowChangedFirstDisplayColumn
        {
            get { return _Allowchangedfirstdisplaycolumn; }
            set { _Allowchangedfirstdisplaycolumn = value; }
        }

        private YesNoInhert _AllowInputExpress = YesNoInhert.Yes;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(YesNoInhert.Yes)]
        public virtual YesNoInhert AllowInputExpress
        {
            get { return _AllowInputExpress; }
            set { _AllowInputExpress = value; }
        }

        private bool _AllowSaveAs =  true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(true)]
        public virtual bool AllowSaveAs
        {
            get { return _AllowSaveAs; }
            set { _AllowSaveAs = value; }
        }

        private bool _selectchangborder = false;
        [DefaultValue(false)]
        public virtual bool SelectChangedBorder
        {
            get { return _selectchangborder; }
            set
            {
                if (_selectchangborder == value)
                {
                    return;
                }
                this.BeginReFresh();
                _selectchangborder = value;
                this.EndReFresh();
            }
        }
        [Browsable(false)]
        public override Rectangle Rect
        {
            get { return new Rectangle(0 , 0, this.Width, this.Height); }
        }
        [Browsable(false)]
        public Rectangle TopLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left, rectf.Top, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public Rectangle TopRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right - _SelectBorderWidth, rectf.Top, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public Rectangle BottomLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left, rectf.Bottom - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public Rectangle BottomRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right - _SelectBorderWidth, rectf.Bottom - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;


            }
        }
        [Browsable(false)]
        public Rectangle MidTop
        {
            get
            {
                Rectangle rectf = this.Rect;


                rectf = new Rectangle(
                 Feng.Utils.ConvertHelper.ToInt32(rectf.Left + rectf.Width / 2 - _SelectBorderWidth / 2),
                    rectf.Top,
                    _SelectBorderWidth,
                    _SelectBorderWidth);

                return rectf;


            }
        }
        [Browsable(false)]
        public Rectangle MidBottom
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left + rectf.Width / 2 - _SelectBorderWidth / 2, rectf.Bottom - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }
        [Browsable(false)]
        public Rectangle MidLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left, rectf.Top + rectf.Height / 2 - _SelectBorderWidth / 2, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }
        [Browsable(false)]
        public Rectangle MidRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right - _SelectBorderWidth, rectf.Top + rectf.Height / 2 - _SelectBorderWidth / 2, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IBounds TempSelectRect { get; set; }

        //public Rectangle WorkArea {
        //    get {
        //        return new Rectangle(this.LeftSideWidth, this.TopSideHeight, this.Width, this.Height);
        //    }
        //}


        //protected override ImeMode DefaultImeMode
        //{
        //    get
        //    {
        //        return ImeMode.On;
        //    }
        //}

        #region IReadOnly 成员
        private bool _readonly = false;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(false)]
        public override bool ReadOnly
        {
            get
            {
                return _readonly;
            }
            set
            {
                _readonly = value;
            }
        }

        #endregion


        private bool _showselectaddrect = true;
        [Browsable(true)]
        [DefaultValue(true)]
        public virtual bool ShowSelectAddRect
        {
            get
            {
                return _showselectaddrect;
            }
            set
            {
                _showselectaddrect = value;
            }
        }

        private bool _showfocusedcellborder = true;
        [Browsable(true)]
        [DefaultValue(true)]
        public virtual bool ShowFocusedCellBorder
        {
            get
            {
                return _showfocusedcellborder;
            }
            set
            {
                _showfocusedcellborder = value;
            }
        }


        private Color _showfocusedcellbordercolor = System.Drawing.Color.DeepSkyBlue;
        [Browsable(true)]
        [DefaultValue(typeof(Color), "DeepSkyBlue")]
        public virtual Color ShowFocusedCellBorderColor
        {
            get
            {
                return _showfocusedcellbordercolor;
            }
            set
            {
                _showfocusedcellbordercolor = value;
            }
        }

        //[NonSerialized]
        //private Objects _dicsobjs;
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Objects Objects
        //{
        //    get
        //    {
        //        return _dicsobjs;
        //    }
        //    set
        //    {
        //        _dicsobjs = value;
        //    }
        //}


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IRowCollection Rows
        {
            get
            {
                if (this._Rows == null)
                {
                    _Rows = new RowCollection(this);
                }
                return _Rows;
            }

            set { _Rows = value; }
        }
        [NonSerialized]
        private IRowCollection _Rows = null;


        [NonSerialized]
        private int _freshversion = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FreshVersion
        {
            get { return _freshversion; }
            set { _freshversion = value; }
        }
        private int _MouseWheelChangedValue = -1;
        [Browsable(true)]
        [DefaultValue(-1)]
        public int MouseWheelChangedValue
        {
            get { return _MouseWheelChangedValue; }
            set { _MouseWheelChangedValue = value; }
        }

        private IMergeCellCollection _Mergecells = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IMergeCellCollection MergeCells
        {
            get
            {
                if (_Mergecells == null)
                {
                    _Mergecells = new MergeCellCollection(this);
                }
                return _Mergecells;
            }

            set { _Mergecells = value; }
        }

        private IBackCellCollection _backcells = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IBackCellCollection BackCells
        {
            get
            {
                if (_backcells == null)
                {
                    _backcells = new BackCellCollection(this);
                }
                return _backcells;
            }

            set { _backcells = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IColumnCollection Columns
        {
            get
            {
                if (_Columns == null)
                {
                    _Columns = new ColumnCollection(this);
                }
                return _Columns;
            }

            set { _Columns = value; }
        }
        [NonSerialized]
        private IColumnCollection _Columns = null;

        [NonSerialized]
        private SelectItemCollection _SelectColumns = null;

        [Browsable(false)]
        [Category(CategorySetting.Design)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectItemCollection SelectColumns
        {
            get
            {
                if (_SelectColumns == null)
                {
                    _SelectColumns = new SelectItemCollection();
                }
                return this._SelectColumns;
            }
        }


        [NonSerialized]
        private SelectItemCollection _SelectRows = null;

        [Browsable(false)]
        [Category(CategorySetting.Design)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectItemCollection SelectRows
        {
            get
            {
                if (_SelectRows == null)
                {
                    _SelectRows = new SelectItemCollection();
                }
                return this._SelectRows;
            }
        }

        //[NonSerialized]
        //private IImageCellCollection _ImageCells = null;

        //[Browsable(false)]
        //[Category(Setting.PropertyCollection)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public IImageCellCollection ImageCells
        //{
        //    get { return this._ImageCells; }

        //    set { this._ImageCells = value; }
        //}

        //[NonSerialized]
        //private ITextCellCollection _TextCells = null;

        [NonSerialized]
        private IExtendCellCollection _ListExtendCells;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IExtendCellCollection ListExtendCells
        {
            get
            {
                if (_ListExtendCells == null)
                {
                    _ListExtendCells = new ExtendCellCollection(this);
                }
                return _ListExtendCells;
            }
            set { _ListExtendCells = value; }
        }

        private ICellEditControl _DefaultEdit = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellEditControl DefaultEdit
        {
            get
            {
                if (this._DefaultEdit == null)
                {
                    this._DefaultEdit = new CellEdit(this);
                }

                return this._DefaultEdit;
            }

            set { this._DefaultEdit = value; }
        }

        private IFunctionCell _FunctionCells = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IFunctionCell FunctionCells
        {
            get { return _FunctionCells; }
            set { _FunctionCells = value; }
        }

        //[NonSerialized]
        //private TopCell _TopCell = null;
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[Browsable(true)]
        //public TopCell TopCell
        //{
        //    get { return _TopCell; }
        //    set { _TopCell = value; }
        //}
        private System.Text.Encoding _Encoding = System.Text.Encoding.Default;

        private Graphics graphicscache = null;
        public override Graphics GetGraphics()
        {
            if (this.BackGroundMode)
            {
                return null;
            }
            //if (graphicscache == null)
            {  
                graphicscache =  base.GetGraphics();
            }
            return graphicscache;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Text.Encoding Encoding
        {
            get { return this._Encoding; }

            set { this._Encoding = value; }
        }
        private int _DefaultRowHeight = 20;//20
        private int _DefaultColumnWidth = 72;//72
        [Browsable(true), DefaultValue(20)]
        [Category(CategorySetting.Design)]
        public int DefaultRowHeight
        {
            get { return _DefaultRowHeight; }
            set
            {
                _DefaultRowHeight = value;
            }
        }
        [Browsable(true), DefaultValue(72)]
        [Category(CategorySetting.Design)]
        public int DefaultColumnWidth
        {
            get { return _DefaultColumnWidth; }
            set
            {
                _DefaultColumnWidth = value;
            }
        }


        [Browsable(false), DefaultValue(0)]
        [Category(CategorySetting.Design)]
        public int LeftSideWidth
        {
            get
            {
                int x = this.PaddingLeft;
                if (this.ShowVerticalRuler)
                {
                    if (this.VerticalRuler != null)
                    {
                        x = x + this.VerticalRuler.Width;
                    }
                }
                return x;
            }
        }


        [Browsable(false), DefaultValue(0)]
        [Category(CategorySetting.Design)]
        public int RightSideWidth
        {
            get
            {
                int x = this.PaddingRight;

                //if (this.VScroller.Visible)
                //{
                //    x = x + this.VScroller.Width;
                //}
                return x;
            }
        }

        [Browsable(false), DefaultValue(0)]
        [Category(CategorySetting.Design)]
        public int TopSideHeight
        {
            get
            {
                int x = this.PaddingTop;
                if (this.ShowHorizontalRuler)
                {
                    if (this.HorizontalRuler != null)
                    {
                        x = x + this.HorizontalRuler.Height;
                    }
                }
                return x;
            }
        }

        [Browsable(false), DefaultValue(0)]
        [Category(CategorySetting.Design)]
        public int BottomSideHeight
        {
            get
            {
                int x = this.PaddingBottom;
                //if (this.HScroller.Visible)
                //{
                //    x = x + this.HScroller.Height;
                //}
                return x;
            }
        }
        [Browsable(false)]
        public Rectangle ClientBounds
        {
            get
            {
                int left = this.ContentLeft;
                int top = this.ContentTop;
                int width = this.Width - left;
                int height = this.Height - top;
                return new Rectangle(
                    left,
                    top,
                    width,
                    height);
            }
        }
        private int _PaddingLeft = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int PaddingLeft
        {
            get { return _PaddingLeft; }
            set { _PaddingLeft = value; }
        }

        private int _PaddingTop = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int PaddingTop
        {
            get { return _PaddingTop; }
            set { _PaddingTop = value; }
        }
        private int _PaddingRight = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int PaddingRight
        {
            get { return _PaddingRight; }
            set { _PaddingRight = value; }
        }

        private int _PaddingBottom = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int PaddingBottom
        {
            get { return _PaddingBottom; }
            set { _PaddingBottom = value; }
        }

        [NonSerialized]
        private bool _IsInit = false;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsInit
        {
            get { return _IsInit; }
            set { _IsInit = value; }
        }

        [NonSerialized]
        private bool _Focused = false;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Focused
        {
            get { return _Focused; }
            set { _Focused = value; }
        }
        [NonSerialized]
        private MergeCellCollectionRect _MergeCellCollectionRect = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MergeCellCollectionRect MergeCellCollectionRect
        {
            get { return _MergeCellCollectionRect; }

            set { _MergeCellCollectionRect = value; }
        }

        [NonSerialized]
        private MergeCellCollectionAddRect _MergeCellCollectionAddRect = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MergeCellCollectionAddRect MergeCellCollectionAddRect
        {
            get { return _MergeCellCollectionAddRect; }

            set { _MergeCellCollectionAddRect = value; }
        }

        private SelectAddRectCollection _SelectAddRectCollection = null;

        [NonSerialized]
        private ISelectCellCollection _SelectCells = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISelectCellCollection SelectCells
        {
            get
            {
                return this._SelectCells;
            }

            set
            {
                this._SelectCells = value;
            }
        }

        [NonSerialized]
        private ISelectCellCollection _FunctionSelectCells = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISelectCellCollection FunctionSelectCells
        {
            get
            {
                return this._FunctionSelectCells;
            }

            set
            {
                this._FunctionSelectCells = value;


            }
        }

        //[NonSerialized]
        //private ISelectCellCollection _CellRange = null;
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public ISelectCellCollection CellRange
        //{
        //    get
        //    {
        //        return this._CellRange;
        //    }

        //    set
        //    {
        //        this._CellRange = value; 
        //    }
        //}

        [NonSerialized]
        private SelectCellCollection _displayarea = null;
        [DefaultValue(null)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectCellCollection DisplayArea
        {
            get
            {
                return _displayarea;
            }
            set
            {
                _displayarea = value;
            }
        }

        [NonSerialized]
        private IEdit _currentedit = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEdit CurrentEdit
        {
            get { return this._currentedit; }

            set { this._currentedit = value; }
        }

        [NonSerialized]
        private ICell _focusedcell = null;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public ICell FocusedCell
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _focusedcell; }
            set
            {
                if (this._focusedcell == value)
                {
                    return;
                }
                SetFocuseddCell(value);
            }
        }


        public bool IsSingleSelect
        {
            get
            {
                if (this.FocusedCell != null)
                {
                    if (this.SelectCells != null)
                    {
                        if (this.SelectCells.BeginCell == this.FocusedCell)
                        {
                            if (this.SelectCells.EndCell.MaxRowIndex == this.FocusedCell.MaxRowIndex && this.SelectCells.EndCell.MaxColumnIndex == this.FocusedCell.MaxColumnIndex)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        private bool _rowautosize = false;

        [DefaultValue(false)]
        [Category(CategorySetting.Design)]
        [Description("")]
        public virtual bool RowAutoSize
        {
            get { return this._rowautosize; }
            set { _rowautosize = value; }
        }

        [Browsable(false)]
        public System.Windows.Forms.Form ParentForm
        {
            get { return this.FindForm(); }
        }

        private float _FontSize = 12f;
        [Browsable(true), DefaultValue(12f)]
        [Category(CategorySetting.Design)]
        public float FontSize
        {
            get { return this._FontSize; }

            set { this._FontSize = value; }
        }


        //private Cursor _Defaultcursor = null;
        //[Browsable(true)]
        //[Category(CategorySetting.DataExcel)]
        //public new Cursor DefaultCursor
        //{
        //    get {
        //        if (_Defaultcursor == null)
        //        {
        //            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Feng.Excel.Properties.Resources.DataExcel1))
        //            {
        //                _Defaultcursor = new Cursor(ms);
        //            }
        //        }
        //        return _Defaultcursor;
        //    }
        //    set { _Defaultcursor = value; 
        //    }
        //}


        //private ICursorManage _CursorManage = null;
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public ICursorManage CursorManage
        //{
        //    get { return _CursorManage; }
        //    set { _CursorManage = value; }
        //}

        private IExtendCell _ExtendCell;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IExtendCell ExtendCell
        {
            get { return _ExtendCell; }
            set { _ExtendCell = value; }
        }
        private Point _MouseDownPoint = Point.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point MouseDownPoint
        {
            get { return _MouseDownPoint; }
            set { _MouseDownPoint = value; }
        }

        private Size _MouseDownsize = Size.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MouseDownSize
        {
            get { return _MouseDownsize; }
            set { _MouseDownsize = value; }
        }

        private SizeChangMode _SizeChangMode = SizeChangMode.Null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SizeChangMode SizeChangMode
        {
            get { return _SizeChangMode; }
            set { _SizeChangMode = value; }
        }

        [NonSerialized]
        int _Selectmode = SelectMode.Null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Selectmode
        {
            get { return _Selectmode; }
            set
            {
                _Selectmode = value;
            }
        }
        [NonSerialized]
        private IImageCell _CurrentSelectImageCell;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IImageCell CurrentSelectImageCell
        {
            get { return _CurrentSelectImageCell; }

            set { _CurrentSelectImageCell = value; }
        }

        private IClassFactory _IClassFactory = null;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IClassFactory ClassFactory
        {
            get
            {
                if (_IClassFactory == null)
                {
                    ClassFactory = new DefultClassFactory(this);
                }
                return _IClassFactory;
            }
            set { _IClassFactory = value; }
        }

        private IMethodCollection _Methods = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IMethodCollection Methods
        {
            get { return _Methods; }

            set { this._Methods = value; }
        }

        [Browsable(false)]
        public virtual bool InEdit
        {
            get
            {

                if (this.FocusedCell == null)
                {
                    return false;
                }
                return this.FocusedCell.InEdit;
            }
        }

        private bool _multiple = true;
        [Browsable(true)]
        [DefaultValue(true)]
        public virtual bool MultiSelect
        {
            get
            {
                return _multiple;
            }
            set
            {
                _multiple = value;
            }
        }


        private Color _GridLineColor = Color.Empty;
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color GridLineColor
        {
            get { return _GridLineColor; }
            set
            {
                _GridLineColor = value;
                if (value != Color.Empty)
                {
                    _GridLinePen = new Pen(_GridLineColor);
                }
                else
                {
                    _GridLinePen = new Pen(Color.FromArgb(192, 192, 192));
                }
            }
        }

        private Pen _GridLinePen = null;
        [Browsable(false)]
        public Pen GridLinePen
        {
            get
            {
                if (_GridLinePen == null)
                {
                    _GridLinePen = new Pen(Color.FromArgb(192, 192, 192));
                }
                return _GridLinePen;
            }
            set { _GridLinePen = value; }
        }


        #region IReadOnlyForeColor 成员
        private Color _ReadOnlyForeColor = Color.Gray;
        [DefaultValue(typeof(Color), "Gray")]
        public Color ReadOnlyForeColor
        {
            get
            {
                if (_ReadOnlyForeColor == Color.Empty)
                {
                    return this.ReadOnlyForeColor;
                }
                return _ReadOnlyForeColor;
            }
            set
            {
                _ReadOnlyForeColor = value;
            }
        }

        #endregion

        #region IReadOnlyBackColor 成员
        private Color _ReadOnlyBackColor = Color.Empty;
        [DefaultValue(typeof(Color), "Empty")]
        public Color ReadOnlyBackColor
        {
            get
            {
                if (_ReadOnlyBackColor == Color.Empty)
                {
                    return this.BackColor;
                }
                return _ReadOnlyBackColor;
            }
            set
            {
                _ReadOnlyBackColor = value;
            }
        }

        #endregion

        #region IFocusForeColor 成员

        private Color _FocusForeColor = Color.Empty;
        [DefaultValue(typeof(Color), "Empty")]
        public Color FocusForeColor
        {
            get
            {
                if (_FocusForeColor == Color.Empty)
                {
                    return this.ForeColor;
                }
                return _FocusForeColor;
            }
            set
            {
                _FocusForeColor = value;
            }
        }

        #endregion

        #region IFocusBackColor 成员

        private Color _FocusBackColor = Color.Empty;
        [DefaultValue(typeof(Color), "Empty")]
        public Color FocusBackColor
        {
            get
            {
                if (_FocusBackColor == Color.Empty)
                {
                    return this.BackColor;
                }
                return _FocusBackColor;
            }
            set
            {
                _FocusBackColor = value;
            }
        }

        #endregion

        private System.Drawing.Color _LineColor = System.Drawing.Color.LightSkyBlue;
        [DefaultValue(typeof(Color), "LightSkyBlue")]
        public System.Drawing.Color LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; }
        }
        private System.Drawing.Color _RowBackColor = System.Drawing.SystemColors.Window;
        [DefaultValue(typeof(Color), "Window")]
        public System.Drawing.Color RowBackColor
        {
            get { return _RowBackColor; }

            set
            {
                this._RowBackColor = value;
            }
        }
        private System.Drawing.Color _SelectBorderColor = Color.BlueViolet;
        [DefaultValue(typeof(Color), "BlueViolet")]
        public System.Drawing.Color SelectBorderColor
        {
            get { return _SelectBorderColor; }
            set { _SelectBorderColor = value; }
        }
        private int _SelectBorderWidth = 6;
        [Browsable(true)]
        [DefaultValue(6)]
        public virtual int SelectBorderWidth
        {
            get { return _SelectBorderWidth; }
            set { _SelectBorderWidth = value; }
        }

        private List<IRow> _allVisiblerows = new List<IRow>();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<IRow> AllVisibleRows
        {
            get { return _allVisiblerows; }
        }
        [Browsable(false)]
        public virtual int AllVisibleRowCount
        {
            get { return this._allVisiblerows.Count; }
        }

        private List<IColumn> _Alllistcolumns = new List<IColumn>();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<IColumn> AllVisibleColumns
        {
            get { return _Alllistcolumns; }
        }
        [Browsable(false)]
        public virtual int AllVisibleColumnCount
        {
            get { return this._Alllistcolumns.Count; }
        }

        private List<IRow> _Visiblerows = new List<IRow>();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<IRow> VisibleRows
        {
            get { return _Visiblerows; }
        }

        private List<IColumn> _Visiblecolumns = new List<IColumn>();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<IColumn> VisibleColumns
        {
            get { return _Visiblecolumns; }
        }
 
        private int _maxRow = int.MaxValue;
        [Browsable(true)]
        [DefaultValue(int.MaxValue)]
        public virtual int MaxRow
        {
            get { return _maxRow; }
            set
            {
                _maxRow = value; 
            }
        }
        public const int MAXCOLUMNINDEX = 256;
        private int _maxColumn = MAXCOLUMNINDEX;
        [Browsable(true)]
        [DefaultValue(MAXCOLUMNINDEX)]
        public virtual int MaxColumn
        {
            get { return _maxColumn; }
            set
            {
                if (value > MAXCOLUMNINDEX)
                {
                    return;
                }
                _maxColumn = value;
                this.ReFreshFirstDisplayColumnIndex();
                this.ClearSelect();
            }
        }


        private int _FirstDisplayedColumnIndex = 1;
        [Browsable(true)]
        [DefaultValue(1)]
        public virtual int FirstDisplayedColumnIndex
        {
            get
            {
                return _FirstDisplayedColumnIndex;
            }
            set
            {

                BeforeFirstDisplayColumnChangedArgs e = new BeforeFirstDisplayColumnChangedArgs(value);
                this.OnBeforeFirstDisplayColumnChanged(e);
                if (e.Cancel)
                {
                    return;
                }
                SetFirstColumnShowIndex(value);

            }
        }
        private int _contentwidth = -1;
        [Browsable(false)]
        [DefaultValue(-1)]
        public int ContentWidth
        {
            get
            {
                return _contentwidth;
            }
            set
            {
                _contentwidth = value;
            }
        }
        private int _contentheight = -1;
        [Browsable(false)]
        [DefaultValue(-1)]
        public int ContentHeight
        {
            get
            {
                return _contentheight;
            }
            set
            {
                _contentheight = value;
            }
        }

        private int _toprowheight = 0;
        [Browsable(false)]
        [DefaultValue(0)]
        public int RowHeaderHeight
        {
            get
            {
                return _toprowheight;
            }
        }
         
 
        private int __FirstDisplayedRowIndex = 1;
        private int _FirstDisplayedRowIndex 
        {
            get { 
                return __FirstDisplayedRowIndex; 
            } 
            set {
                if (value == 3)
                {

                }
                __FirstDisplayedRowIndex = value; 
            } 
        }
        [Browsable(true)]
        [DefaultValue(1)]
        public virtual int FirstDisplayedRowIndex
        {
            get { return _FirstDisplayedRowIndex; }
            set
            {

                BeforeFirstDisplayRowChangedArgs e = new BeforeFirstDisplayRowChangedArgs(value);
                this.OnBeforeFirstDisplayRowChanged(e);
                if (e.Cancel)
                {
                    return;
                }
                _FirstDisplayedRowIndex = value;
                SetFirstRowShowIndex(value);

            }
        }
        private int _EndDisplayedRowIndex = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int EndDisplayedRowIndex
        {
            get { return _EndDisplayedRowIndex; }
            set
            {
                int index = value;
                int height = 0;
                for (int i = index; i > 0; i--)
                {
                    IRow row = this.Rows[i];
                    if (row != null)
                    {
                        height = height + row.Height;
                    }
                }

            }
        }
        private int HScrollerHeight
        {
            get
            {
                //if (this.HScroller.Visible)
                //    return this.HScroller.Height;
                return 0;
            }
        }

        private int VScrollerWidth
        {
            get
            {
                //if (this.VScroller.Visible)
                //    return this.VScroller.Width;
                return 0;
            }
        }
        private int _EndDisplayedColumnIndex = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int EndDisplayedColumnIndex
        {
            get { return _EndDisplayedColumnIndex; }
        }


        private bool _showrowheader = true;
        private bool _showcolumnheader = true;
        private bool _showgridrowline = true;
        private bool _showgridcolumnline = true;
        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowRowHeader
        {
            get { return _showrowheader; }
            set
            {
                BeforeHeaderVisibleChangedArgs args = new BeforeHeaderVisibleChangedArgs(HeaderMode.RowHeader);
                if (this.BeforeHeaderVisibleChange != null)
                {
                    BeforeHeaderVisibleChange(this, args);
                }
                if (args.Cancel)
                {
                    return;
                }
                this.BeginReFresh();
                _showrowheader = value;
                this.ReFreshFirstDisplayColumnIndex();
                if (RowHeaderVisibleChanged != null)
                {
                    RowHeaderVisibleChanged(this, args);
                }
                this.EndReFresh();
            }

        }


        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowColumnHeader
        {
            get { return _showcolumnheader; }

            set
            {
                BeforeHeaderVisibleChangedArgs args = new BeforeHeaderVisibleChangedArgs(HeaderMode.ColumnHeader);
                if (this.BeforeHeaderVisibleChange != null)
                {
                    BeforeHeaderVisibleChange(this, args);
                }
                if (args.Cancel)
                {
                    return;
                }
                //this.BeginReFresh();
                _showcolumnheader = value;
                //this.ReFreshFirstDisplayRowIndex();
                //if (ColumnHeaderVisibleChanged != null)
                //{
                //    ColumnHeaderVisibleChanged(this, args);
                //}
                //this.EndReFresh();
            }
        }

        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowGridRowLine
        {
            get { return this._showgridrowline; }

            set
            {
                BeforeGridRowLineVisibleChangedArgs args = new BeforeGridRowLineVisibleChangedArgs();
                if (this.BeforeGridRowLineVisibleChanged != null)
                {
                    BeforeGridRowLineVisibleChanged(this, args);
                }
                if (args.Cancel)
                {
                    return;
                }
                this._showgridrowline = value;
                ReFresh();
                if (GridRowLineVisibleChanged != null)
                {
                    GridRowLineVisibleChanged(this, args);
                }
            }
        }

        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowGridColumnLine
        {
            get { return this._showgridcolumnline; }

            set
            {
                BeforeGridColumnLineVisibleChangedArgs args = new BeforeGridColumnLineVisibleChangedArgs();
                if (this.BeforeGridColumnLineVisibleChanged != null)
                {
                    BeforeGridColumnLineVisibleChanged(this, args);
                }
                if (args.Cancel)
                {
                    return;
                }
                this.BeginReFresh();
                this._showgridcolumnline = value;
                if (GridColumnLineVisibleChanged != null)
                {
                    GridColumnLineVisibleChanged(this, args);
                }
                this.EndReFresh();
            }
        }



        private bool _showborder = true;
        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowBorder
        {
            get
            {
                return _showborder;
            }
            set
            {
                _showborder = value;
                this.ReFresh();
            }
        }

        private bool _ShowMultipleCheckBox = false;
        [Browsable(true), DefaultValue(false)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowCheckBox
        {
            get
            {
                return _ShowMultipleCheckBox;
            }
            set
            {
                _ShowMultipleCheckBox = value;
                this.ReFresh();
            }
        }

        private Color _BorderColor = Color.Gray;
        [Browsable(true)]
        [Category(CategorySetting.PropertyUI)]
        [DefaultValue(typeof(Color), "Gray")]
        public virtual Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                this.ReFresh();
            }
        }

 
        [DefaultValue(null)]
        [Localizable(true)] 
        public virtual Image BackgroundImage { get; set; }
 
        [DefaultValue(ImageLayout.Tile)] 
        public virtual ImageLayout BackgroundImageLayout { get; set; }

        private bool _autoshowscroller = true;
        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool AutoShowScroller
        {
            get { return _autoshowscroller; }
            set { _autoshowscroller = value; }
        }

        //private VScroller _vScroller = null;
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public virtual VScroller VScroller
        //{
        //    get
        //    {
        //        if (_vScroller == null)
        //        {
        //            this._vScroller = new VScroller();
        //            //this.Controls.Add(_vScroller);
        //            this._vScroller.BringToFront();
        //            this._vScroller.Location = new Point(this.Right - this._vScroller.Width, 0);
        //            this._vScroller.Height = this.Height - this._vScroller.Width;
        //            this._vScroller.Minimum = 1;
        //            //this._vScroller.ValueChanged += new EventHelper.ValueChangedEventHandler(_vScroller_ValueChanged);
        //            //this._vScroller.UpArrowAreaClick += new EventHelper.ClickEventHandler(VScroller_UpArrowAreaClick);
        //            //this._vScroller.DownArrowAreaClick += new EventHelper.ClickEventHandler(VScroller_DownArrowAreaClick);
        //            //this._vScroller.Max = this.Rows.Max; 
        //            this._vScroller.VisibleChanged += new EventHandler(_vScroller_VisibleChanged);
        //            this._vScroller.ValueChanged += new EventHandler(VScroller_ValueChanged);
        //        }
        //        return _vScroller;
        //    }
        //    set { _vScroller = value; }
        //}



        //void _vScroller_VisibleChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        this.ScrollerSizeChanged();
        //    }
        //    catch (Exception ex)
        //    {
        //        Feng.Utils.ExceptionHelper.ShowError(ex);
        //    }

        //}

        //private HScroller _hScroller = null;
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public virtual HScroller HScroller
        //{
        //    get
        //    {
        //        if (_hScroller == null)
        //        {
        //            this._hScroller = new HScroller();
        //            //this.Controls.Add(_hScroller);
        //            this._hScroller.BringToFront();
        //            this._hScroller.Location = new Point(0, this.Height - this._hScroller.Height);
        //            this._hScroller.Width = this.Width - this._hScroller.Height;
        //            this._hScroller.Minimum = 1;
        //            this._hScroller.Maximum = 26;
        //            //this._hScroller.ValueChanged += new EventHelper.ValueChangedEventHandler(_hScroller_ValueChanged);
        //            //this._hScroller.UpArrowAreaClick += new EventHelper.ClickEventHandler(HScroller_UpArrowAreaClick);
        //            //this._hScroller.DownArrowAreaClick += new EventHelper.ClickEventHandler(HScroller_DownArrowAreaClick);
        //            //this._hScroller.Max = this.Columns.Max;
        //            this._hScroller.VisibleChanged += new EventHandler(_hScroller_VisibleChanged);
        //            this._hScroller.ValueChanged += new EventHandler(HScroller_ValueChanged);
        //        }
        //        return _hScroller;
        //    }
        //    set { _hScroller = value; }
        //}



        //void _hScroller_VisibleChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        this.ScrollerSizeChanged();
        //    }
        //    catch (Exception ex)
        //    {
        //        Feng.Utils.ExceptionHelper.ShowError(ex);
        //    }

        //}

        private short _SrollStep = 3;
        [Browsable(true)]
        [DefaultValue((ushort)3)]
        public virtual short ScrollStep
        {
            get { return _SrollStep; }

            set { this._SrollStep = value; }
        }

        private bool _AutoExecuteExpress = true;
        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool AutoExecuteExpress
        {
            get
            {
                return _AutoExecuteExpress;
            }
            set
            {
                _AutoExecuteExpress = value;
            }
        }

        private PenManage _penmanage = new PenManage();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual PenManage PenManage
        {
            get { return _penmanage; }
            set { _penmanage = value; }
        }

        //private Feng.Excel.ICellEditManage _ICellEditManage = new CellEditManage();
        //public Feng.Excel.ICellEditManage CellEditManage
        //{
        //    get { return _ICellEditManage; }
        //    set { _ICellEditManage = value; }
        //}

        [NonSerialized]
        private IViewEvent _ICellEvents = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IViewEvent CellEvent
        {
            get { return _ICellEvents; }
            set { _ICellEvents = value; }
        }

        [Browsable(false)]
        public IRow HeightChangedRow { get; set; }
        [Browsable(false)]
        public IColumn WidthChangedColumn { get; set; }

        private bool _showhorizontalruler = false;
        [Browsable(true), DefaultValue(false)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowHorizontalRuler
        {
            get
            {
                return _showhorizontalruler;
            }
            set
            { 
                _showhorizontalruler = value; 
            }
        }

        private bool _showverticalruler = false;
        [Browsable(true), DefaultValue(false)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowVerticalRuler
        {
            get
            {
                return _showverticalruler;
            }
            set
            {
                this.BeginReFresh();
                _showverticalruler = value;
                this.ReFreshFirstDisplayColumnIndex();
                this.EndReFresh();
            }
        }

        private bool _showverticalscroller = true;
        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowVerticalScroller
        {
            get { return _showverticalscroller; }
            set
            {
                this.BeginReFresh();
                _showverticalscroller = value;
                //this.VScroller.Visible = value;
                this.EndReFresh();
            }
        }

        private bool _showhorizontalscroller = true;
        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowHorizontalScroller
        {
            get { return _showhorizontalscroller; }
            set
            {
                this.BeginReFresh();
                _showhorizontalscroller = value;
                //this.HScroller.Visible = value;
                this.EndReFresh();
            }
        }

        private bool _showgridreticle = true;
        [Browsable(true), DefaultValue(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowReticle
        {
            get { return _showgridreticle; }
            set
            {
                this.BeginReFresh();
                _showgridreticle = value;
                this.EndReFresh();
            }
        }
        private float _figures = 2.54f;
        [Browsable(true), DefaultValue(2.54f)]
        public virtual float Figures
        {
            get { return _figures; }
            set
            {
                this.BeginReFresh();
                this.OnFiguresChanged(this, new FiguresEventArgs()
                {
                    NewValue = value,
                    OldValue = _figures
                });

                _figures = value;
                this.EndReFresh();
            }
        }

        private VerticalRuler _VerticalRuler;
        /// <summary>
        /// 垂直标尺
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual VerticalRuler VerticalRuler
        {
            get { return _VerticalRuler; }
            set { _VerticalRuler = value; }
        }

        private HorizontalRuler _HorizontalRuler;
        /// <summary>
        /// 水平标尺
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HorizontalRuler HorizontalRuler
        {
            get { return _HorizontalRuler; }
            set { _HorizontalRuler = value; }
        }

        private DataExcelViewVScroll _VScroll;
        /// <summary>
        /// 垂直标尺
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataExcelViewVScroll VScroll
        {
            get {
                if (_VScroll == null)
                {
                    _VScroll = new DataExcelViewVScroll(this);
                    _VScroll.Min = 1;
                    _VScroll.LargeChange = 20;
                }
                return _VScroll;
            }
            set { _VScroll = value; }
        }

        private DataExcelViewHScroll _HScroll;
        /// <summary>
        /// 水平标尺
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataExcelViewHScroll HScroll
        {
            get {
                if (_HScroll == null)
                {
                    _HScroll = new DataExcelViewHScroll(this);
                    _HScroll.Min = 1;
                }
                return _HScroll; }
            set { _HScroll = value; }
        }
        [NonSerialized]
        private ICell _MouseDownCell = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ICell MouseDownCell
        {
            get { return _MouseDownCell; }
            set
            {
                _MouseDownCell = value;
            }
        }

        [NonSerialized]
        private ICell _MouseOverCell = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ICell MouseOverCell
        {
            get { return _MouseOverCell; }
            set { _MouseOverCell = value; }
        }

        //[NonSerialized]
        //private CellMouseState _CellMouseState = CellMouseState.Null;
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public virtual CellMouseState CellMouseState
        //{
        //    get { return _CellMouseState; }
        //    set { _CellMouseState = value; }
        //}
        [NonSerialized]
        private PrintSetting _CurrentPrintSetting = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual PrintSetting CurrentPrintSetting
        {
            get
            {
                if (_CurrentPrintSetting == null)
                {
                    _CurrentPrintSetting = new PrintSetting(this);

                }
                return _CurrentPrintSetting;
            }

            set
            {
                _CurrentPrintSetting = value;
            }
        }

        internal MouseEventArgs _mheas = null;


        public virtual void Clone(DataExcel grid)
        {

        }
        [NonSerialized]
        private List<Pen> _FunctionCellDeafultColors = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<Pen> FunctionCellDeafultColors
        {
            get
            {
                if (_FunctionCellDeafultColors == null)
                {
                    _FunctionCellDeafultColors = new List<Pen>();
                    _FunctionCellDeafultColors.AddRange(new Pen[] { Pens.Blue, Pens.Green, Pens.Orchid, Pens.Purple, Pens.SlateBlue,
        Pens .RoyalBlue,Pens.DarkTurquoise,Pens .LawnGreen});
                }
                return _FunctionCellDeafultColors;
            }
            set { _FunctionCellDeafultColors = value; }
        }

        [Browsable(true)]
        [DebuggerHidden(), EditorBrowsable(EditorBrowsableState.Never)]
        [Editor(typeof(AboutDesigner), typeof(UITypeEditor))]
        public string User
        {
            get
            {
                return DataExcel.GetUser();
            }
        }
        [Browsable(true)]
        [DebuggerHidden(), EditorBrowsable(EditorBrowsableState.Never)]
        [Editor(typeof(HomePageDesigner), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HomePage
        {
            get
            {

                return Product.AssemblyHomePage;
            }
        }

        private string _password = string.Empty;
        [DefaultValue("")]
        [Browsable(true)]
        public virtual string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }
        private string _filename = string.Empty;
        [DefaultValue("")]
        public virtual string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        [Browsable(false)]
        public virtual string FilePath { get; set; }

        private string _title = string.Empty;
        [DefaultValue("")]
        public virtual string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private TabList _tablist = null;
        [Browsable(false)]
        [DebuggerHidden(), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual TabList TabList
        {
            get
            {
                if (_tablist == null)
                {
                    _tablist = new TabList();
                }
                return _tablist;
            }
        }

        private BookMarkList _marklist = null;
        [Browsable(false)]
        public virtual BookMarkList BookMarkList
        {
            get
            {
                if (_marklist == null)
                {
                    _marklist = new BookMarkList();
                }
                return _marklist;
            }

        }


        private FocsedCellList _focusedlist = null;
        [Browsable(false)]
        public virtual FocsedCellList FocsedCellList
        {
            get
            {
                if (_focusedlist == null)
                {
                    _focusedlist = new FocsedCellList();
                }
                return _focusedlist;
            }

        }

        private HotKeyList _hotkeylist = null;
        [Browsable(false)]
        [DebuggerHidden(), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual HotKeyList HotKeyList
        {
            get
            {
                if (_hotkeylist == null)
                {
                    _hotkeylist = new HotKeyList();
                }
                return _hotkeylist;
            }
        }



        [NonSerialized]
        private List<ISelected> _Selecteds = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public virtual List<ISelected> Selecteds
        {
            get
            {
                if (_Selecteds == null)
                {
                    _Selecteds = new List<ISelected>();
                }
                return _Selecteds;
            }
            set { _Selecteds = value; }
        }

        [NonSerialized]
        private List<ISortOrder> _ISortOrders = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public virtual List<ISortOrder> SortOrders
        {
            get
            {
                if (_ISortOrders == null)
                {
                    _ISortOrders = new List<ISortOrder>();
                }
                return _ISortOrders;
            }
            set { _ISortOrders = value; }
        }

        public virtual void ClearOrder()
        {
            if (_ISortOrders != null)
            {
                foreach (ISortOrder order in _ISortOrders)
                {
                    order.Order = Feng.Forms.ComponentModel.SortOrder.Null;
                }
            }
        }

        #region IAllowAdd 成员
        private bool _Allowadd = true;
        [DefaultValue(true)]
        public virtual bool AllowAdd
        {
            get
            {
                return _Allowadd;
            }
            set
            {
                _Allowadd = value;
                refreshmaxrow();
            }
        }
        private void refreshmaxrow()
        {
            if (this.DataSource == null)
            {
                return;
            }
            int count = GetBingingDataSourceCount();
            if (this.AllowAdd)
            {
                this.MaxRow = count + 1;
            }
            else
            {
                this.MaxRow = count;
            }
        }
        #endregion

        #region IAllowDelete 成员
        private bool _Allowdelete = true;
        [DefaultValue(true)]
        public virtual bool AllowDelete
        {
            get
            {
                return _Allowdelete;
            }
            set
            {
                _Allowdelete = value;
            }
        }

        #endregion

        #region IAllowEdit 成员
        private bool _allowedit = true;
        [DefaultValue(true)]
        public virtual bool AllowEdit
        {
            get
            {
                return _allowedit;
            }
            set
            {
                _allowedit = value;
            }
        }

        #endregion


        #region IShowSelectBorder 成员
        private bool _ShowSelectBorder = true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [DefaultValue(true)]
        public virtual bool ShowSelectBorder
        {
            get
            {
                return _ShowSelectBorder;
            }
            set
            {
                _ShowSelectBorder = value;
            }
        }

        #endregion

        private bool _allowFullrowselect = true;
        [DefaultValue(true)]
        public virtual bool AllowFullRowSelect
        {
            get
            {
                return _allowFullrowselect;
            }
            set
            {
                _allowFullrowselect = value;
            }
        }

        private bool _allowFullcolumnselect = true;
        [DefaultValue(true)]
        public virtual bool AllowFullColumnSelect
        {
            get
            {
                return _allowFullcolumnselect;
            }
            set
            {
                _allowFullcolumnselect = value;
            }
        }

        private CommandStackt _commands = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CommandStackt Commands
        {
            get
            {
                if (_commands == null)
                {
                    _commands = new CommandStackt();
                }
                return _commands;
            }
        }

        private bool _AutoGenerateColumns = true;
        [Browsable(true)]
        [DefaultValue(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AutoGenerateColumns
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


        private bool _AutoGenerateRows = true;
        [Browsable(true)]
        [DefaultValue(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AutoGenerateRows
        {
            get
            {
                return _AutoGenerateRows;
            }
            set
            {
                _AutoGenerateRows = value;
            }
        }

        [NonSerialized]
        private List<IViewEvent> _MousesMoveEvents = new List<IViewEvent>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<IViewEvent> MousesMoveEvents
        {
            get
            {
                return _MousesMoveEvents;
            }
            set
            {
                _MousesMoveEvents = value;
            }
        }

        [NonSerialized]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CellChangedCollection CellChangeds = null;

        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public CellChangedCollection CellChangeds
        //{
        //    get
        //    {
        //        if (_CellChangeds == null)
        //        {
        //            _CellChangeds = new CellChangedCollection(this);
        //        }
        //        return _CellChangeds;
        //    }
        //}

        private bool _canundoredo = true;
        [DefaultValue(true)]
        public virtual bool CanUndoRedo
        {
            get
            {
                return _canundoredo;
            }
            set
            {
                _canundoredo = value;
            }
        }

        private bool _designmode = false;
        [DefaultValue(false)]
        public virtual bool InDesign
        {
            get
            {
                return _designmode;
            }
            set
            {
                _designmode = value;
            }
        }

        private bool allowinputcode = true;
        [DefaultValue(false)]
        public virtual bool AlliowInputCode
        {
            get
            {
                return allowinputcode;
            }
            set
            {
                allowinputcode = value;
            }
        }
        private System.Windows.Forms.FormBorderStyle _formborderstyle = FormBorderStyle.Sizable;
        [DefaultValue(FormBorderStyle.Sizable)]
        public virtual System.Windows.Forms.FormBorderStyle FormBorderStyle
        {
            get
            {
                return _formborderstyle;
            }
            set
            {
                _formborderstyle = value;
            }
        }

        private string _fileid = string.Empty;
        [DefaultValue("")]
        [Editor(typeof(FileIDDesigner), typeof(UITypeEditor))]
        [Browsable(false)]
        public string FileID
        {
            get
            {
                return _fileid;
            }
            set { _fileid = value; }
        }


        private string _assemblyfileversion = string.Empty;
        [DefaultValue("")] 
        [Browsable(true)]
        public string AssemblyFileVersion
        {
            get
            {
                return _assemblyfileversion;
            } 
        }

        private int _AutoSizeVisibleColumns = 0;

        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(0)]
        public int AutoSizeVisibleColumns
        {
            get { return _AutoSizeVisibleColumns; }
            set { _AutoSizeVisibleColumns = value; }
        }

        private static DataExcel _dataexcel = null;
        public static DataExcel MainExcel
        {
            get
            {
                return _dataexcel;
            }
        }

        private int _AutoSizeVisibleRows = 0;

        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(0)]
        public int AutoSizeVisibleRows
        {
            get { return _AutoSizeVisibleRows; }
            set { _AutoSizeVisibleRows = value; }
        }

        private bool _AutoSize = false;

        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(false)]
        public virtual bool AutoSize
        {
            get
            {
                return _AutoSize;
            }
            set
            {
                _AutoSize = value;
                if (_AutoSize)
                {
                    this.AutoSizeVisibleRows = this.VisibleRows.Count;
                    this.AutoSizeVisibleColumns = this.VisibleColumns.Count;
                }
            }
        }

        private int _zoom = 1;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(1)]
        [Browsable(false)]
        public int Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                this.BeginReFresh();
                _zoom = value;
                this.EndReFresh();
            }
        }
        private UpdateInfo _UpdateInfo = null;
        [DebuggerHidden()]
        [Editor(typeof(UpdateInfoDesigner), typeof(UITypeEditor))]
        [Browsable(false)]
        public UpdateInfo UpdateInfo
        {
            get
            {
                if (_UpdateInfo == null)
                    _UpdateInfo = new UpdateInfo()
                    {
                        UpdataUrl = string.Empty,
                        UpdateMode = 0,
                        Version = new Feng.App.Version()
                        {
                            First = 1,
                            Second = 0,
                            Third = 0,
                            Fouth = 1
                        }
                    };
                return _UpdateInfo;
            }
        }
        private Cursor _Cursor = null;
        public virtual Cursor Cursor
        {
            get { return _Cursor; }
            set { this._Cursor = value; }
        }


        private TagCollection _tags = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public TagCollection Tags
        {
            get
            {
                if (_tags == null)
                {
                    _tags = new TagCollection();
                }
                return this._tags;
            } 
        }
#if DEBUG2
        public event Feng.Excel.DoTest DoTested;
#endif
        #region DragDrop
        [NonSerialized]
        private SelectCellCollection _dragdropcell = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SelectCellCollection DragDropCells
        {
            get { return _dragdropcell; }
            set
            {
                _dragdropcell = value;
            }
        }
        [NonSerialized]
        private bool _drawdragdropcell = false;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool DrawDragDropCell
        {
            get { return _drawdragdropcell; }
            set
            {
                _drawdragdropcell = value;
            }
        }

        #endregion


        [NonSerialized]
        private CellEditCollection _cellsaveedits = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CellEditCollection CellSaveEdits
        {
            get
            {
                if (_cellsaveedits == null)
                    _cellsaveedits = new CellEditCollection();
                return _cellsaveedits;
            }
            set
            {
                _cellsaveedits = value;
            }
        }

        [NonSerialized]
        private CellEditCollection _celledits = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual CellEditCollection CellEdits
        {
            get
            {
                if (_celledits == null)
                    _celledits = new CellEditCollection();
                return _celledits;
            }
            set
            {
                _celledits = value;
            }
        }

        [NonSerialized]
        private Dictionary<string, DataStruct> _ExtensData = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Dictionary<string, DataStruct> UserDefineExtensData
        {
            get
            {
                if (_ExtensData == null)
                    _ExtensData = new Dictionary<string, DataStruct>();
                return _ExtensData;
            }
        }

        private ICell _EditCell = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICell EditCell
        {
            get { return _EditCell; }
            set { _EditCell = value; }
        }


        [NonSerialized]
        private EditControlCollection edits = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditControlCollection Edits
        {
            get
            {
                if (edits == null)
                {
                    edits = new EditControlCollection();
                }
                return edits;
            }
        }

        private string _code = string.Empty;
        [Browsable(false)]
        [DefaultValue(Feng.Utils.Constants.EmptyText)]
        public virtual string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        [Browsable(true), ReadOnly(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual string Version
        {
            get
            {
                return Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
            }
        }

        private int tooltipshowtime = 5;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(5)]
        public virtual int ToolTipShowTime
        {
            get
            {
                return tooltipshowtime;
            }
            set
            {
                this.tooltipshowtime = value;
            }
        }

        private string tooltip = string.Empty;
        [Browsable(false)]
        [DefaultValue(Feng.Utils.Constants.EmptyText)]
        public virtual string ToolTip
        {
            get
            {
                return tooltip;
            }
            set
            {
                this.tooltip = value;
            }
        }


        private bool tooltipVisible = false;
        [Browsable(false)]
        [DefaultValue(false)] 
        public virtual bool ToolTipVisible
        {
            get
            {
                return tooltipVisible;
            }
            set
            {
                this.tooltipVisible = value;
            }
        }


        private bool fileeditmode = false;
        [Browsable(false)]
        [DefaultValue(false)]
        public virtual bool FileEditMode
        {
            get
            {
                return fileeditmode;
            }
            set
            {
                this.fileeditmode = value;
            }
        }



        private int maxhasvaluecolumn= 1;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(5)]
        public virtual int MaxHasValueColumn
        {
            get
            {
                return maxhasvaluecolumn;
            }
            set
            {
                this.maxhasvaluecolumn = value;
            }
        }

        private DateTime _createtime = DateTime.Now;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DateTime CreateTime
        {
            get
            {
                return _createtime;
            }
        }


        private DateTime _updatetime = DateTime.Now;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DateTime UpdateTime
        {
            get
            {
                return _updatetime;
            }
        }


        private string _createuser = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string CreateUser
        {
            get
            {
                return _createuser;
            }
        }

        private string _createusername = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string CreateUserName
        {
            get
            {
                return _createusername;
            }
        }


        private string _updateuser = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string UpdateUser
        {
            get
            {
                return _updateuser;
            }
        }

        private string _updateusername = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string UpdateUserName
        {
            get
            {
                return _updateusername;
            }
        }


        private int _updatetimes = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int UpdateTimes
        {
            get
            {
                return _updatetimes;
            }
        }


        private string _url = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyData)]
        [DefaultValue("")]
        public string URL
        {
            get
            {
                return _url;
            }
        }


        private Feng.Collections.HashtableEx hash = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("")]
        public Feng.Collections.HashtableEx Hash
        {
            get
            {
                if (hash == null)
                {
                    hash = new HashtableEx();
                }
                return hash;
            }
        }


        private Feng.Collections.HashtableEx _FunctionList = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("")]
        public Feng.Collections.HashtableEx FunctionList
        {
            get
            {
                if (_FunctionList == null)
                {
                    _FunctionList = new HashtableEx();
                }
                return _FunctionList;
            }
        }



        private CellDataBase _CellDataBase = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("")]
        public CellDataBase CellDataBase
        {
            get
            {
                if (_CellDataBase == null)
                {
                    _CellDataBase = new CellDataBase(this);
                }
                return _CellDataBase;
            }
        }


        private bool _showendrow = true;
        public bool ShowEndRow
        {
            get
            {
                if (this.FilterExcel != null)
                {
                    if (this.FilterExcel.FilterRows.Count > 0)
                    {
                        return false;
                    }
                }
                return this._showendrow;
            }
            set
            {
                this._showendrow = value;
            }
        }

        public IRow EndRow { get; set; }

        private object _tag= null;
        [Browsable(false)] 
        public virtual object Tag
        {
            get
            { 
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
 
        public string UserID { get; set; }
        public string UserName { get; set; }

        private int _opencount = 0;
        protected int OpenCount
        { 
            get { return _opencount; } 
            set { _opencount = value; } 
        }

        private EnterAccount _enterAccount = null;
        public EnterAccount EnterAccount { get { return _enterAccount; } set { _enterAccount = value; } }


        private Fillter.FilterExcel _filterexcel = null;
        public Fillter.FilterExcel FilterExcel { get { return _filterexcel; } set { _filterexcel = value; } }
 
    }
}
