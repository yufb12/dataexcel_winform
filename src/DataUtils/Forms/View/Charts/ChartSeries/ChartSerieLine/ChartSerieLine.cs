using Feng.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class ChartSerieLine:ChartSerie 
    { 
        public ChartSerieLine()
        {
            Itemes = new AxisItemCollection();
            Zlevel = 20;
        }

        public virtual AxisItemCollection Itemes
        {
            get;
            set;
        }

        public virtual void Add(decimal value)
        {
            Add(new xAxisItem() { Text = value.ToString (), Value = value });
        }
        public virtual void Add(xAxisItem item)
        {
            Itemes.Add(item);
            this.AddView(item);
        } 
        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            refresh();
            return base.OnRefresh(sender, e, ve);
        }
        protected void refresh()
        {

            for (int i = 0; i < Itemes.Count; i++)
            {
                ViewItem item = Itemes[i];
                int left = this.xAxis.GetXPoint(i);
                item.Left = left;
                int top = this.yAxis.GetXPoint(item.Value);
                item.Top = top;
            }
        }
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            refresh();
            return base.OnSizeChanged(sender, e, ve);
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Point start = Point.Empty;
            for (int i = 0; i < Itemes.Count; i++)
            {
                ViewItem item = Itemes[i];
                Point point = new Point(item.Left, item.Top);
                if (start != Point.Empty)
                {
                    if (Style != null)
                    {
                        Style.OnDraw(this ,g, start, point);
                    }
                    else
                    { 
                        g.Graphics.DrawLine(Pens.Black, start, point);
                    }
                }
                start = point;
            }
            return base.OnDraw(sender, g);
        }
        public StylePen Style { get; set; }
    }
 
}
