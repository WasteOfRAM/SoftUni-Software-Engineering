using System;
using System.Collections.Generic;
using System.Linq;

namespace P04.Even_Times
{
    internal class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            Dictionary<int, int> numbers = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                int input = int.Parse(Console.ReadLine());

                if(numbers.ContainsKey(input))
                    numbers[input]++;
                else
                    numbers[input] = 1;
            }

            int num = numbers.FirstOrDefault(n => n.Value % 2 == 0).Key;

            Console.WriteLine(num);
        }
    }
}
