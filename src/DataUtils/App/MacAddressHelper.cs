using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Feng.App
{

    public class MacAddressHelper
    {
        // 导入SendARP函数
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(
            uint destIp,       // 目标IP地址（uint格式）
            uint srcIp,        // 源IP地址（0表示自动选择）
            byte[] macAddr,    // 输出：MAC地址缓冲区
            ref int phyAddrLen // 输入：缓冲区长度；输出：实际MAC长度
        );

        /// <summary>
        /// 将IPAddress转换为uint（适应SendARP参数要求）
        /// </summary>
        private static uint IpToUInt32(IPAddress ip)
        {
            byte[] bytes = ip.GetAddressBytes();
            // 网络字节序（大端）转主机字节序（小端，Windows系统）
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// 根据IP地址获取MAC地址（仅支持IPv4局域网）
        /// </summary>
        /// <param name="ip">远程IP地址</param>
        /// <returns>MAC地址（格式：XX-XX-XX-XX-XX-XX），失败返回"Unknown"</returns>
        public static string GetMacAddress(IPAddress ip)
        {
            // 仅支持IPv4
            if (ip.AddressFamily != AddressFamily.InterNetwork)
                return "不支持IPv6";

            byte[] macBuffer = new byte[6]; // MAC地址为6字节
            int bufferLength = macBuffer.Length;

            uint destIp = IpToUInt32(ip);
            int result = SendARP(destIp, 0, macBuffer, ref bufferLength);

            // 成功获取（返回0）且MAC长度为6字节
            if (result == 0 && bufferLength == 6)
                return BitConverter.ToString(macBuffer);

            return "Unknown"; // 失败（如跨网段、ARP缓存无记录）
        }
    }
}
