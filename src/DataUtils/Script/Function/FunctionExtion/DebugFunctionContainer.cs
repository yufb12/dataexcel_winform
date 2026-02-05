//using Feng.Script.CBEexpress;
//using Feng.Script.Method;
//using System;
//using System.Diagnostics;
//using System.Text;

//namespace Feng.Script.FunctionContainer
//{
//    [Serializable]
//    public class DebugFunctionContainer : CBMethodContainer
//    {
//        public const string Function_Category = "DebugFunction";
//        public const string Function_Description = "调试函数";
//        public override string Name
//        {
//            get { return Function_Category; }

//        }
//        public override string Description
//        {
//            get { return Function_Description; }
//        }
//        public DebugFunctionContainer()
//        {
//            BaseMethod model = new BaseMethod();
//            model.Name = "DebugMode";
//            model.Description = "DebugMode";
//            model.Function = DebugMode;
//            model.Eg = "DebugMode(false)";
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "DebugStack";
//            model.Description = "DebugStack";
//            model.Function = DebugStack;
//            MethodList.Add(model); 
//        }
//        public static bool DebugSate = true;
//        /// <summary>
//        /// DebugMode(true);
//        /// </summary>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        public object DebugMode(params object[] args)
//        {
//            bool  res = base.GetBooleanValue(1, args);
//            if (args.Length == 2)
//            {
//                bool value = base.GetBooleanValue(1, args);
//                DebugSate = value;
//                return Feng.Utils.Constants.OK;
//            }  
//            return res;
//        }

//        public object DebugStack(params object[] args)
//        { 
//            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
//            StringBuilder sb = new StringBuilder();
//            int count = stackTrace.FrameCount;
//            for (int i = 0; i < count; i++)
//            {
//                StackFrame stackFrame = stackTrace.GetFrame(i);
//                string filename = stackFrame.GetFileName();
//                sb.AppendFormat("FileName:{0} ", filename);
//                string line = stackFrame.GetFileLineNumber().ToString();
//                sb.AppendFormat("Line:{0} ", line);
//                string column = stackFrame.GetFileColumnNumber().ToString();
//                sb.AppendFormat("Column:{0} ", column);
//                sb.AppendFormat("Method:{0} ", stackFrame.GetMethod());
//                sb.AppendLine();
//            }
//            return sb.ToString();
//        }
 

//    }
//}
