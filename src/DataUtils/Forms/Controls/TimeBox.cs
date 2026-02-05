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
    public class TimeBox : System.Windows.Forms.UserControl    
    {
        public TimeBox()
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
 
        public DateTime Value
        {
            get
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month
                    , DateTime.Now.Day
                    , (int)this.txtHour.Value
                    , (int)this.txtMin.Value
                    , (int)this.txtsec.Value);
            }
            set
            {
                this.txtHour.Value = value.Hour;
                this.txtMin.Value = value.Minute;
                this.txtsec.Value = value.Second;
            }
        }
        private NumericUpDown txtHour;
        private Label labelmin;
        private NumericUpDown txtMin;
        private NumericUpDown txtsec;
        private Label labelHour;
        private void InitializeComponent()
        {
            this.txtHour = new System.Windows.Forms.NumericUpDown();
            this.labelmin = new System.Windows.Forms.Label();
            this.txtMin = new System.Windows.Forms.NumericUpDown();
            this.labelHour = new System.Windows.Forms.Label();
            this.txtsec = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsec)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHour
            // 
            this.txtHour.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHour.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtHour.Location = new System.Drawing.Point(0, 0);
            this.txtHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(31, 21);
            this.txtHour.TabIndex = 1;
            this.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHour.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.txtHour.ValueChanged += new System.EventHandler(this.txtsec_ValueChanged);
            // 
            // labelmin
            // 
            this.labelmin.AutoSize = true;
            this.labelmin.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelmin.Location = new System.Drawing.Point(74, 0);
            this.labelmin.Name = "labelmin";
            this.labelmin.Size = new System.Drawing.Size(15, 15);
            this.labelmin.TabIndex = 2;
            this.labelmin.Text = ":";
            // 
            // txtMin
            // 
            this.txtMin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMin.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtMin.Location = new System.Drawing.Point(46, 0);
            this.txtMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.txtMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(28, 21);
            this.txtMin.TabIndex = 1;
            this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMin.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtMin.ValueChanged += new System.EventHandler(this.txtsec_ValueChanged);
            // 
            // labelHour
            // 
            this.labelHour.AutoSize = true;
            this.labelHour.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelHour.Location = new System.Drawing.Point(31, 0);
            this.labelHour.Name = "labelHour";
            this.labelHour.Size = new System.Drawing.Size(15, 15);
            this.labelHour.TabIndex = 2;
            this.labelHour.Text = ":";
            // 
            // txtsec
            // 
            this.txtsec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsec.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtsec.Location = new System.Drawing.Point(89, 0);
            this.txtsec.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.txtsec.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtsec.Name = "txtsec";
            this.txtsec.Size = new System.Drawing.Size(28, 21);
            this.txtsec.TabIndex = 1;
            this.txtsec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtsec.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtsec.ValueChanged += new System.EventHandler(this.txtsec_ValueChanged);
            // 
            // TimeBox
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtsec);
            this.Controls.Add(this.labelmin);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.labelHour);
            this.Controls.Add(this.txtHour);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TimeBox";
            this.Size = new System.Drawing.Size(117, 16);
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public void Init()
        {
            this.txtHour.Value = DateTime.Now.Hour;
            this.txtMin.Value = DateTime.Now.Minute;
            this.txtsec.Value = DateTime.Now.Second;
        }
        public delegate void TimeChangedHandler(object sender, DateTime time);
        public event TimeChangedHandler TimeChanged;
        private void txtsec_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                if (TimeChanged != null)
                {
                    TimeChanged(this, Value);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        
        }
 
    }
 
}
