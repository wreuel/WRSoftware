using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WRSoftware.Utils.Helper.Tests
{
    public class StringHelperTests
    {
        public static IEnumerable<object[]> Exception()
        {

            yield return new object[] {new Exception("Error")};
        }

        public static IEnumerable<object[]> Exceptions()
        {
            var myError = new Exception("Error");
            
            yield return new object[] { new Exception("Error", myError) };
            yield return new object[] { myError };
        }
        

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


        [Theory, MemberData(nameof(Exceptions))]
        public void ConvertStringToCollectionExceptionTest(Exception expected)
        {
            var list = StringHelper.ConvertStringToCollection(expected);

            Assert.Equal(expected.Message, list.FirstOrDefault());
            Assert.IsAssignableFrom<IEnumerable<string>>(list);
        }

        [Theory]
        [InlineData(null)]
        public void ConvertCollectionToStringEmptyTest(IEnumerable<string> expected)
        {
            var result = StringHelper.ConvertCollectionToString(expected);
            Assert.Equal(string.Empty,result);
        }

        public static IEnumerable<object[]> ListString()
        {
            yield return new object[] { new List<string>(){"teste", "testes"} };
            
        }

        [Theory, MemberData(nameof(ListString))]
        public void ConvertCollectionToStringTest(IEnumerable<string> expected)
        {
            var result = StringHelper.ConvertCollectionToString(expected);
            Assert.Equal(string.Join("<br />", expected), result);
        }


        [Theory]
        [InlineData("5925250","59.252.50")]
        [InlineData("", "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+")]
        [InlineData("8485", "A84B85")]
        public void OnlyNumberTest(string expected, string entry)
        {
            var result = StringHelper.OnlyNumber(entry);
            Assert.Equal(expected, result);
        }

    }
}

