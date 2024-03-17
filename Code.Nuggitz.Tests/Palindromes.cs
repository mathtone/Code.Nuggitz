
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Nuggitz {
	
	public class Palindromes {

		[InlineData("abcd","dcbabcd")]
		public void TestMethod(string input,string result) =>
			Assert.Equal(ShortestPalindrome(input),result);
		
		public string ShortestPalindrome(string s) {
			var i = 0;
			var j = s.Length - 1;

			while(j >= 0) {
				if(s[i] == s[j]) {
					i++;
				}
				j--;
			}

			if(i == s.Length)
				return s;

			var sfx = s.Substring(i);
			var pfx = new string(sfx.ToCharArray().Reverse().ToArray());
			var mid = ShortestPalindrome(s.Substring(0,i));
			return pfx + mid + sfx;
		}
	}
}