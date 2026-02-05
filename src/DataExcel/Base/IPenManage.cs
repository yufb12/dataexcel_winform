using System;
namespace Feng.Excel.Interfaces
{
    public interface IPenManage
    {
        void Add(System.Drawing.Color key, System.Drawing.Pen value);
        void Clear();
        bool ContainsKey(System.Drawing.Color key);
        int Count { get; }
        bool Remove(System.Drawing.Color key);
        System.Drawing.Pen this[System.Drawing.Color key] { get; set; }
        System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Drawing.Color, System.Drawing.Pen>> GetEnumerator();

    }
}
