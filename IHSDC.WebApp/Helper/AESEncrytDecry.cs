using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace IHSDC.WebApp.Helper
{
    public class AESEncrytDecry
    {
        private static readonly Random RNG = new Random();

        public static string GetSalt()
        {
            var builder = new StringBuilder();

            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }

            return builder.ToString();
        }

        public static string GetKey()
        {
            var builder = new StringBuilder();

            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }

            return builder.ToString();
        }

        public static string DecryptAES(string cipherText, string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cipherText) ||
                    string.IsNullOrWhiteSpace(key))
                {
                    return null;
                }

                // AES supports 16, 24 or 32 byte keys
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                if (keyBytes.Length != 16 &&
                    keyBytes.Length != 24 &&
                    keyBytes.Length != 32)
                {
                    return null;
                }

                byte[] buffer;

                try
                {
                    buffer = Convert.FromBase64String(cipherText);
                }
                catch (FormatException)
                {
                    // Invalid / manipulated Base64
                    return null;
                }

                // IV is first 16 characters of key
                byte[] iv = Encoding.UTF8.GetBytes(
                    key.Substring(0, 16)
                );

                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    aes.Key = keyBytes;
                    aes.IV = iv;

                    using (ICryptoTransform decryptor =
                           aes.CreateDecryptor())
                    {
                        byte[] result =
                            decryptor.TransformFinalBlock(
                                buffer,
                                0,
                                buffer.Length
                            );

                        return Encoding.UTF8.GetString(result);
                    }
                }
            }
            catch (CryptographicException)
            {
                // Wrong key / modified encrypted data
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T DecryptAESWithDTO<T>(
            string cipherText,
            string key)
        {
            try
            {
                string json = DecryptAES(cipherText, key);

                if (string.IsNullOrEmpty(json))
                {
                    return default(T);
                }

                T result = JsonConvert.DeserializeObject<T>(json);

                return result;
            }
            catch (JsonException)
            {
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}