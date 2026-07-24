using System;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】节点样式 - Builder建造者模式
    /// 负责：优雅的Fluent API链式调用构建样式
    /// 【设计模式】Builder建造者模式
    /// </summary>
    public partial class NodeStyle
    {
        #region Clone方法

        /// <summary>
        /// 深拷贝样式
        /// </summary>
        public object Clone()
        {
            NodeStyle clone = new NodeStyle();

            // 背景
            clone._backColor = _backColor;
            clone._backColor2 = _backColor2;
            clone._useGradient = _useGradient;

            // 文本
            clone._foreColor = _foreColor;
            clone._font = (Font)_font.Clone();

            // 边框
            clone._borderColor = _borderColor;
            clone._borderWidth = _borderWidth;
            clone._borderStyle = _borderStyle;
            clone._showBorder = _showBorder;

            // 图标
            clone._iconPosition = _iconPosition;
            clone._iconSize = _iconSize;
            clone._iconSpacing = _iconSpacing;
            foreach (Image icon in _icons)
            {
                clone._icons.Add((Image)icon.Clone());
            }

            // 形状
            clone._shape = _shape;
            clone._cornerRadius = _cornerRadius;

            // 图片
            if (_image != null) clone._image = (Image)_image.Clone();
            if (_topImage != null) clone._topImage = (Image)_topImage.Clone();
            clone._topImageSize = _topImageSize;
            clone._topImageSpacing = _topImageSpacing;
            if (_backgroundImage != null) clone._backgroundImage = (Image)_backgroundImage.Clone();
            clone._backgroundImageMode = _backgroundImageMode;

            // 副标题
            clone._subtitle = _subtitle;
            clone._subtitleFont = (Font)_subtitleFont.Clone();
            clone._subtitleColor = _subtitleColor;
            clone._subtitleSpacing = _subtitleSpacing;

            return clone;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取边框DashStyle
        /// </summary>
        public DashStyle GetDashStyle()
        {
            switch (_borderStyle)
            {
                case NodeBorderStyle.Dash: return DashStyle.Dash;
                case NodeBorderStyle.Dot: return DashStyle.Dot;
                case NodeBorderStyle.DashDot: return DashStyle.DashDot;
                case NodeBorderStyle.DashDotDot: return DashStyle.DashDotDot;
                case NodeBorderStyle.None: return DashStyle.Custom;
                default: return DashStyle.Solid;
            }
        }

        #endregion

        #region Builder建造者模式

        /// <summary>
        /// 创建样式构建器
        /// </summary>
        public static StyleBuilder Builder()
        {
            return new StyleBuilder();
        }

        /// <summary>
        /// 样式构建器 - Fluent API
        /// </summary>
        public class StyleBuilder
        {
            private readonly NodeStyle _style = new NodeStyle();

            public StyleBuilder WithBackColor(Color color)
            {
                _style._backColor = color;
                return this;
            }

            public StyleBuilder WithBackColorGradient(Color color1, Color color2)
            {
                _style._backColor = color1;
                _style._backColor2 = color2;
                _style._useGradient = true;
                return this;
            }

            public StyleBuilder WithForeColor(Color color)
            {
                _style._foreColor = color;
                return this;
            }

            public StyleBuilder WithFont(Font font)
            {
                _style._font = font;
                return this;
            }

            public StyleBuilder WithBorder(Color color, float width)
            {
                _style._borderColor = color;
                _style._borderWidth = width;
                return this;
            }

            public StyleBuilder WithBorderStyle(NodeBorderStyle style)
            {
                _style._borderStyle = style;
                return this;
            }

            public StyleBuilder WithShowBorder(bool show)
            {
                _style._showBorder = show;
                return this;
            }

            public StyleBuilder WithShape(NodeShape shape)
            {
                _style._shape = shape;
                return this;
            }

            public StyleBuilder WithCornerRadius(int radius)
            {
                _style._cornerRadius = radius;
                return this;
            }

            public StyleBuilder WithIconPosition(IconPosition position)
            {
                _style._iconPosition = position;
                return this;
            }

            /// <summary>
            /// 构建最终样式对象
            /// </summary>
            public NodeStyle Build()
            {
                return _style;
            }
        }

        #endregion
    }
}
