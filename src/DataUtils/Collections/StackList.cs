using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Collections
{
    public class StackList<K>
    {
        System.Collections.Generic.Stack<K> dics = new Stack<K>();
        public virtual int MaxNum { get { return _maxnum; } }
        private int _maxnum = 50;
        private K emptykey;
        public virtual void Init(int maxnum)
        {
            _maxnum = maxnum;
        }
        public virtual K Pop()
        {
            lock (this)
            {
                if (dics.Count < 1)
                    return emptykey;
                return dics.Pop();
            }
        }
        public virtual bool Empty
        {
            get
            {
                return dics.Count < 1;
            }
        }
        public virtual void Push(K key)
        {
            lock (this)
            {
                if (dics.Count > MaxNum)
                    return;
                dics.Push(key);
            }
        }
        public virtual K Peek()
        {
            lock (this)
            {
                if (dics.Count < 1)
                    return emptykey;
                return dics.Peek();
            }
        }
    }
}
