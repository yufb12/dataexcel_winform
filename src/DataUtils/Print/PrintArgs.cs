using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace Feng.Print
{
    [Serializable]
    public class PrintArgs
    {
        public PrintArgs()
        {

        }
        private Feng.Drawing.GraphicsObject gob = null;
        public Feng.Drawing.GraphicsObject Graphic
        {
            get
            {
                if (gob == null)
                {
                    gob = new Drawing.GraphicsObject(); 
                }
                gob.Graphics = this.PrintPageEventArgs.Graphics;
                return gob;
            }
        }
        private PrintPageEventArgs _printpageargs = null;
        public PrintPageEventArgs PrintPageEventArgs
        {
            get { return _printpageargs; }
            set { _printpageargs = value; }
        }

        private Point _CurrentLocation = Point.Empty;
        public Point CurrentLocation
        {
            get { return _CurrentLocation; }
            set { _CurrentLocation = value; }
        }

        public Rectangle _clip = Rectangle.Empty;
        public Rectangle Clip
        {
            get
            {
                return _clip;
            }
            set
            {
                _clip = value;
            }
        }

        private int _CurrentColumnIndex = 0;
        public int BeginColumnIndex
        {
            get { return _CurrentColumnIndex; }
            set { _CurrentColumnIndex = value; }

        }
        private bool _HasMorePages = false;
        public virtual bool HasMorePages
        {
            get { return _HasMorePages; }
            set { _HasMorePages = value; }
        }
        private int _CurrentRowIndex = 0;
        public int BeginRowIndex
        {
            get { return _CurrentRowIndex; }
            set { _CurrentRowIndex = value; }
        }

        private int _minrowindex = 1;
        private int _mincolumnindex = 1;
        private int _maxrowindex = 1;
        private int _maxcolumnindex = 1;

        public int MaxRowIndex
        {
            get
            {
                return _maxrowindex;
            }
            set
            {
                _maxrowindex = value;
            }
        }
        public int MaxColumnIndex
        {
            get
            {
                return _maxcolumnindex;
            }
            set
            {
                _maxcolumnindex = value;
            }
        }
        public int MinColumnIndex
        {
            get
            {
                return _mincolumnindex;
            }
            set
            {
                _mincolumnindex = value;
            }
        }
        public int MinRowIndex
        {
            get
            {
                return _minrowindex;
            }
            set
            {
                _minrowindex = value;
            }
        }

        private List<object> _states = null;
        public List<object> States
        {
            get
            {
                if (_states == null)
                {
                    _states = new List<object>();
                }
                return _states;
            }
            set { _states = value; }
        }

        public event PrintEvent PrintEvent;
        public void BeginPrint(PrintArgs e)
        {
            if (PrintEvent != null)
            {
                PrintEvent(this, e);
            }
        }

        public PrintPageBase CurrentPage { get; set; }
        public int Total { get; set; }
        public int Index { get; set; }

    }
    public delegate void PrintEvent(object sender, PrintArgs e);
}
