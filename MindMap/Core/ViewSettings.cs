using System;
using System.Drawing;

namespace MindMap.Core
{
    /// <summary>
    /// 视图设置类，封装视图变换参数
    /// </summary>
    [Serializable]
    public class ViewSettings
    {
        private float _zoom;
        private PointF _offset;
        private const float MinZoom = 0.1f;
        private const float MaxZoom = 3.0f;
        private const float DefaultZoom = 1.0f;

        /// <summary>
        /// 获取或设置缩放比例
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < MinZoom)
                    _zoom = MinZoom;
                if (_zoom > MaxZoom)
                    _zoom = MaxZoom;
            }
        }

        /// <summary>
        /// 获取或设置视图偏移（画布平移）
        /// </summary>
        public PointF Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        /// <summary>
        /// 获取最小缩放比例
        /// </summary>
        public float MinimumZoom
        {
            get { return MinZoom; }
        }

        /// <summary>
        /// 获取最大缩放比例
        /// </summary>
        public float MaximumZoom
        {
            get { return MaxZoom; }
        }

        /// <summary>
        /// 初始化默认视图设置
        /// </summary>
        public ViewSettings()
        {
            _zoom = DefaultZoom;
            _offset = new PointF(0, 0);
        }

        /// <summary>
        /// 放大视图
        /// </summary>
        public void ZoomIn()
        {
            Zoom *= 1.1f;
        }

        /// <summary>
        /// 缩小视图
        /// </summary>
        public void ZoomOut()
        {
            Zoom /= 1.1f;
        }

        /// <summary>
        /// 重置视图到默认状态
        /// </summary>
        public void Reset()
        {
            _zoom = DefaultZoom;
            _offset = new PointF(0, 0);
        }

        /// <summary>
        /// 将屏幕坐标转换为文档坐标
        /// </summary>
        /// <param name="screenPoint">屏幕坐标</param>
        /// <returns>文档坐标</returns>
        public PointF ScreenToDocument(Point screenPoint)
        {
            return new PointF(
                (screenPoint.X / _zoom) - _offset.X,
                (screenPoint.Y / _zoom) - _offset.Y);
        }

        /// <summary>
        /// 将文档坐标转换为屏幕坐标
        /// </summary>
        /// <param name="documentPoint">文档坐标</param>
        /// <returns>屏幕坐标</returns>
        public PointF DocumentToScreen(PointF documentPoint)
        {
            return new PointF(
                (documentPoint.X + _offset.X) * _zoom,
                (documentPoint.Y + _offset.Y) * _zoom);
        }

        /// <summary>
        /// 将文档矩形转换为屏幕矩形
        /// </summary>
        /// <param name="documentRect">文档矩形</param>
        /// <returns>屏幕矩形</returns>
        public RectangleF DocumentToScreen(RectangleF documentRect)
        {
            return new RectangleF(
                (documentRect.X + _offset.X) * _zoom,
                (documentRect.Y + _offset.Y) * _zoom,
                documentRect.Width * _zoom,
                documentRect.Height * _zoom);
        }

        /// <summary>
        /// 将屏幕矩形转换为文档矩形（v2.1.7.2新增，用于框选）
        /// </summary>
        /// <param name="screenRect">屏幕矩形</param>
        /// <returns>文档矩形</returns>
        public RectangleF ScreenRectToDocument(RectangleF screenRect)
        {
            return new RectangleF(
                (screenRect.X / _zoom) - _offset.X,
                (screenRect.Y / _zoom) - _offset.Y,
                screenRect.Width / _zoom,
                screenRect.Height / _zoom);
        }

        /// <summary>
        /// 应用视图变换到Graphics对象
        /// </summary>
        /// <param name="graphics">Graphics对象</param>
        public void ApplyTransform(Graphics graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            graphics.ScaleTransform(_zoom, _zoom);
            graphics.TranslateTransform(_offset.X, _offset.Y);
        }
    }
}
