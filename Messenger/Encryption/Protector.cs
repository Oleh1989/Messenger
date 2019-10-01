using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Messenger.Encryption
{
    public static class Protector
    {
        private static string password = "secret";

        // Amount of salt would be equal 16 bytes
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");

        // Amount of iterations would be equal 2000
        private static readonly int iterations = 2000;

        public static string Encrypt(string data)
        {
            byte[] plainBytes = Encoding.Unicode.GetBytes(data);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);      // set 256-bit key
            aes.IV = pbkdf2.GetBytes(16);       // set 128-bit initializing vector

            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string data)
        {
            byte[] cryptoBytes = Convert.FromBase64String(data);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);

            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cryptoBytes, 0, cryptoBytes.Length);
            }
            return Encoding.Unicode.GetString(ms.ToArray());
        }
    }
}