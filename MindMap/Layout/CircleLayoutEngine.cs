using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 圆形布局引擎
    /// 适用场景：中心主题、关系网络、概念关联
    /// 布局特点：子节点围绕根节点呈圆形均匀分布
    /// </summary>
    public class CircleLayoutEngine : ILayoutEngine
    {
        private const float CIRCLE_RADIUS = 180f;     // 圆形半径
        private const float CHILD_RADIUS = 100f;      // 子节点层级半径

        public void Layout(MindMapDocument document)
        {
            if (document == null || document.RootNode == null)
                return;

            MindMapNode root = document.RootNode;

            // 根节点在圆心
            root.Position = new PointF(0f, 0f);

            if (root.ChildNodes == null || root.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = root.ChildNodes;
            int count = children.Count;

            // 一级子节点：围绕根节点圆形排列
            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];
                float angle = (float)(2 * Math.PI * i / count);

                float x = (float)(CIRCLE_RADIUS * Math.Cos(angle));
                float y = (float)(CIRCLE_RADIUS * Math.Sin(angle));

                child.Position = new PointF(x, y);

                // 二级子节点：在各自父节点周围小圆形排列
                LayoutChildCircle(child, CHILD_RADIUS);
            }
        }

        /// <summary>
        /// 子节点圆形布局
        /// </summary>
        private void LayoutChildCircle(MindMapNode parent, float radius)
        {
            if (parent.ChildNodes == null || parent.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = parent.ChildNodes;
            int count = children.Count;

            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];
                float angle = (float)(2 * Math.PI * i / count);

                float x = parent.Position.X + (float)(radius * Math.Cos(angle));
                float y = parent.Position.Y + (float)(radius * Math.Sin(angle));

                child.Position = new PointF(x, y);

                // 更深层级继续递归
                LayoutChildCircle(child, radius * 0.6f);
            }
        }
    }
}
