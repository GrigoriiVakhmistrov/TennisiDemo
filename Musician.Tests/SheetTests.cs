using Musician.Tests.Utils;
using System.Collections.Generic;
using Xunit;

namespace Musician.Tests {
    public class SheetTests {
        [Fact]
        public void TryParseCorrectDataTest1() {
            string input = "3/4 | 2 4 | 4 4 4 | 8 8 2";

            Duration duration = new Duration(3, 4);
            IEnumerable<Beat> beats = new List<Beat>() {
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 2)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 2))
                })
            };
            Sheet expected = new Sheet(beats, duration);

            bool actualBool = Sheet.TryParse(input, out Sheet actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseCorrectDataTest2() {
            string input = "   3 /   4  | 2 \n\t4|4 \r4 4| 8 8 2 |";

            Duration duration = new Duration(3, 4);
            IEnumerable<Beat> beats = new List<Beat>() {
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 2)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 2))
                })
            };
            Sheet expected = new Sheet(beats, duration);

            bool actualBool = Sheet.TryParse(input, out Sheet actual);
            Assert.True(actualBool);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TryParseEmptyDataTest() {
            string input = "";

            Duration duration = new Duration(3, 4);
            IEnumerable<Beat> beats = new List<Beat>() {
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 2)),
                    new Note(new Duration(1, 4))
                })
            };
            Sheet expected = new Sheet(beats, duration);

            bool actualBool = Sheet.TryParse(input, out Sheet actual);
            Assert.False(actualBool);
            Assert.NotEqual(expected, actual);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseNullDataTest() {
            string input = null;

            Duration duration = new Duration(3, 4);
            IEnumerable<Beat> beats = new List<Beat>() {
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 2)),
                    new Note(new Duration(1, 4))
                })
            };
            Sheet expected = new Sheet(beats, duration);

            bool actualBool = Sheet.TryParse(input, out Sheet actual);
            Assert.False(actualBool);
            Assert.NotEqual(expected, actual);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest1() {
            string input = "-3/-4 | 2 4 | 4 4 4 | 8 8 2";

            Duration duration = new Duration(3, 4);
            IEnumerable<Beat> beats = new List<Beat>() {
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 2)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 2))
                })
            };
            Sheet expected = new Sheet(beats, duration);

            bool actualBool = Sheet.TryParse(input, out Sheet actual);
            Assert.False(actualBool);
            Assert.NotEqual(expected, actual);
            Assert.Null(actual);
        }
        [Fact]
        public void TryParseIncorrectDataTest2() {
            string input = "3 / 4 | 2a b4 | 4 4 4 | 8 8 2";

            Duration duration = new Duration(3, 4);
            IEnumerable<Beat> beats = new List<Beat>() {
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 2)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4)),
                    new Note(new Duration(1, 4))
                }),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 2))
                })
            };
            Sheet expected = new Sheet(beats, duration);

            bool actualBool = Sheet.TryParse(input, out Sheet actual);
            Assert.False(actualBool);
            Assert.NotEqual(expected, actual);
            Assert.Null(actual);
        }
        [Fact]
        public void ToStringTest() {
            string expected = "8 8 4 | 8 8 4 | 8 8 4";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(2, 4);
            List<Beat> beats = new List<Beat>() {
                new Beat(duration, notes),
                new Beat(duration, notes),
                new Beat(duration, notes)
            };
            Sheet actual = new Sheet(beats, duration);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void ToStringGreaterErrorTest() {
            string expected = "8 8 4 | Общая длительность нот в такте на 1/4 больше размера такта | 8 8 4";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(2, 4);
            List<Beat> beats = new List<Beat>() {
                new Beat(duration, notes),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 2))
                }),
                new Beat(duration, notes)
            };
            Sheet actual = new Sheet(beats, duration);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void ToStringLessErrorTest() {
            string expected = "8 8 4 | Общая длительность нот в такте на 1/4 меньше размера такта | 8 8 4";
            List<Note> notes = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            Duration duration = new Duration(2, 4);
            List<Beat> beats = new List<Beat>() {
                new Beat(duration, notes),
                new Beat(duration, new List<Note>() {
                    new Note(new Duration(1, 8)),
                    new Note(new Duration(1, 8))
                }),
                new Beat(duration, notes)
            };
            Sheet actual = new Sheet(beats, duration);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void EqualityTest() {
            Duration duration = new Duration(2, 4);
            IEnumerable<Note> notes1 = new List<Note>() {
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 8)),
                new Note(new Duration(1, 4))
            };
            IEnumerable<Beat> beats1 = new List<Beat>() {
                new Beat(duration, notes1)
            };
            IEnumerable<Note> notes2 = new List<Note>() {
                new Note(new Duration(1, 4)),
                new Note(new Duration(1, 4))
            };
            IEnumerable<Beat> beats2 = new List<Beat>() {
                new Beat(duration, notes2)
            };
            Sheet sheet1 = new Sheet(beats1, duration);
            Sheet sheet2 = new Sheet(beats1, duration);
            Sheet sheet3 = new Sheet(beats2, duration);
            EqualityTests.TestEqualObjects(sheet1, sheet2);
            EqualityTests.TestUnequalObjects(sheet1, sheet3);
            EqualityTests.TestAgainstNull(sheet1);
        }
    }
}
