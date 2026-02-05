using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Printing;
using Feng.Forms.Interface;
using Feng.Data;
using Feng.Args;
using Feng.Excel.Interfaces;
using Feng.Excel.App;
using Feng.Drawing;
using Feng.Forms.Views;

namespace Feng.Excel.View
{

    [Serializable]
    public class HorizontalRuler : DivView
    {

        public HorizontalRuler(DataExcel grid)
        {
            this._grid = grid;
            this._height = 20;
            grid.FiguresChanged += new Feng.EventHelper.FiguresChangedEventHandler(pc_FiguresChanged);
        }

        void pc_FiguresChanged(object sender, FiguresEventArgs e)
        { 
            Graphics g = this._grid.GetGraphics();
            if (g == null)
                return;

            _interval = Feng.Utils.ConvertHelper.ToInt32(g.DpiY / e.NewValue / 10);
            int i = Feng.Utils.ConvertHelper.ToInt32(g.DpiY / 2 / 10);
            LineHeight1 = i;
            LineHeight2 = i * 2;
            LineHeight3 = i * 3;
            LineHeight4 = i * 4;
        }
        private int _max = 100;
        [Browsable(true)]
        public virtual int Max
        {
            get { return _max; }
            set
            {
                _max = value;
            }
        }
        private int _min = 0;
        [Browsable(true)]
        public virtual int Min
        {
            get { return _min; }
            set
            {
                _min = value;
            }
        }
        [NonSerialized]
        private DataExcel _grid;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataExcel Grid
        {
            get
            {
                return _grid;
            }
            set
            {
                _grid = value;
            }
        }

        public virtual bool Visible
        {
            get { return this.Grid.ShowHorizontalRuler; }

            set { this.Grid.ShowHorizontalRuler = value; }
        }

        private int _interval = 4;

        //private float _interval = 3.78f;
#warning private float _interval = 3.78f;

        private int LineHeight1 = 4;
        private int LineHeight2 = 8;
        private int LineHeight4 = 16;
        private int LineHeight3 = 13;

        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this.Visible)
            {
                Draw(g);
            }
            return false;
        }
        [NonSerialized]
        private StringFormat _sf = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        private StringFormat Format
        {
            get
            {
                if (_sf == null)
                {
                    _sf = StringFormat.GenericDefault.Clone() as StringFormat;
                    _sf.Alignment = StringAlignment.Center;
                    _sf.Trimming = StringTrimming.EllipsisCharacter;
                }
                return _sf;
            }
        }

        private void DrawLine(Feng.Drawing.GraphicsObject g, Pen pen, Point pt1, Point pt2)
        {
            g.Graphics.DrawLine(pen, pt1, pt2);
        }
 
        private void Draw(Feng.Drawing.GraphicsObject g)
        {

            SolidBrush brush = SolidBrushCache.GetSolidBrush(this.Grid.BackColor);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.Rect);

            using (Pen pen = new Pen(this._BorderColor))
            {
                brush = SolidBrushCache.GetSolidBrush(this.ForeColor);

                Point pt1 = new Point(this.Grid.VerticalRuler.Right, this.Bottom);
                Point pt2 = new Point(this.Right, this.Bottom);

                g.Graphics.DrawLine(Pens.Black, pt1, pt2);
                pt1.Y = pt2.Y - LineHeight4;
                pt2.X = pt1.X;
                g.Graphics.DrawLine(Pens.Black, pt1, pt2);
                int left = this.Grid.LeftSideWidth;
                while (true)
                {
                    left -= _interval;
                    if (left < this.Grid.VerticalRuler.Right)
                    {
                        break;
                    }
                    pt1.Y = pt2.Y - LineHeight1;
                    pt1.X = left;
                    pt2.X = left;
                    DrawLine(g, pen, pt1, pt2);
                }
                int index = 0;
                left = this.Grid.LeftSideWidth - _interval;
                while (left < this.Width)
                {
                    left += _interval;
                    pt1.Y = pt2.Y - LineHeight3;
                    pt1.X = left;
                    pt2.X = left;
                    DrawLine(g, pen, pt1, pt2);
                    Rectangle rect = new Rectangle(pt1.X, 0, 9 * _interval, this.Height);
                    Feng.Drawing.GraphicsHelper.DrawString(g,index.ToString(), this.Font, brush, rect);
                    for (int i = 0; i < 4; i++)
                    {
                        left += _interval;
                        pt1.Y = pt2.Y - LineHeight1;
                        pt1.X = left;
                        pt2.X = left;
                        DrawLine(g, pen, pt1, pt2);
                    }
                    left += _interval;
                    pt1.Y = pt2.Y - LineHeight2;
                    pt1.X = left;
                    pt2.X = left;
                    DrawLine(g, pen, pt1, pt2);
                    for (int i = 0; i < 4; i++)
                    {
                        left += _interval;
                        pt1.Y = pt2.Y - LineHeight1;
                        pt1.X = left;
                        pt2.X = left;
                        DrawLine(g, pen, pt1, pt2);
                    }
                    index++;

                }
            }

        }

        #region IDataStruct 成员
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = string.Empty,
                    Name = string.Empty,
                };
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (Feng.Excel.IO.BinaryWriter bw = this.Grid.ClassFactory.CreateBinaryWriter(ms))
                    {
#if !DEBUG
//#error Data
#endif
                        bw.Write(1, this._max);
                        bw.Write(2, this._min);
                        bw.Write(3, this._interval);
                        bw.Write(4, this._font);
                        bw.Write(5, this._sf);

                    }
                    data.Data = ms.ToArray();
                }

                return data;
            }
        }

        #endregion

        #region IVersion 成员
        [Browsable(false)]
        public virtual string Version
        {
            get { return Feng.DataUtlis.SmallVersion.AssemblySecondVersion; }
        }


        #endregion

        #region IAssembly 成员
        [Browsable(false)]
        public virtual string DllName
        {
            get { return string.Empty; }
        }

        #endregion

        #region IDownLoadUrl 成员
        [Browsable(false)]
        public virtual string DownLoadUrl
        {
            get { return string.Empty; }
        }

        #endregion

        #region IBounds 成员

   
         
        [DefaultValue(20)]
        public override int Height
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
 
        public override int Width
        {
            get
            {
                return this.Grid.Width;
            }
            set
            {
            }
        }
 
        #endregion


        #region IBorderColor 成员
        private Color _BorderColor = Color.Black;

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
            }
        }

        #endregion
 
        public void ReadDataStruct(DataStruct data)
        {
        }
    }

}
