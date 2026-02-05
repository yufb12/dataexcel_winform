using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Command Pattern 
    /// 命令模式
    /// </summary>
    public class CommandPattern : Pattern
    {
        private CommandPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            ActionProxy action = new ActionProxy();
            action.Do();
        }

    }

    public class Document
    {
        public string Name { get; set; } 
    }

    public abstract class Command
    {
        public Document Doc { get; set; }
        public abstract void Execute();
    }

    public class DoCommand : Command
    {
        public override void Execute()
        {
            Doc.Name = "";
        }
    }

    public class UnDoCommand : Command
    {
        public override void Execute()
        {
            Doc.Name = "";
        }
    }

    public abstract class CommandInvoker
    {
        DoCommand docommand = null;
        public void Do()
        {
            docommand.Execute();
        }
        UnDoCommand undocommand = null;
        public void UnDo()
        {
            undocommand.Execute();
        }
    }
     
}
