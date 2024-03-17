
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Nuggitz {

	public class BowlingScoreTests {
		
		[Theory]
		[InlineData(10, new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 300)]
		[InlineData(10, new[] { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9 }, 190)]
		[InlineData(10, new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 20)]
		[InlineData(10, new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 1 }, 30)]
		[InlineData(10, new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 1 }, 39)]
		public void ScoreGame(int frames, int[] rolls, int expected) {
			var score = new BowlingScore(frames);
			foreach (var r in rolls) {
				score.Roll(r);
			}
			Assert.True(score.IsComplete);
			Assert.Equal(expected, score.TotalScore);
		}
	}

	public class BowlingScore {

		protected int CurrentFrameIdx { get; set; } = 0;
		public IList<Frame> Frames { get; protected set; }

		public Frame CurrentFrame => Frames[CurrentFrameIdx];
		public bool IsComplete => CurrentFrameIdx == -1;
		public int TotalScore => Frames.Sum(f => f.FrameScore ?? 0);

		public BowlingScore(int frames = 10) {
			Frames = Enumerable.Range(1, frames)
				.Select(f => new Frame(f))
				.ToArray();
		}

		public void Roll(int pins) {

			foreach (var f in Frames.Where(f => f.RollsToAdd > 0)) {
				f.ScoreModifier += pins;
				f.RollsToAdd--;
			}

			CurrentFrame.Rolls.Add(pins);
			if (CurrentFrameIdx == Frames.Count - 1) {
				if (CurrentFrame.Rolls[0] == 10 || CurrentFrame.Rolls.Take(2).Sum() == 10) {
					if (CurrentFrame.Rolls.Count == 3)
						CurrentFrameIdx = -1;
				}
				else if (CurrentFrame.Rolls.Count == 2) {
					CurrentFrameIdx = -1;
				}
			}
			else {
				if (CurrentFrame.IsStrike) {
					CurrentFrame.RollsToAdd = 2;
					CurrentFrameIdx++;
				}
				else if (CurrentFrame.IsSpare) {
					CurrentFrame.RollsToAdd = 1;
					CurrentFrameIdx++;
				}
				else if (CurrentFrame.Rolls.Count == 2) {
					CurrentFrameIdx++;
				}
			}
		}
	}

	public class Frame {

		public int Number { get; set; }
		public List<int> Rolls { get; set; }
		public int RollsToAdd { get; set; }
		public int ScoreModifier { get; set; }
		public bool IsStrike => Rolls[0] == 10;
		public bool IsSpare => Rolls.Count == 2 && Rolls.Sum() == 10;

		public int? FrameScore => RollsToAdd > 0 ?
			default :
			Rolls.Sum() + ScoreModifier;

		public Frame(int number) {
			this.Number = number;
			this.Rolls = new List<int>();
		}
	}
}