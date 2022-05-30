using System;
using System.Linq;

namespace P03.Count_Uppercase_Words
{
    internal class Program
    {
        static void Main()
        {
            Func<string, bool> iCapitalLetter = str => str.Length > 0 && char.IsUpper(str[0]);

            string[] words = Console.ReadLine().Split(' ').Where(iCapitalLetter).ToArray();

            Console.WriteLine(string.Join(Environment.NewLine, words));
        }
    }
}
