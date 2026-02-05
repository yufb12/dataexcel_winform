using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// 行为模式
    /// Template Method 
    /// 模版方法
    /// 特点：父类调用子类方法
    /// </summary>
    public class TemplateMethodPattern : Pattern
    {
        private TemplateMethodPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            TemplateSaveFileText action = new TemplateSaveFileText();
            action.Execute();
        }

    }

    public abstract class TemplateSaveFile
    {
        public abstract void Open();
        public abstract void Check();
        public abstract void Save();
        public abstract void Close();

        public virtual void Execute()
        {
            Open();
            Check();
            Save();
            Close();
        }
    }

    public class TemplateSaveFileText : TemplateSaveFile 
    {
        public override void Check()
        {
            
        }
        public override void Close()
        {

        }
        public override void Open()
        {
            
        }
        public override void Save()
        {
            
        }
    }

}
