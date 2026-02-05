using System;
namespace Feng.DevTools.Code
{
    /// <summary>
    /// 实体类clsSysCodes 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SysCodes
    {
        public SysCodes()
        { }
        #region Model
        private int _id;
        private string _tablename;
        private string _columnname;
        private string _projectname;
        private string _datatype;
        private int _length;
        private bool _inidentity;
        private bool _inprimarykey;
        private bool _nullable;
        private int _numericprecision;
        private int _numericscale;
        private int _issparse;
        private bool _Visible;
        private bool _empty;
        private bool _readonly;
        private string _readonlyfromtable;
        private string _readonlyfromfield;
        private bool _selectviable;
        private bool _issum;
        private bool _isavg;
        private bool _iscount;
        private bool _isgridVisible;
        private bool _ismin;
        private bool _ismax;
        private int? _visibleindex;
        private string _labeltitle;
        private bool _isselect;
        private string _selectlisttexts;
        private bool _isdropdown;
        private string _dropdowntext;
        private bool _isnumbercode;
        private string _numbercode;
        private bool _isimage;
        private string _imageurl;
        private bool _isquery;
        private string _querymode;
        private string _defaultcontroltext;
        private bool _iscaption;
        private bool _ismemo;
        private int _fieldprecise;
        /// <summary>
        ///字段名: ID	 | 
        ///字段类型: int	 | 
        ///是否允许空: False	 | 
        ///长度: 4	 | 
        ///是否是标识列: True	 | 
        ///是否是主键: False
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        ///字段名: TableName	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: False	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: True
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        ///字段名: ColumnName	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: False	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: True
        /// </summary>
        public string ColumnName
        {
            set { _columnname = value; }
            get { return _columnname; }
        }
        /// <summary>
        ///字段名: ProjectName	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: False	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: True
        /// </summary>
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        ///字段名: DataType	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: False	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string DataType
        {
            set { _datatype = value; }
            get { return _datatype; }
        }
        /// <summary>
        ///字段名: Length	 | 
        ///字段类型: int	 | 
        ///是否允许空: False	 | 
        ///长度: 4	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public int Length
        {
            set { _length = value; }
            get { return _length; }
        }
        /// <summary>
        ///字段名: InIdentity	 | 
        ///字段类型: bit	 | 
        ///是否允许空: False	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool InIdentity
        {
            set { _inidentity = value; }
            get { return _inidentity; }
        }
        /// <summary>
        ///字段名: InPrimaryKey	 | 
        ///字段类型: bit	 | 
        ///是否允许空: False	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool InPrimaryKey
        {
            set { _inprimarykey = value; }
            get { return _inprimarykey; }
        }
        /// <summary>
        ///字段名: Nullable	 | 
        ///字段类型: bit	 | 
        ///是否允许空: False	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool Nullable
        {
            set { 
                _nullable = value; }
            get { return _nullable; }
        }
        /// <summary>
        ///字段名: NumericPrecision	 | 
        ///字段类型: int	 | 
        ///是否允许空: False	 | 
        ///长度: 4	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public int NumericPrecision
        {
            set { _numericprecision = value; }
            get { return _numericprecision; }
        }
        /// <summary>
        ///字段名: NumericScale	 | 
        ///字段类型: int	 | 
        ///是否允许空: False	 | 
        ///长度: 4	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public int NumericScale
        {
            set { _numericscale = value; }
            get { return _numericscale; }
        }
        /// <summary>
        ///字段名: IsSparse	 | 
        ///字段类型: int	 | 
        ///是否允许空: False	 | 
        ///长度: 4	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public int IsSparse
        {
            set { _issparse = value; }
            get { return _issparse; }
        }
        /// <summary>
        ///字段名: Visible	 | 
        ///字段类型: bit	 | 
        ///是否允许空: False	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool Visible
        {
            set { _Visible = value; }
            get { return _Visible; }
        }
        /// <summary>
        ///字段名: Empty	 | 
        ///字段类型: bit	 | 
        ///是否允许空: False	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool Empty
        {
            set { _empty = value; }
            get { return _empty; }
        }
        /// <summary>
        ///字段名: ReadOnly	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool ReadOnly
        {
            set { _readonly = value; }
            get { return _readonly; }
        }
        /// <summary>
        ///字段名: ReadOnlyFromTable	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string ReadOnlyFromTable
        {
            set { _readonlyfromtable = value; }
            get { return _readonlyfromtable; }
        }
        /// <summary>
        ///字段名: ReadOnlyFromField	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string ReadOnlyFromField
        {
            set { _readonlyfromfield = value; }
            get { return _readonlyfromfield; }
        }
        /// <summary>
        ///字段名: SelectViable	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool SelectViable
        {
            set { _selectviable = value; }
            get { return _selectviable; }
        }
        /// <summary>
        ///字段名: IsSum	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsSum
        {
            set { _issum = value; }
            get { return _issum; }
        }
        /// <summary>
        ///字段名: IsAvg	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsAvg
        {
            set { _isavg = value; }
            get { return _isavg; }
        }
        /// <summary>
        ///字段名: IsCount	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsCount
        {
            set { _iscount = value; }
            get { return _iscount; }
        }
        /// <summary>
        ///字段名: IsGridVisible	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsGridVisible
        {
            set { _isgridVisible = value; }
            get { return _isgridVisible; }
        }
        /// <summary>
        ///字段名: IsMin	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsMin
        {
            set { _ismin = value; }
            get { return _ismin; }
        }
        /// <summary>
        ///字段名: IsMax	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsMax
        {
            set { _ismax = value; }
            get { return _ismax; }
        }
        /// <summary>
        ///字段名: VisibleIndex	 | 
        ///字段类型: int	 | 
        ///是否允许空: True	 | 
        ///长度: 4	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public int? VisibleIndex
        {
            set { _visibleindex = value; }
            get { return _visibleindex; }
        }
        /// <summary>
        ///字段名: LabelTitle	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string LabelTitle
        {
            set { _labeltitle = value; }
            get { return _labeltitle; }
        }
        /// <summary>
        ///字段名: IsSelect	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsSelect
        {
            set { _isselect = value; }
            get { return _isselect; }
        }
        /// <summary>
        ///字段名: SelectListtexts	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 500	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string SelectListtexts
        {
            set { _selectlisttexts = value; }
            get { return _selectlisttexts; }
        }
        /// <summary>
        ///字段名: IsDropdown	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsDropdown
        {
            set { _isdropdown = value; }
            get { return _isdropdown; }
        }
        /// <summary>
        ///字段名: DropdownText	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string DropdownText
        {
            set { _dropdowntext = value; }
            get { return _dropdowntext; }
        }
        /// <summary>
        ///字段名: IsNumberCode	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsNumberCode
        {
            set { _isnumbercode = value; }
            get { return _isnumbercode; }
        }
        /// <summary>
        ///字段名: NumberCode	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string NumberCode
        {
            set { _numbercode = value; }
            get { return _numbercode; }
        }
        /// <summary>
        ///字段名: IsImage	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsImage
        {
            set { _isimage = value; }
            get { return _isimage; }
        }
        /// <summary>
        ///字段名: ImageUrl	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        ///字段名: IsQuery	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsQuery
        {
            set { _isquery = value; }
            get { return _isquery; }
        }
        /// <summary>
        ///字段名: QueryMode	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string QueryMode
        {
            set { _querymode = value; }
            get { return _querymode; }
        }
        /// <summary>
        ///字段名: DefaultControlText	 | 
        ///字段类型: varchar	 | 
        ///是否允许空: True	 | 
        ///长度: 50	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public string DefaultControlText
        {
            set { _defaultcontroltext = value; }
            get { return _defaultcontroltext; }
        }
        /// <summary>
        ///字段名: IsCaption	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsCaption
        {
            set { _iscaption = value; }
            get { return _iscaption; }
        }
        /// <summary>
        ///字段名: IsMemo	 | 
        ///字段类型: bit	 | 
        ///是否允许空: True	 | 
        ///长度: 1	 | 
        ///是否是标识列: False	 | 
        ///是否是主键: False
        /// </summary>
        public bool IsMemo
        {
            set { _ismemo = value; }
            get { return _ismemo; }
        }

        public int FieldPrecise
        {
            get {
                return this._fieldprecise;
            }
            set
            {
                this._fieldprecise = value;
            }
        }
        #endregion Model

   

    }
}

