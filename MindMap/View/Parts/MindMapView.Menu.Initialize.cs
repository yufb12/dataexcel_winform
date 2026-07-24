using System;
using System.Windows.Forms;
using MindMap.Core;
using MindMap.Rendering;

namespace MindMap.View
{
    /// <summary>
    /// 【SRP单一职责】菜单初始化
    /// 负责创建所有右键菜单项
    /// </summary>
    public partial class MindMapView
    {
        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        private void InitializeContextMenu()
        {
            _contextMenu = new ContextMenuStrip();
            _contextMenu.Opening += ContextMenu_Opening;

            // ========== 基础操作 ==========
            AddMenuItem("添加子节点 (Tab)", AddChildNodeItem_Click);
            AddMenuItem("添加同级节点 (Enter)", AddSiblingNodeItem_Click);
            AddMenuItem("在当前节点前插入", InsertNodeBeforeItem_Click);
            AddMenuItem("删除节点 (Delete)", DeleteNodeItem_Click);
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== 编辑操作 ==========
            AddMenuItem("编辑文本 (F2/Space)", EditNodeTextItem_Click);
            AddMenuItem("展开/折叠 (+/-)", ExpandCollapseItem_Click);
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== 复制粘贴 ==========
            AddMenuItem("复制节点 (Ctrl+C)", CopyItem_Click);
            AddMenuItem("粘贴节点 (Ctrl+V)", PasteItem_Click);
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== 布局操作 ==========
            AddMenuItem("重新布局（全局）", RelayoutItem_Click);
            AddMenuItem("仅布局当前节点的子节点", RelayoutSubtreeItem_Click);
            AddLayoutMenu();
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== 对齐操作 ==========
            AddAlignmentMenu();
            AddZOrderMenu();
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== 节点形状 ==========
            AddNodeShapeMenu();
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== 节点样式 ==========
            AddNodeStyleMenu();
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== 连接线设置 ==========
            AddConnectionMenu();
            _contextMenu.Items.Add(new ToolStripSeparator());

            // ========== Tooltip ==========
            AddMenuItem("设置节点提示(Tooltip)...", SetTooltipItem_Click);
            AddMenuItem("清除节点提示", ClearTooltipItem_Click);
        }

        /// <summary>
        /// 添加单个菜单项
        /// </summary>
        private ToolStripMenuItem AddMenuItem(string text, EventHandler clickHandler)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            item.Click += clickHandler;
            _contextMenu.Items.Add(item);
            return item;
        }

        /// <summary>
        /// 添加布局切换子菜单
        /// </summary>
        private void AddLayoutMenu()
        {
            ToolStripMenuItem layoutMenu = new ToolStripMenuItem("切换布局");
            AddLayoutMenuItem(layoutMenu, "放射状布局", "Radial");
            AddLayoutMenuItem(layoutMenu, "树状布局", "Tree");
            AddLayoutMenuItem(layoutMenu, "左右布局", "LeftRight");
            AddLayoutMenuItem(layoutMenu, "鱼骨图布局", "Fishbone");
            AddLayoutMenuItem(layoutMenu, "时间线布局", "Timeline");
            AddLayoutMenuItem(layoutMenu, "组织结构图", "OrgChart");
            AddLayoutMenuItem(layoutMenu, "水平思维导图", "Horizontal");
            AddLayoutMenuItem(layoutMenu, "垂直思维导图", "Vertical");
            AddLayoutMenuItem(layoutMenu, "螺旋布局", "Spiral");
            AddLayoutMenuItem(layoutMenu, "扇形布局", "Fan");
            AddLayoutMenuItem(layoutMenu, "圆形布局", "Circle");
            AddLayoutMenuItem(layoutMenu, "瀑布布局", "Waterfall");
            AddLayoutMenuItem(layoutMenu, "对称布局", "Symmetric");
            AddLayoutMenuItem(layoutMenu, "金字塔布局", "Pyramid");
            AddLayoutMenuItem(layoutMenu, "漏斗布局", "Funnel");
            AddLayoutMenuItem(layoutMenu, "气泡图布局", "Bubble");
            AddLayoutMenuItem(layoutMenu, "矩阵布局", "Matrix");
            AddLayoutMenuItem(layoutMenu, "流程图布局", "Flowchart");
            AddLayoutMenuItem(layoutMenu, "环形布局", "Ring");
            _contextMenu.Items.Add(layoutMenu);
        }

