using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Falquan.KeyRinger.Services
{
    public class CryptographicProvider
    {
        private static readonly byte[] _key = Convert.FromBase64String(System.Configuration.ConfigurationManager.AppSettings["Falquan.KeyRinger.Api.AES.Key"]);

        public static byte[] GetKey()
        {
            using (var _aes = AesManaged.Create())
            {
                return _aes.Key;
            }
        }

        public static byte[] GetIV()
        {
            using (var _aes = AesManaged.Create())
            {
                return _aes.IV;
            }
        }

        public static byte[] Encrypt(byte[] iv, string data)
        {
            if (_key == null || _key.Length <= 0) throw new NullReferenceException("Encryption key cannot be null");
            if (iv == null || iv.Length <= 0) throw new ArgumentNullException("iv", "Initialization vector cannot be null");
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data", "Data to encrypt cannot be null");

            byte[] encrypted;
            using (var _aes = AesManaged.Create())
            {
                var encryptor = _aes.CreateEncryptor(_key, iv);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(data);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public static string Decrypt(byte[] iv, byte[] data)
        {
            if (_key == null || _key.Length <= 0) throw new NullReferenceException("Encryption key cannot be null");
            if (iv == null || iv.Length <= 0) throw new ArgumentNullException("iv", "Initialization vector cannot be null");
            if (data == null || data.Length <= 0) throw new ArgumentNullException("data", "Data to encrypt cannot be null");

            string decrypted;
            using (var _aes = AesManaged.Create())
            {
                ICryptoTransform decryptor = _aes.CreateDecryptor(_key, iv);

                using (MemoryStream msDecrypt = new MemoryStream(data))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decrypted = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }
    }
}
