using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Feng.Data;
using Feng.Forms.Interface;

namespace Feng.Collections
{
    public class CacheTime : Feng.Collections.Cache<string, object>, IDataStruct, IReadData
    {

        public CacheTime()
        {
        }

        public string GetCacheText(string key)
        {
            object value = Get(key, null);
            CacheValue cacheValue = value as CacheValue;
            if (cacheValue != null)
            {
                if (DateTime.Now > cacheValue.AddTime.AddMinutes(cacheValue.Minute))
                {
                    return string.Empty;
                }
                return cacheValue.TextValue;
            }
            return string.Empty;
        }

        public CacheValue GetCacheValue(string key)
        {
            object value = Get(key, null);
            CacheValue cacheValue = value as CacheValue;
            if (cacheValue != null)
            {
                if (DateTime.Now > cacheValue.AddTime.AddMinutes(cacheValue.Minute))
                {
                    this.Remove(key);
                    return null;
                }
                return cacheValue;
            }
            return null;
        }
        public void AddCache(string key, string value, int minute)
        {
            CacheValue cacheValue = new CacheValue();
            cacheValue.Key = key;
            cacheValue.Minute = minute;
            cacheValue.TextValue = value;
            cacheValue.AddTime = DateTime.Now;
            cacheValue.Value = value;
            this.Add(key, cacheValue);
        }
        public void AddCache(string key, object value, int minute)
        {
            CacheValue cacheValue = new CacheValue();
            cacheValue.Key = key;
            cacheValue.Minute = minute;
            cacheValue.Value = value;
            cacheValue.AddTime = DateTime.Now;
            this.Add(key, cacheValue);
        }
        public void Read(DataStruct data)
        {
            this.Clear();
            using (Feng.IO.BufferReader reader = new IO.BufferReader(data.Data))
            {
                int count = reader.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    string value = reader.ReadString();
                    int minute = reader.ReadInt();
                    this.AddCache(key, value,minute);
                }
            }
        }

        public DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(this.Count);
                    foreach (var item in this)
                    {
                        CacheValue cacheValue = item.Value as CacheValue;
                        if (cacheValue != null)
                        {
                            if (DateTime.Now > cacheValue.AddTime.AddMinutes(cacheValue.Minute))
                            {
                                bw.Write(string.Empty);
                                bw.Write(string.Empty);
                                bw.Write(0);
                                continue;
                            }
                            bw.Write(item.Key);
                            bw.Write(cacheValue.TextValue);
                            bw.Write(cacheValue.Minute);
                            continue;
                        }

                        bw.Write(string.Empty);
                        bw.Write(string.Empty);
                        bw.Write(0);

                    }
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public virtual void Clean()
        {
            lock (this)
            {
                List<string> keys = new List<string>();
                foreach (string item in this.Keys)
                {
                    keys.Add(item);
                } 
                foreach (string item in keys)
                {
                    CacheValue cacheValue = this[item] as CacheValue;
                    if (cacheValue != null)
                    {
                        if (DateTime.Now > cacheValue.AddTime.AddMinutes(cacheValue.Minute))
                        {
                            this.Remove(item);
                        }
                    }
                }
            }
        }

        public object TryGet(string key)
        {
            if (this.ContainsKey(key))
            {
                CacheValue cacheValue = GetCacheValue(key);
                if (cacheValue != null)
                {
                    return cacheValue.Value;
                }

            }
            return null;
        }

        public object TryGet(string key,object value)
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }
            return value;
        }
    }
    public class CacheValue
    {
        public string Key { get; set; }
        public DateTime AddTime { get; set; }
        public int Minute { get; set; }
        public string TextValue { get; set; }
        public DateTime TimeValue { get; set; }
        public object  Value { get; set; }
    }
}
