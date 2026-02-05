using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Collections
{
    public class DictionaryEx<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new void Add(TKey key, TValue value)
        {
            lock (this)
            {
                if (this.ContainsKey(key))
                {
                    base[key] = value;
                }
                else
                {
                    base.Add(key, value);
                }
            }
        }
        public new TValue this[TKey key]
        {
            get
            {
                if (this.ContainsKey(key))
                {
                    return base[key];
                }
                return default(TValue);
            }
            set
            {
                lock (this)
                {
                    if (this.ContainsKey(key))
                    {
                        base[key] = value;
                    }
                    else
                    {
                        base.Add(key, value);
                    }
                }
            }
        }

        public TValue Get(TKey key)
        {
            if (this.ContainsKey(key))
            {
                return base[key];
            }
            return default(TValue);
        }
    }

}
