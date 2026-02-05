using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class TrigonometricFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "TrigonometricFunction";
        public const string Function_Description = "几何函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public TrigonometricFunctionContainer()
        {
#warning 错误
            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "Acos";
            model.Description = "反余弦函数";
            model.Function = Acos;
            MethodList.Add(model); 

            model = new BaseMethod();
            model.Name = "Asin";
            model.Description = "Asin";
            model.Function = Asin;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Atan";
            model.Description = "Atan";
            model.Function = Atan;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Atan2";
            model.Description = "Atan2";
            model.Function = Atan2;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Cos";
            model.Description = "Cos";
            model.Function = Cos;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Cosh";
            model.Description = "Cosh";
            model.Function = Cosh;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Sin";
            model.Description = "Sin";
            model.Function = Sin;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Sinh";
            model.Description = "Sinh";
            model.Function = Sinh;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Tan";
            model.Description = "Tan";
            model.Function = Tan;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Tanh";
            model.Description = "Tanh";
            model.Function = Tanh;
            MethodList.Add(model);
             
             
        }


        public object Acos(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Acos(ConvertHelper.ToDouble(value));
        }
        public object Asin(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Asin(ConvertHelper.ToDouble(value));
        }
        public object Atan(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Atan(ConvertHelper.ToDouble(value));
        }
        public object Atan2(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            return Math.Atan2(ConvertHelper.ToDouble(value1), ConvertHelper.ToDouble(value2));
        }
        public object Cos(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Cos(ConvertHelper.ToDouble(value));
        }
        public object Cosh(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Cosh(ConvertHelper.ToDouble(value));
        }
        public object Sin(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            return Math.Sin(ConvertHelper.ToDouble(value1));
        }
        public object Sinh(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            return Math.Sinh(ConvertHelper.ToDouble(value1));
        }
        public object Tan(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            return Math.Tan(ConvertHelper.ToDouble(value1));
        }
        public object Tanh(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            return Math.Tanh(ConvertHelper.ToDouble(value1));
        }

    }
}
