using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public class TreeNodeEx : TreeNode
    {
        public TreeNodeEx() 
        {

        }
        public TreeNodeEx(string text)
            : base(text)
        {

        }
        public string Category { get; set; }
        public string Key { get; set; }
        public string SValue { get; set; }
        public string SValue1 { get; set; }
        public string SValue2 { get; set; }
        public string SValue3 { get; set; }
        public string SValue4 { get; set; }
        public int IValue { get; set; }
        public int IValue1 { get; set; }
        public int IValue2 { get; set; }
        public int IValue3 { get; set; }
        public int IValue4 { get; set; }

    }

}
