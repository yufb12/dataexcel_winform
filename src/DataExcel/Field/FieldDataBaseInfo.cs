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
    public class FieldDataBaseInfo
    {
        public FieldDataBaseInfo()
        {

        }
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
 
}