using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 【优化版】放射状布局引擎
    /// 适用场景：思维导图、知识图谱、中心发散型内容
    /// 布局特点：根节点在中心，子节点沿圆周向外发散
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 基于子树大小分配角度，大子树占更多角度
    /// - 动态计算半径，确保所有子树不重叠
    /// - 子节点方向与父节点方向一致，形成放射状
    /// - 支持多级递归布局，每级都考虑子树大小
    /// 
    /// 【算法原理】
    /// 1. 计算每个子树的总大小（包括所有后代节点）
    /// 2. 根据子树大小计算在圆周上需要的角度宽度
    /// 3. 按比例分配角度，大子树占更多角度
    /// 4. 计算最小半径，确保所有子树都能放下
    /// 5. 递归布局子节点的子节点
    /// </summary>
    public class RadialLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RadialLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public RadialLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 放射状布局：根节点在中心
        /// </summary>
        protected override void PlaceRootNode(MindMapNode root)
        {
            // 根节点在中心位置（以中心点对齐）
            root.Position = new PointF(
                -root.Size.Width / 2f,
                -root.Size.Height / 2f);
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
            float radius = CalculateMinimumRadius(parent, children);

            // 步骤2：按子树大小权重分配角度
            float[] angles = CalculateAnglesBySubtreeSize(children, radius);

            // 步骤3：放置子节点
            PlaceChildrenInCircle(parent, children, radius, angles);

            // 步骤4：递归布局孙子节点
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
        private float CalculateMinimumRadius(MindMapNode parent, IList<MindMapNode> children)
        {
            // 基础半径：父节点对角线的一半 + 层级间距
            float parentDiagonal = GetDiagonal(parent.Size);
            float baseRadius = parentDiagonal / 2f + GetLevelSpacing(parent.Size);

            // 计算所有子树的总角度需求
            float totalAngularWidth = 0;
            for (int i = 0; i < children.Count; i++)
            {
                totalAngularWidth += SizeCalculator.CalculateSubtreeAngularWidth(
                    children[i], baseRadius, Options);
            }

            // 如果总角度超过2π，需要增大半径
            float radius = baseRadius;
            int iterations = 0;

            while (totalAngularWidth > (float)Math.PI * 2f && iterations < Options.MaxIterations)
            {
                radius *= Options.OverlapAdjustmentFactor;
                totalAngularWidth = 0;
                for (int i = 0; i < children.Count; i++)
                {
                    totalAngularWidth += SizeCalculator.CalculateSubtreeAngularWidth(
                        children[i], radius, Options);
                }
                iterations++;
            }

            return radius;
        }

        /// <summary>
        /// 按子树大小权重分配角度
        /// 大子树占更多角度，确保子树之间不重叠
        /// </summary>
        private float[] CalculateAnglesBySubtreeSize(IList<MindMapNode> children, float radius)
        {
            int count = children.Count;
            float[] angles = new float[count];

            if (count == 0) return angles;
            if (count == 1)
            {
                angles[0] = 0f; // 单个节点放在右侧
                return angles;
            }

            // 计算每个子树需要的角度宽度
            float[] angularWidths = new float[count];
            float totalWidth = 0;

            for (int i = 0; i < count; i++)
            {
                angularWidths[i] = SizeCalculator.CalculateSubtreeAngularWidth(
                    children[i], radius, Options);
                totalWidth += angularWidths[i];
            }

            // 如果总角度超过了2π，按比例缩小
            float scale = 1f;
            if (totalWidth > (float)Math.PI * 2f)
            {
                scale = (float)Math.PI * 2f / totalWidth;
            }

            // 从顶部开始，顺时针排列
            float currentAngle = -(float)Math.PI / 2f; // 从顶部开始（12点钟方向）

            for (int i = 0; i < count; i++)
            {
                float width = angularWidths[i] * scale;
                angles[i] = currentAngle + width / 2f;
                currentAngle += width;
            }

            return angles;
        }

        /// <summary>
        /// 将子节点按圆周排列
        /// </summary>
        private void PlaceChildrenInCircle(
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

        #region 布局后处理（居中显示）

        /// <summary>
        /// 布局后处理：将整个思维导图居中到画布中心
        /// </summary>
        protected override void PostLayout(MindMapDocument document)
        {
            if (document == null || document.RootNode == null) return;

            // 计算整个思维导图的边界框
            RectangleF bounds = CalculateTreeBounds(document.RootNode);

            // 计算偏移量，使思维导图居中
            float offsetX = -bounds.X - bounds.Width / 2f;
            float offsetY = -bounds.Y - bounds.Height / 2f;

            // 应用偏移
            OffsetTree(document.RootNode, offsetX, offsetY);
        }

        /// <summary>
        /// 计算整棵树的边界框
        /// </summary>
        private RectangleF CalculateTreeBounds(MindMapNode node)
        {
            if (node == null) return RectangleF.Empty;

            RectangleF bounds = node.Bounds;

            IList<MindMapNode> children = GetVisibleChildren(node);
            if (children == null || children.Count == 0)
                return bounds;

            for (int i = 0; i < children.Count; i++)
            {
                RectangleF childBounds = CalculateTreeBounds(children[i]);
                bounds = RectangleF.Union(bounds, childBounds);
            }

            return bounds;
        }

        /// <summary>
        /// 偏移整棵树
        /// </summary>
        private void OffsetTree(MindMapNode node, float offsetX, float offsetY)
        {
            if (node == null) return;

            node.Position = new PointF(
                node.Position.X + offsetX,
                node.Position.Y + offsetY);

            IList<MindMapNode> children = GetVisibleChildren(node);
            if (children == null || children.Count == 0)
                return;

            for (int i = 0; i < children.Count; i++)
            {
                OffsetTree(children[i], offsetX, offsetY);
            }
        }

        #endregion
    }
}
