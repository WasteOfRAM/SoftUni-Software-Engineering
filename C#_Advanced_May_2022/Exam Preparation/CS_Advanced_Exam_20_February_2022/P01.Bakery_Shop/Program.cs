using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Bakery_Shop
{
    internal class Program
    {
        static void Main()
        {
            var waterInventory = new Queue<double>(Console.ReadLine().Split().Select(double.Parse));
            var flourInventory = new Stack<double>(Console.ReadLine().Split().Select(double.Parse));

            var products = new Dictionary<int, Product>
            {
                {50, new Product("Croissant", 0)},
                {40, new Product("Muffin", 0)},
                {30, new Product("Baguette", 0)},
                {20, new Product("Bagel", 0)}
            };

            while (waterInventory.Count > 0 && flourInventory.Count > 0)
            {
                double water = waterInventory.Dequeue();
                double flour = flourInventory.Pop();

                int waterPercent = (int)((water * 100) / (water + flour));

                if (products.ContainsKey(waterPercent))
                {
                    products[waterPercent].Count++;
                    continue;
                }

                flour -= water;
                products[50].Count++;
                flourInventory.Push(flour);
            }

            Dictionary<string, int> backedProducts = products.Where(p => p.Value.Count > 0).ToDictionary(k => k.Value.Name, v => v.Value.Count);

            foreach (var item in backedProducts.OrderByDescending(c => c.Value).ThenBy(n => n.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            string waterLeft = "Water left: " + (waterInventory.Count == 0 ? "None" : string.Join(", ", waterInventory));
            string flourLeft = "Flour left: " + (flourInventory.Count == 0 ? "None" : string.Join(", ", flourInventory));
            Console.WriteLine(waterLeft);
            Console.WriteLine(flourLeft);
        }
    }

    class Product
    {
        public Product(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }
    }
}