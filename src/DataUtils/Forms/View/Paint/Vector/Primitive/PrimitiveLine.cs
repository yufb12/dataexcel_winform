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
    public partial class PrimitiveLine : PrimitiveBase
    {
        public PrimitiveLine()
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

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            Pen pen= PenCache.GetPen(this.ForeColor) ;
            g.Graphics.DrawLines(pen, Points.ToArray());
           
            return false;
        }
    }
}

