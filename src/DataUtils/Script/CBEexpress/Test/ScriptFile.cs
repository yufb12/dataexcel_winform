using System;
using System.Collections.Generic;
using System.Threading;

namespace Feng.Script.CBEexpress
{
    public enum DebugCommand
    {
        Continue,
        StepInto,
        StepOver,
        StepOut,
        Pause,
        Stop
    }

    public class NetDebug
    { 
        public Token CurrentToken { get; private set; }
        public NetStatementBase CurrentStatement { get; private set; }
         
        private DebugState currentState = DebugState.Running;
        private DebugCommand pendingCommand = DebugCommand.Continue;
        private readonly ManualResetEventSlim commandEvent = new ManualResetEventSlim(true);
         
        private readonly HashSet<NetStatementBase> breakpoints = new HashSet<NetStatementBase>();
        private bool isStepping = false;
        private int stepOutLevel = 0;
         
        public event EventHandler<DebugEventArgs> DebugEvent;
        public delegate void DebugExceptionEventHandler(object sender, Exception ex);
        public event DebugExceptionEventHandler DebugExceptionEvent;

        public NetDebug()
        {
            // 初始化调试状态
            currentState = DebugState.Running;
        }
         
        public void SetCommand(DebugCommand command)
        {
            lock (this)
            {
                pendingCommand = command;

                switch (command)
                {
                    case DebugCommand.Continue:
                        isStepping = false;
                        stepOutLevel = 0;
                        currentState = DebugState.Running;
                        break; 

                    case DebugCommand.StepInto:
                        isStepping = true;
                        currentState = DebugState.Stepping;
                        break;

                    case DebugCommand.StepOver:
                        isStepping = true;
                        currentState = DebugState.Stepping;
                        // 记录当前嵌套层级以实现StepOver
                        stepOutLevel = GetCurrentNestingLevel();
                        break;

                    case DebugCommand.StepOut:
                        isStepping = true;
                        currentState = DebugState.Stepping;
                        // 记录需要跳出的层级
                        stepOutLevel = GetCurrentNestingLevel() - 1;
                        break;

                    case DebugCommand.Pause:
                        isStepping = true;
                        if (currentState == DebugState.Running)
                        {
                            currentState = DebugState.Paused;
                        }
                        break;

                    case DebugCommand.Stop:
                        currentState = DebugState.Stopped;
                        break;
                }

                // 通知等待的线程有新命令
                commandEvent.Set();
            }
        }

        private int NestingLevel = 0;
 
        private int GetCurrentNestingLevel()
        {
            // 实际实现中需要遍历语句树来计算嵌套层级
            // 简化实现，返回0作为示例
            return NestingLevel;
        }
         
        public void CheckBreakpoint(NetStatementBase statement)
        {
            if (currentState == DebugState.Stopped)
                throw new InvalidOperationException("调试器已停止");

            // 更新当前调试上下文
            CurrentStatement = statement;
            CurrentToken = statement?.BeginToken;

            // 触发调试事件
            OnDebugEvent(DebugEventType.ContextChanged);

            // 检查是否需要暂停
            bool shouldPause = false;

            lock (this)
            {
                // 首先检查是否是暂停命令
                if (pendingCommand == DebugCommand.Pause)
                {
                    shouldPause = true;
                }
                // 检查是否有断点
                else if (breakpoints.Contains(statement))
                {
                    shouldPause = true;
                }
                // 检查单步执行模式
                else if (isStepping)
                {
                    if (pendingCommand == DebugCommand.StepInto)
                    {
                        // 单步执行，每条语句都暂停
                        shouldPause = true;
                    }
                    else if (pendingCommand == DebugCommand.StepOver)
                    {
                        // StepOver: 只在当前层级暂停
                        shouldPause = GetCurrentNestingLevel() <= stepOutLevel;
                    }
                    else if (pendingCommand == DebugCommand.StepOut)
                    {
                        // StepOut: 在跳出指定层级时暂停
                        shouldPause = GetCurrentNestingLevel() <= stepOutLevel;

                        // 如果已经跳出，关闭单步模式
                        if (shouldPause)
                        {
                            isStepping = false;
                            pendingCommand = DebugCommand.Continue;
                        }
                    }
                }
            }

            if (shouldPause)
            {
                // 暂停执行，等待调试命令
                WaitForCommand();
            }
        }
         
