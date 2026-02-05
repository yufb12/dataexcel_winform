using System;
using System.Collections.Generic;
namespace Feng.DevTools.Code
{
    /// <summary>
    /// 实体类SysCodes 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SysCodesCollection : IList<SysCodes>
    {
        private List<SysCodes> list = new List<SysCodes>();
        public string TableName { get; set; }
        public SysCodesCollection()
        { }
        SysCodesCollection li = null;
        public SysCodesCollection Keys
        {
            get
            {
                if (li == null)
                {
                    li = new SysCodesCollection();
                    foreach (SysCodes model in list)
                    {
                        if (model.InPrimaryKey)
                        {
                            li.Add(model);
                        }
                    }
                }
                return li;
            }
        }

        public SysCodes Caption {
            get {
                SysCodes model = null;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].IsCaption)
                    {
                        model= list[i];
                    }
                    if (model == null)
                    {
                        if (!list[i].InPrimaryKey)
                        {
                            model = list[i];
                        }
                    }
                }
                return model;
            }
        }

        public int IndexOf(SysCodes item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, SysCodes item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public SysCodes this[int index]
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

        public void Add(SysCodes item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(SysCodes item)
        {
            return list.Contains(item);
        }

        public void CopyTo(SysCodes[] array, int arrayIndex)
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

        public bool Remove(SysCodes item)
        {
            return list.Remove(item);
        }

        public IEnumerator<SysCodes> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public string GetColumnName()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (SysCodes m in this.list)
            {
                sb.Append(m.ColumnName + ",");
            }
            return sb.ToString();
        }

        public void Append(List<SysCodes> list)
        {
            foreach (SysCodes m in list)
            {
                this.list.Add(m);
            }
        }
        public void Append(SysCodes[] list)
        {
            foreach (SysCodes m in list)
            {
                this.list.Add(m);
            }
        }
        public SysCodes[] ToArray()
        {
            return this.list.ToArray();
        }

    }
}

