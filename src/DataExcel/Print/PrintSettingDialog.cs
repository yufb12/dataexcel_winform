using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Feng.Excel.Print
{
    public partial class PrintSettingDialog : System.Windows.Forms.Form
    {
        public PrintSettingDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public List<string> GetPrinter()
        {
            List<string> list = new List<string>();
            foreach (string ps in PrinterSettings.InstalledPrinters)
            {
                list.Add(ps);
                 
            }
            return list;
        }


        private void PrintDialog_Load(object sender, EventArgs e)
        {

            try
            {

                this.txtPaperKind.Items.AddRange(Enum.GetNames(typeof(PaperKind)));
                this.txtPrinter.Items.AddRange(GetPrinter().ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        
        }

        private void txtPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrinterSettings printset = new PrinterSettings();
            printset.PrinterName = this.txtPrinter.Text;
            this.txtPaperKind.Items.Clear();
            int c=printset .PaperSizes .Count;
            for (int i=0;i <c ;i ++)
            {
                this.txtPaperKind.Items.Add(printset.PaperSizes[i].PaperName);
            }
        }
    }
}
