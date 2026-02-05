using Feng.Data;
using Feng.Drawing;
using Feng.Enums;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using Feng.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Feng.Forms.Controls.GridControl
{
    [DefaultProperty("FieldName")]
    public class GridViewColumn : IVisible, IReadOnly, IInhertReadOnly
    {
        public GridViewColumn(GridView grid)
        {
            _grid = grid;
        }

        private GridView _grid = null;
        [Category(CategorySetting.PropertyDesign)]
        [Browsable(false)]
        public GridView Grid
        {
            get
            {
                return _grid;
            }
        }
        [Browsable(false)]
        public Rectangle ColumnHeader
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Grid.ColumnHeaderHeight);
            }
        }

        private FooterCell _footercell = null;
        [Browsable(false)]
        public virtual FooterCell FooterCell
        {
            get
            {
                if (_footercell == null)
                {
                    _footercell = new FooterCell();
                    _footercell.Column = this;
                    _footercell.Footer = this.Grid.Footer;
                }
                return _footercell;
            }
        }

        private object _totalvalue = null;
        [Browsable(false)]
        public object TotalValue
        {
            get
            {
                return _totalvalue;
            }
            set
            {
                _totalvalue = value;
                _totaltext = Feng.Utils.TextHelper.Format(value, this.FormatType, this.FormatString);
            }
        }

        private string _totaltext = string.Empty;
        [Browsable(false)]
        public string TotalText
        {
            get
            {
                return _totaltext;
            }
            set
            {
                _totaltext = value;
            }
        }
        private TotalMode _totalmode = TotalMode.Null;
        [Browsable(true)]
        public virtual TotalMode TotalMode
        {
            get
            {
                return _totalmode;
            }
            set
            {
                _totalmode = value;
                this.Grid.ReFreshTotalCount(this.FieldName);
            }
        }
        #region ICellSelect 成员
        [Browsable(false)]
        public virtual bool CellSelect
        {
            get
            {
                if (this.Grid.SelectCells != null)
                {
                    foreach (GridViewCell cell in this.Grid.SelectCells)
                    {
                        if (cell.Column == this)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        #endregion

        #region IEditMode 成员
        private EditMode _EditMode = EditMode.Default;
        [DefaultValue(EditMode.KeyDown)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
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

        private string _EditName = string.Empty;
        [DefaultValue("")]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual string EditName
        {
            get
            {
                return _EditName;
            }
            set
            {
                _EditName = value;
            }
        }

        #endregion

        #region IOwnEditControl 成员
        private Feng.Forms.Interface.IEditView _OwnEditControl = null;
        [Browsable(false)]
        public Feng.Forms.Interface.IEditView OwnEditControl
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return _OwnEditControl;
            }
            set
            {
                this.Grid.BeginReFresh();
                _OwnEditControl = value;
                this.Grid.EndReFresh();
            }
        }

        #endregion

        #region ITextDirection 成员
        private bool _DirectionVertical = false;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool DirectionVertical
        {
            get
            {
                return _DirectionVertical;
            }
            set
            {
                _DirectionVertical = value;
            }
        }

        #endregion

        #region IHorizontalAlignment 成员
        private StringAlignment _HorizontalAlignment = StringAlignment.Near;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public StringAlignment HorizontalAlignment
        {
            get
            {
                return _HorizontalAlignment;
            }
            set
            {
                _HorizontalAlignment = value;
            }
        }

        #endregion

        #region IVerticalAlignment 成员
        private StringAlignment _VerticalAlignment = StringAlignment.Center;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public StringAlignment VerticalAlignment
        {
            get
            {
                return _VerticalAlignment;
            }
            set
            {
                _VerticalAlignment = value;
            }
        }

        #endregion

        #region IBounds 成员

        [Browsable(false)]
        public virtual int Height
        {
            get
            {
                return this.Grid.Height;
            }
            set
            {
            }
        }
        [Browsable(false)]
        public virtual int Right
        {
            get { return this._left + this.Width; }
        }
        [Browsable(false)]
        public virtual int Bottom
        {
            get { return this.Top + this.Height; }
        }

        private int _left = 0;
        [Browsable(false)]
        public virtual int Left
        {
            get
            {
                return _left;
            }
            set { _left = value; }

        }
        [Browsable(false)]
        public virtual int Top
        {
            get
            {
                return 0;
            }
            set { }
        }

        private int _width = 72;
        [Browsable(true)]
        [DefaultValue(72)]
        public virtual int Width
        {
            get
            {
                return _width;
            }
            set
            {
                this._width = value;
                if (this._width < 10)
                {
                    this._width = 10;
                }
            }
        }
        [Browsable(false)]
        public virtual RectangleF Rect
        {
            get
            {
                return new RectangleF(this.Left, this.Top, this.Width, this.Height);
            }
        }

        #endregion

        #region IReadOnly 成员
        private bool _readonly = false;

        [Category(CategorySetting.PropertyDesign)]
        public virtual bool ReadOnly
        {
            get
            {
                return _readonly;
            }
            set
            {
                _readonly = value;
                InhertReadOnly = false;
            }
        }
        private bool _inhertreadonly = true;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
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

        #region IControlColor 成员

        private Color _forecolor = Color.Empty;
        [Category(CategorySetting.PropertyUI)]
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
        [Category(CategorySetting.PropertyUI)]
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

        #region IIndex 成员

        [Browsable(false)]
        public virtual int Index
        {
            get
            {
                return this.Grid.Columns.IndexOf(this);
            }

        }

        #endregion

        #region IDraw 成员

        private void DrawCellText(Feng.Drawing.GraphicsObject g, RectangleF rect)
        {
            rect.Height = this.Grid.ColumnHeaderHeight;
            //g.Graphics.FillRectangle(Brushes.SandyBrown, rect);
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            if (this.DirectionVertical)
            {
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionVertical;
            }
            string text = this.Caption;

            if (text != string.Empty)
            {
                Color forecolor = this.ForeColor;
                if (forecolor == Color.Empty)
                {
                    forecolor = Color.Black;
                }

                using (SolidBrush sb = new SolidBrush(forecolor))
                {
                    rect.Location = new PointF(rect.Location.X, rect.Location.Y + 2);
                   SizeF sff= g.Graphics.MeasureString(text, this.Font);
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Font, sb, rect, sf);
                }
            }
        }
        public void DrawGridRectangle(Feng.Drawing.GraphicsObject g, float x, float y, float width, float height)
        {
            g.Graphics.DrawRectangle(PenCache.BorderGray, x, y, width, height);

        }

        private RectangleF GetSortRect()
        {

            if (this.Order != Feng.Forms.ComponentModel.SortOrder.Null)
            {
                RectangleF rectsort = new RectangleF(this.Right - 18, this.Top + 4, 9, 9);
                if (rectsort.Height > this.Height)
                {
                    return RectangleF.Empty;
                }
                if (rectsort.Width > this.Width)
                {
                    return RectangleF.Empty;
                }
                return rectsort;
            }
            return RectangleF.Empty;

        }
        private void DrawRect(Feng.Drawing.GraphicsObject g, RectangleF bounds)
        {
            Color backcolor = this.BackColor;
            RectangleF rect = bounds;
            Color c = ColorHelper.Dark(backcolor);
            if (this.Grid.ShowLines)
            {
                DrawGridRectangle(g, rect.Left, rect.Top, rect.Width, this.Grid.ColumnHeaderHeight);
            }

            RectangleF rectsort = GetSortRect();
            RectangleF rectf = new RectangleF(bounds.Left, bounds.Top, bounds.Width - rectsort.Width, bounds.Height);
            DrawCellText(g, rectf);

            SmoothingMode sm = g.Graphics.SmoothingMode;
            g.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.Order == Feng.Forms.ComponentModel.SortOrder.Ascending)
            {
                GraphicsPath path = GraphicsHelper.GetTriangle(rectsort
                    , Feng.Drawing.Orientation.Top);
                backcolor = ColorHelper.InvertColors(backcolor);
                GraphicsHelper.FillColorPath(g.Graphics, backcolor, rectsort, path,
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);

            }
            else if (this.Order == Feng.Forms.ComponentModel.SortOrder.Descending)
            {
                GraphicsPath path = GraphicsHelper.GetTriangle(rectsort
                    , Feng.Drawing.Orientation.Bottom);

                backcolor = ColorHelper.InvertColors(backcolor);
                GraphicsHelper.FillColorPath(g.Graphics, backcolor, rectsort, path,
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
            }
        }

        public Feng.Forms.ComponentModel.SortOrder GetSortOrder()
        {
            if (this.Grid.SortData != null)
            {
                if (this.Grid.SortData.SortInfo != null)
                {
                    foreach (var sortinfo in this.Grid.SortData.SortInfo)
                    {
                        if (sortinfo.Field == this.FieldName)
                        {
                            return sortinfo.SortOrder;
                        }
                    }
                }
            }
            return ComponentModel.SortOrder.Null;
        }

        public virtual void DrawSortIcon(Feng.Drawing.GraphicsObject g)
        {
            Feng.Forms.ComponentModel.SortOrder order = GetSortOrder();
            if (order == ComponentModel.SortOrder.Ascending)
            {
                g.Graphics.DrawImage(Feng.Drawing.Images.SortUp, this.Right - 20, this.Top + 4, 16, 16);
            }
            else if (order == ComponentModel.SortOrder.Descending)
            {
                g.Graphics.DrawImage(Feng.Drawing.Images.Sortdown, this.Right - 20, this.Top + 4, 16, 16);
            }
        }
        public virtual bool OnDraw(Feng.Drawing.GraphicsObject g)
        {
            if (this.Grid.ShowColumnHeader)
            {
                if (this.CellSelect)
                {
                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, this.Grid.CellSelectBrush, new Rectangle(this.Left, this.Top, this.Width, this.Grid.ColumnHeaderHeight));
                }
            }

            if (_FullColumnSelected)
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
                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, br, this.Left, this.Top, this.Width, this.Height);

                }
            }
            if (this.Grid.ShowColumnHeader)
            {
                DrawRect(g, this.Rect);
                DrawSortIcon(g);
            }
            return false;
        }

        #endregion

        #region ICaption 成员

        string _caption = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
            }
        }


        #endregion

        #region IVisible 成员

        private bool _Visible = true;
        [Category(CategorySetting.PropertyDesign)]
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

        #region ISelectColor 成员
        private Color _SelectColor = Color.DarkOrange;
        [Browsable(false)]
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
        [Browsable(false)]
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
        [Category(CategorySetting.PropertyData)]
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
        private bool _FullColumnSelected = false;
        [Browsable(false)]
        public virtual bool Selected
        {
            get
            {
                return _FullColumnSelected;
            }
            set
            {
                _FullColumnSelected = value;
            }
        }

        #endregion

        #region IFullColumnSelectedColor 成员

        private Color _FullColumnSelectedColor = Color.SlateBlue;

        [Category(CategorySetting.PropertyUI)]
        public virtual Color FullColumnSelectedColor
        {
            get { return _FullColumnSelectedColor; }

            set { _FullColumnSelectedColor = value; }
        }


        #endregion

        #region IFont 成员
        private Font _font = null;
        [Category(CategorySetting.PropertyUI)]
        public virtual Font Font
        {
            get
            {
                if (this._font == null)
                {
                    return this.Grid.Font;
                }
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        #endregion

        #region IAutoWidth 成员
        private bool _autowidth = true;
        [Category(CategorySetting.PropertyDesign)]
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

        #region IDataStruct 成员



        [Browsable(false)]
        public virtual DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = string.Empty,
                    Name = string.Empty,
                };

                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, this._allowchangedsize);
                    bw.Write(2, this._autowidth);
                    bw.Write(3, this._backcolor);
                    bw.Write(4, this._caption);
                    bw.Write(5, this._datatype);
                    bw.Write(6, this._DirectionVertical);
                    bw.Write(7, (int)this._EditMode);
                    bw.Write(8, this._fieldname);
                    bw.Write(9, this._font);
                    bw.Write(10, this._forecolor);
                    bw.Write(11, this._FormatString);
                    bw.Write(12, (int)this._FormatType);
                    bw.Write(13, this._Frozen);
                    bw.Write(14, this._FullColumnSelectedColor);
                    bw.Write(15, (int)this._HorizontalAlignment);
                    bw.Write(16, 0);
                    bw.Write(17, this._left);
                    bw.Write(18, (int)this._order);
                    bw.Write(19, this._readonly);
                    bw.Write(20, this._SelectColor);
                    bw.Write(21, this._SelectForceColor);
                    bw.Write(22, (int)this._VerticalAlignment);
                    bw.Write(23, this._Visible);
                    bw.Write(24, this._width);
                    if (this.OwnEditControl == null)
                    {
                        bw.Write(25, DataStruct.DataStructNull);
                    }
                    else
                    {
                        bw.Write(25, this.OwnEditControl.Data);
                    }
                    bw.Write(26, (int)this._totalmode);
                    bw.Write(27, this._totaltext);
                    bw.WriteBaseValue(28, this._totalvalue);
                    bw.Write(29, this._inhertreadonly);
                    data.Data = bw.GetData();
                    bw.Close();
                }

                return data;
            }
        }

        #endregion

        #region IRead 成员


        public virtual void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader stream = new IO.BufferReader(data.Data))
            {
                this._allowchangedsize = stream.ReadIndex(1, this._allowchangedsize);
                this._autowidth = stream.ReadIndex(2, this._autowidth);
                this._backcolor = stream.ReadIndex(3, this._backcolor);
                this._caption = stream.ReadIndex(4, this._caption);
                this._datatype = stream.ReadIndex(5, this._datatype);
                this._DirectionVertical = stream.ReadIndex(6, this._DirectionVertical);
                this._EditMode = (EditMode)stream.ReadIndex(7, (int)this._EditMode);
                this._fieldname = stream.ReadIndex(8, this._fieldname);
                this._font = stream.ReadIndex(9, this._font);
                this._forecolor = stream.ReadIndex(10, this._forecolor);
                this._FormatString = stream.ReadIndex(11, this._FormatString);
                this._FormatType = (FormatType)stream.ReadIndex(12, (int)this._FormatType);
                this._Frozen = stream.ReadIndex(13, this._Frozen);
                this._FullColumnSelectedColor = stream.ReadIndex(14, this._FullColumnSelectedColor);
                this._HorizontalAlignment = (StringAlignment)stream.ReadIndex(15, (int)this._HorizontalAlignment);
                stream.ReadIndex(16, 0);
                this._left = stream.ReadIndex(17, this._left);
                this._order = (Feng.Forms.ComponentModel.SortOrder)stream.ReadIndex(18, (int)this._order);
                this._readonly = stream.ReadIndex(19, this._readonly);
                this._SelectColor = stream.ReadIndex(20, this._SelectColor);
                this._SelectForceColor = stream.ReadIndex(21, this._SelectForceColor);
                this._VerticalAlignment = (StringAlignment)stream.ReadIndex(22, (int)this._VerticalAlignment);
                this._Visible = stream.ReadIndex(23, this._Visible);
                this._width = stream.ReadIndex(24, this._width);
                DataStruct ds = stream.ReadIndex(25, DataStruct.DataStructNull);
                this._totalmode = (TotalMode)stream.ReadIndex(26, (int)this._totalmode);
                this._totaltext = stream.ReadIndex(27, this._totaltext);
                this._totalvalue = stream.ReadBaseValueIndex(28, this._totalvalue);
                this._inhertreadonly = stream.ReadIndex(29, this._inhertreadonly);
            }

        }

        #endregion

        #region IField 成员
        private string _fieldname = string.Empty;
        [Category(CategorySetting.PropertyData)]
        public virtual string FieldName
        {
            get
            {
                return _fieldname;
            }
            set
            {
                _fieldname = value;
            }
        }

        #endregion

        #region IField 成员
        private string _datatype = string.Empty;
        [Category(CategorySetting.PropertyData)]
        public virtual string DataType
        {
            get
            {
                return _datatype;
            }
            set
            {
                _datatype = value;
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
        [Browsable(false)]
        [Category(CategorySetting.PropertyDesign)]
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
        [Browsable(false)]
        [Category(CategorySetting.PropertyDesign)]
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

        public override string ToString()
        {
#if DEBUG2
            return this.FieldName + " " + this.Caption;
#endif
            return this.Caption;
        }
    }

    public class GridViewSingleColumn: GridViewColumn
    {
        public GridViewSingleColumn(GridView grid) : base(grid)
        {

        }
        public override int Width { get { return this.Grid.Width; } }
    }
}

