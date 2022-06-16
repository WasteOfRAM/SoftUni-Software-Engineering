using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Warm_Winter
{
    internal class Program
    {
        static void Main()
        {
            Stack<int> hats = new Stack<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Queue<int> scarfs = new Queue<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            var sets = new List<int>();


            while (hats.Count > 0 && scarfs.Count > 0)
            {
                int hatPrice = hats.Peek();
                int scarfPrice = scarfs.Peek();

                if (hatPrice > scarfPrice)
                {
                    int set = hatPrice + scarfPrice;

                    hats.Pop();
                    scarfs.Dequeue();

                    sets.Add(set);
                }
                else if (hatPrice < scarfPrice)
                {
                    hats.Pop();
                }
                else if (hatPrice == scarfPrice)
                {
                    scarfs.Dequeue();
                    hats.Pop();
                    hats.Push(++hatPrice);
                }
            }

            Console.WriteLine($"The most expensive set is: {sets.Max()}");
            Console.WriteLine(string.Join(" ", sets));
        }
    }
}
