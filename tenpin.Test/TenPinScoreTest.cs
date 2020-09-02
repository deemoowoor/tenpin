using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tenpin.Test
{
    public class TenPinScoreTest
    {
        [Fact]
        public void TestSimpleFramesShouldAccumulateScore()
        {
            var rolls = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(rolls.Sum(), computer.GetTotalScore());
            Assert.Equal(10, computer.Frames.Count());

            Assert.Equal(2, computer.Frames[0].Score);
            Assert.Equal(4, computer.Frames[1].Score);
            Assert.Equal(6, computer.Frames[2].Score);
            Assert.Equal(8, computer.Frames[3].Score);
            Assert.Equal(10, computer.Frames[4].Score);
        }

        [Fact]
        public void TestSeveralFramesShouldAccumulateScore()
        {
            var rolls = new List<int>() { 1, 1, 1, 1 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(3, computer.Frames.Count());
            
            Assert.Equal(2, computer.Frames[0].Score);
            Assert.Equal(4, computer.Frames[1].Score);
            Assert.Equal(4, computer.Frames[2].Score);
            Assert.Equal(rolls.Sum(), computer.GetTotalScore());
        }

        [Fact]
        public void TestOneFrameShouldAccumulateScore()
        {
            var rolls = new List<int>() { 5, 4 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(2, computer.Frames.Count());
            Assert.Equal(9, computer.Frames[0].Score);
            Assert.Equal(rolls.Sum(), computer.GetTotalScore());
        }

        [Fact]
        public void TestSpareWithZeroShouldAccumulateScore()
        {
            var rolls = new List<int>() { 1, 9, 0, 0 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(3, computer.Frames.Count());

            Assert.Equal(10, computer.Frames[0].Score);
            Assert.Equal(10, computer.Frames[1].Score);
            Assert.Equal(10, computer.Frames[2].Score);
            Assert.Equal(10, computer.GetTotalScore());
        }

        [Fact]
        public void TestSpareShouldAddSubsequentRollTwice()
        {
            var rolls = new List<int>() { 1, 9, 1, 1 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(3, computer.Frames.Count());
            Assert.Equal(10, computer.Frames[0].Score);
            Assert.Equal(13, computer.Frames[1].Score);
            Assert.Equal(13, computer.Frames[2].Score);

            Assert.Equal(13, computer.GetTotalScore());

        }

        [Fact]
        public void TestStrikeWithZeroShouldAccumulateScore()
        {
            var rolls = new List<int>() { 10, 0, 0 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(10, computer.Frames[0].Score);
            Assert.Equal(10, computer.Frames[1].Score);
            Assert.Equal(10, computer.Frames[2].Score);
            Assert.Equal(10, computer.GetTotalScore());
        }

        [Fact]
        public void TestStrikeShouldAddSubsequentTwoRolls()
        {
            var rolls = new List<int>() { 10, 1, 1 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(10, computer.Frames[0].Score);
            Assert.Equal(14, computer.Frames[1].Score);
            Assert.Equal(14, computer.Frames[2].Score);
            Assert.Equal(14, computer.GetTotalScore());
        }

        [Fact]
        public void TestFreeformCase()
        {
            var rolls = new List<int>() { 2, 3, 5, 4, 9, 1, 2, 5, 3, 2, 4, 2, 3, 3, 4, 6, 10, 3, 2 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            var expectedScores = new List<int> { 5, 14, 24, 33, 38, 44, 50, 60, 80, 90 };

            Assert.Equal(expectedScores[0], computer.Frames[0].Score);
            Assert.Equal(expectedScores[1], computer.Frames[1].Score);
            Assert.Equal(expectedScores[2], computer.Frames[2].Score);
            Assert.Equal(expectedScores[3], computer.Frames[3].Score);
            Assert.Equal(expectedScores[4], computer.Frames[4].Score);
            Assert.Equal(expectedScores[5], computer.Frames[5].Score);
            Assert.Equal(expectedScores[6], computer.Frames[6].Score);
            Assert.Equal(expectedScores[7], computer.Frames[7].Score);
            Assert.Equal(expectedScores[8], computer.Frames[8].Score);
            Assert.Equal(expectedScores[9], computer.Frames[9].Score);

            Assert.Equal(90, computer.GetTotalScore());
        }

        [Fact]
        public void TestLastTwoStrikesShouldAddUp()
        {
            var rolls = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 10 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            var expectedScores = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 10, 30 };

            Assert.Equal(expectedScores[0], computer.Frames[0].Score);
            Assert.Equal(expectedScores[1], computer.Frames[1].Score);
            Assert.Equal(expectedScores[2], computer.Frames[2].Score);
            Assert.Equal(expectedScores[3], computer.Frames[3].Score);
            Assert.Equal(expectedScores[4], computer.Frames[4].Score);
            Assert.Equal(expectedScores[5], computer.Frames[5].Score);
            Assert.Equal(expectedScores[6], computer.Frames[6].Score);
            Assert.Equal(expectedScores[7], computer.Frames[7].Score);
            Assert.Equal(expectedScores[8], computer.Frames[8].Score);
            Assert.Equal(expectedScores[9], computer.Frames[9].Score);

            Assert.Equal(30, computer.GetTotalScore());
        }
    }
}
