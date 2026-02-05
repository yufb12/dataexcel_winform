using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using Feng.Utils;
using Feng.Data;


namespace Feng.Assistant
{
    public abstract class SysEvents
    {
        public virtual string Name { get; set; }

        public virtual string ToolTip { get; set; }
        public virtual void Excute()
        {

        }

        public virtual void ReadData(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new IO.BufferReader(data.Data))
            {
                Name = reader.ReadIndex(1, Name);
                ToolTip = reader.ReadIndex(2, ToolTip);
                CreatTime = reader.ReadIndex(3, CreatTime);
            }
        }
        private DateTime CreatTime = DateTime.Now;
        public virtual DataStruct Data
        {
            get {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, Name);
                    bw.Write(2, ToolTip);
                    bw.Write(3, CreatTime);
                    data.Data = bw.GetData(); 
                }
                return data;
            }
        }

    }
    public class SysEventsCollection : IList<SysEvents>
    {
        public const string version = "1.0.0.1";
        private string _guid = string.Empty;
        private string _name = string.Empty;
        private string _file = string.Empty;
        private DateTime _time = DateTime.Now;
        private string _password = string.Empty;
        private string _author = string.Empty;
        private string _url = string.Empty;
        private string _remark = string.Empty;

        public void Save(string file)
        {
            _file = file;
            byte[] data = null;
            using (Feng.IO.BufferWriter bw2 = new IO.BufferWriter())
            {
                bw2.Write(1,version);
                bw2.Write(2,_guid);
                bw2.Write(3,_name);
                bw2.Write(4,_file);
                bw2.Write(5,_time);
                bw2.Write(6,_author);
                bw2.Write(7,_url);
                bw2.Write(8,_remark);
                bw2.Write(9,Feng.IO.DEncrypt.Encrypt(_password));
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1,this.list.Count);
                    for (int i = 0; i < this.list.Count; i++)
                    {
                        bw.Write(2,list[i].GetType().FullName);
                        bw.Write(3, list[i].Data);
                    }
                    byte[] buf = bw.GetData();
                    if (!string.IsNullOrWhiteSpace(_password))
                    {
                        buf = Feng.IO.DEncrypt.Encrypt(_password, buf);
                    }
                    bw2.Write(10,buf);
                    bw2.Write(11,new byte[] { });
                }
                data = bw2.GetData();
            }

            if (data != null)
            {
                IO.FileHelper.WriteAllBytes(_file, data);
            }
        }
        public void Read(string file)
        {
            Read(System.IO.File.ReadAllBytes(file));
        }
        private void Read(byte[] data)
        {
            using (Feng.IO.BufferReader read = new IO.BufferReader(data))
            {
                read.ReadCache();
                string ver = read.ReadIndex(1, version);
                _guid = read.ReadIndex(2, _guid);
                _name = read.ReadIndex(3, _name);
                _file = read.ReadIndex(4, _file);
                _time = read.ReadIndex(5, _time);
                _author = read.ReadIndex(6, _author);
                _url = read.ReadIndex(7, _url);
                _remark = read.ReadIndex(8, _remark);
                _password = Feng.IO.DEncrypt.Decrypt(read.ReadIndex(9, _password));
                byte[] buf = read.ReadIndex(10, (byte[])null);
                if (!string.IsNullOrWhiteSpace(_password))
                {
                    buf = Feng.IO.DEncrypt.Decrypt(_password, buf);
                }
                using (Feng.IO.BufferReader read2 = new IO.BufferReader(buf))
                {
                    int count = read2.ReadIndex(1, 0);
                    for (int i = 0; i < count; i++)
                    {
                        string text = read2.ReadIndex(2, string.Empty);
                        SysEvents eve = this.GetType().Assembly.CreateInstance(text) as SysEvents;
                        eve.ReadData(read2.ReadIndex(3, (DataStruct)null));
                        list.Add(eve); 
                    }
                }
            }
        }
        private List<SysEvents> list = new List<SysEvents>();
        public int IndexOf(SysEvents item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, SysEvents item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public SysEvents this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(SysEvents item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(SysEvents item)
        {
            return list.Contains(item);
        }

        public void CopyTo(SysEvents[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SysEvents item)
        {
            return list.Remove(item);
        }

        public IEnumerator<SysEvents> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void AddRange(SysEvents[] array)
        {
            list.AddRange(array);
        }
        public void InsertRange(int index, SysEvents[] array)
        {
            list.InsertRange(index, array);
        }
    }
    //  以下是   SendKeys   的一些特殊键代码表。   
    //键   代码     
    //BACKSPACE   {BACKSPACE}、{BS}   或   {BKSP}     
    //BREAK   {BREAK}     
    //CAPS   LOCK   {CAPSLOCK}     
    //DEL   或   DELETE   {DELETE}   或   {DEL}     
    //DOWN   ARROW（下箭头键）   {DOWN}     
    //END   {END}     
    //ENTER   {ENTER}   或   ~     
    //ESC   {ESC}     
    //HELP   {HELP}     
    //HOME   {HOME}     
    //INS   或   INSERT   {INSERT}   或   {INS}     
    //LEFT   ARROW（左箭头键）   {LEFT}     
    //NUM   LOCK   {NUMLOCK}     
    //PAGE   DOWN   {PGDN}     
    //PAGE   UP   {PGUP}     
    //PRINT   SCREEN   {PRTSC}（保留，以备将来使用）     
    //RIGHT   ARROW（右箭头键）   {RIGHT}     
    //SCROLL   LOCK   {SCROLLLOCK}     
    //TAB   {TAB}     
    //UP   ARROW（上箭头键）   {UP}     
    //F1   {F1}     
    //F2   {F2}     
    //F3   {F3}     
    //F4   {F4}     
    //F5   {F5}     
    //F6   {F6}     
    //F7   {F7}     
    //F8   {F8}     
    //F9   {F9}     
    //F10   {F10}     
    //F11   {F11}     
    //F12   {F12}     
    //F13   {F13}     
    //F14   {F14}     
    //F15   {F15}     
    //F16   {F16}     
    //数字键盘加号   {ADD}     
    //数字键盘减号   {SUBTRACT}     
    //数字键盘乘号   {MULTIPLY}     
    //数字键盘除号   {DIVIDE}     
    public class KeySysEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "KeySysEvents";
            }
        }
        public string Text { get; set; }
        public override void Excute()
        {
            SendKeys.SendWait(Text);
            SendKeys.Flush();

        }
        public override string ToString()
        {
            return string.Format("发送键：{0}", Text);
        }
 
    }
    public class VersonSysEvents : SysEvents
    {
        int i = 0;
        public override string Name
        {
            get
            {
                return "VersonSysEvents";
            }
        }
        public override void Excute()
        {
            i++;
            SendKeys.SendWait("鼠标键盘辅助工具V1.0.1.1 自动发送" + i.ToString());
            SendKeys.Flush();

        }
        public override string ToString()
        {
            return string.Format("版本信息 {0}", "");
        }
 
    }
    public class SpaceEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "SpaceEvents";
            }
        }
        public int Interval { get; set; }

        public override void Excute()
        {
            System.Threading.Thread.Sleep(1000 * Interval);
        }
        public override string ToString()
        {
            return string.Format("间隔：{0}秒", Interval);
        }


        public override Data.DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, base.Data);
                    bw.Write(2, Interval); 
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
                this.Interval = read.ReadIndex(2, 0);  
            }
        }

  
    }


    public class RandomEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "RandomEvents";
            }
        }
        public int RandomType { get; set; }
        public int lastvalue = 0;
        public DateTime lastdatetime = DateTime.Now;

        public string Text
        {
            get
            {
                switch (RandomType)
                {
                    case 1:
                        return lastvalue++.ToString();
                    case 2:
                        return new Random(DateTime.Now.Millisecond).Next(0, 100).ToString();
                    case 3:
                        return DateTime.Now.ToString("HH:mm:ss");
                    case 4:
                        return Guid.NewGuid().ToString().Substring(0, 3);
                    default:
                        return lastvalue++.ToString();
                }
            }
        }
        public override void Excute()
        {
            SendKeys.SendWait(Text);
            SendKeys.Flush();
        }
        public override string ToString()
        {
            return string.Format("随机：{0}", RandomType);
        }


        public override Data.DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, base.Data);
                    bw.Write(2, RandomType);
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
                this.RandomType = read.ReadIndex(2, 0);
            }
        }
 
    }

    public class CurrentTimeEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "CurrentTimeEvents";
            }
        }
        public string Text
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        public override void Excute()
        {
            SendKeys.SendWait(Text);
            SendKeys.Flush();
        }
        public override string ToString()
        {
            return string.Format("当前日期");
        }
 
    }

    public class MouseClickSysEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "MouseClickSysEvents";
            }
        }
        public Point Point { get; set; }
        public Size OffSize { get; set; }
        public override void Excute()
        {
            UnsafeNativeMethods.MouseMove(Point.X, Point.Y);
            System.Threading.Thread.Sleep(300);
            UnsafeNativeMethods.MouseClick(Point.X, Point.Y);
            System.Threading.Thread.Sleep(300);
        }
        public override string ToString()
        {
            return string.Format("单击：{0},OX={1},OY={2}", Point, OffSize.Width, OffSize.Height);
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

                    bw.Write(4,OffSize.Width);
                    bw.Write(5,OffSize.Height);
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
                int x = read.ReadIndex(2, 0);
                int y = read.ReadIndex(3, 0);
                Point = new Point(x, y);
                int width = read.ReadIndex(4, 0);
                int height = read.ReadIndex(5, 0);
                OffSize = new Size(width, height);
            }
        }
     }
    public class MouseClickPointVarSysEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "MouseClickPointVarSysEvents";
            }
        }
        public Point Point { get; set; }
        public override void Excute()
        {
            UnsafeNativeMethods.MouseMove(Point.X, Point.Y);
            System.Threading.Thread.Sleep(300);
            UnsafeNativeMethods.MouseClick(Point.X, Point.Y);
            System.Threading.Thread.Sleep(300);
        }
        public override string ToString()
        {
            return string.Format("单击：{0} ", Point);
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
    public class MouseDoubleClickSysEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "MouseDoubleClickSysEvents";
            }
        }
        public Point Point { get; set; }
        public Size OffSize { get; set; }

        public override void Excute()
        {
            UnsafeNativeMethods.MouseMove(Point.X, Point.Y);
            System.Threading.Thread.Sleep(300);
            UnsafeNativeMethods.MouseClick(Point.X, Point.Y);
            System.Threading.Thread.Sleep(10);
            UnsafeNativeMethods.MouseClick(Point.X, Point.Y);
            System.Threading.Thread.Sleep(10);
        }
        public override string ToString()
        {
            return string.Format("双击：{0},OX={1},OY={2}", Point, OffSize.Width, OffSize.Height);
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

                    bw.Write(4, OffSize.Width);
                    bw.Write(5, OffSize.Height);
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
                int x = read.ReadIndex(2, 0);
                int y = read.ReadIndex(3, 0);
                Point = new Point(x, y);
                int width = read.ReadIndex(4, 0);
                int height = read.ReadIndex(5, 0);
                OffSize = new Size(width, height);
            }
        }
    }

    public class MouseWheelUpSysEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "MouseWheelUpSysEvents";
            }
        }
        public Point Point { get; set; }

        public override void Excute()
        {
            UnsafeNativeMethods.MouseWheelUP(Point.X, Point.Y);
            System.Threading.Thread.Sleep(300);
        }
        public override string ToString()
        {
            return string.Format("{0}", "向上滚动鼠标");
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
    public class ShutDownEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "ShutDownEvents";
            }
        }

        public string Text
        {
            get
            {
                return ("关机");
            }
        }
        string time = "30";
        public override void Excute()
        {
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            var myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = startInfo;
            myProcess.Start();
            myProcess.StandardInput.WriteLine("shutdown -s -t " + time);
        }
        public override string ToString()
        {
            return string.Format("关机");
        }
 
    }
    public class CloseProcessEvents : SysEvents
    {
        public override string Name
        {
            get
            {
                return "CloseProcessEvents";
            }
        }

        public string Text
        {
            get
            {
                return ("关闭进程");
            }
        }
 
        public string ProcessName = string.Empty;
        public override void Excute()
        {
            try
            {

                Process[] procs = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process p in procs)
                {
                    if (p.ProcessName.ToLower() == ProcessName.ToLower())
                    {
                        try
                        { 
                            p.Close();
                        }
                        catch (Exception ex)
                        {
                            Feng.IO.LogHelper.Log(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
  
        }
        public override string ToString()
        {
            return string.Format("关闭进程:" + ProcessName);
        }

        public override Data.DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, base.Data);
                    bw.Write(2, ProcessName); 
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
                ProcessName = read.ReadIndex(2, ProcessName);  
            }
        }
 
    }
}
