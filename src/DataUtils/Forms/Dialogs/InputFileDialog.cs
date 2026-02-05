using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class InputFileDialog : Form
    {
        public InputFileDialog()
        {
            InitializeComponent();
        }
        public static DialogResult ShowInputTextDialog(string title)
        {
            using (InputTextDialog dlg = new InputTextDialog())
            {
                dlg.Text = title;
                dlg.StartPosition = FormStartPosition.CenterScreen;
                return dlg.ShowDialog();             
            }
        }
        public string Value { get {
            return this.txtInput.Text;
        }
            set
            {
                this.txtInput.Text = string.Empty;
            }
        }
    }
}
