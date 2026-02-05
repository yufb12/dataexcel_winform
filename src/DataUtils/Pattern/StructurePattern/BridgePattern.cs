using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// 桥接模式
    /// </summary>
    public class BridgeClassPattern : Pattern
    {
        private BridgeClassPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
         
        }

    }

    public class JavaConsoleLogOutPut : LogWrite 
    {
        public IOutput Implementor { get; set; }
        public override void WriteLog(string user, string text)
        {
            string log = user + ":" + text;
            WriteLine(log);
        }
        public void WriteLine(string text)
        {
            Implementor.WriteLine(text);
        }
    }

    public class JavaConsoleOutput : IOutput
    {

        public void WriteLine(string text)
        {
         
        }
    }

    public class BradgeObjectPattern : Pattern
    {
        private BradgeObjectPattern()
        {

        }
        public string User = string.Empty;
        public override void Test(string text)
        {
 
        }
    }



}
