using System;

namespace MindMap.Core
{
    /// <summary>
    /// 节点类型枚举
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 根节点（中心主题）
        /// </summary>
        Root,
        /// <summary>
        /// 一级节点（主分支）
        /// </summary>
        MainBranch,
        /// <summary>
        /// 子节点（普通分支）
        /// </summary>
        SubBranch
    }

    /// <summary>
    /// 节点形状枚举（v2.0扩展）
    /// </summary>
    public enum NodeShape
    {
        /// <summary>
        /// 圆角矩形
        /// </summary>
        RoundedRectangle,
        /// <summary>
        /// 矩形
        /// </summary>
        Rectangle,
        /// <summary>
        /// 椭圆形
        /// </summary>
        Ellipse,
        /// <summary>
        /// 菱形
        /// </summary>
        Diamond,
        /// <summary>
        /// 平行四边形
        /// </summary>
        Parallelogram,
        /// <summary>
        /// 六边形
        /// </summary>
        Hexagon,
        /// <summary>
        /// 八角形
        /// </summary>
        Octagon,
        /// <summary>
        /// 图片节点（v1.8新增）
        /// </summary>
        Image,
        /// <summary>
        /// 主标题+副标题（v2.0新增，XMind风格）
        /// </summary>
        TitleWithSubtitle,
        /// <summary>
        /// 胶囊形状（v2.1.6新增，两边半圆）
        /// </summary>
        Pill,
        /// <summary>
        /// 下划线样式（文字+底部横线，XMind风格，v2.1.7新增）
        /// </summary>
        Underline
    }

    /// <summary>
    /// 背景图片填充模式（v2.0新增）
    /// </summary>
    public enum BackgroundImageMode
    {
        /// <summary>
        /// 拉伸填充（铺满整个节点）
        /// </summary>
        Stretch,
        /// <summary>
        /// 平铺（重复排列）
        /// </summary>
        Tile,
        /// <summary>
        /// 居中（保持原图大小居中）
        /// </summary>
        Center,
        /// <summary>
        /// 等比缩放（保持比例，完整显示）
        /// </summary>
        Zoom
    }

    /// <summary>
    /// 连接点位置枚举（v1.5新增）
    /// </summary>
    public enum ConnectionPoint
    {
        /// <summary>
        /// 自动（根据布局方向智能选择）
        /// </summary>
        Auto,
        /// <summary>
        /// 左侧中点
        /// </summary>
        Left,
        /// <summary>
        /// 右侧中点
        /// </summary>
        Right,
        /// <summary>
        /// 顶部中点
        /// </summary>
        Top,
        /// <summary>
        /// 底部中点
        /// </summary>
        Bottom,
        /// <summary>
        /// 中心点
        /// </summary>
        Center,
        /// <summary>
        /// 左上角
        /// </summary>
        TopLeft,
        /// <summary>
        /// 右上角
        /// </summary>
        TopRight,
        /// <summary>
        /// 左下角
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 右下角
        /// </summary>
        BottomRight
    }
    /// </summary>
    public enum ConnectionLineType
    {
        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        Bezier,
        /// <summary>
        /// 直线
        /// </summary>
        Straight,
        /// <summary>
        /// 折线
        /// </summary>
        Step,
        /// <summary>
        /// 正交线（组织结构图直角连接线）
        /// </summary>
        Orthogonal,
        /// <summary>
        /// 弧形连接线
        /// </summary>
        Arc
    }

    /// <summary>
    /// 鼠标操作模式枚举
    /// </summary>
    public enum MouseOperationMode
    {
        /// <summary>
        /// 无操作
        /// </summary>
        None,
        /// <summary>
        /// 拖拽节点模式
        /// </summary>
        DragNode,
        /// <summary>
        /// 平移画布模式
        /// </summary>
        Pan,
        /// <summary>
        /// 框选模式（v1.7新增）
        /// </summary>
        MarqueeSelect
    }

    /// <summary>
    /// 命中测试结果类型
    /// </summary>
    public enum HitTestResultType
    {
        /// <summary>
        /// 未命中任何元素
        /// </summary>
        None,
        /// <summary>
        /// 命中节点
        /// </summary>
        Node,
        /// <summary>
        /// 命中展开/折叠按钮
        /// </summary>
        ExpandButton,
        /// <summary>
        /// 命中连接线
        /// </summary>
        Connection
    }

    /// <summary>
    /// 对齐方式枚举（v1.7新增）
    /// </summary>
    public enum AlignmentType
    {
        /// <summary>
        /// 左对齐
        /// </summary>
        Left,
        /// <summary>
        /// 水平居中对齐
        /// </summary>
        CenterHorizontal,
        /// <summary>
        /// 右对齐
        /// </summary>
        Right,
        /// <summary>
        /// 顶部对齐
        /// </summary>
        Top,
        /// <summary>
        /// 垂直居中对齐
        /// </summary>
        CenterVertical,
        /// <summary>
        /// 底部对齐
        /// </summary>
        Bottom,
        /// <summary>
        /// 水平等间距分布
        /// </summary>
        DistributeHorizontal,
        /// <summary>
        /// 垂直等间距分布
        /// </summary>
        DistributeVertical,
        /// <summary>
        /// 统一宽度
        /// </summary>
        SameWidth,
        /// <summary>
        /// 统一高度
        /// </summary>
        SameHeight,
        /// <summary>
        /// 统一尺寸
        /// </summary>
        SameSize
    }

    /// <summary>
    /// 节点边框样式（v1.9.1修复：重命名避免与System.Windows.Forms.BorderStyle冲突）
    /// </summary>
    public enum NodeBorderStyle
    {
        /// <summary>
        /// 实线
        /// </summary>
        Solid,
        /// <summary>
        /// 虚线
        /// </summary>
        Dash,
        /// <summary>
        /// 点线
        /// </summary>
        Dot,
        /// <summary>
        /// 点划线
        /// </summary>
        DashDot,
        /// <summary>
        /// 双点划线
        /// </summary>
        DashDotDot,
        /// <summary>
        /// 无边框
        /// </summary>
        None
    }

    /// <summary>
    /// 图标位置枚举（v1.9新增）
    /// </summary>
    public enum IconPosition
    {
        /// <summary>
        /// 左侧（图标+文本）
        /// </summary>
        Left,
        /// <summary>
        /// 右侧（文本+图标）
        /// </summary>
        Right,
        /// <summary>
        /// 顶部（图标在上）
        /// </summary>
        Top,
        /// <summary>
        /// 无图标
        /// </summary>
        None
    }

    /// <summary>
    /// 【v2.3新增】节点方向枚举（分方向折叠）
    /// 用于支持节点在4个方向上独立折叠/展开
    /// </summary>
    public enum NodeDirection
    {
        /// <summary>
        /// 右侧（默认方向）
        /// </summary>
        Right,
        /// <summary>
        /// 左侧
        /// </summary>
        Left,
        /// <summary>
        /// 上方
        /// </summary>
        Top,
        /// <summary>
        /// 下方
        /// </summary>
        Bottom
    }
}
