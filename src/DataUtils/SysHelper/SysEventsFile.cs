using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Feng.Assistant
{
    public class SysEventsFile
    {
        public static bool Pause { get; set; }
        public bool OnTime { get; set; }
        public DateTime OnTimeTime { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public int Times { get; set; }
        private SysEventsCollection _Evetns = new SysEventsCollection();
        public SysEventsCollection Events
        {
            get { return _Evetns; }
            set { _Evetns = value; }
        }
        public string Desc { get; set; }
        public DateTime CreateTime { get; set; }
        public byte[] GetData()
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write("Name", Name);
                bw.Write("File", File);
                bw.Write("Times", Times);
                bw.Write("Desc", Desc);
                bw.Write("CreateTime", CreateTime);
                return bw.GetData();
            }
        }
        public void Read(byte[] data)
        {
            using (Feng.IO.BufferReader read = new IO.BufferReader(data))
            {
                Name = read.ReadKey("Name", Name);
                File = read.ReadKey("File", File);
                Times = read.ReadKey("Times", Times);
                Desc = read.ReadKey("Desc", Desc);
                CreateTime = read.ReadKey("CreateTime", CreateTime);
            }
        }
        public bool Finished { get; set; }
        public int RunCount
        {
            get
            {
                return icount;
            }
        }
        int icount = 0;
        public void Start()
        {
            Thread thread = new Thread(StartProc);
            thread.IsBackground = true;
            thread.Start();
        }
        void StartProc()
        {
            icount = 0;
            Finished = false;
            while (true)
            {
                try
                {

                    if (System.Windows.Forms.Control.ModifierKeys == Keys.Control
     || System.Windows.Forms.Control.ModifierKeys == Keys.Shift
     || System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
                    {
                        System.Threading.Thread.Sleep(1000 * 10);
                        continue;
                    }

                    if (Pause)
                    {
                        System.Threading.Thread.Sleep(300);
                        continue;
                    }
                    icount++;
                    foreach (SysEvents p in Events)
                    {
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control
                            || System.Windows.Forms.Control.ModifierKeys == Keys.Shift
                            || System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
                        {
                            System.Threading.Thread.Sleep(1000 * 10);
                            break;
                        }
                        p.Excute();
                        System.Threading.Thread.Sleep(300);
                        if (Pause)
                        {
                            break;
                        }
                    }
                    if (icount > Times)
                    {
                        Finished = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Feng.IO.LogHelper.Log(ex);
                }
                System.Threading.Thread.Sleep(3000);
            }
        }
        public override string ToString()
        {
            if (OnTime)
            {
                return string.Format("名称={0},订时={1}", Name, OnTimeTime.ToString("HH:mm:ss"));
            }
            else
            {
                return string.Format("名称={0},执行次数={1}", Name, Times);
            }
        }
    }

}