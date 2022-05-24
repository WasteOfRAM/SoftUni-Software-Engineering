using System;
using System.Collections.Generic;

namespace P02.Sets_of_Elements
{
    internal class Program
    {
        static void Main()
        {
            string[] setsSizes = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int setOneSize = int.Parse(setsSizes[0]);
            int setTwoSize = int.Parse(setsSizes[1]);

            var setOne = new HashSet<int>();
            var setTwo = new HashSet<int>();

            for (int i = 0; i < setOneSize; i++)
            {
                int number = int.Parse(Console.ReadLine());
                setOne.Add(number);
            }

            for (int i = 0; i < setTwoSize; i++)
            {
                int number = int.Parse(Console.ReadLine());
                setTwo.Add(number);
            }

            setOne.IntersectWith(setTwo);

            Console.WriteLine(string.Join(' ', setOne));
        }
    }
}
