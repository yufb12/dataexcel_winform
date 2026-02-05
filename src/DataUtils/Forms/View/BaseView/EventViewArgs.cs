using Feng.Data;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{ 
    public class ViewMessage
    {

        static ViewMessage()
        {
            ViewMessageCache = new Collections.StackList<ViewMessage>();
          
        }
        private static Feng.Collections.StackList<ViewMessage> StackList = null;
        private static object lckobj = null;
        public static void Post(ViewMessage viewMessage)
        {
            if (StackList ==null)
            {
                StackList = new Collections.StackList<ViewMessage>();
                lckobj = new object();
            }
            lock (lckobj)
            {
                StackList.Push(viewMessage);
                Start();
            }
        }
        private static System.Threading.Thread Thread = null;
        public static void Start()
        {
            if (Thread == null)
            {
                Thread = new System.Threading.Thread(DoMessage);
                Thread.IsBackground = true;
                Thread.Start();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static void DoMessage()
        {
#warning 需要测试
            while (true)
            {
                try
                {
                    ViewMessage viewMessage = null;
                    lock (lckobj)
                    {
                        viewMessage = StackList.Pop();
                        if (viewMessage == null)
                        {
                            Thread = null;
                            return;
                        }
                    }
       
                    viewMessage.ViewRecv.RecvMsg(viewMessage.ViewSend, viewMessage.Sender, viewMessage);
                }
                catch (Exception)
                {
                     Feng.Utils.TraceHelper.WriteTrace("BaseView", "ViewMessage", "", true, "DoMessage");
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        private static Feng.Collections.StackList<ViewMessage> ViewMessageCache = null;

        private ViewMessage()
        {

        }

        public static ViewMessage GetViewMessage(BaseView view)
        {
            lock (ViewMessageCache)
            {
                ViewMessage model = ViewMessageCache.Pop();
                if (model == null)
                {
                    model = new ViewMessage();
                }
                ViewMessageCache.Push(model); 
                return model;
            }
        }
        private BaseView viewsend = null;
        private BaseView viewrecv = null;
        public void Recycle()
        {
            ViewMessageCache.Push(this);
        }
        public BaseView ViewSend { get { return viewsend; } }
        public BaseView ViewRecv { get { return viewrecv; } }
        public int Type { get; set; }
        public int MessageID { get; set; }
        public int MessageMainID { get; set; }
        public object Sender { get; }
        public object Value { get; set; }
    }
    public class EventViewArgs
    {
        private EventViewArgs(Control control)
        {
            this.Control = control;
        }
        public static EventViewArgs GetEventViewArgs(Control control)
        {
            EventViewArgs eventView = new EventViewArgs(control);
            return eventView;
        }
        public static EventViewArgs GetEventViewArgs(Control control,MouseEventArgs e)
        {
            Views.EventViewArgs ve = new Views.EventViewArgs(control);
            ve.AltKeyPress = (System.Windows.Forms.Control.ModifierKeys == Keys.Alt);
            ve.ContrlKeyPress = (System.Windows.Forms.Control.ModifierKeys == Keys.Control);
            ve.ControlPoint = e.Location;
            ve.ScreenPoint = System.Windows.Forms.Control.MousePosition;
            ve.ShiftKeyPress = (System.Windows.Forms.Control.ModifierKeys == Keys.Shift);
            ve.ViewPoint = e.Location;
            ve.Control = control;
            ve.X = e.Location.X;
            ve.Y = e.Location.Y;
            return ve;
        }
        public static EventViewArgs GetEventViewArgs(Control control, EventArgs e)
        {
            Views.EventViewArgs ve = new Views.EventViewArgs(control);
            ve.AltKeyPress = (System.Windows.Forms.Control.ModifierKeys == Keys.Alt);
            ve.ContrlKeyPress = (System.Windows.Forms.Control.ModifierKeys == Keys.Control); 
            ve.ScreenPoint = System.Windows.Forms.Control.MousePosition;
            ve.ShiftKeyPress = (System.Windows.Forms.Control.ModifierKeys == Keys.Shift); 
            return ve;
        }
 
        private static Feng.Collections.StackList<EventViewArgs> stacklist = null;
        public static Feng.Collections.StackList<EventViewArgs> StackList 
        { 
            get 
            {
                if (stacklist == null)
                {
                    stacklist = new Collections.StackList<EventViewArgs>();
                }
                return stacklist;
            }
        }
        public Control Control { get; set; }
        public bool ValueChanged { get; set; }
        public bool TextChanged { get; set; }
        public bool Invalate { get; set; }
        public Rectangle Rectangle { get; set; }
        public Feng.Forms.Views.BaseView BaseView { get; set; }
        public Feng.Forms.Views.BaseView ParentView { get; set; }
        public Point ScreenPoint { get; set; }
        public Point ControlPoint { get; set; }
        public Point ViewPoint { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool ShiftKeyPress { get; set; }
        public bool ContrlKeyPress { get; set; }
        public bool AltKeyPress { get; set; }
        public void Clear()
        {
            this.Invalate = false;
            this.Rectangle = Rectangle.Empty;
            this.TextChanged = false;
            this.ValueChanged = false;
            this.ScreenPoint = Point.Empty;
            this.ControlPoint = Point.Empty;
            this.ViewPoint = Point.Empty;
            this.ShiftKeyPress = false;
            this.ContrlKeyPress = false;
            this.AltKeyPress = false;
        }
    }
}

