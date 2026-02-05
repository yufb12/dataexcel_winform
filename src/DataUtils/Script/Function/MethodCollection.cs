using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.Method
{
    [Serializable]
    public class MethodCollection : IMethodCollection
    {
        private List<IMethod> list = new List<IMethod>();

        public MethodCollection()
        {
        }

        private bool hasMethod(IMethod mthodes)
        {
            foreach (IMethod item in list)
            {
                if (item.Name == mthodes.Name)
                {
                    return true;
                }
            }
            return false;
        }
        #region IList<IMothod> 成员

        public int IndexOf(IMethod item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, IMethod item)
        {
            bool res = hasMethod(item);
            if (res)
            {
                return;
            }
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public IMethod this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                list[index] = value;
            }
        }


        #endregion

        #region ICollection<IMothod> 成员

        public void Add(IMethod item)
        {
            bool res = hasMethod(item);
            if (res)
            {
                return;
            }
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(IMethod item)
        {
            return list.Contains(item);
        }

        public void CopyTo(IMethod[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(IMethod item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<IMothod> 成员

        public IEnumerator<IMethod> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region ICol<IMothod> 成员
        public void AddRange(params IMethod[] ts)
        {
            foreach (IMethod c in ts)
            {
                this.Add(c);
            }
        }

        #endregion



        #region IRunMethod 成员

        public object RunMethod(string methodname, ref bool hesMethod, params object[] args)
        {
            foreach (IMethod method in list)
            {
                if (method.Contains(methodname))
                {
                    hesMethod = true;
                    return method.RunFunction(methodname, args);
                }
            }
            throw new Exception("函数" + methodname + "不存在");
        }

        #endregion
    }
}
