
using Feng.Excel;
using Feng.Excel.Collections;
using Feng.Excel.Interfaces;
using Feng.Net.Extend;
using System.Collections.Generic;

namespace Feng.DataTool.DataProject.Dal
{
    public class DataProjectGridValue
    { 
        public static List<Model.DataProjectGridIDValue> GetIDList(DataExcel tempgrid)
        {
            List<Model.DataProjectGridIDValue> list = new List<Model.DataProjectGridIDValue>();
            IDCellCollection ids = tempgrid.IDCells;
            List<ICell> cells = ids.GetCells(); 
            foreach (ICell cell in cells)
            {
                if (!cell.Visible)
                {
                    continue;
                }
                Model.DataProjectGridIDValue idvalue = new Model.DataProjectGridIDValue()
                {
                    Caption = cell.Caption,
                    ID = cell.ID,
                };
                list.Add(idvalue);
            }
            return list;
        }


        public static List<Model.DataProjectGridIDValue> ReadIDlist(byte[] buf)
        {
            List<Model.DataProjectGridIDValue> list = new List<Model.DataProjectGridIDValue>();
            using (Feng.IO.BufferReader reader = new IO.BufferReader(buf))
            {
                int len = reader.ReadInt32();
                for (int i = 0; i < len; i++)
                {
                    Model.DataProjectGridIDValue didvalue = new Model.DataProjectGridIDValue();
                    didvalue.ID = reader.ReadString();// bw.Write(id.ID);
                    didvalue.Caption = reader.ReadString(); //bw.Write(id.Caption);
                    didvalue.Value = reader.ReadBaseValue();// bw.WriteBaseValue(id.Value);
                    list.Add(didvalue);
                }
            }
            return list;
        }
 
        public static List<Model.DataProjectGridIDValue> GetIDList(List<Model.DataProjectGridIDValue> ids, Data.DataValueCollection dataValues)
        {
            List<Model.DataProjectGridIDValue> list = new List<Model.DataProjectGridIDValue>();
            foreach (Model.DataProjectGridIDValue id in ids)
            {
                Model.DataProjectGridIDValue idv = new Model.DataProjectGridIDValue();
                idv.ID = id.ID;
                idv.Caption = id.Caption;
                foreach (Data.DataValue dataValue in dataValues)
                {
                    if (id.ID.Equals(dataValue.Name))
                    {
                        idv.Value = dataValue.Value;
                    }
                }
                list.Add(idv);
            }
            return list;
        }
        public static void GetIDList(List<Model.DataProjectGridIDValue> ids, Data.DataValueCollection dataValues, List<Model.DataProjectGridIDValue> templist)
        { 
            foreach (Model.DataProjectGridIDValue id in ids)
            {
                Model.DataProjectGridIDValue idv = new Model.DataProjectGridIDValue();
                idv.ID = id.ID;
                idv.Caption = id.Caption;
                if (dataValues != null)
                {
                    foreach (Data.DataValue dataValue in dataValues)
                    {
                        if (id.ID.Equals(dataValue.Name))
                        {
                            idv.Value = dataValue.Value;
                        }
                    }
                }
                templist.Add(idv);
            } 
        } 

        public static List<Model.DataProjectGridIDValue> GetIDList(List<Model.DataProjectGridIDValue> ids, DataExcel orggrid)
        {
            List<Model.DataProjectGridIDValue> list = new List<Model.DataProjectGridIDValue>();
            foreach (Model.DataProjectGridIDValue id in ids)
            {
                Model.DataProjectGridIDValue idv = new Model.DataProjectGridIDValue();
                idv.ID = id.ID;
                idv.Caption= id.Caption;
                ICell cell = orggrid.GetCellByID(id.ID);
                if (cell != null)
                {
                    idv.Value = cell.Value;
                } 
                list.Add(idv);
            }
            return list;
        }
    }

    public class DataProjectGridFileValue
    {
        public DataProjectGridFileValue()
        {
        }
        public List<DataProjectGridIDValue> Items { get; set; }

    }

    public class DataProjectGridIDValue
    {
        public DataProjectGridIDValue()
        {
        }
        public string ID { get; set; }
        public string Caption { get; set; }
        public object Value { get; set; }

    }
}
