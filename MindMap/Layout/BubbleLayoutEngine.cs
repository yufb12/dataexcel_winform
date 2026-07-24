using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【气泡图布局引擎】
    /// 适用场景：头脑风暴、概念图、权重展示、数据分析、创意发散
    /// 布局特点：节点大小表示重要性，围绕中心分布，尽量不重叠
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 节点按大小排序，大节点靠近中心
    /// - 螺旋式放置，避免重叠
    /// - 支持重叠检测与自动调整
    /// </summary>
    public class BubbleLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BubbleLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public BubbleLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 气泡图：根节点在中心，最大
        /// </summary>
        protected override void PlaceRootNode(MindMapNode root)
        {
            // 根节点在中心
            root.Position = new PointF(
                -root.Size.Width / 2f,
                -root.Size.Height / 2f);
        }

        /// <summary>
        /// 布局子节点（核心实现）
        /// 螺旋式放置子节点，大节点靠近中心
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            // 按节点大小排序（从大到小），大节点靠近中心
            List<MindMapNode> sortedChildren = new List<MindMapNode>(children);
            sortedChildren.Sort(delegate(MindMapNode a, MindMapNode b)
            {
                float sizeA = GetDiagonal(a.Size);
                float sizeB = GetDiagonal(b.Size);
                return sizeB.CompareTo(sizeA); // 降序
            });

            // 螺旋式放置节点
            float baseRadius = GetDiagonal(parent.Size) / 2f + GetRadialSpacing(parent.Size);
            float angle = 0f;
            float radius = baseRadius;
            float angleStep = 0.5f; // 角度步长（弧度）

            List<RectangleF> placedBounds = new List<RectangleF>();
            placedBounds.Add(parent.Bounds);

            for (int i = 0; i < sortedChildren.Count; i++)
            {
                var child = sortedChildren[i];

                // 尝试在当前位置放置，如果重叠则增加角度
                int attempts = 0;
                bool isPlaced = false;

                while (!isPlaced && attempts < 100)
                {
                    PointF center = LayoutUtils.PolarToCartesian(
                        parent.Center, radius, angle);
                    PointF position = LayoutUtils.CenterToTopLeft(center, child.Size);
                    RectangleF bounds = new RectangleF(position, child.Size);

                    // 检查是否与已放置的节点重叠
                    bool overlaps = false;
                    foreach (RectangleF placedRect in placedBounds)
                    {
                        if (LayoutUtils.RectanglesOverlap(bounds, placedRect))
                        {
                            overlaps = true;
                            break;
                        }
                    }

                    if (!overlaps)
                    {
                        child.Position = position;
                        placedBounds.Add(bounds);
                        isPlaced = true;
                    }
                    else
                    {
                        // 增加角度，尝试下一个位置
                        angle += angleStep;
                        if (angle > (float)Math.PI * 2f)
                        {
                            angle = 0f;
                            radius += GetRadialSpacing(child.Size) * 0.5f;
                        }
                    }

                    attempts++;
                }

                // 如果没找到合适位置，强制放置
                if (!isPlaced)
                {
                    PointF center = LayoutUtils.PolarToCartesian(
                        parent.Center, radius, angle);
                    child.Position = LayoutUtils.CenterToTopLeft(center, child.Size);
                    placedBounds.Add(child.Bounds);
                }

                // 递归布局孙子节点
                LayoutChildren(child, level + 1);

                // 移动到下一个角度
                angle += angleStep * 2f;
            }
        }

        #endregion
    }
}
