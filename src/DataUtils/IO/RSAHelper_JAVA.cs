
//using System;
//using System.Drawing;
//using System.IO;

//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Engines;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.Security;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Xml;

//namespace Feng.IO
//{
//    //https://www.jianshu.com/p/1de40d21c413 为什么要才用RSA+AES的方式
//    //https://www.jianshu.com/p/8dc4a5f64e06 RSA加密/解密和签名/验签过程理解
//    //https://www.cnblogs.com/f1194361820/p/4260025.html 生动形象的描述了过程
//    //RSA是一种非对称加解密算法，用于实现签名/认证等。在.Net框架中提供了System.Security.Cryptography.RSACryptoServiceProvider类，用于封装实现RSA算法，但这个类使用的公钥/私钥格式为XML，这是.Net特有的格式，而其它语言如Java编程中一般使用PEM或DER等格式，OpenSSL规范中也特荐为PEM格式。项目中经常会遇到XML与PEM格式的密钥相互转换的需求

//    //RSA加密/解密和签名/验签过程理解 A->B:
//    //A:
//    // 1. A提取消息m的消息摘要h(m),并使用自己的私钥对摘要h(m)进行加密,生成签名s
//    // 2. A将签名s和消息m一起,使用B的公钥进行加密,生成密文c,发送给B

//    // B:
//    // 1. B接收到密文c,使用自己的私钥解密c得到明文m和数字签名s
//    // 2. B使用A的公钥解密数字签名s解密得到H(m)
//    // 3. B使用相同的方法提取消息m的消息摘要h(m)
//    // 4. B比较两个消息摘要。相同则验证成功;不同则验证失败
//    public partial class RSAHelper_JAVA
//    {

//        #region 私钥加密，公钥解密
//        #region 私钥加密
//        /// <summary>
//        /// 基于BouncyCastle的RSA私钥加密
//        /// </summary>
//        /// <param name="privateKeyJava"></param>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static string EncryptPrivateKeyJava(string privateKeyJava, string data, string encoding = "UTF-8")
//        {
//            RsaKeyParameters privateKeyParam = (RsaKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKeyJava));
//            byte[] cipherbytes = Encoding.GetEncoding(encoding).GetBytes(data);
//            RsaEngine rsa = new RsaEngine();
//            rsa.Init(true, privateKeyParam);//参数true表示加密/false表示解密。
//            cipherbytes = rsa.ProcessBlock(cipherbytes, 0, cipherbytes.Length);
//            return Convert.ToBase64String(cipherbytes);
//        }
//        #endregion

//        #region 公钥解密
//        /// <summary>
//        /// 基于BouncyCastle的RSA公钥解密
//        /// </summary>
//        /// <param name="publicKeyJava"></param>
//        /// <param name="data"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static string DecryptPublicKeyJava(string publicKeyJava, string data, string encoding = "UTF-8")
//        {
//            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKeyJava));
//            byte[] cipherbytes = Convert.FromBase64String(data);
//            RsaEngine rsa = new RsaEngine();
//            rsa.Init(false, publicKeyParam);//参数true表示加密/false表示解密。
//            cipherbytes = rsa.ProcessBlock(cipherbytes, 0, cipherbytes.Length);
//            return Encoding.GetEncoding(encoding).GetString(cipherbytes);
//        }
//        #endregion

//        #region 加签
//        /// <summary>
//        /// 基于BouncyCastle的RSA签名
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="privateKeyJava"></param>
//        /// <param name="hashAlgorithm">JAVA的和.NET的不一样，如：MD5(.NET)等同于MD5withRSA(JAVA)</param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static string RSASignJavaBouncyCastle(string data, string privateKeyJava, string hashAlgorithm = "SHA256withRSA", string encoding = "UTF-8")
//        {
//            RsaKeyParameters privateKeyParam = (RsaKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKeyJava));
//            ISigner signer = SignerUtilities.GetSigner(hashAlgorithm);
//            signer.Init(true, privateKeyParam);//参数为true验签，参数为false加签
//            var dataByte = Encoding.GetEncoding(encoding).GetBytes(data);
//            signer.BlockUpdate(dataByte, 0, dataByte.Length);
//            //return Encoding.GetEncoding(encoding).GetString(signer.GenerateSignature()); //签名结果 非Base64String
//            return Convert.ToBase64String(signer.GenerateSignature());
//        }
//        #endregion

