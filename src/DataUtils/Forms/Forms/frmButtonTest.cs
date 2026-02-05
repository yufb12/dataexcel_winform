using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class frmButtonTest : Form
    {

        public frmButtonTest()
        {
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.button1.Invalidate();
        }
    }
}
