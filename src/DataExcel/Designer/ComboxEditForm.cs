using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Designer
{
    public class ComboxEditForm : System.Windows.Forms.Form
    {
        private Label label1;
        private TextBox textBox1;
        private Button btnOk;
        private Button btnCancel; 
        public ComboxEditForm()
        {
            InitializeComponent();
        }

        public void SetText(List<string> text)
        {
            if (text == null)
                return;
            for (int i = 0; i < text.Count; i++)
            {
                this.textBox1.Text = this.textBox1.Text + text[i] + "\r\n";
            }
        }

        public List<string> GetText()
        {
            List<string> list = new List<string>();
            list.AddRange(this.textBox1.Lines);
 
            return list;
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入字符串(每行一个)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 37);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(428, 136);
            this.textBox1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(9, 188);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(90, 188);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ComboxEditForm
            // 
            this.ClientSize = new System.Drawing.Size(439, 233);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(457, 280);
            this.MinimumSize = new System.Drawing.Size(457, 280);
            this.Name = "ComboxEditForm";
            this.ShowIcon = false;
            this.Text = "字符集合编辑器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }

}
