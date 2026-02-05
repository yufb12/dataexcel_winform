using Feng.Net.Base;
using Feng.Net.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Net.Tcp
{

    public abstract class AsyncResultBase : IAsyncResult
    {
        internal abstract object StartPostingAsyncOp(object state);
        internal abstract void EndPostingAsyncOp();


        #region IAsyncResult 成员

        public object AsyncState
        {
            get;
            set;
        }

        public abstract System.Threading.WaitHandle AsyncWaitHandle
        {
            get;
        }
        public abstract bool CompletedSynchronously
        {
            get;
        }

        public abstract bool IsCompleted
        {
            get;
        }
        #endregion
    }


    public class AsyncResultPost : AsyncResultBase
    {
        public AsyncResultPost(AsyncCallback callback, IndexObject indexobject, NetPacket packet)
        {
            _callback = callback;
            _indexobject = indexobject;
            _packet = packet;
        }

        private object _state = null;
        public object State
        {
            get { return _state; }
            set { _state = value; }
        }

        public override bool CompletedSynchronously
        {
            get { return (IndexObject.IsReturn || _value != null); }
        }
 
        public override bool IsCompleted
        {
            get { return (IndexObject.IsReturn || _value != null); }
        }

        public override System.Threading.WaitHandle AsyncWaitHandle
        {
            get { return this.IndexObject.AutoResetEvent; }
        }

        internal override object StartPostingAsyncOp(object state)
        {
            return null;
        }
        internal override void EndPostingAsyncOp()
        {

        }

        private AsyncCallback _callback = null;
        public AsyncCallback CallBack
        {
            get { return _callback; }
        }

        private IndexObject _indexobject = null;
        public IndexObject IndexObject
        {
            get { return _indexobject; }
        }

        private NetPacket _packet = null;
        public NetPacket Packet { get { return _packet; } }

        private object _value = null;
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

    }

    public class AsyncResultSqlQuery : AsyncResultPost
    {

        public AsyncResultSqlQuery(AsyncCallback callback, IndexObject indexobject, NetPacket packet)
            : base(callback, indexobject, packet)
        {
        }

    }
}
