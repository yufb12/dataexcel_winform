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
    public class TabList
    {
        private List<ICell> _tablist = null;

        public TabList()
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

        public ICell First
        {
            get
            {
                if (this._tablist.Count > 0)
                {
                    return this._tablist[0];
                }
                return null;
            }
        }

        #region IComparable 成员

        public int CompareTo(ICell f, ICell e)
        {
            if (f.TabIndex > e.TabIndex)
            {
                return 1;
            }
            if (e.TabIndex > f.TabIndex)
            {
                return -1;
            }
            if (e.TabIndex == f.TabIndex)
            {
                if (f.Row.Index > e.Row.Index)
                {
                    return 1;
                }
                else if (f.Row.Index < e.Row.Index)
                {
                    return -1;
                }
                else if (f.Row.Index == e.Row.Index)
                {
                    if (f.Column.Index > e.Column.Index)
                    {
                        return 1;
                    }
                    else if (f.Column.Index < e.Column.Index)
                    {
                        return -1;
                    }
                    else if (f.Column.Index == e.Column.Index)
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }

        public ICell Next(ICell cell)
        {
            int tabindex = cell.TabIndex;
            tabindex = tabindex + 1;
            foreach (ICell tcell in this._tablist)
            {
                if (tcell.TabIndex == tabindex)
                {
                    return tcell;
                }
            }
            tabindex = 1;
            foreach (ICell tcell in this._tablist)
            {
                if (tcell.TabIndex == tabindex)
                {
                    return tcell;
                }
            }
            return null;
        }

        public ICell Prev(ICell cell)
        {
            int tabindex = cell.TabIndex;
            tabindex = tabindex - 1;
            foreach (ICell tcell in this._tablist)
            {
                if (tcell.TabIndex == tabindex)
                {
                    return tcell;
                }
            }
            tabindex = 1;
            foreach (ICell tcell in this._tablist)
            {
                if (tcell.TabIndex == tabindex)
                {
                    return tcell;
                }
            }
            return null;
        }

        #endregion
    }
}
