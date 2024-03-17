
using System;

namespace Code.Nuggitz {

	public class GetMaxProfitsTests {

		[Theory]
		[InlineData(new[] { 10,22,5,75,65,80 },null,97)]
		[InlineData(new[] { 10,22,5,75,65,80 },2,87)]
		[InlineData(new[] { 10,22,5,75,65,80 },1,75)]
		[InlineData(new[] { 1,1,1,1 },null,0)]
		[InlineData(new[] { 1,1,3,1 },null,2)]
		[InlineData(new[] { 1,2,3,4 },null,3)]
		[InlineData(new[] { 1,3,13,0 },1,12)]
		public void GetMaxProfits(int[] prices,int? max,int expected) =>
			Assert.Equal(expected,GetMaxProfit(prices,prices.Length,max ?? prices.Length));

		//static int GetMaxProfit(int[] prices) =>
		//	GetMaxProfit(prices,prices.Length,prices.Length);

		static int GetMaxProfit(int[] price,int n,int k) {
			var profit = new int[k + 1,n + 1];

			for(var i = 0; i <= k; i++)
				profit[i,0] = 0;

			for(var j = 0; j <= n; j++)
				profit[0,j] = 0;

			for(var i = 1; i <= k; i++) {
				for(var j = 1; j < n; j++) {
					var maxProfit = 0;

					for(var m = 0; m < j; m++)
						maxProfit = Math.Max(maxProfit,price[j] - price[m] + profit[i - 1,m]);

					profit[i,j] = Math.Max(profit[i,j - 1],maxProfit);
				}
			}

			return profit[k,n - 1];
		}
	}
}