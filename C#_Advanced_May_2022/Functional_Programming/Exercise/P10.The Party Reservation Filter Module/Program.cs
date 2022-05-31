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

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            string input;
            while ((input = Console.ReadLine()) != "Print")
            {
                string command = input.Split(' ')[0];
                string criteria = input.Split(';')[1];
                string arg = input.Split(';')[2];

                


                if (command == "Add")
                {
                    list.Add(new KeyValuePair<string, string>(criteria, arg));
                }
                else if (command == "Remove")
                {
                    list.Remove(new KeyValuePair<string, string>(criteria, arg));
                }
            }

            foreach (KeyValuePair<string, string> kvp in list)
            {
                Predicate<string> predicate = GetPredicate(kvp);

                guestList.RemoveAll(predicate);
            }

            Console.WriteLine(string.Join(" ", guestList));
        }

        private static Predicate<string> GetPredicate(KeyValuePair<string, string> input)
        {
            Predicate<string> predicate = null;
            string criteria = input.Key;
            string arg = input.Value;

            if (criteria == "Starts with")
                predicate = name => name.StartsWith(arg);
            else if (criteria == "Ends with")
                predicate = name => name.EndsWith(arg);
            else if (criteria == "Length")
                predicate = name => name.Length == int.Parse(arg);
            else if (criteria == "Contains")
                predicate = name => name.Contains(arg);

            return predicate;
        }
    }
}
