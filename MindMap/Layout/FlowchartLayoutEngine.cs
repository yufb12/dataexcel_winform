using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;

namespace MindMap.Layout
{
    /// <summary>
    /// 【流程图布局引擎】
    /// 适用场景：工作流程、业务流程、步骤说明、算法流程、用户旅程
    /// 布局特点：节点从上到下按流程顺序排列，形成垂直流程图
    /// 
    /// 【设计模式】
    /// - Template Method - 继承LayoutEngineBase
    /// - Strategy - 通过LayoutOptions配置不同策略
    /// 
    /// 【核心优化】
    /// - 移除所有固定const常数，全部动态计算
    /// - 垂直间距根据节点大小动态调整
    /// - 所有节点居中对齐
    /// - 支持分支流程（子节点横向排列）
    /// </summary>
    public class FlowchartLayoutEngine : LayoutEngineBase
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FlowchartLayoutEngine() : base() { }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        public FlowchartLayoutEngine(LayoutOptions options) : base(options) { }

        #endregion

        #region 模板方法实现

        /// <summary>
        /// 放置根节点（重写）
        /// 流程图：根节点在顶部居中，作为流程起点
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
        /// 子节点横向排列作为分支，每个分支继续向下延伸
        /// </summary>
        protected override void LayoutChildren(MindMapNode parent, int level)
        {
            if (parent == null) return;

            var children = GetVisibleChildren(parent);
            if (children == null || children.Count == 0) return;

            if (children.Count == 1)
            {
                // 单个子节点：直接放在父节点下方，居中对齐
                var child = children[0];
                float x = parent.Center.X - child.Size.Width / 2f;
                float y = parent.Position.Y + parent.Size.Height + GetVerticalSpacing(parent.Size);
                child.Position = new PointF(x, y);

                // 递归布局孙子节点
                LayoutChildren(child, level + 1);
            }
            else
            {
                // 多个子节点：横向排列作为分支
                float totalWidth = CalculateChildrenTotalWidth(children);
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
