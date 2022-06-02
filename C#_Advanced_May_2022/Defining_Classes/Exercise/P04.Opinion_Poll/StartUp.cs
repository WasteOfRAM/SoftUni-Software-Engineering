using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            List<Person> people = new List<Person>();

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string name = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
                int age = int.Parse(input.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);

                people.Add(new Person(name, age));
            }

            people = people.Where(p => p.Age > 30).OrderBy(p => p.Name).ToList();

            Console.WriteLine(string.Join(Environment.NewLine, people));
        }
    }
}
