using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using Feng.Forms.Interface;
using Feng.Data;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Styles
{
    [Serializable]
    public class LineStyle : Feng.Forms.Interface.IVisible, ICloneable, IDataStruct, IReadDataStruct
    {
        public LineStyle()
        {
        }
 
        private Color _color = Color.Black;

        public virtual Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public virtual LineCap StartCap { get; set; }
        public virtual LineCap EndCap { get; set; }
        public virtual DashStyle DashStyle { get; set; }
        public virtual DashCap DashCap { get; set; }
        public virtual float[] CompoundArray { get; set; }
        public virtual LineJoin LineJoin { get; set; }
        public virtual float MiterLimit { get; set; }
        public virtual PenAlignment Alignment { get; set; }
        private float _width = 1f;
        public float Width
        {
            get { return _width; }
            set
            { _width = value; }
        }
        private static readonly LineStyle Empty = new LineStyle();
        //public byte[] GetData()
        //{
        //    byte[] data;
        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //    { 
        //        using (Feng.Excel.IO.BinaryWriter stream = new Feng.Excel.IO.BinaryWriter(ms))
        //        {
        //            stream.Write(1,Color, Empty.Color);
        //            stream.Write(2,(int)StartCap, (int)Empty.StartCap);
        //            stream.Write(3,(int)EndCap, (int)Empty.EndCap);
        //            stream.Write(4,(int)DashStyle, (int)Empty.DashStyle);
        //            stream.Write(5,(int)DashCap, (int)Empty.DashCap);
        //            stream.Write(6, CompoundArray, Empty.CompoundArray);
        //            stream.Write(7,(int)LineJoin, (int)Empty.LineJoin);
        //            stream.Write(8,MiterLimit, Empty.MiterLimit);
        //            stream.Write(9,(int)Alignment, (int)Empty.Alignment);
        //            stream.Write(10,Width, Empty.Width);
        //            stream.Write(11, Visible, Empty.Visible);
        //        }
        //        data= ms.ToArray();
        //    }
        //    return data;
        //}

        //public void Read(byte[] data)
        //{

        //    using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data))
        //    {
        //        stream.ReadCache();
        //        Color = stream.ReadIndex(1, Color);
        //        StartCap = (LineCap)stream.ReadIndex(2, (int)StartCap);
        //        EndCap = (LineCap)stream.ReadIndex(3, (int)EndCap);
        //        DashStyle = (DashStyle)stream.ReadIndex(4, (int)DashStyle);
        //        DashCap = (DashCap)stream.ReadIndex(5, (int)DashCap);
        //        CompoundArray = stream.ReadIndex(6, CompoundArray);
        //        LineJoin =(LineJoin) stream.ReadIndex(7, (int)LineJoin);
        //        MiterLimit = stream.ReadIndex(8, MiterLimit);
        //        Alignment = (PenAlignment)stream.ReadIndex(9, (int)Alignment);
        //        Width = stream.ReadIndex(10, Width);
        //        Visible = stream.ReadIndex(11, Visible);
        //    } 
             
        //}

        #region ILineStyle 成员

        public Pen GetPen()
        {
            Pen pen = new Pen(Color);
            pen.StartCap = StartCap;
            pen.EndCap = StartCap;
            pen.DashStyle = DashStyle;
            pen.DashCap = DashCap;
            if (CompoundArray != null)
            {
                pen.CompoundArray = CompoundArray;
            }
            pen.LineJoin = LineJoin;
            pen.MiterLimit = MiterLimit;
            pen.Alignment = Alignment;
            pen.Width = Width;
            return pen;
            
        }

        #endregion
 
        #region ILineStyle 成员

        private int _LineCount = 1;
        public int LineCount
        {
            get
            {
                return _LineCount;
            }
            set
            {
                _LineCount = value;
            }
        }

        #endregion

        private bool _visible = false;
        public virtual bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
        public object Clone()
        {
            LineStyle ls = new LineStyle();
            ls.ReadDataStruct(this.Data);
            return ls;
        }

        [Browsable(false)]
        public DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = string.Empty,
                    Name = string.Empty,
                };
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (Feng.Excel.IO.BinaryWriter stream = new Feng.Excel.IO.BinaryWriter(ms))
                    {
                        stream.Write(1, Color, Empty.Color);
                        stream.Write(2, (int)StartCap, (int)Empty.StartCap);
                        stream.Write(3, (int)EndCap, (int)Empty.EndCap);
                        stream.Write(4, (int)DashStyle, (int)Empty.DashStyle);
                        stream.Write(5, (int)DashCap, (int)Empty.DashCap);
                        stream.Write(6, CompoundArray, Empty.CompoundArray);
                        stream.Write(7, (int)LineJoin, (int)Empty.LineJoin);
                        stream.Write(8, MiterLimit, Empty.MiterLimit);
                        stream.Write(9, (int)Alignment, (int)Empty.Alignment);
                        stream.Write(10, Width, Empty.Width);
                        stream.Write(11, Visible, Empty.Visible);
                    }
                    data.Data = ms.ToArray();
                }
 
                return data;
            }
        }
 
        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                stream.ReadCache();
                Color = stream.ReadIndex(1, Color);
                StartCap = (LineCap)stream.ReadIndex(2, (int)StartCap);
                EndCap = (LineCap)stream.ReadIndex(3, (int)EndCap);
                DashStyle = (DashStyle)stream.ReadIndex(4, (int)DashStyle);
                DashCap = (DashCap)stream.ReadIndex(5, (int)DashCap);
                CompoundArray = stream.ReadIndex(6, CompoundArray);
                LineJoin = (LineJoin)stream.ReadIndex(7, (int)LineJoin);
                MiterLimit = stream.ReadIndex(8, MiterLimit);
                Alignment = (PenAlignment)stream.ReadIndex(9, (int)Alignment);
                Width = stream.ReadIndex(10, Width);
                Visible = stream.ReadIndex(11, Visible);
            }
        }

        public static LineStyle GetLineStyle(DataStruct data)
        {
            if (data == null)
                return null;
            if (data.Data.Length < 1)
                return null;
            LineStyle lineStyle = new LineStyle();
            lineStyle.ReadDataStruct(data);
            return lineStyle;
        }
    }

}
