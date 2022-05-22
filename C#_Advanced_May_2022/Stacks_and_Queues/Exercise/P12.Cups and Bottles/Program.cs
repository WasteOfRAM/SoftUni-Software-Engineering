using System;
using System.Collections.Generic;
using System.Linq;

namespace P12.Cups_and_Bottles
{
    internal class Program
    {
        static void Main()
        {
            Queue<int> cups = new Queue<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Stack<int> bottles = new Stack<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            int wastedWater = 0;

            while (cups.Count > 0 && bottles.Count > 0)
            {
                int curentCup = cups.Peek();
                int curentBottle = bottles.Peek();

                if (curentCup <= curentBottle)
                {
                    wastedWater += curentBottle - curentCup;
                    cups.Dequeue();
                    bottles.Pop();
                }
                else
                {
                    while (true)
                    {
                        curentCup -= bottles.Pop();

                        if (curentCup <= bottles.Peek())
                        {
                            wastedWater += bottles.Pop() - curentCup;
                            cups.Dequeue();
                            break;
                        }
                    }
                }
            }

            if (cups.Count == 0)
            {
                Console.WriteLine($"Bottles: {string.Join(' ', bottles)}");
            }
            else
            {
                Console.WriteLine($"Cups: {string.Join(' ', cups)}");
            }

            Console.WriteLine($"Wasted litters of water: {wastedWater}");
        }
    }
}