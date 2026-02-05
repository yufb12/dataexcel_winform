using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace Feng.Collections
{
    public class Cache<K,V>:IDictionary<K,V>
    {
        Dictionary<K, V> dics = new Dictionary<K, V>();
        public int Count { get { return dics.Count; } }

        public ICollection<K> Keys { get { return this.dics.Keys; } }

        public ICollection<V> Values { get { return this.dics.Values; } }

        public bool IsReadOnly { get { return false; } }

        public V this[K key]
        {
            get
            {
                lock (this)
                {
                    return this.dics[key];
                }
            }
            set
            {
                lock (this)
                {
                    this.Add(key, value);
                }
            }
        }

        public V Get(K key,V d)
        {
            lock (this)
            {
                if (dics.ContainsKey(key))
                {
                    return dics[key];
                }
                return d;
            }
        }

        public V Get(K key)
        {
            lock (this)
            {
                if (dics.ContainsKey(key))
                {
                    return dics[key];
                }
                return default(V);
            }
        }
        public void Add(K key, V value)
        {
            lock (this)
            {
                if (dics.ContainsKey(key))
                {
                    dics[key] = value;
                }
                else
                {
                    dics.Add(key, value);
                }
            }
        }

        public void Remove(K key)
        {
            lock (this)
            {
                if (dics.ContainsKey(key))
                {
                    dics.Remove(key);
                }
            }
        }
 
        public virtual void Clear()
        {
            dics.Clear();
        }

        public bool ContainsKey(K key)
        {
            return this.dics.ContainsKey(key);
        }

        bool IDictionary<K, V>.Remove(K key)
        {
            return this.dics.Remove(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            return this.dics.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<K, V> item)
        {
            this.Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return this.dics.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        { 
            
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return this.dics.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return this.dics.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dics.GetEnumerator();
        }
    }
}
