
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Code.Nuggitz {
	
	public class StairWalkTests {



		[Fact]
		public void GetFibonacciSequence() => Assert.Equal(new BigInteger[] { 0, 1, 1, 2, 3, 5, 8 }, StairWalk.FibonacciSequence.Take(7));

		[Fact]
		public void GetFibonacciSequence2() => Assert.Equal(new BigInteger[] { 0, 1, 1, 2, 3, 5, 8 }, Fibonacci.Sequence.Take(7));

		[Fact]
		public void HowMainFibInts() {
			var x = 0;
			try {
				foreach (var i in StairWalk.FibonacciSequence.Take(100)) {
					_ = Convert.ToInt32(i.ToString());
					++x;
					Console.WriteLine(i);
				}
			}
			catch(System.OverflowException) {}
			;

			Assert.Equal(47, x);
		}


		[Fact]
		public void HowManyFibLongs() {
			var x = 0L;
			try {
				foreach (var i in StairWalk.FibonacciSequence.Take(100)) {
					_ = Convert.ToInt64(i.ToString());
					++x;
					Console.WriteLine(i);
				}
			}
			catch (System.OverflowException) { }
			;

			Assert.Equal(93, x);
		}

		[Theory]
		[InlineData(1,2,1)]
		[InlineData(2,2,2)]
		[InlineData(5,2,4)]
		[InlineData(4,3,3)]
		[InlineData(7,3,4)]
		public void WalkStairs(int expected,int maxSteps,int stairs) => Assert.Equal((BigInteger)expected,StairWalk.GetWalksToTop(stairs,maxSteps));
	}

	public static class Fibonacci {
		public static IEnumerable<BigInteger> Sequence {
			get {

				BigInteger a, b, c;
				
				yield return a = 0;
				yield return b = 1;

				while (true) {
					c = a + b;
					a = b;
					b = c;
					yield return b;
				}
			}
		}
	}

	public static class StairWalk {

		public static BigInteger GetWalksToTop(int height,int maxSteps) =>
			Walk(maxSteps).ElementAt(height + 1);

		public static IEnumerable<BigInteger> FibonacciSequence =>
			Walk(2);

		static IEnumerable<BigInteger> Walk(int maxSteps) {

			var r = new BigInteger[maxSteps + 1];
			yield return r[0] = 0;
			yield return r[1] = 1;

			for (var i = 2; i < maxSteps; i++)
				yield return r[i] = Sum(r[0..i]);//.Take(i)); ;

			while(true) {
				r[maxSteps] = 0;
				for(var i = 0U; i < maxSteps; i++) {
					r[maxSteps] += r[i];
					r[i] = r[i + 1];
				}
				yield return r[maxSteps];
			}
		}

		static BigInteger Sum(IEnumerable<BigInteger> items) {
			var rtn = new BigInteger();
			foreach(var item in items) {
				rtn += item;
			}
			return rtn;
		}
	}
}