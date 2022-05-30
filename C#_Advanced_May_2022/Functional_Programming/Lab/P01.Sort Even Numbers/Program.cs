using System;
using System.Linq;

namespace P01.Sort_Even_Numbers
{
    internal class Program
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine().Split(", ").Select(int.Parse).Where(n => n % 2 == 0).OrderBy(n => n).ToArray();

            Console.WriteLine(string.Join(", " , numbers));
        }
    }
}
