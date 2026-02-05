#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Data
{  
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
 
}