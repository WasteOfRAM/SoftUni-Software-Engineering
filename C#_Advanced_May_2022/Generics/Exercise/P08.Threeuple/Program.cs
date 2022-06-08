using System;

namespace P08.Threeuple
{
    internal class Program
    {
        static void Main()
        {
            string inputOne = Console.ReadLine();
            string inputTwo = Console.ReadLine();
            string inputThree = Console.ReadLine();

            var lineOne = new Threeuple<string, string, string>(inputOne.Split()[0] + " " + inputOne.Split()[1], inputOne.Split()[2], inputOne.Split()[3]);
            var lineTwo = new Threeuple<string, int, bool>(inputTwo.Split()[0], int.Parse(inputTwo.Split()[1]), inputTwo.Split()[2] == "drunk");
            var lineThree = new Threeuple<string, double, string>(inputThree.Split()[0], double.Parse(inputThree.Split()[1]), inputThree.Split()[2]);

            Console.WriteLine(lineOne);
            Console.WriteLine(lineTwo);
            Console.WriteLine(lineThree);
        }
    }
}
