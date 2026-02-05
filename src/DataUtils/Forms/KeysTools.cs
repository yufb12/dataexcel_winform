using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;

namespace Feng.Forms
{
    /// <summary>
    /// 参数：
    ///     User 为点击鼠标或者键盘
    ///     System 关机等事件
    ///     ThirdParty 为作为组件调用时的第三方使用机构
    /// 作用:
    ///     只读组件设置时是不是起作用。
    /// 举例:
    ///     this.SetSelectCellFontStyleBold(Feng.Forms.TriggerModeEnum.User, false);
    ///     如果选中的单元格为只读时，这样设置将无效。
    /// </summary>
    //public enum TriggerModeEnum
    //{
    //    User,
    //    System,
    //    ThirdParty
    //}
    public class KeysTools  
    {
        public static Keys GetCombinKeys(Keys functionkey, Keys key)
        {
            return (functionkey | key);
        }
        public static Keys GetCombinKeys(Keys functionkey1, Keys functionkey2, Keys key)
        {
            return (functionkey1 | functionkey2 | key);
        }
        public static string ToString(Keys key)
        {
            return key.ToString();
        }
        public static string ToString(Keys key1, Keys key2)
        {
            Keys key= (key1 | key2);
            return key.ToString();
        }
        public static string ToString(Keys key1, Keys key2, Keys key3)
        {
            Keys key = (key1 | key2 | key3);
            return key.ToString();
        }
    } 
}
