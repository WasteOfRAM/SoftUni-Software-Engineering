using System;
using System.Linq;

namespace P02.Squares_in_Matrix
{
    internal class Program
    {
        static void Main()
        {
            string matrixSize = Console.ReadLine();
            int rows = int.Parse(matrixSize.Split()[0]);
            int columns = int.Parse(matrixSize.Split()[1]);

            char[,] matrix = new char[rows, columns];

            int totalEqualSquares = 0;

            MatrixFill(matrix);

            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    char mainChar = matrix[row, col];
                    char rightChar = matrix[row, col + 1];
                    char bottomChar = matrix[row + 1, col];
                    char diagonalChar = matrix[row + 1, col + 1];

                    if (rightChar == mainChar && bottomChar == mainChar && diagonalChar == mainChar)
                    {
                        totalEqualSquares++;
                    }
                }
            }

            Console.WriteLine(totalEqualSquares);
        }

        private static void MatrixFill(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string[] matrixRow = Console.ReadLine().Split();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = char.Parse(matrixRow[col]);
                }
            }
        }
    }
}
