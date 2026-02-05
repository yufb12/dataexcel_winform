using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
  

    public partial class WaitingForm : Form
    {
        public WaitingForm()
        {
            InitializeComponent();
        }

        private int _ShowTime = 100;
        public int ShowTime
        {
            get
            {
                return _ShowTime;
            }
        }
        private int _overshowtime = 0;
        private int OverShowTime { get { return _overshowtime; } }
        private void timer1_Tick(object sender, EventArgs e)
        {
            _overshowtime = _overshowtime + 1;
            if ((ShowTime - OverShowTime) < 1)
            {
                this.timer1.Enabled = false;
                this.timer1.Tick -= timer1_Tick;
                WaitingForm.waittingform = null;
                this.Close();
                this.Dispose();
            }
            else
            {
                this.Opacity = 1 - (double)OverShowTime / ShowTime;
                this.pictureBox1.Invalidate();
            }
        }

        public static WaitingForm waittingform = null;

        private bool _autoclose = false;
        public virtual bool AutoClose
        {
            get
            {
                return this._autoclose;
            }
            set
            {
                this._autoclose = value;
            }
        }

        public static void StartFlashForm(WaitingForm frm)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(StartFlashForm));
            //th.ApartmentState = System.Threading.ApartmentState.STA;
            th.Start(frm);
        }

        private static void StartFlashForm(object obj)
        {

            try
            {

                WaitingForm frm = obj as WaitingForm;
                if (frm != null)
                {
                    waittingform = frm;
                    waittingform.TopLevel = true;
                    waittingform.TopMost = true;
                    if (waittingform.ShowTime > 0)
                    {
                        waittingform.timer1.Enabled = true;
                    } 
                    waittingform.ShowDialog(); 
                }
            }
            catch (Exception  )
            { 
            }
        
        }

        public virtual void EndFlashForm()
        {
            _overshowtime = this.ShowTime;
            this.timer1.Enabled = true;
        }

        private delegate void CloseHandler();

        public static void Stop()
        {

            try
            {

                if (waittingform == null)
                    return;
                if (waittingform.IsDisposed)
                    return;
                if (waittingform.InvokeRequired)
                {
                    CloseHandler d = new CloseHandler(Stop);
                    waittingform.Invoke(d);
                }
                else
                {
                    waittingform.EndFlashForm();
                }
            }
            catch (Exception )
            { 
            }
        
        }

        public static void Start(string title)
        { 
            Start(title, -1);
        }
 
        public static void Start(string title, int showtime)
        {
            if (waittingform == null)
            {
                waittingform = new WaitingForm();
                waittingform.Title = title;
                waittingform._ShowTime = showtime;
                StartFlashForm(waittingform);
            }
            if (waittingform.IsDisposed)
            {
                waittingform = new WaitingForm();
                waittingform.Title = title;
                waittingform._ShowTime = showtime;
                StartFlashForm(waittingform);
            }
            waittingform.Title = title;
            waittingform.Invalidate();
        }
        public static void CloseFlashForm()
        { 
            Stop();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            try
            { 
                Font font = new Font(this.Font.FontFamily, 9);
                e.Graphics.DrawString(_title, font, Brushes.Snow, 6, 23);
            }
            catch (Exception  )
            { 
            }
        
        }
        private string _title = "正在执行请稍候...";
        public string Title { get { return _title; } set { _title = value; } }

    }

    public class Waiting : IDisposable
    {
        public Waiting(string title)
        {
            WaitingForm.Start(title);
        }
        public void SetTtitle(string title)
        {
            WaitingForm.waittingform.Title = title;
        }
        public void Dispose()
        {
            WaitingForm.Stop();
        }
    }
}
