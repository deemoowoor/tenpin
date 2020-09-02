using System;
using System.IO;
using System.Linq;

namespace tenpin
{
    class Program
    {
        static void Main(string[] args)
        {
            using var reader = CreateReader(args);
            var input = reader.ReadLine();
            var rolls = input.Split(", ").Select(int.Parse);

            var computer = new TenpinScoreTotal(rolls);

            Console.Write("|");
            foreach (var frame in computer.Frames)
            {
                Console.Write(string.Format(" F{0} ", frame.Index));
                Console.Write("|");
            }

            Console.Write("\n");
            Console.Write("|");

            foreach (var frame in computer.Frames)
            {
                Console.Write(" ");
                Console.Write(frame.ToString());
                Console.Write(" |");
            }

            Console.Write("\n");

            Console.WriteLine("Score: {0}", computer.GetTotalScore());
        }

        static StreamReader CreateReader(string[] args)
        {
            Console.WriteLine(string.Join("\n", args));

            if (args.Length > 0)
            {
                return new StreamReader(args[0]);
            }

            return new StreamReader(Console.OpenStandardInput());
        }
    }
}
