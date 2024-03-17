
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Code.Nuggitz {
	
	public class PrimeNumbersTests {
		[Fact]
		public void GetSequence() =>
			Assert.Equal(new BigInteger[] { 2,3,5,7,11,13,17,19,23,29 },PrimeNumbers.Sequence.Take(10));
	}

	public static class PrimeNumbers {

		public static IEnumerable<BigInteger> Sequence {
			get {
				var rtn = new BigInteger(2);
				var primes = new List<BigInteger>();

				while(true) {

					var i = 0;
					var looksPrime = true;

					while(looksPrime && i < primes.Count) {
						looksPrime = (rtn % primes[i++] != 0);
					}

					//If it still looks prime, it's prime my goodman.
					if(looksPrime) {
						primes.Add(rtn);
						yield return rtn;
					}
					rtn += 1;
				}
			}
		}
	}
}