using System;

namespace Musician {
    public class Note : IEquatable<Note> {
        public Duration Duration { get; set; }
        public Note(Duration Duration) {
            this.Duration = Duration;
        }
        #region Equals
        public bool Equals(Note that) {
            if (that == null)
                return false;
            return this.Duration == that.Duration;
        }
        public override bool Equals(object obj) {
            bool result = false;
            if (obj is Note) {
                result = this.Equals(obj as Note);
            }
            return result;
        }
        public override int GetHashCode() {
            return this.Duration.GetHashCode() ^ this.Duration.GetHashCode();
        }
        public static bool operator ==(Note a, Note b) {
            Object aAsObj = a as Object;
            Object bAsObj = b as Object;
            if (aAsObj == null || bAsObj == null) {
                return aAsObj == bAsObj;
            }
            return a.Equals(b);
        }
        public static bool operator !=(Note a, Note b) {
            return !(a == b);
        }
        #endregion
        public static bool TryParse(string? input, out Note result) {
            if (string.IsNullOrWhiteSpace(input)) {
                result = null;
                return false;
            }

            if (!int.TryParse(input, out int denominator)) {
                result = null;
                return false;
            }

            if (denominator <= 0) {
                result = null;
                return false;
            }

            result = new Note(new Duration(1, denominator));
            return true;
        }
        public override string ToString() {
            return Duration.Denominator.ToString();
        }
    }
}
