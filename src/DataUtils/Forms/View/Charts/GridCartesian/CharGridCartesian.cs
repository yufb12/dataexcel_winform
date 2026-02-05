using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public abstract class CharGridBase : DivView
    {

    }

    public class CharGridCartesian : CharGridBase
    {
        public CharGridCartesian()
        {
            yAxises = new yAxisCollection();
            xAxises = new xAxisCollection();
            ChartContents = new ChartContents();
            MoveLine = new MoveLineView();
            this.AddView(ChartContents);
        }
        public virtual ChartContents ChartContents { get; set; }
        public virtual yAxisCollection yAxises { get; set; }
        public virtual xAxisCollection xAxises { get; set; }
        public virtual MoveLineView MoveLine { get; set; }
        public virtual ChartGridAxisItemCollection ChartGridAxisItemes { get; set; }
        public void AddXAxis(xAxisBase axisbase)
        {
            xAxises.Add(axisbase);
            this.AddView(axisbase);
        }
        public void AddYAxis(yAxisBase yaxisbase)
        {
            yAxises.Add(yaxisbase);
            this.AddView(yaxisbase);
        }
        protected void refrersh()
        {

            xAxises.Sort();
            yAxises.Sort();
            RefreshyAxis();
            RefreshxAxis();

        }
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            refrersh();
            return base.OnSizeChanged(sender, e, ve);
        }
        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            refrersh();
            return base.OnRefresh(sender, e, ve);
        }

        protected void RefreshyAxis()
        {
            int width = 0;
            int left = this.Left;
            int right = this.Right;
            foreach (yAxisBase item in yAxises)
            {
                if (item.AlignMode == AlignMode.Near)
                {
                    item.Left = left;
                    left = left + item.Width;
                }
                if (item.AlignMode == AlignMode.Far)
                {
                    item.Left = right - item.Width;
                    right = right - item.Width;
                }
            }
            width = right - left;
            ChartContents.Width = width;
            ChartContents.Left = left;
            foreach (xAxisBase item in xAxises)
            {
                
                item.Width = width;
                item.Left = left; 
            }
        }

        protected void RefreshxAxis()
        {
            int height = 0;
            int top = this.Top;
            int bottom = this.Bottom;
            foreach (xAxisBase item in xAxises)
            {
                if (item.AlignMode == AlignMode.Near)
                {
                    item.Top = bottom - item.Height;
                    bottom = bottom - item.Height;
                }
                if (item.AlignMode == AlignMode.Far)
                {
                    item.Top = top;
                    top = top + item.Height;
                }
            }
            height = bottom - top;
            ChartContents.Height = height;
            ChartContents.Top = top;
            foreach (yAxisBase item in yAxises)
            {
                item.Height = height;
                item.Top = top; 
            }
        }

        public override bool OnDraw(object sender, Drawing.GraphicsObject g)
        {
            return base.OnDraw(sender, g);
        }
    }

}
