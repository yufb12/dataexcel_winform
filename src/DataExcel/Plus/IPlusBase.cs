using System;
using System.Windows.Forms;
namespace Feng.Excel.Interfaces
{
    public interface IPlus
    {
        string Company { get; }
        string Copyright { get; }
        string Culture { get; }
        string Description { get; }
        Feng.Excel.DataExcel Grid { get; }
        Guid Guid { get; }
        void Load(Feng.Excel.DataExcel grid);
        void Load(Form frm);
        string Name { get; }
        string Product { get; }
        string Title { get; }
    }
}
