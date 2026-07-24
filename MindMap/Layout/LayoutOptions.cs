using System;
using System.Drawing;

namespace MindMap.Layout
{
    /// <summary>
    /// 【布局参数配置类】
    /// 使用对象封装布局参数，替代固定const常数
    /// 支持动态调整，适应不同大小的节点和子树
    /// 
    /// 【设计模式】
    /// - Options模式：将所有配置参数封装为一个对象
    /// - Builder模式：通过Fluent API构建参数
    /// 
    /// 【设计原则】
    /// - SRP单一职责：只负责存储布局参数
    /// - OCP开闭原则：新增参数只需扩展此类，不影响布局引擎
    /// </summary>
    public class LayoutOptions
    {
        #region 间距参数（基于节点大小的系数，不是固定值）

        /// <summary>
        /// 水平间距系数（相对于节点宽度的比例）
        /// 默认0.8 = 节点宽度的80%作为间距
        /// </summary>
        public float HorizontalSpacingRatio { get; set; }

        /// <summary>
        /// 垂直间距系数（相对于节点高度的比例）
        /// 默认1.0 = 节点高度的100%作为间距
        /// </summary>
        public float VerticalSpacingRatio { get; set; }

        /// <summary>
        /// 最小水平间距（像素）
        /// 防止节点太小时间距也太小
        /// </summary>
        public float MinHorizontalSpacing { get; set; }

        /// <summary>
        /// 最小垂直间距（像素）
        /// </summary>
        public float MinVerticalSpacing { get; set; }

        /// <summary>
        /// 层级间距系数（相对于父节点大小的比例）
        /// 默认1.5 = 父节点大小的150%作为层级间距
        /// </summary>
        public float LevelSpacingRatio { get; set; }

        /// <summary>
        /// 最小层级间距（像素）
        /// </summary>
        public float MinLevelSpacing { get; set; }

        #endregion

        #region 布局质量参数

        /// <summary>
        /// 最大迭代次数（重叠调整）
        /// </summary>
        public int MaxIterations { get; set; }

        /// <summary>
        /// 重叠调整步长系数
        /// </summary>
        public float OverlapAdjustmentFactor { get; set; }

        /// <summary>
        /// 是否启用重叠检测
        /// </summary>
        public bool EnableOverlapDetection { get; set; }

        #endregion

        #region 放射状/圆形布局专用参数

        /// <summary>
        /// 径向间距系数（相对于节点大小的比例）
        /// </summary>
        public float RadialSpacingRatio { get; set; }

        /// <summary>
        /// 角度间距（弧度）
        /// </summary>
        public float AngularSpacing { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数，使用推荐的默认值
        /// </summary>
        public LayoutOptions()
        {
            // 间距参数
            HorizontalSpacingRatio = 0.8f;
            VerticalSpacingRatio = 1.0f;
            MinHorizontalSpacing = 60f;
            MinVerticalSpacing = 30f;
            LevelSpacingRatio = 1.5f;
            MinLevelSpacing = 100f;

            // 布局质量
            MaxIterations = 30;
            OverlapAdjustmentFactor = 1.2f;
            EnableOverlapDetection = true;

            // 放射状布局
            RadialSpacingRatio = 1.2f;
            AngularSpacing = 0.1f;
        }

        #endregion

        #region 动态计算方法

        /// <summary>
        /// 根据节点大小计算水平间距
        /// </summary>
        public float CalculateHorizontalSpacing(SizeF nodeSize)
        {
            float spacing = nodeSize.Width * HorizontalSpacingRatio;
            return Math.Max(spacing, MinHorizontalSpacing);
        }

        /// <summary>
        /// 根据节点大小计算垂直间距
        /// </summary>
        public float CalculateVerticalSpacing(SizeF nodeSize)
        {
            float spacing = nodeSize.Height * VerticalSpacingRatio;
            return Math.Max(spacing, MinVerticalSpacing);
        }

        /// <summary>
        /// 根据父节点大小计算层级间距
        /// </summary>
        public float CalculateLevelSpacing(SizeF parentSize)
        {
            // 防御性检查：确保宽高都是正数
            float w = Math.Max(parentSize.Width, 1f);
            float h = Math.Max(parentSize.Height, 1f);
            
            float parentDiagonal = (float)Math.Sqrt(w * w + h * h);
            float spacing = parentDiagonal * LevelSpacingRatio;
            return Math.Max(spacing, MinLevelSpacing);
        }

        /// <summary>
        /// 根据节点大小计算径向间距
        /// </summary>
        public float CalculateRadialSpacing(SizeF nodeSize)
        {
            // 防御性检查：确保宽高都是正数
            float w = Math.Max(nodeSize.Width, 1f);
            float h = Math.Max(nodeSize.Height, 1f);
            
            float nodeDiagonal = (float)Math.Sqrt(w * w + h * h);
            return nodeDiagonal * RadialSpacingRatio;
        }

        #endregion

        #region 工厂方法（预设配置）

        /// <summary>
        /// 创建紧凑布局配置
        /// </summary>
        public static LayoutOptions CreateCompact()
        {
            return new LayoutOptions
            {
                HorizontalSpacingRatio = 0.5f,
                VerticalSpacingRatio = 0.6f,
                MinHorizontalSpacing = 40f,
                MinVerticalSpacing = 20f,
                LevelSpacingRatio = 1.0f,
                MinLevelSpacing = 80f
            };
        }

        /// <summary>
        /// 创建宽松布局配置
        /// </summary>
        public static LayoutOptions CreateSpacious()
        {
            return new LayoutOptions
            {
                HorizontalSpacingRatio = 1.2f,
                VerticalSpacingRatio = 1.5f,
                MinHorizontalSpacing = 80f,
                MinVerticalSpacing = 50f,
                LevelSpacingRatio = 2.0f,
                MinLevelSpacing = 150f
            };
        }

        /// <summary>
        /// 创建默认布局配置
        /// </summary>
        public static LayoutOptions CreateDefault()
        {
            return new LayoutOptions();
        }

        #endregion
    }
}
