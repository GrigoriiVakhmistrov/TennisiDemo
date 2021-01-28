using System.Collections.Generic;
using System.Linq;

namespace Musician.Utils {
    public class IEnumerableComparer<T> : IEqualityComparer<IEnumerable<T>> {
		public int GetHashCode(IEnumerable<T> collection) {
			if (collection != null) {
				unchecked {
					int hash = 17;

					foreach (var item in collection) {
						hash = hash * 23 + ((item != null) ? item.GetHashCode() : 0);
					}

					return hash;
				}
			}

			return 0;
		}

		public bool Equals(IEnumerable<T> firstCollection, IEnumerable<T> secondCollection) {
			if (object.ReferenceEquals(firstCollection, secondCollection)) {
				return true;
			}

			List<T> firstArray = firstCollection.ToList();
			List<T> secondArray = secondCollection.ToList();

			if (firstArray != null && secondArray != null && (firstArray.Count == secondArray.Count)) {
				for (int i = 0; i < firstArray.Count; i++) {
					if (!object.Equals(firstArray[i], secondArray[i])) {
						return false;
					}
				}

				return true;
			}

			return false;
		}
	}
}
