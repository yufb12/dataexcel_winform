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
    public partial class PrimitiveClosedCurve : PrimitiveBase
    {
        public PrimitiveClosedCurve()
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
                g.Graphics.FillClosedCurve(brush, Points.ToArray());
            }
            Color forecolor = GetForeColor();
            if (forecolor == Color.Empty)
            {
                Pen pen = PenCache.GetPen(GetForeColor());
                g.Graphics.DrawClosedCurve(pen, Points.ToArray());
            }
            return false;
        }
    }
}

