using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Singleton Pattern
    /// 单例模式
    /// 静态构造函数，静态函数
    /// </summary>
    public class SingletonPattern : Pattern
    {
        private SingletonPattern()
        {

        }

        public override void Test(string text)
        {
            DebugViewOutput.Instance.WriteLine(text);
        }
 
        //public void Test(string text)
        //{
        //    DebugViewOutput.Instance.WriteLine(text);
        //}
    }

       

}
