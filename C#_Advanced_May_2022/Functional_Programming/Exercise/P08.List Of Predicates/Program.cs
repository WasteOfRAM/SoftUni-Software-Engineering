using System;
using System.Collections.Generic;
using System.Linq;

namespace P08.List_Of_Predicates
{
    internal class Program
    {
        static void Main()
        {
            int rangeEnd = int.Parse(Console.ReadLine());
            List<int> dividers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            List<int> numbers = new List<int>();

            Predicate<int> predicate = num => dividers.All(div => num % div == 0);
            Action<List<int>> print = list => Console.WriteLine(string.Join(' ', list));

            for (int i = 1; i <= rangeEnd; i++)
            {
                if(predicate(i))
                    numbers.Add(i);
            }

            print(numbers);
        }
    }
}
