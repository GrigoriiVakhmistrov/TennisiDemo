using Musician.Tests.Utils;
using System.Collections.Generic;
using Xunit;

namespace Musician.Tests {
    public class BeatTests {
        [Fact]
        public void TryParseCorrectDataTest1() {
            string input = "2 4";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 2)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(3, 4);
            Beat expected = new Beat(duration, notes);

            bool actualBool = Beat.TryParse(input, duration, out Beat actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseCorrectDataTest2() {
            string input = "  8 \n\t\t   8 \r\n   4\t";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(2, 4);
            Beat expected = new Beat(duration, notes);

            bool actualBool = Beat.TryParse(input, duration, out Beat actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseEmptyDataTest() {
            string input = "";
            Duration duration = new Duration(2, 4);
            bool actualBool = Beat.TryParse(input, duration, out Beat actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseNullDataTest() {
            string input = null;
            Duration duration = new Duration(2, 4);
            bool actualBool = Beat.TryParse(input, duration, out Beat actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest1() {
            string input = "8a 8b c,4";
            Duration duration = new Duration(2, 4);
            bool actualBool = Beat.TryParse(input, duration, out Beat actual);
            Assert.False(actualBool);
            Assert.Null(actual);
        }
        [Fact]
        public void IsCorrectCorrectDataTest() {
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(2, 4);
            Beat beat = new Beat(duration, notes);

            bool actualBool = beat.IsCorrect();
            Assert.True(actualBool);
        }
        [Fact]
        public void IsCorrectIncorrectDataTest() {
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(3, 4);
            Beat beat = new Beat(duration, notes);

            bool actualBool = beat.IsCorrect();
            Assert.False(actualBool);
        }
        [Fact]
        public void ToStringTest() {
            string expected = "8 8 4";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(2, 4);
            Beat actual = new Beat(duration, notes);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void ToStringGreaterErrorTest() {
            string expected = "Общая длительность нот в такте на 1/4 больше размера такта";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4)),
                new Note(new Duration(1, 2))
            };
            Duration duration = new Duration(3, 4);
            Beat actual = new Beat(duration, notes);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void ToStringLessErrorTest() {
            string expected = "Общая длительность нот в такте на 1/4 меньше размера такта";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(3, 4);
            Beat actual = new Beat(duration, notes);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void EqualityTest() {
            List<Note> notes1 = new List<Note>() {
                new Note(new Duration(1, 4)),
                new Note(new Duration(1, 4)),
                new Note(new Duration(1, 4))
            };
            Duration duration1 = new Duration(3, 4);
            Beat beat1 = new Beat(duration1, notes1);
            Beat beat2 = new Beat(duration1, notes1);
            List<Note> notes3 = new List<Note>() {
                new Note(new Duration(1, 4)),
                new Note(new Duration(1, 4)),
                new Note(new Duration(2, 4))
            };
            Duration duration3 = new Duration(4, 4);
            Beat beat3 = new Beat(duration3, notes3);

            EqualityTests.TestEqualObjects(beat1, beat2);
            EqualityTests.TestUnequalObjects(beat1, beat3);
            EqualityTests.TestAgainstNull(beat1);
        }
    }
}
