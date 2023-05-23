using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GHIBMS.Pub
{


    public class StringEncryption
    {
        // Fields
        private static readonly byte[] DesIVs = new byte[] { 0xC7, 0x2E, 0xFC, 0x6B, 3, 0xBA, 0x82, 0xDD };
        private static readonly byte[] DesKeys = new byte[] { 0x73, 0x6A, 0x65, 0x74, 0x74, 0x6C, 0x78, 0x6C };

        // Methods
        public StringEncryption()
        {
        }

        public static string Decrypt(string encryptedData)
        {
            return DecryptString(Convert.FromBase64String(encryptedData));
        }

        public static string DecryptString(byte[] encryptedData)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(encryptedData, 0, encryptedData.Length);
                stream.Seek(0L, SeekOrigin.Begin);
                using (CryptoStream stream2 = new CryptoStream(stream, GetDesObject().CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(stream2, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static string Encrypt(string strData)
        {
            return Convert.ToBase64String(EncryptString(strData));
        }

        public static byte[] EncryptString(string strData)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strData);
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, GetDesObject().CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                }
                return stream.ToArray();
            }
        }

        private static DES GetDesObject()
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = DesKeys;
            des.IV = DesIVs;
            return des;
        }

        /// <summary>
        /// 32byte,MD5
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string StringToMD5Hash(string inputString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
    }


}
