using Feng.Data;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public abstract class BaseView : Feng.Forms.Interface.IDataStruct, Feng.Print.IPrint,IFont,IRect,IControlColor,IText,IViewEvent
    {
        public BaseView()
        {

        }

        public abstract void BindingControl(ViewControl ctl);
        private ViewControl control = null;
        public virtual ViewControl Control
        {
            get 
            { if (this.control != null)
                    return control;
                if (this.ParentView == null)
                    return null;
                return this.ParentView.Control;
            }
        }

        private Graphics graphics = null;
        public virtual Graphics GetGraphics()
        {
            if (this.Control == null)
                return null;
            if (graphics == null)
            {
                graphics = this.Control.CreateGraphics();
            }
            return graphics;
        }

        public BaseView RootView { get; set; } 
        public BaseView ParentView { get; set; }

        protected Font _font = null;
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

        protected int _zoom = 0;
        [Browsable(true)]
        public virtual int Zoom
        {
            get
            {
                return this._zoom;
            }
            set
            {
                _zoom = value;
            }
        }

        protected int _zlevel = 0;
        [Browsable(true)]
        public virtual int Zlevel
        {
            get
            {
                return this._zlevel;
            }
            set
            {
                _zlevel = value;
            }
        }
        private Point _location = Point.Empty;
        public virtual Point Location {
            get { return _location; }
            set { _location = value;
                this.Left = value.X;
                this.Top = value.Y;
            }
        }

        protected int _height = 0;
        [Browsable(true)]
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

        protected int _left = 0;
        [Browsable(true)]
        public virtual int Left
        {
            get
            {
                return _left;
            }
            set { 
                _left = value; 
            }

        }
        protected int _top = 0;
        [Browsable(true)]
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

        protected int _width = 0;
        [Browsable(true)]
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
                return new Rectangle(0, 0, this.Width, this.Height);
            }
        }

        [Browsable(true)]
        [Category(CategorySetting.PropertyDesign)]
        public virtual Rectangle Bounds
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
            }
        }

        private string _text = string.Empty;
        /// <summary> 
        /// </summary>
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual string Text
        {
            get
            {
                return _text;
            } 
            set
            {
                _text = value;
            }
        }

        protected Color _forecolor = Color.Black;
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
        protected Color _backcolor = Color.Empty;
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

        protected bool _readonly = false;
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


        private System.Windows.Forms.Padding _Padding = System.Windows.Forms.Padding.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public System.Windows.Forms.Padding Padding
        {
            get
            {
                return _Padding;
            }
            set
            {
                _Padding = value;
            }
        }

        #region 初始化

        public virtual void Init()
        {

        }

        #endregion

        #region 保存
 
         
        #endregion

        #region 绘制
        private int beginrefresh = 100;
        public virtual void BeginReFresh()
        {
            beginrefresh++;
        }
        private int endrefresh = 0;
        public virtual void EndReFresh()
        {
            endrefresh++;
            this.ReFresh();

        }

        public virtual void BeginReFresh(RectangleF rect)
        {
            _region.Union(rect);
            beginrefresh++;
        }
        public virtual void EndReFresh(RectangleF rect)
        {
            endrefresh++;
            this.RePaint(rect);
        }
        private System.Drawing.Region _region = new Region();
        public virtual void RePaint(RectangleF rect)
        {

            if (this.beginrefresh == endrefresh)
            {
                this.Invalidate(_region);
                _region.MakeEmpty();
                beginrefresh = endrefresh = 0;
            }

        }
        public virtual void Invalidate()
        {
            
        }
 
        public virtual void Invalidate(Rectangle rc)
        {

        }
        public virtual void Invalidate(Region region)
        {

        }
        public virtual void ReFresh()
        {
            if (this.beginrefresh == endrefresh)
            {
                this.Invalidate();
                beginrefresh = endrefresh = 0;
            }
        }

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        { 
            return false;
        }

        public virtual bool OnDrawBack(object sender, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }
        public virtual void BeginSetCursor(Cursor begincursor)
        {

        }

        #endregion

        #region 事件
        public virtual bool OnInit(object sender, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseHover(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {

            return false;
        }

        public virtual bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {

            return false;
        }

        public virtual bool OnMouseCaptureChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnClick(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessDialogChar(object sender, char charCode, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessDialogKey(object sender, Keys keyData, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessKeyEventArgs(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessKeyMessage(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessKeyPreview(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnDragEnter(object sender, DragEventArgs drgevent, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnDragDrop(object sender, DragEventArgs drgevent, EventViewArgs ve)
        {
            return false;
        }
      
        public virtual bool OnDragLeave(object sender, EventArgs drgevent, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnHandleCreated(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

 
        public virtual bool OnClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnKeyPress(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        { 
            return false;
        }

        public virtual bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnSetTextView(object sender, bool isfont,Font font,
            bool isforecolor,Color color,bool isbackcolor, Color backcolor,
            EventArgs e, EventViewArgs ve)
        {
            if (isfont)
            {
                this.Font = font;
            }
            if (isforecolor)
            {
                this.ForeColor = color;
            }
            if (isbackcolor)
            {
                this.BackColor = backcolor;
            }
            return false;
        }

        public virtual bool OnSetObj(object sender, object obj, EventArgs e, EventViewArgs ve)
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
        private bool lckmessage = false;
        public virtual bool SendMessage(BaseView view, object sender, ViewMessage message)
        {
            if (lckmessage)
                return true;
            lckmessage = true;
            try
            {
                return view.RecvMsg(this, sender, message);
            }
            finally
            {
                lckmessage = false;
            }
        }
        public virtual bool RecvMsg(BaseView view, object sender, ViewMessage message)
        {
            return false;
        }

        public virtual bool PostMessage(BaseView view, object sender, ViewMessage message)
        {
            return true;
        }


        public Point PointViewToControl(Point pt)
        {
            pt.X = this.Left + pt.X;
            pt.Y = this.Top + pt.Y;
            BaseView cadPanelView = this.ParentView;
            if (cadPanelView != null)
            {
                return cadPanelView.PointViewToControl(pt);
            }
            return pt;
        }

        public virtual bool CanFocus { get; set; }
        public virtual bool CanSelect { get; set; } = true;
        public virtual bool CanChangedParent { get; set; } = true;

        private object _value = 0;
        public virtual object Value { get { return _value; } set { _value = value; } }

        private int _index = 0;
        public virtual int Index { get { return _index; } set { _index = value; } }
        private bool _visable = true;
        public virtual bool Visable { get { return _visable; } set { _visable = value; } }

        public Rectangle ADRect { get { return this.Bounds; } }


        public virtual void ReadDataStruct(DataStruct data)
        {
            if (data == null)
                return;
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                //DataStruct ds = reader.ReadIndex(1, (DataStruct)null);
                //base.ReadDataStruct(ds);
                this.Font = reader.ReadIndex(11, this.Font);
                this.Zoom = reader.ReadIndex(12, this.Zoom);
                this.Top = reader.ReadIndex(13, this.Top);
                this.Left = reader.ReadIndex(14, this.Left);
                this.Zlevel = reader.ReadIndex(15, this.Zlevel);
                this.Height = reader.ReadIndex(16, this.Height);
                this.Width = reader.ReadIndex(17, this.Width);
                this.Text = reader.ReadIndex(18, this.Text);
                this.ForeColor = reader.ReadIndex(19, this.ForeColor);
                this.BackColor = reader.ReadIndex(20, this.BackColor); 
                this.CanFocus = reader.ReadIndex(61, CanFocus);
                this.CanSelect = reader.ReadIndex(62, CanSelect); 
                this.Index = reader.ReadIndex(77, this.Index);
                this.ViewID = reader.ReadIndex(78, this.ViewID);
            }
        }
 
        [Browsable(false)]
        public virtual DataStruct Data
        {
            get
            {
                DataStruct ds = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                { 
                    bw.Write(11, this.Font);
                    bw.Write(12, this.Zoom);
                    bw.Write(13, this.Top);
                    bw.Write(14, this.Left);
                    bw.Write(15, this.Zlevel);
                    bw.Write(16, this.Height);
                    bw.Write(17, this.Width);
                    bw.Write(18, this.Text);
                    bw.Write(19, this.ForeColor);
                    bw.Write(20, this.BackColor);

                    bw.Write(61, CanFocus);
                    bw.Write(62, CanSelect); 
                    bw.Write(77, this.Index);
                    bw.Write(78, this.ViewID);
                    ds.Data = bw.GetData();
                }
                return ds;
            }
        }



        private string viewid = string.Empty;
        public virtual string ViewID
        {
            get { return viewid; }
            set { viewid = value; }
        }



        public virtual bool InParentView(BaseView view)
        {
            if (this.Bounds.IntersectsWith(view.Bounds))
            {
                return true;
            }
            return false;
        }

        public static Point GetAtMainViewLocation(DivView divView)
        {
            Point pt = new Point(divView.Left, divView.Top);
            pt = GetAtMainViewLocation(divView, pt);
            return pt;
        }
        public static Point GetAtMainViewLocation(DivView divView, Point point)
        {
            Point pt = new Point(point.X, point.Y);
            BaseView baseView = divView.ParentView;
            for (int i = 0; i < 1000; i++)
            {
                if (baseView == null)
                {
                    break;
                }
                pt.X = pt.X + baseView.Left;
                pt.Y = pt.Y + baseView.Top;
                baseView = baseView.ParentView;
            }
            return pt;
        }
        public virtual Point GetAtMainViewLocation()
        {
            Point pt = new Point(this.Left, this.Top);
            pt = GetAtMainViewLocation(pt);
            return pt;
        }
        public virtual Point GetAtMainViewLocation(Point point)
        {
            Point pt = new Point(point.X, point.Y);
            BaseView baseView = this.ParentView;
            for (int i = 0; i < 1000; i++)
            {
                if (baseView == null)
                {
                    break;
                }
                pt.X = pt.X + baseView.Left;
                pt.Y = pt.Y + baseView.Top;
                baseView = baseView.ParentView;
            }
            return pt;
        }
        public virtual Point GetMainViewPointAtThis(Point point)
        {
            int left = this.Left;
            int top = this.Top;
            BaseView baseView = this.ParentView;
            for (int i = 0; i < 1000; i++)
            {
                if (baseView == null)
                {
                    break;
                }
                left = left + baseView.Left;
                top = top + baseView.Top;
                baseView = baseView.ParentView;
            }
            Point pt = new Point(point.X - left, point.Y - top);
            return pt;
        }
    }

}

