using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Cooking
{
    internal class Program
    {
        static void Main()
        {
            var liqids = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
            var ingredients = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            //const int BREAD = 25;
            //const int CAKE = 50;
            //const int FRUIT_PIE = 100;
            //const int PASTRY = 75;

            //int breadCount = 0;
            //int cakeCount = 0;
            //int fruitPieCount = 0;
            //int pastryCount = 0;

            var itemsCount = new Dictionary<int, int>
            {
                {25, 0},
                {50, 0},
                {100, 0},
                {75, 0},
            };

            while (liqids.Count != 0 && ingredients.Count != 0)
            {
                int sum = liqids.Peek() + ingredients.Peek();

                if (itemsCount.ContainsKey(sum))
                {
                    itemsCount[sum]++;
                    liqids.Dequeue();
                    ingredients.Pop();
                }
                else
                {
                    liqids.Dequeue();
                    int ingredient = ingredients.Pop() + 3;
                    ingredients.Push(ingredient);
                }
            }

            if(itemsCount.Any(i => i.Value == 0))
                Console.WriteLine("Ugh, what a pity! You didn't have enough materials to cook everything.");
            else
                Console.WriteLine("Wohoo! You succeeded in cooking all the food!");

            Console.Write("Liquids left: ");
            Console.WriteLine(liqids.Count == 0 ? "none" : string.Join(", ", liqids));
            Console.Write("Ingredients left: ");
            Console.WriteLine(ingredients.Count == 0 ? "none" : string.Join(", ", ingredients));

            Console.WriteLine($"Bread: {itemsCount[25]}");
            Console.WriteLine($"Cake: {itemsCount[50]}");
            Console.WriteLine($"Fruit Pie: {itemsCount[100]}");
            Console.WriteLine($"Pastry: {itemsCount[75]}");
        }
    }
}
