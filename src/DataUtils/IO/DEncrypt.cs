using System;
using System.Security.Cryptography;
using System.Text;

namespace Feng.IO
{
    public class DEncrypt
    {
        public DEncrypt()
        {

        }

        public static string Encrypt(string original)
        {
            return Encrypt(original, Feng.App.Systeminfo.Key);
        }

        public static byte[] GetBytes(string text)
        {
            return Feng.IO.BitConver.GetBytes(text);
        }

        public static string Decrypt(string original)
        {
            return Decrypt(original, Feng.App.Systeminfo.Key);
        }

        public static string Encrypt(string original, string key)
        {
            byte[] buff = GetBytes(original);
            byte[] kb = GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }

        public static string ZEncrypt(string original, string key)
        {
            byte[] buff = GetBytes(original);
            byte[] kb = GetBytes(key);
            byte[] data = Encrypt(buff, kb);
            //byte[] chars = new byte[data.Length / 2];
            //for (int i = 0; i < data.Length; i++)
            //{

            //}
            string text = Convert.ToBase64String(data);
            return text;
        }

        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Unicode);
        }

        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }

        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = GetBytes(Feng.App.Systeminfo.Key);
            return Decrypt(encrypted, key);
        }

        public static byte[] Encrypt(string password, byte[] original)
        {
            byte[] key = GetBytes(password);
            return Encrypt(original, key);
        }

        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = GetBytes(Feng.App.Systeminfo.Key);
            return Encrypt(original, key);
        }

        public static byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }

        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        public static byte[] Encrypt(byte[] original, string key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            byte[] data = GetBytes(key);
            des.Key = MakeMD5(data);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        public static byte[] Decrypt(byte[] encrypted, string key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            byte[] data = GetBytes(key);
            des.Key = MakeMD5(data);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        public static byte[] Decrypt(string password, byte[] encrypted)
        {
            byte[] key = GetBytes(password);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        public static string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Feng.IO.BitConver.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private static byte[] GetMd5(byte[] data)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(data);
            hashmd5 = null;
            return keyhash;
        }

        public static string GetBase64Text(byte[] data)
        {
            string str = Convert.ToBase64String(data);
            return str;
        }

        public static byte[] GetBase64Bytes(string data)
        {
            byte[] buff = Convert.FromBase64String(data);
            return buff;
        }



    }
}
 