using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Code.Pattern
{
    public abstract class Pattern
    {
        public abstract void Test(string text);
    }

    public interface IOutput
    {
        void WriteLine(string text);
    }

    public interface IInput
    {
        string ReadLine();
    }

    public class ConsoleInput : IInput
    {
        private ConsoleInput()
        {

        }
        private static ConsoleInput _instance = null;
        public static ConsoleInput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConsoleInput();
                }
                return _instance;
            }
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }


    public class ConsoleOutput : IOutput
    {
        private ConsoleOutput()
        {

        }
        private static ConsoleOutput _instance = null;
        public static ConsoleOutput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConsoleOutput();
                }
                return _instance;
            }
        }
        public void WriteLine(string text)
        {
            Console.WriteLine("Console Output:" + DateTime.Now.ToString() + ":" + text);
        }
    }
 
    public class FileInput : IInput
    {
        private FileInput()
        {

        }
        private static FileInput _instance = null;
        public static FileInput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FileInput();
                }
                return _instance;
            }
        }
        public string ReadLine()
        {
            return System.IO.File.ReadAllText("FileOutput.txt"); 
        }
    } 

    public class FileOutput : IOutput
    {
        private FileOutput()
        {

        }
        private static FileOutput _instance = null;
        public static FileOutput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FileOutput();
                }
                return _instance;
            }
        }
        public void WriteLine(string text)
        {
            System.IO.File.WriteAllText("FileOutput.txt", text); 
        }
    }

    public class DebugViewOutput : IOutput
    {
        private DebugViewOutput()
        {

        }
        private static DebugViewOutput _instance = null;
        public static DebugViewOutput Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DebugViewOutput();
                }
                return _instance;
            }
        }
        public void WriteLine(string text)
        {
            System.Diagnostics.Debug.WriteLine("DebugView Output:" + DateTime.Now.ToString() + ":" + text);
        }
    }
}
