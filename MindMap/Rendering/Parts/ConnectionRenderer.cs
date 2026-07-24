using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using MindMap.Core;

namespace MindMap.Rendering
{
    /// <summary>
    /// 连接线渲染器（SRP：单一职责原则）
    /// 职责：专门负责绘制各种类型的连接线和箭头
    /// 
    /// 【v1.8.1修复】箭头方向：
    /// - start = 父节点连接点（带间距，向外）
    /// - end = 子节点连接点（带间距，向内）
    /// - 箭头尖端指向子节点，在节点边界外
    /// 
    /// 【v2.1.3新增】动态连接点计算：
    /// - 根据子节点位置自动选择父节点的连接边缘（左/右/上/下）
    /// - 子节点在父节点右侧 → 父节点右边缘
    /// - 子节点在父节点左侧 → 父节点左边缘
    /// - 子节点在父节点下方 → 父节点下边缘
    /// - 子节点在父节点上方 → 父节点上边缘
    /// </summary>
    internal static class ConnectionRenderer
    {
        #region 常量定义
        private const float CONNECTION_GAP = 8f;      // 连接线与节点间距（呼吸感）
        private const float ARROW_LENGTH = 7f;        // 箭头长度
        private const float ARROW_HALF_WIDTH = 4f;    // 箭头半宽
        #endregion

        #region 连接线绘制（对外接口）
        /// <summary>
        /// 绘制连接线 + 实心箭头（支持5种类型）
        /// 【箭头方向修复】：从父节点指向子节点，箭头在子节点一侧
        /// </summary>
        public static void DrawConnectionWithArrow(Graphics graphics, PointF start, PointF end, 
            ConnectionLineType lineType, Color lineColor, float lineWidth)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");

            using (Pen pen = new Pen(lineColor, lineWidth))
            {
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                switch (lineType)
                {
                    case ConnectionLineType.Bezier:
                        DrawBezierConnection(graphics, start, end, pen);
                        break;
                    case ConnectionLineType.Straight:
                        DrawStraightConnection(graphics, start, end, pen);
                        break;
                    case ConnectionLineType.Step:
                        DrawStepConnection(graphics, start, end, pen);
                        break;
                    case ConnectionLineType.Orthogonal:
                        DrawOrthogonalConnection(graphics, start, end, pen);
                        break;
                    case ConnectionLineType.Arc:
                        DrawArcConnection(graphics, start, end, pen);
                        break;
                }

                // ========== 关键修复：绘制实心箭头 ==========
                // start = 父节点，end = 子节点
                // 箭头方向：从 start 指向 end，箭头尖端在 end 点
                DrawSolidArrow(graphics, end, start, lineColor);
            }
        }

        /// <summary>
        /// 绘制连接线（不含箭头）
        /// </summary>
        public static void DrawConnection(Graphics graphics, PointF start, PointF end, 
            ConnectionLineType lineType, Color lineColor, float lineWidth)
        {
            DrawConnectionWithArrow(graphics, start, end, lineType, lineColor, lineWidth);
        }
        #endregion

        #region 各种连接线类型实现
        /// <summary>
        /// 绘制自然流畅的贝塞尔曲线连接线
        /// </summary>
        private static void DrawBezierConnection(Graphics graphics, PointF start, PointF end, Pen pen)
        {
            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            
            if (distance < 1f) return;

            // 控制点距离与间距成正比，最大80px
            float controlDistance = Math.Min(distance * 0.45f, 80f);
            float sign = dx >= 0 ? 1f : -1f;

            // 水平方向控制点（标准思维导图风格）
            PointF c1 = new PointF(start.X + controlDistance * sign, start.Y);
            PointF c2 = new PointF(end.X - controlDistance * sign, end.Y);

            graphics.DrawBezier(pen, start, c1, c2, end);
        }

        /// <summary>
        /// 绘制直线连接线
        /// </summary>
        private static void DrawStraightConnection(Graphics graphics, PointF start, PointF end, Pen pen)
        {
            graphics.DrawLine(pen, start, end);
        }

