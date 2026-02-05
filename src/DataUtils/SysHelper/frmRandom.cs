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
    public class frmRandom : Form
    {

        public frmRandom()
        {
            InitializeComponent();
        }

        public RadioButton radRandom;
        public RadioButton radTime;
        public RadioButton radCount;
        public RadioButton radText;
        private Button button1; 

        private void InitializeComponent()
        {
            this.radRandom = new System.Windows.Forms.RadioButton();
            this.radTime = new System.Windows.Forms.RadioButton();
            this.radCount = new System.Windows.Forms.RadioButton();
            this.radText = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radRandom
            // 
            this.radRandom.AutoSize = true;
            this.radRandom.Location = new System.Drawing.Point(50, 48);
            this.radRandom.Name = "radRandom";
            this.radRandom.Size = new System.Drawing.Size(73, 19);
            this.radRandom.TabIndex = 0;
            this.radRandom.TabStop = true;
            this.radRandom.Text = "随机数";
            this.radRandom.UseVisualStyleBackColor = true;
            this.radRandom.CheckedChanged += new System.EventHandler(this.radText_CheckedChanged);
            this.radRandom.Click += new System.EventHandler(this.radText_Click);
            // 
            // radTime
            // 
            this.radTime.AutoSize = true;
            this.radTime.Location = new System.Drawing.Point(50, 72);
            this.radTime.Name = "radTime";
            this.radTime.Size = new System.Drawing.Size(58, 19);
            this.radTime.TabIndex = 0;
            this.radTime.TabStop = true;
            this.radTime.Text = "时间";
            this.radTime.UseVisualStyleBackColor = true;
            this.radTime.CheckedChanged += new System.EventHandler(this.radText_CheckedChanged);
            this.radTime.Click += new System.EventHandler(this.radText_Click);
            // 
            // radCount
            // 
            this.radCount.AutoSize = true;
            this.radCount.Checked = true;
            this.radCount.Location = new System.Drawing.Point(50, 24);
            this.radCount.Name = "radCount";
            this.radCount.Size = new System.Drawing.Size(73, 19);
            this.radCount.TabIndex = 0;
            this.radCount.TabStop = true;
            this.radCount.Text = "累计数";
            this.radCount.UseVisualStyleBackColor = true;
            this.radCount.CheckedChanged += new System.EventHandler(this.radText_CheckedChanged);
            this.radCount.Click += new System.EventHandler(this.radText_Click);
            // 
            // radText
            // 
            this.radText.AutoSize = true;
            this.radText.Location = new System.Drawing.Point(50, 96);
            this.radText.Name = "radText";
            this.radText.Size = new System.Drawing.Size(88, 19);
            this.radText.TabIndex = 0;
            this.radText.TabStop = true;
            this.radText.Text = "随机文本";
            this.radText.UseVisualStyleBackColor = true;
            this.radText.CheckedChanged += new System.EventHandler(this.radText_CheckedChanged);
            this.radText.Click += new System.EventHandler(this.radText_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(203, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok&&Save";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmRandom
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(317, 115);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radText);
            this.Controls.Add(this.radTime);
            this.Controls.Add(this.radCount);
            this.Controls.Add(this.radRandom);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(335, 162);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(335, 162);
            this.Name = "frmRandom";
            this.ShowIcon = false;
            this.Text = "发送随机";
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
