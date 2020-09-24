using System;
using System.Collections.Generic;
using System.Linq;

namespace tenpin
{
    public class Frame
    {
        public const int Spare = 10;

        public const int MaxFrameIndex = 10;

        public const int RollsPerFrame = 2;

        public const int MaxRollsPerFrame = 3;

        public Frame Previous { get; }

        public int Index { get; }

        public List<int> Rolls { get; }

        public int Score { get; private set; }

        public bool IsLast => Index == MaxFrameIndex && Rolls.Count >= RollsPerFrame;

        public bool IsStrike => Rolls.Count > 0 && Rolls[0] == Spare;

        public bool IsSpare => Rolls.Count > 1 && Rolls.Sum() == Spare;

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
            Score = 0;
            Index = index;
        }

        private void AddSpareScore(int score)
        {
            if (!IsStrike && !IsSpare) 
                throw new Exception("Should not be called for anything but spares or strikes");

            Score += score;
        }

        public bool Roll(int roll)
        {
            Rolls.Add(roll);

            Score += roll;

            if (IsStrike || IsSpare)
            {
                if (IsLast && Rolls.Count() == MaxRollsPerFrame)
                    return false;
            }

            if (Previous != null)
            {
                if (Previous.IsSpare && Rolls.Count == 1)
                {
                    Previous.AddSpareScore(roll);
                }
                else if (Previous.IsStrike)
                {
                    Previous.AddSpareScore(roll);
                }

                var prePrevious = Previous.Previous;

                if (!IsLast && prePrevious != null && prePrevious.IsStrike)
                {
                    prePrevious.AddSpareScore(roll);
                }
            }

            return !IsStrike && !IsLast && Rolls.Count < 2;
        }

        public override string ToString()
        {
            if (IsStrike && !IsLast)
            {
                return "X";
            }

            if (IsSpare && !IsLast)
            {
                return string.Format("{0}, /", Rolls[0]);
            }

            if (IsLast && IsStrike)
            {
                if (Rolls[1] == Spare)
                {
                    return string.Format("X, X");
                }

                return string.Format("X, {0}, {1}", Rolls[1], Rolls[2]);
            }

            return string.Format("{0}, {1}", Rolls[0], Rolls[1]);
        }
    }
}
