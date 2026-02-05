using Feng.Data;
using Feng.Drawing;
using Feng.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class HSLColorView : DivView
    {
        public HSLColorView()
        {
            BlockWidth = 25;
            BlockHeight = 12;
        } 

        public int BlockWidth { get; set; }

        public int BlockHeight { get; set; }

        List<ColorHSLViewItem> List = new List<ColorHSLViewItem>();
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {  
            return base.OnSizeChanged(sender, e, ve);
        }
        public int CreateHslItems()
        {
            List.Clear();
            int huecount = (this.Width -10) / (BlockWidth + 2);
            int hue = 360 / huecount;
            int left = 5;
            int top = 5;
            for (int k = 10; k >=0; k--)
            {
                for (int j = 10; j >= 0; j--)
                {
                    left = 5;
                    for (int i = 0; i < huecount; i++)
                    {
                        left = left + BlockWidth;
                        ColorHSL model = new ColorHSL();
                        model.Hue = hue*i;
                        model.Saturation = j * 10;
                        model.Lightness = k * 10;

                        ColorHSLViewItem item = new ColorHSLViewItem();
                        item.HSL = model;
                        item.X = left;
                        item.Y = top;
                        item.Color = model.ToColor();
                        List.Add(item);
                    }
                    top = top + BlockHeight;
                }
            }
            top = top + BlockHeight;
            return top;
        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            foreach (ColorHSLViewItem item in List)
            {
                g.Graphics.FillRectangle(SolidBrushCache.GetSolidBrush(item.Color), item.X, item.Y, BlockWidth, BlockHeight);
            }
            return base.OnDraw(sender, g);
        }

        public override DataStruct Data { get { return new DataStruct(); } }
 

    }

    public class ColorHSLViewItem
    {
        public Color Color { get; set; }
        public ColorHSL HSL { get; set; }
        public int X{ get; set; }
        public int Y{ get; set; }
        public override string ToString()
        {
            return X + ":" + Y;
        }
    }
}

