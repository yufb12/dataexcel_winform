using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Popup;
using Feng.Utils;
using System.Data;
using Feng.Data;
using Feng.Forms.Events;
using Feng.Forms.EventHandlers;
using Feng.Forms.Interface;

namespace Feng.Forms.Controls
{

    [ToolboxItem(true)]
    public class DropDownBox : System.Windows.Forms.UserControl, IPopupEdit
    {
        private TextBox txtKey;
        public DropDownBox()
        {
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKey.Location = new System.Drawing.Point(2, 2);
            this.txtKey.Margin = new System.Windows.Forms.Padding(1, 2, 0, 0);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(135, 14);
            this.txtKey.TabIndex = 1;
            this.txtKey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtKey_MouseClick);
            this.txtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyDown);
            this.txtKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyUp);
            this.txtKey.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtKey_MouseDoubleClick);
            this.txtKey.TextChanged += txtKey_TextChanged;
            // 
            // DropDownBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtKey);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(0, 21);
            this.Name = "DropDownBox";
            this.Size = new System.Drawing.Size(175, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public virtual void OnKeyTextChanged(string text)
        {

        }
        void txtKey_TextChanged(object sender, EventArgs e)
        {
            OnKeyTextChanged(txtKey.Text);
        }

        public event Feng.Forms.Events.EditEvent.DropDownButtonClickHandler DropDownButtonClick;
        public event Feng.Forms.Events.EditEvent.MoreButtonClickHandler MoreButtonClick;
        public event Feng.Forms.Events.EditEvent.DropDownBoxTextChangedHandler DropDownBoxTextChanged;
        public virtual void OnDropDownBoxTextChanged(DropDownBoxTextChangedEventArgs key)
        {
            if (DropDownBoxTextChanged != null)
            {
                DropDownBoxTextChanged(this, key);
            }
        }
        public virtual void OnDropDownClick(DropDownButtonClickEventArgs key)
        {
            if (DropDownButtonClick != null)
            {
                DropDownButtonClick(this, key);
            }
        }
        public virtual void OnMoreClick(MoreButtonClickEventArgs key)
        {
            if (MoreButtonClick != null)
            {
                MoreButtonClick(this, key);
            }
        }
        public virtual void ShowDropDownPopup()
        {
            if (PopupForm != null)
            {
                DropDownButtonClickEventArgs key = new DropDownButtonClickEventArgs();
                PopupForm.DropDownButtonClick(this, key);
                if (!key.Cancel)
                {
                    if (!this.PopupForm.Visible)
                    {
                        CancelEventArgs e = new CancelEventArgs();
                        OnBeforeShowPopup(e);
                        if (e.Cancel)
                            return;
                        this.ShowPopup();
                        this.PopupForm.Focus();
                    }
                    else
                    {
                        this.PopupForm.Cancel();
                    }
                }
            }
        }

        public virtual void ShowTextChangedPopup()
        {
            if (PopupForm != null)
            {
                DropDownBoxTextChangedEventArgs key = new DropDownBoxTextChangedEventArgs();
                key.Text = this.txtKey.Text;
                PopupForm.KeyChanged(this, key);
                if (!key.Cancel)
                {
                    CancelEventArgs e = new CancelEventArgs();
                    OnBeforeShowPopup(e);
                    if (e.Cancel)
                        return;
                    this.ShowPopup();
                }
            }
        }

        public delegate void BeforeShowPopupHandler(object sender, CancelEventArgs e);
        public event BeforeShowPopupHandler BeforeShowPopup;
        public void OnBeforeShowPopup(CancelEventArgs e)
        {
            if (BeforeShowPopup != null)
            {
                BeforeShowPopup(this, e);
            }
        }
        void txtKey_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                if (!AllowTextEdit)
                {
                    DropDownButtonClickEventArgs key = new DropDownButtonClickEventArgs();
                    OnDropDownClick(key);
                    if (key.Cancel)
                    {
                        return;
                    }
                    ShowDropDownPopup();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        void txtKey_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private object _editvalue = null;
        public virtual object EditValue
        {
            get
            {
                return this._editvalue;
            }
            set
            {
                this._editvalue = value;
            }
        }
        public override string Text
        {
            get
            {
                return this.txtKey.Text;
            }
            set
            {
                this.txtKey.Text = value;
            }
        }
        public int SelectionStart
        {
            get
            {
                return this.txtKey.SelectionStart;
            }
            set
            {
                this.txtKey.SelectionStart = value;
            }
        }
        private bool _initing = false;
        public virtual bool Initing
        {
            get
            {
                return _initing;
            }

        }

        private bool _AllowTextEdit = false;
        public bool AllowTextEdit
        {
            get
            {
                return _AllowTextEdit;
            }
            set
            {
                this.txtKey.ReadOnly = !value;
                _AllowTextEdit = value;
            }
        }

        bool PopupForm_BeforeMouseClickHide(object sender, MouseEventArgs e)
        {

            try
            {
                Point pt = e.Location;
                Point ptkey = this.txtKey.PointToScreen(this.txtKey.Location);
                Rectangle rect = new Rectangle(ptkey, this.Size);
                if (rect.Contains(pt))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return false;
        }
        private bool lockTextChanged = false;
        public virtual bool LockTextChanged
        {
            get
            {
                return lockTextChanged;
            }
            set
            {
                lockTextChanged = value;
            }
        }
        private bool _showdropdownbutton = true;
        public bool ShowDropDownButton
        {
            get
            {
                return _showdropdownbutton;
            }
            set
            {
                _showdropdownbutton = value;
                ReSetTextEditSize();
            }
        }

        private bool _showmorebutton = true;
        public bool ShowMoreButton
        {
            get
            {
                return _showmorebutton;
            }
            set
            {
                _showmorebutton = value;
                ReSetTextEditSize();
            }
        }
        public Rectangle GetDropDownRect
        {
            get
            {
                if (!this.ShowDropDownButton)
                {
                    return Rectangle.Empty;
                }

                Rectangle rect = new Rectangle();
                rect.Width = 16;
                rect.Height = this.Height;
                if (this.ShowMoreButton)
                {
                    rect.X = this.Width - rect.Width - 1 - 16;
                }
                else
                {
                    rect.X = this.Width - rect.Width - 1;
                }
                rect.Y = 1;
                return rect;
            }
        }
        public Rectangle GetMoreRect
        {
            get
            {
                if (this.ShowMoreButton)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = 16;
                    rect.Height = this.Height;
                    rect.X = this.Width - rect.Width - 1;
                    rect.Y = 1;
                    return rect;
                }
                return Rectangle.Empty;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, this.Width - 1, this.Height - 1);
            if (this.ShowMoreButton)
            {
                Rectangle rect = this.GetMoreRect;
                Feng.Drawing.GraphicsHelper.DrawImage(e.Graphics, Feng.Utils.Properties.Resources.EditButton_More, rect, ImageLayout.Center);
            }
            if (this.ShowDropDownButton)
            {
                Rectangle rect = this.GetDropDownRect;
                Feng.Drawing.GraphicsHelper.DrawImage(e.Graphics, Feng.Utils.Properties.Resources.EditButton_Drop, rect, ImageLayout.Center);
            }
            base.OnPaint(e);
        }

        public void ReSetTextEditSize()
        {
            this.txtKey.Location = new Point(2, 2);
            int width = this.Width - 2;
            if (this.ShowMoreButton)
            {
                width = width - 16 - 1;
            }
            if (this.ShowDropDownButton)
            {
                width = width - 16 - 1;
            }
            this.txtKey.Width = width;
            this.Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            //this.txtKey.Location = new Point(2, 2);
            //this.buttonDropDown.Size = new Size(this.Height - 3, this.Height - 4);
            //this.buttonDropDown.Location = new Point(this.Width - this.buttonDropDown.Width - 2, 2);
            //this.txtKey.Width = this.buttonDropDown.Location.X - this.txtKey.Location.X;
            //this.txtKey.Width = this.Width - 16 - 16 - 2;
            ReSetTextEditSize();
            base.OnSizeChanged(e);
        }
        private DropDownListForm popupfrm = null;
        [Browsable(false)]
        public virtual PopupForm PopupForm
        {
            get
            {
                return popupfrm;
            }
            set
            {
            }
        }
        public virtual void InitForm(PopupForm popupform)
        {
            if (popupform != null)
            {
                popupform.ParentEditForm = this.FindForm(); 
                popupform.BeforeMouseClickHide += new PopupForm.BeforeMouseClickHideEventHandler(PopupForm_BeforeMouseClickHide);
            }
        }
        void PopupForm_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                this.txtKey.Focus();
                OnCancel();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        public virtual void OnCancel()
        {

        }

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
        private string _displayMember = string.Empty;
        public virtual string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; }
        }

        private string _valueMember = string.Empty;
        public virtual string ValueMember
        {
            get { return _valueMember; }
            set { _valueMember = value; }
        }

        private object _datasource = null;
        public virtual object DataSource
        {
            get
            {
                return _datasource;
            }
            set
            {
                OnDataSourceChanged(value);
            }
        }
        public event Feng.Forms.Events.EditEvent.DataSourceChangedHandler DataSourceChanged;
        public virtual void OnDataSourceChanged(object datasource)
        {
            _datasource = datasource;
            if (DataSourceChanged != null)
            {
                DataSourceChanged(this, EventArgs.Empty);
            }
        }
        private object databounditem = null;
        public virtual object DataBoundItem
        {
            get
            {
                return databounditem;
            }
        }
        internal void InitDataBoundItem(object item)
        {
            databounditem = item;
        }

        private bool _ReadLonly = false;
        public virtual bool ReadOnly
        {
            get
            {
                return this._ReadLonly;
            }
            set
            {
                this.txtKey.ReadOnly = value;
                this._ReadLonly = value;
            }
        }

 
        public event Feng.Forms.Events.EditEvent.SelectValueChangedHandler SelectedChanged;
        public virtual void OnSelectedChanged(object value, object model)
        {
            if (SelectedChanged != null)
            {
                SelectedChanged(this, value, model);
            }
        }
        public virtual void OnOK(object value, object model)
        {
            BeforeValueChangedArgs e = new BeforeValueChangedArgs();
            e.Value = value;
            if (BeforeValueChanged != null)
            {
                BeforeValueChanged(this, e);
            }
            if (e.Cancel)
            {
                return;
            }
            _editvalue = value;
            //if (!string.IsNullOrWhiteSpace(this.ValueMember))
            //{ 
            //    object objvalue = ReflectionHelper.GetValue(value, this.ValueMember);
            //    this.txtKey.Text = ConvertHelper.GetFormatString(objvalue, this.FormatType, this.FormatString);
            //    this.txtKey.SelectionStart = this.txtKey.Text.Length; 
            //}
            //else
            //{
            this.txtKey.Text = ConvertHelper.GetFormatString(value, this.FormatType, this.FormatString);
            this.txtKey.SelectionStart = this.txtKey.Text.Length;
            //}
            OnSelectedChanged(value, model);
            OnValueChanged(value);
        }
        public event Feng.Forms.Events.EditEvent.BeforeValueChangedHandler BeforeValueChanged;
        protected override void OnMouseUp(MouseEventArgs e)
        {

            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Rectangle rect = this.GetDropDownRect;
                    if (rect.Contains(e.Location))
                    {
                        buttonDropDown_Click();
                    }
                    rect = this.GetMoreRect;
                    if (rect.Contains(e.Location))
                    {
                        buttonMore_Click();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnMouseUp(e);
        }
        private void buttonDropDown_Click()
        {
            if (this.ReadOnly)
            {
                return;
            }
            DropDownButtonClickEventArgs key = new DropDownButtonClickEventArgs();
            OnDropDownClick(key);
            if (key.Cancel)
            {
                return;
            }
            ShowDropDownPopup();
        }

        private void buttonMore_Click()
        {
            if (this.ReadOnly)
            {
                return;
            }
            MoreButtonClickEventArgs key = new MoreButtonClickEventArgs();
            OnMoreClick(key);
            if (key.Cancel)
            {
                return;
            }
        }
        public event ValueChangedEventHandler ValueChanged;
        public virtual void OnValueChanged(object value)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, value);
            }
        }
        public virtual void HidePopup()
        {
            if (PopupForm != null)
            {
                PopupForm.Hide();
            }
        }

        private bool firstshow = false;
        public virtual void FirstShow(DropDownFormFirstShowEventArgs e)
        {

        }
        public virtual void ShowPopup()
        {
            if (this.ReadOnly)
                return;
            Point pt = this.PointToScreen(new Point(0, this.Height));
            if (PopupForm != null)
            {
                if (!firstshow)
                {
                    firstshow = true;
                    DropDownFormFirstShowEventArgs e = new DropDownFormFirstShowEventArgs();
                    FirstShow(e);
                    if (e.Handler)
                    {
                        return;
                    }
                }

                PopupForm.ParentEditForm = this.FindForm();
                PopupForm.Popup(pt);
                popupfrm.InitPopup(this);
            }
        }

        private void txtKey_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.ReadOnly)
                {
                    return;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.PopupForm != null)
                    {
                        if (this.PopupForm.Visible)
                        {
                            this.PopupForm.SelectFirst();
                        }
                        else
                        {
                            DropDownButtonClickEventArgs key = new DropDownButtonClickEventArgs();
                            OnDropDownClick(key);
                            if (key.Cancel)
                            {
                                return;
                            }
                            ShowDropDownPopup();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (this.PopupForm != null)
                    {
                        this.PopupForm.Visible = false;
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (this.PopupForm != null)
                    {
                        if (this.PopupForm.Visible)
                        {
                            this.PopupForm.MoveToFirst();
                        }
                        else
                        {
                            DropDownButtonClickEventArgs key = new DropDownButtonClickEventArgs();
                            OnDropDownClick(key);
                            if (key.Cancel)
                            {
                                return;
                            }
                            ShowDropDownPopup();
                        }
                    }
                }
                else
                {
                    char c = (char)e.KeyValue;
                    if (!char.IsControl(c))
                    {
                        DropDownBoxTextChangedEventArgs key = new DropDownBoxTextChangedEventArgs();
                        OnDropDownBoxTextChanged(key);
                        if (key.Cancel)
                        {
                            return;
                        }
                        ShowTextChangedPopup();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void txtKey_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.ReadOnly)
                {
                    return;
                }
                if (PopupForm != null)
                {
                    if (AllowTextEdit)
                    {
                        DropDownButtonClickEventArgs key = new DropDownButtonClickEventArgs();
                        OnDropDownClick(key);
                        if (key.Cancel)
                        {
                            return;
                        }
                        ShowDropDownPopup();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        public virtual void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader bw = new IO.BufferReader(data.Data))
            {
                this._AllowTextEdit = bw.ReadIndex(1, this._AllowTextEdit);
                this._displayMember = bw.ReadIndex(2, this._displayMember);
                this._FormatString = bw.ReadIndex(3, this._FormatString);
                this._FormatType = (FormatType)bw.ReadIndex(4, (int)FormatType.Null);
                this._ReadLonly = bw.ReadIndex(5, this._ReadLonly);
                this._showdropdownbutton = bw.ReadIndex(6, this._showdropdownbutton);
                this._showmorebutton = bw.ReadIndex(7, this._showmorebutton);
                this._valueMember = bw.ReadIndex(8, this._valueMember);
            }
        }
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
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, this._AllowTextEdit);
                    bw.Write(2, this._displayMember);
                    bw.Write(3, this._FormatString);
                    bw.Write(4, (int)this._FormatType);
                    bw.Write(5, this._ReadLonly);
                    bw.Write(6, this._showdropdownbutton);
                    bw.Write(7, this._showmorebutton);
                    bw.Write(8, this._valueMember);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }
    }



}
