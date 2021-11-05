using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WRSoftware.Utils.Helper
{
    /// <summary>
    /// String helper, with some check and generator of strings
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringHelper"/> class.
        /// </summary>
        protected StringHelper()
        {

        }


        ///// <summary>
        ///// Randons the number.
        ///// </summary>
        ///// <param name="max">The maximum.</param>
        ///// <returns></returns>
        //private static int RandonNumber(int max)
        //{

        //    byte[] data = new byte[16];
        //    int number = 0;
        //    do
        //    {
        //        var randomGenerator = RandomNumberGenerator.Create(); // Compliant for security-sensitive use cases
        //        randomGenerator.GetBytes(data);

        //        number = BitConverter.ToInt32(data);
        //    } while (number > max);


        //    return number;
        //}

        /// <summary>
        /// Generates the dummy password.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>A Random string</returns>
        public static string GenerateDummyPassword(int length = 8)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%^&*_-";

            Random rand = new Random();
            // Get cryptographically random sequence of bytes
            var bytes = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(bytes);



            // Build up a string using random bytes and character classes
            var res = new StringBuilder();
            foreach (var b in bytes)
            {
                // Randomly select a character class for each byte
                switch (rand.Next(4))
                {
                    // In each case use mod to project byte b to the correct range
                    case 0:
                        res.Append(lower[b % lower.Count()]);
                        break;
                    case 1:
                        res.Append(upper[b % upper.Count()]);
                        break;
                    case 2:
                        res.Append(number[b % number.Count()]);
                        break;
                    case 3:
                        res.Append(special[b % special.Count()]);
                        break;
                }
            }

            return res.ToString();
        }

        /// <summary>
        /// Converts the string to collection.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static IEnumerable<string> ConvertStringToCollection(string message)
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                list.Add(message);
            }

            return list;
        }


        /// <summary>
        /// Converts the string to collection.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static IEnumerable<string> ConvertStringToCollection(Exception exception)
        {
            var list = new List<string>();

            if (!string.IsNullOrWhiteSpace(exception.Message))
            {
                list.Add(exception.Message);
            }

            if (!string.IsNullOrWhiteSpace(exception.StackTrace))
            {
                list.Add(exception.StackTrace);
            }

            if (!string.IsNullOrWhiteSpace(exception.InnerException?.Message))
            {
                list.Add(exception.InnerException.Message);
            }

            if (!string.IsNullOrWhiteSpace(exception.InnerException?.StackTrace))
            {
                list.Add(exception.InnerException.StackTrace);
            }

            return list;
        }

        /// <summary>
        /// Converts the collection to string.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        public static string ConvertCollectionToString(IEnumerable<string> messages) =>
            messages != null ? string.Join("<br />", messages) : string.Empty;

        /// <summary>
        /// Generates the random code.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length = 6)
        {
            var random = new Random();
            var stringChars = new char[length];
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (var i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        /// <summary>
        /// Checks if string is URL.
        /// </summary>
        /// <param name="urlString">The URL string.</param>
        /// <returns></returns>
        public static bool CheckIfStringIsAbsoluteUrl(string urlString)
        {
            return Uri.TryCreate(urlString, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Checks if string is valid base64.
        /// </summary>
        /// <param name="base64String">The base64 string.</param>
        /// <returns></returns>
        public static bool CheckIfStringIsValidBase64(string base64String)
        {
            string str;
            if (base64String.Contains(','))
            {
                str = base64String.Split(',')[1];
            }
            else
            {
                str = base64String;
            }

            var buffer = new byte[(str.Length * 3 + 3) / 4 - (str.Length > 0 && str[str.Length - 1] == '='
                ? CheckLenght(str)
                : 0)];
            return Convert.TryFromBase64String(str, buffer, out _);
        }

        private static int CheckLenght(string str)
        {
            return str.Length > 1 && str[str.Length - 2] == '=' ? 2 : 1;
        }

        /// <summary>
        /// Gets the extension from base64 string.
        /// </summary>
        /// <param name="base64String">The base64 string.</param>
        /// <returns></returns>
        public static string GetExtensionFromBase64String(string base64String)
        {
            return base64String.Split(';')[0].Split('/')[1];
        }

        /// <summary>
        /// Gets the content type from base64 string.
        /// </summary>
        /// <param name="base64String">The base64 string.</param>
        /// <returns></returns>
        public static string GetContentTypeFromBase64String(string base64String)
        {
            return base64String.Split(':')[1].Split(';')[0];
        }

        /// <summary>
        /// Gets the byte array from base64 string.
        /// </summary>
        /// <param name="base64String">The base64 string.</param>
        /// <returns></returns>
        public static byte[] GetByteArrayFromBase64String(string base64String)
        {
            string str;
            if (base64String.Contains(','))
            {
                str = base64String.Split(',')[1];
            }
            else
            {
                str = base64String;
            }

            return Convert.FromBase64String(str);
        }

        /// <summary>
        /// Jsons to query.
        /// </summary>
        /// <param name="jsonQuery">The json query.</param>
        /// <returns></returns>
        public static string JsonToQuery(string jsonQuery)
        {
            string str = "?";
            str += jsonQuery.Replace(":", "=").Replace("{", "").Replace("}", "").Replace(",", "&").Replace("\"", "");
            return str;
        }


        /// <summary>Return only the numbers from a string</summary>
        /// <param name="txt">The text.</param>
        /// <returns>str</returns>
        public static string OnlyNumber(string txt)
        {
            var str = "";

            for (var i = 0; i < txt.Length; i++)
            {
                if (char.IsNumber(txt, i))
                {
                    str += txt.Substring(i, 1);
                }
            }

            return str;
        }
    }
}