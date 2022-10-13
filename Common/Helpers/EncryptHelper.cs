using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class EncryptHelper
    {

        private static string hash
        {
            get
            {
                return "b14ca5898a4e4133bbce2ea2315a1916";
            }
        }

        public static string Decrypt(string clave)
        {
            try
            {
                byte[] longitudBytes = new byte[16];
                byte[] claveBase64 = Convert.FromBase64String(clave);

                using (var aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(hash);
                    aes.IV = longitudBytes;
                    var desencriptador = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (var ms = new MemoryStream(claveBase64))
                    {
                        using (var cryptoStream = new CryptoStream(ms, desencriptador, CryptoStreamMode.Read))
                        {
                            using (var streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public static string Encrypt(string clave)
        {
            byte[] longitudBytes = new byte[16];

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(hash);
                aes.IV = longitudBytes;

                ICryptoTransform encriptador = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, encriptador, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cryptoStream))
                        {
                            sw.Write(clave);
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
    }
}
