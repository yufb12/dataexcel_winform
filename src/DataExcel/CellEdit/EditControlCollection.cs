using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Feng.Excel.Interfaces;
using System.Collections;

namespace Feng.Excel.Edits
{
    public class EditControlCollection  :IEnumerable<ICellEditControl>
    {
        public EditControlCollection()
        {

        }

        private Dictionary<string, ICellEditControl> dics = new Dictionary<string, ICellEditControl>();
        public bool Add(string fullname, ICellEditControl edit)
        {
            if (!dics.ContainsKey(fullname))
            {
                dics.Add(fullname, edit);
                return true;
            }
            return false;
        }
        public bool Add(ICellEditControl edit)
        {
            string fullname = edit.GetType().FullName;
            if (!dics.ContainsKey(fullname))
            {
                dics.Add(fullname, edit);
                return true;
            }
            return false;
        }
        public bool Delete(string fullname)
        {
            if (!dics.ContainsKey(fullname))
            {
                dics.Remove(fullname);
                return true;
            }
            return false;
        }

        public IEnumerator<ICellEditControl> GetEnumerator()
        {
            return dics.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dics.Values.GetEnumerator();
        }

        public ICellEditControl this[string fullname]
        {
            get
            {
                if (dics.ContainsKey(fullname))
                {
                    return dics[fullname];
                }
                return null;
            }
        }
    }


}
