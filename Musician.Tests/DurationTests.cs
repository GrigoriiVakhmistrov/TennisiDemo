using Musician.Tests.Utils;
using Xunit;

namespace Musician.Tests {
    public class DurationTests {
        [Fact]
        public void TryParseCorrectDataTest1() {
            string input = "3/4";
            Duration expected = new Duration(3, 4);

            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseCorrectDataTest2() {
            string input = " 3  /   4    ";
            Duration expected = new Duration(3, 4);

            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseEmptyDataTest() {
            string input = "";
            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseNullDataTest() {
            string input = null;
            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest1() {
            string input = "3";
            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest2() {
            string input = "a/b";
            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest3() {
            string input = "0/3";
            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest4() {
            string input = "-3/3";
            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest5() {
            string input = "3/-1";
            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void ToStringTest1() {
            string expected = "1/8";
            Duration actual = new Duration(1, 8);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void ToStringTest2() {
            string expected = "1/32";
            Duration actual = new Duration(1, 32);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void EqualityTest() {
            Duration duration1 = new Duration(2, 4);
            Duration duration2 = new Duration(2, 4);
            Duration duration3 = new Duration(3, 4);
            EqualityTests.TestEqualObjects(duration1, duration2);
            EqualityTests.TestUnequalObjects(duration1, duration3);
            EqualityTests.TestAgainstNull(duration1);
        }
    }
}
