using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【环形布局引擎】
    /// 适用场景：层级关系、环绕关系、生态系统、圈层结构、太阳系模型
    /// 布局特点：多层同心圆布局，每层一圈节点，从内向外层级递增
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 每层半径根据节点大小动态计算
    /// - 每层节点均匀分布在圆周上
    /// - 支持多层级环形布局
    /// </summary>
    public class RingLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RingLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public RingLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 环形布局：根节点在中心
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
        /// 子节点均匀分布在圆周上
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            int count = children.Count;

            // 计算当前层级的半径
            float radius = CalculateRingRadius(parent, children, level);

            // 计算每个节点的角度（均匀分布）
            float angleStep = (float)(Math.PI * 2.0 / count);
            float startAngle = -(float)Math.PI / 2f; // 从顶部开始

            for (int i = 0; i < count; i++)
            {
                float angle = startAngle + i * angleStep;

                PointF childCenter = LayoutUtils.PolarToCartesian(
                    parent.Center, radius, angle);
                children[i].Position = LayoutUtils.CenterToTopLeft(
                    childCenter, children[i].Size);

                // 递归布局孙子节点
                LayoutChildren(children[i], level + 1);
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 计算环形布局的半径
        /// 根据节点大小和数量动态计算
        /// </summary>
        private float CalculateRingRadius(MindMapNode parent, IList<MindMapNode> children, int level)
        {
            if (children == null || children.Count == 0) return 100f;

            int count = children.Count;

            // 计算最大节点大小
            float maxSize = 0f;
            for (int i = 0; i < count; i++)
            {
                float size = GetDiagonal(children[i].Size);
                if (size > maxSize) maxSize = size;
            }

            // 基于正多边形计算最小半径
            // 半径 = 节点大小 / (2 * sin(π / 节点数))
            float angleStep = (float)(Math.PI * 2.0 / count);
            float minRadius = maxSize / (2f * (float)Math.Sin(angleStep / 2f));

            // 加上父节点的大小和层级间距
            float parentDiagonal = GetDiagonal(parent.Size);
            float baseRadius = parentDiagonal / 2f + GetLevelSpacing(parent.Size);

            // 取较大值
            float radius = Math.Max(minRadius, baseRadius);

            // 根据层级增加额外间距
            radius += level * GetRadialSpacing(parent.Size) * 0.5f;

            return radius;
        }

        #endregion
    }
}
