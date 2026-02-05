using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Feng.Net;

namespace Feng.Forms
{
    public partial class PrintSettingDialog : Form
    {
        public PrintSettingDialog()
        {
            InitializeComponent(); 
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
