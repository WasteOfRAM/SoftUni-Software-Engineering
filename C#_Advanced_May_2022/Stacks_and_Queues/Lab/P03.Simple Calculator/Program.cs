using System;
using System.Collections.Generic;

namespace P03.Simple_Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputSplit = Console.ReadLine().Split(' ');
            Array.Reverse(inputSplit);

            Stack<string> expresion = new Stack<string>(inputSplit);

            int result = int.Parse(expresion.Pop());

            while (expresion.Count > 0)
            {
                char sign = char.Parse(expresion.Pop());

                if (sign == '+')
                {
                    result += int.Parse(expresion.Pop());
                }
                else
                {
                    result -= int.Parse(expresion.Pop());
                }
            }

            Console.WriteLine(result);
        }
    }
}
