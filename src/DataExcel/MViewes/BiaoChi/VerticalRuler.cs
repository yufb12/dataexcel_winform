using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Printing;
using Feng.Data;
using Feng.Args;
using Feng.Excel.Interfaces;
using Feng.Excel.App;
using Feng.Drawing;
using Feng.Forms.Views;

namespace Feng.Excel.View
{
    [Serializable]
    public class VerticalRuler : DivView
    {
        public VerticalRuler(DataExcel grid)
        {
            _width = 20;
            this._grid = grid;
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
        public int Max
        {
            get { return _max; }
            set
            {
                _max = value;
            }
        }
        private int _min = 0;
        [Browsable(true)]
        public int Min
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
        public DataExcel Grid
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
            get { return this.Grid.ShowVerticalRuler; }
            set { this.Grid.ShowVerticalRuler = value; }
        }
#if DEBUG
        public static void ConvertToChina(Margins mar)
        {
            Margins ms = PrinterUnitConvert.Convert(mar, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);

        }
#endif
  
        private int _interval = 4;

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
   
        public StringFormat Format
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
 
        public void Draw(Feng.Drawing.GraphicsObject g)
        {
            Brush brush = SolidBrushCache.GetSolidBrush(this.Grid.BackColor);
            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.Rect);
            
            using (Pen pen = new Pen(this._BorderColor))
            {
                brush = SolidBrushCache.GetSolidBrush(this._ForeColor);

                Point pt1 = new Point(this.Right, this.Grid.HorizontalRuler.Bottom);
                Point pt2 = new Point(this.Right, this.Bottom);

                DrawLine(g, pen, pt1, pt2);
                pt1.X = pt2.X - LineHeight3;
                pt2.Y = pt1.Y;
                DrawLine(g, pen, pt1, pt2);
                int left = this.Grid.TopSideHeight;
                while (true)
                {
                    left -= _interval;
                    if (left < this.Grid.HorizontalRuler.Bottom)
                    {
                        break;
                    }
                    pt1.X = pt2.X - LineHeight1;
                    pt1.Y = left;
                    pt2.Y = left;
                    DrawLine(g, pen, pt1, pt2);
                }
                int indeY = 0;
                left = this.Grid.TopSideHeight - _interval;
                while (left < this.Height)
                {
                    left += _interval;
                    pt1.X = pt2.X - LineHeight4;
                    pt1.Y = left;
                    pt2.Y = left;
                    DrawLine(g, pen, pt1, pt2);
                    Rectangle rect = new Rectangle(pt1.X - LineHeight1, pt1.Y, 9 * _interval, LineHeight4);
                    Feng.Drawing.GraphicsHelper.DrawString(g, indeY.ToString(), this.Font, brush, rect);
                    for (int i = 0; i < 4; i++)
                    {
                        left += _interval;
                        pt1.X = pt2.X - LineHeight1;
                        pt1.Y = left;
                        pt2.Y = left;
                        DrawLine(g, pen, pt1, pt2);
                    }
                    left += _interval;
                    pt1.X = pt2.X - LineHeight2;
                    pt1.Y = left;
                    pt2.Y = left;
                    DrawLine(g, pen, pt1, pt2);
                    for (int i = 0; i < 4; i++)
                    {
                        left += _interval;
                        pt1.X = pt2.X - LineHeight1;
                        pt1.Y = left;
                        pt2.Y = left;
                        DrawLine(g, pen, pt1, pt2);
                    }
                    indeY++;

                }
            }
        }
 
        #region IDataStruct 成员
        [Browsable(false)]
        public override DataStruct Data
        {
            get {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                   AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = string.Empty,
                   Name= string.Empty,
               };
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (Feng.Excel.IO.BinaryWriter bw = this.Grid.ClassFactory.CreateBinaryWriter(ms))
                    { 
#if !DEBUG
//#error Data
#endif
                        bw.Write(1,this._max);
                        bw.Write(2,this._min);
                        bw.Write(3,this._interval);
                        bw.Write(4,this._font);
                        bw.Write(5,this._sf);
 
                    }
                    data.Data = ms.ToArray();
                }

                return data;
            }
        }

        #endregion

        #region IVersion 成员
        [Browsable(false)]
        public string Version
        {
            get { return Feng.DataUtlis.SmallVersion.AssemblySecondVersion; }
        }
 

        #endregion

        #region IAssembly 成员

        public string DllName
        {
            get { return string.Empty; }
        }

        #endregion

        #region IDownLoadUrl 成员
        [Browsable(false)]
        public string DownLoadUrl
        {
            get { return string.Empty; }
        }

        #endregion

        #region IBounds 成员
         
  
 
        public override int Height
        {
            get
            {
                return this.Grid.Height;
            }
            set
            { 
            }
        }
 
  
        [DefaultValue(20)]
        public override int Width
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

        #region IForeColor 成员
        private Color _ForeColor = Color.Blue;
 
        public Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }

        #endregion


        public void ReadDataStruct(DataStruct data)
        { 
        }
    }
}
