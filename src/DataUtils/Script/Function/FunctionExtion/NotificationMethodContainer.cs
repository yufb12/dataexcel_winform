using Feng.Forms;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;

namespace Feng.Script.FunctionContainer
{

    public class NotificationMethodContainer : CBMethodContainer
    {
        public string FullName { get; set; }
        public const string Function_Category = "Notification";
        public const string Function_Description = "Notification";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public NotificationMethodContainer()
        {

            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "NotifySuccess";
            model.Description = @"NotifySuccess()";
            model.Eg = @"NotifySuccess("")";
            model.Function = this.NotifySuccess;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "NotifyError";
            model.Description = @"NotifyError()";
            model.Eg = @"NotifyError("")";
            model.Function = this.NotifyError;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "NotifyInfo";
            model.Description = @"NotifyInfo()";
            model.Eg = @"NotifyInfo("")";
            model.Function = this.NotifyInfo;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "NotifyWarn";
            model.Description = @"NotifyWarn()";
            model.Eg = @"NotifyWarn("")";
            model.Function = this.NotifyWarn;
            MethodList.Add(model);
        }

        public virtual object NotifySuccess(params object[] args)
        {
            string title = base.GetTextValue(1, args);
            string msg = base.GetTextValue(2, args);
            NotificationTool.Instance.Success(title, msg);
            return 1;
        }

        public virtual object NotifyError(params object[] args)
        {
            string title = base.GetTextValue(1, args);
            string msg = base.GetTextValue(2, args);
            NotificationTool.Instance.Error(title, msg);
            return 1;
        }

        public virtual object NotifyInfo(params object[] args)
        {
            string title = base.GetTextValue(1, args);
            string msg = base.GetTextValue(2, args);
            NotificationTool.Instance.Info(title, msg);
            return 1;
        }

        public virtual object NotifyWarn(params object[] args)
        {
            string title = base.GetTextValue(1, args);
            string msg = base.GetTextValue(2, args);
            NotificationTool.Instance.Warn(title, msg);
            return 1;
        }
    }

}
 