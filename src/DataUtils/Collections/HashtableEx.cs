using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Collections
{
    public class HashtableEx : System.Collections.Hashtable
    {
        private List<object> list = new List<object>();
        public override object this[object key] 
        {
            get {
                if (this.ContainsKey(key))
                {
                    return base[key];
                }
                return null;
                    
            }
            set
            {
                if (this.ContainsKey(key))
                {
                    base[key] = value;
                    return;
                }
                this.Add(key, value);
            }
        }
        public override void Add(object key, object value)
        {
            list.Add(key);
            base.Add(key, value);
        }
        public override void Remove(object key)
        {
            list.Remove(key);
            base.Remove(key);
        }
        public override void Clear()
        {
            list.Clear();
            base.Clear();
        }
        public object Index(int index)
        {
            if (index < list.Count && index >= 0)
            {
                return base[list[index]];
            }
            return null;
        }
    }
     
}