        /// <summary>
        /// 添加对齐子菜单
        /// </summary>
        private void AddAlignmentMenu()
        {
            ToolStripMenuItem alignMenu = new ToolStripMenuItem("对齐");
            AddAlignMenuItem(alignMenu, "左对齐", AlignLeftItem_Click);
            AddAlignMenuItem(alignMenu, "水平居中", AlignCenterHorizontalItem_Click);
            AddAlignMenuItem(alignMenu, "右对齐", AlignRightItem_Click);
            alignMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(alignMenu, "顶端对齐", AlignTopItem_Click);
            AddAlignMenuItem(alignMenu, "垂直居中", AlignCenterVerticalItem_Click);
            AddAlignMenuItem(alignMenu, "底端对齐", AlignBottomItem_Click);
            alignMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(alignMenu, "水平分布", DistributeHorizontalItem_Click);
            AddAlignMenuItem(alignMenu, "垂直分布", DistributeVerticalItem_Click);
            alignMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(alignMenu, "统一宽度", SameWidthItem_Click);
            AddAlignMenuItem(alignMenu, "统一高度", SameHeightItem_Click);
            AddAlignMenuItem(alignMenu, "统一尺寸", SameSizeItem_Click);
            _contextMenu.Items.Add(alignMenu);
        }

        /// <summary>
        /// 添加层级子菜单
        /// </summary>
        private void AddZOrderMenu()
        {
            ToolStripMenuItem zorderMenu = new ToolStripMenuItem("层级");
            AddAlignMenuItem(zorderMenu, "置于顶层", BringToFrontItem_Click);
            AddAlignMenuItem(zorderMenu, "置于底层", SendToBackItem_Click);
            AddAlignMenuItem(zorderMenu, "上移一层", BringForwardItem_Click);
            AddAlignMenuItem(zorderMenu, "下移一层", SendBackwardItem_Click);
            _contextMenu.Items.Add(zorderMenu);
        }

        /// <summary>
        /// 添加节点形状子菜单
        /// </summary>
        private void AddNodeShapeMenu()
        {
            ToolStripMenuItem shapeMenu = new ToolStripMenuItem("节点形状");
            AddShapeMenuItem(shapeMenu, "圆角矩形", NodeShape.RoundedRectangle);
            AddShapeMenuItem(shapeMenu, "矩形", NodeShape.Rectangle);
            AddShapeMenuItem(shapeMenu, "椭圆", NodeShape.Ellipse);
            AddShapeMenuItem(shapeMenu, "菱形", NodeShape.Diamond);
            AddShapeMenuItem(shapeMenu, "平行四边形", NodeShape.Parallelogram);
            AddShapeMenuItem(shapeMenu, "六边形", NodeShape.Hexagon);
            AddShapeMenuItem(shapeMenu, "八角形", NodeShape.Octagon);
            AddShapeMenuItem(shapeMenu, "胶囊形状（两端半圆）", NodeShape.Pill);
            AddShapeMenuItem(shapeMenu, "下划线样式（文字+横线）", NodeShape.Underline);
            AddShapeMenuItem(shapeMenu, "主标题+副标题", NodeShape.TitleWithSubtitle);
            AddShapeMenuItem(shapeMenu, "图片节点", NodeShape.Image);
            _contextMenu.Items.Add(shapeMenu);
        }

        /// <summary>
        /// 添加节点样式子菜单
        /// </summary>
        private void AddNodeStyleMenu()
        {
            ToolStripMenuItem styleMenu = new ToolStripMenuItem("节点样式");
            AddAlignMenuItem(styleMenu, "设置背景色...", SetBackColorItem_Click);
            AddAlignMenuItem(styleMenu, "设置文本颜色...", SetForeColorItem_Click);
            AddAlignMenuItem(styleMenu, "设置字体...", SetFontItem_Click);
            styleMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(styleMenu, "设置边框颜色...", SetBorderColorItem_Click);
            AddBorderStyleMenu(styleMenu);
            AddAlignMenuItem(styleMenu, "显示边框", ShowBorderItem_Click);
            AddAlignMenuItem(styleMenu, "隐藏边框", HideBorderItem_Click);
            styleMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(styleMenu, "添加节点图标...", AddIconItem_Click);
            AddIconPositionMenu(styleMenu);
            AddAlignMenuItem(styleMenu, "清除所有图标", ClearIconItem_Click);
            styleMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(styleMenu, "设置节点顶部图片...", SetTopImageItem_Click);
            AddAlignMenuItem(styleMenu, "清除顶部图片", ClearTopImageItem_Click);
            styleMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(styleMenu, "设置节点背景图...", SetBackgroundImageItem_Click);
            AddAlignMenuItem(styleMenu, "清除背景图", ClearBackgroundImageItem_Click);
            styleMenu.DropDownItems.Add(new ToolStripSeparator());
            AddAlignMenuItem(styleMenu, "设置副标题...", SetSubtitleItem_Click);
            _contextMenu.Items.Add(styleMenu);
        }

