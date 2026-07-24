namespace MindMap.Layout
{
    /// <summary>
    /// 布局常量定义（增加间距避免节点重叠）
    /// </summary>
    internal static class LayoutConstants
    {
        /// <summary>
        /// 默认画布中心X坐标
        /// </summary>
        public const float DefaultCenterX = 400f;
        /// <summary>
        /// 默认画布中心Y坐标
        /// </summary>
        public const float DefaultCenterY = 300f;
        /// <summary>
        /// 节点间最小距离增量
        /// </summary>
        public const float DistanceIncrement = 20f;
        /// <summary>
        /// 最大布局距离限制
        /// </summary>
        public const float MaximumDistance = 800f;
        /// <summary>
        /// 初始距离因子（相对于父节点尺寸）
        /// </summary>
        public const float InitialDistanceFactor = 1.8f;

        /// <summary>
        /// 水平间距（节点之间的水平距离）
        /// </summary>
        public const float HorizontalSpacing = 120f;

        /// <summary>
        /// 垂直间距（节点之间的垂直距离）
        /// </summary>
        public const float VerticalSpacing = 60f;

        /// <summary>
        /// 层级间距（不同深度层级之间的距离）
        /// </summary>
        public const float LevelSpacing = 100f;

        /// <summary>
        /// 节点间最小间距
        /// </summary>
        public const float NodeSpacing = 40f;

        /// <summary>
        /// 圆形布局半径增量
        /// </summary>
        public const float CircleRadiusStep = 180f;

        /// <summary>
        /// 螺旋布局间距
        /// </summary>
        public const float SpiralSpacing = 150f;

        /// <summary>
        /// 扇形布局角度间距
        /// </summary>
        public const float FanAngleStep = 25f;

        /// <summary>
        /// 瀑布布局偏移量
        /// </summary>
        public const float WaterfallOffset = 100f;

        /// <summary>
        /// 鱼骨图分支间距
        /// </summary>
        public const float FishboneBranchSpacing = 100f;

        /// <summary>
        /// 时间线节点间距
        /// </summary>
        public const float TimelineSpacing = 180f;

        /// <summary>
        /// 组织结构图间距
        /// </summary>
        public const float OrgChartSpacing = 120f;
    }
}
