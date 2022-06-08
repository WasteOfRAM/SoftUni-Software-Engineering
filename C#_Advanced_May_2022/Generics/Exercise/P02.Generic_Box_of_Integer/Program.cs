using System;
using System.Collections.Generic;

namespace P02.Generic_Box_of_Integer
{
    public class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            var listOfBox = new List<Box<int>>();

            for (int i = 0; i < n; i++)
            {
                int value = int.Parse(Console.ReadLine());
                listOfBox.Add(new Box<int>(value));
            }

            foreach (var item in listOfBox)
            {
                Console.WriteLine(item);
            }
        }
    }
}