//        #region 验签
//        /// <summary>
//        /// 基于BouncyCastle的RSA签名
//        /// </summary>
//        /// <param name="data">源数据</param>
//        /// <param name="publicKeyJava"></param>
//        /// <param name="signature">base64签名</param>
//        /// <param name="hashAlgorithm">JAVA的和.NET的不一样，如：MD5(.NET)等同于MD5withRSA(JAVA)</param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static bool VerifyJavaBouncyCastle(string data, string publicKeyJava, string signature, string hashAlgorithm = "SHA256withRSA", string encoding = "UTF-8")
//        {
//            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKeyJava));
//            ISigner signer = SignerUtilities.GetSigner(hashAlgorithm);
//            signer.Init(false, publicKeyParam);
//            byte[] dataByte = Encoding.GetEncoding(encoding).GetBytes(data);
//            signer.BlockUpdate(dataByte, 0, dataByte.Length);
//            //byte[] signatureByte = Encoding.GetEncoding(encoding).GetBytes(signature);// 非Base64String
//            byte[] signatureByte = Convert.FromBase64String(signature);
//            return signer.VerifySignature(signatureByte);
//        }
//        #endregion
//        #endregion

//        #region 公钥加密，私钥解密
//        #region 加密


//        /// <summary>
//        /// RSA加密
//        /// </summary>
//        /// <param name="publicKeyJava"></param>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static string EncryptJava(string publicKeyJava, string data, string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            byte[] cipherbytes;
//            rsa.FromPublicKeyJavaString(publicKeyJava);

//            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
//            //HashAlgorithmName hashName = new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm);
//            //RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA512;//RSAEncryptionPadding.CreateOaep(hashName);//.NET 4.6以后特有
//            //cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
//            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

//            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
//            cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), false);

//            return Convert.ToBase64String(cipherbytes);
//        }
//        /// <summary>
//        /// RSA加密
//        /// </summary>
//        /// <param name="publicKeyCSharp"></param>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static string EncryptCSharp(string publicKeyCSharp, string data, string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            byte[] cipherbytes;
//            rsa.FromXmlString(publicKeyCSharp);

//            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
//            //HashAlgorithmName hashName = new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm);
//            //RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA512;//RSAEncryptionPadding.CreateOaep(hashName);//.NET 4.6以后特有
//            //cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
//            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

//            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
//            cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), false);

//            return Convert.ToBase64String(cipherbytes);
//        }

//        /// <summary>
//        /// RSA加密PEM秘钥
//        /// </summary>
//        /// <param name="publicKeyPEM"></param>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public static string EncryptPEM(string publicKeyPEM, string data, string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            byte[] cipherbytes;
//            rsa.LoadPublicKeyPEM(publicKeyPEM);

//            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
//            //HashAlgorithmName hashName = new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm);
//            //RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA512;//RSAEncryptionPadding.CreateOaep(hashName);//.NET 4.6以后特有
//            //cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
//            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

//            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
//            cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(data), false);

//            return Convert.ToBase64String(cipherbytes);
//        }
//        #endregion

//        #region 解密


//        /// <summary>
//        /// RSA解密
//        /// </summary>
//        /// <param name="privateKeyJava"></param>
//        /// <param name="content"></param>
//        /// <returns></returns>
//        public static string DecryptJava(string privateKeyJava, string data, string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            byte[] cipherbytes;
//            rsa.FromPrivateKeyJavaString(privateKeyJava);
//            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
//            //RSAEncryptionPadding padding = RSAEncryptionPadding.CreateOaep(new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm));//.NET 4.6以后特有
//            //cipherbytes = rsa.Decrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
//            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

//            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
//            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);

