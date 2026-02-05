using Feng.Data;
using Feng.Drawing;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class Circle:DivView
    {
        public Circle()
        { 

        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            //Brush b = new System.Drawing.Drawing2D.PathGradientBrush();
            //b = new System.Drawing.TextureBrush();
            //b = new System.Drawing.Drawing2D.HatchBrush();
            //b = new System.Drawing.Drawing2D.LinearGradientBrush();
            //b = new System.Drawing.Drawing2D.PathGradientBrush();
            //b = new System.Drawing.SolidBrush();

            //g.Graphics.FillEllipse(new  Brush (), this.Rect);
            return base.OnDraw(sender, g);
        }
    }
}

