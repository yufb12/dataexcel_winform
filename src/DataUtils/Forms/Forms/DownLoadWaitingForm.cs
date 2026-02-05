using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class DownLoadWaitingForm : SplashFormBase
    {
        public DownLoadWaitingForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font(this.Font.FontFamily, 9);
            e.Graphics.DrawString(_title, font, Brushes.Snow, 6, 23);
        } 
        private string _title = "正在下载数据..";
        public string Title { get { return _title; } set { _title = value; } }

        public static void Start()
        {
            StartFlashForm(new DownLoadWaitingForm());
        }

        public override void Ending()
        {
            _title = "数据下载完成！";
            base.Ending();
        }

    }
}
