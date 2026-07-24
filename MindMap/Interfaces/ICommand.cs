namespace MindMap.Interfaces
{
    /// <summary>
    /// 命令接口，定义可撤销操作的契约
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 获取命令名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 执行命令
        /// </summary>
        void Execute();

        /// <summary>
        /// 撤销命令
        /// </summary>
        void Undo();
    }
}
