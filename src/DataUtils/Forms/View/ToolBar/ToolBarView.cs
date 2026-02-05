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
using Feng.Data;
using Feng.Forms.Interface;

namespace Feng.Forms.Views
{
    public class ToolBarView : DivView, IDraw
    {
        public ToolBarView()
        { 
        }

        void control_SizeChanged(object sender, EventArgs e)
        {
            this.Width = Control.Width;
            this.Height = Control.Height;
            RefreshItemLocation();
        }
        public int ItemWidth = 72;
        public virtual void RefreshItemLocation()
        {
            if (this.Items.Count > 0)
            {
                ItemWidth = this.Width / this.Items.Count;
            }
            else
            {
                ItemWidth = 72;
            }
            int left = 0;
            foreach (ToolBarItemView item in this.Items)
            {
                item.Left = left;
                item.Width = ItemWidth;
                left = left + ItemWidth;
            }

        }

        #region 属性
 
        private ToolBarItemView _FocusedItem = null;
        public ToolBarItemView FocusedItem
        {
            get {
                return _FocusedItem;
            }
            set {
                _FocusedItem = value;
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
        private Color _FocusBackColor = Color.CornflowerBlue;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public Color FocusBackColor
        {
            get
            {
                if (_FocusBackColor == Color.Empty)
                {
                    return ColorHelper.FocusColor;
                }

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
        public override DataStruct Data
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
                    bw.Write(2, this._backcolor);
                    bw.Write(8, this._font);
                    bw.Write(9, this._forecolor);
                    bw.Write(12, this._height);
                    bw.Write(13, this._left);
                    bw.Write(19, this._top);
                    bw.Write(20, this._width);
                    bw.Write(23, this._readonly);
                    data.Data = bw.GetData();
                    bw.Close();
                }

                return data;
            }
        }

        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.IO.BufferReader stream = new IO.BufferReader(data.Data))
            {
                this._backcolor = stream.ReadIndex(2, this._backcolor);
                this._font = stream.ReadIndex(8, this._font);
                this._forecolor = stream.ReadIndex(9, this._forecolor);
                this._height = stream.ReadIndex(12, this._height);
                this._left = stream.ReadIndex(13, this._left);
                this._top = stream.ReadIndex(19, this._top);
                this._width = stream.ReadIndex(20, this._width);
                this._readonly = stream.ReadIndex(22, this._readonly);
            }
        }
        #endregion

        #region 绘制

        public virtual void DrawBorder(GraphicsObject g)
        {
            Feng.Drawing.GraphicsHelper.DrawBorder(g.Graphics, Rect);
        }
        public virtual void DrawBackGround(GraphicsObject g)
        {
            if (this.BackColor != Color.Empty)
            {
                Feng.Drawing.GraphicsHelper.FillRectangleLinearGradient(g.Graphics, this.BackColor, Rect);
            }
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            DrawBackGround(g);
            foreach (ToolBarItemView item in Items)
            {
                item.OnDraw(this, g);
            }
            //DrawBorder(g);
            return false;
        }
  
        public virtual void Invalidate(bool invalidateChildren)
        {

        }
 

  

        #endregion

