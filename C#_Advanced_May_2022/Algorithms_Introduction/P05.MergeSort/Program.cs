using System;
using System.Linq;

namespace P05.MergeSort
{
    internal class Program
    {
        static void Main()
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            numbers = MergeSort(numbers);

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
                return array;

            var middleIndex = array.Length / 2;
            var leftArray = array.Take(middleIndex).ToArray();
            var rightArray = array.Skip(middleIndex).ToArray();

            return MergeArrays(MergeSort(leftArray), MergeSort(rightArray));
        }

        private static int[] MergeArrays(int[] leftArray, int[] rightArray)
        {
            var sortedArray = new int[leftArray.Length + rightArray.Length];
            int sortedIndex = 0;
            int leftIndex = 0;
            int rightIndex = 0;

            while (leftIndex < leftArray.Length && rightIndex < rightArray.Length)
            {
                if (leftArray[leftIndex] < rightArray[rightIndex])
                    sortedArray[sortedIndex++] = leftArray[leftIndex++];
                else
                    sortedArray[sortedIndex++] = rightArray[rightIndex++];
            }

            for (int i = leftIndex; i < leftArray.Length; i++)
                sortedArray[sortedIndex++] = leftArray[i];

            for (int i = rightIndex; i < rightArray.Length; i++)
                sortedArray[sortedIndex++] = rightArray[i];

            return sortedArray;
        }
    }
}
