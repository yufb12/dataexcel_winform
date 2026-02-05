
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


namespace Feng.Excel.IO
{
    [Serializable]
    public class BinaryReader : Feng.IO.BufferReader 
    {

        public BinaryReader(Stream output)
            : base(output)
        { 
        }

        public BinaryReader(byte[] data)
            : base(new MemoryStream(data))
        { 
        }
 
        public virtual LineCap ReadLineCap()
        {
            int hre = this.ReadInt32();

            return (LineCap)hre;
        }

        public virtual DashStyle ReadDashStyle()
        {
            int hre = this.ReadInt32();
            return (DashStyle)hre;
        }

        public virtual DashCap ReadDashCap()
        {
            int hre = this.ReadInt32();
            return (DashCap)hre;
        }
 
        public virtual LineJoin ReadLineJoin()
        {
            int hre = this.ReadInt32();
            return (LineJoin)hre;
        }
         
        public virtual PenAlignment ReadPenAlignment()
        {
            int hre = this.ReadInt32();
            return (PenAlignment)hre;
        }


        public virtual StringFormat ReadStringFormat()
        {
            return this.ReadObject() as StringFormat;
        }

        public override string ReadString()
        {
            int le = this.ReadInt32();
            if (le == 0)
            {
                return string.Empty;
            }
            byte[] data = this.ReadBytes(le);

            return Feng.IO.BitConver.GetString(data);
        }
 
        public virtual Cursor ReadCursor()
        {
            return this.ReadObject() as Cursor;
        }
 
        //public virtual Cursor ReadCursor(ushort index, Cursor value)
        //{
        //    if (this.IsEnd())
        //    {
        //        return value;
        //    }
        //    ushort i = this.ReadUInt16();
        //    if (i != index)
        //    {
        //        return value;
        //    }
        //    byte t = this.ReadByte();
        //    if (t == TypeEnum.Tbyte)
        //    {
        //        value = this.ReadObject() as Cursor;
        //    }
        //    else
        //    {
        //        this.ReadTypeEnum(t);
        //    }
        //    return value;
        //}
 
        public virtual object ReadObject()
        {

            try
            {

                int le = this.ReadInt32();
                if (le == 0)
                {
                    return null;
                }
                byte[] data = this.ReadBytes(le);

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bft = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return bft.Deserialize(ms);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except",ex); 
                return null;
            }

        }
         
    }
}
