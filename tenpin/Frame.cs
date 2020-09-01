using System.Collections.Generic;
using System.Linq;

namespace tenpin
{
    public class Frame
    {
        public const int SPARE = 10;
        public const int MISS = 0;

        public int Index { get; }

        public List<int> Rolls { get; }

        public int Score { get; private set; }

        public Frame Previous { get; }

        public static Frame Null = new Frame();

        protected Frame()
        {
            Score = 0;
            Previous = null;
            Rolls = new List<int>();
        }

        public Frame(Frame previous, int index)
        {
            Previous = previous;
            Rolls = new List<int>();
            Score = previous.Score;
            Index = index;
        }

        public bool Roll(int roll)
        {
            Rolls.Add(roll);

            if (IsStrike() || IsSpare())
            {
                if (IsLast() && Rolls.Count() < 3)
                    return true;

                return false;
            }

            Score += roll;

            return (!IsLast() && Rolls.Count < 2);
        }

        public bool IsLast()
        {
            return Index == 9 && Rolls.Count > 2;
        }

        public bool IsStrike()
        {
            return Rolls.Count > 0 && Rolls[0] == SPARE;
        }

        public bool IsSpare()
        {
            return Rolls.Count > 1 && Rolls.Sum() == SPARE;
        }
    }
}
