using System;
using System.Windows.Forms;

namespace Feng.Forms.Events
{
    public class MouseDoubleClickProxy
    {
        private System.DateTime lastclicktime = DateTime.Now;
        private System.Windows.Forms.Control owncontrol = null;
        private MouseEventHandler eventHandler = null;
        public void Init(System.Windows.Forms.Control control, MouseEventHandler handler)
        {
            control.MouseDown += Control_MouseDown;
            this.owncontrol = control;
            eventHandler = handler;
        }
        public void Init(System.Windows.Forms.Control control)
        {
            control.MouseDown += Control_MouseDown;
            this.owncontrol = control; 
        }
        private void Control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            { 
                if ((DateTime.Now - lastclicktime).TotalMilliseconds < 300)
                {
                    if (eventHandler == null)
                    {

                        if (this.owncontrol.FindForm().WindowState == FormWindowState.Maximized)
                        {
                            this.owncontrol.FindForm().WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            this.owncontrol.FindForm().WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        eventHandler(sender, e);
                    }
                    return;
                }
                lastclicktime = DateTime.Now;
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("", "", "", ex);
            }
        }
    }

}

