using System;
using System.Linq;

namespace P03.Custom_Min_Function
{
    internal class Program
    {
        static void Main()
        {
            //Solution 1
            //Console.WriteLine(Console.ReadLine().Split(' ').Select(int.Parse).Min());

            //Solution 2
            Func<int[], int> findMin = numbers =>
            {
                int min = int.MaxValue;

                foreach (var number in numbers)
                {
                    if (number < min)
                        min = number;
                }

                return min;
            };

            int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            Console.WriteLine(findMin(numbers));
        }
    }
}
