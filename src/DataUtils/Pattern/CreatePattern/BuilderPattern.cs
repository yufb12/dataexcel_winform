using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{ 
    /// <summary>
    /// 创建模式
    /// Builder Pattern
    /// 建造者模式
    /// 
    /// </summary>
    public class BuilderPattern: Pattern
    {
        private BuilderPattern()
        {

        }
        IBuilder Builder {
            get {
                return new ConsoleBuilder();
            }
        } 
        public override void Test(string text)
        {
            Builder.BuilderOutput().WriteLine(text);
        }


    }

    public interface IBuilder
    {
        IOutput BuilderOutput();
        IInput BuilderInput();
    }

    public class ConsoleBuilder : IBuilder
    {
        public IInput BuilderInput()
        {
            return ConsoleInput.Instance;
        }

        public IOutput BuilderOutput()
        {
            return ConsoleOutput.Instance;
        }
    }
 
    public class FileBuilder : IBuilder
    {
        public IInput BuilderInput()
        {
            return FileInput.Instance;
        }

        public IOutput BuilderOutput()
        {
            return FileOutput.Instance;
        }
    }
 
}
