using System;
using System.Drawing;
using System.Windows.Forms;
using MindMap.Core;
using MindMap.Interfaces;
using MindMap.Rendering;
using MindMap.Commands;
using MindMap.Layout;

namespace MindMap.View
{
    /// <summary>
    /// 思维导图视图控件 v1.8（架构重构版）
    /// 
    /// 【架构重构 - 符合SOLID原则】
    /// - SRP单一职责：按职责拆分为5个partial类文件
    /// - OCP开闭原则：通过接口扩展，不修改原有代码
    /// - LSP里氏替换：所有渲染器/布局引擎都可替换
    /// - ISP接口隔离：接口细分，不依赖不需要的方法
    /// - DIP依赖倒置：依赖抽象接口，不依赖具体实现
    /// 
    /// 拆分文件：
    /// - MindMapView.cs              - 主文件：字段、属性、构造函数、核心公共方法
    /// - Parts/MindMapView.Render.cs - 绘制渲染逻辑
    /// - Parts/MindMapView.Input.cs  - 鼠标键盘输入处理
    /// - Parts/MindMapView.Menu.cs   - 右键菜单
    /// - Parts/MindMapView.Selection.cs - 多选对齐
    /// </summary>
    public partial class MindMapView : Control
    {
        #region 私有字段（按职责分组）
        // 核心模型
        private MindMapDocument _document;
        
        // 渲染相关（DIP：依赖接口）
        private EnhancedNodeRenderer _renderer;
        private IHitTester _hitTester;
        
        // 命令模式
        private CommandManager _commandManager;
        
        // 布局引擎（策略模式）
        private ILayoutEngine _currentLayoutEngine;
        
        // 操作状态
        private MouseOperationMode _operationMode;
        private PointF _dragStartPoint;
        private PointF _originalNodePosition;  // 保留兼容（单个节点移动）
        private System.Collections.Generic.Dictionary<MindMapNode, PointF> _originalNodePositions;  // v2.1.7.2：多选节点整体移动
        private PointF _originalOffset;
        
        // 多选框选
        private PointF _marqueeStart;
        private RectangleF _marqueeRect;
        
        // 文本编辑
        private TextBox _editTextBox;
        private MindMapNode _editingNode;
        
        // 右键菜单
        private ContextMenuStrip _contextMenu;
        
        // 复制粘贴
        private MindMapNode _copiedNode;
        
        // v3.0：Ctrl+拖拽复制（支持多选批量复制）
        private bool _isDragCopying;  // 是否正在复制拖拽
        private System.Collections.Generic.List<MindMapNode> _dragCopyOriginals;  // 被复制的原始节点列表
        private System.Collections.Generic.List<MindMapNode> _dragCopyNodes;  // 拖拽中的复制节点列表
        
        // v2.1.4新增：Tooltip提示
        private ToolTip _toolTip;
        private MindMapNode _lastHoverNode;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置思维导图文档
        /// </summary>
        public MindMapDocument Document
        {
            get { return _document; }
            set
            {
                if (_document != null)
                {
                    _document.DocumentChanged -= OnDocumentChanged;
                    _document.SelectionChanged -= OnSelectionChanged;
                    _document.MultiSelectionChanged -= OnMultiSelectionChanged;
                }
                _document = value;
                if (_document != null)
                {
                    _document.DocumentChanged += OnDocumentChanged;
                    _document.SelectionChanged += OnSelectionChanged;
                    _document.MultiSelectionChanged += OnMultiSelectionChanged;
                    if (_renderer != null && _document.Theme != null)
                    {
                        _renderer.Theme = _document.Theme;
                    }
                }
                Invalidate();
            }
        }

        /// <summary>
        /// 获取命令管理器
        /// </summary>
        public CommandManager CommandManager
        {
            get { return _commandManager; }
        }

        /// <summary>
        /// 获取或设置当前布局引擎（策略模式，可随时切换）
        /// </summary>
        public ILayoutEngine CurrentLayoutEngine
        {
            get { return _currentLayoutEngine; }
            set { _currentLayoutEngine = value; }
        }

        /// <summary>
        /// 获取当前渲染器
        /// </summary>
        public EnhancedNodeRenderer Renderer
        {
            get { return _renderer; }
        }
        #endregion

