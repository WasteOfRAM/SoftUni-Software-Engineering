using System;

namespace P04.Symbol_in_Matrix
{
    internal class Program
    {
        static void Main()
        {
            int matrixSize = int.Parse(Console.ReadLine());

            char[,] matrix = new char[matrixSize, matrixSize];

            MatrixFill(matrix);

            char symbolToLookFor = char.Parse(Console.ReadLine());

            Console.WriteLine(MatrixSearch(matrix, symbolToLookFor));
        }

        private static string MatrixSearch(char[,] matrix, char lookingFor)
        {
            bool isFound = false;
            string result = string.Empty;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == lookingFor)
                    {
                        result = $"({row}, {col})";
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                    break;
            }

            if (isFound)
                return result;
            else
                return $"{lookingFor} does not occur in the matrix";
        }

        private static void MatrixFill(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] rowInput = Console.ReadLine().ToCharArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = rowInput[col];
                }
            }
        }
    }
}
