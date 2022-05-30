using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.FilterByAge
{
    internal class Program
    {
        static void Main()
        {
            

            int n = int.Parse(Console.ReadLine());

            List<Person> people = new List<Person>();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(", ");
                Person person = new Person() { Name = input[0], Age = int.Parse(input[1])};

                people.Add(person);
            }

            string condition = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            string printFormat = Console.ReadLine();

            Func<Person, bool> filter = AgeFilter(condition, age);
            Action<Person> printPerson = PrintFormat(printFormat);

            Person[] filteredPeople = people.Where(filter).ToArray();

            Print(filteredPeople, printPerson);
        }

        static Func<Person, bool> AgeFilter(string condition, int age)
        {
            if(condition == "older")
                return  person => person.Age >= age;
            if (condition == "younger")
                return person => person.Age < age;

            throw new ArgumentException("Invalid input");
        }

        static Action<Person> PrintFormat(string printFormat)
        {
            if(printFormat == "name age")
                return person => Console.WriteLine($"{person.Name} - {person.Age}");
            if(printFormat == "name")
                return person => Console.WriteLine($"{person.Name}");
            if (printFormat == "age")
                return person => Console.WriteLine($"{person.Age}");

            throw new ArgumentException("Invalid format");
        }

        static void Print(Person[] people, Action<Person> personPrinter)
        {
            foreach(Person person in people)
            {
                personPrinter(person);
            }
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
