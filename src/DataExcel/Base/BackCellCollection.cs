using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Excel.App;

namespace Feng.Excel.Base
{
    [Serializable]
    public class BackCellCollection : IBackCellCollection
    {
        private List<IBackCell> _list = new List<IBackCell>();

        public BackCellCollection(DataExcel grid)
        {
            _grid = grid;
        }

        #region IList<IBackCell> 成员

        public int IndexOf(IBackCell item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, IBackCell item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list[index].Dispose();
            _list.RemoveAt(index);
        }

        public IBackCell this[int index]
        {
            get
            {
                return this._list[index];
            }
            set
            {
                _list[index] = value;
            }
        }


        #endregion

        #region ICollection<IBackCell> 成员

        public void Add(IBackCell item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public virtual bool Contains(IBackCell item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(IBackCell[] array, int arrayIndex)
        {
            this._list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(IBackCell item)
        {
            if (item == null)
                return true;
            item.Dispose();
            return this._list.Remove(item);
        }

        #endregion

        #region IEnumerable<IBackCell> 成员

        public IEnumerator<IBackCell> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        #endregion

        #region ICol<IBackCell> 成员
        public void AddRange(params IBackCell[] ts)
        {
            foreach (IBackCell c in ts)
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
            foreach (IBackCell mc in _list)
            {
                mc.OnDraw(this, g);
            }
            return false;
        }

        #endregion

        #region ISetSize 成员

        public void Refresh()
        {
            foreach (IBackCell mc in _list)
            {
                mc.Refresh();
            }
        }

        #endregion
 
        #region IDataStruct 成员
        [Browsable(false)]
        public DataStruct Data
        {
            get
            {
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
                        bw.Write(1, this._list.Count);
                        DataStructCollection datastructs = new DataStructCollection();
                        foreach (IBackCell key in _list)
                        {
#warning SHOWIDRect
                            if (key != null)
                            {
                                datastructs.Add(key.Data);
                            }
                        }
                        bw.Write(2, datastructs);
                    }
                    data.Data = ms.ToArray();
                } 
                return data;
            }
        }

        public virtual void ReadDataStruct(DataStruct data)
        {
            int count = 0;
            try
            {
                using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
                {
                    Read(stream, out count);
                    DataStructCollection datastructs = null;
                    datastructs = stream.ReadIndex(2, datastructs);
                    foreach (DataStruct datarow in datastructs)
                    {
                        IBackCell item = null;
                        item = this.Grid.ClassFactory.CreateDefaultBackCell(this.Grid);
                        item.ReadDataStruct(datarow);
                        this._list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
        }
        #endregion

        #region IVersion 成员
        [Browsable(false)]
        public string Version
        {
            get { return Feng.DataUtlis.SmallVersion.AssemblySecondVersion; }
        }
 
        #endregion

        #region IAssembly 成员

        public string DllName
        {
            get { return string.Empty; }
        }

        #endregion

        #region IDownLoadUrl 成员
        [Browsable(false)]
        public string DownLoadUrl
        {
            get { return string.Empty; }
        }

        #endregion
 
        #region IRead 成员

        public void Read(Feng.Excel.IO.BinaryReader stream, out  int count)
        {
            count = 0;
            count = stream.ReadIndex(1, this._list.Count);
        }

        public void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        #endregion

    }
}
