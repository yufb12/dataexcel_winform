using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Excel.App;

namespace Feng.Excel.Extend
{
    [Serializable]
    public class ExtendCellCollection : IExtendCellCollection
    {
        private List<IExtendCell> list = new List<IExtendCell>();

        public ExtendCellCollection(DataExcel grid)
        {
            _grid = grid;
        }

        #region IList<clsxieyi> 成员

        public int IndexOf(IExtendCell item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, IExtendCell item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public IExtendCell this[int index]
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

        #region ICollection<clsxieyi> 成员

        public void Add(IExtendCell item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(IExtendCell item)
        {
            return list.Contains(item);
        }

        public void CopyTo(IExtendCell[] array, int arrayIndex)
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

        public virtual bool Remove(IExtendCell item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<clsxieyi> 成员

        public IEnumerator<IExtendCell> GetEnumerator()
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

        #region ICol<clsxieyi> 成员
        public void AddRange(params IExtendCell[] ts)
        {
            foreach (IExtendCell c in ts)
            {
                this.Add(c);
            }
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return _grid; }
        }

        #endregion

        #region IDraw 成员

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            foreach (IExtendCell mc in list)
            {
                mc.OnDraw(this, g);
            }
            return false;
        }

        #endregion

        #region ISetSize 成员

        public void Refresh()
        {
            foreach (IExtendCell mc in list)
            {
                mc.FreshLocation();
            }
        }

        #endregion


        public void Read(DataExcel grid, int version, DataStruct data)
        {
             
        }
        public string Version
        {
            get { return Feng.DataUtlis.SmallVersion.AssemblySecondVersion; }
        }
 
        public string DllName
        {
            get { return string.Empty; }
        }
        [Browsable(false)]
        public string DownLoadUrl
        {
            get { return string.Empty; }
        }

        public DataStruct Data
        {
            get { 
             Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = string.Empty,
                    Name= string.Empty,
                };
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (Feng.Excel.IO.BinaryWriter bw = this.Grid.ClassFactory.CreateBinaryWriter(ms))
                    {
                        bw.Write(1, this.list.Count);
                        bw.Write(this.list.Count);
                        foreach (IBackCell key in list)
                        {
                            bw.Write(key.Data);
                        }
                    }
                    data.Data = ms.ToArray();
                } 
                return data;
            } 
        }


        public void ReadDataStruct(DataStruct data)
        { 
        }
    }
}
