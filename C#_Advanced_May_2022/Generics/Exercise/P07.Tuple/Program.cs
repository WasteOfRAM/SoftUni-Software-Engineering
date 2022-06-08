using System;

namespace P07.Tuple
{
    public class Program
    {
        static void Main()
        {
            string inputOne = Console.ReadLine();
            string inputTwo = Console.ReadLine();
            string inputThree = Console.ReadLine();

            Tuple<string, string> lineOne = new Tuple<string, string>(inputOne.Split()[0] + " " + inputOne.Split()[1], inputOne.Split()[2]);
            Tuple<string, int> lineTwo = new Tuple<string, int>(inputTwo.Split()[0], int.Parse(inputTwo.Split()[1]));
            Tuple<int, double> lineThree = new Tuple<int, double>(int.Parse(inputThree.Split()[0]), double.Parse(inputThree.Split()[1]));

            Console.WriteLine($"{lineOne.Item1} -> {lineOne.Item2}");
            Console.WriteLine($"{lineTwo.Item1} -> {lineTwo.Item2}");
            Console.WriteLine($"{lineThree.Item1} -> {lineThree.Item2}");
         }
    }
}
