using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Blacksmith
{
    internal class Program
    {
        static void Main()
        {
            var steelInventory = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
            var carbonInventory = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            var swords = new Dictionary<int, Sword>
            {
                {70, new Sword("Gladius", 0)},
                {80, new Sword("Shamshir", 0)},
                {90, new Sword("Katana", 0)},
                {110, new Sword("Sabre", 0)},
                {150, new Sword("Broadsword", 0)}
            };

            while (steelInventory.Count > 0 && carbonInventory.Count > 0)
            {
                int steel = steelInventory.Dequeue();
                int carbon = carbonInventory.Pop();

                int curentMixture = steel + carbon;

                if (swords.ContainsKey(curentMixture))
                {
                    swords[curentMixture].Count++;
                    continue;
                }

                carbonInventory.Push(carbon + 5);
            }

            int totalSwordsForged = swords.Sum(s => s.Value.Count);

            Console.WriteLine(totalSwordsForged > 0 ? $"You have forged {totalSwordsForged} swords." : "You did not have enough resources to forge a sword.");
            string steelLeft = steelInventory.Count == 0 ? "none" : string.Join(", ", steelInventory);
            Console.WriteLine($"Steel left: {steelLeft}");
            string carbonLeft = carbonInventory.Count == 0 ? "none" : string.Join(", ", carbonInventory);
            Console.WriteLine($"Carbon left: {carbonLeft}");

            foreach (var sword in swords.OrderBy(s => s.Value.Name))
            {
                if (sword.Value.Count > 0)
                    Console.WriteLine($"{sword.Value.Name}: {sword.Value.Count}");
            }
        }
    }

    class Sword
    {
        public Sword(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }
    }
}
