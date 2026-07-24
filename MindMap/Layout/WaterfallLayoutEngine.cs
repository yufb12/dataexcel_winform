using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 瀑布布局引擎
    /// 适用场景：流程分解、任务拆解、步骤展示
    /// 布局特点：节点从左上向右下阶梯式排列，像瀑布一样流动
    /// </summary>
    public class WaterfallLayoutEngine : ILayoutEngine
    {
        private const float HORIZONTAL_STEP = 120f;   // 水平步长
        private const float VERTICAL_STEP = 80f;      // 垂直步长
        private const float SIBLING_SPACING = 60f;    // 兄弟节点间距

        public void Layout(MindMapDocument document)
        {
            if (document == null || document.RootNode == null)
                return;

            MindMapNode root = document.RootNode;

            // 根节点在左上角
            root.Position = new PointF(0f, 0f);

            // 递归瀑布布局
            LayoutWaterfallChildren(root, 1, 0);
        }

        /// <summary>
        /// 递归瀑布布局子节点
        /// </summary>
        private void LayoutWaterfallChildren(MindMapNode parent, int level, float baseY)
        {
            if (parent.ChildNodes == null || parent.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = parent.ChildNodes;
            int count = children.Count;

            float startX = parent.Position.X + HORIZONTAL_STEP;
            float currentY = parent.Position.Y + baseY;

            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];

                // 瀑布效果：每个子节点向右下偏移
                child.Position = new PointF(
                    startX + i * 20f,
                    currentY + i * SIBLING_SPACING
                );

                // 递归布局孙子节点
                LayoutWaterfallChildren(child, level + 1, VERTICAL_STEP);
            }
        }
    }
}
