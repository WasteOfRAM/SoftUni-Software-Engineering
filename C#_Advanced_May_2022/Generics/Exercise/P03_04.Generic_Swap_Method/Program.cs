using System;
using System.Collections.Generic;

namespace P03_04.Generic_Swap_Method
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

            //=============================

            //Test with integer
            var listOfBox = new List<Box<int>>();

            for (int i = 0; i < n; i++)
            {
                int value = int.Parse(Console.ReadLine());
                listOfBox.Add(new Box<int>(value));
            }

            //============================

            string indexes = Console.ReadLine();
            int indexOne = int.Parse(indexes.Split()[0]);
            int indexTwo = int.Parse(indexes.Split()[1]);

            GenericSwap(listOfBox, indexOne, indexTwo);

            foreach (var item in listOfBox)
            {
                Console.WriteLine(item);
            }
        }

        static void GenericSwap<T>(List<T> list, int indexOne, int indexTwo)
        {
            T firstElem = list[indexOne];

            list[indexOne] = list[indexTwo];
            list[indexTwo] = firstElem;
        }
    }
}
