using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using Feng.Utils;
using Feng.Data;


namespace Feng.Assistant
{
    public class OnTimeSysEvents : SysEvents
    {
        public Point Point { get; set; }
        public override string Name
        {
            get
            {
                return "OnTimeSysEvents";
            }
        }
        public override void Excute()
        {
            UnsafeNativeMethods.MouseWheelDOWN(Point.X, Point.Y);
            System.Threading.Thread.Sleep(300);
        }

        public override string ToString()
        {
            return string.Format("{0}", "定时任务");
        }

        public override Data.DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, base.Data);
                    bw.Write(2, Point.X);
                    bw.Write(3, Point.Y);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override void ReadData(DataStruct data)
        {
            using (Feng.IO.BufferReader read = new IO.BufferReader(data.Data))
            {
                DataStruct database = read.ReadIndex(1, (DataStruct)null);
                base.ReadData(database);
                int x = read.ReadIndex(2, 0);
                int y = read.ReadIndex(3, 0);
                Point = new Point(x, y);
            }
        }
    }
}
