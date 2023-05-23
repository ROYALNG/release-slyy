using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GHCore.EncDec
{
    public static class PasswordEncDec
    {
        public static byte[] GeneratePasswordHash(string password, int salt)
        {
            byte[] array = new byte[]
            {
                (byte)(salt >> 24),
                (byte)(salt >> 16),
                (byte)(salt >> 8),
                (byte)salt
            };
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] array2 = new byte[array.Length + bytes.Length];
            Buffer.BlockCopy(bytes, 0, array2, 0, bytes.Length);
            Buffer.BlockCopy(array, 0, array2, bytes.Length, array.Length);
            SHA1 sHA = SHA1.Create();
            return sHA.ComputeHash(array2);
        }
        public static int GenerateSaltForPassword()
        {
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] array = new byte[4];
            rNGCryptoServiceProvider.GetNonZeroBytes(array);
            return ((int)array[0] << 24) + ((int)array[1] << 16) + ((int)array[2] << 8) + (int)array[3];
        }
        /// <summary>
        /// 密码验证
        /// CheckPassword(password, Convert.ToInt32(user.PasswordSalt), user.PasswordHash.ToArray<byte>())
        /// </summary>
        public static bool CheckPassword(string password, int salt, byte[] second)
        {
            IEnumerable<byte> first = GeneratePasswordHash(password, salt);
            return first.SequenceEqual(second);
        }
    }
}
