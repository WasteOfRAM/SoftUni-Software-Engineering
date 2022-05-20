using System;
using System.Linq;

namespace P03.Maximal_Sum
{
    internal class Program
    {
        static void Main()
        {
            string matrixSize = Console.ReadLine();
            int rows = int.Parse(matrixSize.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
            int cols = int.Parse(matrixSize.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);

            int[,] matrix = new int[rows, cols];

            int subMatrixSum = 0;

            //Submatrix starting position
            int subMatrixRow = 0;
            int subMatrixCol = 0;

            MatrixFill(matrix);

            for (int row = 0; row < matrix.GetLength(0) - 2; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 2; col++)
                {
                    int curentMatrixSum = matrix[row, col] + matrix[row, col + 1] + matrix[row, col + 2]
                    + matrix[row + 1, col] + matrix[row + 1, col + 1] + matrix[row + 1, col + 2]
                    + matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];

                    if (curentMatrixSum > subMatrixSum)
                    {
                        subMatrixSum = curentMatrixSum;
                        subMatrixRow = row;
                        subMatrixCol = col;
                    }
                }


            }

            Console.WriteLine($"Sum = {subMatrixSum}");

            for (int row = subMatrixRow; row < subMatrixRow + 3; row++)
            {
                for (int col = subMatrixCol; col < subMatrixCol + 3; col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }

        }

        private static void MatrixFill(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] matrixRow = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = matrixRow[col];
                }
            }
        }
    }
}
