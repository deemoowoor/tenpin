using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tenpin.Test
{
    public class TenPinScoreTest
    {
        [Fact]
        public void TestSimpleFrames()
        {
            var rolls = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(rolls.Sum(), computer.GetTotalScore());
            Assert.Equal(10, computer.Frames.Count());

            foreach (var frame in computer.Frames)
            {
                Assert.Equal(2, frame.Score);
            }
        }

        [Fact]
        public void TestOneFrame()
        {
            var rolls = new List<int>() { 5, 4 };

            var computer = new TenpinScoreTotal(rolls.ToArray());

            Assert.Equal(rolls.Sum(), computer.GetTotalScore());
            Assert.Equal(2, computer.Frames.Count());
            Assert.Equal(9, computer.Frames[0].Score);
        }
    }
}
