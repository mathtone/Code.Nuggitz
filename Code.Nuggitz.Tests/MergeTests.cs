
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Nuggitz {

	public class MergeTests {

		[Theory, MemberData(nameof(MergeListsCases))]
		public void MergeLists(IEnumerable<int> expected, SortOrder order, params IEnumerable<int>[] input) =>
			Assert.Equal(expected, Merge.Ordered(order, Comparer<int>.Default.Compare, input));

		public static readonly IEnumerable<object[]> MergeListsCases = new[]
		{
			new object[] {
				new[]{1,2,3,4,5,6,7,8,9,10},
				SortOrder.Ascending,
				new IEnumerable<int>[]{
					[1,3,5,7,9],
					[2,4,6,8,10]
				}
			},
			new object[] {
				new[]{1,1,2,3,4,4,5,6,7,8,9,10},
				SortOrder.Ascending,
				new IEnumerable<int>[] {
					[1,3,4,5,7,9],
					[1,2,4,6,8,10]
				}
			},
			new object[] {
				new[]{10,9,8,7,6,5,4,4,3,2,1,1},
				SortOrder.Descending,
				new IEnumerable<int>[] {
					[9,7,5,4,3,1],
					[10,8,6,4,2,1]
				}
			},
			new object[] {
				new[]{1,2,3,4,5,6,7,8,9,10,10,10,10,10},
				SortOrder.Ascending,
				new IEnumerable<int>[] {
					[1,3,5,7,9],
					[2,10],
					[10,10,10,10],
					[4,6,8]
				}
			},
			new object[] {
				new[]{1,1,1,2,3,4,5,6,7,8,9,10},
				SortOrder.Ascending,
				new IEnumerable<int>[] {
					[1,3,5,7,9],
					[2,10],
					[1],
					[],
					[4,6,8],
					[1]
				}
			}
		};


		public enum SortOrder {
			Ascending, Descending
		}

		public static class Merge {

			/// <summary>
			/// Performs an ordered merge on n input lists using supplied comparison.
			/// </summary>
			public static IEnumerable<T> Ordered<T>(SortOrder order, Comparison<T> comparison, params IEnumerable<T>[] input) {

				//Build merges in slices of 2, layering until less than 3 remain.
				while (input.Length > 2)
					input = Slice(input, 2)
						.Select(s => Ordered(order, comparison, s.ToArray()))
						.ToArray();

				if (input.Length > 0) {

					var i1 = input[0].GetEnumerator();
					var itemsExist = i1.MoveNext();
					var reverse = order == SortOrder.Ascending ? 1 : -1; //inverts comparison result for descending order.

					if (input.Length > 1) {
						foreach (var i2 in input[1]) {
							while (itemsExist && comparison(i1.Current, i2) * reverse <= 0) {
								yield return i1.Current;
								itemsExist = i1.MoveNext();
							}
							yield return i2;
						}
					}

					while (itemsExist) {
						while (itemsExist) {
							yield return i1.Current;
							itemsExist = i1.MoveNext();
						}
					}
				}
			}

			public static IEnumerable<IEnumerable<T>> Slice<T>(IList<T> items, int size) {
				int i = 0, max = items.Count - 1;
				while (i < items.Count) {
					yield return Slice(items, i, Math.Min(i + (size - 1), max));
					i += size;
				}
			}

			public static IEnumerable<T> Slice<T>(IList<T> items, int start, int end) {
				for (var i = start; i <= end; i++)
					yield return items[i];
			}
		}
	}
}
