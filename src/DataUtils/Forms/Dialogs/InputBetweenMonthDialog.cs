using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class InputBetweenMonthDialog : Form
    {
        public InputBetweenMonthDialog()
        {
            InitializeComponent();
            Init();
            this.txtBeginyear.Text = DateTime.Now.Year.ToString();
            this.txtBeginMonth.Text = DateTime.Now.Month.ToString();
        }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public void Init()
        {
            for (int i=-10;i<10;i ++)
            {
                this.txtBeginyear.Items.Add(DateTime.Now.AddYears(i).Year);
            }
            this.txtBeginyear.Text = DateTime.Now.Year.ToString();
            this.txtBeginMonth.Text = DateTime.Now.Month.ToString();
            this.txtEndyear.Text = DateTime.Now.Year.ToString();
            this.txtEndMonth.Text = DateTime.Now.Month.ToString();
        }
    }
}
