using Feng.Data;
using Feng.Drawing;
using System.Collections.Generic;
using System.Drawing;

namespace Feng.Forms.Views.Vector
{
    public partial class PrimitiveBezier : PrimitiveBase
    {
        public PrimitiveBezier()
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
            Color forecolor = GetForeColor();
            if (forecolor == Color.Empty)
            {
                Pen pen = PenCache.GetPen(GetForeColor());
                g.Graphics.DrawBeziers(pen, Points.ToArray());
            }
            return false;
        }
    }
}

