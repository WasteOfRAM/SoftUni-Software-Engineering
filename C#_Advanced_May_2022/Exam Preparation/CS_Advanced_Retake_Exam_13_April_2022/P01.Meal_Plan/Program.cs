using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Meal_Plan
{
    internal class Program
    {
        static void Main()
        {
            var meals = new Queue<string>(Console.ReadLine().Split());
            var calories = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            int mealsHad = 0;
            var mealCalories = new Dictionary<string, int>
            {
                {"salad", 350},
                {"soup", 490},
                {"pasta", 680},
                {"steak", 790}
            };

            while (meals.Count > 0 && calories.Count > 0)
            {
                int currentMeal = mealCalories[meals.Dequeue()];
                int currentCalories = calories.Pop();

                currentCalories -= currentMeal;
                mealsHad++;

                if(currentCalories <= 0)
                {
                    if (calories.Count == 0)
                        break;

                    currentCalories += calories.Pop();
                    calories.Push(currentCalories);
                    continue;
                }


                calories.Push(currentCalories);

            }

            if(meals.Count == 0)
            {
                Console.WriteLine($"John had {mealsHad} meals.");
                Console.WriteLine($"For the next few days, he can eat {string.Join(", ", calories)} calories.");
            }
            else
            {
                Console.WriteLine($"John ate enough, he had {mealsHad} meals.");
                Console.WriteLine($"Meals left: {string.Join(", ", meals)}.");
            }
        }
    }
}