using Feng.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Dialogs
{
    public partial class ColorForm : Form
    {
        public ColorForm()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            InitializeComponent();
        }
        private List<ColorText> list = new List<ColorText>();
        public class ColorText
        {
            public string Text { get; set; }
            public Color Color { get; set; }
        }
        private void ColorDialog_Paint(object sender, PaintEventArgs e)
        {
            if (list.Count <= 0)
            {
                Type t = typeof(Color);
                System.Reflection.PropertyInfo[] pis = t.GetProperties();
                foreach (System.Reflection.PropertyInfo pi in pis)
                {
                    try
                    {
                        Color color = (Color)pi.GetValue(null, null);
                        ColorText m = new ColorText();
                        m.Text = pi.Name;
                        m.Color = color;
                        list.Add(m);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
            {
                int i = 0;
                int top = 20;
                int width = 72;
                int left=20;
                int height=20;
                int index = 0;
          
                    for (; index < list.Count ;index++ )
                    {
                        ColorText k = list[index];
                        Rectangle rect = new Rectangle(left, top, width, height);
                        Feng.Drawing.GraphicsHelper.FillRectangle(e.Graphics, SolidBrushCache.GetSolidBrush(k.Color), rect);
                        rect = new Rectangle(left + width, top, width, height);
                        Feng.Drawing.GraphicsHelper.FillRectangle(e.Graphics, SolidBrushCache.GetSolidBrush(k.Color), rect);
                        e.Graphics.DrawString(k.Text, this.Font, Brushes.Black, rect);
                        i++;
                        top = top + height + 5;
                        if (i >= 18)
                        {
                            left = left + width * 2 + 10;
                            top = 20;
                            i = 0;
                        }
                    } 
            }
        }
    }
}
