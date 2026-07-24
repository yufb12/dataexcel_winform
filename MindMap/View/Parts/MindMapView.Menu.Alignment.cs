using System;
using System.Drawing;
using MindMap.Core;

namespace MindMap.View
{
    /// <summary>
    /// 【SRP单一职责】对齐/布局/层级操作
    /// 负责：11种对齐、4种层级操作
    /// </summary>
    public partial class MindMapView
    {
        #region 水平对齐

        private void AlignLeftItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float minX = float.MaxValue;
            foreach (MindMapNode node in _document.SelectedNodes)
                minX = Math.Min(minX, node.Position.X);

            ApplyToSelectedNodes(node => node.Position = new PointF(minX, node.Position.Y));
            Invalidate();
        }

        private void AlignCenterHorizontalItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float sumX = 0;
            foreach (MindMapNode node in _document.SelectedNodes)
                sumX += node.Position.X + node.Bounds.Width / 2f;
            float centerX = sumX / _document.SelectedNodes.Count;

            ApplyToSelectedNodes(node => node.Position = new PointF(
                centerX - node.Bounds.Width / 2f, node.Position.Y));
            Invalidate();
        }

        private void AlignRightItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float maxX = float.MinValue;
            foreach (MindMapNode node in _document.SelectedNodes)
                maxX = Math.Max(maxX, node.Position.X + node.Bounds.Width);

            ApplyToSelectedNodes(node => node.Position = new PointF(
                maxX - node.Bounds.Width, node.Position.Y));
            Invalidate();
        }

        #endregion

        #region 垂直对齐

        private void AlignTopItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float minY = float.MaxValue;
            foreach (MindMapNode node in _document.SelectedNodes)
                minY = Math.Min(minY, node.Position.Y);

            ApplyToSelectedNodes(node => node.Position = new PointF(node.Position.X, minY));
            Invalidate();
        }

        private void AlignCenterVerticalItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float sumY = 0;
            foreach (MindMapNode node in _document.SelectedNodes)
                sumY += node.Position.Y + node.Bounds.Height / 2f;
            float centerY = sumY / _document.SelectedNodes.Count;

            ApplyToSelectedNodes(node => node.Position = new PointF(
                node.Position.X, centerY - node.Bounds.Height / 2f));
            Invalidate();
        }

        private void AlignBottomItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float maxY = float.MinValue;
            foreach (MindMapNode node in _document.SelectedNodes)
                maxY = Math.Max(maxY, node.Position.Y + node.Bounds.Height);

            ApplyToSelectedNodes(node => node.Position = new PointF(
                node.Position.X, maxY - node.Bounds.Height));
            Invalidate();
        }

        #endregion

        #region 分布对齐

        private void DistributeHorizontalItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 3) return;

            float minX = float.MaxValue, maxX = float.MinValue;
            foreach (MindMapNode node in _document.SelectedNodes)
            {
                minX = Math.Min(minX, node.Position.X);
                maxX = Math.Max(maxX, node.Position.X);
            }

            float step = (maxX - minX) / (_document.SelectedNodes.Count - 1);
            
            // 按X坐标排序
            System.Collections.Generic.List<MindMapNode> sortedNodes = 
                new System.Collections.Generic.List<MindMapNode>(_document.SelectedNodes);
            sortedNodes.Sort((a, b) => a.Position.X.CompareTo(b.Position.X));
            
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                sortedNodes[i].Position = new PointF(
                    minX + step * i, sortedNodes[i].Position.Y);
            }
            Invalidate();
        }

        private void DistributeVerticalItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 3) return;

            float minY = float.MaxValue, maxY = float.MinValue;
            foreach (MindMapNode node in _document.SelectedNodes)
            {
                minY = Math.Min(minY, node.Position.Y);
                maxY = Math.Max(maxY, node.Position.Y);
            }

            float step = (maxY - minY) / (_document.SelectedNodes.Count - 1);
            
            // 按Y坐标排序
            System.Collections.Generic.List<MindMapNode> sortedNodes = 
                new System.Collections.Generic.List<MindMapNode>(_document.SelectedNodes);
            sortedNodes.Sort((a, b) => a.Position.Y.CompareTo(b.Position.Y));
            
            for (int i = 0; i < sortedNodes.Count; i++)
            {
                sortedNodes[i].Position = new PointF(
                    sortedNodes[i].Position.X, minY + step * i);
            }
            Invalidate();
        }

        #endregion

        #region 统一尺寸

        private void SameWidthItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float maxWidth = 0;
            foreach (MindMapNode node in _document.SelectedNodes)
                maxWidth = Math.Max(maxWidth, node.Bounds.Width);

            ApplyToSelectedNodes(node => node.Bounds = new RectangleF(
                node.Position.X, node.Position.Y, maxWidth, node.Bounds.Height));
            Invalidate();
        }

        private void SameHeightItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float maxHeight = 0;
            foreach (MindMapNode node in _document.SelectedNodes)
                maxHeight = Math.Max(maxHeight, node.Bounds.Height);

            ApplyToSelectedNodes(node => node.Bounds = new RectangleF(
                node.Position.X, node.Position.Y, node.Bounds.Width, maxHeight));
            Invalidate();
        }

        private void SameSizeItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNodes.Count < 2) return;

            float maxWidth = 0, maxHeight = 0;
            foreach (MindMapNode node in _document.SelectedNodes)
            {
                maxWidth = Math.Max(maxWidth, node.Bounds.Width);
                maxHeight = Math.Max(maxHeight, node.Bounds.Height);
            }

            ApplyToSelectedNodes(node => node.Bounds = new RectangleF(
                node.Position.X, node.Position.Y, maxWidth, maxHeight));
            Invalidate();
        }

        #endregion

        #region 层级操作

        private void BringToFrontItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.ZOrder = int.MaxValue);
            Invalidate();
        }

        private void SendToBackItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.ZOrder = int.MinValue);
            Invalidate();
        }

        private void BringForwardItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.ZOrder++);
            Invalidate();
        }

        private void SendBackwardItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.ZOrder--);
            Invalidate();
        }

        #endregion
    }
}
