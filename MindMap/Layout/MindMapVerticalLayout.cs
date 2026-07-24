using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 垂直思维导图布局引擎
    /// 适用场景：纵向思维导图、流程梳理
    /// 布局特点：根节点在中心，子节点向上下两侧对称展开
    /// </summary>
    public class MindMapVerticalLayout : ILayoutEngine
    {
        private const float HORIZONTAL_SPACING = 80f;
        private const float VERTICAL_SPACING = 100f;

        public void Layout(MindMapDocument document)
        {
            if (document == null || document.RootNode == null)
                return;

            MindMapNode root = document.RootNode;

            // 根节点在中心
            root.Position = new PointF(0f, 0f);

            if (root.ChildNodes == null || root.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = root.ChildNodes;
            int count = children.Count;

            // 子节点分成上下两组
            int topCount = count / 2;
            int bottomCount = count - topCount;

            // 布局下侧节点（向下展开）
            for (int i = 0; i < bottomCount; i++)
            {
                MindMapNode child = children[i];
                float x = (i - bottomCount / 2f) * HORIZONTAL_SPACING;
                child.Position = new PointF(x, VERTICAL_SPACING);
                LayoutChildrenVertical(child, VERTICAL_SPACING * 1.5f, true);
            }

            // 布局上侧节点（向上展开）
            for (int i = 0; i < topCount; i++)
            {
                MindMapNode child = children[bottomCount + i];
                float x = (i - topCount / 2f) * HORIZONTAL_SPACING;
                child.Position = new PointF(x, -VERTICAL_SPACING);
                LayoutChildrenVertical(child, -VERTICAL_SPACING * 1.5f, false);
            }
        }

        /// <summary>
        /// 垂直布局子节点
        /// </summary>
        private void LayoutChildrenVertical(MindMapNode parent, float baseY, bool toBottom)
        {
            if (parent.ChildNodes == null || parent.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = parent.ChildNodes;
            int count = children.Count;
            float direction = toBottom ? 1f : -1f;

            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];
                float y = baseY + direction * VERTICAL_SPACING;
                float x = parent.Position.X + (i - count / 2f) * HORIZONTAL_SPACING;
                child.Position = new PointF(x, y);
                LayoutChildrenVertical(child, y + direction * VERTICAL_SPACING * 0.6f, toBottom);
            }
        }
    }
}
