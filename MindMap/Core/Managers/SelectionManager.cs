using System;
using System.Collections.Generic;
using System.Drawing;

namespace MindMap.Core.Managers
{
    /// <summary>
    /// 【SRP单一职责】选择管理器
    /// 负责：单选/多选/框选/全选管理
    /// 【设计模式】Mediator中介者模式 - 解耦选择逻辑与文档
    /// </summary>
    [Serializable]
    public class SelectionManager
    {
        #region 字段

        [NonSerialized]
        private MindMapDocument _document;
        private readonly List<MindMapNode> _selectedNodes;
        private MindMapNode _primarySelection;

        #endregion

        #region 事件

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

        #region 构造函数

        public SelectionManager(MindMapDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            _document = document;
            _selectedNodes = new List<MindMapNode>();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置主选中节点
        /// </summary>
        public MindMapNode SelectedNode
        {
            get { return _primarySelection; }
            set
            {
                ClearSelection();
                if (value != null)
                {
                    AddToSelection(value);
                    _primarySelection = value;
                }
                OnSelectionChanged();
            }
        }

        /// <summary>
        /// 获取所有选中节点
        /// </summary>
        public IList<MindMapNode> SelectedNodes
        {
            get { return _selectedNodes.AsReadOnly(); }
        }

        /// <summary>
        /// 获取选中节点数量
        /// </summary>
        public int SelectionCount
        {
            get { return _selectedNodes.Count; }
        }

        /// <summary>
        /// 是否有选中节点
        /// </summary>
        public bool HasSelection
        {
            get { return _selectedNodes.Count > 0; }
        }

        #endregion

        #region 核心选择方法

        /// <summary>
        /// 清除所有选择
        /// </summary>
        public void ClearSelection()
        {
            foreach (MindMapNode node in _selectedNodes)
            {
                node.IsSelected = false;
            }
            _selectedNodes.Clear();
            _primarySelection = null;
            OnSelectionChanged();
        }

        /// <summary>
        /// 添加节点到选择
        /// </summary>
        public void AddToSelection(MindMapNode node)
        {
            if (node == null || _selectedNodes.Contains(node)) return;

            node.IsSelected = true;
            _selectedNodes.Add(node);
            if (_primarySelection == null)
            {
                _primarySelection = node;
            }
            OnMultiSelectionChanged();
        }

        /// <summary>
        /// 从选择中移除节点
        /// </summary>
        public void RemoveFromSelection(MindMapNode node)
        {
            if (node == null) return;

            node.IsSelected = false;
            _selectedNodes.Remove(node);
            if (_primarySelection == node)
            {
                _primarySelection = _selectedNodes.Count > 0 ? _selectedNodes[0] : null;
            }
            OnMultiSelectionChanged();
        }

        /// <summary>
        /// 切换节点选择状态（Ctrl+点击）
        /// </summary>
        public void ToggleSelection(MindMapNode node)
        {
            if (node == null) return;

            if (_selectedNodes.Contains(node))
            {
                RemoveFromSelection(node);
            }
            else
            {
                AddToSelection(node);
            }
        }

        /// <summary>
        /// 全选所有节点
        /// </summary>
        public void SelectAll()
        {
            ClearSelection();
            SelectAllRecursive(_document.RootNode);
            OnSelectionChanged();
        }

        /// <summary>
        /// 递归选择所有节点
        /// </summary>
        private void SelectAllRecursive(MindMapNode node)
        {
            if (node == null) return;
            AddToSelection(node);
            foreach (MindMapNode child in node.GetAllExpandedChildNodes())
            {
                SelectAllRecursive(child);
            }
        }

        /// <summary>
        /// 框选矩形内的节点
        /// </summary>
        public void SelectNodesInRect(RectangleF rect)
        {
            ClearSelection();
            SelectNodesInRectRecursive(_document.RootNode, rect);
            OnSelectionChanged();
        }

        /// <summary>
        /// 递归选择矩形内的节点
        /// </summary>
        private void SelectNodesInRectRecursive(MindMapNode node, RectangleF rect)
        {
            if (node == null) return;

            if (rect.IntersectsWith(node.Bounds))
            {
                AddToSelection(node);
            }

            foreach (MindMapNode child in node.GetAllExpandedChildNodes())
            {
                SelectNodesInRectRecursive(child, rect);
            }
        }

        #endregion

        #region 批量操作

        /// <summary>
        /// 对所有选中节点执行操作
        /// </summary>
        public void ApplyToSelectedNodes(Action<MindMapNode> action)
        {
            if (action == null) return;

            foreach (MindMapNode node in _selectedNodes)
            {
                action(node);
            }
        }

        #endregion

        #region 事件触发

        protected virtual void OnSelectionChanged()
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, EventArgs.Empty);
            }
        }

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
