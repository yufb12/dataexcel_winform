using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using Feng.Excel.Interfaces;
namespace Feng.Excel.Collections
{

    [Serializable]
    public class HotKeyList   
    {
        private List<ICell> _tablist = null;

        public HotKeyList()
        {
            _tablist = new List<ICell>();
        }

        public void Add(ICell cell)
        {
            if (!this._tablist.Contains(cell))
            {
                _tablist.Add(cell);
            } 
        }

        public void Remove(ICell cell)
        {
            if (this._tablist.Contains(cell))
            {
                _tablist.Remove(cell);
            } 
        }

        public ICell GetHotKeyCell(Keys data)
        {
            if (this._tablist.Count > 0)
            {
                foreach (ICell cell in this._tablist)
                {
                    if (cell.HotKeyData == data)
                    {
                        return cell;
                    }
                }
            }
            return null;
        }

 
    }
}
