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

using Feng.Utils;
using Feng.Forms.ComponentModel;
using System.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Interface;
using Feng.Forms.Views;
using Feng.Data;

namespace Feng.Forms.Controls.GridControl
{
    [Serializable]
    [DefaultProperty("Value")]
    public class GridViewCell : DivView, IFindControl, IEndEdit, IInitEdit, IValue, IBounds, 
        IPointToClient, IPointToScreen, IPointToControl, IOnKeyDown
    {
        #region 系统属性

        private bool _AutoMultiline = false;
        /// <summary>
        /// 是否自动绘制多行。True时绘制多行，False时不绘制多行。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AutoMultiline
        {
            get { return this._AutoMultiline; }

            set { this._AutoMultiline = value; }
        }

        #endregion

        /// <summary>
        /// 单元格有：行，列属性对应相应的行与列。
        /// 有文本(text)与值(value)两个属性分别对应的是文本与相应的值。
        /// 
        /// </summary>
        /// <param name="grid"></param>
        public GridViewCell(GridViewRow row)
        {
            this._row = row;
        }

        private void init()
        {

        }

        #region 界面绘制

        #region IDraw 成员

        public virtual bool OnDraw(Feng.Drawing.GraphicsObject g)
        {
            try
            {
                ///////////////////////////代码加在中间 
                DrawRect(g, this.Rect, this.Text);
                /////////////////////////// 
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }
            return false;
        }

        public virtual void DrawRect(Feng.Drawing.GraphicsObject g, RectangleF bounds, object value)
        {
            if (this.Selected)
            {
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, this.Grid.CellSelectBrush, this.Rect);
            }
            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            g.Graphics.Clip.Xor(bounds);
            DrawCell(g, bounds, value);
            g.Graphics.Restore(gs);
        }

        private bool _FunctionBorder = false;
        /// <summary>
        /// 当单元格为函数编辑状态时，是否在单元格周围显示函数提示边框。
        /// </summary>
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public virtual bool FunctionBorder
        {
            get { return _FunctionBorder; }
            set { _FunctionBorder = value; }
        }


