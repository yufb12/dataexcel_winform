using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Feng.Excel.App
{
    public partial class Regedit : System.Windows.Forms.Form
    {
        public Regedit()
        {
            InitializeComponent();
            //this.Text = this.Text + String.Format("关于 {0}", Product.AssemblyTitle);
            this.labelProductName.Text = this.labelProductName.Text + ":" + Product.AssemblyProduct;
            this.labelVersion.Text = this.labelVersion.Text + ":" + Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
            this.labelCopyright.Text = this.labelCopyright.Text + ":" + Product.AssemblyCopyright;
            this.labelCompanyName.Text = this.labelCompanyName.Text + ":" + Product.AssemblyCompany;
            this.textBoxDescription.Text =this.textBoxDescription.Text+":"+ Product.AssemblyDescription;
            this.linkLabelHomePage.Text = this.linkLabelHomePage.Text + ":" + Product.AssemblyHomePage;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(Product.AssemblyHomePage))
                {
                    System.Diagnostics.Process.Start(Product.AssemblyHomePage);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }

    }
}
