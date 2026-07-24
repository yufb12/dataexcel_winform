using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 组织结构图布局引擎
    /// 适用场景：公司组织架构、团队结构、层级关系
    /// 布局特点：自上而下的层级布局，每个层级居中对齐
    /// </summary>
    public class OrgChartLayoutEngine : ILayoutEngine
    {
        private const float HORIZONTAL_SPACING = 30f;
        private const float VERTICAL_SPACING = 80f;
        private const float LEVEL_INDENT = 0f;

        public void Layout(MindMapDocument document)
        {
            if (document == null || document.RootNode == null)
                return;

            MindMapNode root = document.RootNode;

            // 根节点在顶部居中
            root.Position = new PointF(0f, 0f);

            // 递归布局所有层级
            LayoutLevel(root, 1);
        }

        /// <summary>
        /// 布局单个层级
        /// </summary>
        private void LayoutLevel(MindMapNode parent, int level)
        {
            if (parent.ChildNodes == null || parent.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = parent.ChildNodes;
            int count = children.Count;

            // 计算总宽度用于居中对齐
            float totalWidth = 0f;
            foreach (MindMapNode child in children)
            {
                totalWidth += 120f; // 假设每个节点宽度约120px
            }
            totalWidth += (count - 1) * HORIZONTAL_SPACING;

            // 起始X坐标（居中对齐）
            float startX = parent.Position.X - totalWidth / 2f;
            float y = parent.Position.Y + VERTICAL_SPACING;

            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];
                float x = startX + i * (120f + HORIZONTAL_SPACING);

                child.Position = new PointF(x, y);

                // 递归布局下一层级
                LayoutLevel(child, level + 1);
            }
        }
    }
}
