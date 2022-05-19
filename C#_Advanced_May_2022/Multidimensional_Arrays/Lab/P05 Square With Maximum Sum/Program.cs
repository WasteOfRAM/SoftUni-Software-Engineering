using System;
using System.Linq;

namespace P05_Square_With_Maximum_Sum
{
    internal class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();
            int rows = int.Parse(input.Split(", ")[0]);
            int columns = int.Parse(input.Split(", ")[1]);

            int[,] matrix = new int[rows, columns];

            MatrixFill(matrix);

            int maxSum = int.MinValue;
            string maxSumMatrix = string.Empty;

            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    int curentSum = matrix[row, col] + matrix[row + 1, col] + matrix[row, col + 1] + matrix[row + 1, col + 1];

                    if (curentSum > maxSum)
                    {
                        maxSumMatrix = $"{matrix[row, col]} {matrix[row, col + 1]}{Environment.NewLine}{matrix[row + 1, col]} {matrix[row + 1, col + 1]}";
                        maxSum = curentSum;
                    }
                }
            }

            Console.WriteLine(maxSumMatrix);
            Console.WriteLine(maxSum);
        }


        private static void MatrixFill(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] rowInput = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = rowInput[col];
                }
            }
        }
    }
}
