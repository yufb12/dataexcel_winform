using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Feng.Net.Base
{
    public static class NetInfo
    {
        public static string Mac
        {
            get
            {
                string mac = string.Empty;
                try
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        System.Net.NetworkInformation.NetworkInterface[] nis = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                        if (nis.Length > 0)
                        {
                            System.Net.NetworkInformation.NetworkInterface ni = nis[0];

                            mac = ni.GetPhysicalAddress().ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
                finally
                {

                }

                return mac;

            }
        }
        public static List<string> GetLocationIp()
        {
            List<string> list = new List<string>();
            System.Net.NetworkInformation.NetworkInterface[] nis = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (System.Net.NetworkInformation.NetworkInterface ni in nis)
            { 
            }

            return list;
        }

        /// <summary>
        /// 获取本地IP地址的备用方法
        /// </summary>
        public static string GetLocalIPAddressFallback()
        {
            try
            { 
                foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
                { 
                    if (ip.AddressFamily == AddressFamily.InterNetwork &&
                        !IPAddress.IsLoopback(ip))
                    {
                        return ip.ToString();
                    }
                } 
                return IPAddress.Loopback.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public static string GetMac()
        {
            string mac = string.Empty;
            try
            {
                System.Net.NetworkInformation.NetworkInterface[] ns = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                if (ns.Length > 0)
                {
                    mac = ns[0].GetPhysicalAddress().ToString();
                }
            }
            catch (Exception)
            { 
            }
            return mac;
        }

        public static string GetLocalIp()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }
        public static List<string> GetLocalIpList()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            List<string> list = new List<string>();
            foreach (var ip in ipEntry.AddressList)
            {
                if (ip.AddressFamily== System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    continue;
                }
                if (ip.IsIPv6LinkLocal)
                {
                    continue;
                }
                if (ip.IsIPv6Multicast)
                {
                    continue;
                }
                if (ip.IsIPv6SiteLocal)
                {
                    continue;
                }
                if (ip.IsIPv6Teredo)
                {
                    continue;
                }
                list.Add(ip.ToString());
                Console.WriteLine("IP Address: " + ip.ToString());
            }
            return list;
        }

}

    public class NetAddress
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }
}
