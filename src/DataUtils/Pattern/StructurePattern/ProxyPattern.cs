using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Proxy Pattern 
    /// 代理模式
    /// </summary>
    public class ProxyPattern : Pattern
    {
        private ProxyPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            ActionProxy action = new ActionProxy();
            action.Do();
        }

    }

    public interface IAction
    {
        void Do();
    }

    public class Action : IAction
    {

        public void Do()
        { 
        }
        public void ReadInfo(byte[] date)
        {

        }
    }

    public class ActionProxy : IAction 
    {
        Action action = null;
        private byte[] data = null;
        public ActionProxy()
        {

        }
        public void Do()
        {
            action = new Action();
            action.ReadInfo(data);
            action.Do();
        }
    }
}
