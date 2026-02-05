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
    public class BeginAndEndDateTimeBox : System.Windows.Forms.UserControl    
    {
        public DateTimePicker txtEndTime;
        private Label label1;
        private Label label2;
        public DateTimePicker txtBeiginTime;
    
        public BeginAndEndDateTimeBox()
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
            Init();
        }
 
        private void InitializeComponent()
        {
            this.txtBeiginTime = new System.Windows.Forms.DateTimePicker();
            this.txtEndTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBeiginTime
            // 
            this.txtBeiginTime.Location = new System.Drawing.Point(57, 3);
            this.txtBeiginTime.Name = "txtBeiginTime";
            this.txtBeiginTime.Size = new System.Drawing.Size(110, 21);
            this.txtBeiginTime.TabIndex = 0;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Location = new System.Drawing.Point(220, 3);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(107, 21);
            this.txtEndTime.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "起始日期:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "结束日期:";
            // 
            // BeginAndEndDateTimeBox
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtBeiginTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BeginAndEndDateTimeBox";
            this.Size = new System.Drawing.Size(332, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public void Init()
        {
            this.txtBeiginTime.Value = DateTime.Now.AddMonths(-1);
            this.txtEndTime.Value = DateTime.Now;
        }
 
    }
 
}
