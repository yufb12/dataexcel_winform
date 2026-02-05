using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{ 
    /// <summary>
    /// Factory Method Pattern
    /// 建造者模式
    /// 通过工厂静态方法创建
    /// 延迟创建 
    /// </summary>
    public class FactoryMethodPattern: Pattern
    {
        private FactoryMethodPattern()
        {
            
        }

        OutPutFactory Facotory {
            get {
                return new ConsoleOutPutFactory();
            }
        }
        public override void Test(string text)
        {
            Facotory.OutPut.WriteLine(text);
        }
    }

    public abstract class OutPutFactory
    {
        public abstract IOutput OutPut { get;   }
    }

    public class FileOutPutFactory : OutPutFactory 
    {
        public override IOutput OutPut
        {
            get
            {
                return ConsoleOutput.Instance;
            } 
        }
    }

    public class ConsoleOutPutFactory : OutPutFactory
    {
        public override IOutput OutPut
        {
            get
            {
                return FileOutput.Instance;
            }
        }
    }
}
