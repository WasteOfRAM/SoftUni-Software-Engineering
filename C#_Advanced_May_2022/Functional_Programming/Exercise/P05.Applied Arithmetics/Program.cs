using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.Applied_Arithmetics
{
    internal class Program
    {
        static void Main()
        {
            //First 3 go with the delegate function
            //Operation add = n => n += 1;
            //Operation sub = n => n -= 1;
            //Operation mul = n => n *= 2;

            //int add(int n) => n += 1;
            //int sub(int n) => n -= 1;
            //int mul(int n) => n *= 2;

            Func<int, int> add = n => n += 1;
            Func<int, int> sub = n => n -= 1;
            Func<int, int> mul = n => n *= 2;

            Action<List<int>> printNumbers = numbers => Console.WriteLine(string.Join(" ", numbers));

            List<int> numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                if (command == "add")
                    numbers = numbers.Select(x => add(x)).ToList();

                if(command == "multiply")
                    numbers = numbers.Select(x => mul(x)).ToList();

                if (command == "subtract")
                    numbers = numbers.Select(x => sub(x)).ToList();

                if (command == "print")
                    printNumbers(numbers);
            }
        }

        //This goes with the first set of functions
        private delegate int Operation(int value);
    }
}
