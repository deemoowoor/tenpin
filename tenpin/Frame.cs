using System.Collections.Generic;
using System.Linq;

namespace tenpin
{
    public class Frame
    {
        public const int Spare = 10;
        public Frame Previous { get; }

        public int Index { get; }

        public List<int> Rolls { get; }

        public int Score { get; private set; }

        public bool IsLast { get { return Index == 10 && Rolls.Count > 2; } }

        public bool IsStrike { get { return Rolls.Count > 0 && Rolls[0] == Spare; } }

        public bool IsSpare { get { return Rolls.Count > 1 && Rolls.Sum() == Spare; } }

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

            Score += roll;

            if (IsStrike || IsSpare)
            {
                if (IsLast && Rolls.Count() == 3)
                    return false;
            }

            if (Previous != null && Previous != Null)
            {
                if (Previous.IsSpare && Rolls.Count == 1)
                {
                    Score += roll;
                }
                else if (Previous.IsStrike)
                {
                    Score += roll;
                }
            }

            return (!IsStrike && !IsLast && Rolls.Count < 2);
        }

    }
}
