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
    public class EditInfoItem : IDataStruct, IReadData
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string EidtTime { get; set; }
        public string SaveTimes { get; set; }
        public string Desc { get; set; }

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
                    bw.Write(1, UserID);
                    bw.Write(2, UserName);
                    bw.Write(3, Desc);
                    bw.Write(4, EidtTime);
                    bw.Write(5, SaveTimes); 

                    data.Data = bw.GetData();
                }

                return data;
            }
        }

        public void Read(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new IO.BinaryReader(data.Data))
            {
                this.UserID = bw.ReadIndex(1, UserID);
                this.UserName = bw.ReadIndex(2, UserName);
                this.Desc = bw.ReadIndex(3, Desc);
                this.EidtTime = bw.ReadIndex(4, EidtTime);
                this.SaveTimes = bw.ReadIndex(5, SaveTimes);
            }
        }
    }

    public class EditInfo : IDataStruct,IReadData
    {
        private Feng.Collections.DictionaryEx<string, EditInfoItem> dics = null;
        public Feng.Collections.DictionaryEx<string, EditInfoItem> Dics
        {
            get {
                if (dics == null)
                {
                    dics = new Feng.Collections.DictionaryEx<string, EditInfoItem>();
                }
                return dics;
            } 
        }
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
                if (dics != null)
                {
                    using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                    {
                        bw.Write(1, this.Dics.Count);
                        foreach (var item in this.Dics)
                        {
                            bw.Write(item.Key);
                            bw.Write(item.Value.Data); 
                        }

                        data.Data = bw.GetData();
                    }
                }
                return data;
            }
        }

        public void Read(DataStruct data)
        {
            if (data.Data.Length > 0)
            {
                using (Feng.Excel.IO.BinaryReader bw = new  IO.BinaryReader(data.Data))
                {
                   int count = bw.ReadIndex(1, 0);
                    if (count > 0)
                    {
                        this.Dics.Clear();
                        for (int i = 0; i < count; i++)
                        {
                            string key = bw.ReadString();
                            DataStruct value = bw.ReadDataStruct();
                            EditInfoItem item = new EditInfoItem();
                            item.Read(value);
                            this.Dics[key] = item;
                        }
                    }
                }
            }
        }
    }
}
