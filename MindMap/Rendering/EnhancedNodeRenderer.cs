using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Rendering
{
    /// <summary>
    /// XMind专业级节点渲染器 v1.8.1（接口兼容修复版）
    /// 
    /// 【架构重构 - Facade外观模式】
    /// - 本类作为外观门面，统一对外提供渲染接口
    /// - 实际职责委托给各个专用渲染器（符合SRP单一职责）
    /// 
    /// 【v1.8.1修复】
    /// - 严格匹配 INodeRenderer 接口签名
    /// - 修复箭头方向：父→子，箭头指向子节点
    /// - 添加所有缺失的公共方法（CalculateNodeBounds, Dispose, SetHighQualityRendering）
    /// </summary>
    public class EnhancedNodeRenderer : INodeRenderer, IDisposable
    {
        #region 字段
        private Theme _theme;
        private bool _disposed;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化增强节点渲染器
        /// </summary>
        public EnhancedNodeRenderer()
        {
            _theme = Theme.CreateDefaultTheme();
            _disposed = false;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置主题
        /// </summary>
        public Theme Theme
        {
            get { return _theme; }
            set { _theme = value ?? Theme.CreateDefaultTheme(); }
        }
        #endregion

        #region INodeRenderer 接口实现（严格匹配签名）

        /// <summary>
        /// 绘制单个节点（接口方法）
        /// </summary>
        /// <summary>
        public void DrawNode(Graphics graphics, MindMapNode node, bool isSelected)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (node == null) throw new ArgumentNullException("node");
            if (_disposed) throw new ObjectDisposedException("EnhancedNodeRenderer");

            SetHighQualityRendering(graphics);

            RectangleF bounds = CalculateNodeBounds(graphics, node);
            node.Bounds = bounds;

            // 1. 绘制节点主体（含图片节点）
            using (GraphicsPath path = NodeBodyRenderer.DrawNodeBody(graphics, bounds, node))
            {
                // 2. 绘制节点阴影
                ShadowRenderer.DrawNodeShadow(graphics, path);

                // 3. 选中状态发光
                if (isSelected)
                {
                    if (path != null)
                    {
                        ShadowRenderer.DrawSelectionGlow(graphics, path, Color.RoyalBlue);
                    }
                    else if (node.Style.Shape == NodeShape.Underline)
                    {
                        // v2.1.7.2：下划线样式特殊选中效果（在文本周围绘制矩形选中框）
                        DrawUnderlineSelection(graphics, bounds);
                    }
                }
            }

            // 4. 绘制节点图标+文本（v1.9新增：支持XMind风格图标+文本并排）
            NodeBodyRenderer.DrawNodeIconAndText(graphics, bounds, node);

            // 5. v2.3：分方向绘制展开按钮（有子节点的方向才显示按钮）
            foreach (NodeDirection direction in System.Enum.GetValues(typeof(NodeDirection)))
            {
                if (node.HasChildrenInDirection(direction))
                {
                    NodeBodyRenderer.DrawExpandButton(
                        graphics, 
                        bounds, 
                        direction, 
                        node.IsExpandedInDirection(direction));
                }
            }
        }
        /// 绘制节点间连接线（接口方法）
        /// 【箭头方向修复】：箭头从父节点指向子节点
        /// </summary>
        public void DrawConnection(Graphics graphics, MindMapNode node)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (node == null) throw new ArgumentNullException("node");
            if (node.ParentNode == null) return;
            if (_disposed) throw new ObjectDisposedException("EnhancedNodeRenderer");

            SetHighQualityRendering(graphics);

            MindMapNode parentNode = node.ParentNode;
            MindMapNode childNode = node;

            RectangleF parentBounds = CalculateNodeBounds(graphics, parentNode);
            RectangleF childBounds = CalculateNodeBounds(graphics, childNode);

            // ========== 连接点计算（父子双向动态连接点 v2.1.4） ==========
            // 【父节点作为起点】→ 根据子节点位置自动选择父节点边缘
            // - 子节点在父节点右侧 → 父节点右边缘
            // - 子节点在父节点左侧 → 父节点左边缘
            // - 子节点在父节点下方 → 父节点下边缘
            // - 子节点在父节点上方 → 父节点上边缘
            PointF start = ConnectionRenderer.CalculateDynamicConnectionPoint(
                parentBounds, childBounds, node.ParentConnection.ParentConnectionPoint, true);

            // 【子节点作为终点】→ 根据父节点位置自动选择子节点边缘（双向对称）
            // - 父节点在子节点右侧 → 子节点右边缘
            // - 父节点在子节点左侧 → 子节点左边缘
            // - 父节点在子节点下方 → 子节点下边缘
            // - 父节点在子节点上方 → 子节点上边缘
            PointF end = ConnectionRenderer.CalculateDynamicConnectionPoint(
                childBounds, parentBounds, node.ParentConnection.ChildConnectionPoint, false);

            // v2.1.7：Underline形状时，连线连到底部横线上（childBounds.Bottom - 4f）
            if (childNode.Style.Shape == NodeShape.Underline)
            {
                end = new PointF(end.X, childBounds.Bottom - 4f);
            }
            // ========== 绘制连接线 + 箭头 ==========
            // 箭头方向：从start（父）指向end（子），箭头在end点
            // v2.1.7.3：连接线属性已移到Connection类，使用默认值
            ConnectionRenderer.DrawConnectionWithArrow(
                graphics, start, end,
                node.ParentConnection.LineType,
                node.ParentConnection.LineColor,
                node.ParentConnection.LineWidth
            );
        }

        /// <summary>
        /// 获取展开按钮边界（接口方法）
        /// </summary>
        public RectangleF GetExpandButtonBounds(MindMapNode node)
        {
            if (node == null) throw new ArgumentNullException("node");
            if (_disposed) throw new ObjectDisposedException("EnhancedNodeRenderer");

            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                RectangleF nodeBounds = CalculateNodeBounds(g, node);
                return NodeBodyRenderer.GetExpandButtonBounds(nodeBounds);
            }
        }

        #endregion

        #region 公共方法（MindMapView调用）

        /// <summary>
        /// 计算节点边界（MindMapView.Selection对齐功能需要）
        /// </summary>
        public RectangleF CalculateNodeBounds(Graphics graphics, MindMapNode node)
        {
            return NodeBodyRenderer.CalculateNodeBounds(graphics, node);
        }

        /// <summary>
        /// 设置高清渲染模式（公共方法，MindMapView调用）
        /// </summary>
        public void SetHighQualityRendering(Graphics graphics)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
        }

        #endregion

        #region 特殊形状选中效果（v2.1.7.2新增）
        /// <summary>
        /// 绘制下划线样式节点的选中效果
        /// </summary>
        private static void DrawUnderlineSelection(Graphics graphics, RectangleF bounds)
        {
            // 绘制两层选中发光效果
            RectangleF selectBounds = new RectangleF(
                bounds.X - 3f,
                bounds.Y - 3f,
                bounds.Width + 6f,
                bounds.Height + 6f);

            // 外层光晕
            using (Pen glowPen1 = new Pen(Color.FromArgb(50, Color.RoyalBlue), 4f))
            {
                glowPen1.LineJoin = LineJoin.Round;
                graphics.DrawRectangle(glowPen1, selectBounds.X, selectBounds.Y, selectBounds.Width, selectBounds.Height);
            }

            // 内层光晕
            using (Pen glowPen2 = new Pen(Color.FromArgb(25, Color.RoyalBlue), 2f))
            {
                glowPen2.LineJoin = LineJoin.Round;
                graphics.DrawRectangle(glowPen2, selectBounds.X, selectBounds.Y, selectBounds.Width, selectBounds.Height);
            }

            // 内边框
            using (Pen borderPen = new Pen(Color.FromArgb(100, Color.RoyalBlue), 2f))
            {
                borderPen.LineJoin = LineJoin.Round;
                graphics.DrawRectangle(borderPen, selectBounds.X, selectBounds.Y, selectBounds.Width, selectBounds.Height);
            }
        }
        #endregion

        #region IDisposable 实现

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源（受保护方法）
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~EnhancedNodeRenderer()
        {
            Dispose(false);
        }

        #endregion
    }
}
