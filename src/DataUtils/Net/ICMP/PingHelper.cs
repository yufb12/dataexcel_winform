
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.Text;
using System.Net.NetworkInformation;

namespace Feng.Net.Base
{ 
    public class PingHelper
    {
        public static IPAddress GetIPAddressByPin(string hostname)
        {
            Ping ping = new Ping(); 
            PingReply pingreply = ping.Send(hostname);
             if ( pingreply.Status == IPStatus.Success)
             {
                 return pingreply.Address;
             }
             return null;
        }
        public static IPAddress GetIPAddressByPin(string hostname,IPAddress ip)
        {
            Ping ping = new Ping();
            PingReply pingreply = ping.Send(hostname);
            if (pingreply.Status == IPStatus.Success)
            {
                return pingreply.Address;
            }
            return ip;
        }
        //private AutoResetEvent _autoevent = new AutoResetEvent(false);
 
    } 
}
