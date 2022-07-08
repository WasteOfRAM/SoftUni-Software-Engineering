namespace P05.Birthday_Celebrations
{
    using System;
    using System.Linq;
    using Models;
    using Models.Interfaces;
    using System.Collections.Generic;

    public class Program
    {
        static void Main()
        {
            var inhabitantsList = new List<IBirthable>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] inputArgs = input.Split();

                if (inputArgs[0] == "Citizen")
                {
                    string name = inputArgs[1];
                    int age = int.Parse(inputArgs[2]);
                    string id = inputArgs[3];
                    DateTime birthDate = GetBirthDate(inputArgs[4]);

                    inhabitantsList.Add(new Citizen(name, age, id, birthDate));
                }
                else if (inputArgs[0] == "Pet")
                {
                    string name = inputArgs[1];
                    DateTime birthDate = GetBirthDate(inputArgs[2]);

                    inhabitantsList.Add(new Pet(name, birthDate));
                }
            }

            int year = int.Parse(Console.ReadLine());

            var inhabitantsByYear = inhabitantsList.Where(inhabitant => inhabitant.BirthDate.Year == year).ToList();


            foreach (var inhabitant in inhabitantsByYear)
            {
                Console.WriteLine($"{inhabitant.BirthDate:dd\\/MM\\/yyyy}");
            }

        }

        private static DateTime GetBirthDate(string birthDate)
        {
            var date = birthDate.Split('/').Select(int.Parse).ToArray();

            return new DateTime(date[2], date[1], date[0]);
        }
    }
}
