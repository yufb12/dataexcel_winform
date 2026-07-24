using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 对称布局引擎
    /// 适用场景：对比分析、优缺点比较、方案对比
    /// 布局特点：完美左右镜像对称，适合A/B对比展示
    /// </summary>
    public class SymmetricLayoutEngine : ILayoutEngine
    {
        private const float HORIZONTAL_SPACING = 100f;
        private const float VERTICAL_SPACING = 60f;

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

            // 子节点分成左右两组（严格对称）
            int leftCount = (count + 1) / 2;
            int rightCount = count / 2;

            // 布局左侧节点
            for (int i = 0; i < leftCount; i++)
            {
                MindMapNode child = children[i];
                float y = (i - leftCount / 2f) * VERTICAL_SPACING;
                child.Position = new PointF(-HORIZONTAL_SPACING, y);
                LayoutSymmetricChildren(child, -HORIZONTAL_SPACING * 1.5f, false);
            }

            // 布局右侧节点（镜像对称）
            for (int i = 0; i < rightCount; i++)
            {
                MindMapNode child = children[leftCount + i];
                float y = (i - rightCount / 2f) * VERTICAL_SPACING;
                child.Position = new PointF(HORIZONTAL_SPACING, y);
                LayoutSymmetricChildren(child, HORIZONTAL_SPACING * 1.5f, true);
            }
        }

        /// <summary>
        /// 对称布局子节点
        /// </summary>
        private void LayoutSymmetricChildren(MindMapNode parent, float baseX, bool toRight)
        {
            if (parent.ChildNodes == null || parent.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = parent.ChildNodes;
            int count = children.Count;
            float direction = toRight ? 1f : -1f;

            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];
                float x = baseX + direction * HORIZONTAL_SPACING;
                float y = parent.Position.Y + (i - count / 2f) * VERTICAL_SPACING;
                child.Position = new PointF(x, y);
                LayoutSymmetricChildren(child, x + direction * HORIZONTAL_SPACING * 0.6f, toRight);
            }
        }
    }
}
