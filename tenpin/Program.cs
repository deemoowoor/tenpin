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
            var values = input.Split(", ").Select(int.Parse);
        }

        static StreamReader CreateReader(string[] args)
        {
            if (args.Length > 0)
            {
                return new StreamReader(args[0]);
            }

            return new StreamReader(Console.OpenStandardInput());
        }
    }
}
