using System.Collections.Generic;
using System.Linq;

namespace tenpin
{
    public class TenpinScoreTotal
    {
        public const int MaxRolls = 10;

        private readonly List<Frame> _frames;

        public List<Frame> Frames { get { return _frames; } }

        public TenpinScoreTotal(int[] rolls)
        {
            _frames = new List<Frame>();
            FillFrames(rolls);
        }

        private void FillFrames(int[] rolls)
        {
            var current = new Frame(Frame.Null, 1);
            _frames.Add(current);

            for (int i = 0; i < rolls.Count() && current.Index <= MaxRolls; i++)
            {
                if (!current.Roll(rolls[i]) && current.Index < MaxRolls)
                {
                    current = new Frame(current, current.Index + 1);
                    _frames.Add(current);
                }
            }
        }

        public int GetTotalScore()
        {
            return _frames.Last().Score;
        }
    }
}
