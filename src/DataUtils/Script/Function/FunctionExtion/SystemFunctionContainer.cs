using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class SystemFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "SystemFunction";
        public const string Function_Description = "系统函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public SystemFunctionContainer()
        {
            BaseMethod model = new BaseMethod();
            model.Name = "SystemVar";
            model.Description = "SystemVar";
            model.Function = SystemVar;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SystemTime";
            model.Description = "SystemTime";
            model.Function = SystemTime;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SystemIP";
            model.Description = "SystemIP";
            model.Function = SystemIP;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SystemMAC";
            model.Description = "SystemMAC";
            model.Function = SystemMAC;
            MethodList.Add(model);
        }
        private static Feng.Collections.DictionaryEx<string, object> systemvardics = new Feng.Collections.DictionaryEx<string, object>();
      
        public object SystemVar(params object[] args)
        {
            string key = base.GetTextValue(1, args);
            if (args.Length == 3)
            { 
                object value = base.GetArgIndex(2, args);
                systemvardics.Add(key, value);
                return Feng.Utils.Constants.YES;
            } 
            return systemvardics[key];
        }

        public object SystemTime(params object[] args)
        { 
            return DateTime.Now;
        }


        public object SystemIP(params object[] args)
        {
            return Feng.Net.Base.NetInfo.GetLocalIp();
        }
        public object SystemMAC(params object[] args)
        {
            return Feng.Net.Base.NetInfo.GetMac();
        }

    }
}
