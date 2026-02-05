using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Feng.Forms.Dialogs
{
    public partial class RegexDialog : Form
    {
        public RegexDialog()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                this.richTextBox2.Clear();
                string pattern = this.textBox1.Text;
                string strResponse = this.richTextBox1.Text;
                MatchCollection matches = new Regex(pattern).Matches(strResponse);
                this.richTextBox2.AppendText(string.Format("找到匹配项={0}\r\n", matches.Count));
                foreach (Match match in matches)
                {
                    string strRef = string.Empty;
                    strRef = match.Value;
                    this.richTextBox2.AppendText(string.Format("begin={0}\r\n", "##########################"));
                    this.richTextBox2.AppendText(string.Format("匹配项内容={0}，找到分组={1}\r\n", strRef, match.Groups.Count));
                    for (int i = 1; i < match.Groups.Count; i++)
                    {
                        this.richTextBox2.AppendText(string.Format("begin={0}\r\n", "************************"));
                        this.richTextBox2.AppendText(string.Format("找到组内容={0}\r\n", match.Groups[i].Value));
                        this.richTextBox2.AppendText(string.Format("end={0}\r\n", "************************"));
                    }
                    this.richTextBox2.AppendText(string.Format("end={0}\r\n", "##########################"));
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            } 
        }
    }
}
