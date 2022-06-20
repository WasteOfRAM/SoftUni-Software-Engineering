using System;

namespace P02.Recursive_Factorial
{
    internal class Program
    {
        static void Main()
        {
            int num = int.Parse(Console.ReadLine());

            Console.WriteLine(GetFactorial(num));
        }

        private static int GetFactorial(int num)
        {
            if (num == 0)
                return 1;

            return num * GetFactorial(num - 1);
        }
    }
}
