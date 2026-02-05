using System;
using System.Collections.Generic;

namespace Feng.IO
{
    public class FileReadCacheCollection : IDisposable 
    { 
        private Dictionary<string, object> dics = new Dictionary<string, object>();
        ~FileReadCacheCollection()
        {
            dics.Clear();
        }
        public void Add(string key, object value)
        {
            if (dics.ContainsKey(key))
            { 
                return;
            }
            dics.Add(key, value);
        }
        public object Get(string key)
        {
            if (!dics.ContainsKey(key))
            {
                return null;
            }
            return dics[key];
        }
        public void Dispose()
        {
            this.dics.Clear();
        }

        public void Clear()
        {
            this.dics.Clear();
        }
    }
 
}
