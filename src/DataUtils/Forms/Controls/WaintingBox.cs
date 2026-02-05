using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;

namespace Feng.Forms.Controls
{
      [ToolboxItem(false)]
    public class WaintingBox : System.Windows.Forms.Control 
    {
        public WaintingBox()
        {
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();
            InitializeComponent();
            if (!this.DesignMode)
            {
                this.timer1.Enabled = true; 
            }
        }
 
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            try
            { 
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                OnDraw(g);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnPaint(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseDown(e);
        }
 
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnSizeChanged(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        { 
            base.OnMouseWheel(e);
        }

        public float angle = 0;
        public void OnDraw(Graphics g)
        {
            g.FillEllipse(Brushes.Beige, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            g.FillEllipse(Brushes.Blue, new Rectangle(this.Width / 2 - 1, this.Height / 2 - 1, 2, 2));
            g.FillPie(Brushes.OrangeRed, new Rectangle(0, 0, this.Width - 1, this.Height - 1), angle, 30f);
            g.FillPie(Brushes.PaleGoldenrod, new Rectangle(this.Width / 2 - 12, this.Height / 2 - 12, 24, 24), angle, 30f);
            g.DrawEllipse(Pens.Aquamarine, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }
        
        private Color _color1 = Color.White;
        [Category(CategorySetting.PropertyDesign)]
        public Color Color1
        {
            get { return _color1; }
            set { _color1 = value; }
        }
        private Color _color2 = Color.Lavender;
        [Category(CategorySetting.PropertyDesign)]

        public Color Color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }
        private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
        [Category(CategorySetting.PropertyDesign)]
        public LinearGradientMode GradientMode
        {
            get { return _GradientMode; }
            set { _GradientMode = value; }
        }
        private bool _drawborder = true;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(true)]
        public bool DrawBorder
        {
            get { return _drawborder; }
            set { _drawborder = value; }
        }
        private int _borderwidth = 1;
        [DefaultValue(1)]
        [Category(CategorySetting.PropertyDesign)]
        public int BorderWidth
        {
            get { return _borderwidth; }
            set { _borderwidth = value; }
        }
        private Color _bordercolor = Color.DarkGray;
        private Timer timer1;
        private IContainer components;
    
        [Category(CategorySetting.PropertyDesign)]
        public Color BorderColor
        {
            get { return _bordercolor; }
            set { _bordercolor = value; }
        }
        private int _radius = 6;

        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(6)]
        public int Radius
        {

            get { return _radius; }
            set { _radius = value; }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 

            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.ResumeLayout(false);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            angle = angle % 360;
            if (angle < 180)
            {
                angle = angle + 2f;
            }
            else
            {
                angle = angle + 1f;
            }
            this.Invalidate();
        }
        
    }
 
}
