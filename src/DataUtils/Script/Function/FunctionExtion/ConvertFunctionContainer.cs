using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class ConvertFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "ConvertFunction";
        public const string Function_Description = "数值转换";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public ConvertFunctionContainer()
        {

            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "ToString";
            model.Description = "转换为字符串 ToString(value)";
            model.Eg = @"var txt=ToString(value)";
            model.Function = ConvertToString;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToBoolean";
            model.Description = "转换为Boolean类型 ToBoolean(value)";
            model.Eg = @"var bol=ToBoolean(value)";
            model.Function = ConvertToBoolean;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToDecimal";
            model.Description = "转换为Decimal类型 ToDecimal(value)";
            model.Eg = @"var dec=ToDecimal(value)";
            model.Function = ConvertToDecimal;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToDouble";
            model.Description = "转换为Double类型 ToDouble(value)";
            model.Eg = @"var dou=ToDouble(value)";
            model.Function = ConvertToDouble;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ToFloat";
            model.Description = "转换为Float类型 ToFloat(value)";
            model.Eg = @"var fl=ToFloat(value)";
            model.Function = ConvertToSingle;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToDateTime";
            model.Description = "转换为DateTime类型 ToDateTime(value)";
            model.Eg = @"var time=ToDateTime(value)";
            model.Function = ConvertToDateTime;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToInt";
            model.Description = "转换为Int类型 ToInt(value)";
            model.Eg = @"var it=ToInt(value)";
            model.Function = ConvertToInt;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToInt64";
            model.Description = "转换为Int64(long)类型 ToInt64(value)";
            model.Eg = @"var it64=ToInt64(value)";
            model.Function = ConvertToInt64;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToUInt64";
            model.Description = "转换为UInt64类型 ToUInt64(value)";
            model.Eg = @"var uit=ToUInt64(value)";
            model.Function = ConvertToUInt64;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToUInt";
            model.Description = "转换为Int32类型 ToUInt(value)";
            model.Eg = @"var itu=ToUInt(value)";
            model.Function = ConvertToUInt;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ToUInt16";
            model.Description = "转换为Int16(ushort)类型 ToUInt16(value)";
            model.Eg = @"var itu=ToUInt16(value)";
            model.Function = ConvertToUInt16;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ToInt16";
            model.Description = "转换为Int16(short)类型 ToInt16(value)";
            model.Eg = @"var itu=ToInt16(value)";
            model.Function = ConvertToInt16;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToByte";
            model.Description = "转换为ToByte(byte)类型 ToByte(value)";
            model.Eg = @"var itu=ToByte(value)";
            model.Function = ConvertToByte;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ToSByte";
            model.Description = "转换为ToSByte(sbyte)类型 ToSByte(value)";
            model.Eg = @"var itu=ToSByte(value)";
            model.Function = ConvertToSByte;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "IsEmpty";
            model.Description = "是否为空字符";
            model.Eg = @"IsEmpty(value)";
            model.Function = IsEmpty;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "IsNull";
            model.Description = "是否为空或者NULL";
            model.Eg = @"IsNull(value)";
            model.Function = IsNull;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ISNULLOREMPTY";
            model.Description = "是否为空或者NULL ISNULLOREMPTY(value)";
            model.Eg = @"ISNULLOREMPTY(value)";
            model.Function = ISNULLOREMPTY;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "IsNumber";
            model.Description = "是否为数字类型";
            model.Eg = @"IsNumber(value)";
            model.Function = IsNumber;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "IsDateTime";
            model.Description = "是否为日期类型";
            model.Eg = @"IsDateTime(value)";
            model.Function = IsDateTime;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ConvertToString";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToString(value)";
            model.Function = ConvertToString;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToBoolean";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToBoolean(value)";
            model.Function = ConvertToBoolean;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToDecimal";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToDecimal(value)";
            model.Function = ConvertToDecimal;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToDouble";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToDouble(value)";
            model.Function = ConvertToDouble;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ConvertToFloat";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToFloat(value)";
            model.Function = ConvertToSingle;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToDateTime";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToDateTime(value)";
            model.Function = ConvertToDateTime;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToInt";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToInt(value)";
            model.Function = ConvertToInt;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToInt64";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToInt64(value)";
            model.Function = ConvertToInt64;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToUInt64";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToUInt64(value)";
            model.Function = ConvertToUInt64;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ConvertToUInt";
            model.Description = "向前兼容已弃用";
            model.Eg = @"ConvertToUInt(value)";
            model.Function = ConvertToUInt;
            MethodList.Add(model);
        }
        public virtual object ConvertToString(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Feng.Utils.ConvertHelper.ToString(value); ;
        }
        public virtual object ConvertToBoolean(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToBoolean(value, throwex); ;
        }
        public virtual object ConvertToDecimal(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToDecimal(value, throwex); ;
        }
        public virtual object ConvertToDouble(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToDouble(value, throwex); ;
        }
        public virtual object ConvertToSingle(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToSingle(value, throwex); ;
        }
        public virtual object ConvertToDateTime(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToDateTime(value, throwex); ;
        }
        public virtual object ConvertToInt(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToInt32(value, 0, throwex); ;
        }
        public virtual object ConvertToInt64(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToInt64(value, throwex); ;
        }
        public virtual object ConvertToUInt(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToUInt32(value, throwex); ;
        }
        public virtual object ConvertToUInt64(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToUInt64(value, throwex); ;
        }
        public virtual object ConvertToUInt16(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToUInt16(value, throwex); ;
        }
        public virtual object ConvertToInt16(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToInt16(value, throwex); ;
        }
        public virtual object ConvertToByte(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToByte(value, throwex); ;
        }
        public virtual object ConvertToSByte(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            bool throwex = base.GetBooleanValue(2, args);
            return Feng.Utils.ConvertHelper.ToSByte(value, throwex); ;
        }
        public virtual object IsEmpty(params object[] args)
        {
            string value = base.GetTextValue(1, args);
            return string.IsNullOrWhiteSpace(value);
        }
        public virtual object IsNull(params object[] args)
        {
            object value = base.GetArgIndex(1, args); 
            if (value==null)
            {
                return true;
            }
            return false;
        }
        public virtual object ISNULLOREMPTY(params object[] args)
        {
            object value = base.GetArgIndex(1, args); 
            if (value==null)
            {
                return true;
            }
            string txt = value.ToString();
            return string.IsNullOrWhiteSpace(txt); 
        }
        public virtual object IsNumber(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            if (value == null)
                return false;
            return Feng.Utils.ConvertHelper.IsNumber(value.GetType());

        }
        public virtual object IsDateTime(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            if (value == null)
                return false;
            return Feng.Utils.ConvertHelper.IsDateTime(value.GetType());

        }
 
    }
}
