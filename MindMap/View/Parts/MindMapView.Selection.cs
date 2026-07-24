using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MindMap.Core;
using MindMap.Rendering;

namespace MindMap.View
{
    /// <summary>
    /// MindMapView - 多选与对齐部分（SRP：单一职责原则）
    /// 职责：多选、框选、对齐、层级管理
    /// </summary>
    public partial class MindMapView : Control
    {
        #region 多选核心方法
        /// <summary>
        /// 对所有选中节点应用操作
        /// </summary>
        public void ApplyToSelectedNodes(Action<MindMapNode> action)
        {
            if (_document == null || action == null) return;
            foreach (MindMapNode node in _document.SelectedNodes)
            {
                action(node);
            }
        }

        /// <summary>
        /// 对齐选中节点（11种对齐方式）
        /// </summary>
        public void AlignSelectedNodes(AlignmentType type)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            List<MindMapNode> selectedNodes = new List<MindMapNode>(_document.SelectedNodes);
            float minLeft = float.MaxValue;
            float maxRight = float.MinValue;
            float minTop = float.MaxValue;
            float maxBottom = float.MinValue;
            float maxWidth = 0;
            float maxHeight = 0;

            using (Graphics g = CreateGraphics())
            {
                // 计算边界
                foreach (MindMapNode node in selectedNodes)
                {
                    RectangleF bounds = _renderer.CalculateNodeBounds(g, node);
                    minLeft = Math.Min(minLeft, bounds.Left);
                    maxRight = Math.Max(maxRight, bounds.Right);
                    minTop = Math.Min(minTop, bounds.Top);
                    maxBottom = Math.Max(maxBottom, bounds.Bottom);
                    maxWidth = Math.Max(maxWidth, bounds.Width);
                    maxHeight = Math.Max(maxHeight, bounds.Height);
                }

                float centerX = (minLeft + maxRight) / 2f;
                float centerY = (minTop + maxBottom) / 2f;

                // 执行对齐
                foreach (MindMapNode node in selectedNodes)
                {
                    RectangleF bounds = _renderer.CalculateNodeBounds(g, node);
                    PointF oldPos = node.Position;

                    switch (type)
                    {
                        case AlignmentType.Left:
                            node.Position = new PointF(minLeft, oldPos.Y);
                            break;
                        case AlignmentType.CenterHorizontal:
                            node.Position = new PointF(centerX - bounds.Width / 2f, oldPos.Y);
                            break;
                        case AlignmentType.Right:
                            node.Position = new PointF(maxRight - bounds.Width, oldPos.Y);
                            break;
                        case AlignmentType.Top:
                            node.Position = new PointF(oldPos.X, minTop);
                            break;
                        case AlignmentType.CenterVertical:
                            node.Position = new PointF(oldPos.X, centerY - bounds.Height / 2f);
                            break;
                        case AlignmentType.Bottom:
                            node.Position = new PointF(oldPos.X, maxBottom - bounds.Height);
                            break;
                    }
                }

                // 等间距分布
                if (type == AlignmentType.DistributeHorizontal && selectedNodes.Count >= 3)
                {
                    selectedNodes.Sort((a, b) => a.Position.X.CompareTo(b.Position.X));
                    float totalSpace = maxRight - minLeft;
                    float nodeWidths = 0;
                    foreach (MindMapNode node in selectedNodes)
                        nodeWidths += _renderer.CalculateNodeBounds(g, node).Width;
                    float gap = (totalSpace - nodeWidths) / (selectedNodes.Count - 1);
                    float currentX = minLeft;
                    foreach (MindMapNode node in selectedNodes)
                    {
                        node.Position = new PointF(currentX, node.Position.Y);
                        currentX += _renderer.CalculateNodeBounds(g, node).Width + gap;
                    }
                }

                if (type == AlignmentType.DistributeVertical && selectedNodes.Count >= 3)
                {
                    selectedNodes.Sort((a, b) => a.Position.Y.CompareTo(b.Position.Y));
                    float totalSpace = maxBottom - minTop;
                    float nodeHeights = 0;
                    foreach (MindMapNode node in selectedNodes)
                        nodeHeights += _renderer.CalculateNodeBounds(g, node).Height;
                    float gap = (totalSpace - nodeHeights) / (selectedNodes.Count - 1);
                    float currentY = minTop;
                    foreach (MindMapNode node in selectedNodes)
                    {
                        node.Position = new PointF(node.Position.X, currentY);
                        currentY += _renderer.CalculateNodeBounds(g, node).Height + gap;
                    }
                }
            }
        }
        #endregion

        #region 框选方法
        /// <summary>
        /// 选择指定矩形内的所有节点（v2.1.7.2新增，用于框选）
        /// </summary>
        public void SelectNodesInRect(RectangleF docRect)
        {
            if (_document == null) return;

            _document.ClearSelection();

            using (Graphics g = CreateGraphics())
            {
                SelectNodesInRectRecursive(_document.RootNode, docRect, g);
            }
        }

        /// <summary>
        /// 递归选择矩形内的节点
        /// </summary>
        private void SelectNodesInRectRecursive(MindMapNode node, RectangleF docRect, Graphics g)
        {
            RectangleF nodeBounds = _renderer.CalculateNodeBounds(g, node);
            if (docRect.IntersectsWith(nodeBounds))
            {
                _document.AddToSelection(node);
            }

            foreach (MindMapNode child in node.ChildNodes)
            {
                SelectNodesInRectRecursive(child, docRect, g);
            }
        }
        /// <summary>
        /// 全选所有节点
        /// </summary>
        public void SelectAllNodes()
        {
            if (_document == null || _document.RootNode == null) return;
            
            _document.ClearSelection();
            SelectAllNodesRecursive(_document.RootNode);
            Invalidate();
        }

        /// <summary>
        /// 递归选择所有节点
        /// </summary>
        private void SelectAllNodesRecursive(MindMapNode node)
        {
            _document.AddToSelection(node);
            foreach (MindMapNode child in node.ChildNodes)
            {
                SelectAllNodesRecursive(child);
            }
        }
        #endregion
    }
}
