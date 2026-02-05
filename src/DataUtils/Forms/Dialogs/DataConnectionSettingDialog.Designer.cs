namespace Feng.Forms.Dialogs
{
    partial class DataConnectionSettingDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.txt服务器地址 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt用户名 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt密码 = new System.Windows.Forms.TextBox();
            this.label数据库名称 = new System.Windows.Forms.Label();
            this.txt数据库名称 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(23, 20);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(77, 12);
            this.label.TabIndex = 0;
            this.label.Text = "服务器地址：";
            // 
            // txt服务器地址
            // 
            this.txt服务器地址.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt服务器地址.Location = new System.Drawing.Point(98, 17);
            this.txt服务器地址.Name = "txt服务器地址";
            this.txt服务器地址.Size = new System.Drawing.Size(466, 21);
            this.txt服务器地址.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(95, 128);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(192, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // txt用户名
            // 
            this.txt用户名.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt用户名.Location = new System.Drawing.Point(98, 44);
            this.txt用户名.Name = "txt用户名";
            this.txt用户名.Size = new System.Drawing.Size(466, 21);
            this.txt用户名.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密码：";
            // 
            // txt密码
            // 
            this.txt密码.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt密码.Location = new System.Drawing.Point(98, 71);
            this.txt密码.Name = "txt密码";
            this.txt密码.Size = new System.Drawing.Size(466, 21);
            this.txt密码.TabIndex = 1;
            // 
            // label数据库名称
            // 
            this.label数据库名称.AutoSize = true;
            this.label数据库名称.Location = new System.Drawing.Point(23, 101);
            this.label数据库名称.Name = "label数据库名称";
            this.label数据库名称.Size = new System.Drawing.Size(77, 12);
            this.label数据库名称.TabIndex = 0;
            this.label数据库名称.Text = "数据库名称：";
            // 
            // txt数据库名称
            // 
            this.txt数据库名称.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt数据库名称.Location = new System.Drawing.Point(98, 98);
            this.txt数据库名称.Name = "txt数据库名称";
            this.txt数据库名称.Size = new System.Drawing.Size(466, 21);
            this.txt数据库名称.TabIndex = 1;
            // 
            // DataConnectionSettingDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(576, 157);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txt数据库名称);
            this.Controls.Add(this.label数据库名称);
            this.Controls.Add(this.txt密码);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt用户名);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt服务器地址);
            this.Controls.Add(this.label);
            this.MinimumSize = new System.Drawing.Size(416, 150);
            this.Name = "DataConnectionSettingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器连接设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox txt服务器地址;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txt用户名;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt密码;
        public System.Windows.Forms.Label label数据库名称;
        public System.Windows.Forms.TextBox txt数据库名称;
    }
}