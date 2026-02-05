using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class frmSaveSusess : Form
    {
        public frmSaveSusess()
        {
            InitializeComponent();
        }
        int count = 3;
        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                if (count <= 1)
                {
                    this.timer1.Tick -= timer1_Tick;
                    this.Close();
                }
                count = count - 1;
                string str = string.Format("[{0}]秒后自动关闭", count);
                txtCount.Text = str;

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        
        }

        public static void ShowSaveSusess()
        {
            frmSaveSusess frm = new frmSaveSusess();
            frm.StartPosition = FormStartPosition.CenterScreen; 
            frm.ShowDialog();
        }

        public static void ShowSaveSusess(string title,string desc)
        {
            frmSaveSusess frm = new frmSaveSusess();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.txttitle.Text = title;
            frm.txtdesc.Text = desc;
            frm.ShowDialog();
        }

        private void frmSaveSusess_KeyDown(object sender, KeyEventArgs e)
        {
            this.timer1.Tick -= timer1_Tick;
            this.Close();
        }

        private void frmSaveSusess_MouseDown(object sender, MouseEventArgs e)
        {
            this.timer1.Tick -= timer1_Tick;
            this.Close();
        }

        private void txtdesc_MouseDown(object sender, MouseEventArgs e)
        {
            this.timer1.Tick -= timer1_Tick;
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.timer1.Tick -= timer1_Tick;
            this.Close();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.pictureBox1.Image = global::Feng.Utils.Properties.Resources.ToolBarClose2;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Feng.Utils.Properties.Resources.ToolBarClose;
        }
    }


}
