#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Feng.Excel.Interfaces;

namespace Feng.Excel
{
    public class FieldDataBaseInfo
    {
        private Dictionary<string, FieldTableInfo> dics = new Dictionary<string, FieldTableInfo>();
        public Dictionary<string, FieldTableInfo> Tables
        {
            get
            {
                return dics;
            }
        }
        public FieldTableInfo this[string table]
        {
            get
            {
                if (dics.ContainsKey(table))
                {
                    return dics[table];
                }
                return null;
            }
        }

        public void Add(FieldTableInfo fi)
        {
            if (!dics.ContainsKey(fi.TableName))
            {
                dics.Add(fi.TableName, fi);
            }
        }
    }

    public class FieldTableInfo
    {
        public FieldTableInfo(FieldDataBaseInfo database)
        {
            _database = database;
        }
        private FieldDataBaseInfo _database = null;
        public FieldDataBaseInfo DataBase
        {
            get
            {
                return _database;
            }
        }
        public string TableName { get; set; }
        public FieldRowInfo this[int index]
        {
            get
            {
                if (this.Rows.ContainsKey(index))
                {
                    return this.Rows[index];
                }
                return null;
            }
        }
        public Dictionary<int, FieldRowInfo> _Rows = new Dictionary<int, FieldRowInfo>();
        public Dictionary<int, FieldRowInfo> Rows
        {
            get
            {
                return _Rows;
            }
        }
        public void Add(FieldRowInfo fri)
        {
            if (!Rows.ContainsKey(fri.Index))
            {
                Rows.Add(fri.Index, fri);
            }
        }
    }

    public class FieldRowInfo
    {
        public FieldRowInfo(FieldTableInfo table)
        {
            _Table = table;
        }
        private FieldTableInfo _Table = null;
        public FieldTableInfo Table
        {
            get
            {
                return _Table;
            }
        }
        public int Index { get; set; }
        private Dictionary<string, FieldCellInfo> _Cells = new Dictionary<string, FieldCellInfo>();
        public Dictionary<string, FieldCellInfo> Cells { get {
            return _Cells;
        } }
        public void Add(FieldCellInfo fci)
        {
            if (!Cells.ContainsKey(fci.ColumName))
            {
                Cells.Add(fci.ColumName, fci);
            }
        }
    }

    public class FieldCellInfo
    {
        public FieldCellInfo(FieldRowInfo row)
        {
            _Row = row;
        }
        private FieldRowInfo _Row = null;
        public FieldRowInfo Row
        {
            get
            {
                return _Row;
            }
        }
        public string ColumName { get; set; }
        public ICell Cell { get; set; }
    }
}