//            return Encoding.GetEncoding(encoding).GetString(cipherbytes);
//        }
//        /// <summary>
//        /// RSA解密
//        /// </summary>
//        /// <param name="privateKeyCSharp"></param>
//        /// <param name="content"></param>
//        /// <returns></returns>
//        public static string DecryptCSharp(string privateKeyCSharp, string data, string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            byte[] cipherbytes;
//            rsa.FromXmlString(privateKeyCSharp);
//            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
//            //RSAEncryptionPadding padding = RSAEncryptionPadding.CreateOaep(new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm));//.NET 4.6以后特有
//            //cipherbytes = rsa.Decrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
//            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

//            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
//            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);

//            return Encoding.GetEncoding(encoding).GetString(cipherbytes);
//        }
//        /// <summary>
//        /// RSA解密
//        /// </summary>
//        /// <param name="privateKeyPEM"></param>
//        /// <param name="content"></param>
//        /// <returns></returns>
//        public static string DecryptPEM(string privateKeyPEM, string data, string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            byte[] cipherbytes;
//            rsa.LoadPrivateKeyPEM(privateKeyPEM);
//            //☆☆☆☆.NET 4.6以后特有☆☆☆☆
//            //RSAEncryptionPadding padding = RSAEncryptionPadding.CreateOaep(new System.Security.Cryptography.HashAlgorithmName(hashAlgorithm));//.NET 4.6以后特有
//            //cipherbytes = rsa.Decrypt(Encoding.GetEncoding(encoding).GetBytes(data), padding);
//            //☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆☆

//            //☆☆☆☆.NET 4.6以前请用此段代码☆☆☆☆
//            cipherbytes = rsa.Decrypt(Convert.FromBase64String(data), false);

//            return Encoding.GetEncoding(encoding).GetString(cipherbytes);
//        }
//        #endregion


//        #region 加签

//        /// <summary>
//        /// RSA签名
//        /// </summary>
//        /// <param name="privateKeyJava">私钥</param>
//        /// <param name="data">待签名的内容</param>
//        /// <returns></returns>
//        public static string RSASignJava(string data, string privateKeyJava, string hashAlgorithm = "MD5", string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            rsa.FromPrivateKeyJavaString(privateKeyJava);//加载私钥
//                                                         //RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(rsa);
//                                                         ////设置签名的算法为MD5 MD5withRSA 签名
//                                                         //RSAFormatter.SetHashAlgorithm(hashAlgorithm);


//            var dataBytes = Encoding.GetEncoding(encoding).GetBytes(data);
//            var HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
//            return Convert.ToBase64String(HashbyteSignature);

//            //byte[] HashbyteSignature = ConvertToRgbHash(data, encoding);

//            //byte[] dataBytes =Encoding.GetEncoding(encoding).GetBytes(data);
//            //HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
//            //return Convert.ToBase64String(HashbyteSignature);
//            //执行签名
//            //EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
//            //return Convert.ToBase64String(RSAFormatter.CreateSignature(HashbyteSignature));
//            //return result.Replace("=", String.Empty).Replace('+', '-').Replace('/', '_');
//        }
//        /// <summary>
//        /// RSA签名
//        /// </summary>
//        /// <param name="privateKeyPEM">私钥</param>
//        /// <param name="data">待签名的内容</param>
//        /// <returns></returns>
//        public static string RSASignPEM(string data, string privateKeyPEM, string hashAlgorithm = "MD5", string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            rsa.LoadPrivateKeyPEM(privateKeyPEM);//加载私钥
//            var dataBytes = Encoding.GetEncoding(encoding).GetBytes(data);
//            var HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
//            return Convert.ToBase64String(HashbyteSignature);
//        }
//        /// <summary>
//        /// RSA签名CSharp
//        /// </summary>
//        /// <param name="privateKeyCSharp">私钥</param>
//        /// <param name="data">待签名的内容</param>
//        /// <returns></returns>
//        public static string RSASignCSharp(string data, string privateKeyCSharp, string hashAlgorithm = "MD5", string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            rsa.FromXmlString(privateKeyCSharp);//加载私钥
//            var dataBytes = Encoding.GetEncoding(encoding).GetBytes(data);
//            var HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
//            return Convert.ToBase64String(HashbyteSignature);
//        }

