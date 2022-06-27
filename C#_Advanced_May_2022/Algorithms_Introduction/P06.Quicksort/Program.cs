using System;
using System.Linq;

namespace P06.Quicksort
{
    internal class Program
    {
        static void Main()
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            QuickSort(numbers, 0, numbers.Length - 1);

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void QuickSort(int[] numbers, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex)
                return;

            int pivot = startIndex;
            int leftIndex = pivot + 1;
            int rightIndex = endIndex;

            while (leftIndex <= rightIndex)
            {
                if (numbers[leftIndex] > numbers[pivot] && numbers[rightIndex] < numbers[pivot])
                {
                    Swap(numbers, leftIndex, rightIndex);
                }

                if (numbers[leftIndex] <= numbers[pivot])
                    leftIndex++;

                if (numbers[rightIndex] >= numbers[pivot])
                    rightIndex--;
            }

            Swap(numbers, pivot, rightIndex);


            bool isLeftSubArrSmaler = rightIndex - 1 - startIndex < endIndex - (rightIndex + 1);

            if (isLeftSubArrSmaler)
            {
                QuickSort(numbers, startIndex, rightIndex - 1);
                QuickSort(numbers, rightIndex + 1, endIndex);
            }
            else
            {
                QuickSort(numbers, rightIndex + 1, endIndex);
                QuickSort(numbers, startIndex, rightIndex - 1);
            }
        }

        private static void Swap(int[] arr, int first, int second)
        {
            var temp = arr[first];
            arr[first] = arr[second];
            arr[second] = temp;
        }
    }
}
