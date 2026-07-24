using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【漏斗布局引擎】
    /// 适用场景：转化漏斗、销售流程、筛选流程、用户旅程、数据分析
    /// 布局特点：根节点在顶部最宽，子节点逐层向下收窄，形成漏斗形状
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 每层宽度按比例递减，形成漏斗效果
    /// - 水平间距根据节点大小动态调整
    /// - 垂直间距根据节点大小动态调整
    /// </summary>
    public class FunnelLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FunnelLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public FunnelLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 漏斗布局：根节点在顶部居中，最宽
        /// </summary>
        protected override void PlaceRootNode(MindMapNode root)
        {
            // 根节点在顶部居中
            root.Position = new PointF(
                -root.Size.Width / 2f,
                0f);
        }

        /// <summary>
        /// 布局子节点（核心实现）
        /// 从上到下逐层布局，每层宽度按比例递减
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            // 计算漏斗收缩比例（层级越深，宽度越窄）
            float shrinkRatio = 1f - level * 0.15f;
            if (shrinkRatio < 0.4f) shrinkRatio = 0.4f; // 最小宽度比例

            // 计算所有子节点的总宽度
            float totalWidth = CalculateChildrenTotalWidth(children);
            float availableWidth = parent.Size.Width * shrinkRatio;

            // 如果子节点总宽度超过可用宽度，按比例缩小间距
            float spacingScale = 1f;
            if (totalWidth > availableWidth && children.Count > 1)
            {
                float spacingTotal = totalWidth - SumChildrenWidth(children);
                float availableSpacing = availableWidth - SumChildrenWidth(children);
                if (spacingTotal > 0)
                    spacingScale = availableSpacing / spacingTotal;
            }

            // 第一个子节点的起始X位置（居中对齐到父节点）
            float startX = parent.Center.X - totalWidth * spacingScale / 2f;
            float startY = parent.Position.Y + parent.Size.Height + GetVerticalSpacing(parent.Size);

            float currentX = startX;

            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                child.Position = new PointF(currentX, startY);

                // 递归布局孙子节点
                LayoutChildren(child, level + 1);

                // 移动到下一个子节点位置
                currentX += child.Size.Width + GetHorizontalSpacing(child.Size) * spacingScale;
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 计算所有子节点的总宽度（包含间距）
        /// </summary>
        private float CalculateChildrenTotalWidth(IList<MindMapNode> children)
        {
            if (children == null || children.Count == 0) return 0f;

            float totalWidth = 0f;

            for (int i = 0; i < children.Count; i++)
            {
                totalWidth += children[i].Size.Width;
                if (i < children.Count - 1)
                    totalWidth += GetHorizontalSpacing(children[i].Size);
            }

            return totalWidth;
        }

        /// <summary>
        /// 计算子节点宽度之和（不含间距）
        /// </summary>
        private float SumChildrenWidth(IList<MindMapNode> children)
        {
            if (children == null || children.Count == 0) return 0f;

            float sum = 0f;
            for (int i = 0; i < children.Count; i++)
            {
                sum += children[i].Size.Width;
            }
            return sum;
        }

        #endregion
    }
}
