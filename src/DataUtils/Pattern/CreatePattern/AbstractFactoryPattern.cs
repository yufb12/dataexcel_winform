using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{ 
    /// <summary>
    /// 创建模式
    /// 抽象工厂
    /// 特点：配置文件创建
    /// 
    /// </summary>
    public class AbstractFactoryPattern : Pattern
    {
        private AbstractFactoryPattern()
        {

        }

        public override void Test(string text)
        {
            OutputAbsFactory.Instance.WriteLine(text);
        }
    }


    public class OutputAbsFactory : IOutput 
    {
        private static IOutput _output = null;
        public static IOutput Instance
        {
            get {
                Assembly ass = Assembly.GetExecutingAssembly();
                object value = ass.CreateInstance(typeof(DebugViewOutput).FullName);
                return value as IOutput;
            }
        }

        public void WriteLine(string text)
        {
            Instance.WriteLine("SingletonPattern Output:" + DateTime.Now.ToString() + ":" + text);
        }
    }
}
