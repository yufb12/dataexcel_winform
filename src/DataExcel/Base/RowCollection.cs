using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel; 
using Feng.Data;
using Feng.Excel.Print;
using Feng.Print;
using Feng.Excel.Args;
using Feng.Excel.Interfaces;
using Feng.Excel.App;

namespace Feng.Excel.Base
{
    [Serializable]
    public class RowCollection : IRowCollection
    {
   
        private Dictionary<int, IRow> _list = new Dictionary<int, IRow>();
        private List<IRow> _items = new List<IRow>();
        [NonSerialized]
        private DataExcel _grid;
        public RowCollection(DataExcel g)
        {
            _grid = g;
        }

        public int IndexOf(IRow item)
        {
            return item.Index;
        }
        public virtual void Refresh()
        {
            _list.Clear();
            _max = 0;
            foreach (IRow col in _items)
            {
                if (col.Index > _max)
                {
                    _max = col.Index;
                }
                _list.Add(col.Index, col);
            }
        }
 
        public void Insert(int index, IRow item)
        {
            BeforeInsertRowCancelArgs e = new BeforeInsertRowCancelArgs(item);
            this.Grid.OnBeforeInsertRow(e);
            if (e.Cancel)
            {
                return;
            }  
            foreach (IRow col in _items)
            {
                if (col.Index >= index)
                {
                    col.Index = col.Index + 1;
                }
            }
            _items.Add(item);
            //自行刷新
            Refresh();
            this.Grid.ReSetMergeCellSize();

            this.Grid.ReFreshFirstDisplayRowIndex();
            this.Grid.EndReFresh();
            this.Grid.OnInsertRow(item);
        }

        public void RemoveAt(int index)
        {
            IRow item = this.Grid.Rows[index];
            BeforeDeleteRowCancelArgs e = new BeforeDeleteRowCancelArgs(item);
            this.Grid.OnBeforeDeleteRow(e);
            if (e.Cancel)
            {
                return;
            }
            this.Grid.BeginReFresh();
            _items.Remove(item);
            bool res = _list.Remove(index);
            foreach (IRow col in _items)
            {
                if (col.Index >= index)
                {
                    col.Index = col.Index - 1;
                }
            }
        } 
        public IRow this[int index]
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

        public void Add(IRow item)
        {
            if (this._list.ContainsKey(item.Index))
            {
                //Feng.Utils.TraceHelper.DebuggerBreak();
                return;
            }
            BeforeInsertRowCancelArgs e = new BeforeInsertRowCancelArgs(item);
            this.Grid.OnBeforeInsertRow(e);
            if (e.Cancel)
            {
                return;
            } 
            if (item.Index > _max)
            {
                _max = item.Index;
            }
            if (_max == 99)
            {

            }
            _items.Add(item);
            this._list.Add(item.Index, item);   
        }

        public void Clear()
        {
            foreach (KeyValuePair<int, IRow> key in _list)
            {
                key.Value.Clear();
            }
            _list.Clear();
            _items.Clear();
            _max = 0;
            _MaxHasValueIndex = 0;
        }

        public virtual bool Contains(IRow item)
        {
            return _list.ContainsKey(item.Index);
        }

        public virtual bool Contains(int item)
        {
            return _list.ContainsKey(item);
        }

        public void CopyTo(IRow[] array, int arrayIndex)
        {
            this._list.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(IRow item)
        {
            this.RemoveAt(item.Index);
            return true;
        }

        public IEnumerator<IRow> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._items.GetEnumerator();
        }


        #region IDataExcelGrid 成员

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        #endregion

        #region ISave 成员

        public void Save(IStream stream)
        {

        }

        #endregion

        #region IPrint 成员

        public virtual bool Print(PrintArgs e)
        {
            Page page = e.CurrentPage as Page;
            System.Drawing.Graphics g = e.PrintPageEventArgs.Graphics;
            int rindex = page.StartRowIndex;

            while (rindex <= page.EndRowIndex)
            {
                if (this._list.ContainsKey(rindex))
                {
                    IRow row = this._list[rindex];
                    row.Print(e);
                    if (page.Handled)
                    {
                        return true;
                    }
                    e.CurrentLocation = new System.Drawing.Point(e.PrintPageEventArgs.MarginBounds.Left, 
                        e.CurrentLocation.Y + row.Height);
                }
                else
                {
                    e.CurrentLocation = new System.Drawing.Point(e.PrintPageEventArgs.MarginBounds.Left,
                        e.CurrentLocation.Y + this.Grid.DefaultRowHeight);
                }
                rindex = rindex + 1;
                e.BeginRowIndex = rindex;
            }
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
                        foreach (KeyValuePair<int, IRow> key in _list)
                        {
                            if (key.Value != null)
                            {
                                DataStruct dataStruct = key.Value.Data;
                                if (dataStruct != null)
                                {
                                    datastructs.Add(dataStruct);
                                }
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
 
        public void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }
 
        public void ReadDataStruct(DataStruct data)
        {
            int count = 0;
            try
            {
                using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
                {
                    this._max = stream.ReadIndex(1, this._max);
                    this._MaxHasValueIndex = stream.ReadIndex(2, this._MaxHasValueIndex);
                    count = stream.ReadIndex(3, count);
                    DataStructCollection datastructs = null;
                    datastructs = stream.ReadIndex(4, datastructs);
                    int maxrow = 1;
                    foreach (DataStruct datarow in datastructs)
                    {
                        IRow row = null;
                        if (!string.IsNullOrEmpty(datarow.DllName))
                        {
                            row = DataExcel.CreateInatance<IRow>(datarow.DllName, datarow.FullName,
                                datarow.AessemlyDownLoadUrl, this._grid, new object[] { this });
                        }
                        else
                        {
                            row = this.Grid.ClassFactory.CreateDefaultRow(this._grid);
                        }
                        row.ReadDataStruct(datarow);
                        if (row.RowHasValue)
                        {
                            if (maxrow < row.Index)
                            {
                                maxrow = row.Index;
                            }
                        }
                    }
                    this._max = maxrow;
                    this.MaxHasValueIndex = maxrow;
                }
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Rowes", "Read", ex);
            }
        }

        public void Sort()
        {
            this._items.Sort(Compare);
        }
        public int Compare(IRow a,IRow b)
        {
            if (a == b)
                return 0;
            if (a == null)
                return 0;
            if (a.Index > b.Index)
            {
                return 1;
            }
            if (a.Index < b.Index)
            {
                return -1;
            }
            return 0;
        }
        #endregion


    }
}
