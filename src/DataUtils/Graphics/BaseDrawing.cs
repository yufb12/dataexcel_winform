using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Forms.Skin
{ 
    public class BaseDrawing
    { 
        
    }

    public class BaseRectDrawing
    {
        static BaseRectDrawing instanc = null;
        public static BaseRectDrawing Instanc
        {
            get
            {
                if (instanc == null)
                {
                    instanc = new BaseRectDrawing();
                }
                return instanc;
            }
        }


        public virtual void Draw(Graphics g, Rectangle rect)
        {
            g.DrawRectangle(Pens.Black, rect);
        }
        public virtual void Draw(Graphics g, Rectangle rect, Color color)
        {
            using (Pen pen = new Pen(color, 1))
            {
                g.DrawRectangle(pen, rect);
            }
        }
        public virtual void Draw(Graphics g, Rectangle rect, Color color, float width)
        {
            using (Pen pen = new Pen(color, width))
            {
                g.DrawRectangle(pen, rect);
            }
        }
        public virtual void Draw(Graphics g, Rectangle rect, Color color, float width, float[] DashPattern)
        {
            using (Pen pen = new Pen(color, width))
            {
                pen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                pen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
                g.DrawRectangle(pen, rect);
            }
        }

        public virtual void DrawDashPattern(Graphics g, Rectangle rect)
        {
            using (Pen pen = new Pen(Color.BlueViolet, 2))
            {
                pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
                pen.Width = 4;
                pen.DashPattern = new float[] { 2.0F, 1.0F };
                g.DrawRectangle(pen, rect);
            }
        }
    }

    public class CopyRectDrawing : BaseRectDrawing
    {

    }
}
