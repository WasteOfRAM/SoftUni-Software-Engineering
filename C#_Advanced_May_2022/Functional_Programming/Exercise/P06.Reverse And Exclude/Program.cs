using System;
using System.Collections.Generic;
using System.Linq;

namespace P06.Reverse_And_Exclude
{
    internal class Program
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            int divisor = int.Parse(Console.ReadLine());

            Predicate<int> isDivisible = num => num % divisor == 0;
            Func<List<int>, string> strJoin = str => string.Join(" ", str);

            numbers.RemoveAll(isDivisible);
            numbers.Reverse();

            Console.WriteLine(strJoin(numbers));
        }
    }
}
