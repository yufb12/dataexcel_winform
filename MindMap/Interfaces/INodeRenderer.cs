using System.Drawing;
using MindMap.Core;

namespace MindMap.Interfaces
{
    /// <summary>
    /// 节点渲染器接口，定义节点绘制的契约
    /// </summary>
    public interface INodeRenderer
    {
        /// <summary>
        /// 绘制单个节点
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="node">要绘制的节点</param>
        /// <param name="isSelected">是否选中</param>
        void DrawNode(Graphics graphics, MindMapNode node, bool isSelected);

        /// <summary>
        /// 绘制节点间连接线
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="node">子节点</param>
        void DrawConnection(Graphics graphics, MindMapNode node);

        /// <summary>
        /// 计算节点的边界矩形
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="node">节点</param>
        /// <returns>节点边界矩形</returns>
        RectangleF CalculateNodeBounds(Graphics graphics, MindMapNode node);

        /// <summary>
        /// 获取展开/折叠按钮的边界矩形
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>按钮边界矩形</returns>
        RectangleF GetExpandButtonBounds(MindMapNode node);
    }
}
