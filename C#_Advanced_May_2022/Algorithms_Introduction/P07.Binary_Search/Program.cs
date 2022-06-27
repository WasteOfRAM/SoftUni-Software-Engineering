using System;
using System.Linq;

namespace P07.Binary_Search
{
    internal class Program
    {
        static void Main()
        {
            var sortedArr = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine(BinarySearch(sortedArr, n));
        }

        private static int BinarySearch(int[] sortedArr, int n)
        {
            int left = 0;
            int right = sortedArr.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (sortedArr[mid] == n)
                    return mid;

                if (sortedArr[mid] < n)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }
    }
}
