
using Feng.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;


namespace Feng.IO
{
    [Serializable]
    public class BufferReader : BaseReader
    {

        public BufferReader(Stream output)
            : base(output)
        {

        }

        public BufferReader(byte[] data)
            : base(new MemoryStream(data))
        {

        }

        public BufferReader(byte[] data, int index, int count)
            : base(new MemoryStream(data, index, count))
        {

        }

        public virtual void Seek(long position)
        {
            this.BaseStream.Seek(position, SeekOrigin.Begin);
        }

        public virtual float[] Readfloats()
        {
            int len = base.ReadInt32();
            if (len == 0)
            {
                return null;
            }
            float[] data = new float[len];

            for (int i = 0; i < len; i++)
            {
                data[i] = this.ReadSingle();
            }

            return data;
        }

        public override bool ReadBoolean()
        {
            return base.ReadBoolean();
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

        public virtual string[] ReadStrings()
        {
            int len = this.ReadInt32();
            string[] values = new string[len];
            for (int i = 0; i < len; i++)
            {
                string str = this.ReadString();
                values[i] = str;
            }
            return values;
        }

        public virtual Color ReadColor()
        {
            byte e = base.ReadByte();
            if (e == 0)
            {
                return Color.Empty;
            }
            byte a = base.ReadByte();
            byte b = base.ReadByte();
            byte g = base.ReadByte();
            byte r = base.ReadByte();
            Color c = Color.FromArgb(a, r, g, b);
            if (c.A == System.Drawing.SystemColors.Control.A && c.B == System.Drawing.SystemColors.Control.B &&
                c.G == System.Drawing.SystemColors.Control.G && c.R == System.Drawing.SystemColors.Control.R)
            {
                return System.Drawing.SystemColors.Control;
            }
            return c;
        }

        //public virtual Image ReadImage()
        //{
        //    byte[] data = this.ReadBytes();
        //    return Feng.Drawing.ImageHelper.GetImageByCache(data);
        //}
        public virtual Bitmap ReadBitmap()
        {
            byte[] data = this.ReadBytes();
            return Feng.Drawing.ImageHelper.GetBitmap(data);
        }
        public virtual DateTime ReadDateTime()
        {
#if DEBUG22
            int Year = base.ReadInt32();
            int Month = base.ReadInt32();
            int Day = base.ReadInt32();
            int Hour = base.ReadInt32();
            int Minute = base.ReadInt32();
            int Second = base.ReadInt32();
            int Millisecond = base.ReadInt32();
#else
            int Year = base.ReadInt16();
            int Month = base.ReadByte();
            int Day = base.ReadByte();
            int Hour = base.ReadByte();
            int Minute = base.ReadByte();
            int Second = base.ReadByte();
            int Millisecond = Math.Abs(base.ReadInt16());
#endif

            return new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond);
        }

