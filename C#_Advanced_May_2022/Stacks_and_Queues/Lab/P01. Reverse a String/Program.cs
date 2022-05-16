using System;
using System.Collections.Generic;

namespace P01._Reverse_a_String
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Stack<char> inputStack = new Stack<char>(input);
            int stackSize = inputStack.Count;

            for (int i = 0; i < stackSize; i++)
            {
                Console.Write(inputStack.Pop());
            }
        }
    }
}
