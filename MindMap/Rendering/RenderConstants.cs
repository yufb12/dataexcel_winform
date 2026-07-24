using System.Drawing;

namespace MindMap.Rendering
{
    /// <summary>
    /// 渲染常量定义
    /// </summary>
    internal static class RenderConstants
    {
        /// <summary>
        /// 节点圆角半径
        /// </summary>
        public const float NodeCornerRadius = 6f;
        /// <summary>
        /// 节点内边距
        /// </summary>
        public const float NodePadding = 8f;

        /// <summary>
        /// 选中边框宽度
        /// </summary>
        public const float SelectionBorderWidth = 2f;

        /// <summary>
        /// 选中边框外边距
        /// </summary>
        public const float SelectionBorderMargin = 2f;

        /// <summary>
        /// 展开按钮大小
        /// </summary>
        public const float ExpandButtonSize = 10f;

        /// <summary>
        /// 展开按钮与节点的间距
        /// </summary>
        public const float ExpandButtonSpacing = 2f;

        /// <summary>
        /// 默认连接线宽度
        /// </summary>
        public const float DefaultLineWidth = 1.5f;

        /// <summary>
        /// 默认连接线颜色
        /// </summary>
        public static readonly Color DefaultLineColor = Color.Gray;

        /// <summary>
        /// 选中边框颜色
        /// </summary>
        public static readonly Color SelectionBorderColor = Color.Blue;
    }
}
