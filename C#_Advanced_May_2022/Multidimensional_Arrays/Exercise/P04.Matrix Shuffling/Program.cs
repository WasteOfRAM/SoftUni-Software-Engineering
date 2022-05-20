using System;
using System.Linq;

namespace P04.Matrix_Shuffling
{
    internal class Program
    {
        static void Main()
        {
            string matrixSize = Console.ReadLine();
            int rows = int.Parse(matrixSize.Split(' ')[0]);
            int cols = int.Parse(matrixSize.Split(' ')[1]);

            string[,] matrix = new string[rows, cols];

            MatrixFill(matrix);

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] cmdSplit = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (CommandValidation(cmdSplit, matrix))
                {
                    int row1 = int.Parse(cmdSplit[1]);
                    int col1 = int.Parse(cmdSplit[2]);
                    int row2 = int.Parse(cmdSplit[3]);
                    int col2 = int.Parse(cmdSplit[4]);

                    string swap1 = matrix[row1, col1];
                    string swap2 = matrix[row2, col2];

                    matrix[row1, col1] = swap2;
                    matrix[row2, col2] = swap1;

                    MatrixPrint(matrix);
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
        }

        private static void MatrixPrint(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        private static void MatrixFill(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string[] matrixRow = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = matrixRow[col];
                }
            }
        }

        private static bool CommandValidation(string[] command, string[,] matrix)
        {
            if (command.Length == 5 && command[0] == "swap")
            {
                int row1 = int.Parse(command[1]);
                int col1 = int.Parse(command[2]);
                int row2 = int.Parse(command[3]);
                int col2 = int.Parse(command[4]);

                if (row1 >= 0 && row1 < matrix.GetLength(0) && row2 >= 0 && row2 < matrix.GetLength(0) && col1 >= 0 && col2 >= 0 && col1 < matrix.GetLength(1) && col2 < matrix.GetLength(1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
