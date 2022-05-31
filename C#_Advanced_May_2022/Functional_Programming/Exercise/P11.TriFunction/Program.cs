using System;
using System.Collections.Generic;
using System.Linq;

namespace P11.TriFunction
{
    internal class Program
    {
        static void Main()
        {
            int num = int.Parse(Console.ReadLine());

            List<string> names = Console.ReadLine().Split(' ').ToList();

            string result = names.First(name => name.Select(ch => (int)ch).Sum() >= num);
            Console.WriteLine(result);
        }
    }
}
