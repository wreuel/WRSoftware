using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WRSoftware.Utils.Helper.Tests
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData(8)]
        [InlineData(10)]
        [InlineData(20)]
        public void GenerateDummyPasswordTest(int expected)
        {
            var str = StringHelper.GenerateDummyPassword(expected);
            Assert.Equal(expected, str.Length);
        }

        [Theory]
        [InlineData("Error")]
        [InlineData("My Message")]
        public void ConvertStringToCollectionTest(string expected)
        {
            var list = StringHelper.ConvertStringToCollection(expected);

            Assert.Single(list);
            Assert.Equal(expected, list.FirstOrDefault());
            Assert.IsAssignableFrom<IEnumerable<string>>(list);

        }

        public static IEnumerable<object[]> Exceptions()
        {
            yield return new object[] { "Error", 2, new Exception("Error", new Exception("Inner Exception")) };
            yield return new object[] { "Error", 1, new Exception("Error") };
        }

        [Theory]
        [MemberData(nameof(Exceptions))]
        public void ConvertStringToCollectionExceptionTest(string strExpected, int sizeExpected, Exception value)
        {
            var list = StringHelper.ConvertStringToCollection(value);

            Assert.Equal(strExpected, list.FirstOrDefault());
            Assert.Equal(sizeExpected, list.Count());
            Assert.IsAssignableFrom<IEnumerable<string>>(list);

        }

        [Theory]
        [InlineData(null)]
        public void ConvertCollectionToStringEmptyTest(IEnumerable<string> expected)
        {
            var result = StringHelper.ConvertCollectionToString(expected);
            Assert.Equal(string.Empty, result);
        }

        public static IEnumerable<object[]> ListString()
        {
            yield return new object[] { "teste<br /testes", new List<string>() { "teste", "testes" } };

        }

        [Theory]
        [MemberData(nameof(ListString))]
        public void ConvertCollectionToStringTest(string expected, IEnumerable<string> entry)
        {
            var result = StringHelper.ConvertCollectionToString(entry);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData("5925250", "59.252.50")]
        [InlineData("", "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+")]
        [InlineData("8485", "A84B85")]
        public void OnlyNumberTest(string expected, string entry)
        {
            var result = StringHelper.OnlyNumber(entry);
            Assert.Equal(expected, result);
        }

    }
}

