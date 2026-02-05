
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
namespace Feng.Net.Base
{
    internal sealed class SocketAsyncEventArgsPool
    {

        private Int32 nextTokenId = 0;

        Stack<SocketAsyncEventArgs> pool;

        internal SocketAsyncEventArgsPool(Int32 capacity)
        {
            this.pool = new Stack<SocketAsyncEventArgs>(capacity);
        }



        internal Int32 Count
        {
            get { return this.pool.Count; }
        }

        internal Int32 AssignTokenId()
        {
            Int32 tokenId = Interlocked.Increment(ref nextTokenId);
            return tokenId;
        }

        internal SocketAsyncEventArgs Pop()
        {
            lock (this.pool)
            {
                return this.pool.Pop();
            }
        }

        internal void Push(SocketAsyncEventArgs item)
        {
            lock (this.pool)
            {
                this.pool.Push(item);
            }
        }
    }
}