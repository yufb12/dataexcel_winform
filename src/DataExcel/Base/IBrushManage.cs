using System;
using System.Drawing;
namespace Feng.Excel.Interfaces
{
    public interface IRefreshSize
    {
        void ReFreshSize();
    }
    public interface IBrushManage
    {
        void Add(Color key, System.Drawing.Brush value);
        void Clear();
        bool ContainsKey(Color key);
        int Count { get; }
        bool IsReadOnly { get; }
        System.Collections.Generic.ICollection<Color> Keys { get; }
        bool Remove(Color key);
        System.Drawing.Brush this[Color key] { get; set; }
        bool TryGetValue(Color key, out System.Drawing.Brush value);

    }
}
