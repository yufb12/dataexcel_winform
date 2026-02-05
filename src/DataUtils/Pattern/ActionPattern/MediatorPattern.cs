using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Mediator Pattern 
    /// 中介者模式 调停者模式
    /// 示例 中介，WTO
    /// 解释：将属性的变更通知其需要更新者
    /// </summary>
    public class MediatorPattern : Pattern
    {
        private MediatorPattern()
        {

        }
         
        public override void Test(string text)
        {
            RoomMediator mediator = new RoomMediator();
            RoomMediator mediator2 = new RoomMediator();
            User user1 = new User(mediator);
            User user12 = new User(mediator);
            User user13 = new User(mediator);
            User user14 = new User(mediator2);
            User user15 = new User(mediator2);
            User user16 = new User(mediator2);
            User user17 = new User(mediator2);
            user1.SendMsg("User1");
            user12.SendMsg("user12");
            user13.SendMsg("user13");
            user14.SendMsg("user14");
            user15.SendMsg("user15");
            user16.SendMsg("user16");
            user17.SendMsg("user17");


            user12.SendMsgToUser("User1", "Hello");
            user1.SendMsgToUser("user12", "Hello");
            user12.SendMsgToUser("user13", "Hello");
            user13.SendMsgToUser("user14", "Hello");
            user13.SendMsgToUser("user15", "Hello");
            user13.SendMsgToUser("user16", "Hello");
            user13.SendMsgToUser("user17", "Hello"); 
        }

    }

    public class User
    {
        public User(RoomMediator mediator)
        {
            Mediator = mediator;
        }
        public RoomMediator Mediator { get; set; }
        public string Name { get; set; }
        public void SendMsg(string msg)
        {

        }
        public void SendMsgToUser(string user, string msg)
        {

        }
    }

    public class RoomMediator
    { 
        public void SendMsg()
        {

        }
    }
}
