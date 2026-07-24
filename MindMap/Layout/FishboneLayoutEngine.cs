using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Layout
{
    /// <summary>
    /// 鱼骨图布局引擎（石川图）
    /// 适用场景：因果分析、问题根因查找
    /// 布局特点：主骨横向居中，原因分支从主骨向上下两侧展开
    /// </summary>
    public class FishboneLayoutEngine : ILayoutEngine
    {
        private const float HORIZONTAL_SPACING = 120f;
        private const float VERTICAL_SPACING = 60f;
        private const float MAIN_BONE_Y = 0f;

        public void Layout(MindMapDocument document)
        {
            if (document == null || document.RootNode == null)
                return;

            MindMapNode root = document.RootNode;

            // 根节点放在最右侧（鱼头）
            root.Position = new PointF(400f, MAIN_BONE_Y);

            // 递归布局所有子节点
            LayoutChildrenRecursive(root, 0);
        }

        /// <summary>
        /// 递归布局子节点
        /// </summary>
        private void LayoutChildrenRecursive(MindMapNode parent, int depth)
        {
            if (parent.ChildNodes == null || parent.ChildNodes.Count == 0)
                return;

            IList<MindMapNode> children = parent.ChildNodes;
            int count = children.Count;

            // 计算起始X位置（向左展开）
            float startX = parent.Position.X - HORIZONTAL_SPACING;

            // 交替上下排列（奇数上，偶数下）
            for (int i = 0; i < count; i++)
            {
                MindMapNode child = children[i];

                // X坐标：向左递进
                float x = startX - (depth * HORIZONTAL_SPACING * 0.6f);

                // Y坐标：交替上下分布
                bool isTop = (i % 2 == 0);
                float offsetY = ((i / 2) + 1) * VERTICAL_SPACING;
                float y = isTop ? MAIN_BONE_Y - offsetY : MAIN_BONE_Y + offsetY;

                child.Position = new PointF(x, y);

                // 递归布局孙子节点
                LayoutChildrenRecursive(child, depth + 1);
            }
        }
    }
}
