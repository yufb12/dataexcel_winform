using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MindMap.Core;
using MindMap.Rendering;

namespace MindMap.View
{
    /// <summary>
    /// MindMapView - 绘制渲染部分（SRP：单一职责原则）
    /// 职责：所有绘制相关逻辑
    /// </summary>
    partial class MindMapView
    {
        /// <summary>
        /// 重写OnPaint方法，绘制整个思维导图
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                if (_document == null) return;

                Graphics graphics = e.Graphics;
                _renderer.SetHighQualityRendering(graphics);

                // 应用视图变换（缩放 + 平移）
                _document.ViewSettings.ApplyTransform(graphics);

                // 1. 收集所有可见节点（折叠的子节点不收集）
                List<MindMapNode> allNodes = new List<MindMapNode>();
                CollectAllNodes(_document.RootNode, allNodes);

                // 2. 按Z-Order排序（从下到上绘制）
                allNodes.Sort((a, b) => a.ZOrder.CompareTo(b.ZOrder));

                // 3. 绘制所有连接线（先于节点，避免覆盖节点）
                DrawConnections(graphics, _document.RootNode);

                // 4. 绘制所有节点
                foreach (MindMapNode node in allNodes)
                {
                    bool isSelected = _document.IsNodeSelected(node);
                    _renderer.DrawNode(graphics, node, isSelected);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("绘制异常: " + ex.Message);
            }
        }

        /// <summary>
        /// 递归收集所有可见节点（折叠方向的子节点不收集）
        /// </summary>
        private void CollectAllNodes(MindMapNode node, List<MindMapNode> nodes)
        {
            if (node == null) return;
            nodes.Add(node);

            // v2.3：只递归渲染展开方向的子节点
            foreach (Connection conn in node.GetAllExpandedChildConnections())
            {
                CollectAllNodes(conn.ChildNode, nodes);
            }
        }

        /// <summary>
        /// 绘制所有连接线（折叠的子节点不绘制）
        /// </summary>
        private void DrawConnections(Graphics graphics, MindMapNode node)
        {
            if (node == null) return;

            // v2.3：只绘制展开方向的子节点连接线
            foreach (Connection conn in node.GetAllExpandedChildConnections())
            {
                MindMapNode child = conn.ChildNode;
                _renderer.DrawConnection(graphics, child);

                // v2.1.7.2：绘制选中连线的高亮效果
                if (_document != null && _document.SelectedConnection != null &&
                    _document.SelectedConnection.ChildNode == child &&
                    _document.SelectedConnection.ParentNode == node)
                {
                    DrawSelectedConnectionHighlight(graphics, node, child);
                }

                DrawConnections(graphics, child);
            }
        }

        /// <summary>
        /// 绘制选中连线的高亮效果（v2.1.7.2新增）
        /// </summary>
        private void DrawSelectedConnectionHighlight(Graphics graphics, MindMapNode parentNode, MindMapNode childNode)
        {
            RectangleF parentBounds = _renderer.CalculateNodeBounds(graphics, parentNode);
            RectangleF childBounds = _renderer.CalculateNodeBounds(graphics, childNode);

            PointF start = ConnectionRenderer.CalculateDynamicConnectionPoint(
                parentBounds, childBounds, ConnectionPoint.Auto, true);
            PointF end = ConnectionRenderer.CalculateDynamicConnectionPoint(
                childBounds, parentBounds, ConnectionPoint.Auto, false);

            // 6px粗的半透明蓝色高亮
            using (Pen highlightPen = new Pen(Color.FromArgb(100, 0, 120, 215), 6f))
            {
                highlightPen.StartCap = LineCap.Round;
                highlightPen.EndCap = LineCap.Round;
                highlightPen.LineJoin = LineJoin.Round;
                graphics.DrawLine(highlightPen, start, end);
            }
        }
    }
}
