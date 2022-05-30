using System;
using System.Linq;

namespace P02.Sum_Numbers
{
    internal class Program
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

            Console.WriteLine(numbers.Length);
            Console.WriteLine(numbers.Sum());
        }
    }
}
