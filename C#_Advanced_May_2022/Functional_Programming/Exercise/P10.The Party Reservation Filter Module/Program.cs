using System;
using System.Collections.Generic;
using System.Linq;

namespace P10.The_Party_Reservation_Filter_Module
{
    internal class Program
    {
        static void Main()
        {
            List<string> guestList = Console.ReadLine().Split(" ").ToList();


            string input;
            while ((input = Console.ReadLine()) != "Print")
            {
                string command = input.Split(' ')[0];
                string criteria = input.Split(' ')[1];
                string arg = input.Split(' ')[2];

                Predicate<string> predicate = GetPredicate(input);

                if (command == "Add")
                {


                }
                else if (command == "Remove")
                {
                    guestList.RemoveAll(predicate);
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

            if (criteria == "Starts With")
                predicate = name => name.StartsWith(arg);
            else if (criteria == "Ends With")
                predicate = name => name.EndsWith(arg);
            else if (criteria == "Length")
                predicate = name => name.Length == int.Parse(arg);
            else if (criteria == "Contains")
                predicate = name => name.Contains(arg);

            return predicate;
        }
    }
}
