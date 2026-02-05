using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Excel.Interfaces;
namespace Feng.Excel.Styles
{
    [Serializable]
    public class PenManage : IPenManage 
    {
        private Dictionary<Color, System.Drawing.Pen> pens = new Dictionary<Color, System.Drawing.Pen>();
        public PenManage()
        {

        }
 
        public void Add(Color key, System.Drawing.Pen value)
        {
            this.pens.Add(key, value);
        }

        public virtual bool ContainsKey(Color key)
        {
            return this.pens.ContainsKey(key);
        }

        public virtual bool Remove(Color key)
        {
            return this.pens.Remove(key);
        }

        public virtual bool TryGetValue(Color key, out System.Drawing.Pen value)
        {
            return this.pens.TryGetValue(key, out value);
        }

        public ICollection<System.Drawing.Pen> Values
        {
            get { return this.pens.Values; }
        }

        public System.Drawing.Pen this[Color key]
        {
            get
            {
                if (this.pens.ContainsKey(key))
                {
                    return this.pens[key];
                }
                return new System.Drawing.Pen(key);
            }
            set
            {
                this.pens[key] = value;
            }
        }

        public void Clear()
        {
            this.pens.Clear();
        }

        public virtual bool Contains(KeyValuePair<Color, System.Drawing.Pen> item)
        {
            return this.pens.ContainsKey(item.Key);
        }

        public int Count
        {
            get { return this.pens.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(KeyValuePair<Color, System.Drawing.Pen> item)
        {
            return this.pens.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<Color, System.Drawing.Pen>> GetEnumerator()
        {
            return this.pens.GetEnumerator();
        }
 
        public ICollection<Color> Keys
        {
            get { throw new Exception(); }
        }

        public void Add(KeyValuePair<Color, System.Drawing.Pen> item)
        {
            this.pens.Add(item.Key, item.Value);
        }

        public void CopyTo(KeyValuePair<Color, System.Drawing.Pen>[] array, int arrayIndex)
        {
            array.CopyTo(array, arrayIndex);
        } 
    }
}
