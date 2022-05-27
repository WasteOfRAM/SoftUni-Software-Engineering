using System;
using System.Collections.Generic;
using System.Linq;

namespace P10.ForceBook
{
    internal class Program
    {
        static void Main()
        {
            var forceBook = new Dictionary<string, SortedSet<string>>();

            string[] delimiters = new string[] { " | ", " -> " };

            string input;
            while ((input = Console.ReadLine()) != "Lumpawaroo")
            {
                string[] inputSplit = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                string forceSide = string.Empty;
                string forceUser = string.Empty;

                if (input.Contains("|"))
                {
                    forceSide = inputSplit[0];
                    forceUser = inputSplit[1];

                    if (!forceBook.Any(x => x.Value.Contains(forceUser)))
                        if (!forceBook.ContainsKey(forceSide))
                            forceBook[forceSide] = new SortedSet<string>() { forceUser };
                        else if (!forceBook[forceSide].Contains(forceUser))
                            forceBook[forceSide].Add(forceUser);
                }
                else
                {
                    forceSide = inputSplit[1];
                    forceUser = inputSplit[0];

                    if (!forceBook.ContainsKey(forceSide))
                        forceBook[forceSide] = new SortedSet<string>();

                    foreach (var side in forceBook)
                    {
                        side.Value.Remove(forceUser);
                    }

                    forceBook[forceSide].Add(forceUser);

                    Console.WriteLine($"{forceUser} joins the {forceSide} side!");
                }
            }

            foreach (var side in forceBook.OrderByDescending(s => s.Value.Count).ThenBy(s => s.Key))
            {
                if (side.Value.Count > 0)
                    Console.WriteLine($"Side: {side.Key}, Members: {side.Value.Count}");

                foreach (var user in side.Value)
                {
                    Console.WriteLine($"! {user}");
                }
            }
        }
    }
}
