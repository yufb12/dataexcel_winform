//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Drawing;

//namespace ImageLoadData
//{
//    public class CText : ICommandServer
//    {

//        #region Itext 成员
//        private string strtext = string.Empty;

//        public string text
//        {
//            get
//            {
//                return strtext;
//            }
//            set
//            {
//                strtext = value;
//            }
//        }

//        #endregion

//        #region Itext 成员
//        private ICommand _parent;
//        public ICommand parent
//        {
//            get
//            {
//                return _parent;
//            }
//            set
//            {
//                this._parent = value;
//            }
//        }

//        #endregion

//        #region ISizeF 成员

//        public int width
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public int height
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        #endregion

//        #region IColor 成员

//        public System.Drawing.Color FColor
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        #endregion

//        #region IRectF 成员

//        public System.Drawing.Rectangle rect
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        #endregion

//        #region IPointF 成员

//        public int x
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public int y
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        #endregion

//        #region IImage 成员

//        public System.Drawing.Image Image
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        #endregion
//    }

//    public interface ICommand
//    {
//        void Execute(ICommandServer content);
//    }

//    public interface ICommandServer : ISizeF, IColor, IRectF, IText, IPointF, IImage
//    {

//    }

//    public interface IPointF
//    {
//        int x { get; set; }
//        int y { get; set; }
//    }

//    public interface IText
//    {
//        ICommand parent { get; set; }
//        string text { get; set; }
//    }

//    public interface ISizeF
//    {
//        int width { get; set; }
//        int height { get; set; }
//    }

//    public interface IRectF
//    {
//        System.Drawing.Rectangle rect { get; set; }

//    }

//    public interface IColor
//    {
//        System.Drawing.Color FColor { get; set; }
//    }

//    public interface IImage
//    {
//        System.Drawing.Image Image { get; set; }
//    }

//    public class ClsText : ICommand
//    {
//        string text = string.Empty;
//        public System.Windows.Forms.Button btn = null;
//        public string sText
//        {
//            get { return this.text; }
//            set
//            {
//                CText ct = new CText();

//                ct.text = this.text;
//                ct.parent = this;
//                ICommandServer tt = ct as ICommandServer;

//                cslCommandServer.MainDataStackt.Add(tt);
//                this.text = value;

//            }
//        }

//        #region icommand 成员

//        public void Execute(ICommandServer content)
//        {
//            btn.Text = content.text;
//        }
//        #endregion
//    }

//    public class cslCommandServer
//    {
//        public static DataStackt MainDataStackt = new DataStackt();
//    }

//    public class DataStackt
//    {
//        Stack<ICommandServer> Unstack;
//        Stack<ICommandServer> Restack;

//        public DataStackt()
//        {
//            Unstack = new Stack<ICommandServer>();
//            Restack = new Stack<ICommandServer>();
//        }

//        public void Add(ICommandServer obj)
//        {
//            Unstack.Push(obj);
//            Restack.Clear();
//        }

//        public void Clear()
//        {
//            Unstack.Clear();
//            Restack.Clear();
//        }

//        public void Execute()
//        {
//            if (this.Unstack.Count < 1)
//            {
//                return;
//            }
//            ICommandServer obj = Unstack.Pop();
//            obj.parent.Execute(obj);
//            Restack.Push(obj);
//        }

//        public void Redo()
//        {
//            if (this.Restack.Count < 1)
//            {
//                return;
//            }
//            ICommandServer obj = Restack.Pop();
//            obj.parent.Execute(obj);
//        }
//    }
//}
