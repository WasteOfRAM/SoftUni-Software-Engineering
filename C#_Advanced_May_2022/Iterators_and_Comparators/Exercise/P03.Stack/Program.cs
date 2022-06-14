using System;
using System.Linq;

namespace P03.Stack
{
    public class Program
    {
        static void Main()
        {
            var stack = new Stack<string>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (tokens[0] == "Push")
                {
                    var elements = tokens.Skip(1).Select(e => e.Split(",", StringSplitOptions.RemoveEmptyEntries).First()).ToArray();
                    stack.Push(elements);
                }
                else if (tokens[0] == "Pop")
                {
                    try
                    {
                        stack.Pop();
                    }
                    catch (ArgumentException ae)
                    {

                        Console.WriteLine(ae.Message);
                    }
                }

            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }
    }
}
