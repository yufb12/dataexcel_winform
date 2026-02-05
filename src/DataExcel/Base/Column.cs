using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using Feng.Excel.Designer;
using System.Reflection;
using Feng.Forms.Controls.Designer;
using Feng.Print;
using Feng.Utils;
using Feng.Data;
using Feng.Excel.Args;
using Feng.Excel.Interfaces;
using Feng.Excel.App;
using Feng.Drawing;

namespace Feng.Excel.Base
{
    [Serializable]
    [Editor(typeof(ColumnDesigner), typeof(System.Drawing.Design.UITypeEditor))]
    [TypeConverter(typeof(ColumnTypeConverter))]
    public partial class Column : IColumn
    {
        private Column()
        {  
        }
        public Column(DataExcel grid)
        {
            _grid = grid; 
            this._width = this.Grid.DefaultColumnWidth;
        }

        public Column(DataExcel grid, int index)
        {
            this._grid = grid;
            this._Index = index;
            this._width = this.Grid.DefaultColumnWidth;
            if (index == 0)
            {
                this._width = 40;
            }
        }

        public override string ToString()
        { 
            return this.Index.ToString(); 
        }

        private ICell headercell = null;
        private string headercellname = string.Empty;
        public ICell DefaultStyleCell 
        {
            get
            {
                if (headercell == null)
                {
                    if (this.Grid != null)
                    {
                        if (!string.IsNullOrEmpty(this.headercellname))
                        {
                            headercell = this.Grid.GetCellByNameAndID(this.headercellname);
                            //return this.Grid.FindCell(0, this.Index);
                        } 
                    }
                }
                return headercell;
            }
            set
            {
                headercell = value;
                if (value == null)
                {
                    this.headercellname = string.Empty;
                }
                else
                {
                    this.headercellname = value.Name;
                }
            }
        }
  
        #region ICellSelect 成员 
        [Browsable(false)]
        public virtual bool CellSelect
        {
            get
            {
                if (this.Grid == null)
                    return false;
                if (this.Grid.SelectCells == null)
                    return false;
                int max = this.Grid.SelectCells.MaxColumn();
                int min = this.Grid.SelectCells.MinColumn();
                return (this.Index >= min && this.Index <= max);
            } 
        }

        #endregion

        #region IBounds 成员

        [Browsable(true)]
 
        public virtual int Height
        {
            get
            {
                return this.Grid.Height;
            }
            set
            {
                Trace.Assert(false, "Columnn Height");
            }
        }
        [Browsable(true)]
        public virtual int Right
        {
            get { return this._left + this.Width; }
        }
        [Browsable(true)]
        public virtual int Bottom
        {
            get { return this.Top + this.Height; }
        }

        private int _left = 0;
        [Browsable(true)]
        public virtual int Left
        {
            get
            {
                return _left;
            }
            set { _left = value; }

        }
        [Browsable(true)]
        public virtual int Top
        {
            get
            {
                return this.Grid.TopSideHeight;
            }
            set { }
        }

