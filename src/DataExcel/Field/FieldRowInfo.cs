#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Feng.Excel.Interfaces;
using Feng.Excel.Data;

namespace Feng.Excel.Data
{ 
     

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

  
}