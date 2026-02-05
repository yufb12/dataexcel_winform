using Feng.Net.Interfaces;
using System.Collections.Generic;

namespace Feng.Net.Base
{
    public class ClientProxyCollection : IList<IClientProxy>
    {

        private List<IClientProxy> list = new List<IClientProxy>();
        private object newlockobj = new object();

        public IClientProxy this[string name]
        {
            get
            {
                lock (newlockobj)
                {
                    foreach (IClientProxy uc in list)
                    {
                        if (uc.Name == name)
                        {
                            return uc;
                        }
                    }
                    return null;
                }
            }
        }

        public void Add(IClientProxy uc)
        {
            lock (newlockobj)
            {
                if (!list.Contains(uc))
                {
                    list.Add(uc);
                }
            }

        }

        public void Clear()
        {
            lock (newlockobj)
            {
                list.Clear();
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

        public bool Remove(IClientProxy uc)
        {
            lock (newlockobj)
            {
                return list.Remove(uc);
            }
        }

        public void Remove(string name)
        {
            lock (newlockobj)
            {
                foreach (IClientProxy uc in list)
                {
                    if (uc.Name == name)
                    {
                        list.Remove(uc);
                        return;
                    }
                }
            }
        }

        #region IEnumerable<string> 成员

        public IEnumerator<IClientProxy> GetEnumerator()
        {
            lock (newlockobj)
            {
                return this.list.GetEnumerator();
            }
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            lock (newlockobj)
            {
                return this.list.GetEnumerator();
            }
        }

        #endregion

        #region IList<SocketHelper> 成员

        public int IndexOf(IClientProxy item)
        {
            lock (newlockobj)
            {
                return list.IndexOf(item);
            }
        }

        public void Insert(int index, IClientProxy item)
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

        public IClientProxy this[int index]
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

        #region ICollection<SocketHelper> 成员

        public bool Contains(IClientProxy item)
        {
            lock (newlockobj)
            {
                return list.Contains(item);
            }
        }

        public bool Contains(string name)
        {
            lock (newlockobj)
            {
                foreach (IClientProxy soc in list)
                {
                    if (soc.Name == name)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void CopyTo(IClientProxy[] array, int arrayIndex)
        {
            lock (newlockobj)
            {
                list.CopyTo(array, arrayIndex);
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        public IClientProxy Get(string id)
        {
            lock (newlockobj)
            {
                foreach (IClientProxy uc in list)
                {
                    if (uc.ID == id)
                    {
                        return uc;
                    }
                }
                return null;
            }
        }
    }
}
