//using System;
//using System.Drawing;
//using System.Runtime.InteropServices;
//using System.Windows.Forms;

//namespace Feng.DataTool
//{
//    public partial class BaseForm : Form
//    {

//        [DllImport("User32.DLL")]
//        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
//        [DllImport("User32.DLL")]
//        private static extern bool ReleaseCapture();
//        private const uint WM_SYSCOMMAND = 0x0112;
//        private const int WM_SETREDRAW = 11;
//        private const int SC_MOVE = 61456;
//        private SkinPanel skinPanel1;
//        private const int HTCAPTION = 2;
//        public static void MoveWindow(IntPtr handle)
//        {
//            ReleaseCapture();
//            SendMessage(handle, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, 5);
//        }

//        public BaseForm()
//        {
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
//            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
//            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
//            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
//            this.SetStyle(ControlStyles.UserPaint, true);
//            this.SetStyle(ControlStyles.ResizeRedraw, true);
//            this.UpdateStyles();
//        }

//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }
//        protected override void OnPaint(PaintEventArgs e)
//        {

//            try
//            {
//                Rectangle rect = new Rectangle(1, 1, this.Width - 2, this.Height - 2);
//                e.Graphics.DrawRectangle(Pens.Gray, rect);
//                //e.Graphics.FillRectangle(Brushes.Red, this.RectSizeNESWL);
//                //e.Graphics.FillRectangle(Brushes.Gray, this.RectSizeNESWR);
//                //e.Graphics.FillRectangle(Brushes.AliceBlue, this.RectSizeNWSEL);
//                //e.Graphics.FillRectangle(Brushes.Yellow, this.RectSizeNWSER);
//                //e.Graphics.FillRectangle(Brushes.YellowGreen, this.RectSizeWEL);
//                //e.Graphics.FillRectangle(Brushes.Green, this.RectSizeWER);
//                //e.Graphics.FillRectangle(Brushes.GreenYellow, this.RectSizeNSL);
//                //e.Graphics.FillRectangle(Brushes.HotPink, this.RectSizeNSR);


//                //e.Graphics.FillRectangle(Brushes.Green, this.RectSizeNESWL);
//                //e.Graphics.FillRectangle(Brushes.Green, this.RectSizeNESWR);
//                //e.Graphics.FillRectangle(Brushes.Red, this.RectSizeNWSEL);
//                //e.Graphics.FillRectangle(Brushes.Green, this.RectSizeNWSER);
//                //e.Graphics.FillRectangle(Brushes.Red, this.RectSizeWEL);
//                //e.Graphics.FillRectangle(Brushes.Red, this.RectSizeWER);
//                //e.Graphics.FillRectangle(Brushes.Green, this.RectSizeNSL);
//                //e.Graphics.FillRectangle(Brushes.Green, this.RectSizeNSR);
//            }
//            catch (Exception ex)
//            {
//                //SysTools.ExceptionHelper.ShowException(ex);
//            }

//            base.OnPaint(e);
//        }

//        private void panelMenu_MouseDown(object sender, MouseEventArgs e)
//        {
//            MoveWindow(this.Handle);
//        }

//        private void panel1_MouseDown(object sender, MouseEventArgs e)
//        {
//            MoveWindow(this.Handle);
//        }
//        Point downpoint = Point.Empty;
//        Rectangle downsize = Rectangle.Empty;
//        public int SizeMode = -1;
//        public int SizeWEL = 1;
//        public int SizeWER = 2;
//        public int SizeNSL = 3;
//        public int SizeNSR = 5;
//        public int SizeNWSEL = 6;
//        public int SizeNWSER = 7;
//        public int SizeNESWL = 8;
//        public int SizeNESWR = 9;
//        public int PaddingRect = 2;
//        public int MarginRect = 3;
//        public Rectangle RectSizeWEL
//        {
//            get
//            {
//                return new Rectangle(this.Width - PaddingRect, MarginRect, PaddingRect, this.Height - MarginRect - MarginRect);
//            }
//        }
//        public Rectangle RectSizeWER
//        {
//            get
//            {
//                return new Rectangle(0, MarginRect, PaddingRect, this.Height - MarginRect - MarginRect);
//            }
//        }
//        public Rectangle RectSizeNSL
//        {
//            get
//            {
//                return new Rectangle(MarginRect, 0, this.Width - MarginRect - MarginRect, PaddingRect);
//            }
//        }
//        public Rectangle RectSizeNSR
//        {
//            get
//            {
//                return new Rectangle(MarginRect, this.Height - PaddingRect, this.Width - MarginRect - MarginRect, PaddingRect);
//            }
//        }
//        public Rectangle RectSizeNWSEL
//        {
//            get
//            {
//                return new Rectangle(this.Width - PaddingRect, this.Height - PaddingRect, PaddingRect, PaddingRect);
//            }
//        }
//        public Rectangle RectSizeNWSER
//        {
//            get
//            {
//                return new Rectangle(0, 0, PaddingRect, PaddingRect);
//            }
//        }
//        public Rectangle RectSizeNESWL
//        {
//            get
//            {
//                return new Rectangle(0, this.Height - PaddingRect, PaddingRect, PaddingRect);
//            }
//        }
//        public Rectangle RectSizeNESWR
//        {
//            get
//            {
//                return new Rectangle(this.Width - PaddingRect, 0, PaddingRect, PaddingRect);
//            }
//        }
//        protected override void OnMouseDown(MouseEventArgs e)
//        {
//            base.OnMouseDown(e);
//            if (e.Button == System.Windows.Forms.MouseButtons.Left)
//            {
//                downpoint = System.Windows.Forms.Control.MousePosition;
//                downsize = this.Bounds;
//                Rectangle rect = this.RectSizeWEL;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeWEL;
//                    return;
//                }
//                rect = this.RectSizeWER;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeWER;
//                    return;
//                }


