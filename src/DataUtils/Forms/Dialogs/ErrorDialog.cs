using System;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class ErrorDialog : Form
    {
        public ErrorDialog()
        {
            InitializeComponent();
        }
        public string StackTrace { get; set; }

        public static void ShowError(Exception ex)
        {
#if DEBUG
            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "错误提示", MessageBoxButtons.OK);
            return;
#else 
            MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK);
            return;
#endif
            using (ErrorDialog dlg = new ErrorDialog())
            {
                dlg.TopMost = true;
                dlg.StartPosition = FormStartPosition.CenterScreen;
                dlg.txtMessage.Text = ex.Message;
                dlg.StackTrace = ex.StackTrace;
                dlg.ShowDialog();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            try
            {
                Feng.Forms.ClipboardHelper.SetText(this.txtMessage.Text + "\r\n" + this.StackTrace);
            }
            catch (Exception)
            {
            }

        }
    }
}
