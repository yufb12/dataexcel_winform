using Feng.Data;
using Feng.Excel.Interfaces;
using System;
using System.ComponentModel;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellVector : CellBaseEdit
    {
        public CellVector(DataExcel grid)
            : base(grid)
        {
        }
        public override string ShortName { get { return "CellVector"; } set { } }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }
        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
            }
        }
        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
                {
                    bw.Write(1, this.AddressID);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellVector celledit = new CellVector(grid);

            return celledit;
        }
    }

}
