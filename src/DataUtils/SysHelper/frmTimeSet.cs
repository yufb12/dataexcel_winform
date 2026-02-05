using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;


namespace Feng.Utils
{
    public class frmTimeSet : Form
    {

        public frmTimeSet()
        {
            InitializeComponent();
        }

        public ComboBox txtUnit;
        public ComboBox txtTimes;
        private Label label1;
        private Button button1; 

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtUnit = new System.Windows.Forms.ComboBox();
            this.txtTimes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok(&S)";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtUnit
            // 
            this.txtUnit.FormattingEnabled = true;
            this.txtUnit.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "60"});
            this.txtUnit.Location = new System.Drawing.Point(120, 34);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(63, 23);
            this.txtUnit.TabIndex = 2;
            this.txtUnit.Text = "30";
            // 
            // txtTimes
            // 
            this.txtTimes.FormattingEnabled = true;
            this.txtTimes.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "20",
            "30",
            "60"});
            this.txtTimes.Location = new System.Drawing.Point(59, 34);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.Size = new System.Drawing.Size(55, 23);
            this.txtTimes.TabIndex = 2;
            this.txtTimes.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "秒";
            // 
            // frmTimeSet
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(317, 115);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTimes);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(335, 162);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(335, 162);
            this.Name = "frmTimeSet";
            this.ShowIcon = false;
            this.Text = "设置时间";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
         
        private void radText_CheckedChanged(object sender, EventArgs e)
        {
 
        }

        private void radText_Click(object sender, EventArgs e)
        {
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
     
     
}
