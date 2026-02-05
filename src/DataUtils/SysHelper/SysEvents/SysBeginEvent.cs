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
    public class SysBeginEvent : SysEvents
    {
        public override string Name
        {
            get
            {
                return "SysBeginEvent";
            }
        }
        public int Count { get; set; }
        public override void Excute()
        {
        }
        
        public override string ToString()
        {
            return string.Format("执行：{0}", Count) + "次";
        }

        public override string ToolTip
        {
            get
            {
                return "程序段执行开始";
            }
            set
            {
                base.ToolTip = value;
            }
        }

    }

    public class SysEndEvent : SysEvents
    {
        public override string Name
        {
            get
            {
                return "SysEndEvent";
            }
        }
        public override void Excute()
        {
        }
        public override string ToString()
        {
            return "执行结束";
        }

        public override string ToolTip
        {
            get
            {
                return "程序段执行结束";
            }
            set
            {
                base.ToolTip = value;
            }
        }
    }
}
