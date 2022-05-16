using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.Print_Even_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse);

            Queue<int> queOfInts = new Queue<int>(input);

            List<int> evenNums = new List<int>();

            while (queOfInts.Count > 0)
            {
                int removedNum = queOfInts.Dequeue();

                if (removedNum % 2 == 0)
                {
                    evenNums.Add(removedNum);
                }
            }

            Console.WriteLine(string.Join(", ", evenNums));
        }
    }
}
