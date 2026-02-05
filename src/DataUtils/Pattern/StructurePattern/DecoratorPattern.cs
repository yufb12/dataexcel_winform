using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Decorator Pattern 
    /// 装饰模式
    /// </summary>
    public class DecoratorClassPattern : Pattern
    {
        private DecoratorClassPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            IOutput output = ConsoleOutput.Instance;
            CheckOutPutWrapper checkwrapper = new CheckOutPutWrapper(output);
            DateTimeOutPutWrapper timewrapper = new DateTimeOutPutWrapper(checkwrapper);
            timewrapper.WriteLine(text);
        }

    }

    public abstract class OutPutWrapper : IOutput
    {
        private IOutput Output = null;
        public OutPutWrapper(IOutput output)
        {
            Output = output;
        }
 
        public virtual void WriteLine(string text)
        {
            Output.WriteLine(text);
        }
    }


    public class CheckOutPutWrapper : OutPutWrapper
    {
        public CheckOutPutWrapper(IOutput output)
            : base(output)
        {

        }

        public override void WriteLine(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                base.WriteLine("Over Check:");
                base.WriteLine(text);
            }
        }
    }

    public class DateTimeOutPutWrapper : OutPutWrapper
    {
        public DateTimeOutPutWrapper(IOutput output)
            : base(output)
        {

        }

        public override void WriteLine(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                base.WriteLine("Time:" + DateTime.Now.ToString());
                base.WriteLine(text);
            }
        }
    }
}
