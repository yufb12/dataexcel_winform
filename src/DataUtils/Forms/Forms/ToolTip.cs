using Feng.Forms.Controls;
using Feng.Forms.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Popup
{
    public partial class ToolTip : PopupForm
    {
        public ToolTip()
            : base()
        {
            
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();
            this.BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                e.Graphics.DrawString(this.tooltiptext, this.Font, Brushes.Black, 6, 6);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Popup", "ToolTip", "OnPaint", ex);
            }
            base.OnPaint(e);
        }
        protected override void OnLoad(EventArgs e)
        { 
            base.OnLoad(e);
        }
        public void SizeText(string caption)
        {
            try
            {
                SizeF sf = this.CreateGraphics().MeasureString(caption, this.Font);
                this.Width = (int)sf.Width + 13;
                this.Height = (int)sf.Height + 13;
            }
            catch (Exception)
            { 
            }
        }
        public void ShowText(string text, int time, Point pt, float opacity)
        {
            ShowTime = DateTime.Now;
            SizeText(text);
            timer.Enabled = true;
            tooltiptext = text;
            this.Popup(pt);
        }
        private string tooltiptext = string.Empty;
        Timer timer = null;
        int timelong = 5;
        DateTime LastTime = DateTime.Now;
        DateTime ShowTime = DateTime.Now;
        public void InitTimer()
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                LastTime = DateTime.Now;
                if ((LastTime - ShowTime).TotalSeconds > timelong)
                {
                    timer.Enabled = false;
                    this.Hide();
                    return;
                }
                this.Invalidate();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("ToolTip", "ToolTip", "Timer_Tick", ex);
            }
        }

        private static ToolTip toolTip = null;

        public static void Show(Form form, string text, int time, Point pt, float opacity)
        {
            if (toolTip == null)
            {
                toolTip = new ToolTip();
                toolTip.InitTimer();
            }
            toolTip.ParentEditForm = form;
            toolTip.ShowText(text, time, pt, opacity);
        }

        public static void Show(string text)
        {
            Show(null, text, 15,Point.Add ( System.Windows.Forms.Control.MousePosition ,new Size() {  Height =10 }), 0.7f);
        }

        public static void HideToolTip()
        {
            if (toolTip != null)
            {
                toolTip.ClosePopup();
            }
        }
    }
}
