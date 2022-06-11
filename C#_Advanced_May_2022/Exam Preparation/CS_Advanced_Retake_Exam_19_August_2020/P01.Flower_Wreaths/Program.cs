using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Flower_Wreaths
{
    internal class Program
    {
        static void Main()
        {
            var lilies = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            var roses = new Queue<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            int wreathsCount = 0;
            int storedFlowers = 0;

            while (wreathsCount < 5)
            {
                if (lilies.Count == 0 || roses.Count == 0)
                    break;

                int flowerSum = lilies.Peek() + roses.Peek();

                if (flowerSum == 15)
                {
                    wreathsCount++;
                    lilies.Pop();
                    roses.Dequeue();
                }
                else if (flowerSum > 15)
                {
                    int lilieValue = lilies.Pop() - 2;
                    lilies.Push(lilieValue);
                }
                else if(flowerSum < 15)
                {
                    storedFlowers += lilies.Pop() + roses.Dequeue();
                }
            }

            wreathsCount += storedFlowers / 15;

            if(wreathsCount >= 5)
            {
                Console.WriteLine($"You made it, you are going to the competition with {wreathsCount} wreaths!");
            }
            else
            {
                Console.WriteLine($"You didn't make it, you need {5 - wreathsCount} wreaths more!");
            }
        }
    }
}
