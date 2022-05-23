using System;
using System.Collections.Generic;

namespace P06.Record_Unique_Names
{
    internal class Program
    {
        static void Main()
        {
            int namesCount = int.Parse(Console.ReadLine());

            HashSet<string> names = new HashSet<string>();

            for (int i = 0; i < namesCount; i++)
            {
                names.Add(Console.ReadLine());
            }

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
