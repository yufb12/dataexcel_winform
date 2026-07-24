using MindMap.Core;
using MindMap.View;

namespace MindMap.Interfaces
{
    /// <summary>
    /// 文件服务接口，定义文档持久化的契约
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// 保存文档到文件
        /// </summary>
        /// <param name="document">要保存的文档</param>
        /// <param name="path">文件路径</param>
        void SaveDocument(MindMapDocument document, string path);

        /// <summary>
        /// 从文件加载文档
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>加载的文档</returns>
        MindMapDocument LoadDocument(string path);

        /// <summary>
        /// 将视图导出为图片
        /// </summary>
        /// <param name="view">思维导图视图</param>
        /// <param name="path">保存路径</param>
        void ExportToImage(MindMapView view, string path);
    }
}
