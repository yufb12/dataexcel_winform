using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Feng.Utils
{
    public static class TraceHelper
    {
        static TraceHelper()
        {
        }

        private static int _index = 0;
        public static int Index
        {
            get { return _index++; }
        }
 
 
 
        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteTrace(string category, string type,string name, byte[] buffer)
        {
            StringBuilder mes = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            sb.Append("new byte[]{");
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i] + ",");
                mes.Append(i + "=[" + buffer[i] + "];");
            }
            sb.Append("}");
            mes.AppendLine(sb.ToString());

            WriteTrace(category, type, name,true, mes.ToString());
        }
 
        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteTrace(string category, string type, string name,bool show, object obj)
        { 

            WriteTrace(category, type, name, show, Feng.Utils.ConvertHelper.ToString(obj));
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteTrace(string category, string type, string name, string txt)
        { 
            WriteTrace(category, type, name, true, txt);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteTrace(string category, string type, string name, string txt,object instance)
        {
            string ins = "";
            if (instance == null)
            {
                ins = "INS:NULL";
            }
            if (instance != null)
            {
                ins = "INS:"+ instance.GetHashCode();
            }
            WriteTrace(category, type, name, true, ins+ " " + txt);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void DebuggerBreak()
        {
            return;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                 System.Diagnostics.Debugger.Break();
            }
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteTrace(string category,string type, string name, Exception ex)
        {
            string mes = ex.Message + "\r\n" + ex.StackTrace;
            WriteTrace(category,type, name,true, mes);
            Feng.Utils.TraceHelper.DebuggerBreak();
        }


        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteTrace(string category
            , string type, string name, bool result, string successtext,string failtext)
        {
            if (result)
            {
                WriteTrace(category
                   , type, name,true, successtext);
            }
            else
            {
                WriteTrace(category
                   , type, name, true, failtext);
            }
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteTrace(string category
            , string type, string name,bool show, string contents)
        {
            try
            {
            
                if (!show)
                    return;
                category = string.IsNullOrEmpty(category) ? string.Empty : category;
                type = string.IsNullOrEmpty(type) ? string.Empty : type;
                name = string.IsNullOrEmpty(name) ? string.Empty : name;
                contents = string.IsNullOrEmpty(contents) ? string.Empty : contents;

                int len = CharWith(category);
                category = category.PadRight(10 - len, '_');
                len = CharWith(type);
                type = type.PadRight(20 - len, '_');
                len = CharWith(name);
                name = name.PadRight(20 - len, '_');
                string index = TraceHelper.Index.ToString().PadLeft(6, '0');
                string text = (""
                        + "【" + DateTime.Now.ToString("HH:mm:ss:fff") + "】"
                        + "【" + index + "】"
                        + "【" + category + "】"
                        + "【" + type + "】"
                        + "名称【" + name + "】"
                        + "内容【" + contents + "】");
                //System.Diagnostics.Debug.WriteLine(text);
                Console.WriteLine(text);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
            }
          
        }
         


        public static int CharWith(string str)
        {
            if (str == null)
                return 0;
            int len = 0;
            foreach (char c in str)
            {
                if (c > 256)
                {
                    len = len + 1;
                } 
            }
            return len;
        }
         

        private static string CATEGORY = string.Empty;
        private static string TYPE = string.Empty;
        private static string NAME = string.Empty;
        [System.Diagnostics.Conditional("DEBUG")]
        public static void TraceShow(string category
            , string type, string name)
        {
            CATEGORY = category;
            TYPE = type;
            NAME = name;
        }

        public static void WriteStackTrace()
        { 
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            StringBuilder sb = new StringBuilder();
            int count = stackTrace.FrameCount;
            for (int i = 0; i < count; i++)
            {
                StackFrame stackFrame =stackTrace.GetFrame(i);
                string filename = stackFrame.GetFileName();
                sb.AppendFormat("FileName:{0} ", filename);
                string line = stackFrame.GetFileLineNumber().ToString();
                sb.AppendFormat("Line:{0} ", line);
                string column = stackFrame.GetFileColumnNumber().ToString();
                sb.AppendFormat("Column:{0} ", column);
                sb.AppendFormat("Method:{0} ", stackFrame.GetMethod());
                sb.AppendLine();
            }
            WriteTrace("StackTrace", string.Empty, string.Empty, true, sb.ToString());
            //string name = 
            //string name1 = stackTrace.GetFrame(1).GetMethod().Name;
            //string name2 = stackTrace.GetFrame(2).GetMethod().Name;
        }
    }
}
