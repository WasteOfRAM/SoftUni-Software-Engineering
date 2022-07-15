using System;
using System.Collections.Generic;
using System.Linq;

namespace P04.Sum_of_Integers
{
    internal class Program
    {
        static void Main()
        {
            string[] input = Console.ReadLine().Split();
            var numbers = new List<int>();

            foreach (var element in input)
            {
                try
                {
                    int curentElement = int.Parse(element);

                    numbers.Add(curentElement);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"The element '{element}' is in wrong format!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"The element '{element}' is out of range!");
                }
                finally
                {
                    Console.WriteLine($"Element '{element}' processed - current sum: {numbers.Sum()}");
                }
            }


            Console.WriteLine($"The total sum of all integers is: {numbers.Sum()}");
        }
    }
}
