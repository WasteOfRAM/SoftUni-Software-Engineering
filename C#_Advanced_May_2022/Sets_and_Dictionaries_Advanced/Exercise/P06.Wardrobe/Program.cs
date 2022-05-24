using System;
using System.Collections.Generic;
using System.Linq;

namespace P06.Wardrobe
{
    internal class Program
    {
        static void Main()
        {
            int inputLines = int.Parse(Console.ReadLine());

            var wardrobe = new Dictionary<string, Dictionary<string, int>>();

            string[] delimiters = new string[] { " -> ", "," };

            for (int i = 0; i < inputLines; i++)
            {
                List<string> inputLine = Console.ReadLine().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
                string color = inputLine[0];
                inputLine.RemoveAt(0);

                if(!wardrobe.ContainsKey(color))
                    wardrobe[color] = new Dictionary<string, int>();

                foreach (var item in inputLine)
                {
                    if(!wardrobe[color].ContainsKey(item))
                        wardrobe[color][item] = 1;
                    else
                        wardrobe[color][item]++;
                }
            }

            string lookingForItem = Console.ReadLine();
            string itemColor = lookingForItem.Split(' ')[0];
            string itemType = lookingForItem.Split(' ')[1];

            foreach (var (color, items) in wardrobe)
            {
                Console.WriteLine($"{color} clothes:");
                foreach (var (item, itemCount) in items)
                {
                    Console.WriteLine((color == itemColor && item == itemType) ? $"* {item} - {itemCount} (found!)" : $"* {item} - {itemCount}");
                }
            }

        }
    }
}