        /// <summary>
        /// 绘制单元格内容。value为Null时不绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="value"></param>
        private void DrawCell(Feng.Drawing.GraphicsObject g, RectangleF bounds, object value)
        {
            if (this.InEdit)
            {
                return;
            }
            if (this.Grid.ShowLines)
            {
                g.Graphics.DrawLine(PenCache.BorderGray, this.Right, this.Top,
                    this.Right, this.Bottom);

                g.Graphics.DrawLine(PenCache.BorderGray, this.Left, this.Bottom,
                    this.Right, this.Bottom);
            }
            string text = Feng.Utils.ConvertHelper.ToString(value);
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(
                 this.Column.HorizontalAlignment, this.Column.VerticalAlignment, this.Column.DirectionVertical);

            bounds.Offset(1, 1);
            bounds.Inflate(-1, -3);
            RectangleF rect = bounds;

            Color forecolor = this.Grid.ForeColor;

            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                rect.Location = new PointF(rect.Location.X, rect.Location.Y);
                if (this.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Column.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Column.Font, sb, rect, sf);

                }
            }
        }
        public virtual void DrawBack(Feng.Drawing.GraphicsObject g)
        {

        }

        #region IPrintBorder 成员


        #endregion
        #endregion

        #region IOnDrawBack 成员

        public virtual bool OnDrawBack(Graphics g)
        {
            return false;
        }

        #endregion
 

        #region IPrintText 成员

        private bool _printtext = true;
        /// <summary>
        /// 套打，指定是否打印文字内容。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        public virtual bool IsPrintText
        {
            get
            {
                return this._printtext;
            }
            set
            {
                this._printtext = value;
            }
        }

        #endregion

        #region IPrintBackImage 成员
        private bool _PrintBackImage = true;

        /// <summary>
        /// 套打，指定是否打印图象内容。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        public virtual bool IsPrintBackImage
        {
            get
            {
                return this._PrintBackImage;
            }
            set
            {
                this._PrintBackImage = value;
            }
        }

        #endregion

        #region IPrintBorder 成员
        private bool _PrintBorder = true;

        /// <summary>
        /// 套打，指定是否打印边框。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        public virtual bool IsPrintBorder
        {
            get
            {
                return this._PrintBorder;
            }
            set
            {
                this._PrintBorder = value;
            }
        }

        #endregion

        #region IPrintBackColor 成员
        private bool _PrintBackColor = true;

        /// <summary>
        /// 套打，指定是否打印背景颜色。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        public virtual bool IsPrintBackColor
        {
            get
            {
                return this._PrintBackColor;
            }
            set
            {
                this._PrintBackColor = value;
            }
        }

        #endregion

        #endregion

        #region IDataExcelGrid 成员

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public GridView Grid
        {
            get { return this.Row.Grid; }
        }
        #endregion

        #region IBounds 成员

        [Browsable(false)]
        public int Top
        {
            get
            {
                return this.Row.Top;
            }
            set
            {
            }
        }
        [Browsable(false)]
        public int Left
        {
            get
            {
                return this.Column.Left;
            }
            set
            {
            }
        }
        [Browsable(false)]
        public int Bottom
        {
            get { return Top + this.Height; }
        }
        [Browsable(false)]
        public int Right
        {
            get { return Left + this.Width; }
        }
        [Browsable(false)]
        public int Width
        {
            get
            {
                int width = this.Column.Width;

                return width;
            }
            set { }
        }
        [Browsable(false)]
        public int Height
        {
            get
            {
                return this.Row.Height;
            }
            set { }
        }

        [Browsable(true)]
        public Rectangle Rect
        {
            get
            {
                Rectangle rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                return rect;
            }
        }
        #endregion

        #region ISelected 成员 
        [Browsable(false)]
        public virtual bool Selected
        {
            get
            {
                if (this.Grid.SelectCells != null)
                {
                    foreach (GridViewCell cell in this.Grid.SelectCells)
                    {
                        if (cell == this)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        #endregion

        #region IToString 成员
        public override string ToString()
        {
            return this._value == null ? string.Empty : this._value.ToString();

        }


        #endregion

        /// <summary>
        /// 触发单元格值更改事件。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool OnBeforeValueChanged(object value)
        {
            //BeforeCellValueChangedArgs e = new BeforeCellValueChangedArgs()
            //{
            //    Cell = this,
            //    NewValue = value
            //};

            //this.Grid.OnBeforeCellValueChanged(e);
            //if (e.Cancel)
            //{
            //    return false;
            //}
            return true;
        }

        public virtual void OnValueChanged(object value)
        {
            this.Grid.BeginReFresh();
            this._value = value;
            if (this.Column != null)
            {
                this._text = TextHelper.Format(this._value, this.Column.FormatType, this.Column.FormatString);
            }
            this.Grid.SetDataBingdingValue(this.Row.Index - 1, this.Column.FieldName, value);
            this.Grid.EndReFresh();
        }
        public virtual void InitValue(object value)
        {
            this._value = value;
            if (this.Column != null)
            {
                this._text = TextHelper.Format(this._value, this.Column.FormatType, this.Column.FormatString);
            }
        }
        #region ICurrentRow 成员

        private GridViewRow _row = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [ReadOnly(true)]
        public GridViewRow Row
        {
            get
            {
                return _row;
            }
        }

        #endregion

        #region ICurrentColumn 成员

        private GridViewColumn _column = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        [ReadOnly(true)]
        public GridViewColumn Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }

        #endregion

        #region IValue 成员
        private object _value = null;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {

                if (this._value != value)
                {
                    if (this.OnBeforeValueChanged(value))
                    {
                        this.OnValueChanged(value);
                    }
                }

            }
        }

        private string _text = string.Empty;

        public string Text
        {
            get
            {
                return _text;
            }
        }
         
        [Browsable(false)]
        public virtual Feng.Forms.Interface.IEditView OwnEditControl
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return this.Column.OwnEditControl;
            }
            set
            { 
            }
        }
        #endregion

        #region ICellEvents 成员
 
        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if ((this.Column.EditMode & EditMode.Default) == EditMode.Default)
            {
                this.Grid.InitEdit();
            }
            if ((this.Column.EditMode & EditMode.DoubleClick) == EditMode.DoubleClick)
            {
                this.Grid.InitEdit();
            }
            if (this.Column.OwnEditControl != null)
            {
                IOnMouseDoubleClick ionmousedoubleclick = this.Column.OwnEditControl as IOnMouseDoubleClick;
                if (ionmousedoubleclick != null)
                {
                    if (ionmousedoubleclick.OnMouseDoubleClick(this, e, ve))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return this.Grid.OnGridViewCellClick(e, this);
        }
 

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            if (this.OwnEditControl != null)
            {
                IOnKeyDown onKeyDown = this.OwnEditControl as IOnKeyDown;
                if (onKeyDown != null)
                {
                    bool res = onKeyDown.OnKeyDown(sender, e,ve);
                    if (res)
                        return true;
                }
            }
            if (e.KeyCode == Keys.Tab)
            {
                this.Grid.MoveFocusedCellToRightCell();
            } 
            return false;
        }

 
 
        public const int WM_KEYDOWN = 0x100;
 

        #endregion

        #region IInitEdit 成员
        public virtual bool InitEdit()
        {
            return InitEdit(this.Grid);

        }
        public virtual bool InitEdit(object parent)
        {
            if (this.InEdit)
            {
                return false;
            }
            BeforeInitEditCancelArgs e = new BeforeInitEditCancelArgs(this);
            this.Grid.OnBeforeCellInitEdit(e);
            if (e.Cancel)
            {
                return false;
            }
            this.Grid.EditCell = this;
            if (this.Column.ReadOnly)
            {
                return false;
            }

            if (this.Column.OwnEditControl != null)
            {
                this.Column.OwnEditControl.InitEdit(this);
            }
            else
            {
                Feng.Forms.Interface.IEditView edit = this.Grid.DefaultEdit;
                if (Feng.Utils.ConvertHelper.IsNumberType(this.Column.DataType))
                {
                    edit = new CellNumber();
                    this.Column.OwnEditControl = edit;
                }
                else if (this.Column.DataType == typeof(DateTime).FullName)
                {
                    edit = new CellDateTime();
                    this.Column.OwnEditControl = edit;
                }
                else
                {
                    this.Column.OwnEditControl = edit;
                }
                edit.InitEdit(this);

            }
            return this.InEdit;

        }

        #endregion

        #region IEndEdit 成员

        public virtual void EndEdit()
        {
            if (this.Column.OwnEditControl != null)
            {
                this.Column.OwnEditControl.EndEdit();
            }
        }

        public Control FindControl()
        {
            return this.Grid.Control;
        }

        #endregion

        #region IInEdit 成员
        [Browsable(false)]
        [Category(CategorySetting.Design)]
        public virtual bool InEdit
        {
            get
            {
                return (this.Grid.EditCell == this);
            }
        }

        #endregion

        #region IIsMergeCell 成员
        [Browsable(false)]
        public virtual bool IsMergeCell
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region ILocation 成员

        public Point Location
        {
            get { return this.Rect.Location; }
        }

        #endregion

        public virtual Point PointToClient(Point pt)
        {
           
            return this.Grid.PointToClient(pt);
        }

        public virtual Point PointToScreen(Point pt)
        {
            return this.Grid.PointToScreen(pt);
        }

        public virtual Point PointToControl(Point pt)
        {
            return this.Grid.PointToControl(pt);
        }

        public override DataStruct Data { get { return null; } }
    }
}

