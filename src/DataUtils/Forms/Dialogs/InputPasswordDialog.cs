using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class InputPasswordDialog : Form
    {
        public InputPasswordDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(this.textBox2.Text))
                {
                    return;
                }
                if (this.textBox1.Text != this.textBox2.Text)
                {
                    MessageBox.Show("两次输入的密码不一样！");
                    return;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
    }
}
