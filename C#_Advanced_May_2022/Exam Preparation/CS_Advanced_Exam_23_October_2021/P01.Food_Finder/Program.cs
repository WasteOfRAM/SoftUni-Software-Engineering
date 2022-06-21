using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Food_Finder
{
    internal class Program
    {
        static void Main()
        {
            Queue<char> vowels = new Queue<char>(Console.ReadLine().Split().Select(char.Parse));
            Stack<char> consonants = new Stack<char>(Console.ReadLine().Split().Select(char.Parse));

            var words = new List<string>
            {
                "pear",
                "flour",
                "pork",
                "olive"
            };

            var lettersMatched = new HashSet<char>();

            while (consonants.Count > 0)
            {
                char currentVowel = vowels.Dequeue();
                char currentConso = consonants.Pop();

                if(words.Any(w => w.Contains(currentVowel)))
                    lettersMatched.Add(currentVowel);

                if(words.Any(w => w.Contains(currentConso)))
                    lettersMatched.Add(currentConso);

                vowels.Enqueue(currentVowel);
            }


            words = words.Where(word => word.All(ch => lettersMatched.Contains(ch))).ToList();

            Console.WriteLine($"Words found: {words.Count}");
            Console.WriteLine(string.Join(Environment.NewLine, words));
        }
    }
}