using Musician.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Musician {
    public class Beat : IEquatable<Beat> {
        private readonly IEnumerableComparer<Note> comparer = new IEnumerableComparer<Note>();
        string error;
        Duration Duration { get; set; }
        IEnumerable<Note> Notes { get; set; }
        public Beat(Duration duration, IEnumerable<Note> notes) {
            this.Duration = duration;
            this.Notes = notes;
        }
        #region Equals
        public bool Equals(Beat that) {
            if (that == null)
                return false;

            if (this.Duration != that.Duration)
                return false;

            if (!comparer.Equals(this.Notes, that.Notes))
                return false;

            return true;
        }
        public override bool Equals(object obj) {
            bool result = false;
            if (obj is Beat) {
                result = this.Equals(obj as Beat);
            }
            return result;
        }
        public override int GetHashCode() {
            return this.Duration.GetHashCode() ^ comparer.GetHashCode(this.Notes);
        }
        public static bool operator ==(Beat a, Beat b) {
            Object aAsObj = a as Object;
            Object bAsObj = b as Object;
            if (aAsObj == null || bAsObj == null) {
                return aAsObj == bAsObj;
            }
            return a.Equals(b);
        }
        public static bool operator !=(Beat a, Beat b) {
            return !(a == b);
        }
        #endregion
        public bool IsCorrect() {
            Fraction result = new Fraction(0);
            foreach (var note in Notes)
                result += note.Duration;

            if (Duration > result)
                error = String.Format("Общая длительность нот в такте на {0} меньше размера такта", Duration - result);

            if (Duration < result)
                error = String.Format("Общая длительность нот в такте на {0} больше размера такта", result - Duration);

            return Duration == result;
        }
        public static bool TryParse(string? input, Duration duration, out Beat result) {
            if (string.IsNullOrWhiteSpace(input)) {
                result = null;
                return false;
            }

            string normalizedInput = input.Replace('\n', ' ')
                .Replace('\t', ' ')
                .Replace('\r', ' ');

            List<Note> notes = new List<Note>();
            foreach (string noteString in normalizedInput.Split(" ", StringSplitOptions.RemoveEmptyEntries)) {
                if (Note.TryParse(noteString, out Note note))
                    notes.Add(note);
                else {
                    result = null;
                    return false;
                }
            }
            result = new Beat(duration, notes);
            return true;
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            if (IsCorrect()) {
                List<Note> notes = Notes.ToList();
                for (int i = 0; i < notes.Count; i++) {
                    sb.Append(notes[i]);
                    if (i < notes.Count - 1)
                        sb.Append(' ');
                }
            } else {
                sb.Append(error);
                error = null;
            }

            return sb.ToString();
        }
    }
}
