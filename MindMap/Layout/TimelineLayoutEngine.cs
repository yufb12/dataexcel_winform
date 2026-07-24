using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 时间线布局引擎
    /// 适用场景：项目进度、历史事件、发展历程
    /// 布局特点：节点沿水平时间轴排列，子节点在时间轴上下交替分布
    /// </summary>
    public class TimelineLayoutEngine : ILayoutEngine
    {
        // v2.1.7修复：大幅增加间距避免节点重叠
        private const float NODE_SPACING_X = 250f;      // 节点水平间距
        private const float NODE_SPACING_Y = 120f;      // 节点垂直间距（时间轴上下）
        private const float LEVEL_SPACING = 300f;       // 层级间距
        private const float TIMELINE_Y = 0f;

        public void Layout(MindMapDocument document)
        {
            if (document == null || document.RootNode == null)
                return;

            MindMapNode root = document.RootNode;

            // 根节点在时间轴起点
            root.Position = new PointF(0f, TIMELINE_Y);

            // 布局子节点沿时间轴
            LayoutTimelineNodes(root, 1);
        }

        /// <summary>
        /// 沿时间轴布局节点（v2.1.7修复：优化位置计算避免重叠）
        /// </summary>
        private void LayoutTimelineNodes(MindMapNode parent, int level)
        {
            if (parent.ChildNodes == null || parent.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = parent.ChildNodes;
            int count = children.Count;

            // v2.1.7修复：每个子节点独立水平位置，不共享父节点位置
            float startX = parent.Position.X + LEVEL_SPACING;

            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];

                // v2.1.7修复：X坐标：每个子节点有独立的水平位置，间距足够大
                float x = startX + (i * NODE_SPACING_X);

                // Y坐标：子节点在时间轴上下交替，间距足够大
                bool isTop = (i % 2 == 0);
                float y = isTop ? TIMELINE_Y - NODE_SPACING_Y : TIMELINE_Y + NODE_SPACING_Y;

                child.Position = new PointF(x, y);

                // 孙子节点继续沿时间线展开（从各自父节点位置开始）
                LayoutTimelineNodes(child, level + 1);
            }
        }
    }
}
