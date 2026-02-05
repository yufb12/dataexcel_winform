

using Feng.Data;
using Feng.Drawing;
using Feng.Excel.App;
using Feng.Excel.Args;
using Feng.Excel.Interfaces;
using Feng.Excel.Print;
using Feng.Forms.Controls.Designer;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Feng.Excel.Base
{
    [Serializable]
    public class Row : IRow
    {
        #region 构造

        private Row()
        {
        }

        public Row(DataExcel grid)
        {
            this._grid = grid;
            this._height = this.Grid.DefaultRowHeight;
            this._cells = this.Grid.ClassFactory.CreateDefaultCells(this);
          
        }

        public Row(DataExcel grid, int index)
        {
            _grid = grid;
            _cells = this.Grid.ClassFactory.CreateDefaultCells(this);
            this._Index = index;
            this._height = this.Grid.DefaultRowHeight;
            if (index==0)
            {
                this._height = this._height + 5;
            }
        }

        #endregion

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
        private Color _FullRowSelectedColor = Color.SlateBlue;

        public virtual Color FullRowSelectedColor
        {
            get { return _FullRowSelectedColor; }

            set { _FullRowSelectedColor = value; }
        }

        public virtual bool Selected
        {
            get
            {
                if (this.Grid != null)
                {
                    if (this.Grid.SelectRows.Contains(this))
                    {
                        return true;
                    }
                }
                return false;
            }

            set
            {
                if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
                {

                }
                if (this.Grid == null)
                    return;
                if (value)
                {
                    if (!this.Grid.AllowFullRowSelect)
                    {
                        return;
                    }
                }
                if (value)
                {
                    this.Grid.SelectRows.Add(this);
                    this.Grid.Selecteds.Add(this);
                }
                else
                {
                    this.Grid.SelectRows.Remove(this);
                    this.Grid.Selecteds.Remove(this);
                }
                if (FullRowSelectedChanged != null)
                {
                    FullRowSelectedChanged(this, new EventArgs());
                }
            }
        }

        private EventHandler FullRowSelectedChanged;

        public virtual ICell this[int columnindex]
        {
            get {
                ICell cell = this.Cells[columnindex];
                if (cell == null)
                {
                    cell = this.Grid.ClassFactory.CreateDefaultCell(this.Grid, this.Index, columnindex);
                }
                return cell;
            }
        }
        public virtual ICell this[IColumn column]
        {
            get { return this.Cells[column]; }
        }

        public virtual ICell GetCellByIndex(int index)
        {
            return this[index];
        }

        #region ICellSelect 成员 
        public virtual bool CellSelect
        {
            get
            {
                if (this.Grid == null)
                    return false;
                if (this.Grid.SelectCells == null)
                    return false;
                int max = this.Grid.SelectCells.MaxRow();
                int min = this.Grid.SelectCells.MinRow();
                return (this.Index >= min && this.Index <= max);
            }
        }

        #endregion

        #region IBounds 成员


        public virtual int Right
        {
            get { return this.Left + this.Width; }
        }

        public virtual int Bottom
        {
            get { return this._top + this.Height; }
        }

        public virtual int Width
        {
            get
            {
                return this.Grid.Width;
            }
            set
            { 
            }
        }
        public virtual Rectangle Rect
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }

        public virtual int Left
        {
            get
            {
                return this.Grid.LeftSideWidth;
            }
            set { throw new Exception("Read Only"); }
        }

        private int _top = 0;
        public virtual int Top
        {
            get
            {
                return _top;
            }
            set
            {
#if DEBUG
                if (this.Index == 1)
                {

                }

#endif
                _top = value;
            }
        }

        private int _height = 20;

        public virtual int Height
        {
            get
            {
                return _height;
            }
            set
            {
                //if (!_allowchangedsize)
                //{
                //    return;
                //}
                //BeforRowHeightChangedArgs e = new BeforRowHeightChangedArgs();
                //e.Row = this;
                //e.Height = value;
                //this.Grid.OnBeforRowHeightChanged(e);
                //if (e.Cancel)
                //{
                //    return;
                //}
                //this.Grid.BeginSetFirstDisplayRowIndex();
#if DEBUG
                if (this.Index==1)
                {

                }

#endif
                _height = value;
                if (this._height < 0)
                {
                    this._height = 0;
                }
                //this.Grid.EndSetFirstDisplayRowIndex();
                //this.Grid.OnRowHeightChanged(this);
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

        #region IDataExcelGrid 成员

        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataExcel Grid
        {
            get { return this._grid; }

            set { this._grid = value; }
        }

        #endregion

        #region IControlColor 成员

        private Color _forecolor = Color.Empty;
        public virtual Color ForeColor
        {
            get
            {
                if (_forecolor == Color.Empty)
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
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }

        #endregion

        #region ISelectColor 成员
        private Color _SelectColor = Color.DarkOrange;
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

        #region ISelectBorderColor 成员

        private Color _SelectBorderColor = Color.Empty;
        public virtual Color SelectBorderColor
        {
            get
            {
                return _SelectBorderColor;
            }
            set
            {
                _SelectBorderColor = value;
            }
        }

        #endregion

        #region ISelectBorderWidth 成员

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

        #endregion

        #region ICaption 成员

        string _caption = string.Empty;
        [Browsable(false)]
        [ReadOnly(true)]
        public virtual string Caption
        {
            get
            {
                _caption = string.Format("Index:{0}", this.Index);
                return _caption;
            }
            set { _caption = value; }
        }


        #endregion

        public override string ToString()
        {
#if Test
            string str = string.Format("Row Index:{0};Point({1},{2})",
                 this.Index,
                  this.Rect.Location.X, this.Rect.Location.Y);
            return str;
#else
            return this.Index.ToString();
#endif
        }

        #region IIndex 成员

        private int _Index = ConstantValue.NullValueIndex;

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
            DrawRowArgs args = new DrawRowArgs(g, this);
            this.Grid.OnDrawRow(args);
            if (!args.Handled)
            {
                DrawRow(g);
                Draw(g);
            }
            //g.Graphics.DrawString(string.Format("this.Index={0}, this.Top={1}, this.Height={2}", this.Index, this.Top, this.Height), this.Font, Brushes.Red, this.Rect);
            return false;
        }
        public virtual void DrawRow(Feng.Drawing.GraphicsObject g)
        {
            if (this.Selected)
            {
                Color cbrush = Color.FromArgb(100, this.FullRowSelectedColor);
                SolidBrush brush = Feng.Drawing.SolidBrushCache.GetSolidBrush(cbrush);
                GraphicsHelper.FillRectangle(g.Graphics, brush, this.Left, this.Top, this.Width, this.Height);
            }
            else
            {
                if (!this.BackColor.Equals(Color.Empty))
                {
                    SolidBrush brush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.BackColor);
                    GraphicsHelper.FillRectangle(g.Graphics, brush, this.Left, this.Top, this.Width, this.Height); ;
                }
            }
        }
        public virtual bool OnDrawBack(object sender, Feng.Drawing.GraphicsObject g)
        {
            DrawRowBackArgs args = new DrawRowBackArgs(g, this);
            this.Grid.OnDrawRowBack(args);
            if (!args.Handled)
            {
                DrawBac(g);
            }
            return false;
        }
        private void DrawBac(Feng.Drawing.GraphicsObject g)
        {
            if (this.BackColor != Color.Empty)
            {
                System.Drawing.SolidBrush br = SolidBrushCache.GetSolidBrush(this.BackColor);

                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, br, this.Left, this.Top, this.Width, this.Height);

            }
            foreach (IColumn cl in this.Grid.VisibleColumns)
            {
                ICell cell = this.Cells[cl];
                if (cell == null)
                {
                    cell = this.Grid.ClassFactory.CreateDefaultCell(this, cl);
                }
                if (cell != null)
                {
                    cell.OnDrawBack(this, g);
                }

            }

        }
        private void Draw(Feng.Drawing.GraphicsObject g)
        {
            foreach (IColumn cl in this.Grid.VisibleColumns)
            {
                ICell cell = this.Cells[cl];
                if (cell != null)
                {
                    cell.OnDraw(this, g);
                }
            }
        }
        #endregion

        #region ILineColor 成员
        private Color _linecolor = Color.Gray;
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

        #region IICellCollection 成员


        private ICellCollection _cells;
        [ReadOnly(true)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual ICellCollection Cells
        {
            get { return this._cells; }

            set { this._cells = value; }
        }

        #endregion

        #region ICurrentRowHeader 成员

        //private IRowHeaderCell _RowHeader = null;
        //[ReadOnly(true)]
        //[Browsable(true)]
        //[Category(CategorySetting.DataExcel)]
        //public IRowHeaderCell RowHeader
        //{
        //    get { return this._RowHeader; }

        //    set
        //    {
        //        this._RowHeader = value;
        //        this.Top = value.Top;
        //    }
        //}
        #endregion

        #region ICurrentIRosCollection 成员
        [ReadOnly(true)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual IRowCollection Rows
        {
            get { return this._rows; }

            set { this._rows = value; }
        }

        private IRowCollection _rows = null;

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

        #region IName 成员
        public virtual string Name
        {
            get { return this.Index.ToString(); }
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
            this.Cells.Clear();
        }

        #endregion

        #region bool 成员
        private bool _autoheight = true;
        public virtual bool AutoHeight
        {
            get
            {
                return _autoheight;
            }
            set
            {
                _autoheight = value;
            }
        }

        #endregion

        #region IDrawGridLine 成员

        public virtual void CreatDefaultCell()
        {
            for (int i = this.Grid.AllVisibleColumns.Count - 1; i >= 0; i--)
            {
                IColumn cl = this.Grid.AllVisibleColumns[i];
                ICell cell = this.Cells[cl];
                if (cell == null)
                {
                    cell = this.Grid.ClassFactory.CreateDefaultCell(this, cl);
                } 
            }
        }
        public virtual void DrawGridLine(Feng.Drawing.GraphicsObject g)
        {
            for (int i = this.Grid.AllVisibleColumns.Count - 1; i >= 0; i--)
            {
                IColumn cl = this.Grid.AllVisibleColumns[i];
                ICell cell = this.Cells[cl]; 
                if (cell != null)
                {
                    cell.DrawGridLine(g);
                }
            }
        }

        #endregion

        #region IPrint 成员

        public virtual bool Print(PrintArgs e)
        {
            System.Drawing.Graphics g = e.PrintPageEventArgs.Graphics;
            Page page = e.CurrentPage as Page;
            int cindex = page.StartColumnIndex;
            while (cindex <= page.EndColumnIndex)
            {

                IColumn dl = this.Grid.Columns[cindex];
                if (dl != null)
                {
                    e.BeginColumnIndex = cindex;
                    ICell cell = this.Cells[dl];
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            cell.OwnMergeCell.Print(e);
                        }
                        else
                        {
                            cell.Print(e);
                        }
                        if (page.Handled)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        cell = this.Grid.ClassFactory.CreateDefaultCell(this, dl);
                        cell.Print(e);
                        if (page.Handled)
                        {
                            return true;
                        }
                    }
                }
                int columnwidth = this.Grid.DefaultColumnWidth;
                if (dl != null)
                {
                    columnwidth = dl.Width;
                }
                e.CurrentLocation = new Point(e.CurrentLocation.X + columnwidth, e.CurrentLocation.Y);
                cindex = cindex + 1;

            }
            return false;
        }

        #endregion

        #region IPrintBorder 成员

        public void PrintBorder(PrintArgs e)
        {
            Page page = e.CurrentPage as Page;
            System.Drawing.Graphics g = e.PrintPageEventArgs.Graphics;
            int cindex = page.StartColumnIndex;
            while (cindex <= page.EndColumnIndex)
            {

                IColumn dl = this.Grid.Columns[cindex];
                if (dl != null)
                {
                    e.BeginColumnIndex = cindex;
                    ICell cell = this.Cells[dl];
                    if (cell != null)
                    {
                        cell.PrintBorder(e);
                        if (page.Handled)
                        {
                        }
                    }
                    e.CurrentLocation = new Point(e.CurrentLocation.X + dl.Width, e.CurrentLocation.Y);

                }
                cindex = cindex + 1;
            }
        }

        #endregion

        #region IRead 成员


        public virtual void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        #endregion
        #region IDataStruct 成员
        private static readonly Row Empty = new Row();
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
                        bw.Write(1, this._autoheight, Empty._autoheight);
                        bw.Write(2, this._caption, Empty._caption);
                        bw.Write(3, this._font, Empty._font);
                        bw.Write(4, this._forecolor, Empty._forecolor);
                        bw.Write(5, this._height, Empty._height);
                        bw.Write(7, this._linecolor, Empty._linecolor);
                        bw.Write(8, this._backcolor, Empty._backcolor);
                        bw.Write(10, this._Frozen, Empty._Frozen);
                        //bw.Write(12, this._cells.Count, 0);
                        bw.Write(13, this._inhertreadonly, Empty._inhertreadonly);
                        bw.Write(14, this._Visible, Empty._Visible);
                        bw.Write(15, this._id, Empty._id);
                        DataStructCollection datastructs = new DataStructCollection();
                        foreach (ICell cell in this.Cells)
                        {
                            if (cell != null)
                            {
                                if (!cell.Column.Deleted)
                                {
                                    DataStruct dataStruct = cell.Data;
                                    if (dataStruct != null)
                                    {
                                        datastructs.Add(cell.Data);
                                    }
                                }
                            }
                        }

                        if (datastructs.Count > 0)
                        {
                            bw.Write(16, datastructs);
                        }
                        bw.Write(17, this._readonly, Empty._readonly);
                        bw.Write(18, this._ReadOnlyBackColor, Empty._ReadOnlyBackColor);
                        bw.Write(19, this._ReadOnlyForeColor, Empty._ReadOnlyForeColor);


               

                        bw.Write(20, this._SelectBorderColor, Empty._SelectBorderColor);
                        bw.Write(21, this._SelectForceColor, Empty._SelectForceColor);
                        bw.Write(22, this._aliases, Empty._aliases);
#if DEBUG
                        if (this.Index > 10000)
                        {

                        }
#endif

                        byte[] ds = bw.GetData();
                        if (ds.Length < 1)
                            return null;
                        bw.Write(23, this._Index, Empty._Index);
                        bw.Write(24, this._checked, Empty._checked); 
                        bw.Write(25, this._lockversion, Empty._lockversion); 
                        bw.Write(26, this.headercellname, Empty.headercellname);
                    }
                    data.Data = ms.ToArray();
                }
