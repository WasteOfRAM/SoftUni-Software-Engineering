using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.Fashion_Boutique
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack<int> clothes = new Stack<int>(Console.ReadLine().Split(" ").Select(int.Parse));

            int rackCapacity = int.Parse(Console.ReadLine());
            int racksCount = 1;

            int clothesCount = clothes.Count();
            int curentRack = 0;

            for (int i = 0; i < clothesCount; i++)
            {
                if ((clothes.Peek() + curentRack) > rackCapacity)
                {
                    curentRack = 0;
                    racksCount++;
                }

                curentRack += clothes.Pop();
            }

            Console.WriteLine(racksCount);
        }
    }
}
