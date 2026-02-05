using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class ScreenForm : Form
    {
        public ScreenForm()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.FromArgb(0, 0, 0, 0);
            //this.Opacity = 0.10;
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.UpdateStyles();
            ScreenImage = Feng.Drawing.ImageHelper.GetScreenImage();
            //ScreenImage.Save("aaa.jpg");
        }
        
        public Rectangle GetSelectRect()
        {
            Rectangle rect = new Rectangle(Feng.Utils.ConvertHelper.Min(MouseDownPoint.X, EndPoint.X),
                Feng.Utils.ConvertHelper.Min(MouseDownPoint.Y, EndPoint.Y),
                 Feng.Utils.ConvertHelper.Abs(MouseDownPoint.X - EndPoint.X),
                 Feng.Utils.ConvertHelper.Abs(MouseDownPoint.Y- EndPoint.Y)
                );
            return rect;
        }

        public Bitmap GetSelectImage()
        {
            Rectangle rect = this.GetSelectRect();
            Bitmap bmp = null;
            bmp = Feng.Drawing.ImageHelper.CopyBitmap(ScreenImage, rect);
            return bmp;
        }

        private Bitmap ScreenImage = null;
 
        protected override void OnPaint(PaintEventArgs e)
        {
            if (ScreenImage != null)
            {
                e.Graphics.DrawImageUnscaled(ScreenImage, 0, 0);
            }
            Region region = new System.Drawing.Region();
            region.Union(new Rectangle(0, 0, this.Width, this.Height));
            region.Exclude(this.GetSelectRect());
            Color c = Color.FromArgb(100, Color.Gray);
            using (SolidBrush brush = new SolidBrush(c))
            {
                e.Graphics.FillRegion(brush, region);
            }
            base.OnPaint(e);
        }
        public Point MouseDownPoint = Point.Empty;
        public Point EndPoint = Point.Empty;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                EndPoint = System.Windows.Forms.Control.MousePosition;
                this.Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                EndPoint = System.Windows.Forms.Control.MousePosition;
                this.Invalidate();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                Bitmap bmp = GetSelectImage();
                //bmp.Save("bbb.jpg");
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseDownPoint = System.Windows.Forms.Control.MousePosition;
            EndPoint = Point.Empty;
            base.OnMouseDown(e);
        }
    }
}
