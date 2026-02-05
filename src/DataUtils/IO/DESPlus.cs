using System;
using System.Security.Cryptography;
namespace Feng.IO
{
    public class DESPlus
    { 
 
        public static String byteArr2HexStr(byte[] arrB)
        {
            int iLen = arrB.Length;
            // 每个byte用两个字符才能表示，所以字符串的长度是数组长度的两倍
            System.Text.StringBuilder sb = new System.Text.StringBuilder(iLen * 2);
            for (int i = 0; i < iLen; i++)
            {
                int intTmp = arrB[i];
                // 把负数转换为正数
                while (intTmp < 0)
                {
                    intTmp = intTmp + 256;
                }
                // 小于0F的数需要在前面补0
                if (intTmp < 16)
                {
                    sb.Append("0");
                }
                sb.Append(Convert.ToString(intTmp, 16));
            }
            return sb.ToString();
        }

        public static byte[] hexStr2ByteArr(String strIn)
        {
            byte[] arrB = Feng.IO.BitConver.GetBytes(strIn);
            int iLen = arrB.Length;

            // 两个字符表示一个字节，所以字节数组长度是字符串长度除以2
            byte[] arrOut = new byte[iLen / 2];
            for (int i = 0; i < iLen; i = i + 2)
            {
                String strTmp = Feng.IO.BitConver.GetString(arrB, i, 2);
                arrOut[i / 2] = (byte)Convert.ToInt32(strTmp, 16);
            }
            return arrOut;
        }

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        public DESPlus(String strKey)
        {
            
            des.Mode = CipherMode.ECB;
            byte[] keydata = Feng.IO.BitConver.GetBytes(strKey);

            des.Key = getKey(keydata);
 
        }
 
        public byte[] encrypt(byte[] arrB)
        {
            return des.CreateEncryptor().TransformFinalBlock(arrB, 0, arrB.Length);
        }
 
        public String encrypt(String strIn)
        {
            return byteArr2HexStr(encrypt(Feng.IO.BitConver.GetBytes(strIn)));
        }

        public String getEncode(String strIn)
        {
            return byteArr2HexStr(encrypt(Feng.IO.BitConver.GetBytes(strIn)));
        }

        public byte[] decrypt(byte[] arrB)
        {
            return des.CreateDecryptor().TransformFinalBlock(arrB, 0, arrB.Length);
        }
 
        public String decrypt(String strIn)
        {
            return Feng.IO.BitConver.GetString(decrypt(hexStr2ByteArr(strIn)));
        }

        public String getDecode(String strIn)
        {
            return Feng.IO.BitConver.GetString(decrypt(hexStr2ByteArr(strIn)));
        }
 
        private byte[] getKey(byte[] arrBTmp)
        { 
            byte[] arrB = new byte[8];
 
            for (int i = 0; i < arrBTmp.Length && i < arrB.Length; i++)
            {
                arrB[i] = arrBTmp[i];
            } 

            return arrB;
        }
 
    }
    /*
    public static void main(String[] args) {
   // TODO Auto-generated method stub
   try {
    String test = "Hellow Word!";
    //DESPlus des = new DESPlus();//默认密钥
    DESPlus des = new DESPlus("leemenz");//自定义密钥
    System.out.println("加密前的字符："+test);
    System.out.println("加密后的字符："+des.encrypt(test));
    System.out.println("解密后的字符："+des.decrypt(des.encrypt(test)));
   } catch (Exception e) {
    // TODO: handle exception
    e.printStackTrace();
   }
    }
   */
}