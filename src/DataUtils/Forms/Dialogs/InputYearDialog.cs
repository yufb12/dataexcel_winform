using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class InputYearDialog : Form
    {
        public InputYearDialog()
        {
            InitializeComponent();
            Init();
            this.txtyear.Text = DateTime.Now.Year.ToString(); 
        }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public void Init()
        {
            for (int i=-10;i<10;i ++)
            {
                this.txtyear.Items.Add(DateTime.Now.AddYears(i).Year);
            }
        }
    }
}
