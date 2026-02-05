using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class BaseForm : Form
    { 
        public BaseForm()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true); 
            this.UpdateStyles();
            this.Load += BaseForm_Load;
        }
        private bool loading = false;
        void BaseForm_Load(object sender, EventArgs e)
        {
            loading = true;
        }
        public override Size MaximumSize
        {
            get
            {
                if (loading)
                {
                    Size size = base.MaximumSize;
                    if (size == Size.Empty)
                    {
                        size = System.Windows.Forms.Screen.FromControl(this).WorkingArea.Size;
                    }
                    return size;
                }
                return base.MaximumSize;
            }
            set
            {
                base.MaximumSize = value;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            //try
            //{
            //    Rectangle rect = new Rectangle(1, 1, this.Width - 2, this.Height - 2);
            //    e.Graphics.DrawRectangle(Pens.Gray, rect); 
            //}
            //catch (Exception ex)
            //{
            //    //SysTools.ExceptionHelper.ShowException(ex);
            //}
  
            base.OnPaint(e);
        }

        private void panelMenu_MouseDown(object sender, MouseEventArgs e)
        {
          Feng.Utils.UnsafeNativeMethods.MoveWindow(this.Handle);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Feng.Utils.UnsafeNativeMethods.MoveWindow(this.Handle);
        }
        Point downpoint = Point.Empty;
        Rectangle downsize = Rectangle.Empty;
        public int SizeMode = -1;
        public int SizeWEL = 1;
        public int SizeWER = 2;
        public int SizeNSL = 3;
        public int SizeNSR = 5;
        public int SizeNWSEL = 6;
        public int SizeNWSER = 7;
        public int SizeNESWL = 8;
        public int SizeNESWR = 9;
        public int PaddingRect = 2;
        public int MarginRect = 3;
        public Rectangle RectSizeWEL
        {
            get {
                return new Rectangle(this.Width - PaddingRect, MarginRect, PaddingRect, this.Height - MarginRect - MarginRect);
            }
        }
        public Rectangle RectSizeWER
        {
            get
            {
                return new Rectangle(0, MarginRect, PaddingRect, this.Height - MarginRect - MarginRect);
            }
        }
        public Rectangle RectSizeNSL
        {
            get
            {
                return new Rectangle(MarginRect, 0, this.Width - MarginRect - MarginRect, PaddingRect);
            }
        }
        public Rectangle RectSizeNSR
        {
            get
            {
                return new Rectangle(MarginRect, this.Height - PaddingRect, this.Width - MarginRect - MarginRect, PaddingRect);
            }
        }
        public Rectangle RectSizeNWSEL
        {
            get
            {
                return new Rectangle(this.Width - PaddingRect*3, this.Height - PaddingRect*3, PaddingRect*3, PaddingRect*3);
            }
        }
        public Rectangle RectSizeNWSER
        {
            get
            {
                return new Rectangle(0, 0, PaddingRect, PaddingRect);
            }
        }
        public Rectangle RectSizeNESWL
        {
            get
            {
                return new Rectangle(0, this.Height - PaddingRect, PaddingRect, PaddingRect);
            }
        }
        public Rectangle RectSizeNESWR
        {
            get
            {
                return new Rectangle(this.Width - PaddingRect, 0, PaddingRect, PaddingRect);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { 
                downpoint = System.Windows.Forms.Control.MousePosition;
                downsize = this.Bounds;
                Rectangle rect = this.RectSizeWEL;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeWEL;
                    return;
                } rect = this.RectSizeWER;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeWER;
                    return;
                }


                rect = this.RectSizeNSL;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeNSL;
                    return;
                }
                rect = this.RectSizeNSR;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeNSR;
                    return;
                }

                rect = this.RectSizeNWSEL;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeNWSEL;
                    return;
                }
                rect = this.RectSizeNWSER;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeNWSER;
                    return;
                }

                rect = this.RectSizeNESWL;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeNESWL;
                    return;
                }
                rect = this.RectSizeNESWR;
                if (rect.Contains(e.Location))
                {
                    SizeMode = SizeNESWR;
                    return;
                }
               Feng.Utils.UnsafeNativeMethods.MoveWindow(this.Handle);
            }
           
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            SizeMode = -1; 
            this.Cursor = Cursors.Default;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point pt = System.Windows.Forms.Control.MousePosition; 
            base.OnMouseMove(e);
            if (SizeMode == SizeNWSEL)
            {
                int x = pt.X - downpoint.X;
                int y = pt.Y - downpoint.Y;
                this.Width = downsize.Width + x;
                this.Height = downsize.Height + y;
            }
            else if (SizeMode == SizeWEL)
            {
                int x = pt.X - downpoint.X;
                int y = downpoint.Y - downpoint.Y;
                this.Width = downsize.Width + x;
                this.Height = downsize.Height + y;
            }
            else if (SizeMode == SizeNSR)
            {
                int x = downpoint.X - downpoint.X;
                int y = pt.Y - downpoint.Y;
                this.Width = downsize.Width + x;
                this.Height = downsize.Height + y;
            }
            else if (SizeMode == SizeNESWR)
            {
                int x = pt.X - downpoint.X;
                int y = pt.Y - downpoint.Y; 
                this.Width = downsize.Width + x;
                this.Height = downsize.Height - y;
                this.Top = downsize.Top + y; 
            }
            else if (SizeMode == SizeNSL)
            {
                int x = pt.X - downpoint.X;
                int y = pt.Y - downpoint.Y;  
                this.Height = downsize.Height - y;
                this.Top = downsize.Top + y; 
            }
            else if (SizeMode == SizeNWSER)
            {
                int x = pt.X - downpoint.X;
                int y = pt.Y - downpoint.Y;
                this.Width = downsize.Width - x;
                this.Height = downsize.Height - y;
                this.Left = downsize.Left + x; 
                this.Top = downsize.Top + y;
            }
            else if (SizeMode == SizeWER)
            {
                int x = pt.X - downpoint.X;
                int y = pt.Y - downpoint.Y;
                this.Width = downsize.Width - x; 
                this.Left = downsize.Left + x;  
            }
            else if (SizeMode == SizeNESWL)
            {
                int x = pt.X - downpoint.X;
                int y = pt.Y - downpoint.Y;
                this.Width = downsize.Width - x;
                this.Height = downsize.Height + y;
                this.Left = downsize.Left + x;  
            }
            else
            {
                Rectangle rect = this.RectSizeWEL;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeWE;
                    return;
                } rect = this.RectSizeWER;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeWE;
                    return;
                } 
                rect = this.RectSizeNSL;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeNS;
                    return;
                }
                rect = this.RectSizeNSR;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeNS;
                    return;
                }

                rect = this.RectSizeNWSEL;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeNWSE;
                    return;
                }
                rect = this.RectSizeNWSER;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeNWSE;
                    return;
                }

                rect = this.RectSizeNESWL;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeNESW;
                    return;
                }
                rect = this.RectSizeNESWR;
                if (rect.Contains(e.Location))
                {
                    this.Cursor = Cursors.SizeNESW;
                    return;
                }

                this.Cursor = Cursors.Default;
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(713, 512);
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
         
    }
}
