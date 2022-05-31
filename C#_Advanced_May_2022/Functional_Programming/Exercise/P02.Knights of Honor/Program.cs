using System;
using System.Linq;

namespace P02.Knights_of_Honor
{
    internal class Program
    {
        static void Main()
        {
            Action<string> appendTitle = str => Console.WriteLine("Sir " + str);

            string[] knigts = Console.ReadLine().Split(' ');

            foreach (var knight in knigts)
            {
                appendTitle(knight);
            }
        }
    }
}
