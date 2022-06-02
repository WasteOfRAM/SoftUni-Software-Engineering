using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main()
        {
            Family family = new Family();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string name = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
                int age = int.Parse(input.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);

                family.AddMember(new Person(name, age));
            }

            Person oldestPeron = family.GetOldestMember();

            Console.WriteLine(oldestPeron.Name + " " + oldestPeron.Age);
        }
    }
}
