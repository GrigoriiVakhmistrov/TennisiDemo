using Musician.Tests.Utils;
using Xunit;

namespace Musician.Tests {
    public class NoteTests {
        [Fact]
        public void TryParseCorrectDataTest1() {
            string input = "8";
            Note expected = new Note(new Duration(1, 8));

            bool actualBool = Note.TryParse(input, out Note actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseCorrectDataTest2() {
            string input = "  4   ";
            Note expected = new Note(new Duration(1, 4));

            bool actualBool = Note.TryParse(input, out Note actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseEmptyDataTest() {
            string input = "";
            Note expected = new Note(new Duration(1, 4));

            bool actualBool = Note.TryParse(input, out Note actual);
            Assert.False(actualBool);
            Assert.NotEqual(expected, actual);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseNullDataTest() {
            string input = null;
            Note expected = new Note(new Duration(1, 4));

            bool actualBool = Note.TryParse(input, out Note actual);
            Assert.False(actualBool);
            Assert.NotEqual(expected, actual);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest1() {
            string input = "0";
            bool actualBool = Note.TryParse(input, out Note actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest2() {
            string input = "-5";
            bool actualBool = Note.TryParse(input, out Note actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest3() {
            string input = "a/b";
            Duration expected = new Duration(3, 4);

            bool actualBool = Duration.TryParse(input, out Duration actual);
            Assert.False(actualBool);
            Assert.NotEqual(expected, actual);
            Assert.Null(actual);
        }
        [Fact]
        public void ToStringTest1() {
            string expected = "8";
            Note actual = new Note(new Duration(1, 8));
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void ToStringTest2() {
            string expected = "32";
            Note actual = new Note(new Duration(1, 32));
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void EqualityTest() {
            Note note1 = new Note(new Duration(1, 8));
            Note note2 = new Note(new Duration(1, 8));
            Note note3 = new Note(new Duration(1, 4));
            EqualityTests.TestEqualObjects(note1, note2);
            EqualityTests.TestUnequalObjects(note1, note3);
            EqualityTests.TestAgainstNull(note1);
        }
    }
}
