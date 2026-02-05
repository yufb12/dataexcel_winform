using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;

namespace Feng.Forms.Controls
{
    [ToolboxItem(false)]
    public class NumTextBox : TextBox
    {
        public NumTextBox()
        {
            InitializeComponent();
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal? Value
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Text))
                {
                    decimal d = decimal.Zero;
                    if (decimal.TryParse(this.Text, out d))
                    {
                        if (this.DecimalPlaces > -1)
                        {
                            d = decimal.Round(d, this.DecimalPlaces, MidpointRounding.ToEven);
                        }
                        return d;
                    }
                }
                return null;
            }
        }
        [DefaultValue(0)]
        [Browsable(false)]
        public decimal Value1
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Text))
                {
                    decimal d = decimal.Zero;
                    if (decimal.TryParse(this.Text, out d))
                    {
                        if (this.DecimalPlaces > -1)
                        {
                            d = decimal.Round(d, this.DecimalPlaces, MidpointRounding.ToEven);
                        }
                        return d;
                    }
                }
                return decimal.Zero;
            }
            set
            {
                decimal d = value;
                if (this.DecimalPlaces > -1)
                {
                    d = decimal.Round(value, this.DecimalPlaces, MidpointRounding.ToEven);
                }
                this.Text = d.ToString();
            }
        }

        private ErrorProvider errorProvider;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new char PasswordChar
        {
            get
            {
                return ' ';
            }
        }
        private int _DecimalPlaces = -1;
        [DefaultValue(-1)]
        public virtual int DecimalPlaces
        {
            get
            {
                return _DecimalPlaces;
            }
            set
            {
                _DecimalPlaces = value;
            }
        }

        private System.ComponentModel.IContainer components;
        private decimal _maxvalue = decimal.MaxValue;
        public const decimal dMaxValue = decimal.MaxValue;
        public const decimal dMinValue = decimal.MinValue;

        public virtual decimal MaxValue
        {
            get
            {
                return _maxvalue;
            }
            set
            {
                this._maxvalue = value;
            }
        }
        private decimal _minvalue = decimal.MinValue;

        public virtual decimal MinValue
        {
            get
            {
                return _minvalue;
            }
            set
            {
                this._minvalue = value;
            }
        }
        private bool _showerroricon = false;
        [DefaultValue(false)]
        public virtual bool ShowErrorIcon
        {
            get
            {
                return _showerroricon;
            }
            set
            {
                _showerroricon = value;
            }
        }

        private bool lcktextchanged = false;
        protected override void OnTextChanged(EventArgs e)
        {
            try
            {
                if (lcktextchanged)
                {
                    return;
                }
                lcktextchanged = true;
                if (this.Value != null)
                {
                    if (DecimalPlaces > -1)
                    {
                        string text = this.Text;
                        int index = text.IndexOf(".");
                        if (index > 0)
                        {
                            int len = text.Length - index;
                            if (len > DecimalPlaces)
                            {
                                string str = text.Substring(0, index + DecimalPlaces + 1);
                                this.Text = str;
                                this.Select(str.Length, 0);
                            }
                        }
                    }
                }
            }
            finally
            {
                lcktextchanged = false;
            }
            this.errorProvider.Clear();
            base.OnTextChanged(e);
        }
        private bool _OnErrorChangedToSapce = true;
        [DefaultValue(true)]
        public virtual bool OnErrorChangedToSapce
        {
            get
            {
                return _OnErrorChangedToSapce;
            }
            set
            {
                _OnErrorChangedToSapce = true;
            }
        }
        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                string text = this.Text;
                if (text.StartsWith("=") && text.Length > 1)
                { 
                    return;
                }
                decimal d = decimal.Zero;
                if (!decimal.TryParse(this.Text, out d))
                {
                    if (OnErrorChangedToSapce)
                    {
                        try
                        {
                            lcktextchanged = true;
                            this.Text = string.Empty;
                        }
                        finally
                        {
                            lcktextchanged = false;
                        }
               
                    }
                    else
                    {
                        e.Cancel = true;
                        if (this.ShowErrorIcon)
                        {
                            errorProvider.SetError(this, "不是数字");
                        }
                    }
                }
                else
                {
                    if (d > MaxValue)
                    {
                        if (OnErrorChangedToSapce)
                        {
                            lcktextchanged = true;
                            this.Text = MaxValue.ToString();
                        }
                        else
                        {
                            e.Cancel = true;
                            if (this.ShowErrorIcon)
                            {
                                errorProvider.SetError(this, "大能大于最大值");
                            }
                        }
                    }
                    if (d < MinValue)
                    {
                        if (OnErrorChangedToSapce)
                        {
                            lcktextchanged = true;
                            this.Text = MinValue.ToString();
                        }
                        else
                        {
                            e.Cancel = true;
                            if (this.ShowErrorIcon)
                            {
                                errorProvider.SetError(this, "大能小于最小值");
                            }
                        }

                    }
                }
            }
            base.OnValidating(e);
        }
        public void Init()
        {
            this.SelectAll();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }


        [Browsable(false)]
        private int _addressid = 0;
        public virtual int AddressID
        {
            get
            {
                return _addressid;
            }
            set
            {
                _addressid = value;
            }
        }
    }

}
