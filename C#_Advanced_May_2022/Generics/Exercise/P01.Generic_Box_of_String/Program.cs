using System;
using System.Collections.Generic;

namespace P01.Generic_Box_of_String
{
    public class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            var listOfBox = new List<Box<string>>();

            for (int i = 0; i < n; i++)
            {
                string value = Console.ReadLine();
                listOfBox.Add(new Box<string>(value));
            }

            foreach (var item in listOfBox)
            {
                Console.WriteLine(item);
            }
        }
    }
}
