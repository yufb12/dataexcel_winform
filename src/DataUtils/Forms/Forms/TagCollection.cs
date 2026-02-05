using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Feng.Drawing;
using Feng.Forms.Interface;

namespace Feng.Forms
{
    public class TagCollection
    {
        public TagCollection()
        {
        }
        private Feng.Collections.DictionaryEx<string, object> dics = new Collections.DictionaryEx<string, object>();
        public Feng.Collections.DictionaryEx<string, object> Items { get { return dics; } }
    }
}
