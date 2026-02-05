using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Actions
{
    public delegate void BaseActionHandler(object sender, ActionArgs e);

    public class ActionArgs : Feng.Forms.Events.BaseCanceelEventArgs
    {
        private DataExcel _grid = null;
        private string _action = null;
        private ICell _cell = null;
        public ICell Cell
        {
            get
            {
                return _cell;
            }
        }
        public ActionArgs(string action, DataExcel grid, ICell cell)
        {
            _action = action;
            _grid = grid;
            _cell = cell;
        }

        public string Action
        {
            get
            {
                return _action;
            }
        }

        public DataExcel Grid
        {
            get
            {
                return _grid;
            }
        }

        public bool Handle { get; set; }

        public EventArgs Arg { get; set; }

    }

}
