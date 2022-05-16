using System;
using System.Collections.Generic;
using System.Linq;

namespace P02.Stack_Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse);

            Stack<int> stack = new Stack<int>(input);

            string command;
            while ((command = Console.ReadLine().ToLower()) != "end")
            {
                string[] cmdSplit = command.Split(' ');

                if (cmdSplit[0].ToLower() == "add")
                {
                    stack.Push(int.Parse(cmdSplit[1]));
                    stack.Push(int.Parse(cmdSplit[2]));
                }
                else if (cmdSplit[0].ToLower() == "remove")
                {
                    int countToRemove = int.Parse(cmdSplit[1]);

                    if (countToRemove > stack.Count)
                        continue;

                    for (int i = 0; i < countToRemove; i++)
                    {
                        stack.Pop();
                    }
                }
            }

            Console.WriteLine($"Sum: {stack.Sum()}");
        }
    }
}
