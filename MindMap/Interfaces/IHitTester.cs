using System.Drawing;
using MindMap.Core;

namespace MindMap.Interfaces
{
    /// <summary>
    /// 命中测试器接口，定义元素命中检测的契约
    /// </summary>
    public interface IHitTester
    {
        /// <summary>
        /// 测试指定位置命中的元素
        /// </summary>
        /// <param name="point">测试位置（文档坐标）</param>
        /// <param name="rootNode">根节点</param>
        /// <returns>命中测试结果</returns>
        HitTestResult HitTest(PointF point, MindMapNode rootNode);
    }
}
