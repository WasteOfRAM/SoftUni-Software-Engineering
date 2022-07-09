namespace P09.Explicit_Interfaces
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Interfaces;
    public class Program
    {
        static void Main()
        {
            var citizens = new List<Citizen>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split();
                string name = inputArgs[0];
                string country = inputArgs[1];
                int age = int.Parse(inputArgs[2]);

                var citizen = new Citizen(name, age, country);

                citizens.Add(citizen);
            }

            foreach (var citizen in citizens)
            {
                var resident = citizen as IResident;
                var person = citizen as IPerson;

                Console.WriteLine(person.GetName());
                Console.WriteLine(resident.GetName());
            }
        }
    }
}
