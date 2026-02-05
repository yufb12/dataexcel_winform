using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;

namespace Feng.Net.Base 
{
    public class IndexObject
    {
        public DateTime SendTime { get; set; }
        public bool IsReturn { get; set; }
        public byte[] Value { get; set; }
        public ushort Index { get; set; }
        private System.Threading.AutoResetEvent _AutoResetEvent = new System.Threading.AutoResetEvent(true);
        public System.Threading.AutoResetEvent AutoResetEvent { get { return _AutoResetEvent; } }
    }
    public class IndexObjectStack
    {
        private static System.Collections.Generic.Stack<IndexObject> stack = new Stack<IndexObject>();
        public static  IndexObject Popup()
        {
            IndexObject obj = stack.Pop();
            if (obj == null)
            {
                obj = new IndexObject();
            }
            return obj;
        }
        public static void Push(IndexObject obj)
        {
            stack.Push(obj);
        }
    }
}
