using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;
using Feng.Net.Packets;

namespace Feng.Net.Packets
{
    public class NetResultCache
    {
        private static NetResultCache _Default = null;
        public static NetResultCache Default
        {
            get
            {
                if (_Default == null)
                {
                    _Default = new NetResultCache();
                }
                return _Default;
            }
        }
        private Stack<NetResult> stack = new Stack<NetResult>();
        public NetResult Pop()
        {
            lock (this)
            {
                if (stack.Count > 0)
                {
                    return stack.Pop();
                }
                NetResult item = new NetResult();
                return item;
            } 
        }
        public void Push(NetResult item)
        {
            lock (this)
            {
                if (stack.Count > 300)
                    return;
                item.Clear();
                stack.Push(item);
            }
        }
    }
  
}
