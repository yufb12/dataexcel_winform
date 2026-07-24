using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 【优化版】螺旋布局引擎
    /// 适用场景：艺术化展示、概念地图、创意发散、时间线
    /// 布局特点：节点沿阿基米德螺旋线向外展开，视觉效果美观
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 螺旋增长率根据节点大小动态调整
    /// - 角度步长根据节点大小动态计算
    /// - 支持重叠检测与自动调整
    /// 
    /// 【算法原理】
    /// 阿基米德螺旋: r = a + b*θ
    /// 1. 根据节点大小计算基础螺旋参数
    /// 2. 沿螺旋线放置子节点
    /// 3. 根据每个节点的大小动态调整角度步长
    /// 4. 检测重叠，自动增大螺旋增长率
    /// </summary>
    public class SpiralLayoutEngine : LayoutEngineBase
    {
        #region 螺旋布局参数（可配置）

        /// <summary>
        /// 螺旋密度系数（值越大越稀疏）
        /// </summary>
        public float SpiralDensity { get; set; }

        /// <summary>
        /// 起始角度偏移（每层的起始角度偏移）
        /// </summary>
        public float LevelAngleOffset { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SpiralLayoutEngine() : base()
        {
            SpiralDensity = 1.5f;
            LevelAngleOffset = 0.5f;
        }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public SpiralLayoutEngine(LayoutOptions options) : base(options)
        {
            SpiralDensity = 1.5f;
            LevelAngleOffset = 0.5f;
        }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 螺旋布局：根节点在中心
        /// </summary>
        protected override void PlaceRootNode(MindMapNode root)
        {
            // 根节点在中心位置
            root.Position = new PointF(
                -root.Size.Width / 2f,
                -root.Size.Height / 2f);
        }

        /// <summary>
        /// 布局子节点（核心实现）
        /// 沿螺旋线排列所有子节点
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            IList<MindMapNode> children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            PointF parentCenter = parent.Center;

            // 计算起始角度和基础半径
            float startAngle = level * LevelAngleOffset;
            float baseRadius = CalculateLevelRadius(parent, level);

            // 计算螺旋增长率（根据节点大小动态调整）
            float growth = CalculateSpiralGrowth(parent, children);

            // 迭代调整参数，直到节点不重叠
            float currentGrowth = growth;
            bool success = false;
            int iterations = 0;

            while (!success && iterations < Options.MaxIterations)
            {
                // 沿螺旋线排列子节点
                PlaceChildrenOnSpiral(children, parentCenter, baseRadius, startAngle, currentGrowth);

                // 检查是否重叠
                success = !LayoutUtils.HasOverlap(children);

                if (!success)
                {
                    // 重叠则增加螺旋增长率
                    currentGrowth *= Options.OverlapAdjustmentFactor;
                    iterations++;
                }
            }

            // 递归布局孙子节点
            for (int i = 0; i < children.Count; i++)
            {
                LayoutChildren(children[i], level + 1);
            }
        }

        #endregion

        #region 核心算法

        /// <summary>
        /// 计算当前层级的基础半径
        /// </summary>
        private float CalculateLevelRadius(MindMapNode parent, int level)
        {
            // 基础半径：父节点对角线的一半 + 层级间距
            float parentDiagonal = GetDiagonal(parent.Size);
            float baseRadius = parentDiagonal / 2f + GetLevelSpacing(parent.Size);

            // 层级越深，半径递增
            float levelFactor = 1f + (level - 1) * 0.2f;
            baseRadius *= levelFactor;

            return baseRadius;
        }

        /// <summary>
        /// 计算螺旋增长率（根据节点大小动态调整）
        /// </summary>
        private float CalculateSpiralGrowth(MindMapNode parent, IList<MindMapNode> children)
        {
            // 计算最大节点大小
            float maxNodeSize = 0;
            for (int i = 0; i < children.Count; i++)
            {
                float nodeDiagonal = GetDiagonal(children[i].Size);
                if (nodeDiagonal > maxNodeSize)
                    maxNodeSize = nodeDiagonal;
            }

            // 螺旋增长率 = 最大节点大小 × 密度系数
            return maxNodeSize * SpiralDensity;
        }

        /// <summary>
        /// 将子节点沿螺旋线排列
        /// </summary>
        private void PlaceChildrenOnSpiral(
            IList<MindMapNode> children,
            PointF center,
            float startRadius,
            float startAngle,
            float growth)
        {
            int count = children.Count;
            if (count == 0) return;

            float currentAngle = startAngle;
            float currentRadius = startRadius;

            for (int i = 0; i < count; i++)
            {
                // 计算螺旋线上的位置
                // 阿基米德螺旋: r = a + b*θ
                float radius = startRadius + growth * currentAngle / (2f * (float)Math.PI);
                PointF childCenter = LayoutUtils.PolarToCartesian(center, radius, currentAngle);
                children[i].Position = LayoutUtils.CenterToTopLeft(childCenter, children[i].Size);

                // 计算下一个节点的角度步长（考虑节点大小）
                float nodeDiagonal = GetDiagonal(children[i].Size);
                float angleIncrement = nodeDiagonal / radius * 0.8f;

                // 确保最小角度间距
                angleIncrement = Math.Max(angleIncrement, 0.2f);

                currentAngle += angleIncrement;
            }
        }

        #endregion
    }
}
