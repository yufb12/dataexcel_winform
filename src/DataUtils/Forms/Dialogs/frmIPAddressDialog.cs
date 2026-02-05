using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class IPAddressDialog : Form
    {
        public IPAddressDialog()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }
    }
}
