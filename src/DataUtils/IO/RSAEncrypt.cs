using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RsaEncryptionExample
{
    public class RSAEncrypt
    {
        public RSAEncrypt()
        {
            // 创建 RSA 对象
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            // 获取私钥和公钥
            string privateKey = rsa.ToXmlString(true); 
            string publicKey = rsa.ToXmlString(false);

            //// 准备要加密的数据
            //byte[] rawData = Encoding.UTF8.GetBytes("Hello, world!");

            //// 加密数据
            //byte[] encryptedData = RSAEncrypt(rawData, publicKey);

            //// 解密数据
            //byte[] decryptedData = RSADecrypt(encryptedData, privateKey);

            //// 显示结果
            //Console.WriteLine(Encoding.UTF8.GetString(decryptedData));
        }

        /// <summary>
        /// 使用指定的公钥加密数据
        /// </summary>
        /// <param name="data">要加密的字节数组</param>
        /// <param name="publicKey">公钥字符串</param>
        /// <returns>加密后的字节数组</returns>
        public static byte[] Encrypt(byte[] data, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(data, false);
        }

        /// <summary>
        /// 使用指定的私钥解密数据
        /// </summary>
        /// <param name="data">要解密的字节数组</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <returns>解密后的字节数组</returns>
        private static byte[] Decrypt(byte[] data, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            return rsa.Decrypt(data, false);
        }
    }
}