//        #endregion

//        #region 验签

//        /// <summary>
//        /// 验证签名-方法一
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="signature"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static bool VerifyJava(string data, string publicKeyJava, string signature, string hashAlgorithm = "MD5", string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            //导入公钥，准备验证签名
//            rsa.FromPublicKeyJavaString(publicKeyJava);
//            //返回数据验证结果
//            byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
//            byte[] rgbSignature = Convert.FromBase64String(signature);

//            return rsa.VerifyData(Data, hashAlgorithm, rgbSignature);

//            //return SignatureDeformatter(publicKeyJava, data, signature);

//            //return CheckSign(publicKeyJava, data, signature);

//            //return rsa.VerifyData(Encoding.GetEncoding(encoding).GetBytes(data), "MD5", Encoding.GetEncoding(encoding).GetBytes(signature));
//        }
//        /// <summary>
//        /// 验证签名PEM
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="signature"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static bool VerifyPEM(string data, string publicKeyPEM, string signature, string hashAlgorithm = "MD5", string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            //导入公钥，准备验证签名
//            rsa.LoadPublicKeyPEM(publicKeyPEM);
//            //返回数据验证结果
//            byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
//            byte[] rgbSignature = Convert.FromBase64String(signature);

//            return rsa.VerifyData(Data, hashAlgorithm, rgbSignature);
//        }

//        /// <summary>
//        /// 验证签名CSharp
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="signature"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static bool VerifyCSharp(string data, string publicKeyCSharp, string signature, string hashAlgorithm = "MD5", string encoding = "UTF-8")
//        {
//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            //导入公钥，准备验证签名
//            rsa.LoadPublicKeyPEM(publicKeyCSharp);
//            //返回数据验证结果
//            byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
//            byte[] rgbSignature = Convert.FromBase64String(signature);

//            return rsa.VerifyData(Data, hashAlgorithm, rgbSignature);
//        }

//        #region 签名验证-方法二
//        /// <summary>
//        /// 签名验证
//        /// </summary>
//        /// <param name="publicKey">公钥</param>
//        /// <param name="p_strHashbyteDeformatter">待验证的用户名</param>
//        /// <param name="signature">注册码</param>
//        /// <returns>签名是否符合</returns>
//        public static bool SignatureDeformatter(string publicKey, string data, string signature, string hashAlgorithm = "MD5")
//        {
//            try
//            {
//                byte[] rgbHash = ConvertToRgbHash(data);
//                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//                //导入公钥，准备验证签名
//                rsa.FromPublicKeyJavaString(publicKey);

//                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(rsa);
//                deformatter.SetHashAlgorithm("MD5");
//                byte[] rgbSignature = Convert.FromBase64String(signature);
//                if (deformatter.VerifySignature(rgbHash, rgbSignature))
//                {
//                    return true;
//                }
//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 签名数据转化为RgbHash
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static byte[] ConvertToRgbHash(string data, string encoding = "UTF-8")
//        {
//            using (MD5 md5 = new MD5CryptoServiceProvider())
//            {
//                byte[] bytes_md5_in = Encoding.GetEncoding(encoding).GetBytes(data);
//                return md5.ComputeHash(bytes_md5_in);
//            }
//        }
//        #endregion

//        #region 签名验证-方法三
//        /// <summary>
//        /// 验证签名
//        /// </summary>
//        /// <param name="data">原始数据</param>
//        /// <param name="sign">签名</param>
//        /// <returns></returns>
//        public static bool CheckSign(string publicKey, string data, string sign, string encoding = "UTF-8")
//        {

//            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
//            rsa.FromPublicKeyJavaString(publicKey);
//            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

//            byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
//            byte[] rgbSignature = Convert.FromBase64String(sign);
//            if (rsa.VerifyData(Data, md5, rgbSignature))
//            {
//                return true;
//            }
//            return false;
//        }
//        #endregion
//        #endregion
//        #endregion
//    }
//}

