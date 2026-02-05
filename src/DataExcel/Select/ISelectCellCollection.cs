using System;
using System.Collections.Generic;
using System.Drawing;
using Feng.Forms.Interface;
namespace Feng.Excel.Interfaces
{
    public interface ISelectCellCollection : IRefresh, IBounds, ICellRange, IGrid, IDraw, IBackColor, IRefreshSize
    {
        System.Collections.Generic.List<ICell> GetAllCells();
        ICell MaxCell { get; }
        int MaxColumn();
        int MaxRow();
        ICell MinCell { get; }
        int MinColumn();
        int MinRow();
        bool AddRectContains(Point pt);
    }
}
