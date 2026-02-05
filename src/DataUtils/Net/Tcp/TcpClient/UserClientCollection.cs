using System.Collections.Generic;

namespace Feng.Net.Base
{
    public class UserClientCollection : IList<string>
    {

        private List<string> list = new List<string>();
        private object newlockobj = new object();

        public string this[string name]
        {
            get
            {
                lock (newlockobj)
                {
                    foreach (string uc in list)
                    {
                        if (uc == name)
                        {
                            return uc;
                        }
                    }
                    return null;
                }
            }
        }

        public void Add(string uc)
        {
            lock (newlockobj)
            {
                list.Add(uc);
            }

        }

        public void Clear()
        {
            lock (newlockobj)
            {
                list.Clear();
            }
        }

        public bool Remove(string uc)
        {
            lock (newlockobj)
            {
                return list.Remove(uc);
            }
        }


        #region IEnumerable<string> 成员

        public IEnumerator<string> GetEnumerator()
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

        #region IList<string> 成员

        public int IndexOf(string item)
        {
            lock (newlockobj)
            {
                return list.IndexOf(item);
            }
        }

        public void Insert(int index, string item)
        {
            lock (newlockobj)
            {
                list.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (newlockobj)
            {
                list.RemoveAt(index);
            }
        }

        public string this[int index]
        {
            get
            {
                lock (newlockobj)
                {
                    return list[index];
                }
            }
            set
            {
                lock (newlockobj)
                {
                    list[index] = value;
                }
            }
        }

        #endregion

        #region ICollection<string> 成员


        public bool Contains(string item)
        {
            lock (newlockobj)
            {
                return list.Contains(item);
            }
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            lock (newlockobj)
            {
                list.CopyTo(array, arrayIndex);
            }
        }

        public int Count
        {
            get
            {
                lock (newlockobj)
                {
                    return list.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }


        #endregion
    }
}
