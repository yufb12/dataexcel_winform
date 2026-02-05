
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

using System.Data.SqlClient; 
using System.Collections.Generic;
using Feng.Data;
using Feng.Forms.Interface;
using Feng.Forms.Base;
using System.Drawing.Printing;

namespace Feng.IO
{
    [Serializable]
    public class BufferWriter : BaseWriter
    {
        public BufferWriter(Stream output)
            : base(output)
        {

        }
        public BufferWriter()
            : base(new MemoryStream())
        {

        }

        public BufferWriter(byte[] data)
            : base(new MemoryStream(data))
        {

        }

        public virtual void Write(Color value)
        {
            if (value == Color.Empty)
            {
                base.Write((byte)0);
                return;
            }
            else
            {
                base.Write((byte)1);
            }
            base.Write(value.A);
            base.Write(value.B);
            base.Write(value.G);
            base.Write(value.R);
        }

        public virtual void Write(Margins value)
        {
            if (value != null)
            {
                using (Feng.IO.BufferWriter bw = new BufferWriter())
                {
                    bw.Write(value.Bottom);
                    bw.Write(value.Left);
                    bw.Write(value.Right);
                    bw.Write(value.Top);
                    this.Write(bw.GetData());
                    bw.Close();
                }
            }
            else
            {
                this.Write(new byte[] { });
            }

        }
        public virtual void Write(System.Drawing.Font value)
        {
            if (value != null)
            {
                using (Feng.IO.BufferWriter bw = new BufferWriter())
                {
                    bw.Write(value.FontFamily.Name);
                    bw.Write(value.Size);
                    bw.Write((int)value.Style);
                    this.Write(bw.GetData());
                    bw.Close();
                }
            }
            else
            {
                this.Write(new byte[] { });
            }
        }

        public virtual void Write(float[] value)
        {
            if (value == null)
            {
                this.WriteInt(0);
                return;
            }
            this.Write(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                this.Write(value[i]);
            }
        }




        public virtual void Write(string[] value)
        {
            this.Write(value.Length);
            foreach (string str in value)
            {
                this.Write(str);
            }
        }

        public override void Write(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                base.Write(Feng.Constant.DataSetting.ZeroLen);
                return;
            }
            byte[] data = Feng.IO.BitConver.GetBytes(value);
            base.Write(data.Length);
            base.Write(data);
        }

        public override void Write(byte[] value)
        {
            if (value == null)
            {
                base.WriteInt(0);
                return;
            }

            base.Write(value.Length);
            base.Write(value);
        }

        public virtual void Write(byte[] value, int len)
        {
            if (value == null)
            {
                base.WriteInt(0);
                return;
            }

            base.Write(len);
            base.Write(value, 0, len);
        }

        public virtual void Write(DateTime date)
        {
            base.Write((short)date.Year);
            base.Write((byte)date.Month);
            base.Write((byte)date.Day);
            base.Write((byte)date.Hour);
            base.Write((byte)date.Minute);
            base.Write((byte)date.Second);
            base.Write((short)date.Millisecond);
        }

        public virtual void WriteSerializeValue(object value)
        {
            try
            {

                if (value == null)
                {
                    base.WriteInt(0);
                    return;
                }
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bft = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bft.Serialize(ms, value);
                    this.Write(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Feng.IO", "BufferWriter", "WriteSerializeValue", ex);
            }
        }

        public virtual void Write(ushort index, DateTime value)
        {
            base.Write(index);
            base.Write(TypeEnum.TDateTime);
            this.Write(value);

        }

        public virtual void Write(ushort index, bool value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbool);
            base.Write(value);

        }