        #region 事件
 

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            this.AddDefaultItem();
            return false;
        }
 
 

        #endregion
 

        private ToolBarItemViewCollection _items = null;
        public ToolBarItemViewCollection Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ToolBarItemViewCollection();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }
        public ToolBarItemView RightItem
        {
            get
            {
                if (Items.Count > 0)
                {
                    return Items[Items.Count - 1];
                }
                return null;
            }
        }
        public int ConntentsLeft = 0;
        public void AddItem(ToolBarItemView item)
        {
            int width = item.CalculateWidth();
            int left = ConntentsLeft;
            if (RightItem != null)
            {
                left = RightItem.Right;
            }
            item.Left = left;
            Items.Add(item);
        }

        public void AddDefaultItem()
        {
            ToolBarItemView item = new ToolBarItemView(this);
            item.Image = Feng.Utils.Properties.Resources.ArrowDown;
            item.Text = "回字符表达式最左端字符的";
            AddItem(item);
            item = new ToolBarItemView(this);
            item.Image = Feng.Utils.Properties.Resources.ArrowUp;
            item.Text = "过期，请重新绑定！";
            AddItem(item);
            item = new ToolBarItemView(this);
            item.Image = Feng.Utils.Properties.Resources.EditButton_Drop;
            item.Text = "有库位的库存价值量";
            AddItem(item);
            item = new ToolBarItemView(this);
            item.Image = Feng.Utils.Properties.Resources.Copy;
            item.Text = "制品库存价值量";
            AddItem(item);
            item = new ToolBarItemView(this);
            item.Image = Feng.Utils.Properties.Resources.EditButton_More;
            item.Text = "（1701 -1709  9个库位";
            AddItem(item);

        }

    }

    public class ToolBarViewBrower : ToolBarView
    {
        public ToolBarViewBrower() 
        {

        }
        private ToolBarItemView _CloseItem = null;
        public ToolBarItemView CloseItem
        {
            get
            {
                if (_CloseItem != null)
                    return _CloseItem;
                _CloseItem = new ToolBarItemView(this);
                _CloseItem.Image = Feng.Utils.Properties.Resources.bule_del_2;
                _CloseItem.Text = "关闭";
                _CloseItem.Width = 20;
                _CloseItem.ShowCaption = false;
                _CloseItem.ShowSplitLine = false;
                return _CloseItem;
            }
        }

        private ToolBarItemView _MaxItem = null;
        public ToolBarItemView MaxItem
        {
            get
            {
                if (_MaxItem != null)
                    return _MaxItem;
                _MaxItem = new ToolBarItemView(this);
                _MaxItem.Image = Feng.Utils.Properties.Resources.bule_max;
                _MaxItem.Text = "最大化";
                _MaxItem.Width = 20;
                _MaxItem.ShowCaption = false;
                _MaxItem.ShowSplitLine = false;
                return _MaxItem;
            }
        }

        private ToolBarItemView _MinItem = null;
        public ToolBarItemView MinItem
        {
            get
            {
                if (_MinItem != null)
                    return _MinItem;
                _MinItem = new ToolBarItemView(this);
                _MinItem.Image = Feng.Utils.Properties.Resources.bule_min;
                _MinItem.Text = "最小化";
                _MinItem.Width = 20;
                _MinItem.ShowCaption = false;
                _MinItem.ShowSplitLine = false;
                return _MinItem;
            }
        }

        private ToolBarItemView _NewItem = null;
        public ToolBarItemView NewItem
        {
            get
            {
                if (_NewItem != null)
                    return _NewItem;
                _NewItem = new ToolBarItemView(this);
                _NewItem.Image = Feng.Utils.Properties.Resources.bule_add;
                _NewItem.Text = "添加";
                _NewItem.Width = 20;
                _NewItem.ShowCaption = false;
                _NewItem.ShowSplitLine = false;
                return _NewItem;
            }
        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            base.OnDraw(this, g);
            CloseItem.OnDraw(this, g);
            //MinItem.OnDraw(g);
            //MaxItem.OnDraw(g);
            NewItem.OnDraw(this, g);
            return false;
        }

        public override void RefreshItemLocation()
        {
            int width = this.Width - 200;
            int count = this.Items.Count + 1;
            if (count > 0)
            {
                ItemWidth = width / count;
            }
            else
            {
                ItemWidth = 72;
            }
            int left = 0;
            foreach (ToolBarItemView item in this.Items)
            {
                item.Left = left;
                item.Width = ItemWidth;
                left = left + ItemWidth;
            }
            NewItem.Left = left;
            NewItem.Width = ItemWidth;

            MinItem.Left = this.Width - 90;
            MaxItem.Left = this.Width - 60;
            CloseItem.Left = this.Width - 30;
             
        }
    }
 
}
