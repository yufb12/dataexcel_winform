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
using Feng.Forms.Popup;

namespace Feng.Forms.Controls
{
    [ToolboxItem(true)]
    public class ToolBar : System.Windows.Forms.Panel
    {
        public ToolBar()
        {
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();
            if (this.DesignMode)
            {
                ToolBarItem item = new ToolBarItem("添加");
                this.Items.Add(item);
            }
            this.Height = 25;
            this.SizeChanged += ToolBar_SizeChanged;
        }

        void ToolBar_SizeChanged(object sender, EventArgs e)
        {
            this.Height = ToolBarHeight;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                Feng.Drawing.GraphicsObject graphicsobject = Feng.Drawing.GraphicsObjectCache.Get(this.GetType().FullName);
                graphicsobject.Control = this;
                graphicsobject.ModifierKeys = System.Windows.Forms.Control.ModifierKeys;
                graphicsobject.MouseButtons = System.Windows.Forms.Control.MouseButtons;
                graphicsobject.MousePoint = System.Windows.Forms.Control.MousePosition;
                graphicsobject.WorkArea = new Rectangle(0, 0, this.Width, this.Height);
                graphicsobject.ClipRectangle = e.ClipRectangle;
                graphicsobject.ClientPoint = this.PointToClient(System.Windows.Forms.Control.MousePosition);
                graphicsobject.Graphics = e.Graphics;
                OnDraw(graphicsobject);
                graphicsobject.Dispose();
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Controls", "ToolBar", "OnPaint", ex);
                Feng.IO.LogHelper.Log(ex);
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
            try
            {
                ToolBarItem item = GetItem(e.Location);
                if (item != null)
                {
                    item.OnMouseMove(e);
                    if (item.ShowToolTip)
                    {
                        Feng.Forms.Popup.ToolTip.Show(item.ToolTip);
                    }
                }
                else
                {
                    Feng.Forms.Popup.ToolTip.HideToolTip();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Controls", "ToolBar", "OnMouseMove", ex);
                Feng.IO.LogHelper.Log(ex);
            }
            this.Invalidate();
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                Feng.Forms.Popup.ToolTip.HideToolTip();
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Controls", "ToolBar", "OnMouseMove", ex);
                Feng.IO.LogHelper.Log(ex);
            }
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        private ToolBarItemDropDownForm dropdownform = null;
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }
        private System.DateTime lastclicktime = DateTime.Now;
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if ((DateTime.Now - lastclicktime).TotalMilliseconds < 300)
                {
                    base.OnMouseDoubleClick(e);
                }
                lastclicktime = DateTime.Now;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (this.BarItemFooter.Rect.Contains(e.Location))
                    {
                        if (dropdownform == null)
                        {
                            dropdownform = new ToolBarItemDropDownForm(this);
                            dropdownform.VisibleChanged += dropdownform_VisibleChanged;
                        }
                        if (dropdownform.IsDisposed)
                        {
                            dropdownform = new ToolBarItemDropDownForm(this);
                            dropdownform.VisibleChanged += dropdownform_VisibleChanged;
                        }
                        dropdownform.Init(this.listhide);
                        dropdownform.ParentEditForm = this.FindForm();
                        Point pt = this.PointToScreen(new Point(this.BarItemFooter.Rect.Right - dropdownform.Width, this.BarItemFooter.Rect.Bottom));
                        dropdownform.Popup(pt);
                    }
                    ToolBarItem item = GetItem(e.Location);
                    if (item != null)
                    {

                        Rectangle rect = item.GetCloseRect();
                        if (rect.Contains(e.Location))
                        {
                            this.Items.Remove(item);
                            this.ReSetItemPoint();
                        }
                        else
                        {
                            this.OnToolBarItemClick(item);
                            OnItemClick(item);
                            ProcItemClick(item);
                            this.FocusItem = item;
                        }
                    }
                }
                base.OnMouseDown(e);
                this.Invalidate();
            }
            catch (Exception ex)
            {
                base.OnMouseDown(e);
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Controls", "ToolBar", "OnMouseDown", ex);
                Feng.IO.LogHelper.Log(ex);
            }
        }
        private bool _allowdelete = false;
        public virtual bool AllowDelete
        {
            get {
                return this._allowdelete;
            }
            set
            {
                this._allowdelete=value;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (AllowDelete)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Controls", "ToolBar", "OnKeyUp", ex);
            }
            base.OnKeyUp(e);
        }
        public void ShowDropDownItems(ToolBarItem item)
        {
            if (dropdownform == null)
            {
                dropdownform = new ToolBarItemDropDownForm(this);
                dropdownform.VisibleChanged += dropdownform_VisibleChanged;
            }
            if (dropdownform.IsDisposed)
            {
                dropdownform = new ToolBarItemDropDownForm(this);
                dropdownform.VisibleChanged += dropdownform_VisibleChanged;
            }
            dropdownform.Init(item.Items);
            dropdownform.ParentEditForm = this.FindForm();
            Point pt = this.PointToScreen(new Point(item.Rect.Left, item.Rect.Bottom));
            dropdownform.Popup(pt);
            popupitem = item;
        }
        private ToolBarItem popupitem = null;
        void dropdownform_VisibleChanged(object sender, EventArgs e)
        {
            if (!dropdownform.Visible)
            {
                popupitem = null;
            }
        }
        public ToolBarItem GetItem(Point pt)
        {
            foreach (ToolBarItem item in this.Items)
            {
                if (item.Rect.Contains(pt))
                {
                    return item;
                }
            }
            return null;
        }
 
        protected override bool ProcessMnemonic(char charCode)
        {
            try
            { 
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
                {
                    string text = ("(" + charCode + ")").ToUpper();
                    foreach (ToolBarItem item in Items)
                    {
                        if (item.Text.Contains(text))
                        {
                            this.OnItemClick(item);
                            ProcItemClick(item);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
            return base.ProcessMnemonic(charCode);
        }
        public delegate void ItemClickHandler(object sender, ToolBarItem item);
        public event ItemClickHandler ItemClick;
        public virtual void OnItemClick(ToolBarItem item)
        {
            if (ItemClick != null)
            {
                ItemClick(this, item);
            }
        }
        public virtual void ProcItemClick(ToolBarItem item)
        {
            if (this.popupitem == item)
            {
                if (dropdownform != null)
                {
                    dropdownform.Visible = false;
                }
            }
            else if (item.Items.Count > 0)
            {
                ShowDropDownItems(item);
            }
        }
        public void ReSetItemSize()
        {
            Graphics g = this.CreateGraphics();
            foreach (ToolBarItem item in this.Items)
            {
                item.ReSzie(g);

            }
            g.Dispose();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            try
            {

                ReSize();
                base.OnSizeChanged(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Controls", "ToolBar", "OnSizeChanged", ex);
            }
        }
        public void ReSize()
        {

            ReSetItemSize();
            ReSetItemPoint();
            this.Invalidate();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }
        private Feng.Forms.Skins.ToolBarItemSkin _skin = null;
        public Feng.Forms.Skins.ToolBarItemSkin Skin
        {
            get
            {
                if (_skin == null)
                {
                    _skin = new Skins.ToolBarItemSkin();
                }
                return _skin;
            }
            set
            {
                _skin = value;
            }
        }
        public void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            Skin.BarBackColor = this.BackColor;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Skin.DrawBarBack(g.Graphics, rect);
            BarItemHeader.OnDraw(g);
            foreach (ToolBarItem item in listvisable)
            {
                if (item.Visable)
                {
                    item.OnDraw(g);
                }
            }
            if (listhide.Count > 0)
            {
                BarItemFooter.OnDraw(g);
            }
            if (this.newItem != null)
            {
                this.newItem.OnDraw(g);
            }
        }
        private List<ToolBarItem> listvisable = new List<ToolBarItem>();
        private List<ToolBarItem> listhide = new List<ToolBarItem>();
        public void ReSetItemPoint()
        {
            listvisable.Clear();
            listhide.Clear();
            int left = BarItemHeader.Width;
            int width = this.Width - BarItemFooter.Width - 1;
            int right = width;
            BarItemHeader.Left = 0;
            BarItemHeader.Top = 0;
            BarItemFooter.Left = width;
            BarItemFooter.Top = 0;

            foreach (ToolBarItem item in this.Items)
            {
                if (item == this.BarItemHeader)
                    continue;
                if (item == this.BarItemFooter)
                    continue;
                if (item == this.newItem)
                    continue; 
                if (!item.Visable)
                {
                    continue;
                }
                if (item.AlignRight)
                {
                    item.Left = right - item.Width;
                    item.Top = 0;
                    right = right - item.Width;
                    if (Overflow)
                    {
                        if (right < left)
                        {
                            listhide.Add(item);
                        }
                        else
                        {
                            listvisable.Add(item);
                        }
                    }
                    else
                    {
                        listvisable.Add(item);
                    }
                }
            }

            left = BarItemHeader.Width;
            foreach (ToolBarItem item in this.Items)
            {
                if (item == this.BarItemHeader)
                    continue;
                if (item == this.BarItemFooter)
                    continue;
                if (item == this.newItem)
                    continue;
                if (!item.Visable)
                {
                    continue;
                }
                if (!item.AlignRight)
                {
                    item.Left = left;
                    item.Top = 0;
                    left = left + item.Width;
                    if (Overflow)
                    {
                        if (left > right)
                        {
                            listhide.Add(item);
                        }
                        else
                        {
                            listvisable.Add(item);
                        }
                    }
                    else
                    {
                        listvisable.Add(item);
                    }
                }
            }
            if (this.newItem != null)
            {
                this.newItem.Left = left;
                this.newItem.Top = 0;
            }


        }
        private ToolBarItemCollection _items = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        public ToolBarItemCollection Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ToolBarItemCollection(this);
                }
                return _items;
            }
        }
        public void HideDropDown()
        {
            if (dropdownform != null)
            {
                dropdownform.Hide();
            }
        }
        private void ItemChanged(ToolBarItem item)
        {

            try
            {
                Graphics g = this.CreateGraphics();
                Feng.Forms.Skins.ToolBarItemSkin.Default.SizeItem(g, item);
                g.Dispose();
                ReSetItemPoint();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataUtils", "ToolBar", "ItemChanged", ex);
            }
            this.Invalidate();
        }

        private ToolBarItem _baritemheader = null;
        public virtual ToolBarItem BarItemHeader
        {
            get
            {
                if (_baritemheader == null)
                {
                    _baritemheader = new ToolBarItemHeader();
                    _baritemheader.ToolBar = this;
                }
                return _baritemheader;
            }
        }


        private ToolBarItem _focusitem = null;
        public virtual ToolBarItem FocusItem
        {
            get
            {
                return _focusitem;
            }
            set
            {
                _focusitem = value;
            }
        }


        private ToolBarItem _newitem = null;
        public virtual ToolBarItem newItem
        {
            get
            {
                return _newitem;
            }
            set
            {
                _newitem = value;
            }
        }

        private ToolBarItem _baritemfooter = null;
        public virtual ToolBarItem BarItemFooter
        {
            get
            {

                if (_baritemfooter == null)
                {
                    _baritemfooter = new ToolBarItemFooter();
                    _baritemfooter.ToolBar = this;
                }
                return _baritemfooter;
            }
            set
            {
                _baritemfooter = value;
            }
        }
        public event ToolBarItemChangedHandler ToolBarItemChanged = null;
        public event ToolBarItemClickHandler ToolBarItemClick = null;
        /// <summary>
        /// OnToolBarItemChanged ChangedReason 触发原因
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Reason">ChangedReason 触发原因</param>
        public virtual void OnToolBarItemChanged(ToolBarItem item, int Reason)
        {
            if (Reason < Feng.Forms.Controls.GridControl.ChangedReason.Remove)
            {
                ItemChanged(item);
            }
            if (ToolBarItemChanged != null)
            {
                ToolBarItemChanged(this, item, Reason);
            }
        }
        public virtual void OnToolBarItemClick(ToolBarItem item)
        {
            if (ToolBarItemClick != null)
            {
                ToolBarItemClick(this, item);
            }
        }

        private int _ToolBarHeight = 25;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(25)]
        public virtual int ToolBarHeight
        {
            get
            {
                if (_ToolBarHeight < 10)
                {
                    _ToolBarHeight = 18;
                }
                return _ToolBarHeight;
            }
            set
            {
                _ToolBarHeight = value;
            }
        }

        private int _ImageSize = 20;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(20)]
        public virtual int ImageSize
        {
            get
            {
                return _ImageSize;
            }
            set
            {
                _ImageSize = value;
            }
        }
        private int _PaddingLeft = 15;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(15)]
        public virtual int PaddingLeft
        {
            get
            {
                return _PaddingLeft;
            }
            set
            {
                _PaddingLeft = value;
            }
        }

        private bool _Overflow = true;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DefaultValue(true)]
        public virtual bool Overflow
        {
            get
            {
                return this._Overflow;
            }
            set
            {
                this._Overflow = value;
            }
        }


        public int GetItemSize()
        {
            int w = this.BarItemHeader.Width;
            w = w + BarItemFooter.Width;
            foreach (ToolBarItem item in this.Items)
            {
                w = w + item.Width;
            }
            return w;
        }

        public ToolBarItem Get(string v)
        {
            foreach (ToolBarItem item in this.Items)
            {
                if (item.ID == v)
                {
                    return item;
                }
            }
            return null;
        }
        public ToolBarItem Get(string v, ToolBarItem pitem)
        {
            foreach (ToolBarItem item in pitem.Items)
            {
                if (item.ID == v)
                {
                    return item;
                }
            }
            return null;
        }
    }

    public delegate void ToolBarItemChangedHandler(object sender, ToolBarItem item, int Reason);
    public delegate void ToolBarItemClickHandler(object sender, ToolBarItem item);

}
