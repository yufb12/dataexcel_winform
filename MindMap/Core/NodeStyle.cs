using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MindMap.Core
{
    /// <summary>
    /// 节点样式类（v2.0架构扩展版）
    /// 封装节点的所有外观属性，支持完整自定义
    /// 
    /// 【v2.0新增】
    /// - 节点顶部图片：TopImage（XMind风格主题图）
    /// - 节点背景图：BackgroundImage + BackgroundImageMode
    /// - 主标题+副标题：Subtitle + SubtitleFont + SubtitleColor
    /// 
    /// 【v1.9.1新增】
    /// - 多图标支持：List<Image> Icons（一个节点可显示多个图标并排）
    /// - 命名修复：BorderStyle → NodeBorderStyle（避免与System.Windows.Forms冲突）
    /// 
    /// 【架构设计】
    /// - 单一职责：只负责样式数据存储
    /// - 开闭原则：新增样式属性只需在此添加
    /// - 可克隆：支持深拷贝
    /// - 序列化：支持二进制序列化
    /// </summary>
    [Serializable]
    public class NodeStyle : ICloneable
    {
        #region 背景样式
        private Color _backColor;
        private Color _backColor2;      // 渐变结束色
        private bool _useGradient;      // 是否使用渐变背景

        /// <summary>
        /// 获取或设置节点背景颜色
        /// </summary>
        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        /// <summary>
        /// 获取或设置节点渐变背景结束色
        /// </summary>
        public Color BackColor2
        {
            get { return _backColor2; }
            set { _backColor2 = value; }
        }

        /// <summary>
        /// 获取或设置是否使用渐变背景
        /// </summary>
        public bool UseGradient
        {
            get { return _useGradient; }
            set { _useGradient = value; }
        }
        #endregion

        #region 背景图片（v2.0新增）
        private Image _backgroundImage;
        private BackgroundImageMode _backgroundImageMode;

        /// <summary>
        /// 获取或设置节点背景图片（v2.0新增）
        /// </summary>
        public Image BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }

        /// <summary>
        /// 获取或设置背景图片填充模式（v2.0新增）
        /// </summary>
        public BackgroundImageMode BackgroundImageMode
        {
            get { return _backgroundImageMode; }
            set { _backgroundImageMode = value; }
        }
        #endregion

        #region 文本样式
        private Color _foreColor;
        private Font _font;

        /// <summary>
        /// 获取或设置节点文本颜色（前景色）
        /// </summary>
        public Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        /// <summary>
        /// 获取或设置节点字体
        /// </summary>
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }
        #endregion

        #region 副标题（v2.0新增，主标题+副标题形状专用）
        private string _subtitle;
        private Font _subtitleFont;
        private Color _subtitleColor;
        private int _subtitleSpacing;

        /// <summary>
        /// 获取或设置副标题文本（v2.0新增，TitleWithSubtitle形状专用）
        /// </summary>
        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; }
        }

        /// <summary>
        /// 获取或设置副标题字体（v2.0新增）
        /// </summary>
        public Font SubtitleFont
        {
            get { return _subtitleFont; }
            set { _subtitleFont = value; }
        }

        /// <summary>
        /// 获取或设置副标题颜色（v2.0新增）
        /// </summary>
        public Color SubtitleColor
        {
            get { return _subtitleColor; }
            set { _subtitleColor = value; }
        }

        /// <summary>
        /// 获取或设置主标题与副标题之间的间距（v2.0新增）
        /// </summary>
        public int SubtitleSpacing
        {
            get { return _subtitleSpacing; }
            set { _subtitleSpacing = value; }
        }
        #endregion

        #region 边框样式（v1.9.1修复命名冲突）
        private Color _borderColor;
        private float _borderWidth;
        private NodeBorderStyle _borderStyle;

        /// <summary>
        /// 获取或设置节点边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        /// <summary>
        /// 获取或设置节点边框宽度
        /// </summary>
        public float BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        /// <summary>
        /// 获取或设置节点边框样式
        /// </summary>
        public NodeBorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set { _borderStyle = value; }
        }

        private bool _showBorder;
        /// <summary>
        /// 获取或设置是否显示边框（v2.1.6新增）
        /// </summary>
        public bool ShowBorder
        {
            get { return _showBorder; }
            set { _showBorder = value; }
        }
        #endregion

        #region 图标样式（v1.9.1新增多图标支持）
        private List<Image> _icons;          // 多图标集合（XMind风格：优先级+表情+符号）
        private IconPosition _iconPosition;
        private Size _iconSize;
        private int _iconSpacing;            // 图标之间的间距

        /// <summary>
        /// 获取节点图标集合（v1.9.1新增：支持多图标并排显示）
        /// 如：[优先级1] [笑脸] [对勾] 节点文本
        /// </summary>
        public List<Image> Icons
        {
            get { return _icons; }
        }

        /// <summary>
        /// 获取或设置图标位置
        /// </summary>
        public IconPosition IconPosition
        {
            get { return _iconPosition; }
            set { _iconPosition = value; }
        }

        /// <summary>
        /// 获取或设置单个图标显示尺寸
        /// </summary>
        public Size IconSize
        {
            get { return _iconSize; }
            set { _iconSize = value; }
        }

        /// <summary>
        /// 获取或设置图标之间的间距（多图标时）
        /// </summary>
        public int IconSpacing
        {
            get { return _iconSpacing; }
            set { _iconSpacing = value; }
        }

        /// <summary>
        /// 兼容旧版本：单个图标属性（向后兼容）
        /// </summary>
        [Obsolete("使用Icons集合替代，支持多图标")]
        public Image Icon
        {
            get 
            { 
                if (_icons.Count > 0) 
                    return _icons[0]; 
                return null; 
            }
            set 
            { 
                _icons.Clear(); 
                if (value != null) 
                    _icons.Add(value); 
            }
        }

        /// <summary>
        /// 添加图标（便捷方法）
        /// </summary>
        public void AddIcon(Image icon)
        {
            if (icon != null)
                _icons.Add((Image)icon.Clone());
        }

        /// <summary>
        /// 移除指定索引的图标
        /// </summary>
        public void RemoveIconAt(int index)
        {
            if (index >= 0 && index < _icons.Count)
            {
                _icons[index].Dispose();
                _icons.RemoveAt(index);
            }
        }

        /// <summary>
        /// 清除所有图标
        /// </summary>
        public void ClearIcons()
        {
            foreach (Image icon in _icons)
                icon.Dispose();
            _icons.Clear();
        }

        /// <summary>
        /// 获取图标总宽度（所有图标+间距）
        /// </summary>
        public int GetTotalIconsWidth()
        {
            if (_icons.Count == 0) return 0;
            return _icons.Count * _iconSize.Width + (_icons.Count - 1) * _iconSpacing;
        }
        #endregion

        #region 节点顶部图片（v2.0新增，XMind风格主题图）
        private Image _topImage;
        private Size _topImageSize;
        private int _topImageSpacing;

        /// <summary>
        /// 获取或设置节点顶部图片（v2.0新增，XMind风格主题图）
        /// 图片显示在节点主体上方
        /// </summary>
        public Image TopImage
        {
            get { return _topImage; }
            set { _topImage = value; }
        }

        /// <summary>
        /// 获取或设置顶部图片显示尺寸（v2.0新增）
        /// </summary>
        public Size TopImageSize
        {
            get { return _topImageSize; }
            set { _topImageSize = value; }
        }

        /// <summary>
        /// 获取或设置顶部图片与节点主体的间距（v2.0新增）
        /// </summary>
        public int TopImageSpacing
        {
            get { return _topImageSpacing; }
            set { _topImageSpacing = value; }
        }
        #endregion

        #region 形状与图片
        private NodeShape _shape;
        private int _cornerRadius;
        private Image _image;                            // 大图节点

        /// <summary>
        /// 获取或设置节点形状
        /// </summary>
        public NodeShape Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        /// <summary>
        /// 获取或设置圆角半径（仅对圆角矩形有效）
        /// </summary>
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; }
        }

        /// <summary>
        /// 获取或设置节点图片（图片节点专用，大图）
        /// </summary>
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }
        #endregion



        #region 构造函数
        /// <summary>
        /// 初始化默认节点样式（v2.0扩展版）
        /// </summary>
        public NodeStyle()
        {
            // 背景
            _backColor = Color.LightBlue;
            _backColor2 = Color.White;
            _useGradient = true;

            // 背景图（v2.0）
            _backgroundImage = null;
            _backgroundImageMode = BackgroundImageMode.Stretch;

            // 文本
            _foreColor = Color.Black;
            _font = new Font("微软雅黑", 10f, FontStyle.Regular);

            // 副标题（v2.0）
            _subtitle = string.Empty;
            _subtitleFont = new Font("微软雅黑", 8f, FontStyle.Regular);
            _subtitleColor = Color.Gray;
            _subtitleSpacing = 4;

            // 边框
            _borderColor = Color.Gray;
            _borderWidth = 1f;
            _borderStyle = NodeBorderStyle.Solid;
            _showBorder = true;  // v2.1.6：默认显示边框

            // 图标（v1.9.1：多图标集合）
            _icons = new List<Image>();
            _iconPosition = IconPosition.Left;
            _iconSize = new Size(16, 16);
            _iconSpacing = 4;  // 图标之间4px间距

            // 顶部图片（v2.0）
            _topImage = null;
            _topImageSize = new Size(120, 80);
            _topImageSpacing = 8;

            // 形状
            _shape = NodeShape.RoundedRectangle;
            _cornerRadius = 5;
            _image = null;
        }
        #endregion

        #region 静态工厂方法（预设样式）
        /// <summary>
        /// 创建根节点样式
        /// </summary>
        public static NodeStyle CreateRootStyle()
        {
            return new NodeStyle
            {
                BackColor = Color.Orange,
                BackColor2 = Color.LightGoldenrodYellow,
                UseGradient = true,
                ForeColor = Color.Black,
                BorderColor = Color.DarkOrange,
                BorderWidth = 2f,
                BorderStyle = NodeBorderStyle.Solid,
                Shape = NodeShape.RoundedRectangle,
                CornerRadius = 10,
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                IconPosition = IconPosition.None,
                // v2.0副标题默认值
                SubtitleFont = new Font("微软雅黑", 9f, FontStyle.Regular),
                SubtitleColor = Color.LightGray,
                SubtitleSpacing = 6,
                // v2.0顶部图片默认值
                TopImageSize = new Size(140, 100),
                TopImageSpacing = 10
            };
        }

        /// <summary>
        /// 创建一级节点样式
        /// </summary>
        public static NodeStyle CreateMainBranchStyle()
        {
            return new NodeStyle
            {
                BackColor = Color.LightGreen,
                BackColor2 = Color.White,
                BorderColor = Color.Green,
                BorderWidth = 1.5f,
                BorderStyle = NodeBorderStyle.Solid,
                Shape = NodeShape.RoundedRectangle,
                CornerRadius = 8,
                Font = new Font("微软雅黑", 11f, FontStyle.Bold),
                IconPosition = IconPosition.Left,
                IconSize = new Size(16, 16),
                IconSpacing = 4,
                // v2.0副标题默认值
                SubtitleFont = new Font("微软雅黑", 8.5f, FontStyle.Regular),
                SubtitleColor = Color.Gray,
                SubtitleSpacing = 4,
                // v2.0顶部图片默认值
                TopImageSize = new Size(100, 70),
                TopImageSpacing = 8
            };
        }

        /// <summary>
        /// 创建子节点样式
        /// </summary>
        public static NodeStyle CreateSubBranchStyle()
        {
            return new NodeStyle
            {
                BackColor2 = Color.White,
                UseGradient = true,
                ForeColor = Color.Black,
                BorderColor = Color.Gray,
                BorderWidth = 1f,
                BorderStyle = NodeBorderStyle.Solid,
                Shape = NodeShape.RoundedRectangle,
                CornerRadius = 6,
                Font = new Font("微软雅黑", 10f, FontStyle.Regular),
                IconPosition = IconPosition.Left,
                IconSize = new Size(16, 16),
                IconSpacing = 4,
                // v2.0副标题默认值
                SubtitleFont = new Font("微软雅黑", 8f, FontStyle.Regular),
                SubtitleColor = Color.Gray,
                SubtitleSpacing = 4,
                // v2.0顶部图片默认值
                TopImageSize = new Size(80, 55),
                TopImageSpacing = 6
            };
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 将NodeBorderStyle转换为GDI+ DashStyle（渲染器专用）
        /// </summary>
        public DashStyle GetDashStyle()
        {
            switch (_borderStyle)
            {
                case NodeBorderStyle.Solid:
                    return DashStyle.Solid;
                case NodeBorderStyle.Dash:
                    return DashStyle.Dash;
                case NodeBorderStyle.Dot:
                    return DashStyle.Dot;
                case NodeBorderStyle.DashDot:
                    return DashStyle.DashDot;
                case NodeBorderStyle.DashDotDot:
                    return DashStyle.DashDotDot;
                case NodeBorderStyle.None:
                default:
                    return DashStyle.Solid;
            }
        }

        /// <summary>
        /// 创建样式的深拷贝（v2.0扩展版）
        /// </summary>
        public object Clone()
        {
            NodeStyle clone = new NodeStyle();
            
            // 背景
            clone._backColor = this._backColor;
            clone._backColor2 = this._backColor2;
            clone._useGradient = this._useGradient;

            // 背景图（v2.0）
            if (this._backgroundImage != null)
                clone._backgroundImage = (Image)this._backgroundImage.Clone();
            clone._backgroundImageMode = this._backgroundImageMode;
            
            // 文本
            clone._foreColor = this._foreColor;
            clone._font = (Font)this._font.Clone();

            // 副标题（v2.0）
            clone._subtitle = this._subtitle;
            clone._subtitleFont = (Font)this._subtitleFont.Clone();
            clone._subtitleColor = this._subtitleColor;
            clone._subtitleSpacing = this._subtitleSpacing;
            
            // 边框
            clone._borderColor = this._borderColor;
            clone._borderWidth = this._borderWidth;
            clone._borderStyle = this._borderStyle;
            clone._showBorder = this._showBorder;  // v2.1.6：复制边框显示属性
            
            // 图标（v1.9.1：深拷贝所有图标）
            clone._iconPosition = this._iconPosition;
            clone._iconSize = this._iconSize;
            clone._iconSpacing = this._iconSpacing;
            clone._icons.Clear();
            foreach (Image icon in this._icons)
                clone._icons.Add((Image)icon.Clone());

            // 顶部图片（v2.0）
            if (this._topImage != null)
                clone._topImage = (Image)this._topImage.Clone();
            clone._topImageSize = this._topImageSize;
            clone._topImageSpacing = this._topImageSpacing;
            
            // 形状
            clone._shape = this._shape;
            clone._cornerRadius = this._cornerRadius;
            if (this._image != null)
                clone._image = (Image)this._image.Clone();
            
            return clone;
        }
        #endregion
    }
}
