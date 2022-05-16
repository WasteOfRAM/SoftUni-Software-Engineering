using System;
using System.Collections.Generic;

namespace P04.Matching_Brackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string expresion = Console.ReadLine();

            Stack<int> bracketsIndexes = new Stack<int>();

            for (int i = 0; i < expresion.Length; i++)
            {
                if (expresion[i] == '(')
                {
                    bracketsIndexes.Push(i);
                }
                else if (expresion[i] == ')')
                {
                    int startIndex = bracketsIndexes.Pop();
                    int endIndex = i + 1;

                    Console.WriteLine(expresion.Substring(startIndex, endIndex - startIndex));
                }
            }
        }
    }
}
