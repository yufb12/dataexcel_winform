using Feng.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class xAxisCategory : xAxisBase
    {
        public xAxisCategory()
        {
            Itemes = new AxisItemCollection();
            Height = 23;
        }
        [Browsable(true)]
        [DefaultValue(23)]
        public override int Height 
        { 
            get { return base.Height; } 
            set { base.Height = value; }
        }

        public override int GetXPoint(int index)
        {
            if (Itemes.Count < 1)
                return this.Left;
            if (index >= Itemes.Count)
                return this.Right;
            return Itemes[index].Left;
        } 

        public override AxisItemCollection Itemes
        {
            get;
            set;
        }

        public virtual void Add(string text)
        {
            Add(new xAxisItem() { Text = text });
        }

        public virtual void Add(string[] textes)
        {
            foreach (string text in textes)
            {
                Add(new xAxisItem() { Text = text });
            }
        }

        public virtual void Add(xAxisItem item)
        {
            Itemes.Add(item);
            this.AddView(item);
        }

        public virtual void RefreshItem()
        {
            if (Itemes.Count < 1)
                return;
            int left = this.Left;
            int d = this.Width / Itemes.Count;
            foreach (xAxisItem item in Itemes)
            {
                item.Top = this.Top+2;
                item.Left = left;
                item.Width = d;
                item.Height = this.Height;
                left = left + d;
            }
        }

        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            RefreshItem();
            return base.OnRefresh(sender, e, ve);
        }
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            RefreshItem();
            return base.OnSizeChanged(sender, e, ve);
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.DrawLine(System.Drawing.Pens.Black, this.Left, this.Top, this.Right, this.Top);
            return base.OnDraw(sender, g);
        }
    }
 
}
