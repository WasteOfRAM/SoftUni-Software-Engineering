using System;

namespace P05.Snake_Moves
{
    internal class Program
    {
        static void Main()
        {
            string matrixSize = Console.ReadLine();
            int rows = int.Parse(matrixSize.Split()[0]);
            int cols = int.Parse(matrixSize.Split()[1]);

            string snake = Console.ReadLine();

            char[,] matrix = new char[rows, cols];
            int snakeIndex = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                if (row % 2 == 0)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        if (snakeIndex == snake.Length)
                            snakeIndex = 0; ;

                        matrix[row, col] = snake[snakeIndex];

                        snakeIndex++;
                    }
                }
                else
                {
                    for (int col = matrix.GetLength(1) - 1; col >= 0 ; col--)
                    {
                        if (snakeIndex == snake.Length)
                            snakeIndex = 0; ;

                        matrix[row, col] = snake[snakeIndex];

                        snakeIndex++;
                    }
                }
            }

            MatrixPrint(matrix);
        }

        private static void MatrixPrint(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}
