using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace TianZiGeGenerator
{
    public class PageLayout
    {
        private float _pageWidth, _pageHeight;
        private float _marginLeft, _marginRight, _marginTop, _marginBottom;
        private float _infoBarHeight, _headerHeight;
        private int _columns, _rows;
        private float _gridStartX, _gridStartY;
        private float _pinyinHeight, _rowHeight;

        public float PageWidth { get { return _pageWidth; } set { _pageWidth = value; } }
        public float PageHeight { get { return _pageHeight; } set { _pageHeight = value; } }
        public float MarginLeft { get { return _marginLeft; } set { _marginLeft = value; } }
        public float MarginRight { get { return _marginRight; } set { _marginRight = value; } }
        public float MarginTop { get { return _marginTop; } set { _marginTop = value; } }
        public float MarginBottom { get { return _marginBottom; } set { _marginBottom = value; } }
        public float InfoBarHeight { get { return _infoBarHeight; } set { _infoBarHeight = value; } }
        public float HeaderHeight { get { return _headerHeight; } set { _headerHeight = value; } }
        public int Columns { get { return _columns; } set { _columns = value; } }
        public int Rows { get { return _rows; } set { _rows = value; } }
        public float GridStartX { get { return _gridStartX; } set { _gridStartX = value; } }
        public float GridStartY { get { return _gridStartY; } set { _gridStartY = value; } }
        public float PinyinHeight { get { return _pinyinHeight; } set { _pinyinHeight = value; } }
        public float RowHeight { get { return _rowHeight; } set { _rowHeight = value; } }
    } 
}
