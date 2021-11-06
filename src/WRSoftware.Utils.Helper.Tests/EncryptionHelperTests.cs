using System.Collections.Generic;
using Xunit;

namespace WRSoftware.Utils.Helper.Tests
{
    public class EncryptionHelperTests
    {
        public static IEnumerable<object[]> SHA256_String()
        {
            yield return new object[] { "My Phrase", "5691F3F4BBD584713E61C1BA4C8452F87F529137453569CD7C0C5BF4CB560FA3" };

        }

        [Theory]
        [MemberData(nameof(SHA256_String))]
        public void GenerateSHA256String_Expected(string entry, string expected)
        {
            var result = EncryptionHelper.GenerateSHA256String(entry);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void GenerateRandomString_Expected(int expected)
        {
            var result = EncryptionHelper.GenerateRandomString(expected);
            Assert.Equal(expected, result.Length);
        }

        public static IEnumerable<object[]> EmailCodeResult()
        {
            yield return new object[] { "email@email.com", "1233456", "ZW1haWxAZW1haWwuY29tOjEyMzM0NTY=" };

        }

        [Theory]
        [MemberData(nameof(EmailCodeResult))]
        public void Encode_Expected(string email, string code, string expected)
        {
            var result = EncryptionHelper.Encode(email, code);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(EmailCodeResult))]
        public void Decode_Expected(string email, string code, string encoded)
        {
            var result = EncryptionHelper.DecodeToken(encoded);
            Assert.Equal(2, result.Length);
            Assert.Equal(result[0], email);
            Assert.Equal(result[1], code);
        }
    }
}
