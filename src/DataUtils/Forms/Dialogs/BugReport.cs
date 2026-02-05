using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Feng.Net;

namespace Feng.Forms.Dialogs
{
    public partial class BugReport : Form
    {
        public BugReport()
        {
            InitializeComponent(); 
        }
        public void Init(Exception ex)
        {
            this.richTextBox1.AppendText("######软件发生异常#####");
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText(ex.ToString());
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText("Message_____________");
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText(ex.Message);
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText("Source______________");
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText(ex.Source);
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText("StackTrace______________");
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText(ex.StackTrace);
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText("TargetSite_______________");
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText(ex.TargetSite == null ? string.Empty : ex.TargetSite.ToString());
            this.richTextBox1.AppendText("\r\n");
            this.richTextBox1.AppendText("#######################");
            this.richTextBox1.AppendText("\r\n");
            if (ex.InnerException != null)
            {
                Init(ex.InnerException);
            }
        }
        public static void ShowBugReport(Exception ex)
        {
            BugReport frm = new BugReport();
            frm.Init(ex);
            frm.ShowDialog();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            try
            { 
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        
        }
    }
}
