using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【子树大小计算器】
    /// 计算节点子树的整体占用空间，用于布局时预留足够空间
    /// 
    /// 【设计模式】
    /// - Strategy策略模式：可替换不同的计算策略
    /// - SRP单一职责：专门负责子树大小计算
    /// 
    /// 【重要性】
    /// 布局时不能只考虑单个节点大小，必须考虑整个子树的占用空间
    /// 否则会出现子树之间重叠的问题
    /// </summary>
    public class SubtreeSizeCalculator
    {
        #region 单例模式

        private static SubtreeSizeCalculator _instance;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static SubtreeSizeCalculator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SubtreeSizeCalculator();
                return _instance;
            }
        }

        private SubtreeSizeCalculator() { }

        #endregion

        #region 基础大小计算

        /// <summary>
        /// 计算子树的总宽度（水平方向）
        /// 递归计算所有子节点的宽度之和
        /// </summary>
        public SizeF CalculateSubtreeSize(MindMapNode node, LayoutOptions options)
        {
            if (node == null) return SizeF.Empty;

            // 节点本身的大小
            float width = node.Size.Width;
            float height = node.Size.Height;

            // 获取可见子节点
            IList<MindMapNode> children = GetVisibleChildren(node);
            if (children == null || children.Count == 0)
            {
                return new SizeF(width, height);
            }

            // 递归计算所有子树的大小
            float totalChildrenWidth = 0;
            float maxChildHeight = 0;

            for (int i = 0; i < children.Count; i++)
            {
                SizeF childSize = CalculateSubtreeSize(children[i], options);
                totalChildrenWidth += childSize.Width;
                if (childSize.Height > maxChildHeight)
                    maxChildHeight = childSize.Height;

                // 添加子节点之间的间距
                if (i < children.Count - 1)
                {
                    totalChildrenWidth += options.CalculateHorizontalSpacing(children[i].Size);
                }
            }

            // 总宽度取父节点宽度和子节点总宽度的最大值
            float totalWidth = Math.Max(width, totalChildrenWidth);

            // 总高度 = 父节点高度 + 层级间距 + 子节点最大高度
            float levelSpacing = options.CalculateLevelSpacing(node.Size);
            float totalHeight = height + levelSpacing + maxChildHeight;

            return new SizeF(totalWidth, totalHeight);
        }

        /// <summary>
        /// 计算子树在某个角度方向上的投影宽度
        /// 用于放射状布局的角度分配
        /// </summary>
        public float CalculateSubtreeAngularWidth(
            MindMapNode node, 
            float radius, 
            LayoutOptions options)
        {
            if (node == null) return 0;

            // 计算子树的总大小
            SizeF subtreeSize = CalculateSubtreeSize(node, options);

            // 计算在圆周上的投影宽度
            // 使用节点的对角线作为参考
            float subtreeDiagonal = (float)Math.Sqrt(
                subtreeSize.Width * subtreeSize.Width + 
                subtreeSize.Height * subtreeSize.Height);

            // 转换为角度（弧度）
            // 角度 = 2 * arcsin(子树大小 / (2 * 半径))
            if (radius <= 0) radius = 100f;
            
            // 防止参数超出[-1, 1]范围导致Asin返回NaN
            float sinValue = subtreeDiagonal / (2f * radius);
            if (sinValue > 1f) sinValue = 1f;
            if (sinValue < -1f) sinValue = -1f;
            
            float angularWidth = 2f * (float)Math.Asin(sinValue);

            // 添加额外的间距角度
            angularWidth += options.AngularSpacing;

            return Math.Max(angularWidth, 0.1f); // 最小角度
        }

        #endregion

        #region 放射状布局专用计算

        /// <summary>
        /// 计算子节点在放射状布局中需要的角度范围
        /// 返回每个子节点的起始角度和结束角度
        /// </summary>
        public float[] CalculateRadialAngles(
            IList<MindMapNode> children, 
            float radius, 
            float startAngle,
            float totalAngle,
            LayoutOptions options)
        {
            int count = children.Count;
            if (count == 0) return new float[0];
            if (count == 1) return new float[] { startAngle + totalAngle / 2f };

            // 计算每个子树需要的角度宽度
            float[] angularWidths = new float[count];
            float totalWidth = 0;

            for (int i = 0; i < count; i++)
            {
                angularWidths[i] = CalculateSubtreeAngularWidth(children[i], radius, options);
                totalWidth += angularWidths[i];
            }

            // 如果总角度超过了可用角度，需要按比例缩小
            float scale = 1f;
            if (totalWidth > totalAngle)
            {
                scale = totalAngle / totalWidth;
            }

            // 计算每个子节点的中心角度
            float[] angles = new float[count];
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
        /// 计算放射状布局的最小半径
        /// 确保所有子树都能放下且不重叠
        /// </summary>
        public float CalculateMinimumRadius(
            MindMapNode parent,
            IList<MindMapNode> children,
            LayoutOptions options)
        {
            if (children == null || children.Count == 0) return 100f;

            // 基础半径：父节点大小 + 层级间距
            float parentDiagonal = (float)Math.Sqrt(
                parent.Size.Width * parent.Size.Width + 
                parent.Size.Height * parent.Size.Height);
            float baseRadius = parentDiagonal / 2f + options.CalculateLevelSpacing(parent.Size);

            // 计算所有子树的总角度需求
            float totalAngularWidth = 0;
            for (int i = 0; i < children.Count; i++)
            {
                totalAngularWidth += CalculateSubtreeAngularWidth(children[i], baseRadius, options);
            }

            // 如果总角度超过2π，需要增大半径
            float radius = baseRadius;
            int iterations = 0;

            while (totalAngularWidth > (float)Math.PI * 2f && iterations < options.MaxIterations)
            {
                radius *= options.OverlapAdjustmentFactor;
                totalAngularWidth = 0;
                for (int i = 0; i < children.Count; i++)
                {
                    totalAngularWidth += CalculateSubtreeAngularWidth(children[i], radius, options);
                }
                iterations++;
            }

            return radius;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取可见的子节点（考虑折叠状态）
        /// </summary>
        private IList<MindMapNode> GetVisibleChildren(MindMapNode node)
        {
            List<MindMapNode> visibleChildren = new List<MindMapNode>();
            if (node == null || node.ChildNodes == null)
                return visibleChildren;

            foreach (MindMapNode child in node.ChildNodes)
            {
                // 简单处理：假设所有子节点都可见
                // 实际使用时应该检查折叠状态
                visibleChildren.Add(child);
            }

            return visibleChildren;
        }

        /// <summary>
        /// 计算节点的对角线长度
        /// </summary>
        public float GetDiagonal(SizeF size)
        {
            return (float)Math.Sqrt(size.Width * size.Width + size.Height * size.Height);
        }

        #endregion
    }
}
