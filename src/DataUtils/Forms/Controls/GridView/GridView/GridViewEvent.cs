using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums; 
using Feng.Forms.Events;

namespace Feng.Forms.Controls.GridControl
{ 
    public partial class GridView  
    {
        public virtual void OnBeforeCellInitEdit(BeforeInitEditCancelArgs e)
        {
            if (BeforeCellInitEdit != null)
            {
                BeforeCellInitEdit(this, e);
            }
        }

        public virtual void OnBeforeMoveToNextCell(BeforeMoveFocusCellCancelArgs e, NextCellType nct)
        {
            if (BeforeMoveToNextCell != null)
            {
                BeforeMoveToNextCell(this, e, nct);
            }
        }
        public virtual bool OnGridViewCellClick(MouseEventArgs e, GridViewCell cell)
        {
            if (GridViewCellClick != null)
            {
                return GridViewCellClick(this, e, cell);
            }
            return false;
        }
        public virtual void OnFocusedCellChanged(Feng.Forms.Controls.GridControl.GridViewCell cell)
        {
            this.SelectCells.Clear();
            this.SelectCells.Add(cell);
            this.FocusedCell = cell;
            if (FocusedCellChanged != null)
            {
                FocusedCellChanged(this, cell);
            }
        }
        /// <summary>
        /// OnColumnChanged ChangedReason 触发原因
        /// </summary>
        /// <param name="column"></param>
        /// <param name="Reason">ChangedReason 触发原因</param>
        public virtual void OnColumnChanged(GridViewColumn column, int Reason)
        {
            if (ColumnChanged != null)
            {
                ColumnChanged(this, column, Reason);
            }
        }
        public virtual void OnSizeChanged(EventArgs e)
        {
            if (SizeChanged != null)
            {
                SizeChanged(this, e);
            }
        }
        public virtual void OnCellValueChanged(Feng.Forms.Controls.GridControl.GridViewCell cell)
        { 
            if (CellValueChanged != null)
            {
                CellValueChanged(this, cell);
            }
        }
        public virtual void OnCellEndEdit(Feng.Forms.Controls.GridControl.GridViewCell cell)
        {
            if (CellEndEdit != null)
            {
                CellEndEdit(this, cell);
            } 
        }
 
        public event EventHandler SizeChanged; 
        public event ColumnChangedHandler ColumnChanged;
        public event BeforeCellInitEditHandler BeforeCellInitEdit;
        public event BeforeMoveToNextCellHandler BeforeMoveToNextCell;
        public event GridViewCellClickHandler GridViewCellClick;
        public event CellEventHandler FocusedCellChanged;
        public event CellDoubleClickHandler CellDoubleClick;
        public event CellEventHandler CellValueChanged;
        public event CellEventHandler CellEndEdit;

    }

    public static class ChangedReason
    {
        public const int Add = 101;
        public const int SizeChanged = 105;
        public const int ValueChanged = 106;
        public const int Remove = 201;
        public const int Clear = 202;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="column"></param>
    /// <param name="Reason">ChangedReason 触发原因</param>
    public delegate void ColumnChangedHandler(object sender, GridViewColumn column,int Reason);
    public delegate bool CellDoubleClickHandler(object sender, GridViewCell gridviewcell);
    public delegate void BeforeMoveToNextCellHandler(object sender, BeforeMoveFocusCellCancelArgs e, NextCellType nexttype);
    public delegate void BeforeCellInitEditHandler(object sender, BeforeInitEditCancelArgs e);
    public delegate bool GridViewCellClickHandler(object sender,MouseEventArgs e,GridViewCell cell);
    public class BeforeInitEditCancelArgs : BaseCanceelEventArgs
    {
        public BeforeInitEditCancelArgs(GridViewCell cell)
        {
            _cell = cell;
        }
        private GridViewCell _cell = null;
        public virtual GridViewCell Cell
        {
            get
            {

            return _cell;
        }
            set {
                _cell = value;
            }
        }
    }

    public delegate void CellEventHandler(object sender, GridViewCell cell);
    public class BeforeMoveFocusCellCancelArgs : BaseCanceelEventArgs
    {
        private GridViewCell _cell = null;
        public virtual GridViewCell Cell
        {
            get
            {

                return _cell;
            }
            set
            {
                _cell = value;
            }
        }
        public GridViewCell NextCell { get; set; }
    }
}

