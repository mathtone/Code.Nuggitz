
using System;

namespace Code.Nuggitz {
	
	public class RoundToTests {

		[Theory]
		[InlineData(5,1,5), InlineData(91,10,90), InlineData(77,5,75)]
		public void RoundToTest(decimal number,decimal increment,decimal expected) =>
			Assert.Equal(expected,RoundOff.RoundToNearest(number,increment));

		[Theory]
		[InlineData(5,1,5), InlineData(91,10,100), InlineData(71,5,75)]
		public void RoundUpTest(decimal number,decimal increment,decimal expected) =>
			Assert.Equal(expected,RoundOff.RoundUp(number,increment));

		[Theory]
		[InlineData(5,1,5), InlineData(91,10,90), InlineData(74,5,70)]
		public void RoundDownTest(decimal number,decimal increment,decimal expected) =>
			Assert.Equal(expected,RoundOff.RoundDown(number,increment));
	}

	public static class RoundOff {
		public static int RoundToNearest(decimal number,decimal increment) =>
			(int)(Math.Round(number / increment) * increment);

		public static int RoundUp(decimal number,decimal increment) =>
			(int)(Math.Ceiling(number / increment) * increment);

		public static int RoundDown(decimal number,decimal increment) =>
			(int)(Math.Floor(number / increment) * increment);
	}
}