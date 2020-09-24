using System.Collections.Generic;
using System.Linq;

namespace tenpin
{
    public class TenpinScoreTotal
    {
        public const int MaxRolls = 10;

        public List<Frame> Frames { get; }

        public TenpinScoreTotal(IEnumerable<int> rolls)
        {
            Frames = new List<Frame>();
            FillFrames(rolls);
        }

        private void FillFrames(IEnumerable<int> rolls)
        {
            var current = new Frame(null, 1);
            Frames.Add(current);

            foreach (var roll in rolls)
            {
                if (!current.Roll(roll) && current.Index < MaxRolls)
                {
                    current = new Frame(current, current.Index + 1);
                    Frames.Add(current);
                }
            }
        }

        public int GetTotalScore()
        {
            return Frames.Sum(frame => frame.Score);
        }
    }
}
