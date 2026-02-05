using Feng.Data;
using Feng.Drawing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class ScrollView : DivView
    {
        public ScrollView()
        {
        }
        public ScrollThumdView thumd = null;
        public ScrollThumdView Thumd
        {
            get
            {
                if (thumd == null)
                {
                    thumd = new ScrollThumdView();
                    thumd.Width = this.Width;
                }
                return thumd;
            }
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            return base.OnDraw(sender, g);
        }
        public int Value { get; set; }
        public int Max { get; set; }
        public int ItemSize { get; set; }
        public int ItemCount { get; set; }
        public override bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            Value = Value + 3;
            int top = Value / Max * this.Height;
            this.thumd.Top = top;
            return true;
        }
        public override DataStruct Data { 
            get { return new DataStruct(); } 
        }

        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            int top = Value / Max * this.Height;
            this.thumd.Top = top;
            return base.OnSizeChanged(sender, e, ve);
        }
    }
    public class ScrollThumdView : DivView
    {
        public ScrollThumdView()
        {

        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.FillRectangle(Brushes.AntiqueWhite, this.Bounds); ;
            return base.OnDraw(sender, g);
        }

        public override DataStruct Data
        {
            get
            {
                return new DataStruct();
            }
        }

    }
}

