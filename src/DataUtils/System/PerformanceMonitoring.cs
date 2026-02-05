using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
#if DEBUG
namespace Feng.Utils
{
    public class PerformanceMonitoring  
    {
        private Feng.Collections.DictionaryEx<string, DateTime> dics = new Collections.DictionaryEx<string, DateTime>();
        public void Begin(string key)
        {
            dics[key] = DateTime.Now;
        }

        public TimeSpan End(string key)
        {
            if (dics.ContainsKey(key))
            {
                DateTime time = dics[key];
                return DateTime.Now - time;
            }
            return TimeSpan.MinValue;
        }

    }
}
#endif