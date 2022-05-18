using System;
using System.Collections.Generic;
using System.Text;

namespace P09.Simple_Text_Editor
{
    internal class Program
    {
        static void Main()
        {
            int operations = int.Parse(Console.ReadLine());

            Stack<string> undoStack = new Stack<string>();

            StringBuilder argument = new StringBuilder();

            for (int i = 0; i < operations; i++)
            {
                string[] commandSplit = Console.ReadLine().Split();

                if (commandSplit[0] == "1")
                {
                    undoStack.Push(argument.ToString());

                    argument.Append(commandSplit[1]);
                }
                else if (commandSplit[0] == "2")
                {
                    undoStack.Push(argument.ToString());

                    argument.Remove(argument.Length - int.Parse(commandSplit[1]), int.Parse(commandSplit[1]));
                }
                else if (commandSplit[0] == "3")
                {
                    Console.WriteLine(argument[int.Parse(commandSplit[1]) - 1]);
                }
                else if (commandSplit[0] == "4")
                {
                    argument = new StringBuilder(undoStack.Pop());
                }
            }
        }
    }
}