        /// <summary>
        /// 添加连接线设置子菜单
        /// </summary>
        private void AddConnectionMenu()
        {
            ToolStripMenuItem connectionMenu = new ToolStripMenuItem("连接线设置");
            
            // 连接线类型
            ToolStripMenuItem connLineTypeMenu = new ToolStripMenuItem("连接线类型");
            AddConnectionLineTypeMenuItem(connLineTypeMenu, "贝塞尔曲线", ConnectionLineType.Bezier);
            AddConnectionLineTypeMenuItem(connLineTypeMenu, "直线", ConnectionLineType.Straight);
            AddConnectionLineTypeMenuItem(connLineTypeMenu, "折线", ConnectionLineType.Step);
            AddConnectionLineTypeMenuItem(connLineTypeMenu, "正交线", ConnectionLineType.Orthogonal);
            AddConnectionLineTypeMenuItem(connLineTypeMenu, "弧形", ConnectionLineType.Arc);
            connectionMenu.DropDownItems.Add(connLineTypeMenu);
            
            // 连接线颜色
            ToolStripMenuItem connLineColorItem = new ToolStripMenuItem("设置连接线颜色...");
            connLineColorItem.Click += SetConnectionLineColorItem_Click;
            connectionMenu.DropDownItems.Add(connLineColorItem);
            
            // 连接线宽度
            ToolStripMenuItem connLineWidthMenu = new ToolStripMenuItem("连接线宽度");
            AddConnectionLineWidthMenuItem(connLineWidthMenu, "1像素", 1f);
            AddConnectionLineWidthMenuItem(connLineWidthMenu, "2像素", 2f);
            AddConnectionLineWidthMenuItem(connLineWidthMenu, "3像素", 3f);
            AddConnectionLineWidthMenuItem(connLineWidthMenu, "4像素", 4f);
            connectionMenu.DropDownItems.Add(connLineWidthMenu);
            
            connectionMenu.DropDownItems.Add(new ToolStripSeparator());
            
            // 父节点连接点
            ToolStripMenuItem parentConnMenu = new ToolStripMenuItem("父节点连接点");
            AddConnectionPointMenuItem(parentConnMenu, "自动", ConnectionPoint.Auto, true);
            AddConnectionPointMenuItem(parentConnMenu, "左侧", ConnectionPoint.Left, true);
            AddConnectionPointMenuItem(parentConnMenu, "右侧", ConnectionPoint.Right, true);
            AddConnectionPointMenuItem(parentConnMenu, "顶部", ConnectionPoint.Top, true);
            AddConnectionPointMenuItem(parentConnMenu, "底部", ConnectionPoint.Bottom, true);
            connectionMenu.DropDownItems.Add(parentConnMenu);
            
            // 本节点连接点
            ToolStripMenuItem childConnMenu = new ToolStripMenuItem("本节点连接点");
            AddConnectionPointMenuItem(childConnMenu, "自动", ConnectionPoint.Auto, false);
            AddConnectionPointMenuItem(childConnMenu, "左侧", ConnectionPoint.Left, false);
            AddConnectionPointMenuItem(childConnMenu, "右侧", ConnectionPoint.Right, false);
            AddConnectionPointMenuItem(childConnMenu, "顶部", ConnectionPoint.Top, false);
            AddConnectionPointMenuItem(childConnMenu, "底部", ConnectionPoint.Bottom, false);
            connectionMenu.DropDownItems.Add(childConnMenu);
            
            _contextMenu.Items.Add(connectionMenu);
        }

        /// <summary>
        /// 菜单打开前事件
        /// </summary>
        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool hasSelection = _document != null && _document.SelectedNode != null;
            foreach (ToolStripItem item in _contextMenu.Items)
            {
                ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                if (menuItem != null && menuItem.Text != "重新布局（全局）")
                {
                    menuItem.Enabled = hasSelection;
                }
            }
        }
    }
}
