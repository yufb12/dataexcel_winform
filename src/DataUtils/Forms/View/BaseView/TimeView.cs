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
    public class ClockView : MoveView
    {
        public ClockView()
        {
            this.Left = 130;
            this.Top = 130;
            this.Width = 60;
            this.Height = 60;
        }

        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.ActionRect.Contains(ve.ViewPoint))
            {
                return true;
            }
            return base.OnMouseDown(sender, e, ve);
        }

        public override bool OnDrawBack(object sender, GraphicsObject g)
        {
            return base.OnDrawBack(sender, g);
        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            Color color = System.Drawing.Color.AliceBlue;
            System.Drawing.SolidBrush brush = SolidBrushCache.GetSolidBrush(color);
            try
            {
                g.Graphics.FillEllipse(brush, this.Bounds);
                Bitmap bmp = Feng.Utils.Properties.Resources.CommandPlay16;
                if (this.StateRun)
                {
                    bmp = Feng.Utils.Properties.Resources.CommandPause16;
                }
                g.Graphics.DrawImageUnscaled(bmp, ActionRect);

                g.Graphics.DrawEllipse(Pens.GreenYellow, this.Bounds);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "LockView", "OnDraw", ex);
            }
            finally
            {
            }
            return base.OnDraw(sender, g);
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
        private bool state = false;
        public bool StateRun
        {
            get
            {
                return this.state;
            }
        }

        public Rectangle ActionRect
        {
            get
            {
                Rectangle rect = new Rectangle();
                rect.X = this.Left + this.Width / 2 - 9;
                rect.Y = this.Top + this.Height / 2 - 9;
                rect.Width = 16;
                rect.Height = 16;
                return rect;
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

