using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class TextDialog : Form
    {
        public TextDialog()
        {
            InitializeComponent();
        }
        public static DialogResult ShowInputTextDialog(string title, string text)
        {
            using (TextDialog dlg = new TextDialog())
            {
                dlg.Text = title;
                dlg.Value = text;
                dlg.StartPosition = FormStartPosition.CenterScreen;
                return dlg.ShowDialog();             
            }
        }
        public string Value
        {
            get
            {
                return this.txtInput.Text;
            }
            set
            {
                this.txtInput.Text = value;
            }
        }
    }
}
