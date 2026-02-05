using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class WaitingTimeDialog : Form
    {
        public WaitingTimeDialog()
        {
            InitializeComponent();
        }
        private int WaitingSecond = 3000;
        DialogResult DefaultResult = DialogResult.Cancel;

        public static DialogResult ShowInputTextDialog(string title, string text)
        {
            return ShowInputTextDialog(title, text, 3, DialogResult.Cancel);

        }

        public static DialogResult ShowInputTextDialog(string title, string text, int second, DialogResult result)
        {
            using (WaitingTimeDialog dlg = new WaitingTimeDialog())
            {
                dlg.Text = title;
                dlg.label1.Text = text;
                dlg.StartPosition = FormStartPosition.CenterScreen;
                dlg.timer1.Enabled = true;
                dlg.progressBar1.Maximum = second * 1000;
                dlg.WaitingSecond = second * 1000;
                dlg.DefaultResult = result;
                dlg.progressBar1.Value = 0;
                return dlg.ShowDialog();             
            }
        }
 
        private void WaitingTimeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
        }

        private void WaitingTimeDialog_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = progressBar1.Value + 100;
                decimal v = (WaitingSecond - progressBar1.Value);
                label2.Text = string.Format("将在{0}秒后关闭", (v/1000).ToString("0.0"));
                if (v < 100)
                {
                    timer1.Enabled = false;
                    this.DialogResult = DefaultResult;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
