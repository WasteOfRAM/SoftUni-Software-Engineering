using System;
using System.Collections.Generic;
using System.Linq;

namespace P07.Custom_Comparator
{
    public class CustomComparator : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x % 2 == 0 && y % 2 != 0)
                return -1;
            else if (x % 2 != 0 && y % 2 == 0)
                return 1;
            else if (x < y)
                return -1;
            else if (x > y)
                return 1;

            return 0;
        }
    }

    internal class Program
    {
        static void Main()
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();


            //Lecturer solution
            //Func<int, int, int> sortFunction = (x, y) =>
            //    (x % 2 == 0 && y % 2 != 0) ? -1 : (x % 2 != 0 && y % 2 == 0) ? 1 : x > y ? 1 : x < y ? -1 : 0;

            //Array.Sort(arr, (x, y) => sortFunction(x, y));

            Array.Sort(arr, new CustomComparator());

            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
