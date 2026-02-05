using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class InputDialogSendkeys : System.Windows.Forms.Form
    {
        public InputDialogSendkeys()
        { 
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                string url = @"https://baike.baidu.com/item/sendkeys/7257234";
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
  
        }
        
    }
}
