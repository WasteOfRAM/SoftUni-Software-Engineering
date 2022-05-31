using System;
using System.Collections.Generic;
using System.Linq;

namespace P09.Predicate_Party_
{
    internal class Program
    {
        static void Main()
        {
            List<string> guestList = Console.ReadLine().Split(" ").ToList();


            string input;
            while ((input = Console.ReadLine()) != "Party!")
            {
                string command = input.Split(' ')[0];
                string criteria = input.Split(' ')[1];
                string arg = input.Split(' ')[2];

                Predicate<string> predicate = GetPredicate(input);

                if (command == "Remove")
                {
                    guestList.RemoveAll(predicate);

                }
                else if (command == "Double")
                {

                    for (int i = 0; i < guestList.Count; i++)
                    {
                        if (predicate(guestList[i]))
                        {
                            guestList.Insert(i + 1, guestList[i]);
                            i++;
                        }  
                    }
                }
            }

            if (guestList.Count > 0)
                Console.WriteLine(string.Join(", ", guestList) + " are going to the party!");
            else
                Console.WriteLine("Nobody is going to the party!");
        }

        private static Predicate<string> GetPredicate(string input)
        {
            Predicate<string> predicate = null;
            string criteria = input.Split(' ')[1];
            string arg = input.Split(' ')[2];

            if (criteria == "StartsWith")
                predicate = name => name.StartsWith(arg);
            else if (criteria == "EndsWith")
                predicate = name => name.EndsWith(arg);
            else if (criteria == "Length")
                predicate = name => name.Length == int.Parse(arg);


            return predicate;
        }
    }
}
