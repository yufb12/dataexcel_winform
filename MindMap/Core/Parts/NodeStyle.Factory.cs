using System.Drawing;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】节点样式 - 工厂方法部分
    /// 负责：创建不同层级的默认样式
    /// 【设计模式】Factory工厂模式
    /// </summary>
    public partial class NodeStyle
    {
        #region 工厂方法

        /// <summary>
        /// 创建根节点样式
        /// </summary>
        public static NodeStyle CreateRootStyle()
        {
            return new NodeStyle
            {
                BackColor = Color.FromArgb(66, 133, 244),
                BackColor2 = Color.FromArgb(30, 100, 200),
                UseGradient = true,
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 14f, FontStyle.Bold),
                BorderColor = Color.FromArgb(25, 80, 160),
                BorderWidth = 2f,
                CornerRadius = 12
            };
        }

        /// <summary>
        /// 创建主分支节点样式
        /// </summary>
        public static NodeStyle CreateMainBranchStyle()
        {
            return new NodeStyle
            {
                BackColor = Color.FromArgb(255, 255, 255),
                BackColor2 = Color.FromArgb(240, 245, 255),
                UseGradient = true,
                ForeColor = Color.FromArgb(32, 33, 36),
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                BorderColor = Color.FromArgb(66, 133, 244),
                BorderWidth = 1.5f,
                CornerRadius = 8
            };
        }

        /// <summary>
        /// 创建子分支节点样式
        /// </summary>
        public static NodeStyle CreateSubBranchStyle()
        {
            return new NodeStyle
            {
                BackColor = Color.White,
                UseGradient = false,
                ForeColor = Color.FromArgb(60, 64, 67),
                Font = new Font("微软雅黑", 10f),
                BorderColor = Color.FromArgb(218, 220, 224),
                BorderWidth = 1f,
                CornerRadius = 6
            };
        }

        #endregion

        #region 图标管理方法

        /// <summary>
        /// 添加图标
        /// </summary>
        public void AddIcon(Image icon)
        {
            if (icon != null)
            {
                _icons.Add(icon);
            }
        }

        /// <summary>
        /// 移除指定索引的图标
        /// </summary>
        public void RemoveIconAt(int index)
        {
            if (index >= 0 && index < _icons.Count)
            {
                _icons.RemoveAt(index);
            }
        }

        /// <summary>
        /// 清除所有图标
        /// </summary>
        public void ClearIcons()
        {
            _icons.Clear();
        }

        /// <summary>
        /// 获取所有图标总宽度（含间距）
        /// </summary>
        public int GetTotalIconsWidth()
        {
            if (_icons.Count == 0) return 0;
            return _icons.Count * _iconSize.Width + (_icons.Count - 1) * _iconSpacing;
        }

        #endregion
    }
}
