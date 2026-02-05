using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Edits.EditForm
{
    public partial class CellImageShow : Form
    {
        public CellImageShow()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (this.pictureBox1.SizeMode == PictureBoxSizeMode.Zoom)
            {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }
    }
}
