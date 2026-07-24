using System;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 【优化版】树状布局引擎（组织结构图模式）
    /// 根节点在顶部，子节点向下展开
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
    /// 1. 递归计算每个子树的总宽度
    /// 2. 父节点居中对齐到子树中心
    /// 3. 子节点水平排列，间距根据节点大小动态计算
    /// 4. 垂直间距根据节点大小动态计算
    /// </summary>
    public class TreeLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TreeLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public TreeLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 树状布局：根节点在顶部居中
        /// </summary>
        protected override void PlaceRootNode(MindMapNode root)
        {
            // 根节点在顶部居中
            root.Position = new PointF(
                -root.Size.Width / 2f,
                -root.Size.Height - 50f); // 向上偏移一些
        }

        /// <summary>
        /// 布局子节点（核心实现）
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            // 计算所有子树的总宽度
            float totalWidth = CalculateSubtreeTotalWidth(parent);

            // 第一个子节点的起始X位置（居中对齐到父节点）
            float startX = parent.Center.X - totalWidth / 2f;
            float startY = parent.Position.Y + parent.Size.Height + GetVerticalSpacing(parent.Size);

            float currentX = startX;

            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                float subtreeWidth = CalculateSubtreeWidth(child);

                // 子节点在其子树范围内居中
                float childX = currentX + subtreeWidth / 2f - child.Size.Width / 2f;
                child.Position = new PointF(childX, startY);

                // 递归布局孙子节点
                LayoutChildren(child, level + 1);

                // 移动到下一个子树位置
                currentX += subtreeWidth + GetHorizontalSpacing(child.Size);
            }
        }

        #endregion

        #region 子树宽度计算

        /// <summary>
        /// 计算节点及其所有子节点的总宽度（包含间距）
        /// </summary>
        private float CalculateSubtreeTotalWidth(MindMapNode node)
        {
            if (node == null) return 0f;

            var children = GetVisibleChildren(node);
            if (children == null || children.Count == 0)
                return node.Size.Width;

            float totalWidth = 0f;

            for (int i = 0; i < children.Count; i++)
            {
                totalWidth += CalculateSubtreeWidth(children[i]);
                if (i < children.Count - 1)
                    totalWidth += GetHorizontalSpacing(children[i].Size);
            }

            return Math.Max(node.Size.Width, totalWidth);
        }

        /// <summary>
        /// 计算单个子树的宽度
        /// </summary>
        private float CalculateSubtreeWidth(MindMapNode node)
        {
            if (node == null) return 0f;

            var children = GetVisibleChildren(node);
            if (children == null || children.Count == 0)
                return node.Size.Width;

            float totalWidth = 0f;

            for (int i = 0; i < children.Count; i++)
            {
                totalWidth += CalculateSubtreeWidth(children[i]);
                if (i < children.Count - 1)
                    totalWidth += GetHorizontalSpacing(children[i].Size);
            }

            return Math.Max(node.Size.Width, totalWidth);
        }

        #endregion
    }
}