        #region 构造函数与初始化
        /// <summary>
        /// 初始化思维导图视图
        /// </summary>
        public MindMapView()
        {
            // 控件样式
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // 键盘焦点设置（关键：否则无法接收Tab/Enter键事件）
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            BackColor = Color.FromArgb(255, 248, 248, 248);

            // 依赖注入（DIP：依赖抽象）
            _renderer = new EnhancedNodeRenderer();
            _hitTester = new HitTester(_renderer);
            _commandManager = new CommandManager();
            _currentLayoutEngine = new RadialLayoutEngine();
            _operationMode = MouseOperationMode.None;

            // 初始化子模块
            InitializeContextMenu();
            
            // v2.1.4：初始化Tooltip
            _toolTip = new ToolTip();
            _toolTip.AutoPopDelay = 5000;
            _toolTip.InitialDelay = 500;
            _toolTip.ReshowDelay = 100;
            _toolTip.ShowAlways = true;
            _lastHoverNode = null;
        }
        #endregion

        #region 核心公共方法（OCP：通过扩展增加功能）
        /// <summary>
        /// 切换布局引擎（策略模式）
        /// </summary>
        public void SwitchLayout(ILayoutEngine layoutEngine)
        {
            if (layoutEngine == null) throw new ArgumentNullException("layoutEngine");
            _currentLayoutEngine = layoutEngine;
            if (_document != null)
            {
                _currentLayoutEngine.Layout(_document);
            }
            Invalidate();
        }

        /// <summary>
        /// 设置主题（v1.8修复：应用到所有已有节点）
        /// </summary>
        public void SetTheme(Theme theme)
        {
            if (theme == null) throw new ArgumentNullException("theme");
            if (_document != null)
            {
                _document.Theme = theme;
                // v1.8修复：主题切换时更新所有节点样式
                ApplyThemeToNode(_document.RootNode, theme);
            }
            _renderer.Theme = theme;
            Invalidate();
        }

        /// <summary>
        /// 递归应用主题到所有节点
        /// </summary>
        private void ApplyThemeToNode(MindMapNode node, Theme theme)
        {
            if (node == null) return;
            
            if (node.NodeType == NodeType.Root)
            {
                node.Style.BackColor = theme.RootBackColor;
                node.Style.ForeColor = theme.RootForeColor;
                node.Style.BorderColor = theme.RootBorderColor;
            }
            else if (node.NodeType == NodeType.MainBranch)
            {
                node.Style.BackColor = theme.MainBranchBackColor;
                node.Style.ForeColor = theme.MainBranchForeColor;
                node.Style.BorderColor = theme.MainBranchBorderColor;
            }
            else
            {
                node.Style.BackColor = theme.SubBranchBackColor;
                node.Style.ForeColor = theme.SubBranchForeColor;
                node.Style.BorderColor = theme.SubBranchBorderColor;
            }

            if (node.IsExpanded && node.ChildCount > 0)
            {
                foreach (MindMapNode child in node.ChildNodes)
                {
                    ApplyThemeToNode(child, theme);
                }
            }
        }

        /// <summary>
        /// 重置视图
        /// </summary>
        public void ResetView()
        {
            if (_document == null) return;
            int width = ClientSize.Width > 0 ? ClientSize.Width : 800;
            int height = ClientSize.Height > 0 ? ClientSize.Height : 600;
            _document.ViewSettings.Zoom = 1f;
            _document.ViewSettings.Offset = new PointF(
                width / 2f - _document.RootNode.Position.X - _document.RootNode.Size.Width / 2f,
                height / 2f - _document.RootNode.Position.Y - _document.RootNode.Size.Height / 2f);
            Invalidate();
        }
        #endregion

        #region 节点操作方法
        public void AddChildNode()
        {
            if (_document == null || _document.SelectedNode == null) return;
            MindMapNode parent = _document.SelectedNode;
            MindMapNode newNode = new MindMapNode("新节点");
            if (parent.NodeType == NodeType.Root)
                newNode.Style = NodeStyle.CreateMainBranchStyle();
            else
                newNode.Style = NodeStyle.CreateSubBranchStyle();

            float offsetX = parent.Bounds.Width + 80;
            float offsetY = (parent.ChildCount) * 60;
            newNode.Position = new PointF(
                parent.Position.X + offsetX,
                parent.Position.Y + offsetY - parent.ChildCount * 30);

            AddNodeCommand command = new AddNodeCommand(_document, parent, newNode);
            _commandManager.ExecuteCommand(command);
            newNode.Position = new PointF(
                parent.Position.X + offsetX,
                parent.Position.Y + offsetY - parent.ChildCount * 30);
            parent.IsExpanded = true;
            _document.ClearSelection();
            _document.AddToSelection(newNode);
            BeginEditNode(newNode);
            Invalidate();
        }

