using System;
using System.Linq;

namespace P04.Add_VAT
{
    internal class Program
    {
        static void Main()
        {
            Func<double, double> addVAT = price => price *= 1.20d;

            double[] numbers = Console.ReadLine().Split(", ").Select(double.Parse).Select(addVAT).ToArray();

            numbers.ToList().ForEach(str => Console.WriteLine($"{str:f2}"));
        }
    }
}
