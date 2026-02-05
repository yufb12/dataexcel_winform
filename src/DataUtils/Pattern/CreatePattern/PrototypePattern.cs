using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Prototype Pattern
    /// 原型
    /// 克隆 方法
    /// </summary> 
    public class PrototypePattern : Pattern
    {
        public PrototypePattern()
        {
            Manage["RED"] = new ProtoTypeColor(1);
            Manage["BLUE"] = new ProtoTypeColor(2);
            Manage["GREEN"] = new ProtoTypeColor(3);

        }
        ProtoTypeColorManage Manage = new ProtoTypeColorManage();
        public override void Test(string text)
        {
            ProtoTypeColor ptc = Manage["BLACK"];
            if (ptc == null)
            {
                ptc = Manage["RED"].Clone();
            }
        }
    }
    public class ProtoTypeColorManage
    {
        public Dictionary<string, ProtoTypeColor> dics = new Dictionary<string, ProtoTypeColor>();
        public ProtoTypeColor this[string key]
        {
            get
            {
                if (dics.ContainsKey(key))
                {
                    return dics[key];
                }
                return null;
            }
            set
            {
                if (dics.ContainsKey(key))
                {
                    dics[key] = value;
                }
                else
                {
                    dics.Add(key, value);
                }
            }
        }
    }
    public class ProtoTypeColor
    {
        public ProtoTypeColor()
        {
        }
        public ProtoTypeColor(int color)
        {
            this.Color = color;
        }
        public int Color = 255;
        public ProtoTypeColor Clone()
        {
            ProtoTypeColor pt = new ProtoTypeColor();
            pt.Color = Color;
            return pt;
        }
    }

}
