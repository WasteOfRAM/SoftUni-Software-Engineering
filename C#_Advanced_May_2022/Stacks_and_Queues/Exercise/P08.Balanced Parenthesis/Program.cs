using System;
using System.Collections.Generic;

namespace P08.Balanced_Parenthesis
{
    internal class Program
    {
        static void Main()
        {
            // Gets the input into a char array
            char[] input = Console.ReadLine().ToCharArray();

            bool isBalanced = false;

            Stack<char> openParentheses = new Stack<char>();

            //Checks if number of chars is even if not it cant be balanced
            if (input.Length % 2 == 0)
            {
                foreach (var ch in input)
                {
                    // Puts open parentheses in a stack
                    if (ch == '(' || ch == '{' || ch == '[')
                    {
                        openParentheses.Push(ch);
                    }
                    else if (ch == ')' || ch == '}' || ch == ']')
                    {
                        char curentOpenParentheses;

                        // If the stack is empty and ch is closing parenthese returns false and breaks
                        if (openParentheses.TryPop(out curentOpenParentheses))
                        {
                            if (curentOpenParentheses == '(')
                            {
                                if (ch == ')')
                                {
                                    isBalanced = true;
                                }
                                else
                                {
                                    isBalanced = false;
                                    break;
                                }
                            }
                            else if (curentOpenParentheses == '{')
                            {
                                if (ch == '}')
                                {
                                    isBalanced = true;
                                }
                                else
                                {
                                    isBalanced = false;
                                    break;
                                }
                            }
                            else if (curentOpenParentheses == '[')
                            {
                                if (ch == ']')
                                {
                                    isBalanced = true;
                                }
                                else
                                {
                                    isBalanced = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            isBalanced = false;
                            break;
                        }
                    }
                } 
            }

            // Prints the result
            if (isBalanced)
                Console.WriteLine("YES");
            else
                Console.WriteLine("NO");
        }
    }
}
