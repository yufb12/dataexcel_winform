using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Forms.Interface;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Collections
{
    [Serializable]
    public class SelectRowCollection : IList<IRow>, ICollection<IRow>, IEnumerable<IRow>, IAddrangle<IRow>
    {
        public SelectRowCollection()
        {

        }
        public List<IRow> list = new List<IRow>();

        #region 用户属性

        #endregion
  

        #region IList<IRow> 成员

        public int IndexOf(IRow item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, IRow item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public IRow this[int index]
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

        #region ICollection<IRow> 成员

        public void Add(IRow item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(IRow item)
        {
            return list.Contains(item);
        }

        public void CopyTo(IRow[] array, int arrayIndex)
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

        public virtual bool Remove(IRow item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<IRow> 成员

        public IEnumerator<IRow> GetEnumerator()
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

        #region ICol<IRow> 成员
        public void AddRange(params IRow[] ts)
        {
            foreach (IRow c in ts)
            {
                this.Add(c);
            }
        }

        #endregion

        public void Sort()
        {
            list.Sort(new Comparison<IRow>(comparison));
        }

        public int comparison(IRow x, IRow y)
        {

            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    if (x.Index > y.Index)
                    {
                        return 0;
                    }
                    else if (x.Index < y.Index)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;

                    }

                }

            }
        }

    }


}