        /// <summary>
        /// 在选中节点与其父节点之间插入新节点（v2.1.1新增）
        /// </summary>
        public void InsertNodeBefore()
        {
            if (_document == null || _document.SelectedNode == null) return;
            MindMapNode targetNode = _document.SelectedNode;
            
            // 根节点不能插入
            if (targetNode.ParentNode == null) return;

            MindMapNode newNode = new MindMapNode("新节点");
            
            // 根据层级设置样式
            if (targetNode.NodeType == NodeType.MainBranch)
                newNode.Style = NodeStyle.CreateMainBranchStyle();
            else
                newNode.Style = NodeStyle.CreateSubBranchStyle();

            // 设置插入节点的位置（在目标节点位置）
            newNode.Position = new PointF(
                targetNode.Position.X,
                targetNode.Position.Y);

            // 目标节点向右下方偏移
            targetNode.Position = new PointF(
                targetNode.Position.X + 80,
                targetNode.Position.Y + 30);

            InsertNodeCommand command = new InsertNodeCommand(_document, targetNode, newNode);
            _commandManager.ExecuteCommand(command);
            
            newNode.Position = new PointF(
                targetNode.Position.X - 80,
                targetNode.Position.Y - 30);
            
            newNode.IsExpanded = true;
            BeginEditNode(newNode);
            Invalidate();
        }

        public void AddSiblingNode()
        {
            if (_document == null || _document.SelectedNode == null || _document.SelectedNode.ParentNode == null) return;
            MindMapNode parent = _document.SelectedNode.ParentNode;
            MindMapNode newNode = new MindMapNode("新节点");
            newNode.Style = NodeStyle.CreateSubBranchStyle();
            newNode.Position = new PointF(
                _document.SelectedNode.Position.X,
                _document.SelectedNode.Position.Y + 60);

            AddNodeCommand command = new AddNodeCommand(_document, parent, newNode);
            _commandManager.ExecuteCommand(command);
            parent.IsExpanded = true;
            _document.ClearSelection();
            _document.AddToSelection(newNode);
            BeginEditNode(newNode);
            Invalidate();
        }

        public void DeleteSelectedNode()
        {
            if (_document == null || _document.SelectedNode == null || _document.SelectedNode.ParentNode == null) return;
            MindMapNode parent = _document.SelectedNode.ParentNode;
            DeleteNodeCommand command = new DeleteNodeCommand(_document, parent, _document.SelectedNode);
            _commandManager.ExecuteCommand(command);
            _document.ClearSelection();
            Invalidate();
        }
        #endregion

        #region 文本编辑
        /// <summary>
        /// 开始编辑节点文本（v2.1.5修复：正确计算输入框位置）
        /// 
        /// 修复内容：
        /// 1. 正确应用ViewSettings的缩放和平移变换
        /// 2. 大括号节点考虑左侧30px偏移
        /// 3. 内边距调整为更美观的8px
        /// </summary>
        public void BeginEditNode(MindMapNode node)
        {
            if (node == null) return;
            _editingNode = node;
            if (_editTextBox == null)
            {
                _editTextBox = new TextBox();
                _editTextBox.BorderStyle = BorderStyle.FixedSingle;
                _editTextBox.KeyDown += EditTextBox_KeyDown;
                _editTextBox.LostFocus += EditTextBox_LostFocus;
                Controls.Add(_editTextBox);
            }

            using (Graphics g = CreateGraphics())
            {
                // 1. 计算节点在文档坐标系中的边界
                RectangleF docBounds = _renderer.CalculateNodeBounds(g, node);

                // 2. 应用视图变换（缩放 + 平移）转换为控件坐标系
                PointF controlTopLeft = _document.ViewSettings.DocumentToScreen(
                    new PointF(docBounds.X, docBounds.Y));

                // 3. 计算变换后的尺寸（考虑缩放）
                float scaledWidth = docBounds.Width * _document.ViewSettings.Zoom;
                float scaledHeight = docBounds.Height * _document.ViewSettings.Zoom;

                // 4. 设置输入框位置和大小（8px内边距）
                const float padding = 8f;
                _editTextBox.Location = new Point(
                    (int)(controlTopLeft.X + padding),
                    (int)(controlTopLeft.Y + padding));
                _editTextBox.Size = new Size(
                    (int)(scaledWidth - padding * 2),
                    (int)(scaledHeight - padding * 2));
            }

            _editTextBox.Text = node.Text;
            _editTextBox.Font = node.Style.Font;
            _editTextBox.Visible = true;
            _editTextBox.SelectAll();
            _editTextBox.Focus();
        }

