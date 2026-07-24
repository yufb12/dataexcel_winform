using MindMap.Core;

namespace MindMap.Interfaces
{
    /// <summary>
    /// 布局引擎接口，定义节点布局算法的契约
    /// </summary>
    public interface ILayoutEngine
    {
        /// <summary>
        /// 对文档执行布局
        /// </summary>
        /// <param name="document">要布局的文档</param>
        void Layout(MindMapDocument document);
    }
}
