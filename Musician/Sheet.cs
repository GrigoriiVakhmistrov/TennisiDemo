using Musician.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Musician {
    public class Sheet : IEquatable<Sheet> {
        private readonly IEnumerableComparer<Beat> comparer = new IEnumerableComparer<Beat>();
        Duration BeatDuration { get; }
        public IEnumerable<Beat> Beats { get; set; }
        public Sheet(IEnumerable<Beat> beats, Duration duration) {
            Beats = beats;
            BeatDuration = duration;
        }
        #region Equals
        public bool Equals(Sheet that) {
            if (that == null)
                return false;

            if (this.BeatDuration != that.BeatDuration)
                return false;

            if (!comparer.Equals(this.Beats, that.Beats))
                return false;

            return true;
        }
        public override bool Equals(object obj) {
            bool result = false;
            if (obj is Sheet) {
                result = this.Equals(obj as Sheet);
            }
            return result;
        }
        public override int GetHashCode() {
            return this.BeatDuration.GetHashCode() ^ comparer.GetHashCode(this.Beats);
        }
        public static bool operator ==(Sheet a, Sheet b) {
            Object aAsObj = a as Object;
            Object bAsObj = b as Object;
            if (aAsObj == null || bAsObj == null) {
                return aAsObj == bAsObj;
            }
            return a.Equals(b);
        }
        public static bool operator !=(Sheet a, Sheet b) {
            return !(a == b);
        }
        #endregion
        public static bool TryParse(string? input, out Sheet result) {
            if (string.IsNullOrWhiteSpace(input)) {
                result = null;
                return false;
            }
            string[] splitResult = input.Split("|", StringSplitOptions.RemoveEmptyEntries);
            string[] beatStrings = splitResult.Skip(1).ToArray();

            if (!Duration.TryParse(splitResult[0], out Duration duration)) {
                result = null;
                return false;
            }

            List<Beat> beats = new List<Beat>();
            foreach (string beatString in beatStrings) {
                if (Beat.TryParse(beatString, duration, out Beat beat))
                    beats.Add(beat);
                else {
                    result = null;
                    return false;
                }
            }
            result = new Sheet(beats, duration);
            return true;
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            List<Beat> beats = Beats.ToList();
            for (int i = 0; i < beats.Count; i++) {
                sb.Append(beats[i]);
                if (i < beats.Count - 1)
                    sb.Append(" | ");
            }
            return sb.ToString();
        }
    }
}
