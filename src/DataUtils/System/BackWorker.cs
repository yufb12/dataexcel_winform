using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Feng.Data;
using Feng.Forms.Interface;
using Feng.Collections;

namespace Feng.App
{
    public interface IBackWorkerMethod
    {
        void Do();
        bool Disposing();
    }
    public class BackWorker  
    {
        public static void RunWork()
        {
            BackWorker backWorker = new BackWorker();
            backWorker.Start();
        }

        public BackWorker()
        {
            methods = new ListEx<IBackWorkerMethod>();
        }
        private Feng.Collections.ListEx<IBackWorkerMethod> methods = null;
        private Feng.Collections.ListEx<IBackWorkerMethod> Methods { get { return methods; } }
 
        public void Add(IBackWorkerMethod method)
        {
            lock (this)
            {
                Methods.Add(method);
            }
        }
        private bool running = false;
        public void Start()
        {
            if (running)
                return;
            running = true;
            System.Threading.Thread thread = new System.Threading.Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    lock (this)
                    {
                        for (int i = Methods.Count - 1; i >= 0; i--)
                        {
                            IBackWorkerMethod workerMethod = Methods[i];
                            if (workerMethod == null)
                                continue;
                            if (workerMethod.Disposing())
                                continue;
                            workerMethod.Do();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Feng.Utils.TraceHelper.WriteTrace("App", "BackWorker", "Run", ex);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    } 
}
