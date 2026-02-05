using Feng.Excel.Interfaces;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Forms
{
    public partial class frmColumnWidth : System.Windows.Forms.Form
    {
        public frmColumnWidth()
        {
            InitializeComponent();
        }
 
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Forms", "FrmFind", "btnNext_Click", true, ex);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
 
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Forms", "FrmFind", "btnPrev_Click", true, ex);
            }
        }


    }
}
 