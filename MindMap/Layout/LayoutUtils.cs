using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【工具类】布局计算工具
    /// 提供布局算法中常用的辅助方法
    /// 【设计原则】SRP单一职责 - 只负责布局计算，不负责具体布局逻辑
    /// </summary>
    public static class LayoutUtils
    {
        #region 重叠检测

        /// <summary>
        /// 检查节点列表中是否存在重叠
        /// </summary>
        public static bool HasOverlap(IList<MindMapNode> nodes)
        {
            if (nodes == null || nodes.Count < 2)
                return false;

            int count = nodes.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (RectanglesOverlap(nodes[i].Bounds, nodes[j].Bounds))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查两个矩形是否重叠
        /// </summary>
        public static bool RectanglesOverlap(RectangleF rect1, RectangleF rect2)
        {
            return !(rect1.Right < rect2.Left ||
                     rect1.Left > rect2.Right ||
                     rect1.Bottom < rect2.Top ||
                     rect1.Top > rect2.Bottom);
        }

        /// <summary>
        /// 计算节点之间的最小间距
        /// </summary>
        public static float CalculateMinSpacing(IList<MindMapNode> nodes)
        {
            if (nodes == null || nodes.Count < 2)
                return 0;

            float minSpacing = float.MaxValue;
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    float spacing = GetDistanceBetweenNodes(nodes[i], nodes[j]);
                    if (spacing < minSpacing)
                        minSpacing = spacing;
                }
            }
            return minSpacing;
        }

        #endregion

        #region 距离计算

        /// <summary>
        /// 计算两个节点中心点之间的距离
        /// </summary>
        public static float GetDistanceBetweenNodes(MindMapNode node1, MindMapNode node2)
        {
            float dx = node1.Center.X - node2.Center.X;
            float dy = node1.Center.Y - node2.Center.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 计算节点的最大尺寸（宽度或高度）
        /// </summary>
        public static float GetMaxNodeSize(MindMapNode node)
        {
            return Math.Max(node.Size.Width, node.Size.Height);
        }

        /// <summary>
        /// 计算节点列表中的最大宽度
        /// </summary>
        public static float GetMaxWidth(IList<MindMapNode> nodes)
        {
            float maxWidth = 0;
            foreach (MindMapNode node in nodes)
            {
                if (node.Size.Width > maxWidth)
                    maxWidth = node.Size.Width;
            }
            return maxWidth;
        }

        /// <summary>
        /// 计算节点列表中的最大高度
        /// </summary>
        public static float GetMaxHeight(IList<MindMapNode> nodes)
        {
            float maxHeight = 0;
            foreach (MindMapNode node in nodes)
            {
                if (node.Size.Height > maxHeight)
                    maxHeight = node.Size.Height;
            }
            return maxHeight;
        }

        #endregion

        #region 坐标转换

        /// <summary>
        /// 极坐标转笛卡尔坐标
        /// </summary>
        public static PointF PolarToCartesian(PointF center, float radius, float angleRadians)
        {
            float x = center.X + (float)(radius * Math.Cos(angleRadians));
            float y = center.Y + (float)(radius * Math.Sin(angleRadians));
            return new PointF(x, y);
        }

        /// <summary>
        /// 将中心点坐标转换为左上角坐标
        /// </summary>
        public static PointF CenterToTopLeft(PointF center, SizeF size)
        {
            return new PointF(
                center.X - size.Width / 2f,
                center.Y - size.Height / 2f);
        }

        #endregion

        #region 节点收集

        /// <summary>
        /// BFS广度优先收集所有节点
        /// </summary>
        public static List<MindMapNode> CollectAllNodesBFS(MindMapNode root)
        {
            List<MindMapNode> nodes = new List<MindMapNode>();
            if (root == null) return nodes;

            Queue<MindMapNode> queue = new Queue<MindMapNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                MindMapNode node = queue.Dequeue();
                nodes.Add(node);

                if (node.ChildNodes != null)
                {
                    foreach (MindMapNode child in node.ChildNodes)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
            return nodes;
        }

        /// <summary>
        /// 计算树的最大深度
        /// </summary>
        public static int GetMaxDepth(MindMapNode root)
        {
            if (root == null) return 0;
            if (root.ChildNodes == null || root.ChildNodes.Count == 0) return 1;

            int maxChildDepth = 0;
            foreach (MindMapNode child in root.ChildNodes)
            {
                int childDepth = GetMaxDepth(child);
                if (childDepth > maxChildDepth)
                    maxChildDepth = childDepth;
            }
            return maxChildDepth + 1;
        }

        #endregion

        #region 扇形/环形布局辅助

        /// <summary>
        /// 计算扇形布局中每个子节点所需的角度（基于节点大小）
        /// </summary>
        public static float[] CalculateFanAngles(
            IList<MindMapNode> children, 
            float radius, 
            float startAngle, 
            float endAngle)
        {
            int count = children.Count;
            float[] angles = new float[count];

            if (count == 0) return angles;
            if (count == 1)
            {
                angles[0] = (startAngle + endAngle) / 2f;
                return angles;
            }

            // 计算每个节点需要的角度宽度（基于节点大小）
            float totalAngleRange = endAngle - startAngle;
            float totalWeight = 0;
            float[] weights = new float[count];

            for (int i = 0; i < count; i++)
            {
                // 使用节点宽度作为权重（扇形布局中宽度影响角度）
                weights[i] = children[i].Size.Width;
                totalWeight += weights[i];
            }

            // 添加间距权重
            float spacingAngle = totalAngleRange * 0.1f / count;
            totalWeight += spacingAngle * count;

            // 按权重分配角度
            float currentAngle = startAngle;
            for (int i = 0; i < count; i++)
            {
                float nodeAngleWidth = (weights[i] / totalWeight) * totalAngleRange;
                angles[i] = currentAngle + nodeAngleWidth / 2f;
                currentAngle += nodeAngleWidth + spacingAngle;
            }

            return angles;
        }

        /// <summary>
        /// 计算环形布局的最小半径（确保节点不重叠）
        /// </summary>
        public static float CalculateCircleRadius(IList<MindMapNode> children, float centerX, float centerY)
        {
            if (children == null || children.Count == 0)
                return 100f;

            int count = children.Count;
            float maxWidth = GetMaxWidth(children);
            float maxHeight = GetMaxHeight(children);

            // 估算最小半径（基于正多边形内切圆）
            float angleStep = (float)(2 * Math.PI / count);
            float minRadius = Math.Max(maxWidth, maxHeight) / (2f * (float)Math.Sin(angleStep / 2f));

            // 增加额外间距
            minRadius += LayoutConstants.NodeSpacing;

            return minRadius;
        }

        #endregion
    }
}