        /// <summary>
        /// 绘制折线连接线
        /// </summary>
        private static void DrawStepConnection(Graphics graphics, PointF start, PointF end, Pen pen)
        {
            float midX = (start.X + end.X) / 2f;
            PointF[] points = new PointF[]
            {
                start,
                new PointF(midX, start.Y),
                new PointF(midX, end.Y),
                end
            };
            graphics.DrawLines(pen, points);
        }

        /// <summary>
        /// 绘制正交连接线（组织结构图风格）
        /// </summary>
        private static void DrawOrthogonalConnection(Graphics graphics, PointF start, PointF end, Pen pen)
        {
            PointF[] points = new PointF[]
            {
                start,
                new PointF(start.X, end.Y),
                end
            };
            graphics.DrawLines(pen, points);
        }

        /// <summary>
        /// 绘制弧形连接线（用贝塞尔模拟优雅弧线）
        /// </summary>
        private static void DrawArcConnection(Graphics graphics, PointF start, PointF end, Pen pen)
        {
            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            float controlDistance = Math.Min(distance * 0.4f, 60f);

            PointF c1 = new PointF(start.X + controlDistance, start.Y);
            PointF c2 = new PointF(end.X - controlDistance, end.Y);

            graphics.DrawBezier(pen, start, c1, c2, end);
        }
        #endregion

        #region 实心箭头绘制（核心修复）
        /// <summary>
        /// 绘制实心三角形箭头
        /// 【方向逻辑】：从 source 指向 target，箭头尖端在 target 点
        /// </summary>
        private static void DrawSolidArrow(Graphics graphics, PointF target, PointF source, Color color)
        {
            float dx = target.X - source.X;
            float dy = target.Y - source.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);

            if (length < 0.1f) return;

            // 单位方向向量：从 source 指向 target
            float dirX = dx / length;
            float dirY = dy / length;

            // 垂直向量（顺时针90度）- 用于箭头两翼
            float perpX = -dirY;
            float perpY = dirX;

            // 三角形三个顶点
            PointF tip = target;  // 箭头尖端 = 目标点
            PointF wing1 = new PointF(
                target.X - ARROW_LENGTH * dirX + ARROW_HALF_WIDTH * perpX,
                target.Y - ARROW_LENGTH * dirY + ARROW_HALF_WIDTH * perpY
            );
            PointF wing2 = new PointF(
                target.X - ARROW_LENGTH * dirX - ARROW_HALF_WIDTH * perpX,
                target.Y - ARROW_LENGTH * dirY - ARROW_HALF_WIDTH * perpY
            );

