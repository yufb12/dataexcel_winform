using System;
using System.Drawing;

namespace MindMap.Core
{
    /// <summary>
    /// 主题配色方案
    /// </summary>
    [Serializable]
    public class Theme
    {
        private string _name;
        private Color _rootBackColor;
        private Color _rootForeColor;
        private Color _rootBorderColor;
        private Color _mainBranchBackColor;
        private Color _mainBranchForeColor;
        private Color _mainBranchBorderColor;
        private Color _subBranchBackColor;
        private Color _subBranchForeColor;
        private Color _subBranchBorderColor;
        private Color _connectionLineColor;
        private Color _backgroundColor;

        /// <summary>
        /// 主题名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 根节点背景色
        /// </summary>
        public Color RootBackColor
        {
            get { return _rootBackColor; }
            set { _rootBackColor = value; }
        }

        /// <summary>
        /// 根节点前景色
        /// </summary>
        public Color RootForeColor
        {
            get { return _rootForeColor; }
            set { _rootForeColor = value; }
        }

        /// <summary>
        /// 根节点边框色
        /// </summary>
        public Color RootBorderColor
        {
            get { return _rootBorderColor; }
            set { _rootBorderColor = value; }
        }

        /// <summary>
        /// 主分支背景色
        /// </summary>
        public Color MainBranchBackColor
        {
            get { return _mainBranchBackColor; }
            set { _mainBranchBackColor = value; }
        }

        /// <summary>
        /// 主分支前景色
        /// </summary>
        public Color MainBranchForeColor
        {
            get { return _mainBranchForeColor; }
            set { _mainBranchForeColor = value; }
        }

        /// <summary>
        /// 主分支边框色
        /// </summary>
        public Color MainBranchBorderColor
        {
            get { return _mainBranchBorderColor; }
            set { _mainBranchBorderColor = value; }
        }

        /// <summary>
        /// 子分支背景色
        /// </summary>
        public Color SubBranchBackColor
        {
            get { return _subBranchBackColor; }
            set { _subBranchBackColor = value; }
        }

        /// <summary>
        /// 子分支前景色
        /// </summary>
        public Color SubBranchForeColor
        {
            get { return _subBranchForeColor; }
            set { _subBranchForeColor = value; }
        }

        /// <summary>
        /// 子分支边框色
        /// </summary>
        public Color SubBranchBorderColor
        {
            get { return _subBranchBorderColor; }
            set { _subBranchBorderColor = value; }
        }

        /// <summary>
        /// 连接线颜色
        /// </summary>
        public Color ConnectionLineColor
        {
            get { return _connectionLineColor; }
            set { _connectionLineColor = value; }
        }

        /// <summary>
        /// 连接线颜色（兼容别名）
        /// </summary>
        public Color LineColor
        {
            get { return _connectionLineColor; }
            set { _connectionLineColor = value; }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        /// 创建默认主题（经典蓝绿）
        /// </summary>
        public static Theme CreateDefaultTheme()
        {
            return new Theme
            {
                _name = "经典蓝绿",
                _rootBackColor = Color.FromArgb(255, 108, 171, 221),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 74, 137, 187),
                _mainBranchBackColor = Color.FromArgb(255, 147, 213, 147),
                _mainBranchForeColor = Color.FromArgb(255, 30, 80, 30),
                _mainBranchBorderColor = Color.FromArgb(255, 107, 173, 107),
                _subBranchBackColor = Color.FromArgb(255, 200, 230, 255),
                _subBranchForeColor = Color.FromArgb(255, 40, 80, 120),
                _subBranchBorderColor = Color.FromArgb(255, 160, 190, 215),
                _connectionLineColor = Color.FromArgb(255, 150, 150, 150),
                _backgroundColor = Color.FromArgb(255, 248, 248, 248)
            };
        }

        /// <summary>
        /// 创建暖橙主题
        /// </summary>
        public static Theme CreateWarmOrangeTheme()
        {
            return new Theme
            {
                _name = "暖橙活力",
                _rootBackColor = Color.FromArgb(255, 255, 140, 0),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 220, 110, 0),
                _mainBranchBackColor = Color.FromArgb(255, 255, 200, 100),
                _mainBranchForeColor = Color.FromArgb(255, 100, 60, 0),
                _mainBranchBorderColor = Color.FromArgb(255, 220, 170, 70),
                _subBranchBackColor = Color.FromArgb(255, 255, 240, 200),
                _subBranchForeColor = Color.FromArgb(255, 120, 80, 20),
                _subBranchBorderColor = Color.FromArgb(255, 220, 200, 160),
                _connectionLineColor = Color.FromArgb(255, 200, 150, 100),
                _backgroundColor = Color.FromArgb(255, 255, 252, 245)
            };
        }

