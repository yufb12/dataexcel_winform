using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Controls.GridControl;
using System.Threading;

namespace Feng.Forms.Views
{
    public class TimeView : DivView
    {
        public TimeView()
        {

        }
 
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        { 
            return base.OnMouseDown(sender, e, ve);
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        { 
            return base.OnMouseUp(sender, e, ve);
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        { 
            return base.OnMouseMove(sender, e, ve);
        }

        public override bool OnInit(object sender, EventViewArgs ve)
        {
            if (this.AutoRun)
            {
                Start();
            }
            return base.OnInit(sender, ve);
        }

        public override DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, Script);
                    bw.Write(2, Interval);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }
        public virtual object ExecObject { get; set; }
        public virtual Feng.Script.CBEexpress.NetParser Function { get; set; }
        private System.Threading.Thread Thread;

        public virtual void Start()
        {
            if (Thread == null)
            {
                Thread = new System.Threading.Thread(new ParameterizedThreadStart(Exec));
                Thread.IsBackground = true;
                Thread.Start(ExecObject);
            }

        }

        public virtual void Close()
        {
            Thread.Abort();
        }

        public virtual void Exec(object state)
        {
            Function.Exec(Script);
        }

        public virtual bool AutoRun { get; set; }

        public virtual int Interval { get; set; }
      
        public virtual string Script { get; set; }

    }
}

