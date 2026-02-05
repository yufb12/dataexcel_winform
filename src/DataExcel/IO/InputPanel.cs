
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Feng.Data; 
using Feng.Excel.Styles;
using Feng.Forms.Interface;


namespace Feng.Excel
{
    [Serializable]
    public class InputPanel : IDraw ,IText,IBounds
    { 
        public InputPanel()
        { 

        }
 
        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            Feng.Drawing.GraphicsHelper.DrawString(g, this.Text, this.Font, Brushes.Black, this.Rect);
            return true;
        }
        public int SelectIndex { get; set; }

        StringBuilder sb = new StringBuilder();

        public void Add(string text)
        {
            sb.Insert(SelectIndex, text);
        }
        public void Del()
        {
            sb.Remove(SelectIndex, 1);
        }

        public Font Font { get; set; }

        private string _text = string.Empty;
        public virtual string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        private Rectangle _rect = new Rectangle();
        public virtual int Left
        {
            get
            {
                return _rect.Left;
            }
            set
            {
                _rect.X = value;
            }
        }

        public virtual int Height
        {
            get
            {
                return _rect.Height;
            }
            set
            {
                _rect.Height = value;
            }
        }

        public virtual int Right
        {
            get { return _rect.Right; }
        }

        public virtual int Bottom
        {
            get { return _rect.Bottom; }
        }

        public virtual int Top
        {
            get
            {
                return _rect.Top;
            }
            set
            {
                _rect.Y= value;
            }
        }

        public virtual int Width
        {
            get
            {
                return _rect.Width;
            }
            set
            {
                _rect.Width = value;
            }
        }

        public virtual Rectangle Rect
        {
            get { return _rect; }
        }
    }
}
