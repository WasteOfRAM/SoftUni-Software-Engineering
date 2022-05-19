using System;
using System.Linq;

namespace P03.Primary_Diagonal
{
    internal class Program
    {
        static void Main()
        {
            int matrixSize = int.Parse(Console.ReadLine());

            int[,] matrix = new int[matrixSize, matrixSize];

            MatrixFill(matrix);

            int primaryDiagonalSum = 0;

            for (int i = 0; i < matrixSize; i++)
            {
                primaryDiagonalSum += matrix[i, i];
            }

            Console.WriteLine(primaryDiagonalSum);
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
