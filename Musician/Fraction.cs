using System;

namespace Musician {
    public class Fraction : IEquatable<Fraction> {
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public int Sign { get; }

        public Fraction(int numerator, int denominator) {
            if (denominator == 0) {
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            }
            Numerator = Math.Abs(numerator);
            Denominator = Math.Abs(denominator);
            if (numerator * denominator < 0) {
                Sign = -1;
            } else {
                Sign = 1;
            }
        }
        public Fraction(int number) : this(number, 1) { }

        private static int GetGreatestCommonDivisor(int a, int b) {
            while (b != 0) {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static int GetLeastCommonMultiple(int a, int b) {
            return a * b / GetGreatestCommonDivisor(a, b);
        }

        private static Fraction PerformOperation(Fraction a, Fraction b, Func<int, int, int> operation) {
            int leastCommonMultiple = GetLeastCommonMultiple(a.Denominator, b.Denominator);
            int additionalMultiplierFirst = leastCommonMultiple / a.Denominator;
            int additionalMultiplierSecond = leastCommonMultiple / b.Denominator;
            int operationResult = operation(a.Numerator * additionalMultiplierFirst * a.Sign,
            b.Numerator * additionalMultiplierSecond * b.Sign);
            return new Fraction(operationResult, a.Denominator * additionalMultiplierFirst);
        }

        public static Fraction operator +(Fraction a, Fraction b) {
            return PerformOperation(a, b, (int x, int y) => x + y);
        }
        public static Fraction operator +(Fraction a, int b) {
            return a + new Fraction(b);
        }
        public static Fraction operator +(int a, Fraction b) {
            return b + a;
        }
        public static Fraction operator -(Fraction a, Fraction b) {
            return PerformOperation(a, b, (int x, int y) => x - y);
        }
        public static Fraction operator -(Fraction a, int b) {
            return a - new Fraction(b);
        }
        public static Fraction operator -(int a, Fraction b) {
            return b - a;
        }
        public static Fraction operator *(Fraction a, Fraction b) {
            return new Fraction(a.Numerator * a.Sign * b.Numerator * b.Sign, a.Denominator * b.Denominator);
        }
        public static Fraction operator *(Fraction a, int b) {
            return a * new Fraction(b);
        }
        public static Fraction operator *(int a, Fraction b) {
            return b * a;
        }
        public static Fraction operator /(Fraction a, Fraction b) {
            return a * b.GetReverse();
        }
        public static Fraction operator /(Fraction a, int b) {
            return a / new Fraction(b);
        }
        public static Fraction operator /(int a, Fraction b) {
            return new Fraction(a) / b;
        }
        public static Fraction operator -(Fraction a) {
            return a.GetWithChangedSign();
        }
        public static Fraction operator ++(Fraction a) {
            return a + 1;
        }
        public static Fraction operator --(Fraction a) {
            return a - 1;
        }

        private Fraction GetReverse() {
            return new Fraction(this.Denominator * this.Sign, this.Numerator);
        }
        private Fraction GetWithChangedSign() {
            return new Fraction(-this.Numerator * this.Sign, this.Denominator);
        }

        public bool Equals(Fraction that) {
            if (that == null)
                return false;

            Fraction a = this.Reduce();
            Fraction b = that.Reduce();
            return a.Numerator == b.Numerator &&
            a.Denominator == b.Denominator &&
            a.Sign == b.Sign;
        }
        public override bool Equals(object obj) {
            bool result = false;
            if (obj is Fraction) {
                result = this.Equals(obj as Fraction);
            }
            return result;
        }
        public override int GetHashCode() {
            return this.Sign * (this.Numerator * this.Numerator + this.Denominator * this.Denominator);
        }

        public static bool operator ==(Fraction a, Fraction b) {
            Object aAsObj = a as Object;
            Object bAsObj = b as Object;
            if (aAsObj == null || bAsObj == null) {
                return aAsObj == bAsObj;
            }
            return a.Equals(b);
        }
        public static bool operator ==(Fraction a, int b) {
            return a == new Fraction(b);
        }
        public static bool operator ==(int a, Fraction b) {
            return new Fraction(a) == b;
        }
        public static bool operator !=(Fraction a, Fraction b) {
            return !(a == b);
        }
        public static bool operator !=(Fraction a, int b) {
            return a != new Fraction(b);
        }
        public static bool operator !=(int a, Fraction b) {
            return new Fraction(a) != b;
        }

        private int CompareTo(Fraction that) {
            if (this.Equals(that)) {
                return 0;
            }
            Fraction a = this.Reduce();
            Fraction b = that.Reduce();
            if (a.Numerator * a.Sign * b.Denominator > b.Numerator * b.Sign * a.Denominator) {
                return 1;
            }
            return -1;
        }

        public static bool operator >(Fraction a, Fraction b) {
            return a.CompareTo(b) > 0;
        }
        public static bool operator >(Fraction a, int b) {
            return a > new Fraction(b);
        }
        public static bool operator >(int a, Fraction b) {
            return new Fraction(a) > b;
        }
        public static bool operator <(Fraction a, Fraction b) {
            return a.CompareTo(b) < 0;
        }
        public static bool operator <(Fraction a, int b) {
            return a < new Fraction(b);
        }
        public static bool operator <(int a, Fraction b) {
            return new Fraction(a) < b;
        }
        public static bool operator >=(Fraction a, Fraction b) {
            return a.CompareTo(b) >= 0;
        }
        public static bool operator >=(Fraction a, int b) {
            return a >= new Fraction(b);
        }
        public static bool operator >=(int a, Fraction b) {
            return new Fraction(a) >= b;
        }
        public static bool operator <=(Fraction a, Fraction b) {
            return a.CompareTo(b) <= 0;
        }
        public static bool operator <=(Fraction a, int b) {
            return a <= new Fraction(b);
        }
        public static bool operator <=(int a, Fraction b) {
            return new Fraction(a) <= b;
        }

        public Fraction Reduce() {
            Fraction result = this;
            int greatestCommonDivisor = GetGreatestCommonDivisor(this.Numerator, this.Denominator);
            result.Numerator /= greatestCommonDivisor;
            result.Denominator /= greatestCommonDivisor;
            return result;
        }
        public override string ToString() {
            if (this.Numerator == 0) {
                return "0";
            }
            string result;
            if (this.Sign < 0) {
                result = "-";
            } else {
                result = "";
            }
            if (this.Numerator == this.Denominator) {
                return result + "1";
            }
            if (this.Denominator == 1) {
                return result + this.Numerator;
            }
            return result + this.Numerator + "/" + this.Denominator;
        }
    }
}
