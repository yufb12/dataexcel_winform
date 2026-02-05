using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Memento Pattern 
    /// 备忘录模式
    /// 解释： 存放另外对象的快照
    /// </summary>
    public class MementoPattern : Pattern
    {
        private MementoPattern()
        {

        }
         
        public override void Test(string text)
        {
 
        }

    }
    /// <summary>
    /// 发起人
    /// </summary>
    public class Originator
    {
        public string Contents { get; set; }
        public string State { get; set; }
        private Memento memento = null;
        public Memento Create()
        {
            memento = new Memento();
            return memento;
        }
        public Memento Memto { get { return memento; } }

        public void RestoreMemento()
        {

        }
    }

    /// <summary>
    /// 责任人
    /// </summary>
    public class Catetaker
    {

    }

    /// <summary>
    /// 备忘录
    /// </summary>
    public class Memento
    {

    }
    public interface IWidthInterfave
    {
        
    }
}
