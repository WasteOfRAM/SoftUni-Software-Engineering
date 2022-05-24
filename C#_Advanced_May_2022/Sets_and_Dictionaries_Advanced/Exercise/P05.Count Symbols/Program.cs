using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.Count_Symbols
{
    internal class Program
    {
        static void Main()
        {
            string text = Console.ReadLine();

            Dictionary<char, int> characters = new Dictionary<char, int>();

            foreach (var ch in text)
            {
                if(characters.ContainsKey(ch))
                    characters[ch]++;
                else
                    characters[ch] = 1;
            }

            foreach (var character in characters.OrderBy(n => n.Key))
            {
                Console.WriteLine($"{character.Key}: {character.Value} time/s");
            }
        }
    }
}
