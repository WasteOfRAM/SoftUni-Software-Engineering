using System;
using System.Collections.Generic;

namespace P06.Equality_Logic
{
    internal class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            var set = new HashSet<Person>();
            var sortedSet = new SortedSet<Person>();

            for(int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string name = input.Split()[0];
                int age = int.Parse(input.Split()[1]);

                set.Add(new Person(name, age));
                sortedSet.Add(new Person(name, age));
            }

            Console.WriteLine(set.Count);
            Console.WriteLine(sortedSet.Count);
        }
    }
}