        public virtual void Write(ushort index, byte value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbyte);
            base.Write(value);

        }

        public virtual void Write(ushort index, byte[] value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);

        }

        public virtual void Write(ushort index, char value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tchar);
            this.Write(value);

        }

        public virtual void Write(ushort index, char[] value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tchars);

            byte[] data = System.Text.Encoding.Unicode.GetBytes(value);
            base.Write(data.Length);
            base.Write(data);

        }

        public virtual void Write(ushort index, decimal value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tdecimal);
            this.Write(value);

        }

        public virtual void Write(ushort index, double value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tdouble);
            base.Write(value);

        }

        public virtual void Write(ushort index, float value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tfloat);
            base.Write(value);

        }

        public virtual void Write(ushort index, int value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tint);
            base.Write(value);

        }

        public virtual void Write(ushort index, long value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tlong);
            base.Write(value);

        }

        public virtual void Write(ushort index, sbyte value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tsbyte);
            base.Write(value);

        }

        public virtual void Write(ushort index, short value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tshort);
            base.Write(value);

        }

        public virtual void Write(ushort index, uint value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tuint);
            base.Write(value);

        }

        public virtual void Write(ushort index, ulong value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tulong);
            base.Write(value);

        }

        public virtual void Write(ushort index, ushort value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tushort);
            base.Write(value);

        }

        public virtual void Write(ushort index, string value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tstring);
            this.Write(value);

        }

        public virtual void Write(ushort index, Color value)
        {
            base.Write(index);
            base.Write(TypeEnum.TColor);
            this.Write(value);

        }

        public virtual void Write(ushort index, Margins value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);

        }
        public virtual void Write(ushort index, Font value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);

        }
 
        public virtual void Write(ushort index, DataStruct value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value); 
        }

        public virtual void Write(ushort index, System.Windows.Forms.Padding value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);

        }

        public virtual void Write(System.Windows.Forms.Padding value)
        {
            if (value != null)
            {
                using (Feng.IO.BufferWriter bw = new BufferWriter())
                {
                    bw.Write(value.Left);
                    bw.Write(value.Right);
                    bw.Write(value.Top);
                    bw.Write(value.Bottom);
                    this.Write(bw.GetData());
                    bw.Close();
                }
            }
            else
            {
                this.Write(new byte[] { });
            }
        }

        public virtual void Write(ushort index, System.Data.DataTable value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);
        }
        public virtual void Write(List<string> list)
        {
            this.Write(list.Count);
            foreach (string str in list)
            {
                this.Write(str);
            }
        }

        //public virtual void Write(ushort index, Image value)
        //{
        //    base.Write(index);
        //    base.Write(TypeEnum.TImage);
        //    this.WriteImage(value);

        //}
        public virtual void Write(ushort index, Bitmap value)
        {
            base.Write(index);
            base.Write(TypeEnum.TImage);
            this.WriteBitmap(value);

        }

        public virtual void Write(ushort index, DataStructCollection value)
        {
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);
            
        }

        public virtual void WriteBaseValue(ushort index,object value)
        {
            base.Write(index);
            if (value == null)
            {
                base.Write(TypeEnum.TNull);
            }
            else
            {
                this.Write(value.GetType(), value);
            }
        }

        public virtual void WriteBaseValue(object value)
        { 
            if (value == null)
            {
                this.Write(TypeEnum.TNull);
                return;
            }
            Type t = value.GetType();
            if (t == null)
            {
                this.Write(TypeEnum.TNull);
                return;
            }
            if (t == typeof(bool))
            {
                this.Write(TypeEnum.Tbool);
                this.Write((bool)value);
            }
            else if (t == typeof(byte))
            {
                this.Write(TypeEnum.Tbyte);
                this.Write((byte)value);
            }
            else if (t == typeof(char))
            {
                this.Write(TypeEnum.Tchar);
                this.Write((char)value);
            }
            else if (t == typeof(Color))
            {
                this.Write(TypeEnum.TColor);
                this.Write((Color)value);
            }
            else if (t == typeof(DateTime))
            {
                this.Write(TypeEnum.TDateTime);
                this.Write((DateTime)value);
            }
            else if (t == typeof(decimal))
            {
                this.Write(TypeEnum.Tdecimal);
                this.Write((decimal)value);
            }
            else if (t == typeof(double))
            {
                this.Write(TypeEnum.Tdouble);
                this.Write((double)value);
            }
            else if (t == typeof(float))
            {
                this.Write(TypeEnum.Tfloat);
                this.Write((float)value);
            }
            else if (t == typeof(int))
            {
                this.Write(TypeEnum.Tint);
                this.Write((int)value);
            }
            else if (t == typeof(long))
            {
                this.Write(TypeEnum.Tlong);
                this.Write((long)value);
            }
            else if (t == typeof(sbyte))
            {
                this.Write(TypeEnum.Tsbyte);
                this.Write((sbyte)value);
            }
            else if (t == typeof(short))
            {
                this.Write(TypeEnum.Tshort);
                this.Write((short)value);
            }
            else if (t == typeof(string))
            {
                this.Write(TypeEnum.Tstring);
                this.Write((string)value);
            }
            else if (t == typeof(uint))
            {
                this.Write(TypeEnum.Tuint);
                this.Write((uint)value);
            }
            else if (t == typeof(ulong))
            {
                this.Write(TypeEnum.Tulong);
                this.Write((ulong)value);
            }
            else if (t == typeof(ushort))
            {
                this.Write(TypeEnum.Tushort);
                this.Write((ushort)value);
            }
            else if (t == typeof(bool?))
            {
                this.Write(TypeEnum.TNullablebool);
                this.Write(((bool?)value).Value);
            }
            else if (t == typeof(byte?))
            {
                this.Write(TypeEnum.TNullablebyte);
                this.Write(((byte?)value).Value);
            }
            else if (t == typeof(char?))
            {
                this.Write(TypeEnum.TNullablechar);
                this.Write(((char?)value).Value);
            }
            else if (t == typeof(Color?))
            {
                this.Write(TypeEnum.TNullableColor);
                this.Write(((Color?)value).Value);
            }
            else if (t == typeof(DateTime?))
            {
                this.Write(TypeEnum.TNullableDateTime);
                this.Write(((DateTime?)value).Value);
            }
            else if (t == typeof(decimal?))
            {
                this.Write(TypeEnum.TNullabledecimal);
                this.Write(((decimal?)value).Value);
            }
            else if (t == typeof(double?))
            {
                this.Write(TypeEnum.TNullabledouble);
                this.Write(((double?)value).Value);
            }
            else if (t == typeof(float?))
            {
                this.Write(TypeEnum.TNullablefloat);
                this.Write(((float?)value).Value);
            }
            else if (t == typeof(int?))
            {
                this.Write(TypeEnum.TNullableint);
                this.Write(((int?)value).Value);
            }
            else if (t == typeof(long?))
            {
                this.Write(TypeEnum.TNullablelong);
                this.Write(((long?)value).Value);
            }
            else if (t == typeof(sbyte?))
            {
                this.Write(TypeEnum.TNullablesbyte);
                this.Write(((sbyte?)value).Value);
            }
            else if (t == typeof(short?))
            {
                this.Write(TypeEnum.TNullableshort);
                this.Write(((short?)value).Value);
            }
            else if (t == typeof(uint?))
            {
                this.Write(TypeEnum.TNullableuint);
                this.Write(((uint?)value).Value);
            }
            else if (t == typeof(ulong?))
            {
                this.Write(TypeEnum.TNullableulong);
                this.Write(((ulong?)value).Value);
            }
            else if (t == typeof(ushort?))
            {
                this.Write(TypeEnum.TNullableushort);
                this.Write(((ushort?)value).Value);
            }
            else
            {
                this.Write(TypeEnum.TObject);
                this.WriteSerializeValue(value);
            }
        }
        public virtual void WriteSerializeValue(ushort index, object value)
        {
            base.Write(index);
            base.Write(TypeEnum.TObject);
            this.WriteSerializeValue(value);
        }

        public virtual void Write(SqlParameter sqm)
        {
            if (sqm == null)
            {
                this.Write(false);
                return;
            }
            else
            {
                this.Write(true);
            }
            this.Write((int)sqm.DbType);
            this.Write((int)sqm.Direction);
            this.Write(sqm.IsNullable);
            this.Write(sqm.LocaleId);
            this.Write(sqm.Offset);
            this.Write(sqm.ParameterName);
            this.Write(sqm.Precision);
            this.Write(sqm.Scale);
            this.Write(sqm.Size);
            this.Write(sqm.SourceColumn);
            this.Write(sqm.SourceColumnNullMapping);
            this.Write((int)sqm.SourceVersion);
            this.Write((int)sqm.SqlDbType);
            this.Write(sqm.TypeName);
            this.Write(sqm.UdtTypeName);
            this.WriteSerializeValue(sqm.Value);
            this.Write(sqm.XmlSchemaCollectionDatabase);
            this.Write(sqm.XmlSchemaCollectionName);
            this.Write(sqm.XmlSchemaCollectionOwningSchema);
        }

        public virtual void Write(ModleInfo sqm)
        {
            if (sqm == null)
            {
                this.Write(false);
                return;
            }
            else
            {
                this.Write(true);
            }
            this.Write(sqm.Sql);
            if (sqm.cmdParms == null)
            {
                this.Write(0);
                return;
            }
            this.Write(sqm.cmdParms.Length);
            for (int i = 0; i < sqm.cmdParms.Length; i++)
            {
                this.Write(sqm.cmdParms[i]);
            }
        }

        public virtual void Write(DataValue value)
        {
            if (value == null)
            {
                this.Write(new byte[] { });
            }
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.Write(value.Name);
                switch (value.Type)
                {
                    case TypeEnum.Tbool:
                        bw.Write(TypeEnum.Tbool);
                        bw.Write((bool)value.Value);
                        break;
                    case TypeEnum.Tbyte:
                        bw.Write(TypeEnum.Tbyte);
                        bw.Write((byte)value.Value);
                        break;
                    case TypeEnum.Tbytes:
                        bw.Write(TypeEnum.Tbytes);
                        bw.Write((byte[])value.Value);
                        break;
                    case TypeEnum.Tchar:
                        bw.Write(TypeEnum.Tchar);
                        bw.Write((char)value.Value);
                        break;
                    case TypeEnum.Tchars:
                        bw.Write(TypeEnum.Tchars);
                        bw.Write((char[])value.Value);
                        break;
                    case TypeEnum.Tdecimal:
                        bw.Write(TypeEnum.Tdecimal);
                        bw.Write((decimal)value.Value);
                        break;
                    case TypeEnum.Tfloat:
                        bw.Write(TypeEnum.Tfloat);
                        bw.Write((float)value.Value);
                        break;
                    case TypeEnum.Tint:
                        bw.Write(TypeEnum.Tint);
                        bw.Write((int)value.Value);
                        break;
                    case TypeEnum.TInt32:
                        bw.Write(TypeEnum.TInt32);
                        bw.Write((Int32)value.Value);
                        break;
                    case TypeEnum.Tlong:
                        bw.Write(TypeEnum.Tlong);
                        bw.Write((long)value.Value);
                        break;
                    case TypeEnum.TInt64:
                        bw.Write(TypeEnum.TInt64);
                        bw.Write((Int64)value.Value);
                        break;
                    case TypeEnum.Tsbyte:
                        bw.Write(TypeEnum.Tsbyte);
                        bw.Write((sbyte)value.Value);
                        break;
                    case TypeEnum.Tshort:
                        bw.Write(TypeEnum.Tshort);
                        bw.Write((short)value.Value);
                        break;
                    case TypeEnum.TInt16:
                        bw.Write(TypeEnum.TInt16);
                        bw.Write((Int16)value.Value);
                        break;
                    case TypeEnum.Tstring:
                        bw.Write(TypeEnum.Tstring);
                        bw.Write((string)value.Value);
                        break;
                    case TypeEnum.Tuint:
                        bw.Write(TypeEnum.Tuint);
                        bw.Write((uint)value.Value);
                        break;
                    case TypeEnum.TUInt32:
                        bw.Write(TypeEnum.TUInt32);
                        bw.Write((UInt32)value.Value);
                        break;
                    case TypeEnum.Tulong:
                        bw.Write(TypeEnum.Tulong);
                        bw.Write((ulong)value.Value);
                        break;
                    case TypeEnum.TUInt64:
                        bw.Write(TypeEnum.TUInt64);
                        bw.Write((UInt64)value.Value);
                        break;
                    case TypeEnum.Tushort:
                        bw.Write(TypeEnum.Tushort);
                        bw.Write((ushort)value.Value);
                        break;
                    case TypeEnum.TUInt16:
                        bw.Write(TypeEnum.TUInt16);
                        bw.Write((UInt16)value.Value);
                        break;
                    case TypeEnum.Tdouble:
                        bw.Write(TypeEnum.Tdouble);
                        bw.Write((double)value.Value);
                        break;
                    case TypeEnum.TDateTime:
                        bw.Write(TypeEnum.TDateTime);
                        bw.Write((DateTime)value.Value);
                        break;

                    case TypeEnum.TNullablebool:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.TDateTime);
                            bw.Write(((bool?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullablebyte:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tbyte);
                            bw.Write(((byte?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullablechar:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tchar);
                            bw.Write(((char?)value.Value).Value);
                        }
                        break;

                    case TypeEnum.TNullabledecimal:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tdecimal);
                            bw.Write(((decimal?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullablefloat:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tfloat);
                            bw.Write(((float?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullableint:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tint);
                            bw.Write(((int?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullablelong:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tlong);
                            bw.Write(((long?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullablesbyte:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tsbyte);
                            bw.Write(((sbyte?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullableshort:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tshort);
                            bw.Write(((short?)value.Value).Value);
                        }
                        break;
                    //case TypeEnum.TNullablestring:
                    //    if (((bool?)value.Value).HasValue)
                    //    {
                    //        bw.Write(TypeEnum.TDateTime);
                    //        bw.Write(((bool?)value.Value).Value);
                    //    }
                    //    break;
                    case TypeEnum.TNullableuint:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.TDateTime);
                            bw.Write(((uint?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullableulong:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tulong);
                            bw.Write(((ulong?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullableushort:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tushort);
                            bw.Write(((ushort?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullabledouble:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.Tdouble);
                            bw.Write(((double?)value.Value).Value);
                        }
                        break;
                    case TypeEnum.TNullableDateTime:
                        if (((bool?)value.Value).HasValue)
                        {
                            bw.Write(TypeEnum.TDateTime);
                            bw.Write(((DateTime?)value.Value).Value);
                        }
                        break;
                    default:
                        bw.Write(TypeEnum.TNull);
                        break;
                }
                this.Write(bw.GetData());
            }
        }

        public virtual void Write(Type t, object value)
        {
            if (t == null)
            {
                this.Write(TypeEnum.TNull);
                return;
            }
            if (t == typeof(bool))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tbool);
                    this.Write((bool)value);
                }
            }
            else if (t == typeof(byte))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tbyte);
                    this.Write((byte)value);
                }
            }

            else if (t == typeof(byte[]))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tbytes);
                    this.Write((byte[])value);
                }
            }
            else if (t == typeof(char))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tchar);
                    this.Write((char)value);
                }
            }
            else if (t == typeof(char[]))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tchars);
                    this.Write((char[])value);
                }
            }
            else if (t == typeof(Color))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.TColor);
                    this.Write((Color)value);
                }
            }
            else if (t == typeof(DateTime))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.TDateTime);
                    this.Write((DateTime)value);
                }
            }
            else if (t == typeof(decimal))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tdecimal);
                    this.Write((decimal)value);
                }
            }
            else if (t == typeof(double))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tdouble);
                    this.Write((double)value);
                }
            }
            else if (t == typeof(float))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tfloat);
                    this.Write((float)value);
                }
            }
            else if (t == typeof(int))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tint);
                    this.Write((int)value);
                }
            }
            else if (t == typeof(long))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tlong);
                    this.Write((long)value);
                }
            }
            else if (t == typeof(sbyte))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tsbyte);
                    this.Write((sbyte)value);
                }
            }
            else if (t == typeof(short))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tshort);
                    this.Write((short)value);
                }
            }
            else if (t == typeof(string))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tstring);
                    this.Write((string)value);
                }
            }
            else if (t == typeof(uint))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tuint);
                    this.Write((uint)value);
                }
            }
            else if (t == typeof(ulong))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tulong);
                    this.Write((ulong)value);
                }
            }
            else if (t == typeof(ushort))
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.Tushort);
                    this.Write((ushort)value);
                }
            }
            else
            {
                if (value == null)
                {
                    this.Write(TypeEnum.TNull);
                }
                else
                {
                    this.Write(TypeEnum.TObject);
                    this.WriteSerializeValue(value);
                }
            }

        }
 
        //public virtual void WriteImage(Image value)
        //{
        //    byte[] data = Feng.Drawing.ImageHelper.GetData(value);
        //    this.Write(data);
        //}

        public virtual void WriteBitmap(Bitmap value)
        {
            byte[] data = Feng.Drawing.ImageHelper.GetData(value);
            this.Write(data);
        }
        public virtual void Write(Feng.IO.BufferWriter bw,System.Data.DataColumn datacolumn)
        {
            bw.Write(datacolumn.AllowDBNull);
            bw.Write(datacolumn.Caption);
            bw.Write(datacolumn.ColumnName); 
            bw.Write(TypeEnum.GetTypeEnum(datacolumn.DataType));
            bw.Write(new byte [0]);
        }
 
        public virtual void Write(System.Data.DataTable table)
        {
            if (table == null)
            {
                this.Write(0);
                return;
            }
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.Write(new byte[0]);
                bw.Write(table.Columns.Count);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Write(bw, table.Columns[i]);
                }
                bw.Write(new byte[0]);
                bw.Write(table.Rows.Count);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    System.Data.DataRow row = table.Rows[i];
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        System.Data.DataColumn col = table.Columns[j];
                        object value = row[j];
                        if (value == DBNull.Value)
                        {
                            bw.Write(TypeEnum.TNull);
                        }
                        else
                        {
                            bw.Write(col.DataType, row[j]);
                        }
                    }
                }
                this.Write(bw.GetData());
            }
        }

        public virtual void Write(System.Data.DataSet dataset)
        {

            if (dataset == null)
            {
                this.Write(0);
                return;
            }
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.WriteSerializeValue(dataset.Clone());
                bw.Write(dataset.Tables.Count);
                for (int i = 0; i < dataset.Tables.Count; i++)
                {
                    bw.Write(dataset.Tables[i]);
                }
                this.Write(bw.GetData());
            }
        }
 
        public virtual void Write(DataStruct data)
        {
            if (data == null)
            {
                this.Write(new byte[] { });
            }
            else
            {
                using (Feng.IO.BufferWriter bs = new BufferWriter())
                {
                    bs.Write(data.Name);
                    bs.Write(data.Version);
                    bs.Write(data.FullName);
                    bs.Write(data.AessemlyDownLoadUrl);
                    bs.Write(data.DllName);
                    bs.Write(data.Data); 
                    this.Write(bs.GetData());
                }
            }
        }

        public virtual void Write(DataStructCollection value)
        {
            if (value == null)
            {
                this.Write(new byte[] { });
            }
            else
            {
                using (Feng.IO.BufferWriter bs = new BufferWriter())
                {
                    bs.Write(value.Count);
                    foreach (DataStruct ds in value)
                    {
                        bs.Write(ds);
                    }
                    byte[] data = bs.GetData();
                    this.Write(data);
                }
            }
        }

        public virtual void Write(string key, byte[] value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);

        }

        public virtual void Write(string key, char[] value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tchars);

            byte[] data = System.Text.Encoding.Unicode.GetBytes(value);
            base.Write(data.Length);
            base.Write(data);

        }

        public virtual void Write(string key, string value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tstring);
            this.Write(value);

        }

        public virtual void Write(string key, DateTime value)
        {
            this.Write(key);
            base.Write(TypeEnum.TDateTime);
            this.Write(value);

        }

        public virtual void Write(string key, bool value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tbool);
            base.Write(value);

        }

        public virtual void Write(string key, byte value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tbyte);
            base.Write(value);

        }

        public virtual void Write(string key, char value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tchar);
            this.Write(value);

        }

        public virtual void Write(string key, decimal value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tdecimal);
            this.Write(value);

        }

        public virtual void Write(string key, double value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tdouble);
            base.Write(value);

        }

        public virtual void Write(string key, float value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tfloat);
            base.Write(value);

        }

        public virtual void Write(string key, int value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tint);
            base.Write(value);

        }

        public virtual void Write(string key, long value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tlong);
            base.Write(value);

        }

        public virtual void Write(string key, sbyte value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tsbyte);
            base.Write(value);

        }

        public virtual void Write(string key, short value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tshort);
            base.Write(value);

        }

        public virtual void Write(string key, uint value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tuint);
            base.Write(value);

        }

        public virtual void Write(string key, ulong value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tulong);
            base.Write(value);

        }

        public virtual void Write(string key, ushort value)
        {
            this.Write(key);
            base.Write(TypeEnum.Tushort);
            base.Write(value);

        }

        public virtual void WriteNullAble(string key, int? value)
        {
            this.Write(key);
            base.Write(TypeEnum.TNullableint);
            base.Write(value.HasValue);
            if (value.HasValue)
            {
                base.Write(value.Value);
            }
    
        }
        public virtual void WriteNullAble(string key, DateTime? value)
        {
            this.Write(key);
            base.Write(TypeEnum.TNullableDateTime);
            base.Write(value.HasValue);
            if (value.HasValue)
            {
                this.Write(value.Value);
            }

        }
        public virtual void WriteNullAble(string key, decimal? value)
        {
            this.Write(key);
            base.Write(TypeEnum.TNullabledecimal);
            base.Write(value.HasValue);
            if (value.HasValue)
            {
                base.Write(value.Value);
            }
    
        }
        public virtual void WriteNullAble(string key, long? value)
        {
            this.Write(key);
            base.Write(TypeEnum.TNullableulong);
            base.Write(value.HasValue);
            if (value.HasValue)
            {
                base.Write(value.Value);
            }

        }
 
        public virtual void Write(ushort index, string value, string defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tstring);
            this.Write(value);

        }


        public virtual void Write(ushort index, byte value, byte defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tbyte);
            base.Write(value);

        }

        public virtual void Write(ushort index, byte[] value, byte[] defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);

        }

        public virtual void Write(ushort index, char value, char defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tchar);
            this.Write(value);

        }

        public virtual void Write(ushort index, char[] value, char[] defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tchars);

            byte[] data = System.Text.Encoding.Unicode.GetBytes(value);
            base.Write(data.Length);
            base.Write(data);

        }

        public virtual void Write(ushort index, decimal value, decimal defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tdecimal);
            this.Write(value);

        }

        public virtual void Write(ushort index, double value, double defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tdouble);
            base.Write(value);

        }

        public virtual void Write(ushort index, float value, float defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tfloat);
            base.Write(value);

        }

        public virtual void Write(ushort index, int value, int defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tint);
            base.Write(value);

        }

        public virtual void Write(ushort index, long value, long defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tlong);
            base.Write(value);

        }

        public virtual void Write(ushort index, sbyte value, sbyte defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tsbyte);
            base.Write(value);

        }

        public virtual void Write(ushort index, short value, short defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tshort);
            base.Write(value);

        }

        public virtual void Write(ushort index, uint value, uint defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tuint);
            base.Write(value);

        }

        public virtual void Write(ushort index, ulong value, ulong defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tulong);
            base.Write(value);

        }

        public virtual void Write(ushort index, ushort value, ushort defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tushort);
            base.Write(value);

        }

        public virtual void Write(ushort index, float[] value, float[] defaultvalue)
        {

            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
 
            if (value == null)
            {
                this.WriteInt(0);
                return;
            }
            this.Write(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                this.Write(value[i]);
            }
        }

        public virtual void WriteBaseValue(ushort index, object value, object defaultvalue)
        { 
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            this.WriteBaseValue(value);

        }


        public virtual void Write(StringFormat value)
        {
            this.WriteSerializeValue(value);
        }

        public virtual void Write(ushort index, System.Windows.Forms.Cursor value)
        {
            base.Write(index);
            base.Write(TypeEnum.TObject);
            this.WriteSerializeValue(value);

        }

        public virtual void Write(ushort index, StringFormat value)
        {
            base.Write(index);
            base.Write(TypeEnum.TObject);
            this.Write(value);

        }

        public virtual void Write(ushort index, LockVersion value)
        { 
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value.GetData());

        }

        public virtual void Write(ushort index, LockVersion value, LockVersion defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value.GetData());

        }

        public virtual void Write(ushort index, bool value, bool defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tbool);
            base.Write(value);

        }

        public virtual void Write(ushort index, Color value, Color defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.TColor);
            this.Write(value);

        }

        public virtual void Write(ushort index, Bitmap value, Bitmap defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.TImage);
            this.WriteBitmap(value);

        }

        public virtual void Write(ushort index, Font value, Font defaultvalue)
        {
            if (value != null)
            {

            }
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);

        }

        public virtual void Write(ushort index, DataStruct value, DataStruct defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            if (value.Data == null)
                return;
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(value);
        }

        public virtual void Write(ushort index, IDataStruct value, IDataStruct defaultvalue)
        {
            if (value == null)
            {
                return;
            }
            if (value == defaultvalue)
            {
                return;
            }
            DataStruct data = value.Data;
            if (data == null)
                return;
            if (data.Data.Length < 1)
                return;
            base.Write(index);
            base.Write(TypeEnum.Tbytes);
            this.Write(data);
        }

        public virtual void Write(ushort index, StringFormat value, StringFormat defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.TObject);
            this.Write(value);

        }

        public virtual void WriteSerializeValue(ushort index, object value, object defaultvalue)
        {
            if (value == defaultvalue)
            {
                return;
            }
            base.Write(index);
            base.Write(TypeEnum.TObject);
            this.WriteSerializeValue(value);
        }
    }
}