using System;

using Xunit;

namespace SOCATemplate.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("b", "b")]
        public void Test2(string data, string expected)
        {
            Assert.Equal(expected, data);
        }
    }
}
