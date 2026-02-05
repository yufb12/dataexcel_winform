using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Collections
{
    public class ListEx<T> : List<T>
    {
        public T Get(int index)
        {
            if (this.Count > index)
            {
                return this[index];
            }
            return default(T);
        }
        public T GetOrderDesc(int index)
        {
            if (this.Count > index)
            {
                return this[this.Count - index - 1];
            }
            return default(T);
        }
        public T First()
        {
            if (this.Count > 0)
            {
                return this[0];
            }
            return default(T);
        }


        public T Last()
        {
            if (this.Count > 0)
            {
                return this[this.Count - 1];
            }
            return default(T);
        }

        public void AddSingle(T value)
        {
            if (this.Contains(value))
            {
                return;
            }
            this.Add(value);
        }
    }
     
}
