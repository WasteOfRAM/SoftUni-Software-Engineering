using System;
using System.Collections.Generic;
using System.Linq;

namespace P04.Find_Evens_or_Odds
{
    internal class Program
    {
        static void Main()
        {
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int lowerBound = input[0];
            int upperBound = input[1];

            string command = Console.ReadLine();

            List<int> numbers = new List<int>();

            Predicate<int> filter = null;

            if (command == "odd")
                filter = num => num % 2 != 0;

            if (command == "even")
                filter = num => num % 2 == 0;

            for (int i = lowerBound; i <= upperBound; i++)
            {
                numbers.Add(i);
            }

            Console.WriteLine(string.Join(" ", numbers.Where(n => filter(n))));
        } 
    }
}
