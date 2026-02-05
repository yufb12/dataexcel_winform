using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Excel.Interfaces;
using Feng.Excel.Collections;
using Feng.Forms.Interface;
using Feng.Data;

namespace Feng.Excel.Extend
{
    public class DisplayArea: IDataStruct,IReadData
    {
        public SelectCellCollection Area { get; set; }
        public virtual DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = string.Empty,
                    Name = string.Empty,
                };

                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    bw.Write(1, this.Area.BeginCell.Row.Index);
                    bw.Write(2, this.Area.BeginCell.Column.Index);
                    bw.Write(3, this.Area.EndCell.Row.Index);
                    bw.Write(4, this.Area.EndCell.Column.Index);

                    data.Data = bw.GetData();
                }

                return data;
            }
        }

        public void Read(DataStruct data)
        {

        }
    }
}
