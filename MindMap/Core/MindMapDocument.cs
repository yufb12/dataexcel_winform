using System;
using System.Collections.Generic;
using System.Drawing;
using MindMap.Core.Managers;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】思维导图文档 - 纯文档模型
    /// 选择管理委托给SelectionManager
    /// 【设计模式】Aggregate根聚合
    /// </summary>
    [Serializable]
    public class MindMapDocument
    {
        #region 字段

        private MindMapNode _rootNode;
        private ViewSettings _viewSettings;
        private readonly SelectionManager _selectionManager;
        private Theme _theme;
        private Connection _selectedConnection;
        private string _title;
        private string _filePath;

        #endregion

        #region 构造函数

        public MindMapDocument()
        {
            _rootNode = new MindMapNode("中心主题");
            _rootNode.NodeType = NodeType.Root;
            _rootNode.Style = NodeStyle.CreateRootStyle();

            _viewSettings = new ViewSettings();
            _selectionManager = new SelectionManager(this);
            _theme = Theme.CreateDefaultTheme();
            _selectedConnection = null;
            _title = "新建思维导图";
            _filePath = string.Empty;

            SubscribeSelectionEvents();
        }

        /// <summary>
        /// 订阅选择管理器事件
        /// </summary>
        private void SubscribeSelectionEvents()
        {
            _selectionManager.SelectionChanged += (s, e) => OnSelectionChanged();
            _selectionManager.MultiSelectionChanged += (s, e) => OnMultiSelectionChanged();
        }

        #endregion

        #region 核心属性

        /// <summary>
        /// 获取根节点
        /// </summary>
        public MindMapNode RootNode
        {
            get { return _rootNode; }
        }

        /// <summary>
        /// 获取视图设置
        /// </summary>
        public ViewSettings ViewSettings
        {
            get { return _viewSettings; }
        }

        /// <summary>
        /// 获取选择管理器
        /// </summary>
        public SelectionManager SelectionManager
        {
            get { return _selectionManager; }
        }

        /// <summary>
        /// 获取或设置主题
        /// </summary>
        public Theme Theme
        {
            get { return _theme; }
            set { _theme = value; }
        }

        /// <summary>
        /// 获取或设置选中的连接线
        /// </summary>
        public Connection SelectedConnection
        {
            get { return _selectedConnection; }
            set { _selectedConnection = value; }
        }

        /// <summary>
        /// 获取或设置文档标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value ?? string.Empty; }
        }

        /// <summary>
        /// 获取或设置文件路径
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value ?? string.Empty; }
        }

        #endregion

        #region 选择属性（向后兼容，委托给SelectionManager）

        /// <summary>
        /// 获取或设置选中节点
        /// </summary>
        public MindMapNode SelectedNode
        {
            get { return _selectionManager.SelectedNode; }
            set { _selectionManager.SelectedNode = value; }
        }

        /// <summary>
        /// 获取所有选中节点
        /// </summary>
        public IList<MindMapNode> SelectedNodes
        {
            get { return _selectionManager.SelectedNodes; }
        }

        /// <summary>
        /// 获取选中节点数量
        /// </summary>
        public int SelectionCount
        {
            get { return _selectionManager.SelectionCount; }
        }

        #endregion

        #region 选择方法（向后兼容，委托给SelectionManager）

        /// <summary>
        /// 清除选择
        /// </summary>
        public void ClearSelection()
        {
            _selectionManager.ClearSelection();
            _selectedConnection = null;
        }

        /// <summary>
        /// 添加节点到选择
        /// </summary>
        public void AddToSelection(MindMapNode node)
        {
            _selectionManager.AddToSelection(node);
        }

        /// <summary>
        /// 切换节点选择状态
        /// </summary>
        public void ToggleSelection(MindMapNode node)
        {
            _selectionManager.ToggleSelection(node);
        }

        /// <summary>
        /// 检查节点是否被选中
        /// </summary>
        public bool IsNodeSelected(MindMapNode node)
        {
            return _selectionManager.SelectedNodes.Contains(node);
        }

        /// <summary>
        /// 全选所有节点
        /// </summary>
        public void SelectAllNodes()
        {
            _selectionManager.SelectAll();
        }

        /// <summary>
        /// 选择矩形内的节点
        /// </summary>
        public void SelectNodesInRect(RectangleF rect)
        {
            _selectionManager.SelectNodesInRect(rect);
        }

        /// <summary>
        /// 对选中节点执行操作
        /// </summary>
        public void ApplyToSelectedNodes(Action<MindMapNode> action)
        {
            _selectionManager.ApplyToSelectedNodes(action);
        }

        #endregion

        #region 事件定义

        /// <summary>
        /// 文档变化事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler DocumentChanged;

        /// <summary>
        /// 选择变化事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SelectionChanged;

        /// <summary>
        /// 多选变化事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler MultiSelectionChanged;

        #endregion

        #region 事件触发

        /// <summary>
        /// 触发文档变化事件
        /// </summary>
        public void OnDocumentChanged()
        {
            if (DocumentChanged != null)
            {
                DocumentChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 触发选择变化事件
        /// </summary>
        protected virtual void OnSelectionChanged()
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 触发多选变化事件
        /// </summary>
        protected virtual void OnMultiSelectionChanged()
        {
            if (MultiSelectionChanged != null)
            {
                MultiSelectionChanged(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
