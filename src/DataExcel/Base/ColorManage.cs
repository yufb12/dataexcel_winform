using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Drawing
{
    [Serializable]
    public class ColorManage : IDictionary<string, System.Drawing.Color >
    {
        private Dictionary<string, System.Drawing.Color> _pens = new Dictionary<string, System.Drawing.Color>();
        public ColorManage()
        {

        }


        public void Add(string key, System.Drawing.Color value)
        {
            this._pens.Add(key, value);
        }

        public virtual bool ContainsKey(string key)
        {
            return this._pens.ContainsKey(key);
        }

        public virtual bool Remove(string key)
        {
            return this._pens.Remove(key);
        }

        public virtual bool TryGetValue(string key, out System.Drawing.Color value)
        {
            return this._pens.TryGetValue(key, out value);
        }

        public ICollection<System.Drawing.Color> Values
        {
            get { return this._pens.Values; }
        }

        public System.Drawing.Color this[string key]
        {
            get
            {
                if (this._pens.ContainsKey(key))
                {
                    return this._pens[key];
                }
                return System.Drawing.Color.Red;
            }
            set
            {
                this._pens[key] = value;
            }
        }

        public void Clear()
        {
            this._pens.Clear();
        }

        public virtual bool Contains(KeyValuePair<string, System.Drawing.Color> item)
        {
            return this._pens.ContainsKey(item.Key);
        }

        public int Count
        {
            get { return this._pens.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(KeyValuePair<string, System.Drawing.Color> item)
        {
            return this._pens.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, System.Drawing.Color>> GetEnumerator()
        {
            return this._pens.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._pens.GetEnumerator();
        }

        public ICollection<string> Keys
        {
            get { throw new Exception(); }
        }

        public void Add(KeyValuePair<string, System.Drawing.Color> item)
        {
            this._pens.Add(item.Key, item.Value);
        }

        public void CopyTo(KeyValuePair<string, System.Drawing.Color>[] array, int arrayIndex)
        {
            array.CopyTo(array, arrayIndex);
        }
    }
}
