using System;
using System.Linq;

namespace P01_02.ListyIterator_Collection
{
    internal class Program
    {
        static void Main()
        {
            ListyIterator<string> listy = null;

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var command = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (command[0] == "Create")
                {
                    listy = new ListyIterator<string>(command.Skip(1).ToArray());
                }
                else if (command[0] == "Move")
                {
                    Console.WriteLine(listy.Move());
                }
                else if (command[0] == "HasNext")
                {
                    Console.WriteLine(listy.HasNext());
                }
                else if (command[0] == "Print")
                {
                    listy.Print();
                }
                else if (command[0] == "PrintAll")
                {
                    listy.PrintAll();
                }
            }
        }
    }
}
