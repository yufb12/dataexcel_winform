using Feng.Collections;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class ReflectionContainer : CBMethodContainer
    {
#warning 具备风险，需要设置执行条件
        public const string Function_Category = "ReflectionFunction";
        public const string Function_Description = "反射调用";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public ReflectionContainer()
        { 
            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "RefGetValue";
            model.Description = @"通过反射获取数据 VAR VALUE =RefGetValue(obj,""NAME"")";
            model.Eg = @"VAR VALUE =RefGetValue(obj,""NAME"")";
            model.Function = RefGetValue;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RefExec";
            model.Description = @"通过反射执行方法 VAR VALUE =RefExec(obj,""GetName"")";
            model.Eg = @"VAR VALUE =RefExec(obj,""GetName"")";
            model.Function = RefExec;
            MethodList.Add(model);

        }
         

        public virtual object RefGetValue(params object[] args)
        {
            object obj = base.GetArgIndex(1, args);
            string name = base.GetTextValue(2, string.Empty);
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name);
            return propertyInfo.GetValue(obj,null);
        }

        public virtual object RefExec(params object[] args)
        {
            object obj = base.GetArgIndex(1, args);
            string name = base.GetTextValue(2, string.Empty);
            MethodInfo methodInfo = obj.GetType().GetMethod(name);
            object[] objs = new object[args.Length - 3];
            for (int i = 0; i < args.Length; i++)
            {
                objs[i] = args[i + 3];
            }
            return methodInfo.Invoke(obj, objs);
        }

    }
}
