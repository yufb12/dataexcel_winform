using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MindMap.Core
{
    /// <summary>
    /// 【v2.2重大重构】思维导图节点数据模型
    /// 
    /// 【架构优化】：
    /// - 原：MindMapNode Parent / List<MindMapNode> Children
    /// - 新：Connection ParentConnection / List<Connection> ChildConnections
    /// 
    /// 【设计原则】：
    /// - ✅ SRP单一职责：节点只管节点属性，连接线只管连接线属性
    /// - ✅ 面向对象：节点和连接线成为两个独立一等公民
    /// - ✅ 向后兼容：提供ParentNode/ChildNodes便捷属性
    /// </summary>
    [Serializable]
    public class MindMapNode
    {
        #region 字段
        private string _text;
        private string _tooltip;
        private PointF _position;
        private SizeF _size;
        // v2.3分方向折叠：按方向存储展开状态（替代单一的_isExpanded）
        private System.Collections.Generic.Dictionary<NodeDirection, bool> _expandedByDirection;
        private int _zOrder;
        
        // 【v2.2重大重构】通过Connection关联，而非直接关联节点
        private Connection _parentConnection;           // 到父节点的连接线
        private List<Connection> _childConnections;     // 到子节点的连接线集合
        
        private NodeStyle _style;
        private NodeType _nodeType;
        #endregion

        #region 事件
        /// <summary>
        /// 节点文本变化事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<NodeTextChangedEventArgs> TextChanged;

        /// <summary>
        /// 子连接线添加事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ConnectionEventArgs> ConnectionAdded;

        /// <summary>
        /// 子连接线移除事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ConnectionEventArgs> ConnectionRemoved;
        #endregion

        #region 基本属性
        /// <summary>
        /// 获取或设置节点显示文本
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    string oldText = _text;
                    _text = value;
                    OnTextChanged(oldText, value);
                }
            }
        }

        /// <summary>
        /// 获取或设置节点提示文本（鼠标悬停显示）
        /// </summary>
        public string Tooltip
        {
            get { return _tooltip; }
            set { _tooltip = value; }
        }

        /// <summary>
        /// 获取或设置节点左上角坐标
        /// </summary>
        public PointF Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// 获取或设置节点大小
        /// </summary>
        public SizeF Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// 获取或设置节点的边界矩形
        /// </summary>
        public RectangleF Bounds
        {
            get { return new RectangleF(_position, _size); }
            set 
            { 
                _position = value.Location; 
                _size = value.Size; 
            }
        }

        /// <summary>
        /// 获取节点的中心点
        /// </summary>
        public PointF Center
        {
            get
            {
                return new PointF(
                    _position.X + _size.Width / 2f,
                    _position.Y + _size.Height / 2f);
            }
        }

        /// <summary>
        /// 获取或设置节点样式
        /// </summary>
        public NodeStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// 获取或设置节点类型
        /// </summary>
        public NodeType NodeType
        {
            get { return _nodeType; }
            set { _nodeType = value; }
        }

        /// <summary>
        /// 获取或设置Z-Order层级值
        /// 值越大，显示越靠上
        /// </summary>
        public int ZOrder
        {
            get { return _zOrder; }
            set { _zOrder = value; }
        }

        #region v2.3新增：分方向折叠/展开（核心功能）
        /// <summary>
        /// 【向后兼容】获取或设置是否展开子节点（默认操作Right方向）
        /// 为了兼容旧代码，默认操作Right方向
        /// </summary>
        [Obsolete("建议使用 IsExpandedInDirection 和 SetExpandedInDirection")]
        public bool IsExpanded
        {
            get { return IsExpandedInDirection(NodeDirection.Right); }
            set { SetExpandedInDirection(NodeDirection.Right, value); }
        }

        /// <summary>
        /// 【v2.3新增】获取指定方向是否展开
        /// </summary>
        /// <param name="direction">方向（左/右/上/下）</param>
        /// <returns>是否展开</returns>
        public bool IsExpandedInDirection(NodeDirection direction)
        {
            EnsureExpandedDictionaryInitialized();
            bool result;
            if (_expandedByDirection.TryGetValue(direction, out result))
            {
                return result;
            }
            return true;  // 默认展开
        }

        /// <summary>
        /// 【v2.3新增】设置指定方向的展开/折叠状态
        /// </summary>
        /// <param name="direction">方向（左/右/上/下）</param>
        /// <param name="expanded">是否展开</param>
        public void SetExpandedInDirection(NodeDirection direction, bool expanded)
        {
            EnsureExpandedDictionaryInitialized();
            _expandedByDirection[direction] = expanded;
        }

        /// <summary>
        /// 【v2.3新增】切换指定方向的展开/折叠状态
        /// </summary>
        /// <param name="direction">方向</param>
        public void ToggleExpandedInDirection(NodeDirection direction)
        {
            SetExpandedInDirection(direction, !IsExpandedInDirection(direction));
        }

        /// <summary>
        /// 【v2.3新增】获取指定方向的子连接线列表
        /// </summary>
        /// <param name="direction">方向</param>
        /// <returns>该方向的所有子连接线</returns>
        public IList<Connection> GetChildConnections(NodeDirection direction)
        {
            List<Connection> result = new List<Connection>();
            foreach (Connection conn in _childConnections)
            {
                if (conn.Direction == direction)
                {
                    result.Add(conn);
                }
            }
            return result.AsReadOnly();
        }

        /// <summary>
        /// 【v2.3新增】获取指定方向且已展开的子连接线列表
        /// 用于渲染时只绘制展开方向的子节点
        /// </summary>
        /// <param name="direction">方向</param>
        /// <returns>该方向且已展开的子连接线</returns>
        public IList<Connection> GetExpandedChildConnections(NodeDirection direction)
        {
            if (!IsExpandedInDirection(direction))
            {
                return new Connection[0];  // 折叠状态返回空
            }
            return GetChildConnections(direction);
        }

        /// <summary>
        /// 【v2.3新增】获取所有展开的子连接线（所有方向）
        /// </summary>
        public IList<Connection> GetAllExpandedChildConnections()
        {
            List<Connection> result = new List<Connection>();
            foreach (NodeDirection direction in System.Enum.GetValues(typeof(NodeDirection)))
            {
                if (IsExpandedInDirection(direction))
                {
                    foreach (Connection conn in _childConnections)
                    {
                        NodeDirection connDirection = CalculateConnectionDirection(conn);
                        if (connDirection == direction)
                        {
                            result.Add(conn);
                        }
                    }
                }
            }
            return result.AsReadOnly();
        }

        /// <summary>
        /// 【v2.3新增】检查指定方向是否有子节点
        /// </summary>
        /// <param name="direction">方向</param>
        /// <returns>是否有子节点</returns>
        public bool HasChildrenInDirection(NodeDirection direction)
        {
            // v2.3修复：只要有子节点，所有4个方向都显示展开按钮
            // （用户体验优先，避免严格的方向计算导致按钮不显示）
            return _childConnections.Count > 0;
        }
        /// <summary>
        /// v2.3：计算连接线的方向（向后兼容）
        /// </summary>
        private NodeDirection CalculateConnectionDirection(Connection conn)
        {
            if (conn.ChildNode == null) return NodeDirection.Right;
            
            float dx = conn.ChildNode.Position.X - Position.X;
            float dy = conn.ChildNode.Position.Y - Position.Y;
            float angle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
            if (angle < 0) angle += 360;
            
            if (angle >= 315 || angle < 45)
                return NodeDirection.Right;
            else if (angle >= 45 && angle < 135)
                return NodeDirection.Bottom;
            else if (angle >= 135 && angle < 225)
                return NodeDirection.Left;
            else
                return NodeDirection.Top;
        }
        /// </summary>
        /// </summary>
        private void EnsureExpandedDictionaryInitialized()
        {
            if (_expandedByDirection == null)
            {
                _expandedByDirection = new System.Collections.Generic.Dictionary<NodeDirection, bool>();
                // 默认所有方向都展开
                foreach (NodeDirection direction in System.Enum.GetValues(typeof(NodeDirection)))
                {
                    _expandedByDirection[direction] = true;
                }
            }
        }
        #endregion
        #endregion

        #region 【v2.2重构】连接线关联属性（核心改动）
        /// <summary>
        /// 获取或设置到父节点的连接线
        /// </summary>
        public Connection ParentConnection
        {
            get { return _parentConnection; }
            set { _parentConnection = value; }
        }

        /// <summary>
        /// 获取到子节点的连接线列表（只读）
        /// </summary>
        public IList<Connection> ChildConnections
        {
            get { return _childConnections.AsReadOnly(); }
        }

        /// <summary>
        /// 获取子连接线数量
        /// </summary>
        public int ChildConnectionCount
        {
            get { return _childConnections.Count; }
        }
        #endregion

        #region 【向后兼容】节点便捷属性
        /// <summary>
        /// 【向后兼容】获取父节点（通过ParentConnection.ParentNode）
        /// </summary>
        public MindMapNode ParentNode
        {
            get 
            { 
                return _parentConnection != null ? _parentConnection.ParentNode : null; 
            }
        }

        /// <summary>
        /// 【向后兼容】获取所有子节点列表（通过ChildConnections.Select(c => c.ChildNode)）
        /// </summary>
        public IList<MindMapNode> ChildNodes
        {
            get 
            { 
                return _childConnections.Select(c => c.ChildNode).ToList().AsReadOnly(); 
            }
        }

        /// <summary>
        /// 【向后兼容】获取子节点数量
        /// </summary>
        public int ChildNodeCount
        {
            get { return _childConnections.Count; }
        }
        #endregion
        /// <summary>
        /// 【向后兼容】获取子节点数量（旧API）
        /// </summary>
        [Obsolete("建议使用 ChildNodeCount 属性")]
        public int ChildCount
        {
            get { return ChildNodeCount; }
        }

        #region 计算属性
        /// <summary>
        /// 获取节点的深度（根节点为0）
        /// </summary>
        public int Depth
        {
            get
            {
                int depth = 0;
                MindMapNode current = ParentNode;
                while (current != null)
                {
                    depth++;
                    current = current.ParentNode;
                }
                return depth;
            }
        }

        /// <summary>
        /// 获取一个值，指示是否为根节点
        /// </summary>
        public bool IsRoot
        {
            get { return _parentConnection == null; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化节点
        /// </summary>
        public MindMapNode()
        {
            _text = string.Empty;
            _childConnections = new List<Connection>();
            // v2.3：_expandedByDirection 延迟初始化，默认所有方向展开
            _zOrder = 0;
            _style = new NodeStyle();
            _nodeType = NodeType.SubBranch;
            _position = new PointF(0, 0);
            _size = new SizeF(100, 30);
        }

        /// <summary>
        /// 初始化带文本的节点
        /// </summary>
        /// <param name="text">节点文本</param>
        public MindMapNode(string text)
            : this()
        {
            _text = text;
            AutoCalculateSize();
        }
        #endregion

        #region 【v2.2重构】连接线操作方法（核心）
        /// <summary>
        /// 添加子节点（自动创建连接线）
        /// </summary>
        /// <param name="childNode">要添加的子节点</param>
        public void AddChildNode(MindMapNode childNode)
        {
            if (childNode == null)
                throw new ArgumentNullException("childNode");

            // 创建连接线并建立双向关联
            Connection connection = new Connection(this, childNode);
            
            // v2.3：根据子节点相对于父节点的位置，自动计算并设置方向
            connection.Direction = CalculateChildDirection(childNode);
            
            AddChildConnection(connection);
        }

        /// <summary>
        /// v2.3：根据子节点相对于父节点的位置，计算子节点方向
        /// </summary>
        private NodeDirection CalculateChildDirection(MindMapNode childNode)
        {
            float parentCenterX = Position.X + Bounds.Width / 2f;
            float parentCenterY = Position.Y + Bounds.Height / 2f;
            float childCenterX = childNode.Position.X + childNode.Bounds.Width / 2f;
            float childCenterY = childNode.Position.Y + childNode.Bounds.Height / 2f;

            float dx = childCenterX - parentCenterX;
            float dy = childCenterY - parentCenterY;

            // 计算角度（0度=右，90度=下，180度=左，270度=上）
            float angle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
            if (angle < 0) angle += 360;

            // 根据角度判断方向
            if (angle >= 315 || angle < 45)
                return NodeDirection.Right;      // 右侧
            else if (angle >= 45 && angle < 135)
                return NodeDirection.Bottom;     // 下方
            else if (angle >= 135 && angle < 225)
                return NodeDirection.Left;       // 左侧
            else
                return NodeDirection.Top;        // 上方
        }

        /// <summary>
        /// 添加子连接线
        /// </summary>
        /// <param name="connection">要添加的连接线</param>
        public void AddChildConnection(Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (connection.ParentNode != this)
                throw new ArgumentException("连接线的父节点必须是当前节点");

            // 设置双向关联
            connection.ChildNode._parentConnection = connection;
            connection.ChildNode.UpdateNodeTypeByDepth();
            _childConnections.Add(connection);
            OnConnectionAdded(connection);
        }

        /// <summary>
        /// 在指定位置插入子节点（自动创建连接线）
        /// </summary>
        /// <param name="index">插入位置索引</param>
        /// <param name="childNode">要插入的子节点</param>
        public void InsertChildNode(int index, MindMapNode childNode)
        {
            if (childNode == null)
                throw new ArgumentNullException("childNode");
            if (index < 0 || index > _childConnections.Count)
                throw new ArgumentOutOfRangeException("index");

            Connection connection = new Connection(this, childNode);
            InsertChildConnection(index, connection);
        }

        /// <summary>
        /// 在指定位置插入子连接线
        /// </summary>
        /// <param name="index">插入位置索引</param>
        /// <param name="connection">要插入的连接线</param>
        public void InsertChildConnection(int index, Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (index < 0 || index > _childConnections.Count)
                throw new ArgumentOutOfRangeException("index");
            if (connection.ParentNode != this)
                throw new ArgumentException("连接线的父节点必须是当前节点");

            connection.ChildNode._parentConnection = connection;
            connection.ChildNode.UpdateNodeTypeByDepth();
            _childConnections.Insert(index, connection);
            OnConnectionAdded(connection);
        }

        /// <summary>
        /// 移除子节点（同时移除对应的连接线）
        /// </summary>
        /// <param name="childNode">要移除的子节点</param>
        /// <returns>是否成功移除</returns>
        public bool RemoveChildNode(MindMapNode childNode)
        {
            if (childNode == null)
                throw new ArgumentNullException("childNode");

            // 找到对应的连接线
            Connection connection = _childConnections.FirstOrDefault(c => c.ChildNode == childNode);
            if (connection != null)
            {
                return RemoveChildConnection(connection);
            }
            return false;
        }

        /// <summary>
        /// 移除子连接线
        /// </summary>
        /// <param name="connection">要移除的连接线</param>
        /// <returns>是否成功移除</returns>
        public bool RemoveChildConnection(Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            if (_childConnections.Remove(connection))
            {
                connection.ChildNode._parentConnection = null;
                OnConnectionRemoved(connection);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除所有子连接线和子节点
        /// </summary>
        public void ClearAllChildConnections()
        {
            foreach (Connection connection in _childConnections)
            {
                connection.ChildNode._parentConnection = null;
                OnConnectionRemoved(connection);
            }
            _childConnections.Clear();
        }
        #endregion

        #region 【向后兼容】旧API方法名（便于迁移）
        /// <summary>
        /// 【向后兼容】添加子节点（旧API）
        /// </summary>
        [Obsolete("建议使用 AddChildNode 方法")]
        public void AddChild(MindMapNode child)
        {
            AddChildNode(child);
        }

        /// <summary>
        /// 【向后兼容】在指定位置插入子节点（旧API）
        /// </summary>
        [Obsolete("建议使用 InsertChildNode 方法")]
        public void InsertChild(int index, MindMapNode child)
        {
            InsertChildNode(index, child);
        }

        /// <summary>
        /// 【向后兼容】移除子节点（旧API）
        /// </summary>
        [Obsolete("建议使用 RemoveChildNode 方法")]
        public bool RemoveChild(MindMapNode child)
        {
            return RemoveChildNode(child);
        }

        /// <summary>
        /// 【向后兼容】移除所有子节点（旧API）
        /// </summary>
        [Obsolete("建议使用 ClearAllChildConnections 方法")]
        public void ClearChildren()
        {
            ClearAllChildConnections();
        }

        /// <summary>
        /// 【向后兼容】获取子节点列表（旧API）
        /// </summary>
        [Obsolete("建议使用 ChildNodes 属性")]
        public IList<MindMapNode> Children
        {
            get { return ChildNodes; }
        }

        /// <summary>
        /// 【向后兼容】获取或设置父节点（旧API）
        /// </summary>
        [Obsolete("建议使用 ParentNode 属性")]
        public MindMapNode Parent
        {
            get { return ParentNode; }
            set 
            {
                // 注意：setter仅用于反序列化兼容，正常应通过AddChildNode建立关联
                if (value == null)
                {
                    _parentConnection = null;
                }
            }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 根据节点深度更新节点类型
        /// </summary>
        private void UpdateNodeTypeByDepth()
        {
            int depth = Depth;
            if (depth == 0)
            {
                _nodeType = NodeType.Root;
                _style = NodeStyle.CreateRootStyle();
            }
            else if (depth == 1)
            {
                _nodeType = NodeType.MainBranch;
                _style = NodeStyle.CreateMainBranchStyle();
            }
            else
            {
                _nodeType = NodeType.SubBranch;
                _style = NodeStyle.CreateSubBranchStyle();
            }
        }

        /// <summary>
        /// 自动计算节点大小
        /// </summary>
        public void AutoCalculateSize()
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    SizeF textSize = g.MeasureString(_text, _style.Font);
                    const float padding = 8f;
                    _size = new SizeF(textSize.Width + padding * 2, textSize.Height + padding);
                }
            }
        }

        /// <summary>
        /// 检查指定点是否在节点边界内
        /// </summary>
        /// <param name="point">要检查的点</param>
        /// <returns>是否在节点边界内</returns>
        public bool ContainsPoint(PointF point)
        {
            return Bounds.Contains(point);
        }
        #endregion

        #region 事件触发
        /// <summary>
        /// 触发文本变化事件
        /// </summary>
        protected virtual void OnTextChanged(string oldText, string newText)
        {
            EventHandler<NodeTextChangedEventArgs> handler = TextChanged;
            if (handler != null)
            {
                handler(this, new NodeTextChangedEventArgs(this, oldText, newText));
            }
        }

        /// <summary>
        /// 触发连接线添加事件
        /// </summary>
        protected virtual void OnConnectionAdded(Connection connection)
        {
            EventHandler<ConnectionEventArgs> handler = ConnectionAdded;
            if (handler != null)
            {
                handler(this, new ConnectionEventArgs(connection));
            }
        }

        /// <summary>
        /// 触发连接线移除事件
        /// </summary>
        protected virtual void OnConnectionRemoved(Connection connection)
        {
            EventHandler<ConnectionEventArgs> handler = ConnectionRemoved;
            if (handler != null)
            {
                handler(this, new ConnectionEventArgs(connection));
            }
        }
        #endregion
    }

    #region 新增事件参数类
    /// <summary>
    /// 连接线事件参数
    /// </summary>
    public class ConnectionEventArgs : EventArgs
    {
        public Connection Connection { get; private set; }

        public ConnectionEventArgs(Connection connection)
        {
            Connection = connection;
        }
    }
    #endregion
}