        private void WaitForCommand()
        {
            // 更新状态为暂停
            currentState = DebugState.Paused;
            OnDebugEvent(DebugEventType.BreakpointHit);

            // 重置事件，准备等待命令
            commandEvent.Reset();

            while (true)
            {
                // 等待命令
                commandEvent.Wait();

                DebugCommand command;
                DebugState state;

                lock (this)
                {
                    command = pendingCommand;
                    state = currentState;
                }

                // 处理命令
                switch (command)
                {
                    case DebugCommand.Continue:
                        currentState = DebugState.Running;
                        OnDebugEvent(DebugEventType.Resumed);
                        return;

                    case DebugCommand.StepInto:
                    case DebugCommand.StepOver:
                    case DebugCommand.StepOut:
                        currentState = DebugState.Stepping;
                        OnDebugEvent(DebugEventType.Resumed);
                        return;

                    case DebugCommand.Pause:
                        // 已经暂停，继续等待命令
                        commandEvent.Reset();
                        break;

                    case DebugCommand.Stop:
                        throw new OperationCanceledException("调试被用户停止");
                }
            }
        }
         
        public void AddBreakpoint(NetStatementBase statement)
        {
            lock (breakpoints)
            {
                breakpoints.Add(statement);
            }
        }

        public void RemoveBreakpoint(NetStatementBase statement)
        {
            lock (breakpoints)
            {
                breakpoints.Remove(statement);
            }
        }

        public bool HasBreakpoint(NetStatementBase statement)
        {
            lock (breakpoints)
            {
                return breakpoints.Contains(statement);
            }
        }

        internal void OnDebugExceptionEvent(Exception ex)
        {
            DebugExceptionEvent?.Invoke(this, ex);
        }
        private void OnDebugEvent(DebugEventType eventType)
        {
            DebugEvent?.Invoke(this, new DebugEventArgs
            {
                EventType = eventType,
                CurrentStatement = CurrentStatement,
                CurrentToken = CurrentToken,
                DebugState = currentState,
                NestingLevel= NestingLevel
            });
        }

        internal void OnDebugEvent(DebugEventType eventType, NetStatementBase statement,object value)
        {
            DebugEvent?.Invoke(this, new DebugEventArgs
            {
                EventType = eventType,
                CurrentStatement = statement,
                CurrentToken = CurrentToken,
                DebugState = currentState,
                NestingLevel = NestingLevel,
                Value=value 
            });
        }

        internal void OnDebugEvent(DebugEventType eventType, NetStatementBase statement, string varname,object value)
        {
            DebugEvent?.Invoke(this, new DebugEventArgs
            {
                EventType = eventType,
                CurrentStatement = statement,
                CurrentToken = CurrentToken,
                DebugState = currentState,
                NestingLevel = NestingLevel,
                VarName = varname,
                Value =value 
            });
        }

        public void Dispose()
        {
            commandEvent.Dispose();
        }

        public virtual void AddLevel()
        {
            NestingLevel++;
        }
        public virtual void SubLevel()
        {
            NestingLevel--;
        }
    }
     
    public enum DebugState
    {
        Running,
        Paused,
        Stepping,
        Stopped
    }
     
    public enum DebugEventType
    {
        LogValue,
        ContextChanged,
        BreakpointHit,
        Resumed
    }
     
    public class DebugEventArgs : EventArgs
    {
        public virtual DebugEventType EventType { get; set; }
        public virtual NetStatementBase CurrentStatement { get; set; }
        public virtual Token CurrentToken { get; set; }
        public virtual DebugState DebugState { get; set; }
        public virtual int NestingLevel { get; internal set; }
        public virtual object Value { get; internal set; }
        public virtual string VarName { get; internal set; }
    }
     
}
