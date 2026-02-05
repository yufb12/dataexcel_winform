using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Observer Pattern 
    /// 观察者
    /// 解释：将属性的变更通知其需要更新者
    /// </summary>
    public class ObserverPattern : Pattern
    {
        private ObserverPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            Things th = new Things();
            HeightRecv recvheight = new HeightRecv();
            WeightRecv recvweight = new WeightRecv();
            th.list.Add(recvheight);
            th.list.Add(recvweight);
            th.Update();
        }

    }

    public interface IRecvObject
    {
        bool Update();
    }
    public abstract class RecvObject : IRecvObject
    {

        public abstract bool Update();
    }
    public class Things : IRecvObject
    {
        public object Property { get; set; }
        public List<IRecvObject> list = new List<IRecvObject>();


        public bool Update()
        {
            foreach (IRecvObject recv in list)
            {
                try
                {
                    recv.Update();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Update Exception:" + ex.Message);
                }
            }
            return true;
        }
    }
    public class HeightRecv : RecvObject
    {

        public override bool Update()
        {
            Console.WriteLine("Recv Height");
            return true;
        }
    }
    public class WeightRecv : RecvObject
    {

        public override bool Update()
        {
            Console.WriteLine("Recv Weight");
            return true;
        }
    }
}
