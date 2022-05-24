using System;
using System.Collections.Generic;
using System.Linq;

namespace P03.Periodic_Table
{
    internal class Program
    {
        static void Main()
        {
            int inputLines = int.Parse(Console.ReadLine());

            var elements = new HashSet<string>();

            for (int i = 0; i < inputLines; i++)
            {
                string[] line = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var elem in line)
                {
                    elements.Add(elem);
                }
            }

            Console.WriteLine(string.Join(' ', elements.OrderBy(e => e)));
        }
    }
}
