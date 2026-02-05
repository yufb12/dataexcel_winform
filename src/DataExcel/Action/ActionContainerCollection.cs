using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Actions
{
    public class ActionContainerCollection : IList<BaseActionContainer>
    {
        public ActionContainerCollection()
        {

        }
        private List<BaseActionContainer> list = new List<BaseActionContainer>();
        public int IndexOf(BaseActionContainer item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, BaseActionContainer item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public BaseActionContainer this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(BaseActionContainer item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(BaseActionContainer item)
        {
            return list.Contains(item);
        }

        public void CopyTo(BaseActionContainer[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(BaseActionContainer item)
        {
            return list.Remove(item);
        }

        public IEnumerator<BaseActionContainer> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
 

}
