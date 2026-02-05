using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Feng.Drawing
{
    public class GraphicsLayer:IDisposable
    {
        public GraphicsLayer()
        {
 
        }
        public void Clear()
        {
            Region.MakeEmpty();
            Path.ClearMarkers();
        }
        private Region _region = new Region();
        public Region Region
        {
            get {
                return _region;
            }
        }

        private GraphicsPath _path = new GraphicsPath();
        public GraphicsPath Path {
            get {
                return _path;
            }
        }

        public virtual void Dispose()
        {
            _region = null;
            _path = null;
        }
    } 
}
