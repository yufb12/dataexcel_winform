using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class DownLoadSplashForm : SplashFormBase
    {
        public DownLoadSplashForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font(this.Font.FontFamily, 16, FontStyle.Bold);
            e.Graphics.DrawString(_title, font, Brushes.WhiteSmoke, 100, 30);
            font = new Font(this.Font.FontFamily, 9);
            e.Graphics.DrawString(_title2, font, Brushes.Snow, 200, 100);
        }
 
        private string _title = "系统版本已更新";
        public string Title { get { return _title; } set { _title = value; } }
        private string _title2 = "正在下载最新版本.......";
        public string Title2 { get { return _title2; } set { _title2 = value; } }

        public static void Start()
        {
            StartFlashForm(new DownLoadSplashForm());
        }

        public override void Ending()
        {
            _title2 = "系统已加载！";
            base.Ending();
        }

    }
}
