using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class ConsoleFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "ConsoleMethod";
        public const string Function_Description = "控制台方法";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public ConsoleFunctionContainer()
        {
            BaseMethod model = new BaseMethod();
            model.Name = "ConsoleWrite";
            model.Description = "ConsoleWrite";
            model.Function = ConsoleWrite;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DebuggerBreak";
            model.Description = "DebuggerBreak";
            model.Function = DebuggerBreak;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "ConsoleWriteLine";
            model.Description = "ConsoleWriteLine";
            model.Function = ConsoleWriteLine;
            MethodList.Add(model);
        }

        public object ConsoleWrite(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            string value3 = base.GetTextValue(3, args);
            string value4 = base.GetTextValue(4, args);
            string txt = string.Format("{0},{1},{2},{3}", value1, value2, value3, value4);
            Console.WriteLine(txt);
            return Feng.Utils.Constants.TRUE;
        }
        public object ConsoleWriteLine(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            string value3 = base.GetTextValue(3, args);
            string value4 = base.GetTextValue(4, args);
            string txt = string.Format("{0},{1},{2},{3}", value1, value2, value3, value4);
            Console.WriteLine(txt);
            return Feng.Utils.Constants.TRUE;
        }

        public object DebuggerBreak(params object[] args)
        {
            System.Diagnostics.Debugger.Break();
            return Feng.Utils.Constants.TRUE;
        }

    }
}
