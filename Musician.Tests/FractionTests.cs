using Musician.Tests.Utils;
using Xunit;

namespace Musician.Tests {
    public class FractionTests {
        [Fact]
        public void ToStringTest1() {
            string expected = "1/8";
            Fraction actual = new Fraction(1, 8);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void ToStringTest2() {
            string expected = "1/32";
            Fraction actual = new Fraction(1, 32);
            Assert.True(actual.ToString().Equals(expected));
        }
        [Fact]
        public void EqualityTest() {
            Fraction fraction1 = new Fraction(2, 4);
            Fraction fraction2 = new Fraction(2, 4);
            Fraction fraction3 = new Fraction(3, 4);
            EqualityTests.TestEqualObjects(fraction1, fraction2);
            EqualityTests.TestUnequalObjects(fraction1, fraction3);
            EqualityTests.TestAgainstNull(fraction1);
        }
    }
}
