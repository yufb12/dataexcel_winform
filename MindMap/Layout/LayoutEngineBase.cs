using System;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 【抽象基类】布局引擎基类
    /// 使用Template Method模板方法模式，定义布局算法的骨架
    /// 子类只需实现特定的布局步骤
    /// 
    /// 【设计模式】
    /// - Template Method模板方法：定义算法骨架，子类实现具体步骤
    /// - Strategy策略模式：通过LayoutOptions可配置不同布局策略
    /// 
    /// 【设计原则】
    /// - SRP单一职责：只负责布局流程控制，具体布局由子类实现
    /// - OCP开闭原则：新增布局只需继承此类，不修改原有代码
    /// - DIP依赖倒置：依赖抽象的LayoutOptions，不依赖具体常量
    /// 
    /// 【重构优化】
    /// - 移除所有固定const常数，使用动态计算
    /// - 集成LayoutOptions配置对象
    /// - 集成SubtreeSizeCalculator子树大小计算器
    /// - 支持子树大小感知的布局
    /// </summary>
    public abstract class LayoutEngineBase : ILayoutEngine
    {
        #region 布局配置

        private LayoutOptions _options;

        /// <summary>
        /// 布局参数配置
        /// </summary>
        public LayoutOptions Options
        {
            get { return _options; }
            set { _options = value ?? LayoutOptions.CreateDefault(); }
        }

        /// <summary>
        /// 子树大小计算器
        /// </summary>
        protected SubtreeSizeCalculator SizeCalculator
        {
            get { return SubtreeSizeCalculator.Instance; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected LayoutEngineBase()
        {
            _options = LayoutOptions.CreateDefault();
        }

        /// <summary>
        /// 带配置的构造函数
        /// </summary>
        protected LayoutEngineBase(LayoutOptions options)
        {
            _options = options ?? LayoutOptions.CreateDefault();
        }

        #endregion

        #region 模板方法（算法骨架）

        /// <summary>
        /// 执行布局（模板方法）
        /// 定义布局算法的固定流程
        /// </summary>
        public void Layout(MindMapDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (document.RootNode == null)
                return;

            // 步骤1：布局前准备（测量节点大小等）
            PrepareLayout(document);

            // 步骤2：放置根节点
            PlaceRootNode(document.RootNode);

            // 步骤3：递归布局子节点（由子类实现）
            LayoutChildren(document.RootNode, 1);

            // 步骤4：布局后处理（调整位置、居中显示等）
            PostLayout(document);
        }

        #endregion

        #region 可重写的步骤方法

        /// <summary>
        /// 布局前准备（可重写）
        /// 默认实现：自动计算所有节点大小
        /// </summary>
        protected virtual void PrepareLayout(MindMapDocument document)
        {
            // 自动计算所有节点大小
            CalculateAllNodeSizes(document.RootNode);
        }

        /// <summary>
        /// 放置根节点（可重写）
        /// 默认实现：根节点在原点
        /// </summary>
        protected virtual void PlaceRootNode(MindMapNode root)
        {
            root.Position = new PointF(0f, 0f);
        }

        /// <summary>
        /// 布局子节点（抽象方法，必须由子类实现）
        /// 这是Template Method的核心扩展点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="level">当前层级（从1开始）</param>
        protected abstract void LayoutChildren(MindMapNode parent, int level);

        /// <summary>
        /// 布局后处理（可重写）
        /// 默认实现：不做任何处理
        /// </summary>
        protected virtual void PostLayout(MindMapDocument document)
        {
            // 默认不做处理，子类可重写
        }

        #endregion

        #region 公共工具方法（子类可直接使用）

        /// <summary>
        /// 递归计算所有节点的大小
        /// </summary>
        protected void CalculateAllNodeSizes(MindMapNode node)
        {
            if (node == null) return;

            node.AutoCalculateSize();

            if (node.ChildNodes != null)
            {
                foreach (MindMapNode child in node.ChildNodes)
                {
                    CalculateAllNodeSizes(child);
                }
            }
        }

        /// <summary>
        /// 检查节点是否有子节点（考虑折叠状态）
        /// </summary>
        protected bool HasVisibleChildren(MindMapNode node)
        {
            if (node == null) return false;
            if (node.ChildNodes == null || node.ChildNodes.Count == 0) return false;

            // 检查是否有任何展开方向的子节点
            foreach (NodeDirection direction in Enum.GetValues(typeof(NodeDirection)))
            {
                if (node.HasChildrenInDirection(direction) && node.IsExpandedInDirection(direction))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取可见的子节点列表（考虑折叠状态）
        /// </summary>
        protected System.Collections.Generic.IList<MindMapNode> GetVisibleChildren(MindMapNode node)
        {
            System.Collections.Generic.List<MindMapNode> visibleChildren = 
                new System.Collections.Generic.List<MindMapNode>();

            if (node == null || node.ChildNodes == null)
                return visibleChildren;

            foreach (MindMapNode child in node.ChildNodes)
            {
                // 检查子节点所在的方向是否展开
                NodeDirection? direction = GetChildDirection(node, child);
                if (direction.HasValue && node.IsExpandedInDirection(direction.Value))
                {
                    visibleChildren.Add(child);
                }
            }

            return visibleChildren;
        }

        /// <summary>
        /// 获取子节点相对于父节点的方向
        /// </summary>
        protected NodeDirection? GetChildDirection(MindMapNode parent, MindMapNode child)
        {
            if (parent == null || child == null) return null;

            // 优先使用Connection.Direction
            if (child.ParentConnection != null && child.ParentConnection.Direction != NodeDirection.Right)
            {
                return child.ParentConnection.Direction;
            }

            // 动态计算方向（基于位置）
            float dx = child.Center.X - parent.Center.X;
            float dy = child.Center.Y - parent.Center.Y;

            // 如果位置相同，默认右侧
            if (Math.Abs(dx) < 0.01f && Math.Abs(dy) < 0.01f)
                return NodeDirection.Right;

            float angle = (float)Math.Atan2(dy, dx) * 180f / (float)Math.PI;

            if (angle >= -45f && angle < 45f)
                return NodeDirection.Right;
            else if (angle >= 45f && angle < 135f)
                return NodeDirection.Bottom;
            else if (angle >= 135f || angle < -135f)
                return NodeDirection.Left;
            else
                return NodeDirection.Top;
        }

        #endregion

        #region 动态间距计算方法（替代const常量）

        /// <summary>
        /// 根据节点大小计算水平间距
        /// </summary>
        protected float GetHorizontalSpacing(SizeF nodeSize)
        {
            return _options.CalculateHorizontalSpacing(nodeSize);
        }

        /// <summary>
        /// 根据节点大小计算垂直间距
        /// </summary>
        protected float GetVerticalSpacing(SizeF nodeSize)
        {
            return _options.CalculateVerticalSpacing(nodeSize);
        }

        /// <summary>
        /// 根据父节点大小计算层级间距
        /// </summary>
        protected float GetLevelSpacing(SizeF parentSize)
        {
            return _options.CalculateLevelSpacing(parentSize);
        }

        /// <summary>
        /// 根据节点大小计算径向间距
        /// </summary>
        protected float GetRadialSpacing(SizeF nodeSize)
        {
            return _options.CalculateRadialSpacing(nodeSize);
        }

        /// <summary>
        /// 计算节点的对角线长度
        /// </summary>
        protected float GetDiagonal(SizeF size)
        {
            return (float)Math.Sqrt(size.Width * size.Width + size.Height * size.Height);
        }

        #endregion
    }
}
