using System;

namespace P06.Food_Shortage
{
    using Models;
    using Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            var inhabitantList = new List<IBuyer>();

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();

                string name = input[0];
                int age = int.Parse(input[1]);

                if (input.Length == 4)
                {
                    string id = input[2];
                    DateTime birthDate = GetBirthDate(input[3]);

                    inhabitantList.Add(new Citizen(name, age, id, birthDate));
                }
                else
                {
                    string group = input[2];

                    inhabitantList.Add(new Rebel(name, age, group));
                }
            }

            string buyerName;
            while ((buyerName = Console.ReadLine()) != "End")
            {
                IBuyer buyer = inhabitantList.FirstOrDefault(b => b.Name == buyerName);

                if (buyer != null)
                    buyer.BuyFood();
            }

            int totalFood = inhabitantList.Sum(b => b.Food);

            Console.WriteLine(totalFood);
        }

        private static DateTime GetBirthDate(string birthDate)
        {
            var date = birthDate.Split('/').Select(int.Parse).ToArray();

            return new DateTime(date[2], date[1], date[0]);
        }
    }
}