            // 填充实心三角形
            using (SolidBrush brush = new SolidBrush(color))
            {
                PointF[] arrowPoints = new PointF[] { tip, wing1, wing2 };
                graphics.FillPolygon(brush, arrowPoints);
            }
        }
        #endregion

        #region 连接点计算
        /// <summary>
        /// 【v2.1.3新增】动态计算父节点连接点（XMind风格）
        /// 根据子节点相对于父节点的位置，自动选择最佳连接边缘
        /// - 子节点在父节点右侧 → 父节点右边缘
        /// - 子节点在父节点左侧 → 父节点左边缘
        /// - 子节点在父节点下方 → 父节点下边缘
        /// - 子节点在父节点上方 → 父节点上边缘
        /// 
        /// 【向后兼容】：如果用户手动设置了连接点（非Auto），则使用用户设置
        /// </summary>
        public static PointF CalculateDynamicConnectionPoint(
            RectangleF parentBounds, RectangleF childBounds, 
            ConnectionPoint parentConnectionPoint, bool isStart)
        {
            // 如果用户手动设置了连接点（非Auto），使用用户设置
            if (parentConnectionPoint != ConnectionPoint.Auto)
            {
                return CalculateConnectionPoint(parentBounds, parentConnectionPoint, isStart);
            }

            // 计算子节点相对于父节点的位置
            float parentCenterX = parentBounds.X + parentBounds.Width / 2f;
            float parentCenterY = parentBounds.Y + parentBounds.Height / 2f;
            float childCenterX = childBounds.X + childBounds.Width / 2f;
            float childCenterY = childBounds.Y + childBounds.Height / 2f;

            float dx = childCenterX - parentCenterX;
            float dy = childCenterY - parentCenterY;

            // 根据相对位置自动选择连接点
            ConnectionPoint autoPoint;
            
            // 计算角度（0度=右，90度=下，180度=左，270度=上）
            float angle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
            if (angle < 0) angle += 360;

            // 根据角度选择连接边缘
            if (angle >= 315 || angle < 45)
                autoPoint = ConnectionPoint.Right;      // 右侧
            else if (angle >= 45 && angle < 135)
                autoPoint = ConnectionPoint.Bottom;     // 下方
            else if (angle >= 135 && angle < 225)
                autoPoint = ConnectionPoint.Left;       // 左侧
            else
                autoPoint = ConnectionPoint.Top;        // 上方

            return CalculateConnectionPoint(parentBounds, autoPoint, isStart);
        }

        /// <summary>
        /// 计算节点连接点（带呼吸间距）
        /// 【关键修复】：无论起点终点，都加呼吸间距，确保箭头在节点外可见
        /// 
        /// isStart=true:  作为父节点（连接线起点）→ 边界向外延伸 CONNECTION_GAP
        /// isStart=false: 作为子节点（连接线终点）→ 边界向内缩进 CONNECTION_GAP
        ///                （确保箭头在节点边界外，不会被节点遮挡）
        /// </summary>
        public static PointF CalculateConnectionPoint(RectangleF bounds, ConnectionPoint connectionPoint, bool isStart)
        {
            float centerX = bounds.X + bounds.Width / 2f;
            float centerY = bounds.Y + bounds.Height / 2f;
            float gap = CONNECTION_GAP;

            switch (connectionPoint)
            {
                case ConnectionPoint.Left:
                    // 左连接：起点向左，终点向右（缩进节点内，箭头在边界）
                    return new PointF(bounds.X + (isStart ? -gap : gap), centerY);
                case ConnectionPoint.Right:
                    // 右连接：起点向右，终点向左（缩进节点内，箭头在边界）
                    return new PointF(bounds.Right + (isStart ? gap : -gap), centerY);
                case ConnectionPoint.Top:
                    // 上连接：起点向上，终点向下（缩进节点内，箭头在边界）
                    return new PointF(centerX, bounds.Y + (isStart ? -gap : gap));
                case ConnectionPoint.Bottom:
                    // 下连接：起点向下，终点向上（缩进节点内，箭头在边界）
                    return new PointF(centerX, bounds.Bottom + (isStart ? gap : -gap));
                case ConnectionPoint.Center:
                    return new PointF(centerX, centerY);
                default: // Auto
                    // 默认向右连接（标准思维导图）
                    return new PointF(bounds.Right + (isStart ? gap : -gap), centerY);
            }
        }

        #region 连线命中测试（v2.1.7.2新增）
        /// <summary>
        /// 检测点是否在连线附近（用于点击选中连线）
        /// </summary>
        public static bool HitTestConnection(PointF start, PointF end, PointF point, float tolerance = 5f)
        {
            // 计算点到线段的距离
            float distance = DistancePointToLineSegment(point, start, end);
            return distance <= tolerance;
        }

        /// <summary>
        /// 计算点到线段的最短距离
        /// </summary>
        private static float DistancePointToLineSegment(PointF point, PointF lineStart, PointF lineEnd)
        {
            float dx = lineEnd.X - lineStart.X;
            float dy = lineEnd.Y - lineStart.Y;

            // 线段长度为0的情况
            if (dx == 0 && dy == 0)
            {
                dx = point.X - lineStart.X;
                dy = point.Y - lineStart.Y;
                return (float)Math.Sqrt(dx * dx + dy * dy);
            }

            // 计算投影参数
            float t = ((point.X - lineStart.X) * dx + (point.Y - lineStart.Y) * dy) / (dx * dx + dy * dy);

            // 投影在线段起点之前
            if (t < 0)
            {
                dx = point.X - lineStart.X;
                dy = point.Y - lineStart.Y;
            }
            // 投影在线段终点之后
            else if (t > 1)
            {
                dx = point.X - lineEnd.X;
                dy = point.Y - lineEnd.Y;
            }
            // 投影在线段上
            else
            {
                PointF projection = new PointF(lineStart.X + t * dx, lineStart.Y + t * dy);
                dx = point.X - projection.X;
                dy = point.Y - projection.Y;
            }

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        #endregion
        #endregion
    }
}
