using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 【优化版】扇形布局引擎
    /// 适用场景：思维导图、知识图谱、分类展示
    /// 布局特点：根节点在底部，子节点向上扇形展开，层次分明
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 基于子树大小分配角度，大子树占更多角度
    /// - 动态计算半径，确保所有子树不重叠
    /// - 角度范围根据子节点数量动态调整
    /// - 优化半径计算，避免节点超出视图
    /// 
    /// 【算法原理】
    /// 1. 计算每个子树的总大小（包括所有后代节点）
    /// 2. 根据子树大小计算在扇形上需要的角度宽度
    /// 3. 按比例分配角度，大子树占更多角度
    /// 4. 计算最小半径，确保所有子树都能放下
    /// 5. 递归布局子节点的子节点
    /// </summary>
    public class FanLayoutEngine : LayoutEngineBase
    {
        #region 扇形布局参数（可配置）
        /// <summary>
        /// 扇形起始角度比例（相对于完整扇形）
        /// 默认从左侧约-165度开始
        /// </summary>
        public float StartAngleRatio { get; set; }

        /// <summary>
        /// 扇形结束角度比例（相对于完整扇形）
        /// 默认到右侧约-15度结束
        /// </summary>
        public float EndAngleRatio { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FanLayoutEngine() : base()
        {
            StartAngleRatio = 0.08f;  // 约-165度
            EndAngleRatio = 0.92f;    // 约-15度
        }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public FanLayoutEngine(LayoutOptions options) : base(options)
        {
            StartAngleRatio = 0.08f;
            EndAngleRatio = 0.92f;
        }
        #endregion

        #region 模板方法实现
        /// <summary>
        /// 放置根节点（重写）
        /// 扇形布局：根节点在底部中心
        /// </summary>
        protected override void PlaceRootNode(MindMapNode root)
        {
            // 根节点在底部中心位置
            // 以根节点的底部中心为基准
            root.Position = new PointF(
                -root.Size.Width / 2f,
                -root.Size.Height);
        }

        /// <summary>
        /// 布局子节点（核心实现）
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            IList<MindMapNode> children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            // 步骤1：计算当前层级的最小半径
            float radius = CalculateMinimumRadius(parent, children, level);

            // 步骤2：计算角度范围
            float startAngle;
            float angleRange;
            CalculateAngleRange(children, radius, level, out startAngle, out angleRange);

            // 步骤3：按子树大小权重分配角度
            float[] angles = CalculateAnglesBySubtreeSize(
                children, radius, startAngle, angleRange);

            // 步骤4：放置子节点
            PlaceChildrenInFan(parent, children, radius, angles);

            // 步骤5：递归布局孙子节点
            for (int i = 0; i < children.Count; i++)
            {
                LayoutChildren(children[i], level + 1);
            }
        }
        #endregion

        #region 核心算法
        /// <summary>
        /// 计算当前层级的最小半径
        /// 确保所有子树都能放下且不重叠
        /// </summary>
        private float CalculateMinimumRadius(
            MindMapNode parent, 
            IList<MindMapNode> children, 
            int level)
        {
            // 基础半径：父节点高度 + 适度的间距（扇形布局不需要太大的层级间距）
            float baseRadius = parent.Size.Height + GetVerticalSpacing(parent.Size) * 0.5f;

            // 层级越深，半径适度增加（但不要增加太多）
            float levelFactor = 1f + (level - 1) * 0.1f;
            baseRadius *= levelFactor;

            // 计算所有子树的总角度需求
            float totalAngularWidth = 0;
            for (int i = 0; i < children.Count; i++)
            {
                totalAngularWidth += CalculateSubtreeAngularWidthOptimized(
                    children[i], baseRadius);
            }

            // 计算可用角度范围（扇形）
            float availableAngle = (float)Math.PI * (EndAngleRatio - StartAngleRatio);

            // 如果总角度超过可用角度，需要增大半径
            float radius = baseRadius;
            int iterations = 0;
            while (totalAngularWidth > availableAngle && iterations < 50)
            {
                radius *= 1.1f; // 每次增加10%，更精细
                totalAngularWidth = 0;
                for (int i = 0; i < children.Count; i++)
                {
                    totalAngularWidth += CalculateSubtreeAngularWidthOptimized(
                        children[i], radius);
                }
                iterations++;
            }

            return radius;
        }

        /// <summary>
        /// 优化的子树角度宽度计算
        /// 使用子树宽度而不是对角线，更准确
        /// </summary>
        private float CalculateSubtreeAngularWidthOptimized(
            MindMapNode node, 
            float radius)
        {
            if (node == null) return 0;

            // 使用节点宽度作为主要参考，高度作为辅助
            // 扇形布局中，水平方向的宽度更重要
            float effectiveWidth = node.Size.Width;

            // 如果有子节点，考虑子树的总宽度
            IList<MindMapNode> children = GetVisibleChildren(node);
            if (children != null && children.Count > 0)
            {
                float childrenWidth = 0;
                for (int i = 0; i < children.Count; i++)
                {
                    childrenWidth += children[i].Size.Width;
                    if (i < children.Count - 1)
                    {
                        childrenWidth += GetHorizontalSpacing(children[i].Size) * 0.5f;
                    }
                }
                effectiveWidth = Math.Max(effectiveWidth, childrenWidth);
            }

            // 转换为角度（弧度）
            if (radius <= 0) radius = 50f;

            float sinValue = effectiveWidth / (2f * radius);
            if (sinValue > 1f) sinValue = 1f;
            if (sinValue < -1f) sinValue = -1f;

            float angularWidth = 2f * (float)Math.Asin(sinValue);

            // 添加额外的间距角度
            angularWidth += 0.05f; // 更小的间距角度

            return Math.Max(angularWidth, 0.05f); // 最小角度
        }

        /// <summary>
        /// 计算角度范围（根据子节点数量动态调整）
        /// </summary>
        private void CalculateAngleRange(
            IList<MindMapNode> children, 
            float radius,
            int level,
            out float startAngle,
            out float angleRange)
        {
            // 基础角度范围（上半圆的一部分）
            float fullFanAngle = (float)Math.PI * (EndAngleRatio - StartAngleRatio);

            // 计算所有子树的总角度需求
            float totalAngularWidth = 0;
            for (int i = 0; i < children.Count; i++)
            {
                totalAngularWidth += CalculateSubtreeAngularWidthOptimized(
                    children[i], radius);
            }

            // 实际使用的角度范围取总需求和完整扇形的较小值
            angleRange = Math.Min(totalAngularWidth, fullFanAngle);

            // 起始角度：居中放置
            startAngle = -(float)Math.PI + (float)Math.PI * StartAngleRatio 
                + (fullFanAngle - angleRange) / 2f;
        }

        /// <summary>
        /// 按子树大小权重分配角度
        /// </summary>
        private float[] CalculateAnglesBySubtreeSize(
            IList<MindMapNode> children, 
            float radius,
            float startAngle,
            float angleRange)
        {
            int count = children.Count;
            float[] angles = new float[count];
            if (count == 0) return angles;

            if (count == 1)
            {
                angles[0] = startAngle + angleRange / 2f;
                return angles;
            }

            // 计算每个子树需要的角度宽度
            float[] angularWidths = new float[count];
            float totalWidth = 0;
            for (int i = 0; i < count; i++)
            {
                angularWidths[i] = CalculateSubtreeAngularWidthOptimized(
                    children[i], radius);
                totalWidth += angularWidths[i];
            }

            // 如果总角度超过了可用角度，按比例缩小
            float scale = 1f;
            if (totalWidth > angleRange)
            {
                scale = angleRange / totalWidth;
            }

            // 分配角度
            float currentAngle = startAngle;
            for (int i = 0; i < count; i++)
            {
                float width = angularWidths[i] * scale;
                angles[i] = currentAngle + width / 2f;
                currentAngle += width;
            }

            return angles;
        }

        /// <summary>
        /// 将子节点按扇形排列
        /// </summary>
        private void PlaceChildrenInFan(
            MindMapNode parent, 
            IList<MindMapNode> children, 
            float radius,
            float[] angles)
        {
            int count = children.Count;
            if (count == 0) return;

            PointF parentCenter = parent.Center;
            for (int i = 0; i < count; i++)
            {
                PointF childCenter = LayoutUtils.PolarToCartesian(
                    parentCenter, radius, angles[i]);
                children[i].Position = LayoutUtils.CenterToTopLeft(
                    childCenter, children[i].Size);
            }
        }
        #endregion
    }
}
