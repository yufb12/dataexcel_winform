using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Excel.Interfaces;
namespace Feng.Excel.Drawing
{
    [Serializable]
    public class BrushManage : IBrushManage
    {
        private Dictionary<Color, System.Drawing.Brush> _pens = new Dictionary<Color, System.Drawing.Brush>();
        public BrushManage()
        {

        }

        public void Add(Color key, System.Drawing.Brush value)
        {
            this._pens.Add(key, value);
        }

        public virtual bool ContainsKey(Color key)
        {
            return this._pens.ContainsKey(key);
        }

        public virtual bool Remove(Color key)
        {
            return this._pens.Remove(key);
        }

        public virtual bool TryGetValue(Color key, out System.Drawing.Brush value)
        {
            return this._pens.TryGetValue(key, out value);
        }

        public ICollection<System.Drawing.Brush> Values
        {
            get { return this._pens.Values; }
        }

        public System.Drawing.Brush this[Color key]
        {
            get
            {
                if (this._pens.ContainsKey(key))
                {
                    return this._pens[key];
                }
                return new System.Drawing.SolidBrush(System.Drawing.Color.Red);
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

        public virtual bool Contains(KeyValuePair<Color, System.Drawing.Brush> item)
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

        public virtual bool Remove(KeyValuePair<Color, System.Drawing.Brush> item)
        {
            return this._pens.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<Color, System.Drawing.Brush>> GetEnumerator()
        {
            return this._pens.GetEnumerator();
        }

        public ICollection<Color> Keys
        {
            get { throw new Exception(); }
        }

        public void Add(KeyValuePair<Color, System.Drawing.Brush> item)
        {
            this._pens.Add(item.Key, item.Value);
        }

        public void CopyTo(KeyValuePair<Color, System.Drawing.Brush>[] array, int arrayIndex)
        {
            array.CopyTo(array, arrayIndex);
        }
    }
}
