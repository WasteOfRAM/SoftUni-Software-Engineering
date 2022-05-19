using System;
using System.Linq;

namespace P01.Sum_Matrix_Elements
{
    internal class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();

            int rows = int.Parse(input.Split(", ")[0]);
            int columns = int.Parse(input.Split(", ")[1]);

            int[,] matrix = new int[rows, columns];

            int matrixSum = 0;

            for (int row = 0; row < rows; row++)
            {
                int[] rowInput = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
                for (int col = 0; col < columns; col++)
                {
                    matrix[row, col] = rowInput[col];
                    matrixSum += matrix[row, col];
                }
            }

            Console.WriteLine($"{rows}{Environment.NewLine}{columns}{Environment.NewLine}{matrixSum}");
        }
    }
}
