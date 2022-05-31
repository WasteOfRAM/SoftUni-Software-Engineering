using System;

namespace P01.ActionPrint
{
    internal class Program
    {
        static void Main()
        {
            string[] names = Console.ReadLine().Split();

            Action<string> printNames = name => Console.WriteLine(name);
            
            Array.ForEach(names, printNames);
        }
    }
}
