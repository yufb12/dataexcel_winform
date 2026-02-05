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
    public class MonthBox : System.Windows.Forms.UserControl 
    {
        public MonthBox()
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
        public DateTime Month
        {
            get
            {
                return new DateTime((int)this.txtYear.Value, (int)this.txtMonth.Value, 1);
            }
            set
            {
                this.txtYear.Value = value.Year;
                this.txtMonth.Value = value.Month;
            }
        }
        public DateTime DateTime
        {
            get
            {
                return new DateTime((int)this.txtYear.Value, (int)this.txtMonth.Value, 1);
            }
            set
            {
                this.txtYear.Value = value.Year;
                this.txtMonth.Value = value.Month;
            }
        }
        private NumericUpDown txtYear;
        private Label label1;
        private NumericUpDown txtMonth;
        private Label label2;
        private void InitializeComponent()
        {
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).BeginInit();
            this.SuspendLayout();
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(1, 1);
            this.txtYear.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(49, 21);
            this.txtYear.TabIndex = 1;
            this.txtYear.Value = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "年";
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(67, 1);
            this.txtMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(35, 21);
            this.txtMonth.TabIndex = 1;
            this.txtMonth.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "月";
            // 
            // MonthBox
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.Name = "MonthBox";
            this.Size = new System.Drawing.Size(122, 25);
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public void Init()
        {
            this.txtYear.Value = DateTime.Now.Year;
            this.txtMonth.Value = DateTime.Now.Month;
        }
 
    }
 
}
