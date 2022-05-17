using System;
using System.Collections.Generic;
using System.Linq;

namespace P04.Fast_Food
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int quantityForTheDay = int.Parse(Console.ReadLine());
            Queue<int> ordersQuantity = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));

            Console.WriteLine(ordersQuantity.Max());

            int ordersCount = ordersQuantity.Count();

            for (int i = 0; i < ordersCount; i++)
            {
                if (quantityForTheDay >= ordersQuantity.Peek())
                {
                    quantityForTheDay -= ordersQuantity.Dequeue();
                }
                else
                {
                    break;
                }
            }

            if (ordersQuantity.Count == 0)
            {
                Console.WriteLine("Orders complete");
            }
            else
            {
                Console.WriteLine($"Orders left: {string.Join(" ", ordersQuantity)}");
            }
        }
    }
}
