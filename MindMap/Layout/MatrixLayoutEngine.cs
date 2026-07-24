using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【矩阵布局引擎】
    /// 适用场景：分类对比、四象限分析、矩阵图、SWOT分析、产品对比
    /// 布局特点：节点按二维矩阵排列，行列整齐，适合对比分析
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 自动计算列数，使矩阵接近正方形
    /// - 水平间距根据节点大小动态调整
    /// - 垂直间距根据节点大小动态调整
    /// </summary>
    public class MatrixLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MatrixLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public MatrixLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 矩阵布局：根节点在顶部居中
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
        /// 按二维矩阵排列子节点
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            int count = children.Count;

            // 计算列数（使矩阵接近正方形）
            int columns = (int)Math.Ceiling(Math.Sqrt(count));
            if (columns < 1) columns = 1;
            int rows = (int)Math.Ceiling((double)count / columns);

            // 计算最大节点大小
            float maxWidth = 0f;
            float maxHeight = 0f;
            for (int i = 0; i < count; i++)
            {
                if (children[i].Size.Width > maxWidth)
                    maxWidth = children[i].Size.Width;
                if (children[i].Size.Height > maxHeight)
                    maxHeight = children[i].Size.Height;
            }

            // 计算单元格大小（包含间距）
            float cellWidth = maxWidth + GetHorizontalSpacing(new SizeF(maxWidth, maxHeight));
            float cellHeight = maxHeight + GetVerticalSpacing(new SizeF(maxWidth, maxHeight));

            // 计算总宽度和总高度
            float totalWidth = columns * cellWidth - GetHorizontalSpacing(new SizeF(maxWidth, maxHeight));
            float totalHeight = rows * cellHeight - GetVerticalSpacing(new SizeF(maxWidth, maxHeight));

            // 起始位置（居中对齐到父节点）
            float startX = parent.Center.X - totalWidth / 2f;
            float startY = parent.Position.Y + parent.Size.Height + GetVerticalSpacing(parent.Size);

            // 按矩阵排列节点
            for (int i = 0; i < count; i++)
            {
                int row = i / columns;
                int col = i % columns;

                float x = startX + col * cellWidth;
                float y = startY + row * cellHeight;

                // 节点在单元格内居中
                x += (maxWidth - children[i].Size.Width) / 2f;
                y += (maxHeight - children[i].Size.Height) / 2f;

                children[i].Position = new PointF(x, y);

                // 递归布局孙子节点
                LayoutChildren(children[i], level + 1);
            }
        }

        #endregion
    }
}
