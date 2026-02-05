using Feng.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public abstract class StyleBase
    {
        public Color Color { get; set; }  
        public virtual BaseView View { get; set; }
        public abstract void OnDraw(object sender, GraphicsObject g);
        public abstract Pen GetPen();
        public abstract Brush GetBrush();
    }

    public class StylePen : StyleBase
    {
        public StylePen()
        {
            this.Color = Color.Black;
            pen = new Pen(this.Color);
            this.StartCap = pen.StartCap;
            this.EndCap = pen.EndCap;
            this.DashStyle = pen.DashStyle;
            this.DashCap = pen.DashCap;
            this.CompoundArray = pen.CompoundArray;
            this.LineJoin = pen.LineJoin;
            this.MiterLimit = pen.MiterLimit;
            this.Alignment = pen.Alignment; 
        }
        public virtual LineCap StartCap { get; set; }
        public virtual LineCap EndCap { get; set; }
        public virtual DashStyle DashStyle { get; set; }
        public virtual DashCap DashCap { get; set; }
        public virtual float[] CompoundArray { get; set; }
        public virtual LineJoin LineJoin { get; set; }
        public virtual float MiterLimit { get; set; }
        public virtual PenAlignment Alignment { get; set; }
        private float _width = 1f;
        public float Width
        {
            get { return _width; }
            set
            { _width = value; }
        }
        public override void OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.DrawLine(GetPen(), new Point(this.View.Left ,this.View .Bottom) , new Point(this.View.Right, this.View.Top));
        }
        public void OnDraw(object sender, GraphicsObject g,Point point1,Point point2)
        {
            g.Graphics.DrawLine(GetPen(), point1, point2);
        }
        public override Brush GetBrush()
        {
            throw new NotImplementedException();
        }
        private Pen pen = null;
        public override Pen GetPen()
        {
            pen = new Pen(this.Color, this.Width);
            pen.StartCap = this.StartCap;
            pen.EndCap = this.EndCap;
            pen.DashStyle = this.DashStyle;
            pen.DashCap = this.DashCap;
            if (this.CompoundArray.Length > 1)
            {
                pen.CompoundArray = this.CompoundArray;
            }
            pen.LineJoin = this.LineJoin;
            pen.MiterLimit = this.MiterLimit;
            pen.Alignment = this.Alignment;
            return pen;
        }
    }
 
    public class StyleRect: StyleBase
    {
        public StyleRect()
        {
            Color2 = Color.Empty;
            LinearGradientMode = LinearGradientMode.BackwardDiagonal;
        }
        public Color Color2 { get; set; }
        public LinearGradientMode LinearGradientMode { get; set; }
        public override void OnDraw(object sender, GraphicsObject g)
        {
            Feng.Drawing.GraphicsHelper.FillRect(g.Graphics, this.View.Rect, this.Color, this.Color2, LinearGradientMode); 
        }
        public override Brush GetBrush()
        {
            throw new NotImplementedException();
        }
        public override Pen GetPen()
        {
            throw new NotImplementedException();
        }
    }

    public class StyleLine : StyleBase
    {
        public bool ShowBorder { get; set; }
        public float Borderbwidth { get; set; }
        public override void OnDraw(object sender, GraphicsObject g)
        {
            throw new NotImplementedException();
        }
        public override Brush GetBrush()
        {
            throw new NotImplementedException();
        }
        public override Pen GetPen()
        {
            throw new NotImplementedException();
        }
    }

    public class StyleBorder : StyleBase
    { 
        public bool ShowBorder { get; set; }
        public float Borderbwidth { get; set; }
        public override void OnDraw(object sender, GraphicsObject g)
        {
            throw new NotImplementedException();
        }
        public override Brush GetBrush()
        {
            throw new NotImplementedException();
        }
        public override Pen GetPen()
        {
            throw new NotImplementedException();
        }
    }

    public class StyleText : StyleBase
    {
        public override Brush GetBrush()
        {
            throw new NotImplementedException();
        }
        public override Pen GetPen()
        {
            throw new NotImplementedException();
        }
        public override void OnDraw(object sender, GraphicsObject g)
        {
            throw new NotImplementedException();
        }
    }

    public class StyleImage : StyleBase
    {
        public override Brush GetBrush()
        {
            throw new NotImplementedException();
        }
        public override Pen GetPen()
        {
            throw new NotImplementedException();
        }
        public override void OnDraw(object sender, GraphicsObject g)
        {
            throw new NotImplementedException();
        }
    }
}
