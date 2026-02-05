using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Feng.App
{
    public static class Systeminfo
    { 
        public static string GetFirstEthernetMacAddress()
        {
            try
            {

                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                    {
                        // 获取并返回MAC地址  
                        // 注意：MAC地址可能为空或未分配  
                        if (nic.GetPhysicalAddress().GetAddressBytes().Length > 0)
                        {
                            return BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes()).Replace("-", ":").ToUpper();
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
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
                            mac = ni.Id;
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

        public static string MachineName
        {
            get {
                try
                {
                    return System.Environment.MachineName;
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static string ApplicationName { get; set; }

        public static Microsoft.Win32.RegistryKey ApplicationRegistryKey
        {
            get
            {
                try
                {
                    return System.Windows.Forms.Application.CommonAppDataRegistry;
                }
                catch (Exception)
                {
                }
                return null;
            }
        }

        public static string Key = @"75ED9A6x686c,5-0x4818, 0x88, 0x52, 0x8a, 0xa6, 0x79, 0x66, 0686C-4818-8852-8Ax686c,A679664C29 0x75ed9a65, 0 x4c, 0x29);";



        public static DateTime GetSystemBootTime()
        {
            ulong tickCount = UnsafeNativeMethods.GetTickCount64();
            TimeSpan upTime = TimeSpan.FromMilliseconds(tickCount);
            return DateTime.Now - upTime;
        }

        /// <summary>
        /// 获取所有本地IPv4（非环回）对应的MAC地址
        /// </summary>
        public static List<string> GetAllIpMacMappings()
        {
            List<string> list = new List<string>();
            // 获取所有网络接口
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface ni in interfaces)
            {
                // 只处理运行中的接口
                if (ni.OperationalStatus != OperationalStatus.Up)
                    continue;

                // 获取接口的IP属性
                IPInterfaceProperties ipProps = ni.GetIPProperties();

                // 遍历接口绑定的单播IP地址（排除环回和IPv6）
                foreach (UnicastIPAddressInformation ipInfo in ipProps.UnicastAddresses)
                {
                    IPAddress ip = ipInfo.Address;

                    // 过滤：只保留IPv4且非环回地址
                    if (ip.AddressFamily != AddressFamily.InterNetwork || IPAddress.IsLoopback(ip))
                        continue;

                    // 获取MAC地址并格式化
                    PhysicalAddress mac = ni.GetPhysicalAddress();
                    string macAddress = FormatMacAddress(mac);
                    list.Add(macAddress);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据指定IPv4地址获取对应的MAC地址
        /// </summary>
        /// <param name="targetIp">目标IPv4地址（如"192.168.1.100"）</param>
        /// <returns>MAC地址（格式XX:XX:XX:XX:XX:XX）</returns>
        public static string GetMacByIp(string targetIp)
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface ni in interfaces)
            {
                //if (ni.OperationalStatus != OperationalStatus.Up)
                //    continue;

                IPInterfaceProperties ipProps = ni.GetIPProperties();

                foreach (UnicastIPAddressInformation ipInfo in ipProps.UnicastAddresses)
                {
                    IPAddress ip = ipInfo.Address;

                    // 匹配目标IPv4且非环回地址
                    if (ip.ToString() == targetIp)
                    {
                        return FormatMacAddress(ni.GetPhysicalAddress());
                    }
                    else
                    {

                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 将PhysicalAddress格式化为XX:XX:XX:XX:XX:XX形式
        /// </summary>
        private static string FormatMacAddress(PhysicalAddress mac)
        {
            if (mac == null || mac.GetAddressBytes().Length == 0)
                return string.Empty;

            byte[] macBytes = mac.GetAddressBytes();
            string[] hexParts = Array.ConvertAll(macBytes, b => b.ToString("X2")); // 转为两位十六进制
            return string.Join(":", hexParts);
        }
    }
}
