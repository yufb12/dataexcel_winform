using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using Feng.Forms.Interface;

namespace Feng.Forms.Controls
{ 
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class Pane : Component ,IDraw
    { 
        public Pane(Chart chart)
        {
            _chart = chart;
            _SeriesList = new SeriesCollection();
            _xaxis = new XAxis(this);
            _yaxis = new YAxis(this);
        }

        private SeriesCollection _SeriesList = null;
        public SeriesCollection SeriesList
        {
            get {
                return _SeriesList;
            }
        }
        private XAxis _xaxis = null;
        public XAxis XAxis
        {
            get {
                return _xaxis;
            }
        }
        private YAxis _yaxis = null;
        public YAxis YAxis
        {
            get {
                return _yaxis;
            }
        }


        protected virtual void OnMouseUp(MouseEventArgs e)
        {
 
        }
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
 
        }
        protected virtual void OnMouseLeave(EventArgs e)
        {
 
        }
        protected virtual void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
 
        }

        protected virtual void OnSizeChanged(EventArgs e)
        {
 
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {  
        }
        private Chart _chart = null;
        public Chart ToolBar
        {
            get {
                return _chart;
            } 
        }
        private int _left = 0;
        private int _top = 0;
        private int _width = 72;
        private int _height = 40;

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int Left {
            get {
                return _left;
            }
            set {
                _left = value;
            }
        }
 
        public int Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }

        public Rectangle Rect {
            get {
                Rectangle rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                return rect;
            }
        }

        public bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (SeriesList != null)
            {
                foreach (Series ser in SeriesList)
                {
                    ser.OnDraw(g);
                }
            }
            if (this.XAxis != null)
            {
                this.XAxis.OnDraw(this, g);
            }
            if (this.YAxis != null)
            {
                this.YAxis.OnDraw(this, g);
            }
            return false;
        }
    }
 
}
