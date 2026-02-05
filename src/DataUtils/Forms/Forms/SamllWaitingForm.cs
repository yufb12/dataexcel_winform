using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class SamllWaitingForm : SplashFormBase
    {
        public SamllWaitingForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font(this.Font.FontFamily, 9);
            e.Graphics.DrawString(_title, font, Brushes.Snow, 6, 23);
        } 
        private string _title = "正在执行请稍候...";
        public string Title { get { return _title; } set { _title = value; } }
        private static SamllWaitingForm _CurrentSmallWaitingForm = null;
        public static SamllWaitingForm CurrentSmallWaitingForm
        {
            get {
                return _CurrentSmallWaitingForm;
            }
        }
        public static void Start()
        {
            StartFlashForm(new SamllWaitingForm());
        }

        public static void SetTitle(string value)
        {
            if (CurrentSmallWaitingForm != null)
            {
                CurrentSmallWaitingForm.Title = value;
            } 
        } 
    }
}
