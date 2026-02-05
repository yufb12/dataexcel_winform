using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using Feng.Print;
using Feng.Data;
using Feng.Excel.Args;
using Feng.Excel.Interfaces;
using Feng.Excel.App;

namespace Feng.Excel.Base
{
    [Serializable]
    public class ColumnCollection : IColumnCollection
    {
        private Dictionary<int, IColumn> _list = new Dictionary<int, IColumn>();
 
        public ColumnCollection(DataExcel grid)
        {
            _grid = grid;
        }

        public int IndexOf(IColumn item)
        {
            return item.Index;
        }
        public void Refresh()
        {
            List<IColumn> list = new List<IColumn>();
            foreach (var col in _list)
            {
                list.Add(col.Value);
            }
            _list.Clear();
            _max = 0;
            foreach (IColumn col in list)
            {
                if (col.Index > _max)
                {
                    _max = col.Index;
                }
                _list.Add(col.Index, col);
            }
        }
 
        public void Insert(int index, IColumn item)
        {
            BeforeInsertColumnCancelArgs e = new BeforeInsertColumnCancelArgs(item);
            this.Grid.OnBeforeInsertColumn(e);
            if (e.Cancel)
            {
                return;
            } 
            this.Grid.BeginReFresh();
            foreach (var col in _list)
            {
                if (col.Value.Index >= index)
                {
                    col.Value.Index = col.Value.Index + 1;
                }
            } 
            Refresh();
            this.Grid.ReSetMergeCellSize();
   
            this.Grid.ReFreshFirstDisplayColumnIndex();
            this.Grid.EndReFresh();
            this.Grid.OnInsertColumn(item);
             
        }
         
        public void RemoveAt(int index)
        {
            IColumn item = this.Grid.Columns[index];
            BeforeDeleteColumnCancelArgs e = new BeforeDeleteColumnCancelArgs(item);
            this.Grid.OnBeforeDeleteColumn(e);
            if (e.Cancel)
            {
                return;
            }  
            this.Grid.BeginReFresh();
            this._list.Remove(index);
            item.Deleted = true;
            foreach (var col in _list)
            {
                if (col.Value.Index >= index)
                {
                    col.Value.Index = col.Value.Index - 1;
                }
            }  
            this.Grid.EndReFresh();
            this.Grid.OnDeleteColumn(item);
        }

        public IColumn this[int index]
        {
            get
            {
                if (this._list.ContainsKey(index))
                {
                    return _list[index];
                }

                return null;
            }
            set
            {
                this.Add(value);
            }
        }

        public void Add(IColumn item)
        {
            BeforeInsertColumnCancelArgs e = new BeforeInsertColumnCancelArgs(item);
            this.Grid.OnBeforeInsertColumn(e);
            if (e.Cancel)
            {
                return;
            }
            this.Grid.BeginReFresh();
            if (item.Index > _max)
            {
                _max = item.Index;
            } 
            this._list.Add(item.Index, item); 
            this.Grid.ReFreshFirstDisplayColumnIndex();
            this.Grid.EndReFresh();
        }

        public void Clear()
        {
            foreach (KeyValuePair<int, IColumn> key in _list)
            {
                key.Value.Clear();
            }
            _list.Clear(); 
            _max = 0;
            _MaxHasValueIndex = 0;
        }

        public virtual bool Contains(IColumn item)
        {
            return _list.ContainsKey(item.Index);
        }

        public virtual bool Contains(int index)
        {
            return _list.ContainsKey(index);
        }

        public void CopyTo(IColumn[] array, int arrayIndex)
        {
            this._list.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(IColumn item)
        {
            this.RemoveAt(item.Index);
            return true;
        }

        public IEnumerator<IColumn> GetEnumerator()
        {
            return this._list.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._list.Values.GetEnumerator();
        }
 
        #region IDataExcelGrid 成员

        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }

            set { this._grid = value; }
        }
        #endregion
 
        #region IPrint 成员

        public virtual bool Print(PrintArgs e)
        {
            return false;
        }

        #endregion

        #region IMax 成员
        private int _max = 0;
        public int Max
        {
            get { return _max; }
        }
        #endregion

        #region IMaxHasValueIndex 成员
        private int _MaxHasValueIndex = 0;
        public int MaxHasValueIndex
        {
            get
            {
                return _MaxHasValueIndex;
            }
            set
            {
                _MaxHasValueIndex = value;
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
                        bw.Write(1, this._max);
                        bw.Write(2, this._MaxHasValueIndex);
                        bw.Write(3, this._list.Count);
                        DataStructCollection datastructs = new DataStructCollection(); 
                        foreach (KeyValuePair<int, IColumn> key in _list)
                        {
                            if (key.Value != null)
                            { 
                                datastructs.Add(key.Value.Data);
                            }
                        }
                        bw.Write(4, datastructs);
                    }
                    data.Data = ms.ToArray();
                }
      
                return data;
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
            this._max = stream.ReadIndex(1, this._max);
            this._MaxHasValueIndex = stream.ReadIndex(2, this._MaxHasValueIndex);
            count = stream.ReadIndex(3, count);
        }

        public void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }
        

        #endregion
 
        public IColumn this[string field]
        {
            get
            {
                foreach (IColumn col in this._list.Values)
                {
                    if (col.ID == field)
                    {
                        return col;
                    }
                }
                return null;
            } 
        }


        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                int count = 0;
                Read(stream, out count);
                DataStructCollection datastructs = null;
                datastructs = stream.ReadIndex(4, datastructs);


                try
                {
                    int maxcolumn = 1;
                    foreach (DataStruct datarow in datastructs)
                    {
                        IColumn column = null;
                        if (!string.IsNullOrEmpty(datarow.FullName))
                        {
                            column = DataExcel.CreateInatance<IColumn>(datarow.DllName, datarow.FullName,
                                datarow.AessemlyDownLoadUrl, this._grid, new object[] { this });
                        }
                        else
                        {
                            column = this.Grid.ClassFactory.CreateDefaultColumn(this._grid);
                        }

                        column.ReadDataStruct(datarow);
                        this._list.Add(column.Index, column);
                        if (maxcolumn < column.Index)
                        {
                            maxcolumn = column.Index;
                        }
                    }
                    this.MaxHasValueIndex = maxcolumn;
                }
                catch (Exception ex)
                {
                    Feng.IO.LogHelper.Log(ex);
                }
            }
        }


    }
}
