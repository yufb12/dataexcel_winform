using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;

namespace Feng.Forms.Skins
{
    public class SkinStyle
    {
        public static void InitControl(Control c)
        {
            c.Paint += c_Paint;
            c.MouseEnter += c_MouseEnter;
            c.MouseLeave += c_MouseLeave;
        }

        static void c_MouseLeave(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            if (ctl != null)
            {
                ctl.BackColor = Color.Transparent;
            }
        }

        static void c_MouseEnter(object sender, EventArgs e)
        {

            Control ctl = sender as Control;
            if (ctl != null)
            {
                ctl.BackColor = Color.Gray;
            }
        }

        static void c_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
