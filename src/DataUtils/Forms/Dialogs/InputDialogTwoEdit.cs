using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class InputDialogTwoEdit : Form
    {
        public InputDialogTwoEdit()
        {
            InitializeComponent();
        }


        public InputDialogTwoEdit(string caption1,string caption2)
        {
            InitializeComponent();
            this.label1.Text = caption1;
            this.label2.Text = caption2;
        }
        public InputDialogTwoEdit(string caption1, string caption2,string text)
        {
            InitializeComponent();
            this.label1.Text = caption1;
            this.label2.Text = caption2;
            this.Text = text;
        }
    }
}
