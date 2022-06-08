using System;
using System.Collections.Generic;

namespace P05_06.Generic_Count_Method
{
    public class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            //Test with string
            //var listOfBox = new List<Box<string>>();

            //for (int i = 0; i < n; i++)
            //{
            //    string value = Console.ReadLine();
            //    listOfBox.Add(new Box<string>(value));
            //}

            //string compareTo = Console.ReadLine();
            //=============================


            //Test with double
            var listOfBox = new List<Box<double>>();

            for (int i = 0; i < n; i++)
            {
                double value = double.Parse(Console.ReadLine());
                listOfBox.Add(new Box<double>(value));
            }

            double compareTo = double.Parse(Console.ReadLine());

            //============================

            Console.WriteLine(LargerCount(listOfBox, compareTo));
        }

        static int LargerCount<T>(List<Box<T>> list, T compareTo)
        {
            int count = 0;

            foreach (var item in list)
            {
                if(item.CompareTo(compareTo) == 1)
                    count++;
            }

            return count;
        }
    }
}
