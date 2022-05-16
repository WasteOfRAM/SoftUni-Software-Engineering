using System;
using System.Collections.Generic;

namespace P06.Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<string> customerLine = new Queue<string>();

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                if (command == "Paid")
                {
                    foreach (var customer in customerLine)
                    {
                        Console.WriteLine(customer);
                    }

                    customerLine.Clear();
                }
                else
                {
                    customerLine.Enqueue(command);
                }
            }

            Console.WriteLine($"{customerLine.Count} people remaining.");
        }
    }
}
