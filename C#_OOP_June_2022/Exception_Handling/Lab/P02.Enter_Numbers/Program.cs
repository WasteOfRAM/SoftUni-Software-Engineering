using System;
using System.Collections.Generic;

namespace P02.Enter_Numbers
{
    internal class Program
    {
        private static HashSet<int> numbers = new HashSet<int>();
        static void Main()
        {
            ReadNumber(1, 100);

            Console.WriteLine(string.Join(", ", numbers));
        }

        private static void ReadNumber(int start, int end)
        {
            while (numbers.Count < 10)
            {
                string input = Console.ReadLine();
                int number;

                try
                {
                    if (!int.TryParse(input, out number))
                        throw new ArgumentException("Invalid Number!");

                    if (number > start && number < end)
                    {
                        numbers.Add(number);
                        start = number;
                    }
                    else
                    {
                        throw new ArgumentException($"Your number is not in range {start} - {end}!");
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
