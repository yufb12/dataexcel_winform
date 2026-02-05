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

namespace Feng.Forms.Controls
{
     [ToolboxItem(false)]
    public class ListImageView : System.Windows.Forms.Control 
    {
        public ListImageView()
        {
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles(); 
        }
 
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            try
            { 
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                OnDraw(g);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnPaint(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseDown(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnSizeChanged(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        { 
            base.OnMouseWheel(e);
        }
 
        public void OnDraw(Graphics g)
        {
            foreach (ListImageViewItem item in this.Items)
            {
                item.OnDraw(g);
            }
        }
        public void PropertyChanged()
        {

        }
        private ListImageViewItemCollection _items = null;
        public ListImageViewItemCollection Items
        {
            get {
                if (_items == null)
                {
                    _items = new ListImageViewItemCollection();
                }
                return this._items;
            }
        }

        #region 文字边框

        private StringAlignment _alignment = StringAlignment.Near;
        public virtual StringAlignment Alignment
        {
            get { return _alignment; }
            set
            {
                _alignment = value;
                 GenericDefault.Alignment = this.Alignment;
                PropertyChanged();
            }
        }
        private StringAlignment _lineAlignment = StringAlignment.Center;
        public virtual StringAlignment LineAlignment
        {
            get { return _lineAlignment; }

            set
            {
                _lineAlignment = value;
                 GenericDefault.LineAlignment = this.LineAlignment;
                PropertyChanged();
            }
        }
        private StringFormat _GenericDefault;
        private StringFormat GenericDefault
        {
            get
            {
                if (_GenericDefault == null)
                {
                    _GenericDefault = StringFormat.GenericDefault.Clone() as StringFormat;
                    _GenericDefault.Alignment = this.Alignment;
                    _GenericDefault.LineAlignment = this.LineAlignment;
                }
                return _GenericDefault;
            }
        }
        private Color _color1 = Color.White;
        [Category(CategorySetting.PropertyDesign)]
        public Color Color1
        {
            get { return _color1; }
            set { _color1 = value; }
        }
        private Color _color2 = Color.Lavender;
        [Category(CategorySetting.PropertyDesign)]

        public Color Color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }
        private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
        [Category(CategorySetting.PropertyDesign)]
        public LinearGradientMode GradientMode
        {
            get { return _GradientMode; }
            set { _GradientMode = value; }
        }
        private bool _drawborder = true;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(true)]
        public bool DrawBorder
        {
            get { return _drawborder; }
            set { _drawborder = value; }
        }
        private int _borderwidth = 1;
        [DefaultValue(1)]
        [Category(CategorySetting.PropertyDesign)]
        public int BorderWidth
        {
            get { return _borderwidth; }
            set { _borderwidth = value; }
        }
        private Color _bordercolor = Color.DarkGray;
        [Category(CategorySetting.PropertyDesign)]
        public Color BorderColor
        {
            get { return _bordercolor; }
            set { _bordercolor = value; }
        }
        private int _radius = 6;

        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(6)]
        public int Radius
        {

            get { return _radius; }
            set { _radius = value; }
        }

        #endregion

    }
 
}