        public virtual byte[] ReadBuffer()
        {
            int len = base.ReadInt32();
            if (len == 0)
            {
                return new byte[0]; ;
            }
#warning return null
            byte[] data = new byte[len];
            data = base.ReadBytes(len);
            return data;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int Read()
        {
            return base.ReadInt32();
        }

        public override char ReadChar()
        {
            return base.ReadChar();
        }

        public virtual byte[] ReadBytes()
        {
            int len = base.ReadInt32();
            if (len <1)
            {
                return new byte[0];
            }
            byte[] data = new byte[len];
            data = base.ReadBytes(len);
            return data;
        }

        public virtual char[] ReadChars()
        {
            int len = base.ReadInt32();
            if (len == 0)
            {
                return null;
            }
            byte[] data = new byte[len];
            data = base.ReadBytes(len);

            return System.Text.Encoding.Unicode.GetChars(data);
        }

        public virtual object ReadSerializeValue()
        {
            int le = this.ReadInt32();
            if (le == 0)
            {
                return null;
            }
            byte[] data = this.ReadBytes(le);

            return Feng.IO.SerializableHelper.DeSerializeFromBinary(data);
        }

        public virtual object ReadSerializeValueIndex(ushort index, object value)
        {
            if (this.IsEnd())
            {
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TObject)
            {
                value = this.ReadSerializeValue();
            }
            else
            {
                this.ReadTypeEnum(t);
            }

            return value;
        }

        public virtual SqlParameter ReadSqlParameter()
        {
            bool hasvalue = this.ReadBoolean();
            if (!hasvalue)
            {
                return null;
            }
            SqlParameter sqm = null;

            DbType dbType = (DbType)this.ReadInt32();
            ParameterDirection direction = (ParameterDirection)this.ReadInt32();
            bool IsNullable = this.ReadBoolean();
            int LocaleId = this.ReadInt32();
            int Offset = this.ReadInt32();
            string parameterName = this.ReadString();
            byte precision = this.ReadByte();
            byte scale = this.ReadByte();
            int size = this.ReadInt32();
            string sourceColumn = this.ReadString();
            bool sourceColumnNullMapping = this.ReadBoolean();
            DataRowVersion sourceVersion = (DataRowVersion)this.ReadInt32();
            SqlDbType sqldbType = (SqlDbType)this.ReadInt32();
            string TypeName = this.ReadString();
            string UdtTypeName = this.ReadString();
            object value = this.ReadSerializeValue();
            string xmlSchemaCollectionDatabase = this.ReadString();
            string xmlSchemaCollectionName = this.ReadString();
            string xmlSchemaCollectionOwningSchema = this.ReadString();

            sqm = new SqlParameter(parameterName, sqldbType, size, direction, precision, scale, sourceColumn, sourceVersion,
                sourceColumnNullMapping, value, xmlSchemaCollectionDatabase, xmlSchemaCollectionOwningSchema, xmlSchemaCollectionName);
            return sqm;
        }

        public virtual ModleInfo ReadModleInfo()
        {
            bool hasvalue = this.ReadBoolean();
            if (!hasvalue)
            {
                return null;
            }
            ModleInfo sqm = new ModleInfo();
            sqm.Sql = this.ReadString();
            int len = this.ReadInt32();
            SqlParameter[] cmdParms = new SqlParameter[len];
            for (int i = 0; i < len; i++)
            {
                cmdParms[i] = this.ReadSqlParameter();
            }
            sqm.cmdParms = cmdParms;
            return sqm;
        }

        public virtual object ReadType(byte ttype)
        {
            switch (ttype)
            {
                case TypeEnum.TNull:
                    return null;
                case TypeEnum.Tbool:
                    return this.ReadBoolean();
                case TypeEnum.Tbyte:
                    return this.ReadByte();
                case TypeEnum.Tbytes:
                    return this.ReadBytes();
                case TypeEnum.Tchar:
                    return this.ReadChar();
                case TypeEnum.Tchars:
                    return this.ReadChars();
                case TypeEnum.TColor:
                    return this.ReadColor();
                case TypeEnum.TDateTime:
                    return this.ReadDateTime();
                case TypeEnum.Tdecimal:
                    return this.ReadDecimal();
                case TypeEnum.Tdouble:
                    return this.ReadDouble();
                case TypeEnum.Tfloat:
                    return this.ReadSingle();
                case TypeEnum.Tint:
                    return this.ReadInt32();
                case TypeEnum.Tlong:
                    return this.ReadInt64();
                case TypeEnum.Tsbyte:
                    return this.ReadSByte();
                case TypeEnum.Tshort:
                    return this.ReadInt16();
                case TypeEnum.Tstring:
                    return this.ReadString();
                case TypeEnum.Tuint:
                    return this.ReadUInt32();
                case TypeEnum.Tulong:
                    return this.ReadUInt64();
                case TypeEnum.Tushort:
                    return this.ReadUInt16();
                case TypeEnum.TObject:
                    return this.ReadSerializeValue();
                default:
                    break;
            }
            throw new Exception("ReadType Error No Fount Type！");
        }

        public virtual Font ReadFont()
        {
            byte[] buf = this.ReadBytes();
            if (buf.Length < 1)
            {
                return null;
            }
            using (Feng.IO.BufferReader reader = new BufferReader(buf))
            {
                string familyname = reader.ReadString();
                if (!string.IsNullOrEmpty(familyname))
                {
                    float size = reader.ReadSingle();
                    FontStyle style = (FontStyle)reader.ReadInt32();
                    return new Font(familyname, size, style);
                }
            }
            return null;
        }
        public virtual bool Read(bool value)
        {
            return this.ReadBoolean();
        }
        public virtual string Read(string value)
        {
            return this.ReadString();
        }
        public virtual byte Read(byte value)
        {
            return this.ReadByte();
        }
        public virtual void Read(Feng.IO.BufferReader bw, System.Data.DataColumn datacolumn)
        {
            datacolumn.AllowDBNull = bw.Read(datacolumn.AllowDBNull);
            datacolumn.Caption = bw.Read(datacolumn.Caption);
            datacolumn.ColumnName = bw.Read(datacolumn.ColumnName);
            byte datatype = bw.Read(TypeEnum.TNull);
            datacolumn.DataType = TypeEnum.GetType(datatype);
            bw.ReadBytes();
        }
        //public virtual System.Data.DataTable ReadDataTable()
        //{
        //    byte[] data = this.ReadBytes();
        //    if (data.Length < 1)
        //    {
        //        return null;
        //    }
        //    using (Feng.IO.BufferReader reader = new BufferReader(data))
        //    {
        //        System.Data.DataTable table = reader.ReadSerializeValue() as System.Data.DataTable;
        //        int count = reader.ReadInt32();
        //        for (int i = 0; i < count; i++)
        //        {
        //            System.Data.DataRow row = table.NewRow();
        //            for (int j = 0; j < table.Columns.Count; j++)
        //            {
        //                byte dbtype = reader.ReadByte();
        //                if (dbtype == TypeEnum.TNull)
        //                {
        //                    row[j] = DBNull.Value;
        //                }
        //                else
        //                {
        //                    row[j] = reader.ReadType(dbtype);
        //                }
        //            }
        //            table.Rows.Add(row);
        //        }
        //        return table;
        //    }
        //}

        public virtual System.Data.DataTable ReadDataTable()
        {
            byte[] data = this.ReadBytes();
            if (data.Length < 1)
            {
                return null;
            }
            using (Feng.IO.BufferReader reader = new BufferReader(data))
            {
                DataTable table = new DataTable();
                reader.ReadBytes ();
                int count= reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    DataColumn dataColumn = new DataColumn();
                    Read(reader, dataColumn);
                    table.Columns.Add(dataColumn);
                }
                reader.ReadBytes();
                count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    System.Data.DataRow row = table.NewRow();
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        byte dbtype = reader.ReadByte();
                        if (dbtype == TypeEnum.TNull)
                        {
                            row[j] = DBNull.Value;
                        }
                        else
                        {
                            row[j] = reader.ReadType(dbtype);
                        }
                    }
                    table.Rows.Add(row);
                }
                return table;
            }
        }
        public virtual DataValue ReadDataValue()
        {
            byte[] data = this.ReadBytes();
            if (data.Length < 1)
            {
                return null;
            }
            using (Feng.IO.BufferReader reader = new BufferReader(data))
            {
                string name = reader.ReadString();
                byte type = reader.ReadByte();
                object value = reader.ReadType(type);
                return new DataValue()
                {
                    Name = name,
                    Value = value,
                    Type = type
                };
            }
        }

        public virtual System.Data.DataSet ReadDataSet()
        {
            byte[] data = this.ReadBytes();
            if (data.Length < 1)
            {
                return null;
            }
            using (Feng.IO.BufferReader reader = new BufferReader(data))
            {
                System.Data.DataSet dataset = reader.ReadSerializeValue() as System.Data.DataSet;
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    DataTable table = reader.ReadDataTable();
                    if (table != null)
                    {
                        dataset.Tables[i].Merge(table);
                    }
                }
                return dataset;
            }
        }
        private Dictionary<ushort, object> dics = null;
        public virtual object ReadTypeEnum(byte t)
        {
            object obj = null;
            switch (t)
            {
                case TypeEnum.TNullablebool:
                    obj = ReadBoolean();
                    break;
                case TypeEnum.TNullablebyte:
                    obj = ReadByte();
                    break;
                case TypeEnum.TNullablechar:
                    obj = ReadChar();
                    break;
                case TypeEnum.TNullableColor:
                    obj = ReadColor();
                    break;
                case TypeEnum.TNullableDateTime:
                    obj = ReadDateTime();
                    break;
                case TypeEnum.TNullabledecimal:
                    obj = ReadDecimal();
                    break;
                case TypeEnum.TNullabledouble:
                    obj = ReadDouble();
                    break;
                case TypeEnum.TNullablefloat:
                    obj = ReadSingle();
                    break;
                case TypeEnum.TNullableint:
                    obj = ReadInt32();
                    break;
                case TypeEnum.TNullablelong:
                    obj = ReadInt64();
                    break;
                case TypeEnum.TNullablesbyte:
                    obj = ReadSByte();
                    break;
                case TypeEnum.TNullableshort:
                    obj = ReadInt16();
                    break;
                case TypeEnum.TNullableuint:
                    obj = ReadUInt32();
                    break;
                case TypeEnum.TNullableulong:
                    obj = ReadUInt64();
                    break;
                case TypeEnum.TNullableushort:
                    obj = ReadShort();
                    break;
                case TypeEnum.Tbool:
                    obj = ReadBoolean();
                    break;
                case TypeEnum.Tbyte:
                    obj = ReadByte();
                    break;
                case TypeEnum.Tbytes:
                    obj = ReadBytes();
                    break;
                case TypeEnum.Tchar:
                    obj = ReadChar();
                    break;
                case TypeEnum.Tchars:
                    obj = ReadChars();
                    break;
                case TypeEnum.TColor:
                    obj = ReadColor();
                    break;
                case TypeEnum.TDateTime:
                    obj = ReadDateTime();
                    break;
                case TypeEnum.Tdecimal:
                    obj = ReadDecimal();
                    break;
                case TypeEnum.Tdouble:
                    obj = ReadDouble();
                    break;
                case TypeEnum.Tfloat:
                    obj = ReadSingle();
                    break;
                case TypeEnum.Tint:
                    obj = ReadInt32();
                    break;
                case TypeEnum.Tlong:
                    obj = ReadInt64();
                    break;
                case TypeEnum.TNull:
                    break;
                case TypeEnum.TObject:
                    obj = ReadSerializeValue();
                    break;
                case TypeEnum.Tsbyte:
                    obj = ReadSByte();
                    break;
                case TypeEnum.Tshort:
                    obj = ReadInt16();
                    break;
                case TypeEnum.Tstring:
                    obj = ReadString();
                    break;
                case TypeEnum.Tuint:
                    obj = ReadUInt32();
                    break;
                case TypeEnum.Tulong:
                    obj = ReadUInt64();
                    break;
                case TypeEnum.Tushort:
                    obj = ReadUInt16();
                    break;
                case TypeEnum.TImage:
                    obj = ReadBitmap();
                    break;
                default:
                    break;
            }
            return obj;
        }
        public bool IsEnd()
        {
            bool result = false;
            result = this.BaseStream.Position >= this.BaseStream.Length;
            return result;
        }
        public virtual void ReadCache()
        {
            while (!this.IsEnd())
            {
                ushort i = this.ReadUInt16();

                object res = ReadIndexBuffer(0, i);
            }
        }
        public virtual object ReadIndexBuffer(ushort index, ushort errorindex)
        {
            byte t = this.ReadByte();
            object value = this.ReadTypeEnum(t);
            if (dics == null)
            {
                dics = new Dictionary<ushort, object>();
            }
            if (!dics.ContainsKey(errorindex))
            {
                dics.Add(errorindex, value);
            }
            if (dics.ContainsKey(index))
            {
                return dics[index];
            }
            return null;
        }
        public virtual object ReadIndexBuffer(ushort index)
        {
            if (dics != null)
            {
                if (dics.ContainsKey(index))
                {
                    return dics[index];
                }
            }
            return null;
        }
        public virtual bool ReadIndex(ushort index, bool value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is bool)
                {
                    return (bool)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is bool)
                {
                    return (bool)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbool)
            {
                value = this.ReadBoolean();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
 
        public virtual byte ReadIndex(ushort index, byte value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is byte)
                {
                    return (byte)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is byte)
                {
                    return (byte)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbyte)
            {
                value = this.ReadByte();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual byte[] ReadIndex(ushort index, byte[] value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is byte[])
                {
                    return (byte[])res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is byte[])
                {
                    return (byte[])res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                value = this.ReadBytes();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual char ReadIndex(ushort index, char value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is char)
                {
                    return (char)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is char)
                {
                    return (char)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tchar)
            {
                value = this.ReadChar();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual char[] ReadIndex(ushort index, char[] value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is char[])
                {
                    return (char[])res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is char[])
                {
                    return (char[])res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tchars)
            {
                value = this.ReadChars();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }

        //public virtual string[] ReadIndex(ushort index, string[] value)
        //{
        //    if (this.IsEnd())
        //    {
        //        object res = ReadIndexBuffer(index);
        //        if (res is string[])
        //        {
        //            return (string[])res;
        //        }
        //        return value;
        //    }
        //    ushort i = this.ReadUInt16();
        //    if (i != index)
        //    {
        //        object res = ReadIndexBuffer(index, i);
        //        if (res is string[])
        //        {
        //            return (string[])res;
        //        }
        //        return value;
        //    }
        //    byte t = this.ReadByte();
        //    if (t == TypeEnum.TObject)
        //    {
        //        byte[] buf = this.ReadBytes();
        //        if (buf.Length < 1)
        //        {
        //            return null;
        //        }
        //        using (Feng.IO.BufferReader reader = new BufferReader(buf))
        //        {
        //            int len = reader.ReadInt();
        //            string[] data = new string[len];
        //            for (int j = 0; j < len; j++)
        //            {
        //                string v = reader.ReadString();
        //                data[j] = v;
        //            }
        //            return data;
        //        }
        //    }
        //    else
        //    {
        //        this.ReadTypeEnum(t);
        //    }
        //    return value;
        //}

        public virtual DateTime ReadIndex(ushort index, DateTime value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is DateTime)
                {
                    return (DateTime)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is DateTime)
                {
                    return (DateTime)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TDateTime)
            {
                value = this.ReadDateTime();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual decimal ReadIndex(ushort index, decimal value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is decimal)
                {
                    return (decimal)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is decimal)
                {
                    return (decimal)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tdecimal)
            {
                value = this.ReadDecimal();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual double ReadIndex(ushort index, double value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is double)
                {
                    return (double)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is double)
                {
                    return (double)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tdouble)
            {
                value = this.ReadDouble();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual float ReadIndex(ushort index, float value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is float)
                {
                    return (float)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is float)
                {
                    return (float)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tfloat)
            {
                value = this.ReadSingle();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual int ReadIndex(ushort index, int value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is int)
                {
                    return (int)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is int)
                {
                    return (int)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tint)
            {
                value = this.ReadInt32();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual long ReadIndex(ushort index, long value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is long)
                {
                    return (long)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is long)
                {
                    return (long)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tlong)
            {
                value = this.ReadInt64();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual sbyte ReadIndex(ushort index, sbyte value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is sbyte)
                {
                    return (sbyte)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is sbyte)
                {
                    return (sbyte)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tsbyte)
            {
                value = this.ReadSByte();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual short ReadIndex(ushort index, short value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is short)
                {
                    return (short)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is short)
                {
                    return (short)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tshort)
            {
                value = this.ReadInt16();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual string ReadIndex(ushort index, string value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is string)
                {
                    return (string)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is string)
                {
                    return (string)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tstring)
            {
                value = this.ReadString();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual ushort ReadIndex(ushort index, ushort value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is ushort)
                {
                    return (ushort)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is ushort)
                {
                    return (ushort)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tuint)
            {
                value = this.ReadUInt16();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual uint ReadIndex(ushort index, uint value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is uint)
                {
                    return (uint)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is uint)
                {
                    return (uint)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tuint)
            {
                value = this.ReadUInt32();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual ulong ReadIndex(ushort index, ulong value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is ulong)
                {
                    return (ulong)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is ulong)
                {
                    return (ulong)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tulong)
            {
                value = this.ReadUInt64();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
 
        public virtual object ReadBaseValueIndex(ushort index, object value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                return res;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                return res;
            }
            byte t = this.ReadByte();
            return this.ReadTypeEnum(t);
        }
        public virtual Color ReadIndex(ushort index, Color value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is Color)
                {
                    return (Color)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is Color)
                {
                    return (Color)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TColor)
            {
                value = this.ReadColor();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual Object ReadObjectIndex(ushort index, Object value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is Object)
                {
                    return (Object)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is Object)
                {
                    return (Object)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TObject)
            {
                value = this.ReadSerializeValue();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual Image ReadIndex(ushort index, Image value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is Image)
                {
                    return (Image)res;
                }
                if (res is byte[])
                {
                    return Feng.Drawing.ImageHelper.GetImageByCache(res as byte[]);
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is Image)
                {
                    return (Image)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TImage)
            {
                value = this.ReadBitmap();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual Bitmap ReadIndex(ushort index, Bitmap value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is Bitmap)
                {
                    return (Bitmap)res;
                }
                if (res is byte[])
                {
                    return Feng.Drawing.ImageHelper.GetBitmap(res as byte[]);
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is Bitmap)
                {
                    return (Bitmap)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TImage)
            {
                value = this.ReadBitmap();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual Cursor ReadIndex(ushort index, Cursor value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is Cursor)
                {
                    return (Cursor)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is Cursor)
                {
                    return (Cursor)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TObject)
            {
                value = this.ReadSerializeValue() as Cursor;
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual DataTable ReadIndex(ushort index, DataTable value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is DataTable)
                {
                    return (DataTable)res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is DataTable)
                {
                    return (DataTable)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                value = this.ReadDataTable();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual Font ReadIndex(ushort index, Font value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is Font)
                {
                    return (Font)res;
                }
                if (res is byte[])
                {
                    using (Feng.IO.BufferReader reader = new BufferReader(res as byte[]))
                    {
                        string familyname = reader.ReadString();
                        if (!string.IsNullOrEmpty(familyname))
                        {
                            float size = reader.ReadSingle();
                            FontStyle style = (FontStyle)reader.ReadInt32();
                            return new Font(familyname, size, style);
                        }
                    }
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is Font)
                {
                    return (Font)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                value = this.ReadFont();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }

        public virtual Margins ReadIndex(ushort index, Margins value)
        {
            try
            { 
                if (this.IsEnd())
                {
                    object res = ReadIndexBuffer(index);
                    if (res is Margins)
                    {
                        return (Margins)res;
                    }
                    if (res is byte[])
                    {
                        byte[] buf = res as byte[];
                        if (buf.Length < 1)
                            return null;
                        using (Feng.IO.BufferReader reader = new BufferReader(buf))
                        {

                            Margins margins = new Margins();
                            margins.Bottom = reader.ReadInt();
                            margins.Left = reader.ReadInt();
                            margins.Right = reader.ReadInt();
                            margins.Top = reader.ReadInt();
                            return margins;
                        }
                    }
                    return value;
                }
                ushort i = this.ReadUInt16();
                if (i != index)
                {
                    object res = ReadIndexBuffer(index, i);
                    if (res is Margins)
                    {
                        return (Margins)res;
                    }
                    return value;
                }
                byte t = this.ReadByte();
                if (t == TypeEnum.Tbytes)
                {
                    value = this.ReadMargins();
                }
                else
                {
                    this.ReadTypeEnum(t);
                }
                return value;

            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);

            }
            return null;
        }

        public virtual Margins ReadMargins()
        {
            Margins margins = null;
            byte[] buf = this.ReadBytes();
            if (buf.Length < 1)
            {
                return null;
            }
            using (Feng.IO.BufferReader reader = new BufferReader(buf))
            {
                margins = new Margins();
                margins.Bottom= reader.ReadInt();
                margins.Left = reader.ReadInt();
                margins.Right = reader.ReadInt();
                margins.Top = reader.ReadInt(); 
            }
            return margins;
        }
        public virtual System.Windows.Forms.Padding ReadIndex(ushort index, System.Windows.Forms.Padding value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is System.Windows.Forms.Padding)
                {
                    return (System.Windows.Forms.Padding)res;
                }
                if (res is byte[])
                {
                    byte[] data = res as byte[];
                    if (data.Length < 1)
                        return System.Windows.Forms.Padding.Empty;
                    value = ReadPadding(data);
                    return value;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                byte[] data = this.ReadBytes();
                if (data.Length < 1)
                    return System.Windows.Forms.Padding.Empty;
                value = ReadPadding(data);
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }

        public virtual Feng.Forms.Base.LockVersion ReadIndex(ushort index, Feng.Forms.Base.LockVersion value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is Feng.Forms.Base.LockVersion)
                {
                    return (Feng.Forms.Base.LockVersion)res;
                }
                if (res is byte[])
                {
                    byte[] data = res as byte[];
                    if (data.Length < 1)
                        return null;
                    value = ReadLockVersion(data);
                    return value;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                byte[] data = this.ReadBytes();
                if (data.Length < 1)
                    return null;
                value = ReadLockVersion(data);
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }

        public virtual Feng.Forms.Base.LockVersion ReadLockVersion(byte[] data)
        {
            if (data.Length < 1)
                return null;
            Feng.Forms.Base.LockVersion value = new Feng.Forms.Base.LockVersion();
            value.ReadData(data);
            return value;
        }
        public virtual System.Windows.Forms.Padding ReadPadding(byte[] data)
        {
            if (data.Length < 1)
                return Padding.Empty;
            System.Windows.Forms.Padding value = Padding.Empty;
            using (Feng.IO.BufferReader stream = new BufferReader(data))
            {
                value.Left = stream.ReadInt32();
                value.Right = stream.ReadInt32();
                value.Top = stream.ReadInt32();
                value.Bottom = stream.ReadInt32();
            }
            return value;
        }

        public virtual float[] ReadIndex(ushort index, float[] value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is float[])
                {
                    return (float[])res;
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is float[])
                {
                    return (float[])res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                value = this.Readfloats();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual byte[] ReadIndexFile(ushort index, ref string file)
        {
            if (this.IsEnd())
            {
                return null;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index, i);
                if (res is byte[])
                {
                    return (byte[])res;
                }
                return null;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TFile)
            {
                return ReadFile(ref file);
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return null;
        }
        public virtual DataStructCollection ReadIndex(ushort index, DataStructCollection value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is DataStructCollection)
                {
                    return (DataStructCollection)res;
                }

                if (res is byte[])
                {
                    return BinaryReaderTool.ReadDataStructs(res as byte[]);
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index);
                if (res is DataStructCollection)
                {
                    return (DataStructCollection)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                return this.ReadDataStructs();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual DataStruct ReadIndex(ushort index, DataStruct value)
        {
            if (this.IsEnd())
            {
                object res = ReadIndexBuffer(index);
                if (res is DataStruct)
                {
                    return (DataStruct)res;
                }
                else if (res is byte [])
                {
                    return BinaryReaderTool.ReadDataStruct(res as byte[]);
                }
                return value;
            }
            ushort i = this.ReadUInt16();
            if (i != index)
            {
                object res = ReadIndexBuffer(index);
                if (res is DataStruct)
                {
                    return (DataStruct)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                return this.ReadDataStruct();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual DataStruct ReadDataStruct()
        {
            byte[] buf = this.ReadBytes();
            if (buf.Length < 1)
            {
                return null;
            }
            DataStruct data = new DataStruct();
            using (Feng.IO.BufferReader reader = new BufferReader(buf))
            {
                data.Name = reader.ReadString();
                data.Version = reader.ReadString();
                data.FullName = reader.ReadString();
                data.AessemlyDownLoadUrl = reader.ReadString();
                data.DllName = reader.ReadString();
                data.Data = reader.ReadBytes();
            }
            return data;
        }
        public virtual DataStruct ReadDataStruct(string name)
        {
            byte[] buf = this.ReadBytes();
            if (buf.Length < 1)
            {
                return null;
            }
            DataStruct data = new DataStruct();
            using (Feng.IO.BufferReader reader = new BufferReader(buf))
            {
                data.Name = reader.ReadString();
                data.Version = reader.ReadString();
                data.FullName = reader.ReadString();
                data.AessemlyDownLoadUrl = reader.ReadString();
                data.DllName = reader.ReadString();
                if (data.FullName == name)
                {
                    data.Data = reader.ReadBytes();
                }
                else
                {
                    int len = reader.ReadInt();
                    reader.BaseStream.Seek(len, SeekOrigin.Current);
                    data.ReadValue = false;
                }

            }
            return data;
        }
        public virtual byte[] ReadFile(ref string file)
        {
            file = ReadString();
            byte[] data = ReadBytes();
            return data;
        }
        public virtual object ReadBaseValue()
        {
            byte e = base.ReadByte();
            return ReadTypeEnum(e);
        }


        public virtual DataStructCollection ReadDataStructs()
        {
            byte[] buf = this.ReadBytes();
            if (buf.Length < 1)
            {
                return null;
            }
            DataStructCollection data = new DataStructCollection();
            using (Feng.IO.BufferReader reader = new BufferReader(buf))
            {
                int count = reader.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    DataStruct ds = reader.ReadDataStruct();
                    data.Add(ds);
                }
            }
            return data;
        }
        private Dictionary<string, object> dicKeys = null;
        public Dictionary<string, object> DicKeys
        {
            get
            {
                return dicKeys;
            }
        }
        public virtual void ReadAllKeys()
        {
            while (base.BaseStream.Position < base.BaseStream.Length)
            {
                string txt = this.ReadString();
                object res = ReadKeyBuffer(string.Empty, txt);
            }
        }
        public virtual object ReadKey(string key)
        {
            if (dicKeys != null)
            {
                if (dicKeys.ContainsKey(key))
                {
                    return dicKeys[key];
                }
            }
            return null;
        }
        public virtual object ReadKeyBuffer(string key, string keyError)
        {
            byte t = this.ReadByte();
            object value = this.ReadTypeEnum(t);
            if (dicKeys == null)
            {
                dicKeys = new Dictionary<string, object>();
            }
            dicKeys.Add(keyError, value);
            if (dicKeys.ContainsKey(key))
            {
                return dicKeys[key];
            }
            return null;
        }
        public virtual char[] ReadKey(string key, char[] value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is char[])
                {
                    return (char[])res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is char[])
                {
                    return (char[])res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tchars)
            {
                value = this.ReadChars();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual byte[] ReadKey(string key, byte[] value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is byte[])
                {
                    return (byte[])res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is byte[])
                {
                    return (byte[])res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbytes)
            {
                value = this.ReadBytes();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual bool ReadKey(string key, bool value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is bool)
                {
                    return (bool)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is bool)
                {
                    return (bool)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbool)
            {
                value = this.ReadBoolean();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual byte ReadKey(string key, byte value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is byte)
                {
                    return (byte)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is byte)
                {
                    return (byte)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tbyte)
            {
                value = this.ReadByte();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual char ReadKey(string key, char value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is char)
                {
                    return (char)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is char)
                {
                    return (char)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tchar)
            {
                value = this.ReadChar();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual DateTime ReadKey(string key, DateTime value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is DateTime)
                {
                    return (DateTime)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is DateTime)
                {
                    return (DateTime)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TDateTime)
            {
                value = this.ReadDateTime();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual decimal ReadKey(string key, decimal value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is decimal)
                {
                    return (decimal)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is decimal)
                {
                    return (decimal)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tdecimal)
            {
                value = this.ReadDecimal();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual double ReadKey(string key, double value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is double)
                {
                    return (double)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is double)
                {
                    return (double)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tdouble)
            {
                value = this.ReadDouble();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual float ReadKey(string key, float value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is float)
                {
                    return (float)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is float)
                {
                    return (float)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tfloat)
            {
                value = this.ReadSingle();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual int ReadKey(string key, int value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is int)
                {
                    return (int)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is int)
                {
                    return (int)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tint)
            {
                value = this.ReadInt32();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual long ReadKey(string key, long value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is long)
                {
                    return (long)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is long)
                {
                    return (long)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tlong)
            {
                value = this.ReadInt64();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual sbyte ReadKey(string key, sbyte value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is sbyte)
                {
                    return (sbyte)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is sbyte)
                {
                    return (sbyte)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tsbyte)
            {
                value = this.ReadSByte();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual short ReadKey(string key, short value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is short)
                {
                    return (short)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is short)
                {
                    return (short)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tshort)
            {
                value = this.ReadInt16();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual string ReadKey(string key, string value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is string)
                {
                    return (string)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is string)
                {
                    return (string)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tstring)
            {
                value = this.ReadString();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual uint ReadKey(string key, uint value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is uint)
                {
                    return (uint)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is uint)
                {
                    return (uint)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tuint)
            {
                value = this.ReadUInt32();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual ulong ReadKey(string key, ulong value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is ulong)
                {
                    return (ulong)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is ulong)
                {
                    return (ulong)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tulong)
            {
                value = this.ReadUInt64();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual ushort ReadKey(string key, ushort value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is ushort)
                {
                    return (ushort)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is ushort)
                {
                    return (ushort)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.Tushort)
            {
                value = this.ReadUInt16();
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual int? ReadKeyNullAble(string key, int? value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is int)
                {
                    return (int)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is int)
                {
                    return (int)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TNullableint)
            {
                bool res = this.ReadBoolean();
                if (res)
                {
                    value = this.ReadInt32();
                }
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual DateTime? ReadKeyNullAble(string key, DateTime? value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is int)
                {
                    return (DateTime)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is int)
                {
                    return (DateTime)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TNullableint)
            {
                bool res = this.ReadBoolean();
                if (res)
                {
                    value = this.ReadDateTime();
                }
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual decimal? ReadKeyNullAble(string key, decimal? value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is decimal)
                {
                    return (decimal)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is decimal)
                {
                    return (decimal)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TNullabledecimal)
            {
                bool res = this.ReadBoolean();
                if (res)
                {
                    value = this.ReadDecimal();
                }
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
        public virtual long? ReadKeyNullAble(string key, long? value)
        {
            if (this.IsEnd())
            {
                object res = ReadKey(key);
                if (res is long)
                {
                    return (long)res;
                }
                return value;
            }
            string txt = this.ReadString();
            if (txt != key)
            {
                object res = ReadKeyBuffer(key, txt);
                if (res is long)
                {
                    return (long)res;
                }
                return value;
            }
            byte t = this.ReadByte();
            if (t == TypeEnum.TNullableint)
            {
                bool res = this.ReadBoolean();
                if (res)
                {
                    value = this.ReadInt64();
                }
            }
            else
            {
                this.ReadTypeEnum(t);
            }
            return value;
        }
    }
}
