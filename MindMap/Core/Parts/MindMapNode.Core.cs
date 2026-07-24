using System;
using System.Drawing;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】思维导图节点 - 核心属性部分
    /// 负责：节点基本属性（文本、位置、大小、层级、Tooltip等）
    /// </summary>
    [Serializable]
    public partial class MindMapNode
    {
        #region 字段

        private string _text;
        private PointF _position;
        private SizeF _size;
        private RectangleF _bounds;
        private NodeStyle _style;
        private NodeType _nodeType;
        private int _depth;
        private int _zOrder;
        private string _tooltip;
        private bool _isSelected;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化思维导图节点
        /// </summary>
        /// <param name="text">节点文本</param>
        public MindMapNode(string text)
        {
            _text = text ?? string.Empty;
            _position = new PointF(0, 0);
            _size = new SizeF(100, 30);
            _bounds = RectangleF.Empty;
            _style = new NodeStyle();
            _nodeType = NodeType.SubBranch;
            _depth = 0;
            _zOrder = 0;
            _tooltip = string.Empty;
            _isSelected = false;

            InitializeCollections();
            InitializeExpandState();
        }

        /// <summary>
        /// 初始化集合（由Tree部分实现）
        /// </summary>
        partial void InitializeCollections();

        /// <summary>
        /// 初始化展开状态（由Expand部分实现）
        /// </summary>
        partial void InitializeExpandState();

        #endregion

        #region 核心属性

        /// <summary>
        /// 获取或设置节点文本
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value ?? string.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 获取或设置节点位置
        /// </summary>
        public PointF Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _bounds.X = value.X;
                _bounds.Y = value.Y;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 获取或设置节点大小
        /// </summary>
        public SizeF Size
        {
            get { return _size; }
            set
            {
                _size = value;
                _bounds.Width = value.Width;
                _bounds.Height = value.Height;
            }
        }

        /// <summary>
        /// 获取节点中心点
        /// </summary>
        public PointF Center
        {
            get { return new PointF(_bounds.X + _bounds.Width / 2, _bounds.Y + _bounds.Height / 2); }
        }

        /// <summary>
        /// 获取子节点数量
        /// </summary>
        public int ChildCount
        {
            get { return ChildNodeCount; }
        }

        /// <summary>
        /// 获取或设置节点边界
        /// </summary>
        public RectangleF Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
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
        /// 获取或设置节点深度
        /// </summary>
        public int Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }

        /// <summary>
        /// 获取或设置Z序（层级）
        /// </summary>
        public int ZOrder
        {
            get { return _zOrder; }
            set { _zOrder = value; }
        }

        /// <summary>
        /// 获取或设置节点提示文本
        /// </summary>
        public string Tooltip
        {
            get { return _tooltip; }
            set { _tooltip = value ?? string.Empty; }
        }

        /// <summary>
        /// 获取或设置是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 检查点是否在节点边界内
        /// </summary>
        public bool ContainsPoint(PointF point)
        {
            return _bounds.Contains(point);
        }

        /// <summary>
        /// 自动计算节点大小
        /// </summary>
        /// <summary>
        /// 自动计算节点大小（无参数版本，使用默认字体估算）
        /// </summary>
        public void AutoCalculateSize()
        {
            if (string.IsNullOrEmpty(_text)) return;

            // 使用默认字体估算大小
            using (Font font = new Font("微软雅黑", 10f))
            {
                float charWidth = 8f;
                float charHeight = 16f;
                float padding = 16;
                float textWidth = _text.Length * charWidth;
                
                // 根据形状类型增加额外内边距
                float extraWidth = 0;
                float extraHeight = 0;
                
                switch (_style.Shape)
                {
                    case NodeShape.Diamond:
                        extraWidth = textWidth * 0.4f;
                        extraHeight = charHeight * 0.6f;
                        break;
                    case NodeShape.Hexagon:
                    case NodeShape.Octagon:
                        extraWidth = textWidth * 0.15f;
                        extraHeight = charHeight * 0.2f;
                        break;
                    case NodeShape.Parallelogram:
                        extraWidth = textWidth * 0.2f;
                        break;
                }
                
                _size = new SizeF(
                    textWidth + padding * 2 + extraWidth, 
                    charHeight + padding + extraHeight);
                _bounds.Width = _size.Width;
                _bounds.Height = _size.Height;
            }
        }

        public void AutoCalculateSize(Graphics g)
        {
            if (g == null || string.IsNullOrEmpty(_text)) return;

            SizeF textSize = g.MeasureString(_text, _style.Font);
            float padding = 16;
            
            // 根据形状类型增加额外内边距，确保文字在形状内部
            float extraWidth = 0;
            float extraHeight = 0;
            
            switch (_style.Shape)
            {
                case NodeShape.Diamond:
                    // 菱形：边角空间小，需要大幅增加宽高
                    extraWidth = textSize.Width * 0.4f;  // 增加40%宽度
                    extraHeight = textSize.Height * 0.6f; // 增加60%高度
                    break;
                case NodeShape.Hexagon:
                case NodeShape.Octagon:
                    // 多边形：增加少量内边距
                    extraWidth = textSize.Width * 0.15f;
                    extraHeight = textSize.Height * 0.2f;
                    break;
                case NodeShape.Parallelogram:
                    // 平行四边形：左右倾斜，增加宽度
                    extraWidth = textSize.Width * 0.2f;
                    break;
            }
            
            _size = new SizeF(
                textSize.Width + padding * 2 + extraWidth, 
                textSize.Height + padding + extraHeight);
            _bounds.Width = _size.Width;
            _bounds.Height = _size.Height;
        }

        /// <summary>
        /// 根据深度更新节点类型
        /// </summary>
        public void UpdateNodeTypeByDepth()
        {
            if (_depth == 0)
                _nodeType = NodeType.Root;
            else if (_depth == 1)
                _nodeType = NodeType.MainBranch;
            else
                _nodeType = NodeType.SubBranch;
        }

        /// <summary>
        /// 深拷贝节点（递归复制所有子节点）
        /// </summary>
        public MindMapNode DeepClone()
        {
            MindMapNode clone = new MindMapNode(_text);
            clone._position = _position;
            clone._size = _size;
            clone._bounds = _bounds;
            clone._nodeType = _nodeType;
            clone._depth = _depth;
            clone._zOrder = _zOrder;
            clone._tooltip = _tooltip;
            clone._isSelected = _isSelected;
            
            // 深拷贝样式
            if (_style != null)
            {
                clone._style = (NodeStyle)_style.Clone();
            }
            
            // 递归拷贝子节点和连接线
            // 注意：连接线也需要重新创建
            foreach (Connection conn in _childConnections)
            {
                if (conn.ChildNode != null)
                {
                    MindMapNode childClone = conn.ChildNode.DeepClone();
                    clone.AddChildNode(childClone);
                }
            }
            
            return clone;
        }

        #endregion
    }
}