//                rect = this.RectSizeNSL;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeNSL;
//                    return;
//                }
//                rect = this.RectSizeNSR;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeNSR;
//                    return;
//                }

//                rect = this.RectSizeNWSEL;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeNWSEL;
//                    return;
//                }
//                rect = this.RectSizeNWSER;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeNWSER;
//                    return;
//                }

//                rect = this.RectSizeNESWL;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeNESWL;
//                    return;
//                }
//                rect = this.RectSizeNESWR;
//                if (rect.Contains(e.Location))
//                {
//                    SizeMode = SizeNESWR;
//                    return;
//                }
//                MoveWindow(this.Handle);
//            }

//        }
//        protected override void OnMouseUp(MouseEventArgs e)
//        {
//            SizeMode = -1;
//            this.Cursor = Cursors.Default;
//            base.OnMouseUp(e);
//        }

//        protected override void OnMouseMove(MouseEventArgs e)
//        {
//            Point pt = System.Windows.Forms.Control.MousePosition;
//            base.OnMouseMove(e);
//            if (SizeMode == SizeNWSEL)
//            {
//                int x = pt.X - downpoint.X;
//                int y = pt.Y - downpoint.Y;
//                this.Width = downsize.Width + x;
//                this.Height = downsize.Height + y;
//            }
//            else if (SizeMode == SizeWEL)
//            {
//                int x = pt.X - downpoint.X;
//                int y = downpoint.Y - downpoint.Y;
//                this.Width = downsize.Width + x;
//                this.Height = downsize.Height + y;
//            }
//            else if (SizeMode == SizeNSR)
//            {
//                int x = downpoint.X - downpoint.X;
//                int y = pt.Y - downpoint.Y;
//                this.Width = downsize.Width + x;
//                this.Height = downsize.Height + y;
//            }
//            else if (SizeMode == SizeNESWR)
//            {
//                int x = pt.X - downpoint.X;
//                int y = pt.Y - downpoint.Y;
//                this.Width = downsize.Width + x;
//                this.Height = downsize.Height - y;
//                this.Top = downsize.Top + y;
//            }
//            else if (SizeMode == SizeNSL)
//            {
//                int x = pt.X - downpoint.X;
//                int y = pt.Y - downpoint.Y;
//                this.Height = downsize.Height - y;
//                this.Top = downsize.Top + y;
//            }
//            else if (SizeMode == SizeNWSER)
//            {
//                int x = pt.X - downpoint.X;
//                int y = pt.Y - downpoint.Y;
//                this.Width = downsize.Width - x;
//                this.Height = downsize.Height - y;
//                this.Left = downsize.Left + x;
//                this.Top = downsize.Top + y;
//            }
//            else if (SizeMode == SizeWER)
//            {
//                int x = pt.X - downpoint.X;
//                int y = pt.Y - downpoint.Y;
//                this.Width = downsize.Width - x;
//                this.Left = downsize.Left + x;
//            }
//            else if (SizeMode == SizeNESWL)
//            {
//                int x = pt.X - downpoint.X;
//                int y = pt.Y - downpoint.Y;
//                this.Width = downsize.Width - x;
//                this.Height = downsize.Height + y;
//                this.Left = downsize.Left + x;
//            }
//            else
//            {
//                Rectangle rect = this.RectSizeWEL;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeWE;
//                    return;
//                }
//                rect = this.RectSizeWER;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeWE;
//                    return;
//                }
//                rect = this.RectSizeNSL;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeNS;
//                    return;
//                }
//                rect = this.RectSizeNSR;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeNS;
//                    return;
//                }

//                rect = this.RectSizeNWSEL;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeNWSE;
//                    return;
//                }
//                rect = this.RectSizeNWSER;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeNWSE;
//                    return;
//                }

//                rect = this.RectSizeNESWL;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeNESW;
//                    return;
//                }
//                rect = this.RectSizeNESWR;
//                if (rect.Contains(e.Location))
//                {
//                    this.Cursor = Cursors.SizeNESW;
//                    return;
//                }

//                this.Cursor = Cursors.Default;
//            }
//        }
//        protected override void OnMouseLeave(EventArgs e)
//        {
//            this.Cursor = Cursors.Default;
//            base.OnMouseLeave(e);
//        }

//        private void InitializeComponent()
//        {
//            this.SuspendLayout();
//            // 
//            // BaseForm
//            // 
//            this.ClientSize = new System.Drawing.Size(713, 512);
//            this.Name = "BaseForm";
//            this.ResumeLayout(false);

//        }

//    }
//}
