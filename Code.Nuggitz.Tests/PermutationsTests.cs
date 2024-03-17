
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Code.Nuggitz.Permutations;

namespace Code.Nuggitz {

	public class PermutationsTests {

		[Theory]
		[InlineData(45, 10, 2)]
		[InlineData(1326, 52, 2)]
		[InlineData(2598960, 52, 5)]
		public void Calculate_BinomialCoefficient(int expected, int n, int k) =>
			Assert.Equal(expected, BinomialCoefficient(n, k));

		[Theory]
		[InlineData]
		public void Permutations_OfString() =>
			Assert.Equal(
				new[] { "ABC", "ACB", "BAC", "BCA", "CBA", "CAB" },
				GetAllPermutations("ABC")
			);

		[Fact]
		public void Permutations_OfArray() =>
			Assert.Equal(
				new[] {
					new[] {1,2,3 },
					[1,3,2],
					[2,1,3],
					[2,3,1],
					[3,2,1],
					[3,1,2]
				},
				GetAllPermutations(1, 2, 3)
			);

		[Fact]
		public void Permutations_GetAllSubsets() =>
			Assert.Equal(1326, GetAllSubsets(Enumerable.Range(0, 52), 2).Count());
	}

	public class Permutations {

		public static IEnumerable<string> GetAllPermutations(string input) => GetAllPermutations(input.ToCharArray(), 0).Select(a => new string(a.ToArray()));
		public static IEnumerable<T[]> GetAllPermutations<T>(params T[] items) => GetAllPermutations(items, 0);
		public static IEnumerable<T[]> GetAllPermutations<T>(IEnumerable<T> input) => GetAllPermutations(input, 0);

		protected static IEnumerable<T[]> GetAllPermutations<T>(IEnumerable<T> input, int start = 0) {

			var s = start + 1;
			var list = input.ToArray();

			if (s == list.Length)
				yield return list;
			else {
				foreach (var p in GetAllPermutations(list, s))
					yield return p;

				for (var i = s; i < list.Length; i++) {
					list.Swap(start, i);

					foreach (var v in GetAllPermutations(list, s))
						yield return v.ToArray();

					list.Swap(start, i);
				}
			}
		}

		public static IEnumerable<T[]> GetAllSubsets<T>(IEnumerable<T> items, int choose) {

			var count = 0;

			foreach (var item in items) {
				if (choose == 1) {
					yield return new[] { item };
				}
				else {
					foreach (var subset in GetAllSubsets(items.Skip(++count), choose - 1)) {
						yield return new[] { item }.Concat(subset).ToArray();
					}
				}
			}
		}

		public static long BinomialCoefficient(int n, int k) => BinomialCoefficient((uint)n, (uint)k);

		public static long BinomialCoefficient(uint n, uint k) {

			var rtn = 1L;

			for (var i = 1U; i <= k; i++)
				rtn = rtn * (n - (k - i)) / i;

			return rtn;
		}
	}
}