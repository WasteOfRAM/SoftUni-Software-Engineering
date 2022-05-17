using System;
using System.Collections.Generic;
using System.Linq;

namespace P03.Maximum_and_Minimum_Element
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int queries = int.Parse(Console.ReadLine());

            Stack<int> elements = new Stack<int>();

            for (int i = 0; i < queries; i++)
            {
                string command = Console.ReadLine();

                if (command.StartsWith("1"))
                {
                    elements.Push(int.Parse(command.Split()[1]));
                }
                else if (command.StartsWith("2"))
                {
                    if (elements.Count > 0)
                    {
                        elements.Pop(); 
                    }
                }
                else if (command.StartsWith("3"))
                {
                    if (elements.Count > 0)
                    {
                        Console.WriteLine(elements.Max());
                    }
                }
                else if (command.StartsWith("4"))
                {
                    if (elements.Count > 0)
                    {
                        Console.WriteLine(elements.Min());
                    }
                }
            }

            Console.WriteLine(string.Join(", ", elements));
        }
    }
}