        /// <summary>
        /// 创建清新薄荷主题
        /// </summary>
        public static Theme CreateMintTheme()
        {
            return new Theme
            {
                _name = "清新薄荷",
                _rootBackColor = Color.FromArgb(255, 95, 191, 159),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 65, 161, 129),
                _mainBranchBackColor = Color.FromArgb(255, 165, 220, 200),
                _mainBranchForeColor = Color.FromArgb(255, 30, 80, 60),
                _mainBranchBorderColor = Color.FromArgb(255, 135, 190, 170),
                _subBranchBackColor = Color.FromArgb(255, 225, 245, 235),
                _subBranchForeColor = Color.FromArgb(255, 50, 100, 80),
                _subBranchBorderColor = Color.FromArgb(255, 195, 215, 205),
                _connectionLineColor = Color.FromArgb(255, 120, 180, 160),
                _backgroundColor = Color.FromArgb(255, 245, 250, 248)
            };
        }

        /// <summary>
        /// 创建商务深蓝主题
        /// </summary>
        public static Theme CreateBusinessTheme()
        {
            return new Theme
            {
                _name = "商务深蓝",
                _rootBackColor = Color.FromArgb(255, 52, 73, 94),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 32, 53, 74),
                _mainBranchBackColor = Color.FromArgb(255, 93, 109, 126),
                _mainBranchForeColor = Color.White,
                _mainBranchBorderColor = Color.FromArgb(255, 73, 89, 106),
                _subBranchBackColor = Color.FromArgb(255, 236, 240, 241),
                _subBranchForeColor = Color.FromArgb(255, 52, 73, 94),
                _subBranchBorderColor = Color.FromArgb(255, 206, 210, 211),
                _connectionLineColor = Color.FromArgb(255, 149, 165, 166),
                _backgroundColor = Color.FromArgb(255, 250, 250, 250)
            };
        }

        /// <summary>
        /// 创建粉色浪漫主题
        /// </summary>
        public static Theme CreatePinkTheme()
        {
            return new Theme
            {
                _name = "粉色浪漫",
                _rootBackColor = Color.FromArgb(255, 236, 112, 159),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 206, 82, 129),
                _mainBranchBackColor = Color.FromArgb(255, 255, 182, 193),
                _mainBranchForeColor = Color.FromArgb(255, 120, 40, 80),
                _mainBranchBorderColor = Color.FromArgb(255, 225, 152, 163),
                _subBranchBackColor = Color.FromArgb(255, 255, 230, 240),
                _subBranchForeColor = Color.FromArgb(255, 140, 60, 100),
                _subBranchBorderColor = Color.FromArgb(255, 225, 200, 210),
                _connectionLineColor = Color.FromArgb(255, 220, 150, 180),
                _backgroundColor = Color.FromArgb(255, 255, 248, 252)
            };
        }

        /// <summary>
        /// 创建暗夜黑主题（深色背景，高对比度）
        /// </summary>
        public static Theme CreateDarkTheme()
        {
            return new Theme
            {
                _name = "暗夜黑",
                _rootBackColor = Color.FromArgb(255, 50, 50, 50),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 80, 80, 80),
                _mainBranchBackColor = Color.FromArgb(255, 70, 70, 70),
                _mainBranchForeColor = Color.White,
                _mainBranchBorderColor = Color.FromArgb(255, 100, 100, 100),
                _subBranchBackColor = Color.FromArgb(255, 90, 90, 90),
                _subBranchForeColor = Color.White,
                _subBranchBorderColor = Color.FromArgb(255, 120, 120, 120),
                _connectionLineColor = Color.FromArgb(255, 150, 150, 150),
                _backgroundColor = Color.FromArgb(255, 30, 30, 30)
            };
        }

        /// <summary>
        /// 创建森林绿主题（自然绿色系，护眼）
        /// </summary>
        public static Theme CreateForestTheme()
        {
            return new Theme
            {
                _name = "森林绿",
                _rootBackColor = Color.FromArgb(255, 46, 125, 50),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 27, 94, 32),
                _mainBranchBackColor = Color.FromArgb(255, 129, 199, 132),
                _mainBranchForeColor = Color.FromArgb(255, 27, 94, 32),
                _mainBranchBorderColor = Color.FromArgb(255, 102, 187, 106),
                _subBranchBackColor = Color.FromArgb(255, 200, 230, 201),
                _subBranchForeColor = Color.FromArgb(255, 46, 125, 50),
                _subBranchBorderColor = Color.FromArgb(255, 165, 214, 167),
                _connectionLineColor = Color.FromArgb(255, 129, 199, 132),
                _backgroundColor = Color.FromArgb(255, 245, 250, 245)
            };
        }

        /// <summary>
        /// 创建海洋蓝主题（深浅蓝色渐变）
        /// </summary>
        public static Theme CreateOceanTheme()
        {
            return new Theme
            {
                _name = "海洋蓝",
                _rootBackColor = Color.FromArgb(255, 25, 118, 210),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 13, 71, 161),
                _mainBranchBackColor = Color.FromArgb(255, 100, 181, 246),
                _mainBranchForeColor = Color.FromArgb(255, 13, 71, 161),
                _mainBranchBorderColor = Color.FromArgb(255, 66, 165, 245),
                _subBranchBackColor = Color.FromArgb(255, 187, 222, 251),
                _subBranchForeColor = Color.FromArgb(255, 25, 118, 210),
                _subBranchBorderColor = Color.FromArgb(255, 144, 202, 249),
                _connectionLineColor = Color.FromArgb(255, 100, 181, 246),
                _backgroundColor = Color.FromArgb(255, 240, 248, 255)
            };
        }

        /// <summary>
        /// 创建日落橙主题（橙红紫渐变，温暖）
        /// </summary>
        public static Theme CreateSunsetTheme()
        {
            return new Theme
            {
                _name = "日落橙",
                _rootBackColor = Color.FromArgb(255, 230, 74, 25),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 191, 54, 12),
                _mainBranchBackColor = Color.FromArgb(255, 255, 167, 38),
                _mainBranchForeColor = Color.FromArgb(255, 191, 54, 12),
                _mainBranchBorderColor = Color.FromArgb(255, 255, 145, 0),
                _subBranchBackColor = Color.FromArgb(255, 255, 224, 130),
                _subBranchForeColor = Color.FromArgb(255, 230, 74, 25),
                _subBranchBorderColor = Color.FromArgb(255, 255, 204, 128),
                _connectionLineColor = Color.FromArgb(255, 255, 145, 0),
                _backgroundColor = Color.FromArgb(255, 255, 250, 240)
            };
        }

        /// <summary>
        /// 创建极简灰主题（黑白灰，简约专业）
        /// </summary>
        public static Theme CreateMinimalTheme()
        {
            return new Theme
            {
                _name = "极简灰",
                _rootBackColor = Color.FromArgb(255, 66, 66, 66),
                _rootForeColor = Color.White,
                _rootBorderColor = Color.FromArgb(255, 33, 33, 33),
                _mainBranchBackColor = Color.FromArgb(255, 158, 158, 158),
                _mainBranchForeColor = Color.FromArgb(255, 33, 33, 33),
                _mainBranchBorderColor = Color.FromArgb(255, 117, 117, 117),
                _subBranchBackColor = Color.FromArgb(255, 224, 224, 224),
                _subBranchForeColor = Color.FromArgb(255, 66, 66, 66),
                _subBranchBorderColor = Color.FromArgb(255, 189, 189, 189),
                _connectionLineColor = Color.FromArgb(255, 158, 158, 158),
                _backgroundColor = Color.FromArgb(255, 250, 250, 250)
            };
        }

        /// <summary>
        /// 根据节点类型获取背景色
        /// </summary>
        public Color GetNodeBackColor(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.Root:
                    return _rootBackColor;
                case NodeType.MainBranch:
                    return _mainBranchBackColor;
                default:
                    return _subBranchBackColor;
            }
        }

        /// <summary>
        /// 根据节点类型获取前景色
        /// </summary>
        public Color GetNodeForeColor(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.Root:
                    return _rootForeColor;
                case NodeType.MainBranch:
                    return _mainBranchForeColor;
                default:
                    return _subBranchForeColor;
            }
        }

        /// <summary>
        /// 根据节点类型获取边框色
        /// </summary>
        public Color GetNodeBorderColor(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.Root:
                    return _rootBorderColor;
                case NodeType.MainBranch:
                    return _mainBranchBorderColor;
                default:
                    return _subBranchBorderColor;
            }
        }
    }
}
