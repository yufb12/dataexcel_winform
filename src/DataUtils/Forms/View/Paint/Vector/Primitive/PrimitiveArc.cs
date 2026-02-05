using Feng.Data;
using Feng.Drawing;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views.Vector
{
    public partial class PrimitiveArc : PrimitiveBase
    {
        public PrimitiveArc()
        {

        }

        public override DataStruct Data
        {
            get
            {
                return null;
            }
        }

        private List<Point> _points = null;
        public List<Point> Points
        {
            get {
                if (_points == null)
                    _points = new List<Point>();
                return _points;
            }
        }

        public virtual void AddPoint(Point pt)
        {
            Points.Add(pt);
        }

        public override void ReadDataStruct(DataStruct data)
        {

        }
        public virtual float AngleStart { get; set; }
        public virtual float AngleEnd { get; set; }
        public Color GetBackColor()
        {
            return this.BackColor;
        }
        public Color GetForeColor()
        {
            return this.ForeColor;
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            Color backcolor = GetBackColor(); 
            if(backcolor != Color.Empty)
            {
                SolidBrush brush = SolidBrushCache.GetSolidBrush(backcolor);
                g.Graphics.FillPie(brush, this.Rect, AngleStart, AngleEnd);
            }
            Color forecolor = GetForeColor();
            if (forecolor == Color.Empty)
            {
                Pen pen = PenCache.GetPen(GetForeColor());
                g.Graphics.DrawArc(pen, this.Rect, AngleStart, AngleEnd);
            }
            return false;
        }
    }
}

