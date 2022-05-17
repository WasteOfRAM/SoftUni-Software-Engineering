using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Basic_Stack_Operations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] commands = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int N = commands[0]; // Not needed. Created the stack at line 17 without using Push()
            int S = commands[1];
            int X = commands[2];

            Stack<int> numbers = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            for (int i = 0; i < S; i++)
            {
                numbers.Pop();
            }

            if (numbers.Count == 0)
            {
                Console.WriteLine(0);
            }
            else if (numbers.Contains(X))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine(numbers.Min());
            }
        }
    }
}
