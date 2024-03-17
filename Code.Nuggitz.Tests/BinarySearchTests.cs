
namespace Code.Nuggitz {

	public class BinarySearchTests {

	}

	public class BinarySearch {
		public static int Iterative<T>(IList<T> items,T key,IComparer<T> comparer) {
			var min = 0;
			var max = items.Count - 1;

			while(min <= max) {
				var mid = (min + max) / 2;
				var c = comparer.Compare(key,items[mid]);

				if(c == 0)
					return ++mid;
				else if(c < 0)
					max = mid - 1;
				else
					min = mid + 1;
			}
			return -1;
		}

		public static int Recursive<T>(IList<T> items,T key,int min,int max,IComparer<T> comparer) {
			if(min > max)
				return -1;
			else {
				var mid = (min + max) / 2;
				var c = comparer.Compare(key,items[mid]);

				if(c == 0)
					return ++mid;
				else if(c < 0)
					return Recursive(items,key,min,mid - 1,comparer);
				else
					return Recursive(items,key,mid + 1,max,comparer);
			}
		}
	}
}