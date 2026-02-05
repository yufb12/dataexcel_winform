using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Feng.Services
{
    public class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }
        protected override void OnStop()
        {
            base.OnStop();
        }
        protected override void OnContinue()
        {
            base.OnContinue();
        }
        protected override void OnPause()
        {
            base.OnPause();
        }
        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
        }
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }
        protected override object GetService(Type service)
        {
            return base.GetService(service);
        }
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }
        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }
        public override object InitializeLifetimeService()
        {
            return base.InitializeLifetimeService();
        }

        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.ServiceName = "FengNetServer";

        } 
    }
}
