using System;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class ContinueDialog : Form
    {
        public ContinueDialog()
        {
            InitializeComponent();
        }
        public DialogResult ShowQuestion(string message)
        {
            this.txtMessage.Text = message;
            if (this.chkContinue.Checked)
            {
                return Result;
            }
            return this.ShowDialog(); 
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            try
            {
            }
            catch (Exception)
            {
            }

        }
        private DialogResult Result =  DialogResult.None;
        private void btnOk_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
