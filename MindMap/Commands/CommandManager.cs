using System;
using System.Collections.Generic;
using MindMap.Interfaces;

namespace MindMap.Commands
{
    /// <summary>
    /// 命令管理器，管理撤销/重做栈
    /// </summary>
    public class CommandManager
    {
        private const int DefaultMaxHistorySize = 100;
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();
        private readonly int _maxHistorySize;

        /// <summary>
        /// 命令执行事件
        /// </summary>
        public event EventHandler CommandExecuted;

        /// <summary>
        /// 撤销操作事件
        /// </summary>
        public event EventHandler UndoPerformed;

        /// <summary>
        /// 重做操作事件
        /// </summary>
        public event EventHandler RedoPerformed;

        /// <summary>
        /// 获取是否可以撤销
        /// </summary>
        public bool CanUndo
        {
            get { return _undoStack.Count > 0; }
        }

        /// <summary>
        /// 获取是否可以重做
        /// </summary>
        public bool CanRedo
        {
            get { return _redoStack.Count > 0; }
        }

        /// <summary>
        /// 获取撤销栈大小
        /// </summary>
        public int UndoCount
        {
            get { return _undoStack.Count; }
        }

        /// <summary>
        /// 获取重做栈大小
        /// </summary>
        public int RedoCount
        {
            get { return _redoStack.Count; }
        }

        /// <summary>
        /// 初始化命令管理器
        /// </summary>
        public CommandManager()
            : this(DefaultMaxHistorySize)
        {
        }

        /// <summary>
        /// 初始化命令管理器
        /// </summary>
        /// <param name="maxHistorySize">最大历史记录数</param>
        public CommandManager(int maxHistorySize)
        {
            _maxHistorySize = maxHistorySize > 0 ? maxHistorySize : DefaultMaxHistorySize;
        }

        /// <summary>
        /// 执行新命令
        /// </summary>
        /// <param name="command">要执行的命令</param>
        public void ExecuteCommand(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            command.Execute();
            PushToUndoStack(command);
            _redoStack.Clear();
            OnCommandExecuted();
        }

        /// <summary>
        /// 推入命令到撤销栈（限制栈大小）
        /// </summary>
        private void PushToUndoStack(ICommand command)
        {
            _undoStack.Push(command);
            
            // 限制历史记录大小
            while (_undoStack.Count > _maxHistorySize)
            {
                // 移除最旧的记录（需要转成列表处理）
                List<ICommand> tempList = new List<ICommand>(_undoStack);
                tempList.RemoveAt(tempList.Count - 1);
                _undoStack.Clear();
                for (int i = tempList.Count - 1; i >= 0; i--)
                {
                    _undoStack.Push(tempList[i]);
                }
            }
        }

        /// <summary>
        /// 撤销上一个操作
        /// </summary>
        public void Undo()
        {
            if (CanUndo)
            {
                ICommand command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
                OnUndoPerformed();
            }
        }

        /// <summary>
        /// 重做上一个撤销的操作
        /// </summary>
        public void Redo()
        {
            if (CanRedo)
            {
                ICommand command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                OnRedoPerformed();
            }
        }

        /// <summary>
        /// 清空所有历史记录
        /// </summary>
        public void ClearHistory()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        /// <summary>
        /// 触发命令执行事件
        /// </summary>
        protected virtual void OnCommandExecuted()
        {
            EventHandler handler = CommandExecuted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 触发撤销操作事件
        /// </summary>
        protected virtual void OnUndoPerformed()
        {
            EventHandler handler = UndoPerformed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 触发重做操作事件
        /// </summary>
        protected virtual void OnRedoPerformed()
        {
            EventHandler handler = RedoPerformed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
