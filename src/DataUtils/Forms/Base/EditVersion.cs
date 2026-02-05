using System;
using System.Collections.Generic;

namespace Feng.Forms.Base
{
    public class EditVersion
    {
        public EditVersion()
        {
        }
        public int EditID { get; set; }
        public DateTime EditTime { get; set; }
        public string EditUserID { get; set; }
        public string EditUserName { get; set; }
        public string EditUrl { get; set; }
        public string EditIndex { get; set; }
        public string EditType { get; set; }
        public string EditValue { get; set; }

    }

    public class EditVersionCollection : IList<EditVersion>
    {
        private List<EditVersion> list = new List<EditVersion>();
        public int IndexOf(EditVersion item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, EditVersion item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public EditVersion this[int index]
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

        public void Add(EditVersion item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(EditVersion item)
        {
            return list.Contains(item);
        }

        public void CopyTo(EditVersion[] array, int arrayIndex)
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

        public bool Remove(EditVersion item)
        {
            return list.Remove(item);
        }

        public IEnumerator<EditVersion> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}