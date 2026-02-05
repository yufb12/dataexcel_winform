using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Feng.Excel.App
{
    public partial class AboutBox : System.Windows.Forms.Form
    {
        public AboutBox()
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

        public new static void Show()
        {
            System.Threading.Thread th = new System.Threading.Thread(ShowAbout);
            th.IsBackground = true;
            th.Start();  
        }

        private static System.Threading.AutoResetEvent autoevent = new System.Threading.AutoResetEvent(false);
        //private static AboutBox aboutForm = new AboutBox();
        private static void ShowAbout()
        { 
            try
            {
                autoevent.Reset();
                Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
                autoevent.WaitOne(1000 * 60 * 60*7);
                if (!AppExit)
                {
                    //aboutForm.WindowState = FormWindowState.Normal;
                    //aboutForm.StartPosition = FormStartPosition.Manual;
                    //aboutForm.Top = Screen.PrimaryScreen.WorkingArea.Bottom - aboutForm.Height - 20;
                    //aboutForm.Left = Screen.PrimaryScreen.WorkingArea.Right - aboutForm.Width - 20;
                    //aboutForm.TopMost = true;
                    //aboutForm.ShowDialog();
                    //aboutForm.Dispose();
                } 
            }
            catch (Exception  )
            { 
            }
        

        }
        private static bool AppExit = false;
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {

                AppExit = true;
                autoevent.Set();
                //aboutForm.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
