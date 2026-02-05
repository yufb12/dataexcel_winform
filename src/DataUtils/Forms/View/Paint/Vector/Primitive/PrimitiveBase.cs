using Feng.Data;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views.Vector
{
    public abstract class PrimitiveBase : Feng.Forms.Interface.IDataStruct, Feng.Print.IPrint, IDraw, IBounds 
    {
        public PrimitiveBase()
        {

        }
 

        #region 属性

        private Font _font = null;
        [Browsable(true)]
        [Category(CategorySetting.PropertyUI)]
        public virtual Font Font
        {
            get
            {
                return _font;
            }

            set
            {
                _font = value;
            }
        }

        #region IBounds 成员
        private int _height = 0;
        [Browsable(false)]
        public virtual int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                _height = value;
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
        private int _top = 0;
        [Browsable(false)]
        public virtual int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                _top = value;
            }
        }

        private int _width = 0;
        [Browsable(false)]
        public virtual int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual Rectangle Rect
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
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
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }
        #endregion

        private bool _readonly = false;
        [Browsable(true)]
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
            }
        }

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
        #endregion

        #region 初始化

        public virtual void Init()
        {

        }

        #endregion

        #region 保存


        [Browsable(false)]
        public abstract DataStruct Data { get; }


        public abstract void ReadDataStruct(DataStruct data);
        #endregion

        #region 绘制
        private int _BeginReFresh = 100;
        public virtual void BeginReFresh()
        {
            _BeginReFresh++;
        }
        private int _EndReFresh = 0;
        public virtual void EndReFresh()
        {
            _EndReFresh++;
            this.RePaint();

        }

        public virtual void BeginReFresh(RectangleF rect)
        {
            _region.Union(rect);
            _BeginReFresh++;
        }
        public virtual void EndReFresh(RectangleF rect)
        {
            _EndReFresh++;
            this.RePaint(rect);
        }
        private System.Drawing.Region _region = new Region();
        public virtual void RePaint(RectangleF rect)
        {

            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate(_region);
                _region.MakeEmpty();
                _BeginReFresh = _EndReFresh = 0;
            }

        }
        public virtual void Invalidate()
        {

        }
        public virtual void Invalidate(bool invalidateChildren)
        {

        }
        public virtual void Invalidate(Rectangle rc)
        {

        }
        public void Invalidate(Region region)
        {

        }
        public virtual void RePaint()
        {
            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate(true);
                _BeginReFresh = _EndReFresh = 0;
            }
        }

        public abstract bool OnDraw(object sender, Feng.Drawing.GraphicsObject g);
   
        public virtual void BeginSetCursor(Cursor begincursor)
        {

        } 
 
        #endregion

        #region 事件
 
        public virtual bool OnCellMouseDown(object sender, MouseEventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellMouseUp(object sender, MouseEventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellMouseMove(object sender, MouseEventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellMouseLeave(object sender, EventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellMouseHover(object sender, EventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellMouseEnter(object sender, EventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellMouseDoubleClick(object sender, MouseEventArgs e)
        {
          
            return false;
        }

        public virtual bool OnCellMouseClick(object sender, MouseEventArgs e)
        {
      
            return false;
        } 

        public virtual bool OnCellMouseCaptureChanged(object sender, EventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellMouseWheel(object sender, MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnCellClick(object sender, EventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellKeyDown(object sender, KeyEventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellKeyPress(object sender, KeyPressEventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellKeyUp(object sender, KeyEventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellDoubleClick(object sender, EventArgs e)
        { 
            return false;
        }

        public virtual bool OnCellPreProcessMessage(object sender, ref Message msg)
        { 
            return false;
        }

        public virtual bool OnCellProcessCmdKey(object sender, ref Message msg, Keys keyData)
        { 
            return false;
        }

        public virtual bool OnCellProcessDialogChar(object sender, char charCode)
        { 
            return false;
        }

        public virtual bool OnCellProcessDialogKey(object sender, Keys keyData)
        { 
            return false;
        }

        public virtual bool OnCellProcessKeyEventArgs(object sender, ref Message m)
        { 
            return false;
        }

        public virtual bool OnProcessKeyMessage(object sender, ref Message m)
        { 
            return false;
        }

        public virtual bool OnProcessKeyPreview(object sender, ref Message m)
        { 
            return false;
        }

        public virtual bool OnCellWndProc(object sender, ref Message m)
        {
            return false;
        }

        #endregion

        #region 打印
        public virtual bool Print(Print.PrintArgs e)
        {
            return false;
        }
        #endregion


    }
}

