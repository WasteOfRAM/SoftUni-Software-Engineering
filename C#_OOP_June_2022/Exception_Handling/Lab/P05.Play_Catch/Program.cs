using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.Play_Catch
{
    internal class Program
    {
        private static int[] numbers;

        static void Main(string[] args)
        {
            numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int exceptionCounter = 0;

            while (exceptionCounter < 3)
            {
                var command = Console.ReadLine().Split();

                try
                {
                    ComandProcesing(command);
                }
                catch (IndexOutOfRangeException)
                {
                    exceptionCounter++;
                    Console.WriteLine("The index does not exist!");
                }
                catch (FormatException)
                {
                    exceptionCounter++;
                    Console.WriteLine("The variable is not in the correct format!");
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }

        private static void ComandProcesing(string[] command)
        {
            string commandType = command[0];

            if (commandType == "Replace")
                Replace(int.Parse(command[1]), int.Parse(command[2]));
            else if (commandType == "Print")
                Print(int.Parse(command[1]), int.Parse(command[2]));
            else if (commandType == "Show")
                Show(int.Parse(command[1]));
        }

        private static void Replace(int index, int element)
        {
            numbers[index] = element;
        }

        private static void Print(int start, int end)
        {
            var numbersToPrint = new List<int>();

            for (int i = start; i <= end; i++)
            {
                numbersToPrint.Add(numbers[i]);
            }

            Console.WriteLine(string.Join(", ", numbersToPrint));
        }

        private static void Show(int index)
        {
            Console.WriteLine(numbers[index]);
        }
    }
}
