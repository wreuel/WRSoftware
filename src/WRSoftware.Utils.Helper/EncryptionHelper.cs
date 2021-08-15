using System;
using System.Security.Cryptography;
using System.Text;

namespace WRSoftware.Utils.Helper
{
    /// <summary>
    /// An Encryption Helper that has methods to create some
    /// Encrypted data
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionHelper"/> class.
        /// </summary>
        protected EncryptionHelper()
        {

        }

        /// <summary>
        /// Generates the sh a256 string.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns></returns>
        public static string GenerateSHA256String(string inputString)
        {
            var sha256 = (HashAlgorithm)CryptoConfig.CreateFromName("SHA256Managed");
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        /// <summary>
        /// Gets the string from hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }

        /// <summary>
        /// Generates the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        /// <summary>
        /// Encodes the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static string Encode(string email, string code)
        {
            var bytes = Encoding.UTF8.GetBytes($"{email}:{code}");
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decodes the token.
        /// </summary>
        /// <param name="encodedValue">The encoded value.</param>
        /// <returns></returns>
        public static string[] DecodeToken(string encodedValue)
        {
            var bytes = Convert.FromBase64String(encodedValue);
            var decodedString = Encoding.UTF8.GetString(bytes).Split(':');
            return decodedString;
        }
    }
}