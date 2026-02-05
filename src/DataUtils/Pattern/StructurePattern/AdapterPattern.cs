using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// 结构模式
    /// AdapterPattern
    /// 适配器
    /// </summary>
    public class AdapterClassPattern : Pattern
    {
        private AdapterClassPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            LogWrite.Instance.WriteLog(User, text);
        }

    }

    public interface IWrite
    {
        void WriteLog(string user, string text);
    }

    public class LogWrite : IWrite
    {
        public LogWrite()
        {

        }
        private static LogWrite instance = null;
        public static LogWrite Instance
        {
            get
            {
                if (instance == null)
                {
                    //read XML 
                    // if xml==console
                    instance = new ConsoleLogOutPut();
                    // if xml==file
                    instance = new FileLogOutPut();

                }
                return instance;
            }
        }
        public virtual void WriteLog(string user, string text)
        {

        }
    }
     
    public class ConsoleLogOutPut : LogWrite, IOutput
    {
        public override void WriteLog(string user, string text)
        {
            string log = user + ":" + text;
            WriteLine(log);
        }
        public void WriteLine(string text)
        {
            ConsoleOutput.Instance.WriteLine(text);
        }
    }

    public class FileLogOutPut : LogWrite, IOutput
    {
        public override void WriteLog(string user, string text)
        {
            string log = user + ":" + text;
            WriteLine(log);
        }
        public void WriteLine(string text)
        {
            FileOutput.Instance.WriteLine(text);
        }
    }

    public class LogAdapterWrite : IWrite
    {
        IOutput fileoutput = FileOutput.Instance;
        IOutput consoleoutput = ConsoleOutput.Instance;
        private LogAdapterWrite(IOutput fileout, IOutput consoleout)
        {

        }
        private static LogAdapterWrite instance = null;
        public static LogAdapterWrite Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogAdapterWrite(FileOutput.Instance, ConsoleOutput.Instance);
                }
                return instance;
            }
        }
        public virtual void WriteLog(string user, string text)
        {
            string log = user + ":" + text;
            fileoutput.WriteLine(log);
            consoleoutput.WriteLine(log);
        }
    }

    public class AdapterObjectPattern : Pattern
    {
        private AdapterObjectPattern()
        {

        }
        public string User = string.Empty;
        public override void Test(string text)
        {
            LogAdapterWrite.Instance.WriteLog(User, text);
        }
    }



}
