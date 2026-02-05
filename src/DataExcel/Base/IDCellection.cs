using System;

using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Collections
{
    [Serializable]
    public class IDCellCollection  
    {
        private Dictionary<String, List<ICell>> _list = new Dictionary<string, List<ICell>>(); 
        
        public IDCellCollection()
        { 

        }
 
        public void Clear()
        {
            this._list.Clear(); 
        }
 
        public virtual bool Contains(string id)
        {
            id = id.ToUpper();
            return _list.ContainsKey(id);
        }
 
        public int Count
        {
            get { return this._list.Count; }
        }
 
        public virtual bool Remove(string item)
        {
            item = item.ToUpper();
            return this._list.Remove(item);
        }
 
        public ICell this[string id]
        {
            get
            {
                id = id.ToUpper();
                List<ICell> cells = GetCells(id);
                if (cells.Count > 0)
                {
                    return cells[0];
                }
                return null;
            }
            set
            {
                if (this._list.ContainsKey(id))
                {
                    List<ICell> cells = this._list[id];
                    cells.Add(value);
                }
                else
                {
                    List<ICell> cells = new List<ICell>();
                    cells.Add(value);
                    _list[value.ID.ToUpper()] = cells;
                }
            }
        }

        public List<ICell> GetCells(string id)
        {
            id = id.ToUpper();
            List<ICell> list = new List<ICell>();
            string key = id + ".";
            foreach (string txt in this._list.Keys)
            {
                if (id==txt)
                { 
                    List<ICell> cells = this._list[txt];
                    list.AddRange(cells.ToArray());
                }
            }

            return list;
        }
        public List<ICell> GetCells()
        {
            List<ICell> list = new List<ICell>();
            foreach (string txt in this._list.Keys)
            {
                List<ICell> cells = this._list[txt];
                list.AddRange(cells.ToArray());
            }

            return list;
        }
    }


}
