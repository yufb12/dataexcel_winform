using Feng.Utils;
using System;
using System.ComponentModel;

namespace Feng.Forms.Events
{
    public class EventArgsCache 
    {
        private static CacheStack<BaseEventArgs> pool = null;
        public static CacheStack<BaseEventArgs> Pool
        {
            get
            {
                if (pool == null)
                {
                    pool = new CacheStack<BaseEventArgs>();
                }
                return pool;
            } 
        }

    }
    public class BaseEventArgs : EventArgs
    {
        public virtual object Tag { get; set; }
    }
    public class HandlerEventArgs : BaseEventArgs
    {
        public bool Handler { get; set; }
    }
    public class DropDownFormFirstShowEventArgs : HandlerEventArgs
    {
    }
    public class BaseCanceelEventArgs : CancelEventArgs
    {

    }
    public class BeforeValueChangedArgs : BaseCanceelEventArgs
    {
        public object Value { get; set; }
    }

    public class DropDownButtonClickEventArgs : CancelEventArgs
    {

    }
    public class MoreButtonClickEventArgs : CancelEventArgs
    {

    }
    public class DropDownBoxTextChangedEventArgs : CancelEventArgs
    {
        public string Text { get; set; }
    }


}