        private int _width = 72;
        [Browsable(true)]
        public virtual int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (!this._allowchangedsize)
                {
                    return;
                }
                if (this.Width == value)
                {
                    return;
                }
                //BeforeColumnWidthChangedArgs e = new BeforeColumnWidthChangedArgs();
                //this.Grid.OnBeforColumnWidthChanged(e);
                //if (e.Cancel)
                //{
                //    return;
                //}
#if DEBUG
                if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
                {

                }
#endif
                this._width = value;
                if (this._width < 0)
                {
                    this._width = 0;
                }
                //this.Grid.ReFreshFirstDisplayColumnIndex();
                //this.Grid.OnColumnWidthChanged(this);
            }
        }
        [Browsable(true)]
        public virtual Rectangle Rect
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }

        #endregion

        #region IInhertReadOnly 成员
        private bool _inhertreadonly = true;
        public virtual bool InhertReadOnly
        {
            get
            {
                return _inhertreadonly;
            }
            set
            {
                _inhertreadonly = value;
            }
        }
        #endregion

        #region IReadOnly 成员
        private bool _readonly = false;
        public virtual bool ReadOnly
        {
            get
            {
                if (_inhertreadonly)
                {
                    return this.Grid.ReadOnly;
                }
                return _readonly;
            }
            set
            {

                _readonly = value;
            }
        }

        #endregion

        #region IControlColor 成员

        private Color _forecolor = Color.Empty;
        public virtual Color ForeColor
        {
            get
            {
                if (this._forecolor == Color.Empty)
                {
                    return this.Grid.ForeColor;
                }
                return _forecolor;
            }
            set
            {
                _forecolor = value;
            }
        }
        private Color _backcolor = Color.Empty;
        public virtual Color BackColor
        {
            get
            {
                if (this._backcolor == Color.Empty)
                {
                    return this.Grid.BackColor;
                }
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }

        #endregion

        #region ISave 成员

        public virtual void Save(IStream stream)
        {

        }

        #endregion

        private string _id = string.Empty; 
        [Browsable(true)]
        public virtual string ID
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
        #region IIndex 成员

        private int _Index = ConstantValue.NullValueIndex;
        [Browsable(false)]
        public virtual int Index
        {
            get
            {
                return _Index;
            }

            set { this._Index = value; }
        }
 
        #endregion
 
        #region IDraw 成员

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this.Selected)
            {
                Color cbrush = Color.FromArgb(100, this.FullColumnSelectedColor);
                SolidBrush brush = SolidBrushCache.GetSolidBrush(cbrush);
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, Left, Top, this.Width, this.Grid.Height);
                
            }
            else
            {
                if (this._backcolor != Color.Empty)
                {
                    System.Drawing.SolidBrush br = SolidBrushCache.GetSolidBrush(this.BackColor);
                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics,br, this.Left, this.Top, this.Width, this.Height);
                    
                }
            }
            return false;
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public virtual DataExcel Grid
        {
            get { return this._grid; }

            set { this._grid = value; }
        }
        #endregion

        #region ICaption 成员

        string _caption = string.Empty;
        [Browsable(true)]
        [ReadOnly(true)]
        public virtual string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                if (_caption != string.Empty)
                {
                    Size sf = Feng.Drawing.GraphicsHelper.Sizeof(_caption, this.Font, this.Grid.FindControl());
                    ICell cell = this.Grid[0, this.Index];
                    if (cell == null)
                    {
                        cell = this.Grid.ClassFactory.CreateDefaultCell(this.Grid, 0, this.Index);
                    }
                    if (cell != null)
                    {
                        cell.Text = (_caption);
                        cell.ContensWidth = sf.Width + 20;
                    }
                }
            }
        }


        #endregion

        #region IVisible 成员
        #region IVisible 成员


        private bool _Visible = true;
        public virtual bool Visible
        {
            get
            {
                return this._Visible;
            }
            set
            {
                this._Visible = value;
            }
        }


        #endregion

        #endregion

        #region ILineColor 成员
        private Color _linecolor = Color.Gray;
        [Browsable(true)]
        public virtual Color LineColor
        {
            get
            {
                return _linecolor;
            }
            set
            {
                _linecolor = value;
            }
        }

        #endregion

        #region ISelectColor 成员
        private Color _SelectColor = Color.DarkOrange;
        [Browsable(true)]
        public virtual Color SelectBackColor
        {
            get
            {
                return _SelectColor;
            }
            set
            {
                _SelectColor = value;
            }
        }


        #endregion

        #region ISelectColor 成员

        private Color _SelectForceColor = Color.Empty;
        [Browsable(true)]
        public virtual Color SelectForceColor
        {
            get
            {
                return _SelectForceColor;
            }
            set
            {
                _SelectForceColor = value;
            }
        }

        #endregion

        #region IFrozen 成员
        private bool _Frozen = false;
        public virtual bool Frozen
        {
            get
            {
                return _Frozen;
            }
            set
            {
                _Frozen = value;
            }
        }

        #endregion

        #region IFullColumnSelected 成员 
        [Browsable(true)]
        public virtual bool Selected
        {
            get
            {
                if (this.Grid != null)
                {
                    if (this.Grid.SelectColumns.Contains(this))
                    {
                        return true;
                    }
                }
                return false;
            } 
            set
            {
                if (this.Grid == null)
                    return;
                if (value)
                {
                    if (!this.Grid.AllowFullColumnSelect)
                    {
                        return;
                    }
                }
                if (value)
                {
                    this.Grid.SelectColumns.Add(this);
                    this.Grid.Selecteds.Add(this);
                }
                else
                {
                    this.Grid.SelectColumns.Remove(this);
                    this.Grid.Selecteds.Remove(this);
                }
            }
        }

        #endregion

        #region IFullColumnSelectedColor 成员

        private Color _FullColumnSelectedColor = Color.SlateBlue;

        public virtual Color FullColumnSelectedColor
        {
            get { return _FullColumnSelectedColor; }

            set { _FullColumnSelectedColor = value; }
        }


        #endregion

        #region IName 成员

        public virtual string Name
        {
            get { return DataExcel.GetColumnHeaderByColumnIndex(this.Index); }
            set { }
        }

        #endregion

        #region IFont 成员
        private Font _font = null;
        public virtual Font Font
        {
            get
            {
                if (this._font == null)
                {
                    if (this.Index < 1)
                        return this.Grid.Font;
                    return this.Grid.DefaultCellFont;
                }
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        #endregion

        #region IClear 成员

        public virtual void Clear()
        {

        }

        #endregion

        #region IAutoWidth 成员
        private bool _autowidth = true;
        public virtual bool AutoWidth
        {
            get
            {
                return _autowidth;
            }
            set
            {
                _autowidth = value;
            }
        }

        #endregion

        #region IPrint 成员
        public virtual bool Print(PrintArgs e)
        {
            return false;
        }

        #endregion

        #region IRead 成员
 
        public virtual void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        #endregion
        #region IDataStruct 成员
        private static readonly Column Empty = new Column();
        [Browsable(false)]
        public virtual DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = string.Empty,
                    Name = string.Empty,
                };
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (Feng.Excel.IO.BinaryWriter bw = this.Grid.ClassFactory.CreateBinaryWriter(ms))
                    { 
                        bw.Write(1, this._autowidth,Empty._autowidth);
                        bw.Write(2, this._backcolor,Empty._backcolor);
                        bw.Write(3, this._caption,Empty._caption);
                        bw.Write(4, false,false);
                        bw.Write(5, this._font,Empty._font);
                        bw.Write(6, this._forecolor,Empty._forecolor);
                        bw.Write(7, this._Frozen,Empty._Frozen);
                        bw.Write(8, this._FullColumnSelectedColor,Empty._FullColumnSelectedColor);
                        bw.Write(9, this._Index,Empty._Index);
                        bw.Write(10, this._Index,Empty._Index);
                        bw.Write(11, this._linecolor,Empty._linecolor);
                        bw.Write(12, this._width,Empty._width);
                        bw.Write(13, this._inhertreadonly,Empty._inhertreadonly);
                        bw.Write(14, this._Visible,Empty._Visible);
                        bw.Write(15, this._allowchangedsize,Empty._allowchangedsize);
                        bw.Write(16, false,false);
                        bw.Write(17, columntypetext,Empty.columntypetext);
                        bw.Write(18, this._id, Empty._id);
                        bw.Write(19, false,false);
                        bw.Write(20, this._inhertreadonly,Empty._inhertreadonly);
                        bw.Write(21, this._left,Empty._left);
                        bw.Write(22, this._readonly,Empty._readonly);
                        bw.Write(23, this._SelectColor,Empty._SelectColor);
                        bw.Write(24, this._SelectForceColor,Empty._SelectForceColor); 
                        bw.Write(26, _aliases, Empty._aliases);
                        bw.Write(27, headercellname, Empty.headercellname);

                        
                    }
                    data.Data = ms.ToArray();
                }

                return data;
            }
        }
        private string columntypetext {
            get {
                return this._ColumnType == null ? string.Empty : this._ColumnType.FullName;
            }
        }
 
        #endregion

        #region IVersion 成员
        [Browsable(false)]
        public virtual string Version
        {
            get { return Feng.DataUtlis.SmallVersion.AssemblySecondVersion; }
        }
 
        #endregion

        #region IAssembly 成员
        [Browsable(false)]
        public virtual string DllName
        {
            get { return string.Empty; }
        }

        #endregion

        #region IDownLoadUrl 成员
        [Browsable(false)]
        public virtual string DownLoadUrl
        {
            get { return string.Empty; }
        }

        #endregion


        #region ICurrentIColumnCollection 成员

        [ReadOnly(true)]
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public virtual IColumnCollection Columns
        {
            get { return this._columns; }

            set { this._columns = value; }
        }
        private IColumnCollection _columns;

        #endregion
 
        #region IColumnType 成员
        private Type _ColumnType = null;
        [Browsable(false)]
        public virtual Type DataType
        {
            get
            {
                return _ColumnType;
            }
            set
            {
                _ColumnType = value;
            }
        }

        #endregion

        #region ISortOrder 成员
        private Feng.Forms.ComponentModel.SortOrder _order = Feng.Forms.ComponentModel.SortOrder.Default;
        [Browsable(false)]
        public virtual Feng.Forms.ComponentModel.SortOrder Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;

                //if (value != SortOrder.Null)
                //{
                //    this.Grid.Selecteds.Add(this);
                //} 
            }
        }

        #endregion

        #region IAllowChangedSize 成员
        private bool _allowchangedsize = true;
        [DefaultValue(true)]

        public virtual bool AllowChangedSize
        {
            get
            {
                return _allowchangedsize;
            }
            set
            {
                _allowchangedsize = value;
            }
        }

        #endregion

        #region ITag 成员

        private object _tag = null;
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

        #endregion

        #region IFormat 成员
        private FormatType _FormatType = FormatType.Null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public FormatType FormatType
        {
            get
            {
                return _FormatType;
            }
            set
            {

                _FormatType = value;
            }
        }
        private string _FormatString = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public string FormatString
        {
            get
            {
                return _FormatString;
            }
            set
            {

                _FormatString = value;
            }
        }

        #endregion


        //private string _id = string.Empty;
        //[Browsable(true)]
        //public virtual string ID
        //{
        //    get
        //    {
        //        return _id;
        //    }
        //    set
        //    {
        //        _id = value;
        //    }
        //}
        [Browsable(false)]
        public float SelectBorderWidth
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        private string _aliases = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual string Aliases { get { return _aliases; } set { _aliases = value; } }

        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                stream.ReadCache();
                this._autowidth = stream.ReadIndex(1, this._autowidth);
                this._backcolor = stream.ReadIndex(2, this._backcolor);
                this._caption = stream.ReadIndex(3, this._caption);
                this._font = stream.ReadIndex(5, this._font);
                this._forecolor = stream.ReadIndex(6, this._forecolor);
                this._Frozen = stream.ReadIndex(7, this._Frozen);
                this._FullColumnSelectedColor = stream.ReadIndex(8, this._FullColumnSelectedColor);
                this._Index = stream.ReadIndex(9, this._Index);
                this._Index = stream.ReadIndex(10, this._Index);
                this._linecolor = stream.ReadIndex(11, this._linecolor);
                this._width = stream.ReadIndex(12, this._width);
                this._inhertreadonly = stream.ReadIndex(13, this._inhertreadonly);
                this._Visible = stream.ReadIndex(14, this._Visible);
                this._allowchangedsize = stream.ReadIndex(15, this._allowchangedsize);
                string columntypetext = stream.ReadIndex(17, string.Empty);
                this._id = stream.ReadIndex(18, this._id);
                this._inhertreadonly = stream.ReadIndex(20, this._inhertreadonly);
                this._left = stream.ReadIndex(21, this._left);
                this._readonly = stream.ReadIndex(22, this._readonly);
                this._SelectColor = stream.ReadIndex(23, this._SelectColor);
                this._SelectForceColor = stream.ReadIndex(24, this._SelectForceColor);
                string defaultcelledittext = stream.ReadIndex(25, string.Empty);
                this._aliases = stream.ReadIndex(26, this._aliases);
                this.headercellname = stream.ReadIndex(27, this.headercellname);

                if (columntypetext != string.Empty)
                {
                    this._ColumnType = Type.GetType(columntypetext);
                }
            }
        }

        public bool Deleted { get; set; }
    }
}
