
using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using TXX = System.Numerics.BigInteger;

namespace Code.Nuggitz {
	public class BernoulliNumbersTests {
		
		//[Theory]
		//[InlineData(3)]
		public void BernoulliNumbers(int nums) {
			

			Console.WriteLine("Bernoulli numbers:");
			for (int i = 0; i <= nums; i++) {
				Console.WriteLine($"B_{i}: {BernoulliNumber(i)}");
			}
		}

		public static BigInteger Factorial(int n) {
			var result = (BigInteger)1;
			for (int i = 1; i <= n; i++)
				result *= i;
			return result;
		}

		public static BigInteger BinomialCoefficient(int n, int k) =>
			Factorial(n) / (Factorial(k) * Factorial(n - k));

		public static BigRational BernoulliNumber(int n) {
			if (n < 0) {
				throw new ArgumentException("n must be non-negative.");
			}

			if (n == 0) {
				return new BigRational(1);
			}

			if (n % 2 != 0 && n != 1) {
				return new BigRational(0);
			}

			var result = new BigRational(0);

			for (int k = 0; k <= n; k++) {
				var binom = BinomialCoefficient(n + 1, k);
				var term = new BigRational(binom * BernoulliNumber(k).Numerator, (n + 1 - k) * BernoulliNumber(k).Denominator);
				result += term;
			}

			result *= new BigRational(-1, n + 1);
			return result;
		}
	}


	public struct BigRational {
		public BigInteger Numerator { get; private set; }
		public BigInteger Denominator { get; private set; }

		public BigRational(BigInteger numerator) : this(numerator, BigInteger.One) { }

		public BigRational(BigInteger numerator, BigInteger denominator) {

			if (denominator == 0)
				throw new ArgumentException("Denominator cannot be zero.");

			var gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
			Numerator = numerator / gcd;
			Denominator = denominator / gcd;
		}

		public static BigRational operator +(BigRational a, BigRational b) {
			var numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
			var denominator = a.Denominator * b.Denominator;
			return new BigRational(numerator, denominator);
		}

		public static BigRational operator *(BigRational a, BigRational b) =>
			new BigRational(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

		public override string ToString() {
			if (Denominator == 1) {
				return Numerator.ToString();
			}
			return $"{Numerator}/{Denominator}";
		}
	}
}