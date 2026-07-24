using System;
using System.Drawing;

namespace MindMap.Core
{
    /// <summary>
    /// 节点连接线类（v2.1.7新增，面向对象设计）
    /// 
    /// 【架构设计】
    /// - 单一职责：专门表示两个节点之间的连接关系
    /// - 面向对象：将连接线从渲染逻辑中分离，成为独立的领域对象
    /// - 可扩展：支持后续添加连接线样式、标签、箭头等属性
    /// </summary>
    [Serializable]
    public class Connection
    {
        #region 字段
        private MindMapNode _parentNode;
        private MindMapNode _childNode;
        private ConnectionLineType _lineType;
        private Color _lineColor;
        private float _lineWidth;
        private ConnectionPoint _parentConnectionPoint;
        private ConnectionPoint _childConnectionPoint;
        private NodeDirection _direction;  // v2.3新增：子节点相对于父节点的方向
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置父节点（连接起点）
        /// </summary>
        public MindMapNode ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        /// <summary>
        /// 获取或设置子节点（连接终点）
        /// </summary>
        public MindMapNode ChildNode
        {
            get { return _childNode; }
            set { _childNode = value; }
        }

        /// <summary>
        /// 获取或设置连接线类型
        /// </summary>
        public ConnectionLineType LineType
        {
            get { return _lineType; }
            set { _lineType = value; }
        }

        /// <summary>
        /// 获取或设置连接线颜色
        /// </summary>
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        /// <summary>
        /// 获取或设置连接线宽度
        /// </summary>
        public float LineWidth
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }

        /// <summary>
        /// 获取或设置父节点连接点（v2.1.7新增：从NodeStyle移到Connection类）
        /// </summary>
        public ConnectionPoint ParentConnectionPoint
        {
            get { return _parentConnectionPoint; }
            set { _parentConnectionPoint = value; }
        }

        /// <summary>
        /// 获取或设置子节点连接点（v2.1.7新增：从NodeStyle移到Connection类）
        /// </summary>
        public ConnectionPoint ChildConnectionPoint
        {
            get { return _childConnectionPoint; }
            set { _childConnectionPoint = value; }
        }

        /// <summary>
        /// 【v2.3新增】子节点相对于父节点的方向
        /// 用于支持分方向折叠/展开
        /// </summary>
        public NodeDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 创建一个新的连接线
        /// </summary>
        /// <param name="parentNode">父节点（起点）</param>
        /// <param name="childNode">子节点（终点）</param>
        public Connection(MindMapNode parentNode, MindMapNode childNode)
        {
            if (parentNode == null) throw new ArgumentNullException("parentNode");
            if (childNode == null) throw new ArgumentNullException("childNode");

            _parentNode = parentNode;
            _childNode = childNode;

            // 默认使用子节点的样式设置
            _lineType = ConnectionLineType.Bezier;
            _lineColor = Color.Gray;
            _lineWidth = 1.5f;

            // 默认连接点设置
            _parentConnectionPoint = ConnectionPoint.Auto;
            _childConnectionPoint = ConnectionPoint.Auto;
        }

        /// <summary>
        /// 创建一个新的连接线（指定样式）
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childNode">子节点</param>
        /// <param name="lineType">连接线类型</param>
        /// <param name="lineColor">连接线颜色</param>
        /// <param name="lineWidth">连接线宽度</param>
        public Connection(MindMapNode parentNode, MindMapNode childNode, 
            ConnectionLineType lineType, Color lineColor, float lineWidth)
        {
            if (parentNode == null) throw new ArgumentNullException("parentNode");
            if (childNode == null) throw new ArgumentNullException("childNode");

            _parentNode = parentNode;
            _childNode = childNode;
            _lineType = lineType;
            _lineColor = lineColor;
            _lineWidth = lineWidth;

            // 默认连接点设置
            _parentConnectionPoint = ConnectionPoint.Auto;
            _childConnectionPoint = ConnectionPoint.Auto;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 应用子节点样式到连接线
        /// </summary>
        public void ApplyNodeStyle()
        {
            if (_childNode != null)
            {
                _lineType = ConnectionLineType.Bezier;
                _lineColor = Color.Gray;
                _lineWidth = 1.5f;
            }
        }

        /// <summary>
        /// 判断连接线是否包含指定节点
        /// </summary>
        public bool ContainsNode(MindMapNode node)
        {
            return _parentNode == node || _childNode == node;
        }

        /// <summary>
        /// 判断是否是指定的父子连接
        /// </summary>
        public bool IsConnection(MindMapNode parent, MindMapNode child)
        {
            return _parentNode == parent && _childNode == child;
        }
        #endregion
    }
}
