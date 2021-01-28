using System;

namespace Musician {
    public class Duration : Fraction, IEquatable<Duration> {
        public Duration(int numerator, int denominator) : base(numerator, denominator) {
            if (numerator <= 0)
                throw new ArgumentOutOfRangeException("A numerator of duration must be positive");
            if (denominator <= 0)
                throw new ArgumentOutOfRangeException("A denominator of duration must be positive");
        }
        #region Equals
        public bool Equals(Duration that) {
            if (that == null)
                return false;
            Duration a = (Duration)this.Reduce();
            Duration b = (Duration)that.Reduce();
            return a.Numerator == b.Numerator &&
            a.Denominator == b.Denominator &&
            a.Sign == b.Sign;
        }
        public override bool Equals(object obj) {
            bool result = false;
            if (obj is Duration) {
                result = this.Equals(obj as Duration);
            }
            return result;
        }
        public override int GetHashCode() {
            return this.Sign * (this.Numerator * this.Numerator + this.Denominator * this.Denominator);
        }
        public static bool operator ==(Duration a, Duration b) {
            Object aAsObj = a as Object;
            Object bAsObj = b as Object;
            if (aAsObj == null || bAsObj == null) {
                return aAsObj == bAsObj;
            }
            return a.Equals(b);
        }
        public static bool operator !=(Duration a, Duration b) {
            return !(a == b);
        }
        #endregion
        public static bool TryParse(string? input, out Duration result) {
            if (string.IsNullOrWhiteSpace(input)) {
                result = null;
                return false;
            }

            string[] ints = input.Split("/");

            if (ints.Length != 2) {
                result = null;
                return false;
            }

            if (!int.TryParse(ints[0], out int numerator)) {
                result = null;
                return false;
            }

            if (numerator <= 0) {
                result = null;
                return false;
            }

            if (!int.TryParse(ints[1], out int denominator)) {
                result = null;
                return false;
            }

            if (denominator <= 0) {
                result = null;
                return false;
            }

            result = new Duration(numerator, denominator);
            return true;
        }
    }
}
