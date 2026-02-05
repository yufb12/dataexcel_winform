using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace Feng.Data
{

    public class QeuryInofCollection : IList<QeuryInof>
    {
        private List<QeuryInof> list = new List<QeuryInof>();
        public int IndexOf(QeuryInof item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, QeuryInof item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public QeuryInof this[int index]
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

        public void Add(QeuryInof item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(QeuryInof item)
        {
            return list.Contains(item);
        }

        public void CopyTo(QeuryInof[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(QeuryInof item)
        {
            return list.Remove(item);
        }

        public IEnumerator<QeuryInof> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

    public class QeuryInof
    {
        private string _sql = string.Empty;
        public string Sql
        {
            get
            {
                return _sql;
            }
            set
            {
                _sql = value;
            }
        }
        private QeuryInofArgsCollection _Args = null;
        public QeuryInofArgsCollection Args
        {
            get
            {
                return _Args;
            }
            set
            {
                _Args = value;
            }
        }
    }

    public class QeuryInofArgs
    {
        private string _querymode = "=";
        public string QueryMode
        {
            get { return _querymode; }
            set
            {
                _querymode = value;
            }
        }

        private string _arg = string.Empty;
        public string Arg
        {
            get
            {
                return _arg;
            }
            set
            {
                _arg = value;
            }
        }

        public string _value = string.Empty;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }


    public class QeuryInofArgsCollection : IList<QeuryInofArgs>
    {
        private List<QeuryInofArgs> list = new List<QeuryInofArgs>();
        public int IndexOf(QeuryInofArgs item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, QeuryInofArgs item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public QeuryInofArgs this[int index]
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

        public void Add(QeuryInofArgs item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(QeuryInofArgs item)
        {
            return list.Contains(item);
        }

        public void CopyTo(QeuryInofArgs[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(QeuryInofArgs item)
        {
            return list.Remove(item);
        }

        public IEnumerator<QeuryInofArgs> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

}
