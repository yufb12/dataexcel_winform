using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】节点样式 - 纯数据部分
    /// 负责：所有样式属性定义（无业务逻辑）
    /// </summary>
    [Serializable]
    public partial class NodeStyle
    {
        #region 字段

        // 背景样式
        private Color _backColor;
        private Color _backColor2;
        private bool _useGradient;

        // 文本样式
        private Color _foreColor;
        private Font _font;

        // 边框样式
        private Color _borderColor;
        private float _borderWidth;
        private NodeBorderStyle _borderStyle;
        private bool _showBorder;

        // 图标样式
        private List<Image> _icons;
        private IconPosition _iconPosition;
        private Size _iconSize;
        private int _iconSpacing;

        // 形状与圆角
        private NodeShape _shape;
        private int _cornerRadius;

        // 图片节点
        private Image _image;

        // 节点顶部图片（v2.0）
        private Image _topImage;
        private Size _topImageSize;
        private int _topImageSpacing;

        // 节点背景图（v2.0）
        private Image _backgroundImage;
        private BackgroundImageMode _backgroundImageMode;

        // 主标题+副标题（v2.0）
        private string _subtitle;
        private Font _subtitleFont;
        private Color _subtitleColor;
        private int _subtitleSpacing;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化默认样式
        /// </summary>
        public NodeStyle()
        {
            // 背景
            _backColor = Color.White;
            _backColor2 = Color.LightGray;
            _useGradient = false;

            // 文本
            _foreColor = Color.Black;
            _font = new Font("微软雅黑", 10f);

            // 边框
            _borderColor = Color.Gray;
            _borderWidth = 1f;
            _borderStyle = NodeBorderStyle.Solid;
            _showBorder = true;

            // 图标
            _icons = new List<Image>();
            _iconPosition = IconPosition.Left;
            _iconSize = new Size(16, 16);
            _iconSpacing = 4;

            // 形状
            _shape = NodeShape.RoundedRectangle;
            _cornerRadius = 8;

            // 图片
            _image = null;
            _topImage = null;
            _topImageSize = new Size(120, 80);
            _topImageSpacing = 8;
            _backgroundImage = null;
            _backgroundImageMode = BackgroundImageMode.Stretch;

            // 副标题
            _subtitle = string.Empty;
            _subtitleFont = new Font("微软雅黑", 8f);
            _subtitleColor = Color.Gray;
            _subtitleSpacing = 4;
        }

        #endregion

        #region 背景样式属性

        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        public Color BackColor2
        {
            get { return _backColor2; }
            set { _backColor2 = value; }
        }

        public bool UseGradient
        {
            get { return _useGradient; }
            set { _useGradient = value; }
        }

        #endregion

        #region 文本样式属性

        public Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        #endregion

        #region 边框样式属性

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        public float BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        public NodeBorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set { _borderStyle = value; }
        }

        public bool ShowBorder
        {
            get { return _showBorder; }
            set { _showBorder = value; }
        }

        #endregion

        #region 图标样式属性

        public List<Image> Icons
        {
            get { return _icons; }
        }

        public IconPosition IconPosition
        {
            get { return _iconPosition; }
            set { _iconPosition = value; }
        }

        public Size IconSize
        {
            get { return _iconSize; }
            set { _iconSize = value; }
        }

        public int IconSpacing
        {
            get { return _iconSpacing; }
            set { _iconSpacing = value; }
        }

        #endregion

        #region 形状与图片属性

        public NodeShape Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; }
        }

        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        #endregion

        #region 顶部图片属性

        public Image TopImage
        {
            get { return _topImage; }
            set { _topImage = value; }
        }

        public Size TopImageSize
        {
            get { return _topImageSize; }
            set { _topImageSize = value; }
        }

        public int TopImageSpacing
        {
            get { return _topImageSpacing; }
            set { _topImageSpacing = value; }
        }

        #endregion

        #region 背景图属性

        public Image BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }

        public BackgroundImageMode BackgroundImageMode
        {
            get { return _backgroundImageMode; }
            set { _backgroundImageMode = value; }
        }

        #endregion

        #region 副标题属性

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value ?? string.Empty; }
        }

        public Font SubtitleFont
        {
            get { return _subtitleFont; }
            set { _subtitleFont = value; }
        }

        public Color SubtitleColor
        {
            get { return _subtitleColor; }
            set { _subtitleColor = value; }
        }

        public int SubtitleSpacing
        {
            get { return _subtitleSpacing; }
            set { _subtitleSpacing = value; }
        }

        #endregion

        #region 连接线样式属性（从Connection类读取）

        /// <summary>
        /// 兼容属性（从连接线读取）
        /// </summary>
        [Obsolete("连接线属性已移至Connection类")]
        public Color LineColor
        {
            get { return Color.Gray; }
            set { }
        }

        #endregion
    }
}
