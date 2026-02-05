using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using Feng.Utils;

using System.Drawing;
namespace Feng.Data
{
    public class DataValueCollection : IList<DataValue>
    {
        private List<DataValue> list = new List<DataValue>();

        public DataValue this[int index]
        {
            get
            {
                if (index >= list.Count)
                {
                    return null;
                }
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(DataValue item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DataValue item)
        {
            return list.Contains(item);
        }

        public void CopyTo(DataValue[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DataValue item)
        {
            return list.Remove(item);
        }

        public IEnumerator<DataValue> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(DataValue item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, DataValue item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

    public class DataValue
    {
        public string Name { get; set; }
        public byte Type { get; set; }
        public object Value { get; set; }
    }

    public class TypeEnum
    {
        public const byte Tbool = 1;
        public const byte Tbyte = 2;
        public const byte Tbytes = 3;
        public const byte Tchar = 4;
        public const byte Tchars = 5;
        public const byte Tdecimal = 6;
        public const byte Tfloat = 7;
        public const byte Tint = 8;
        public const byte TInt32 = 9;
        public const byte Tlong = 10;
        public const byte TInt64 = 11;
        public const byte Tsbyte = 12;
        public const byte Tshort = 13;
        public const byte TInt16 = 14;
        public const byte Tstring = 15;
        public const byte Tuint = 16;
        public const byte TUInt32 = 17;
        public const byte Tulong = 18;
        public const byte TUInt64 = 19;
        public const byte Tushort = 20;
        public const byte TUInt16 = 21;
        public const byte Tdouble = 22;
        public const byte TDateTime = 23;

        public const byte TNullablebool = 51;
        public const byte TNullablebyte = 52;
        public const byte TNullablebytes = 53;
        public const byte TNullablechar = 54;
        public const byte TNullablechars = 55;
        public const byte TNullabledecimal = 56;
        public const byte TNullablefloat = 57;
        public const byte TNullableint = 58;
        public const byte TNullablelong = 59;
        public const byte TNullablesbyte = 60;
        public const byte TNullableshort = 61;
        //public const byte TNullablestring = 62;
        public const byte TNullableuint = 63;
        public const byte TNullableulong = 64;
        public const byte TNullableushort = 65;
        public const byte TNullabledouble = 66;
        public const byte TNullableDateTime = 67;


        public const byte TColor = 131;
        public const byte TNullableColor = 231;
        public const byte TObject = 132;
        public const byte TFile = 133;
        public const byte TImage = 134;

        public const byte TMAXVALUE = 100;
        public const byte TNull = 250;

        public static bool IsBasicType(Type type)
        {
            byte btype = GetTypeEnum(type);
            if (btype < 128)
            {
                return true;
            }
            return false;
        }

        public static byte GetTypeEnum(Type type)
        {
            if (type == typeof(bool))
            {
                return TypeEnum.Tbool;
            }
            else if (type == typeof(byte))
            {
                return TypeEnum.Tbyte;
            }
            else if (type == typeof(char))
            {
                return TypeEnum.Tchar;
            }
            else if (type == typeof(char[]))
            {
                return TypeEnum.Tchars;
            }
            else if (type == typeof(Color))
            {
                return TypeEnum.TColor;
            }
            else if (type == typeof(DateTime))
            {
                return TypeEnum.TDateTime;
            }
            else if (type == typeof(decimal))
            {
                return TypeEnum.Tdecimal;
            }
            else if (type == typeof(double))
            {
                return TypeEnum.Tdouble;
            }
            else if (type == typeof(float))
            {
                return TypeEnum.Tfloat;
            }
            else if (type == typeof(int))
            {
                return TypeEnum.Tint;
            }
            else if (type == typeof(long))
            {
                return TypeEnum.Tlong;
            }
            else if (type == typeof(Object))
            {
                return TypeEnum.TObject;
            }
            else if (type == typeof(DateTime?))
            {
                return TypeEnum.TNullableDateTime;
            }
            else if (type == typeof(sbyte))
            {
                return TypeEnum.Tsbyte;
            }
            else if (type == typeof(short))
            {
                return TypeEnum.Tshort;
            }
            else if (type == typeof(string))
            {
                return TypeEnum.Tstring;
            }
            else if (type == typeof(uint))
            {
                return TypeEnum.Tuint;
            }
            else if (type == typeof(ulong))
            {
                return TypeEnum.Tulong;
            }
            else if (type == typeof(ushort))
            {
                return TypeEnum.Tushort;
            }
            else if (type == typeof(bool?))
            {
                return TypeEnum.TNullablebool;
            }
            else if (type == typeof(byte?))
            {
                return TypeEnum.TNullablebyte;
            }
            else if (type == typeof(char?))
            {
                return TypeEnum.TNullablechar;
            }  
            else if (type == typeof(DateTime?))
            {
                return TypeEnum.TNullableDateTime;
            }
            else if (type == typeof(decimal?))
            {
                return TypeEnum.TNullabledecimal;
            }
            else if (type == typeof(double?))
            {
                return TypeEnum.TNullabledouble;
            }
            else if (type == typeof(float?))
            {
                return TypeEnum.TNullablefloat;
            }
            else if (type == typeof(int?))
            {
                return TypeEnum.TNullableint;
            }
            else if (type == typeof(long?))
            {
                return TypeEnum.TNullablelong;
            } 
            else if (type == typeof(DateTime?))
            {
                return TypeEnum.TNullableDateTime;
            }
            else if (type == typeof(sbyte?))
            {
                return TypeEnum.TNullablesbyte;
            }
            else if (type == typeof(short?))
            {
                return TypeEnum.TNullableshort;
            } 
            else if (type == typeof(uint?))
            {
                return TypeEnum.TNullableuint;
            }
            else if (type == typeof(ulong?))
            {
                return TypeEnum.TNullableulong;
            }
            else if (type == typeof(ushort?))
            {
                return TypeEnum.TNullableushort;
            }
            else
            {
                return TypeEnum.TNull;
            }
        }

        public static Type GetType(byte type)
        {
            switch (type)
            {
                case TypeEnum.Tbool:
                    return typeof(bool);
                case TypeEnum.Tbyte:
                    return typeof(byte);
                case TypeEnum.Tchar:
                    return typeof(char);
                case TypeEnum.Tchars:
                    return typeof(char[]);
                case TypeEnum.TColor:
                    return typeof(Color);
                case TypeEnum.TDateTime:
                    return typeof(DateTime);
                case TypeEnum.Tdecimal:
                    return typeof(decimal);
                case TypeEnum.Tdouble:
                    return typeof(double);
                case TypeEnum.Tfloat:
                    return typeof(float);
                case TypeEnum.Tint:
                    return typeof(int);
                case TypeEnum.Tlong:
                    return typeof(long);
                case TypeEnum.TObject:
                    return typeof(Object);
                case TypeEnum.TNullableDateTime:
                    return typeof(DateTime?);
                case TypeEnum.Tsbyte:
                    return typeof(sbyte);
                case TypeEnum.Tshort:
                    return typeof(short);
                case TypeEnum.Tstring:
                    return typeof(string);
                case TypeEnum.Tuint:
                    return typeof(uint);
                case TypeEnum.Tulong:
                    return typeof(ulong);
                case TypeEnum.Tushort:
                    return typeof(ushort);
                case TypeEnum.TNullablebool:
                    return typeof(bool?);
                case TypeEnum.TNullablebyte:
                    return typeof(byte?);
                case TypeEnum.TNullablechar:
                    return typeof(char?);
                case TypeEnum.TNullabledecimal:
                    return typeof(decimal?);
                case TypeEnum.TNullabledouble:
                    return typeof(double?);
                case TypeEnum.TNullablefloat:
                    return typeof(float?);
                case TypeEnum.TNullableint:
                    return typeof(int?);
                case TypeEnum.TNullablelong:
                    return typeof(long?);
                case TypeEnum.TNullablesbyte:
                    return typeof(sbyte?);
                case TypeEnum.TNullableshort:
                    return typeof(short?);
                case TypeEnum.TNullableuint:
                    return typeof(uint?);
                case TypeEnum.TNullableulong:
                    return typeof(ulong?);
                case TypeEnum.TNullableushort:
                    return typeof(ushort?);
                default:
                    return null;
            }
        }
    }
}
