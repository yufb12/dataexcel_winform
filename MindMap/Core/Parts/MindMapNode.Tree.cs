using System;
using System.Collections.Generic;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】思维导图节点 - 树形结构部分
    /// 负责：父子关系管理、子节点增删改查、Composite组合模式实现
    /// 【设计模式】Composite组合模式
    /// </summary>
    public partial class MindMapNode
    {
        #region 字段 - 树形结构

        private Connection _parentConnection;
        private List<Connection> _childConnections;

        #endregion

        #region partial 方法实现

        /// <summary>
        /// 初始化集合
        /// </summary>
        partial void InitializeCollections()
        {
            _childConnections = new List<Connection>();
        }

        #endregion

        #region 父节点属性（向后兼容）

        /// <summary>
        /// 获取父连接线
        /// </summary>
        public Connection ParentConnection
        {
            get { return _parentConnection; }
            internal set { _parentConnection = value; }
        }

        /// <summary>
        /// 获取父节点（向后兼容）
        /// </summary>
        public MindMapNode ParentNode
        {
            get { return _parentConnection != null ? _parentConnection.ParentNode : null; }
        }

        /// <summary>
        /// 兼容旧API（已过时，请使用ParentNode）
        /// </summary>
        [Obsolete("请使用 ParentNode 属性")]
        public MindMapNode Parent
        {
            get { return ParentNode; }
        }

        #endregion

        #region 子节点属性（向后兼容）

        /// <summary>
        /// 获取子连接线列表
        /// </summary>
        public IList<Connection> ChildConnections
        {
            get { return _childConnections.AsReadOnly(); }
        }

        /// <summary>
        /// 获取子节点列表（向后兼容）
        /// </summary>
        public IList<MindMapNode> ChildNodes
        {
            get
            {
                List<MindMapNode> nodes = new List<MindMapNode>();
                foreach (Connection conn in _childConnections)
                {
                    nodes.Add(conn.ChildNode);
                }
                return nodes.AsReadOnly();
            }
        }

        /// <summary>
        /// 获取子节点数量
        /// </summary>
        public int ChildNodeCount
        {
            get { return _childConnections.Count; }
        }

        /// <summary>
        /// 兼容旧API（已过时，请使用ChildNodes）
        /// </summary>
        [Obsolete("请使用 ChildNodes 属性")]
        public IList<MindMapNode> Children
        {
            get { return ChildNodes; }
        }

        #endregion

        #region 添加子节点

        /// <summary>
        /// 添加子节点（自动创建连接线）
        /// </summary>
        public void AddChildNode(MindMapNode child)
        {
            if (child == null)
                throw new ArgumentNullException("child");
            if (child == this)
                throw new InvalidOperationException("不能将自己添加为子节点");

            Connection conn = new Connection(this, child);
            AddChildConnection(conn);
        }

        /// <summary>
        /// 添加子连接线
        /// </summary>
        public void AddChildConnection(Connection conn)
        {
            if (conn == null)
                throw new ArgumentNullException("conn");
            if (conn.ParentNode != this)
                throw new InvalidOperationException("连接线的父节点不匹配");

            _childConnections.Add(conn);
            conn.ChildNode._parentConnection = conn;
            conn.ChildNode._depth = _depth + 1;
            conn.ChildNode.UpdateNodeTypeByDepth();

            OnChildAdded(conn.ChildNode);
            OnConnectionAdded(conn);
        }

        /// <summary>
        /// 兼容旧API
        /// </summary>
        [Obsolete("请使用 AddChildNode 方法")]
        public void AddChild(MindMapNode child)
        {
            AddChildNode(child);
        }

        #endregion

        #region 插入子节点

        /// <summary>
        /// 在指定位置插入子节点
        /// </summary>
        public void InsertChildNode(int index, MindMapNode child)
        {
            if (child == null)
                throw new ArgumentNullException("child");
            if (index < 0 || index > _childConnections.Count)
                throw new ArgumentOutOfRangeException("index");

            Connection conn = new Connection(this, child);
            InsertChildConnection(index, conn);
        }

        /// <summary>
        /// 在指定位置插入子连接线
        /// </summary>
        public void InsertChildConnection(int index, Connection conn)
        {
            if (conn == null)
                throw new ArgumentNullException("conn");
            if (index < 0 || index > _childConnections.Count)
                throw new ArgumentOutOfRangeException("index");

            _childConnections.Insert(index, conn);
            conn.ChildNode._parentConnection = conn;
            conn.ChildNode._depth = _depth + 1;
            conn.ChildNode.UpdateNodeTypeByDepth();

            OnChildAdded(conn.ChildNode);
            OnConnectionAdded(conn);
        }

        /// <summary>
        /// 兼容旧API
        /// </summary>
        [Obsolete("请使用 InsertChildNode 方法")]
        public void InsertChild(int index, MindMapNode child)
        {
            InsertChildNode(index, child);
        }

        #endregion

        #region 移除子节点

        /// <summary>
        /// 移除子节点
        /// </summary>
        public void RemoveChildNode(MindMapNode child)
        {
            if (child == null) return;

            for (int i = _childConnections.Count - 1; i >= 0; i--)
            {
                if (_childConnections[i].ChildNode == child)
                {
                    RemoveChildConnection(_childConnections[i]);
                    return;
                }
            }
        }

        /// <summary>
        /// 移除子连接线
        /// </summary>
        public void RemoveChildConnection(Connection conn)
        {
            if (conn == null) return;
            if (_childConnections.Remove(conn))
            {
                conn.ChildNode._parentConnection = null;
                OnChildRemoved(conn.ChildNode);
                OnConnectionRemoved(conn);
            }
        }

        /// <summary>
        /// 兼容旧API
        /// </summary>
        [Obsolete("请使用 RemoveChildNode 方法")]
        public void RemoveChild(MindMapNode child)
        {
            RemoveChildNode(child);
        }

        /// <summary>
        /// 清除所有子节点
        /// </summary>
        public void ClearAllChildConnections()
        {
            foreach (Connection conn in _childConnections)
            {
                conn.ChildNode._parentConnection = null;
            }
            _childConnections.Clear();
        }

        /// <summary>
        /// 兼容旧API
        /// </summary>
        [Obsolete("请使用 ClearAllChildConnections 方法")]
        public void ClearChildren()
        {
            ClearAllChildConnections();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查是否有子节点
        /// </summary>
        public bool HasChildren
        {
            get { return _childConnections.Count > 0; }
        }

        /// <summary>
        /// 检查是否为根节点
        /// </summary>
        public bool IsRoot
        {
            get { return _parentConnection == null; }
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        public MindMapNode GetRoot()
        {
            MindMapNode current = this;
            while (current.ParentNode != null)
            {
                current = current.ParentNode;
            }
            return current;
        }

        #endregion
    }
}
