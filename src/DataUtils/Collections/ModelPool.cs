using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Feng.Data;
using Feng.Forms.Interface;

namespace Feng.Collections
{
    public interface IModelPoolFilter
    {
        bool Contains(object model);
    }

    public class ModelPool<T>
    {
        private int CurrentIndex = -1;
        public int Size = 512;
        public T[] Models = null;

        public ModelPool()
        {
        }

        public void Init()
        {
            Models = new T[Size];
        }

        public void Add(T model)
        {
            lock (this)
            {
                CurrentIndex = CurrentIndex + 1;
                if (CurrentIndex >= (Size * 2))
                {
                    CurrentIndex = 0;
                }
                int nindex = CurrentIndex % Size;
                if (nindex >= Models.Length)
                {
                    nindex = 0;
                }
                Models[nindex] = model;
            }

        }

        public delegate bool FilterHandler(T value);

        public List<T> GetList(IModelPoolFilter filter)
        {
            lock (this)
            {
                List<T> list = new List<T>();
          
                int index = this.CurrentIndex - this.Size;
                if (index > 0)
                {
                    for (int i = index; i < this.Size - 1; i++)
                    {
                        T model = this.Models[i + 1];
                        if (filter != null)
                        {
                            if (filter.Contains(model))
                            {
                                list.Add(model);
                            }
                        }
                        else
                        {
                            list.Add(model);
                        }
                    }
                }
                index = this.CurrentIndex % this.Size;
                for (int i = 0; i <= index; i++)
                {
                    T model = this.Models[i];
                    if (filter != null)
                    {
                        if (filter.Contains(model))
                        {
                            list.Add(model);
                        }
                    }
                    else
                    {
                        list.Add(model);
                    }
                }
                return list;
            }
        }

        public List<T> GetFilterList(IModelPoolFilter filter)
        {
            lock (this)
            {
                List<T> list = new List<T>();
                int index = this.CurrentIndex % this.Size;
                for (int i = index; i >= 0; i--)
                {
                    T model = this.Models[i];
                    if (filter.Contains(model))
                    {
                        list.Add(model);
                    }
                    else
                    {
                        return list;
                    }
                }

                index = this.CurrentIndex - this.Size;
                if (index > 0)
                {
                    for (int i = (this.Size - 1 - 1); i >= index; i--)
                    {
                        T model = this.Models[i + 1];
                        if (filter.Contains(model))
                        {
                            list.Add(model);
                        }
                        else
                        {
                            return list;
                        }
                    }
                }
                return list;
            }
        }
    }
}
