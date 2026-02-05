using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Feng.Forms.Interface;
using Feng.Excel.Interfaces;
using Feng.Data;

namespace Feng.Excel.Styles
{
    [Serializable]
    public class CellBorderStyle : ICloneable, IDataStruct, IReadDataStruct
    {
        public CellBorderStyle()
        {
        }
 
        private LineStyle _leftlinestyle = null;
        public LineStyle LeftLineStyle
        {
            get
            { 
                if (_leftlinestyle == null)
                {
                    _leftlinestyle = new LineStyle();
                }
                return _leftlinestyle;
            }
            set
            {
                _leftlinestyle = value;
            }
        }
        private LineStyle _toplinestyle = null;
        public LineStyle TopLineStyle
        {
            get
            { 
                if (_toplinestyle==null )
                {
                    _toplinestyle = new LineStyle();
                }
                return _toplinestyle;
            }
            set
            {
                _toplinestyle = value;
            }
        }
        private LineStyle _rightlinestyle = null;
        public LineStyle RightLineStyle
        {
            get
            { 
                if (_rightlinestyle == null)
                {
                    _rightlinestyle = new LineStyle();
                }
                return _rightlinestyle;
            }
            set
            {
                _rightlinestyle = value;
            }
        }
        private LineStyle _boomlinestyle = null;
        public LineStyle BottomLineStyle
        {
            get
            {
               
                if (_boomlinestyle == null)
                {
                    _boomlinestyle = new LineStyle();
                }
                return _boomlinestyle;
            }
            set
            {
                _boomlinestyle = value;
            }
        }
 

        private LineStyle _LeftTopToRightBoomLineStyle;
        public LineStyle LeftTopToRightBottomLineStyle
        {
            get
            { 
                if (_LeftTopToRightBoomLineStyle == null)
                {
                    _LeftTopToRightBoomLineStyle = new LineStyle();
                }
                return _LeftTopToRightBoomLineStyle;
            }
            set
            {
                _LeftTopToRightBoomLineStyle = value;
            }
        }
        private LineStyle _LeftBoomToRightTopLineStyle;
        public LineStyle LeftBottomToRightTopLineStyle
        {
            get
            { 
                if (_LeftBoomToRightTopLineStyle == null)
                {
                    _LeftBoomToRightTopLineStyle = new LineStyle();
                }
                return _LeftBoomToRightTopLineStyle;
            }
            set
            {
                _LeftBoomToRightTopLineStyle = value;
            }
        }
        private static readonly CellBorderStyle Empty = new CellBorderStyle();
        //public byte[] GetData()
        //{
        //    byte[] data;
        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //    {
        //        using (Feng.Excel.IO.BinaryWriter stream = new Feng.Excel.IO.BinaryWriter(ms))
        //        {
        //            stream.Write(1,_leftlinestyle,Empty ._leftlinestyle);
        //            stream.Write(2,_toplinestyle, Empty._toplinestyle);
        //            stream.Write(3,_rightlinestyle, Empty._rightlinestyle);
        //            stream.Write(4,_boomlinestyle, Empty._boomlinestyle);
        //            stream.Write(5,_LeftTopToRightBoomLineStyle, Empty._LeftTopToRightBoomLineStyle);
        //            stream.Write(6, _LeftBoomToRightTopLineStyle, Empty._LeftBoomToRightTopLineStyle);  
        //        }
        //        data= ms.ToArray();
 
        
        //    }
        //    return data;
        //}

        //public void Read(byte[] data)
        //{
        //    using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data))
        //    {
        //        _leftlinestyle = stream.ReadLineStyle();
        //        _toplinestyle = stream.ReadLineStyle();
        //        _rightlinestyle = stream.ReadLineStyle();
        //        _boomlinestyle = stream.ReadLineStyle();
        //        _LeftTopToRightBoomLineStyle = stream.ReadLineStyle();
        //        _LeftBoomToRightTopLineStyle = stream.ReadLineStyle();
        //    }
        //}

        public object Clone()
        {
            CellBorderStyle cbs = new CellBorderStyle();
            cbs.ReadDataStruct(this.Data);
            return cbs;
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
                        stream.Write(1, _leftlinestyle, Empty._leftlinestyle);
                        stream.Write(2, _toplinestyle, Empty._toplinestyle);
                        stream.Write(3, _rightlinestyle, Empty._rightlinestyle);
                        stream.Write(4, _boomlinestyle, Empty._boomlinestyle);
                        stream.Write(5, _LeftTopToRightBoomLineStyle, Empty._LeftTopToRightBoomLineStyle);
                        stream.Write(6, _LeftBoomToRightTopLineStyle, Empty._LeftBoomToRightTopLineStyle);
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
                DataStruct dataStruct = null;
                _leftlinestyle = LineStyle.GetLineStyle(stream.ReadIndex(1, dataStruct));
                _toplinestyle = LineStyle.GetLineStyle(stream.ReadIndex(2, dataStruct));
                _rightlinestyle = LineStyle.GetLineStyle(stream.ReadIndex(3, dataStruct));
                _boomlinestyle = LineStyle.GetLineStyle(stream.ReadIndex(4, dataStruct));
                _LeftTopToRightBoomLineStyle = LineStyle.GetLineStyle(stream.ReadIndex(5, dataStruct));
                _LeftBoomToRightTopLineStyle = LineStyle.GetLineStyle(stream.ReadIndex(6, dataStruct));
            }
        }

        public static CellBorderStyle GetLineStyle(DataStruct data)
        {
            try
            {

                if (data == null)
                    return null;
                if (data.Data.Length < 1)
                    return null;
                CellBorderStyle lineStyle = new CellBorderStyle();
                lineStyle.ReadDataStruct(data);
                return lineStyle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
 
}
