using System;
using System.Linq;

namespace P02.Sum_Matrix_Columns
{
    internal class Program
    {
        static void Main()
        {
            int[] matrixSizeInput = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int rows = matrixSizeInput[0];
            int columns = matrixSizeInput[1];

            int[,] matrix = new int[rows, columns];
            MatrixFill(matrix);

            int columnSum = 0;

            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    columnSum += matrix[row, col];
                }

                Console.WriteLine(columnSum);
                columnSum = 0;
            }
        }

        private static void MatrixFill(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] rowInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = rowInput[col];
                }
            }
        }
    }
}
