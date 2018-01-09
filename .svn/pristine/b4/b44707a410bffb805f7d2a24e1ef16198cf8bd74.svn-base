using System;
using System.Security.Cryptography;
using System.Text;

namespace Eam.Core.Utility
{
    /// <summary>
    /// Hash散列算法
    /// </summary>
    public class HashEncrypt
    {
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Md5Hash(byte[] data)
        {
            MD5 md5 = MD5.Create();
            return md5.ComputeHash(data);
        }
        public static byte[] Md5Hash(string data)
        {
            byte[] md5Source = Encoding.UTF8.GetBytes(data);
            return Md5Hash(md5Source);
        }
        public static string Md5HashForBase64(byte[] data)
        {
            var md5Out = Md5Hash(data);
            return Convert.ToBase64String(md5Out);
        }
        public static string Md5HashForBase64(string data)
        {
            var md5Out = Md5Hash(data);
            return Convert.ToBase64String(md5Out);
        }
        public static string Md5HashForHex(byte[] data)
        {
            var md5Out = Md5Hash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Out.Length; i++)
            {
                sb.Append(i.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        public static string Md5HashForHex(string data)
        {
            var md5Out = Md5Hash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Out.Length; i++)
            {
                sb.Append(md5Out[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }


        /// <summary>
        /// SHA1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Sha1Hash(byte[] data)
        {
            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(data);
        }
        /// <summary>
        /// SHA256
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Sha256Hash(byte[] data)
        {
            SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(data);
        }

        /// <summary>
        /// SHA384
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Sha384Hash(byte[] data)  
        {  
            SHA384 sha384 = SHA384.Create();  
            return sha384.ComputeHash(data);  
        }

        /// <summary>
        /// SHA512
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Sha512Hash(byte[] data)
        {
            SHA512 sha512 = SHA512.Create();
            return sha512.ComputeHash(data);
        }  
    }
}