using Feng.Forms.EventHandlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class SplashFormBase : Form
    {
        public SplashFormBase()
        {
            InitializeComponent();
        }

        int count = 100;
        private void timer1_Tick(object sender, EventArgs e)
        { 
            count = count - 4;
            if (count < 10)
            {
                this.timer1.Enabled = false;
                this.timer1.Tick -= timer1_Tick;
                this.Close();
                this.Dispose();
            }
            else
            {
                this.Opacity = (double)count / 100;
            }
            Ending();
        }

        static SplashFormBase sp = null;

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

        public virtual void Ending()
        {

        }

        public static void StartFlashForm(SplashFormBase frm)
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(StartFlashForm));
 
            th.IsBackground = true;
            th.Start(frm);
        }

        private static void StartFlashForm(object obj)
        {
            SplashFormBase frm = obj as SplashFormBase;
            if (frm != null)
            {
                sp = frm;
                sp.TopLevel = true;
                sp.TopMost = true;
                sp.Show();
            }
        }

        public virtual void EndFlashForm()
        {
            this.timer1.Enabled = true;
        }


        public static void Finish()
        {
            Stop();
        }

        public static void Stop()
        {

            try
            {

                if (sp == null)
                    return;
                if (sp.InvokeRequired)
                {
                     CloseHandler d = new CloseHandler(Stop);
                    sp.Invoke(d);
                }
                else
                {
                    sp.EndFlashForm();
                }
            }
            catch (Exception  )
            { 
            }

        }

        public static void CloseFlashForm()
        { 
            Stop();
        }

    }
}
