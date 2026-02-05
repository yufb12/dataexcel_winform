using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Utils
{
    public class CacheStack<K>
    {
        private static Dictionary<string, CacheStack<K>> dicscache = new Dictionary<string, CacheStack<K>>();
        public static CacheStack<K> GetCacheStack(string key)
        {
            if (!dicscache.ContainsKey(key))
            {
                dicscache.Add(key, new CacheStack<K>());
            }
            return dicscache[key];
        }
        System.Collections.Generic.Stack<K> dics = new Stack<K>();
        public int MaxNum { get { return _maxnum; } }
        private int _maxnum = 50;
        private K emptykey;
        public void Init(int maxnum)
        {
            _maxnum = maxnum;
        }
        public K Pop()
        {
            lock (this)
            {
                if (dics.Count < 1)
                    return emptykey;
                return dics.Pop();
            }
        }
        public bool Empty
        {
            get
            {
                return dics.Count < 1;
            }
        }
        public void Push(K key)
        {
            lock (this)
            {
                if (dics.Count > MaxNum)
                    return;
                dics.Push(key);
            }
        }
    }
 
}
