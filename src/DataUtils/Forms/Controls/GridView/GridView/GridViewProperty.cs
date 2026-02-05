using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Interface;
using Feng.Forms.Views;

namespace Feng.Forms.Controls.GridControl
{
    public partial class GridView : DivView, Feng.Forms.Interface.IDataStruct,
        Feng.Print.IPrint, IShowCheck,IPointToControl
    { 

        #region 属性
        public GridViewCell EditCell
        {
            get;
            set;
        }
        public Feng.Forms.Interface.IEditControl Edit
        {
            get;
            set;
        }
        private Control mcontrol = null;
        [Browsable(false)]
        public virtual Control Control
        {
            get
            {
                return mcontrol;
            }
        }

        private Footer _footer = null;
        [Browsable(false)]
        public virtual Footer Footer
        {
            get
            {
                if (_footer == null)
                {
                    _footer = new Footer(this);
                }
                return _footer;
            }
        }

        #region Scroll
        private HScrollerView mhscroll = null;
        [Browsable(false)]
        public virtual HScrollerView HScroll
        {
            get
            {
                if (mhscroll == null)
                {
                    mhscroll = new HScrollerView();
                    mhscroll.Width = this.Width - mhscroll.Header;
                    mhscroll.Top = this.Height - mhscroll.Height;
                }
                return mhscroll;
            }
        }

        private VScrollerView mvscroll = null;
        [Browsable(false)]
        public virtual VScrollerView VScroll
        {
            get
            {
                if (mvscroll == null)
                {
                    mvscroll = new VScrollerView();
                    mvscroll.Height = this.Height - mvscroll.Header;
                    mvscroll.Left = this.Width - mvscroll.Width;
                }
                return mvscroll;
            }
        }
        
        #endregion

        #region IBounds 成员
 

        private int _position = 0;
        [Browsable(true)]
        [Category(CategorySetting.PropertyData)]
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                int position = value;
                if (position < 0)
                {
                    position = 0;
                }
                SetPosition(position);
                this.VScroll.Value = position;
                
            }
        }

        public void SetPosition(int value)
        {
            _position = value;
            int endposition = Count - this.Rows.Count+ 1;
            if (_position > 0)
            {
                if (_position > endposition)
                {
                    _position = endposition;
                }
            }
            if (_position < 0)
            {
                _position = 0;
            }
            RefreshRowValue();
            RefreshRowHeight();
        }

        private int _firstcolumn = 0;
        [Browsable(true)]
        [Category(CategorySetting.PropertyData)]
        public int FirstColumn
        {
            get
            {
                return _firstcolumn;
            }
            set
            {
                SetFirstColumn(value);
                this.HScroll.Value = _firstcolumn;
            }
        }

        public void SetFirstColumn(int value)
        {
            if (value < this.Columns.Count)
            {
                _firstcolumn = value;
            }
        }


        private int _Frozencolumn = -1;
        [DefaultValue(-1)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyData)]
        public int FrozenColumn
        {
            get { return _Frozencolumn; }
            set { _Frozencolumn = value; }
        }



        private bool _RightButtonClickSelect = false;
        [DefaultValue(false)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public bool RightButtonClickSelect
        {
            get { return _RightButtonClickSelect; }
            set { _RightButtonClickSelect = value; }
        }

        private int _frozenrow = -1;
        [DefaultValue(-1)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyData)]
        public int FrozenRow
        {
            get { return _frozenrow; }
            set { _frozenrow = value; }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyData)]
        public virtual int Count
        {
            get
            {
                if (this.DataSource is DataSet)
                {
                    if (string.IsNullOrEmpty(this._datamember))
                    {
                        return (((DataSet)this.DataSource).Tables[0]).Rows.Count; ;
                    }
                    else
                    {
                        return (((DataSet)this.DataSource).Tables[this._datamember]).Rows.Count; ;
                    }
                }
                else if (this.DataSource is DataTable)
                {
                    return ((DataTable)this.DataSource).Rows.Count;
                }
                else if (IsIlistDataSource(this.DataSource))
                {
                    return (this.DataSource as System.Collections.IList).Count;
                }
                return 0;
            }

        }
        #endregion

 

        #region datasource
        private object _DataSource = null;
        [AttributeProvider(typeof(IListSource))]
        [DefaultValue(null)]
        [Category(CategorySetting.DataBingding)]
        public virtual object DataSource
        {
            get { return this._DataSource; }
            set
            {
                _DataSource = value;
                InitDataSource();
            }
        }
        private GridViewCell _focusedcell = null;
        [Browsable(false)]
        public virtual GridViewCell FocusedCell
        {
            get
            {
                return _focusedcell;
            }
            set
            {
                _focusedcell = value;
            }
        }
        private SelectCellCollection _selectcells = null;
        [Browsable(false)]
        public SelectCellCollection SelectCells
        {
            get
            {
                if (_selectcells == null)
                {
                    _selectcells = new SelectCellCollection();
                }
                return _selectcells;
            }
            set
            {
                _selectcells = value;
            }
        }
        [NonSerialized]
        private Feng.Forms.ComponentModel.SortInfo _SortInfo = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Feng.Forms.ComponentModel.SortInfo SortInfo
        {
            get
            {
                return _SortInfo;
            }
            set
            {
                _SortInfo = value;
            }
        }
        [NonSerialized]
        private Feng.Forms.ComponentModel.FilterInfo _filterinfo = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Feng.Forms.ComponentModel.FilterInfo FilterInfo
        {
            get
            {
                return _filterinfo;
            }
            set
            {
                _filterinfo = value;
            }
        }

        private bool _AutoGenerateColumns = true;
        [Browsable(true)]
        [DefaultValue(true)]
        [Category(CategorySetting.PropertyData)]
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

        private string _datamember = string.Empty;
        [DefaultValue("")]
        [Browsable(true)]
        [Category(CategorySetting.PropertyData)]
        public virtual string DataMember
        {
            get
            {
                return _datamember;
            }
            set
            {
                _datamember = value;
                InitDataSource();
            }
        }

        private int _rowheight = 23;
        [Category(CategorySetting.PropertyUI)]
        public virtual int RowHeight
        {
            get
            {
                return this._rowheight;
            }
            set
            {
                this._rowheight = value;
            }
        }


        private int _columnwidth = 72;
        [Category(CategorySetting.PropertyUI)]
        public virtual int ColumnWidth
        {
            get
            {
                return this._columnwidth;
            }
            set
            {
                this._columnwidth = value;
            }
        }

        private int _rowheaderwidth = 40;
        [Category(CategorySetting.PropertyUI)]
        public virtual int RowHeaderWidth
        {
            get
            {
                return this._rowheaderwidth; 
            }
            set
            {
                this._rowheaderwidth = value;
            }
        }
        public int DefaultLeftSpace = 4;
        public virtual int LeftSapce {
            get {
                if (this.ShowRowHeader)
                {
                    return this.RowHeaderWidth;
                }
                return DefaultLeftSpace;
            }
        }
        public int DefaultTopSpace = 4;
        public virtual int TopSapce
        {
            get
            {
                if (this.ShowColumnHeader)
                {
                    return this.ColumnHeaderHeight;
                }
                return DefaultTopSpace;
            }
        }
        private int _columnheaderheight = 36;
        [Category(CategorySetting.PropertyUI)]
        public virtual int ColumnHeaderHeight
        {
            get
            {
                return this._columnheaderheight; 
            }
            set
            {
                this._columnheaderheight = value;
            }
        }

        private Pen _cellselectpen = null;
        [Browsable(false)]
        public virtual Pen CellSelectPen
        {
            get
            {
                if (_cellselectpen == null)
                {
                    _cellselectpen = new Pen(Color.FromArgb(100, Color.Gray));
                }
                return _cellselectpen;
            }
        }

        private Brush _cellselectbrush = null;
        [Browsable(false)]
        public virtual Brush CellSelectBrush
        {
            get
            {
                if (_cellselectbrush == null)
                {
                    _cellselectbrush = new SolidBrush(Color.FromArgb(100, Color.Gray));
                }
                return _cellselectbrush;
            }
        }
        #region IEditMode 成员
        private EditMode _EditMode = EditMode.Default;
        [DefaultValue(EditMode.KeyDown)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual EditMode EditMode
        {
            get
            {
                return _EditMode;
            }
            set
            {
                _EditMode = value;
            }
        }

        #endregion
        #endregion

        private bool _showcolumndesign = false;
        [Browsable(true)]
        [Category(CategorySetting.PropertyUI)]
        [DefaultValue(false)]
        public virtual bool InDesign
        {
            get
            {
                return _showcolumndesign;
            }
            set
            {
                _showcolumndesign = value;
            }
        }


        private bool _showcolumnheader = true;
        [Browsable(true)]
        [Category(CategorySetting.PropertyUI)]
        [DefaultValue(true)]
        public virtual bool ShowColumnHeader
        {
            get
            {
                return _showcolumnheader;
            }
            set
            {
                _showcolumnheader = value;
            }
        }

        private bool _showrowheader = true;
        [Browsable(true)]
        [Category(CategorySetting.PropertyUI)]
        [DefaultValue(true)]
        public virtual bool ShowRowHeader
        {
            get
            {
                return _showrowheader;
            }
            set
            {
                _showrowheader = value;
            }
        }

        private bool _showfooter = true;
        [Browsable(true)]
        [Category(CategorySetting.PropertyUI)]
        [DefaultValue(true)]
        public virtual bool ShowFooter
        {
            get
            {
                return _showfooter;
            }
            set
            {
                _showfooter = value;
            }
        }

        public virtual Rectangle OperationRect
        {
            get
            {
                return new Rectangle(this.Width - 38, 4, 16, 16);
            }
        }

        private Feng.Forms.Interface.IEditView _DefaultEdit = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Feng.Forms.Interface.IEditView DefaultEdit
        {
            get
            {
                if (this._DefaultEdit == null)
                {
                    this._DefaultEdit = new CellTextBoxEdit();
                }

                return this._DefaultEdit;
            }

            set { this._DefaultEdit = value; }
        }

        private ColumnCollection _Columns = null;
        [Browsable(false)]
        public virtual ColumnCollection Columns
        {
            get
            {
                if (_Columns == null)
                {
                    _Columns = new ColumnCollection(this);
                }
                return _Columns;
            }
        }

        private RowCollection _Rows = null;
        [Browsable(false)]
        public virtual RowCollection Rows
        {
            get
            {
                if (_Rows == null)
                {
                    _Rows = new RowCollection();
                }
                return _Rows;
            }
        }

        private short _SrollStep = 1;
        [DefaultValue(1)]
        [Browsable(true)]
        [Category(CategorySetting.PropertyUI)] 
        public virtual short ScrollStep
        {
            get { return _SrollStep; }

            set { this._SrollStep = value; }
        }
        public virtual Point PointToClient(Point pt)
        {
            return pt;
        }
        public virtual Point PointToScreen(Point pt)
        {
            return pt;
        }
        public virtual Point PointToControl(Point pt)
        {
            pt.Offset(this.Left, this.Top);
            return pt;
        }
        public virtual Point PointToView(Point pt)
        {
            return pt;
        }
        public SortData SortData = null;

        public virtual void ColumnHeaderClick(GridViewColumn column)
        {
            if (string.IsNullOrEmpty(column.FieldName))
            {
                return;
            }
            DataTable table = this.DataSource as DataTable;
            if (table == null)
                return;
            ComponentModel.SortOrder sortorder = ComponentModel.SortOrder.Ascending;
            bool has = false;
            if (SortData == null)
            {
                SortDataTable sortdatatable = new SortDataTable();
                SortData = sortdatatable;
                Feng.Forms.ComponentModel.SortInfo sortinfo = new ComponentModel.SortInfo();
                SortData.SortInfo = sortinfo;
                sortdatatable.Source = table;
            }

            foreach (Feng.Forms.ComponentModel.SortColumn sortcol in SortData.SortInfo)
            {
                if (sortcol.Field == column.FieldName)
                {
                    has = true;
                    if (sortcol.SortOrder == ComponentModel.SortOrder.Ascending)
                    {
                        sortcol.SortOrder = ComponentModel.SortOrder.Descending;
                    }
                    else if (sortcol.SortOrder == ComponentModel.SortOrder.Descending)
                    {
                        sortcol.SortOrder = ComponentModel.SortOrder.Ascending;
                    }
                    break;
                }
            }

            if (!has)
            {
                SortData.SortInfo.Clear();
                Feng.Forms.ComponentModel.SortColumn sortcolumn = new ComponentModel.SortColumn();
                sortcolumn.Field = column.FieldName;
                sortcolumn.SortOrder = sortorder;
                sortcolumn.Type = TypeEnum.GetTypeEnum(table.Columns[column.FieldName].DataType);
                SortData.SortInfo.Add(sortcolumn);
            }


            SortData.Sort();

        }

        private short _selectmode = 0;
        [Browsable(false)]
        public virtual short SelectMode
        {
            get
            {
                return _selectmode;
            }
            set
            {
                _selectmode = value;
            }
        }

        private bool _columnAutosize = true;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ColumnAutoSize
        {
            get {
                return _columnAutosize;
            }
            set
            {
                _columnAutosize = value;
            }
        }

        private bool _showlinex = true;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(true)]
        public virtual bool ShowLines
        {
            get { return this._showlinex; }
            set { this._showlinex = value; }
        }
        private bool _rowAutosize = true;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool RowAutoSize
        {
            get
            {
                return _rowAutosize;
            }
            set
            {
                _rowAutosize = value;
            }
        }


 

        private int _triple = 3;
        [Browsable(true)]
        [DefaultValue(3)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual int triple
        {
            get {
                return _triple;
            }
            set {
                _triple = value;
            }
        }

 
        public const short SelectModel_Null = 0;
        public const short SelectModel_ColumnSizeChanged = 1;
        public const short SelectModel_RowHeaderWidthChanged = 2;
        public const short SelectModel_ColumnHeaderHeightChanged = 3;


        #region IMouseOverBackColor 成员
        private Color _MouseOverBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseOverBackColor
        {
            get
            {
                return _MouseOverBackColor;
            }
            set
            {
                _MouseOverBackColor = value;
            }
        }

        #endregion

        #region IMouseDownBackColor 成员
        private Color _MouseDownBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseDownBackColor
        {
            get
            {
                return _MouseDownBackColor;
            }
            set
            {
                _MouseDownBackColor = value;
            }
        }

        #endregion

        #region IFocusBackColor 成员
        private Color _FocusBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color FocusBackColor
        {
            get
            {
                return _FocusBackColor;
            }
            set
            { 
                _FocusBackColor = value;
            }
        }

        #endregion

        #region IMouseOverForeColor 成员
        private Color _MouseOverForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseOverForeColor
        {
            get
            {
                return _MouseOverForeColor;
            }
            set
            { 
                _MouseOverForeColor = value;
            }
        }

        #endregion

        #region IMouseDownForeColor 成员
        private Color _MouseDownForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color MouseDownForeColor
        {
            get
            {
                return _MouseDownForeColor;
            }
            set
            { 
                _MouseDownForeColor = value;
            }
        }

        #endregion

        #region IFocusForeColor 成员
        private Color _FocusForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color FocusForeColor
        {
            get
            { 
                return _FocusForeColor;
            }
            set
            { 
                _FocusForeColor = value;
            }
        }

        #endregion

        private bool _ShowCheckBox = false;
        [Browsable(true), DefaultValue(false)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ShowCheckBox
        {
            get
            {
                return _ShowCheckBox;
            }
            set
            {
                _ShowCheckBox = value;
            }
        }
        #endregion

 
    }
}

