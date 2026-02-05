
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

using System.Data.SqlClient;
using System.Collections.Generic;
using Feng.Data;
using Feng.Forms.Interface;
using Feng.Forms.Base;
using System.Drawing.Printing;
using System.Data;

namespace Feng.IO
{
    public class BufferTools
    {
        public static byte[] GetData(DataTable table)
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.Write(table);
                data = bw.GetData();
            }
            return data;
        }

        public static DataTable GetDataTable(byte[] data)
        {
            DataTable table = null;
            using (Feng.IO.BufferReader reader = new BufferReader(data))
            {
                table = reader.ReadDataTable();
            }
            return table;
        }
    }
}