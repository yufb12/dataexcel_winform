using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Flyweight Pattern 
    /// 享元模式 共享元对象（细粒度对象）
    /// </summary>
    public class FlyweightPattern : Pattern
    {
        private FlyweightPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
 
        }

    }

    public class UserEditCollection
    {
        private UserEditCollection()
        {
        }

        public Dictionary<string, UserEdit> dics = new Dictionary<string, UserEdit>();
        public UserEdit GetUserEdit(string name)
        {
            UserEdit eidt = null;
            if (dics.ContainsKey(name))
            {
                return dics[name];
            }
            switch (name)
            {
                case "AA":
                    eidt = new UserEditA(); 
                    break;
                case "BB":
                    eidt = new UserEditB(); 
                    break;
                case "CC":
                    eidt = new UserEditC(); 
                    break;
                default:
                    break;
            }
            if (eidt != null)
            {
                dics.Add(name, eidt);
            }
            return eidt;
        }
    }
    public class UserEdit
    {
        public Point Location { get; set; }
        public string Name { get; set; }
        public Color Facade { get; set; }
    }
    public class UserEditA : UserEdit
    {

    }
    public class UserEditB : UserEdit
    {

    }
    public class UserEditC : UserEdit
    {

    }
    public class UserEditD : UserEdit
    {

    }
}