#warning 必须删除
#if DEBUG
                using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
                {
                    stream.ReadCache();
                }
#endif
                return data;
            }
        }

        public bool RowHasValue { get; set; }
        #endregion

        #region IVersion 成员
        [Browsable(false)]
        public virtual string Version
        {
            get { return Feng.DataUtlis.SmallVersion.AssemblySecondVersion; }
        }


        #endregion

        #region IAssembly 成员

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

        #region IDefaultCellEdit 成员
        private ICellEditControl _defaultcelledit = null;
        public virtual ICellEditControl DefaultCellEdit
        {
            get
            {
                return _defaultcelledit;
            }
            set
            {
                _defaultcelledit = value;
            }
        }

        #endregion

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

        #region IReadOnlyForeColor 成员
        private Color _ReadOnlyForeColor = Color.Empty;

        public Color ReadOnlyForeColor
        {
            get
            {
                if (_ReadOnlyForeColor == Color.Empty)
                {
                    return this.Grid.ReadOnlyForeColor;
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
        public Color ReadOnlyBackColor
        {
            get
            {
                if (_ReadOnlyBackColor == Color.Empty)
                {
                    return this.Grid.ReadOnlyBackColor;
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
        public Color FocusForeColor
        {
            get
            {
                if (_FocusForeColor == Color.Empty)
                {
                    return this.Grid.FocusForeColor;
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
        public Color FocusBackColor
        {
            get
            {
                if (_FocusBackColor == Color.Empty)
                {
                    return this.Grid.FocusBackColor;
                }
                return _FocusBackColor;
            }
            set
            {
                _FocusBackColor = value;
            }
        }

        #endregion

        #region ITag 成员

        private object _tag = null;

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


        private string _id = string.Empty;
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

        public ICell this[string column]
        {
            get
            {
                foreach (IColumn col in this.Grid.Columns)
                {
                    if (col.Name.ToLower() == column.ToLower()
                        || col.ID.ToLower() == column.ToLower())
                    {
                        return this.Cells[col];
                    }
                }
                return null;
            }
        }

        public ICell GetCellByName(string column)
        {
            return this[column];
        }


        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                DataStructCollection datastructs = null;
                stream.ReadCache();
                this._autoheight = stream.ReadIndex(1, this._autoheight);
                this._caption = stream.ReadIndex(2, this._caption);
                this._font = stream.ReadIndex(3, this._font);
                this._forecolor = stream.ReadIndex(4, this._forecolor);
                this._height = stream.ReadIndex(5, this._height);
                this._linecolor = stream.ReadIndex(7, this._linecolor);
                this._backcolor = stream.ReadIndex(8, this._backcolor);
                this._Frozen = stream.ReadIndex(10, this._Frozen);
                this._Index = stream.ReadIndex(11, this._Index);
                if (this._Index > 0)
                {

                }
                this._inhertreadonly = stream.ReadIndex(13, this._inhertreadonly);
                this._Visible = stream.ReadIndex(14, this._Visible);
                this._id = stream.ReadIndex(15, this._id);
                datastructs = stream.ReadIndex(16, datastructs);
                this._readonly = stream.ReadIndex(17, this._readonly);
                this._ReadOnlyBackColor = stream.ReadIndex(18, this._ReadOnlyBackColor);
                this._ReadOnlyForeColor = stream.ReadIndex(19, this._ReadOnlyForeColor);

                this._SelectBorderColor = stream.ReadIndex(20, this._SelectBorderColor);
                this._SelectForceColor = stream.ReadIndex(21, this._SelectForceColor);
                this._aliases = stream.ReadIndex(22, this._aliases);
                this._Index = stream.ReadIndex(23, this._Index);
                this._checked = stream.ReadIndex(24, this._checked);
                this._lockversion = stream.ReadIndex(25, this._lockversion);
                this.headercellname = stream.ReadIndex(26, this.headercellname);

                this.Grid.Rows.Add(this);
                try
                {
                    if (datastructs != null)
                    {
                        foreach (DataStruct datacell in datastructs)
                        {
                            ICell cell = null;
                            if (!string.IsNullOrEmpty(datacell.DllName))
                            {
                                cell = DataExcel.CreateInatance<ICell>(datacell.DllName, datacell.FullName,
                datacell.AessemlyDownLoadUrl, this._grid, new object[] { this });
                            }
                            else
                            {
                                cell = this.Grid.ClassFactory.CreateDefaultCell(this._grid);
                            }
                            cell.ReadDataStruct(datacell);
#if DEBUG2
                            if (this.Index > 0 && cell.Column.Index >0)
                            {
                                if (cell.OwnEditControl.GetType() == typeof(Feng.Excel.Edits.CellRowHeader))
                                {
                                    cell.OwnEditControl = null;
                                }
                                if (cell.OwnEditControl.GetType() == typeof(Feng.Excel.Edits.CellColumnHeader))
                                {
                                    cell.OwnEditControl = null;
                                }
                            }
#endif
                            if ((cell.Value != null) || (!string.IsNullOrWhiteSpace(cell.Text)))
                            {
                                this.RowHasValue = true;
                                if (cell.Column.Index > this.Grid.MaxHasValueColumn)
                                {
                                    this.Grid.MaxHasValueColumn = cell.Column.Index;
                                }
                            }
                            this.Cells.Add(cell);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Feng.IO.LogHelper.Log(ex);
                    Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
                }
            }
        }

        private string _aliases = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual string Aliases { get { return _aliases; } set { _aliases = value; } }

        private bool _checked = false;
        [Browsable(true)]
        public bool Checked 
        { 
            get { return _checked; } 
            set { _checked = value; }
        }


        private Feng.Forms.Base.LockVersion _lockversion = null; 
        public Feng.Forms.Base.LockVersion LockVersion
        {
            get { return _lockversion; }
            set { _lockversion = value; }
        }

    }
}
