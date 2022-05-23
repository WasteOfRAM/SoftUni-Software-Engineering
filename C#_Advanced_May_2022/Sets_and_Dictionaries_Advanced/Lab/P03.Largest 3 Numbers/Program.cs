using System;
using System.Linq;

namespace P03.Largest_3_Numbers
{
    internal class Program
    {
        static void Main()
        {
            var numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).OrderByDescending(n => n).ToArray().Take(3);

            Console.WriteLine(string.Join(' ', numbers));
        }
    }
}
