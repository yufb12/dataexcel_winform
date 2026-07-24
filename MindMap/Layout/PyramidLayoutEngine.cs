using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【金字塔布局引擎】
    /// 适用场景：层级结构、优先级展示、重要性递减、组织架构
    /// 布局特点：根节点在顶部，子节点逐层向下展开，形成金字塔形状
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 水平间距根据节点大小动态调整
    /// - 垂直间距根据节点大小动态调整
    /// - 每层居中对齐，形成金字塔效果
    /// </summary>
    public class PyramidLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PyramidLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public PyramidLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 金字塔布局：根节点在顶部居中
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
        /// 从上到下逐层布局，每层居中对齐
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            // 计算所有子节点的总宽度
            float totalWidth = CalculateChildrenTotalWidth(children);

            // 第一个子节点的起始X位置（居中对齐到父节点）
            float startX = parent.Center.X - totalWidth / 2f;
            float startY = parent.Position.Y + parent.Size.Height + GetVerticalSpacing(parent.Size);

            float currentX = startX;

            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                child.Position = new PointF(currentX, startY);

                // 递归布局孙子节点
                LayoutChildren(child, level + 1);

                // 移动到下一个子节点位置
                currentX += child.Size.Width + GetHorizontalSpacing(child.Size);
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

        #endregion
    }
}
