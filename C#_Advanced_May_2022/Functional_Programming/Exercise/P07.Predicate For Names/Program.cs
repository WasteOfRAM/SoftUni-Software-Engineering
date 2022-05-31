using System;
using System.Collections.Generic;
using System.Linq;

namespace P07.Predicate_For_Names
{
    internal class Program
    {
        static void Main()
        {
            int maxLength = int.Parse(Console.ReadLine());
            List<string> names = Console.ReadLine().Split().ToList();

            Predicate<string> lengthCheck = name => name.Length <= maxLength;

            Action<string> print = name => { if (lengthCheck(name)) Console.WriteLine(name); };

            names.ForEach(print);
        }
    }
}
