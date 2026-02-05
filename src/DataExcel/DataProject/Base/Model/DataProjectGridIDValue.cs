
using Feng.Excel;
using Feng.Excel.Collections;
using Feng.Excel.Interfaces;
using Feng.Net.Extend;
using System.Collections.Generic;

namespace Feng.DataTool.DataProject.Model
{ 
    public class DataProjectGridIDValue
    {
        public DataProjectGridIDValue()
        {
        }
        public string ID { get; set; }
        public string Caption { get; set; }
        public object Value { get; set; }
        public override string ToString()
        {
            return string.Format("ID={0}, Caption={1}, Value={2}", ID, Caption, Value);
        }
    }
}
