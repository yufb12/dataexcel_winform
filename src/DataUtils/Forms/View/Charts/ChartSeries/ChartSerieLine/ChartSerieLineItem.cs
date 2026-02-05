using Feng.Drawing;
using Feng.Enums;
using Feng.Forms.Interface;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class ChartSerieLineItem : ViewItem
    {
        public ChartSerieLineItem()
        { 

        }

        public ChartSerieLine ChartSeriebar { get; set; }
        public TextLabel TextLabel { get; set; }

        public override bool OnDraw(object sender, GraphicsObject g)
        { 
            if (ChartSeriebar.Style != null)
            {
                ChartSeriebar.Style.View = this;
                ChartSeriebar.Style.OnDraw(sender, g);
                return false;
            }
            Brush brush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.BackColor);
            g.Graphics.FillRectangle(Brushes.Red, this.Rect);
            return base.OnDraw(sender, g);
        }


    }

}
