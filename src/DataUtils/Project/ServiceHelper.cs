namespace Feng.Utils
{
    public class ServiceHelper
    {
        public static bool IsRunning(string windowsServiceName)
        {
            using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController(windowsServiceName))
            {
                if (control.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool StartWindowsService(string windowsServiceName)
        {
            using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController(windowsServiceName))
            {
                if (control.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                {
                    control.Start();
                    return true;
                }
                else if (control.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool StopWindowsService(string windowsServiceName)
        {
            using (System.ServiceProcess.ServiceController control = new System.ServiceProcess.ServiceController(windowsServiceName))
            {
                if (control.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    control.Stop();
                    return true;
                }
                else if (control.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                {
                    return true;
                }
            }
            return false;
        }
    }

}