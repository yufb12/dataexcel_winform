using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Feng.Drawing
{
    public class GraphicsObject:IDisposable
    {
        public GraphicsObject()
        {
 
        }

        public GraphicsLayer CurrentLayer { get; set; }

        private Rectangle _workarea = Rectangle.Empty;
        public virtual Rectangle WorkArea
        {
            get
            {
                return this._workarea;
            }
            set
            {
                this._workarea = value;
            }
        }
        private Graphics _graphics = null;
        public virtual Graphics Graphics
        {
            get
            {
                return _graphics;
            }
            set
            {
                _graphics = value;
            }
        }
        private List<object> _Items = null;
        public virtual List<object> Items
        {
            get
            {
                if (_Items == null)
                {
                    _Items = new List<object>();
                }
                return _Items;
            }
            set
            {
                _Items = value;
            }
        }
        private Point _ClientPoint = Point.Empty;
        public virtual Point ClientPoint
        {
            get
            {
                return _ClientPoint;
            }
            set
            {
                _ClientPoint = value;
            }
        }
        private Rectangle _ClipRectangle = Rectangle.Empty;
        public virtual Rectangle ClipRectangle
        {
            get
            {
                return _ClipRectangle;
            }
            set
            {
                _ClipRectangle = value;
            }
        }



        private Point _CurrentPoint = Point.Empty;
        public virtual Point CurrentPoint
        {
            get
            {
                return _CurrentPoint;
            }
            set
            {
                _CurrentPoint = value;
            }
        }

        private Point _MousePoint = Point.Empty;
        public virtual Point MousePoint
        {
            get
            {
                return _MousePoint;
            }
            set
            {
                _MousePoint = value;
            }
        }
        private System.Windows.Forms.Keys _ModifierKeys = System.Windows.Forms.Keys.None;
        public virtual System.Windows.Forms.Keys ModifierKeys
        {
            get
            {
                return _ModifierKeys;
            }
            set
            {
                _ModifierKeys = value;
            }
        }
        private System.Windows.Forms.MouseButtons _MouseButtons = System.Windows.Forms.MouseButtons.None;
        public virtual System.Windows.Forms.MouseButtons MouseButtons
        {
            get
            {
                return _MouseButtons;
            }
            set
            {
                _MouseButtons = value;
            }
        }
        private System.Windows.Forms.Control _Control = null;
        public virtual System.Windows.Forms.Control Control
        {
            get
            {
                return _Control;
            }
            set
            {
                _Control = value;
            }
        }
        public virtual Font DefaultFont
        {
            get
            {
                return _Control.Font;
            }
        }
        public virtual void Dispose()
        {
            _graphics = null;
            _Control = null;
        }

        public static string DebugText { get; set; }
    }
    public class GraphicsObjectCache
    {
        private static Dictionary<string, GraphicsObject> dics = new Dictionary<string, GraphicsObject>();
 
        public static GraphicsObject Get(string key)
        {
            if (dics.ContainsKey(key))
            {
                return dics[key];
            }
            GraphicsObject p = new GraphicsObject();
            dics.Add(key, p);
            return p;
        }
    }
    public class GraphicsLayerCollection
    {
        private Dictionary<string, GraphicsLayer> dics = new Dictionary<string, GraphicsLayer>();

        public GraphicsLayer Get(string key)
        {
            if (dics.ContainsKey(key))
            {
                return dics[key];
            }
            GraphicsLayer p = new GraphicsLayer();
            dics.Add(key, p);
            return p;
        }
    }
}
