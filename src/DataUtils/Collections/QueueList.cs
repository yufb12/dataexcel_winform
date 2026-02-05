using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Collections
{
    public class QueueList<K>
    {
        System.Collections.Generic.Queue<K> dics = new Queue<K>();
        public virtual int MaxNum { get { return _maxnum; } }
        private int _maxnum = 50;
        private readonly K emptykey;
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
                return dics.Dequeue();
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
                dics.Enqueue(key);
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
