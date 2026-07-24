using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Rendering
{
    /// <summary>
    /// 节点渲染器实现
    /// </summary>
    public class NodeRenderer : INodeRenderer, IDisposable
    {
        private readonly GdiResourcePool _resourcePool = new GdiResourcePool();

        /// <summary>
        /// 绘制单个节点
        /// </summary>
        public void DrawNode(Graphics graphics, MindMapNode node, bool isSelected)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (node == null)
                throw new ArgumentNullException("node");

            RectangleF bounds = node.Bounds;
            NodeStyle style = node.Style;

            // 根据节点形状绘制
            switch (style.Shape)
            {
                case NodeShape.RoundedRectangle:
                    DrawRoundedRectangleNode(graphics, bounds, style, isSelected);
                    break;
                case NodeShape.Rectangle:
                    DrawRectangleNode(graphics, bounds, style, isSelected);
                    break;
                case NodeShape.Ellipse:
                    DrawEllipseNode(graphics, bounds, style, isSelected);
                    break;
            }

            // 绘制文本
            DrawNodeText(graphics, node);

            // 绘制选中边框
            if (isSelected)
            {
                DrawSelectionBorder(graphics, bounds);
            }

            // 绘制展开/折叠按钮
            if (node.ChildCount > 0)
            {
                DrawExpandButton(graphics, node);
            }
        }

        /// <summary>
        /// 绘制圆角矩形节点
        /// </summary>
        private void DrawRoundedRectangleNode(Graphics g, RectangleF bounds, NodeStyle style, bool isSelected)
        {
            GraphicsPath path = _resourcePool.CreateRoundedRectangle(bounds, style.CornerRadius);
            
            // 填充背景
            g.FillPath(_resourcePool.GetBrush(style.BackColor), path);
            
            // 绘制边框
            g.DrawPath(_resourcePool.GetPen(style.BorderColor, style.BorderWidth), path);
        }

        /// <summary>
        /// 绘制矩形节点
        /// </summary>
        private void DrawRectangleNode(Graphics g, RectangleF bounds, NodeStyle style, bool isSelected)
        {
            // 填充背景
            g.FillRectangle(_resourcePool.GetBrush(style.BackColor), bounds);
            
            // 绘制边框
            g.DrawRectangle(
                _resourcePool.GetPen(style.BorderColor, style.BorderWidth),
                bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        /// <summary>
        /// 绘制椭圆形节点
        /// </summary>
        private void DrawEllipseNode(Graphics g, RectangleF bounds, NodeStyle style, bool isSelected)
        {
            // 填充背景
            g.FillEllipse(_resourcePool.GetBrush(style.BackColor), bounds);
            
            // 绘制边框
            g.DrawEllipse(
                _resourcePool.GetPen(style.BorderColor, style.BorderWidth),
                bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        /// <summary>
        /// 绘制节点文本
        /// </summary>
        private void DrawNodeText(Graphics g, MindMapNode node)
        {
            RectangleF bounds = node.Bounds;
            float textX = bounds.X + RenderConstants.NodePadding;
            float textY = bounds.Y + (bounds.Height - node.Style.Font.Height) / 2f;
            
            g.DrawString(
                node.Text,
                node.Style.Font,
                _resourcePool.GetBrush(node.Style.ForeColor),
                textX,
                textY);
        }

        /// <summary>
        /// 绘制选中边框
        /// </summary>
        private void DrawSelectionBorder(Graphics g, RectangleF bounds)
        {
            float margin = RenderConstants.SelectionBorderMargin;
            RectangleF selectionRect = new RectangleF(
                bounds.X - margin,
                bounds.Y - margin,
                bounds.Width + margin * 2,
                bounds.Height + margin * 2);

            g.DrawRectangle(
                _resourcePool.GetPen(RenderConstants.SelectionBorderColor, RenderConstants.SelectionBorderWidth),
                selectionRect.X, selectionRect.Y, selectionRect.Width, selectionRect.Height);
        }

        /// <summary>
        /// 绘制展开/折叠按钮
        /// </summary>
        private void DrawExpandButton(Graphics g, MindMapNode node)
        {
            RectangleF btnRect = GetExpandButtonBounds(node);
            
            // 按钮背景
            g.FillEllipse(_resourcePool.GetBrush(Color.White), btnRect);
            
            // 按钮边框
            g.DrawEllipse(_resourcePool.GetPen(Color.Black, 1f), btnRect);
            
            // 横线
            float centerX = btnRect.X + btnRect.Width / 2f;
            float centerY = btnRect.Y + btnRect.Height / 2f;
            float lineHalfLength = btnRect.Width / 2f - 2f;
            
            g.DrawLine(
                _resourcePool.GetPen(Color.Black, 1f),
                centerX - lineHalfLength, centerY,
                centerX + lineHalfLength, centerY);
            
            // 未展开时绘制竖线
            if (!node.IsExpanded)
            {
                g.DrawLine(
                    _resourcePool.GetPen(Color.Black, 1f),
                    centerX, centerY - lineHalfLength,
                    centerX, centerY + lineHalfLength);
            }
        }

        /// <summary>
        /// 绘制节点间连接线
        /// </summary>
        public void DrawConnection(Graphics g, MindMapNode node)
        {
            if (g == null)
                throw new ArgumentNullException("graphics");
            if (node == null)
                throw new ArgumentNullException("node");
            if (node.ParentNode == null)
                return;

            PointF parentCenter = node.ParentNode.Center;
            PointF childCenter = node.Center;

            // 贝塞尔平滑连线
            Pen linePen = _resourcePool.GetPen(RenderConstants.DefaultLineColor, RenderConstants.DefaultLineWidth);
            
            PointF control1 = new PointF(
                parentCenter.X + (childCenter.X - parentCenter.X) / 2f,
                parentCenter.Y);
            PointF control2 = new PointF(
                parentCenter.X + (childCenter.X - parentCenter.X) / 2f,
                childCenter.Y);

            g.DrawBezier(linePen, parentCenter, control1, control2, childCenter);
        }

        /// <summary>
        /// 计算节点的边界矩形
        /// </summary>
        public RectangleF CalculateNodeBounds(Graphics graphics, MindMapNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            return node.Bounds;
        }

        /// <summary>
        /// 获取展开/折叠按钮的边界矩形
        /// </summary>
        public RectangleF GetExpandButtonBounds(MindMapNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            float btnSize = RenderConstants.ExpandButtonSize;
            float spacing = RenderConstants.ExpandButtonSpacing;
            
            return new RectangleF(
                node.Bounds.Right + spacing,
                node.Bounds.Y + (node.Bounds.Height - btnSize) / 2f,
                btnSize,
                btnSize);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_resourcePool != null)
            {
                _resourcePool.Dispose();
            }
        }
    }
}
