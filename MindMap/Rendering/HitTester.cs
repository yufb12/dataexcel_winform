using System;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Rendering
{
    /// <summary>
    /// 命中测试器实现
    /// </summary>
    public class HitTester : IHitTester
    {
        private readonly INodeRenderer _renderer;

        /// <summary>
        /// 初始化命中测试器
        /// </summary>
        /// <param name="renderer">节点渲染器（用于获取按钮边界）</param>
        public HitTester(INodeRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer");

            _renderer = renderer;
        }

        /// <summary>
        /// 测试指定位置命中的元素
        /// </summary>
        public HitTestResult HitTest(PointF point, MindMapNode rootNode)
        {
            if (rootNode == null)
                return HitTestResult.None;

            // 倒序遍历，上层节点优先命中
            return HitTestRecursive(point, rootNode, true);
        }

        /// <summary>
        /// 递归进行命中测试
        /// </summary>
        private HitTestResult HitTestRecursive(PointF point, MindMapNode node, bool reverse)
        {
            // v2.3：先测试4个方向的展开/折叠按钮（优先级高于子节点）
            using (Graphics g = Graphics.FromImage(new Bitmap(1, 1)))
            {
                RectangleF nodeBounds = _renderer.CalculateNodeBounds(g, node);
                foreach (NodeDirection direction in Enum.GetValues(typeof(NodeDirection)))
                {
                    if (node.HasChildrenInDirection(direction))
                    {
                        RectangleF btnRect = NodeBodyRenderer.GetExpandButtonBounds(nodeBounds, direction);
                        if (btnRect.Contains(point))
                        {
                            return HitTestResult.ExpandButtonHit(node);
                        }
                    }
                }
            }

            // 再测试子节点（倒序，保证上层优先）
            // 使用GetAllExpandedChildNodes获取所有展开方向的子节点
            System.Collections.Generic.IList<MindMapNode> expandedChildren = node.GetAllExpandedChildNodes();
            if (expandedChildren.Count > 0)
            {
                if (reverse)
                {
                    for (int i = expandedChildren.Count - 1; i >= 0; i--)
                    {
                        HitTestResult childResult = HitTestRecursive(point, expandedChildren[i], reverse);
                        if (childResult.IsHit)
                            return childResult;
                    }
                }
                else
                {
                    for (int i = 0; i < expandedChildren.Count; i++)
                    {
                        HitTestResult childResult = HitTestRecursive(point, expandedChildren[i], reverse);
                        if (childResult.IsHit)
                            return childResult;
                    }
                }
            }

            // 最后测试节点本身
            if (node.ContainsPoint(point))
            {
                return HitTestResult.NodeHit(node);
            }

            return HitTestResult.None;
        }
    }
}
