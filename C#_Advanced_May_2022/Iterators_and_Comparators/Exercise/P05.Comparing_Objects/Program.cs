using System;
using System.Collections.Generic;

namespace P05.Comparing_Objects
{
    internal class Program
    {
        static void Main()
        {
            var people = new List<Person>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string name = input.Split()[0];
                int age = int.Parse(input.Split()[1]);
                string town = input.Split()[2];

                people.Add(new Person(name, age, town));
            }

            int index = int.Parse(Console.ReadLine()) - 1;

            int maches = 0;
            int notEqual = 0;

            foreach (var person in people)
            {
                if(people[index].CompareTo(person) == 0)
                    maches++;
                else
                    notEqual++;
            }

            if(maches <= 1)
                Console.WriteLine("No matches");
            else
                Console.WriteLine($"{maches} {notEqual} {people.Count}");
        }
    }
}
