using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Feng.Utils
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
         
        private void label3_Click(object sender, EventArgs e)
        {
 
        }
        private string Mac {
            get {
                string mac = string.Empty;
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    System.Net.NetworkInformation.NetworkInterface[] nis = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                    if (nis.Length > 0)
                    {
                        System.Net.NetworkInformation.NetworkInterface ni = nis[0];
                        mac = ni.Id;
                    }
                }

                return mac;
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        { 
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://yufb.taobao.com/");
        }
    }
}
