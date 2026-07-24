using System;
using System.Drawing;
using System.Windows.Forms;
using MindMap.Core;
using MindMap.Commands;
using MindMap.Layout;
using MindMap.Interfaces;

namespace MindMap.View
{
    /// <summary>
    /// 【SRP单一职责】节点操作事件处理
    /// 负责：添加/删除/编辑/复制/粘贴/展开折叠
    /// </summary>
    public partial class MindMapView
    {
        #region 节点添加/删除

        private void AddChildNodeItem_Click(object sender, EventArgs e)
        {
            AddChildNode();
        }

        private void AddSiblingNodeItem_Click(object sender, EventArgs e)
        {
            AddSiblingNode();
        }

        private void InsertNodeBeforeItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;
            if (_document.SelectedNode.ParentNode == null) return;

            MindMapNode newNode = new MindMapNode("新节点");
            InsertNodeCommand command = new InsertNodeCommand(_document, _document.SelectedNode, newNode);
            _commandManager.ExecuteCommand(command);
            _document.SelectedNode = newNode;
            BeginEditNode(newNode);
        }

        private void DeleteNodeItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedNode();
        }

        #endregion

        #region 节点编辑

        private void EditNodeTextItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;
            BeginEditNode(_document.SelectedNode);
        }

        private void ExpandCollapseItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;
            _document.SelectedNode.IsExpanded = !_document.SelectedNode.IsExpanded;
            Invalidate();
        }

        #endregion

        #region 复制粘贴

        private void CopyItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("复制功能开发中...");
        }

        private void PasteItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("粘贴功能开发中...");
        }

        #endregion

        #region 布局操作

        private void RelayoutItem_Click(object sender, EventArgs e)
        {
            Relayout();
        }

        private void RelayoutSubtreeItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null || _currentLayoutEngine == null)
                return;

            MindMapNode parentNode = _document.SelectedNode;
            if (parentNode.ChildNodeCount == 0) return;

            PointF originalParentPos = parentNode.Position;
            MindMapDocument tempDoc = new MindMapDocument();
            tempDoc.RootNode.Text = parentNode.Text;
            tempDoc.RootNode.Position = new PointF(0, 0);

            foreach (MindMapNode child in parentNode.ChildNodes)
            {
                MindMapNode tempChild = new MindMapNode(child.Text);
                tempChild.Style = (NodeStyle)child.Style.Clone();
                tempDoc.RootNode.AddChildNode(tempChild);
            }

            _currentLayoutEngine.Layout(tempDoc);

            for (int i = 0; i < parentNode.ChildNodeCount && i < tempDoc.RootNode.ChildNodeCount; i++)
            {
                PointF relativePos = tempDoc.RootNode.ChildNodes[i].Position;
                parentNode.ChildNodes[i].Position = new PointF(
                    originalParentPos.X + relativePos.X,
                    originalParentPos.Y + relativePos.Y);
            }

            Invalidate();
        }

        private void LayoutItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null && item.Tag is string)
            {
                ILayoutEngine engine = CreateLayoutEngine((string)item.Tag);
                if (engine != null)
                {
                    SwitchLayout(engine);
                }
            }
        }

        /// <summary>
        /// 根据布局名称创建布局引擎实例
        /// </summary>
        private ILayoutEngine CreateLayoutEngine(string layoutName)
        {
            switch (layoutName)
            {
                case "Radial": return new RadialLayoutEngine();
                case "Tree": return new TreeLayoutEngine();
                case "LeftRight": return new LeftRightLayoutEngine();
                case "Fishbone": return new FishboneLayoutEngine();
                case "Timeline": return new TimelineLayoutEngine();
                case "OrgChart": return new OrgChartLayoutEngine();
                case "Horizontal": return new MindMapHorizontalLayout();
                case "Vertical": return new MindMapVerticalLayout();
                case "Spiral": return new SpiralLayoutEngine();
                case "Fan": return new FanLayoutEngine();
                case "Circle": return new CircleLayoutEngine();
                case "Waterfall": return new WaterfallLayoutEngine();
                case "Symmetric": return new SymmetricLayoutEngine();
                case "Pyramid": return new PyramidLayoutEngine();
                case "Funnel": return new FunnelLayoutEngine();
                case "Bubble": return new BubbleLayoutEngine();
                case "Matrix": return new MatrixLayoutEngine();
                case "Flowchart": return new FlowchartLayoutEngine();
                case "Ring": return new RingLayoutEngine();
                default: return new TreeLayoutEngine();
            }
        }

        #endregion

        #region 辅助方法

        private void AddLayoutMenuItem(ToolStripMenuItem parent, string text, string layoutName)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = layoutName;
            item.Click += LayoutItem_Click;
            parent.DropDownItems.Add(item);
        }

        private void AddAlignMenuItem(ToolStripMenuItem parent, string text, EventHandler clickHandler)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Click += clickHandler;
            parent.DropDownItems.Add(item);
        }

        private void AddShapeMenuItem(ToolStripMenuItem parent, string text, NodeShape shape)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = shape;
            item.Click += ShapeItem_Click;
            parent.DropDownItems.Add(item);
        }

        private void AddBorderStyleMenu(ToolStripMenuItem parent)
        {
            ToolStripMenuItem borderStyleMenu = new ToolStripMenuItem("边框样式");
            AddBorderStyleMenuItem(borderStyleMenu, "实线", NodeBorderStyle.Solid);
            AddBorderStyleMenuItem(borderStyleMenu, "虚线", NodeBorderStyle.Dash);
            AddBorderStyleMenuItem(borderStyleMenu, "点线", NodeBorderStyle.Dot);
            AddBorderStyleMenuItem(borderStyleMenu, "点划线", NodeBorderStyle.DashDot);
            AddBorderStyleMenuItem(borderStyleMenu, "双点划线", NodeBorderStyle.DashDotDot);
            AddBorderStyleMenuItem(borderStyleMenu, "无边框", NodeBorderStyle.None);
            parent.DropDownItems.Add(borderStyleMenu);
        }

        private void AddBorderStyleMenuItem(ToolStripMenuItem parent, string text, NodeBorderStyle style)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = style;
            item.Click += BorderStyleItem_Click;
            parent.DropDownItems.Add(item);
        }

        private void AddIconPositionMenu(ToolStripMenuItem parent)
        {
            ToolStripMenuItem iconPosMenu = new ToolStripMenuItem("图标位置");
            AddIconPositionMenuItem(iconPosMenu, "左侧", IconPosition.Left);
            AddIconPositionMenuItem(iconPosMenu, "右侧", IconPosition.Right);
            AddIconPositionMenuItem(iconPosMenu, "顶部", IconPosition.Top);
            AddIconPositionMenuItem(iconPosMenu, "无图标", IconPosition.None);
            parent.DropDownItems.Add(iconPosMenu);
        }

        private void AddIconPositionMenuItem(ToolStripMenuItem parent, string text, IconPosition pos)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = pos;
            item.Click += IconPositionItem_Click;
            parent.DropDownItems.Add(item);
        }

        private void AddConnectionLineTypeMenuItem(ToolStripMenuItem parent, string text, ConnectionLineType lineType)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = lineType;
            item.Click += ConnectionLineTypeItem_Click;
            parent.DropDownItems.Add(item);
        }

        private void AddConnectionLineWidthMenuItem(ToolStripMenuItem parent, string text, float width)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = width;
            item.Click += ConnectionLineWidthItem_Click;
            parent.DropDownItems.Add(item);
        }

        private void AddConnectionPointMenuItem(ToolStripMenuItem parent, string text, ConnectionPoint point, bool isParent)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Tag = new Tuple<ConnectionPoint, bool>(point, isParent);
            item.Click += ConnectionPointItem_Click;
            parent.DropDownItems.Add(item);
        }

        #endregion
    }
}
