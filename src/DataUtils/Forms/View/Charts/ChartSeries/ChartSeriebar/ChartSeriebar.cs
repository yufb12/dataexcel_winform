using Feng.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public interface IItemSize
    {
        int ItemLeft { get; set; }
        int ItemTop { get; set; }
        int ItemWidth { get; set; }
        int ItemHeight { get; set; }
    }
    public class ChartSeriebar : ChartSerie, IItemSize
    {
        public ChartSeriebar()
        {
            Itemes = new AxisItemCollection();
            Zlevel = 10;
            ItemWidth = 22;
        }

        public virtual AxisItemCollection Itemes
        {
            get;
            set;
        }

        public virtual void Add(decimal value)
        {
            Add(new ChartSeriebarItem() { Text = value.ToString(), Value = value, ChartSeriebar = this }); ;
        }
        public virtual void Add(ChartSeriebarItem item)
        {
            Itemes.Add(item);
            item.Height = this.Height;
            item.Left = this.Left;
            item.Top = this.Top;
            item.Width = 20;
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
                item.Left = left + this.ItemLeft;
                int top = this.yAxis.GetXPoint(item.Value);
                item.Top = top;
                item.Height = this.Bottom - item.Top;
            }
        }
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            refresh();
            return base.OnSizeChanged(sender, e, ve);
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            return base.OnDraw(sender, g);
        }

        public StyleBase Style { get; set; }
        public int ItemLeft { get; set; }
        public int ItemTop { get; set; }
        public int ItemWidth { get; set; }
        public int ItemHeight { get; set; }
    }
 
}
