using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Count_Same_Values_in_Array
{
    internal class Program
    {
        static void Main()
        {
            var numbersCount = new Dictionary<double, int>();

            double[] numbers = Console.ReadLine().Split(' ').Select(double.Parse).ToArray();

            foreach (var num in numbers)
            {
                if (!numbersCount.ContainsKey(num))
                    numbersCount[num] = 1;
                else
                    numbersCount[num]++;
            }

            foreach (var (number, count) in numbersCount)
            {
                Console.WriteLine($"{number} - {count} times");
            }
        }
    }
}
