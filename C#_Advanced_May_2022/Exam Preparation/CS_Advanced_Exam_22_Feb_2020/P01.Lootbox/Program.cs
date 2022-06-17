using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Lootbox
{
    internal class Program
    {
        static void Main()
        {
            Queue<int> firstBox = new Queue<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Stack<int> secondBox = new Stack<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            int lootValue = 0;

            while (firstBox.Count > 0 && secondBox.Count > 0)
            {
                int firstBoxItem = firstBox.Peek();
                int secondBoxItem = secondBox.Peek();
                int itemValue = firstBoxItem + secondBoxItem;

                if(itemValue % 2 == 0)
                {
                    lootValue += itemValue;
                    firstBox.Dequeue();
                    secondBox.Pop();
                }
                else
                {
                    secondBox.Pop();
                    firstBox.Enqueue(secondBoxItem);
                }
            }

            Console.WriteLine(firstBox.Count < secondBox.Count ? "First lootbox is empty" : "Second lootbox is empty");
            Console.WriteLine(lootValue >= 100 ? $"Your loot was epic! Value: {lootValue}" : $"Your loot was poor... Value: {lootValue}");
        }
    }
}
