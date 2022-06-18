using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Masterchef
{
    internal class Program
    {
        static void Main()
        {
            Queue<int> ingredients = new Queue<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Stack<int> freshnesses = new Stack<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            List<Dish> dishes = new List<Dish>
            {
                new Dish("Dipping sauce", 150),
                new Dish("Green salad", 250),
                new Dish("Chocolate cake", 300),
                new Dish("Lobster", 400)
            };

            while (ingredients.Count > 0 && freshnesses.Count > 0)
            {
                if(ingredients.Peek() == 0)
                {
                    ingredients.Dequeue();
                    continue;
                }

                int ingredient = ingredients.Dequeue();
                int freshness = freshnesses.Pop();

                int freshnessNeeded = ingredient * freshness;

                Dish currentDish = dishes.FirstOrDefault(f => f.Freshness == freshnessNeeded);

                if (currentDish != null)
                    currentDish.Count++;
                else
                    ingredients.Enqueue(ingredient + 5);
            }

            dishes = dishes.Where(d => d.Count > 0).ToList();

            Console.WriteLine(dishes.Count == 4 ? "Applause! The judges are fascinated by your dishes!" : "You were voted off. Better luck next year.");

            if(ingredients.Count > 0)
                Console.WriteLine($"Ingredients left: {ingredients.Sum()}");

            foreach (var dish in dishes.OrderBy(d => d.Name))
            {
                Console.WriteLine($" # {dish.Name} --> {dish.Count}");
            }
        }
    }

    class Dish
    {
        public Dish(string name, int freshness)
        {
            Name = name;
            Freshness = freshness;
            Count = 0;
        }

        public string Name { get; set; }
        public int Freshness { get; set; }
        public int Count { get; set; }
    }
}