        private void EditTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EndEditNode(true);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                EndEditNode(false);
                e.Handled = true;
            }
        }

        private void EditTextBox_LostFocus(object sender, EventArgs e)
        {
            EndEditNode(true);
        }

        private void EndEditNode(bool saveChanges)
        {
            if (_editingNode == null || _editTextBox == null || !_editTextBox.Visible) return;
            if (saveChanges && _editTextBox.Text != _editingNode.Text)
            {
                EditNodeTextCommand command = new EditNodeTextCommand(_editingNode, _editingNode.Text, _editTextBox.Text);
                _commandManager.ExecuteCommand(command);
            }
            _editTextBox.Visible = false;
            _editingNode = null;
            Invalidate();
        }
        #endregion

        #region 复制粘贴
        public void CopySelectedNode()
        {
            if (_document == null || _document.SelectedNode == null) return;
            _copiedNode = new MindMapNode(_document.SelectedNode.Text);
            _copiedNode.Style.BackColor = _document.SelectedNode.Style.BackColor;
            _copiedNode.Style.ForeColor = _document.SelectedNode.Style.ForeColor;
            _copiedNode.Style.BorderColor = _document.SelectedNode.Style.BorderColor;
            _copiedNode.Style.Shape = _document.SelectedNode.Style.Shape;
        }

        public void PasteNode()
        {
            if (_document == null || _document.SelectedNode == null || _copiedNode == null) return;
            MindMapNode parent = _document.SelectedNode;
            MindMapNode newNode = new MindMapNode(_copiedNode.Text + " (副本)");
            newNode.Style.BackColor = _copiedNode.Style.BackColor;
            newNode.Style.ForeColor = _copiedNode.Style.ForeColor;
            newNode.Style.BorderColor = _copiedNode.Style.BorderColor;
            newNode.Style.Shape = _copiedNode.Style.Shape;

            float offsetX = parent.Bounds.Width + 80;
            float offsetY = (parent.ChildCount) * 60;
            newNode.Position = new PointF(
                parent.Position.X + offsetX,
                parent.Position.Y + offsetY - parent.ChildCount * 30);

            AddNodeCommand command = new AddNodeCommand(_document, parent, newNode);
            _commandManager.ExecuteCommand(command);
            newNode.Position = new PointF(
                parent.Position.X + offsetX,
                parent.Position.Y + offsetY - parent.ChildCount * 30);
            parent.IsExpanded = true;
            _document.ClearSelection();
            _document.AddToSelection(newNode);
            Invalidate();
        }
        #endregion

        private void OnDocumentChanged(object sender, EventArgs e) { Invalidate(); }
        private void OnSelectionChanged(object sender, EventArgs e) { Invalidate(); }
        private void OnMultiSelectionChanged(object sender, EventArgs e) { Invalidate(); }

        #region 公共方法
        /// <summary>
        /// 重新布局（自动排列所有节点）
        /// </summary>
        public void Relayout()
        {
            if (_document != null && _currentLayoutEngine != null)
            {
                _currentLayoutEngine.Layout(_document);
                Invalidate();
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (_renderer != null) _renderer.Dispose();
                if (_editTextBox != null) _editTextBox.Dispose();
                if (_contextMenu != null) _contextMenu.Dispose();
                if (_toolTip != null) _toolTip.Dispose();
                _disposed = true;
            }
            base.Dispose(disposing);
        }
        private bool _disposed;
 
    }
}
