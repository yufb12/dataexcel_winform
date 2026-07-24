using System;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 【优化版】左右布局引擎（标准思维导图模式）
    /// 根节点在左侧，所有分支向右展开
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 水平间距根据节点大小动态调整
    /// - 垂直间距根据节点大小动态调整
    /// - 支持子树大小感知的布局
    /// 
    /// 【算法原理】
    /// 1. 递归计算每个子树的总高度
    /// 2. 父节点居中对齐到子树中心
    /// 3. 子节点垂直排列，间距根据节点大小动态计算
    /// 4. 水平间距根据节点大小动态计算
    /// </summary>
    public class LeftRightLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LeftRightLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public LeftRightLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 左右布局：根节点在左侧居中
        /// </summary>
        protected override void PlaceRootNode(MindMapNode root)
        {
            // 根节点在左侧居中
            root.Position = new PointF(
                -root.Size.Width - 50f, // 向左偏移一些
                -root.Size.Height / 2f);
        }

        /// <summary>
        /// 布局子节点（核心实现）
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            // 计算所有子树的总高度
            float totalHeight = CalculateSubtreeTotalHeight(parent);

            // 第一个子节点的起始Y位置（居中对齐到父节点）
            float startY = parent.Center.Y - totalHeight / 2f;
            float startX = parent.Position.X + parent.Size.Width + GetHorizontalSpacing(parent.Size);

            float currentY = startY;

            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                float subtreeHeight = CalculateSubtreeHeight(child);

                // 子节点在其子树范围内居中
                float childY = currentY + subtreeHeight / 2f - child.Size.Height / 2f;
                child.Position = new PointF(startX, childY);

                // 递归布局孙子节点
                LayoutChildren(child, level + 1);

                // 移动到下一个子树位置
                currentY += subtreeHeight + GetVerticalSpacing(child.Size);
            }
        }

        #endregion

        #region 子树高度计算

        /// <summary>
        /// 计算节点及其所有子节点的总高度（包含间距）
        /// </summary>
        private float CalculateSubtreeTotalHeight(MindMapNode node)
        {
            if (node == null) return 0f;

            var children = GetVisibleChildren(node);
            if (children == null || children.Count == 0)
                return node.Size.Height;

            float totalHeight = 0f;

            for (int i = 0; i < children.Count; i++)
            {
                totalHeight += CalculateSubtreeHeight(children[i]);
                if (i < children.Count - 1)
                    totalHeight += GetVerticalSpacing(children[i].Size);
            }

            return Math.Max(node.Size.Height, totalHeight);
        }

        /// <summary>
        /// 计算单个子树的高度
        /// </summary>
        private float CalculateSubtreeHeight(MindMapNode node)
        {
            if (node == null) return 0f;

            var children = GetVisibleChildren(node);
            if (children == null || children.Count == 0)
                return node.Size.Height;

            float totalHeight = 0f;

            for (int i = 0; i < children.Count; i++)
            {
                totalHeight += CalculateSubtreeHeight(children[i]);
                if (i < children.Count - 1)
                    totalHeight += GetVerticalSpacing(children[i].Size);
            }

            return Math.Max(node.Size.Height, totalHeight);
        }

        #endregion
    }
}
