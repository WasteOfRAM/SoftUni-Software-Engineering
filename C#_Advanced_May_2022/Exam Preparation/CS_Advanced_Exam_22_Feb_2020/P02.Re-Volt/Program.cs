using System;
using System.Collections.Generic;
using System.Text;

namespace P02.Re_Volt
{
    internal class Program
    {
        static void Main()
        {
            int matrixSize = int.Parse(Console.ReadLine());
            int commandsCount = int.Parse(Console.ReadLine());

            char[,] matrix = new char[matrixSize, matrixSize];
            Vector2 playerPosition = null;

            var directions = new Dictionary<string, Tuple<int, int>>
            {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, 1)},
            };

            for (int row = 0; row < matrixSize; row++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < matrixSize; col++)
                {
                    matrix[row, col] = line[col];
                    if(line[col] == 'f')
                    {
                        playerPosition = new Vector2(row, col);
                        matrix[row, col] = '-';
                    }
                }
            }

            while (commandsCount > 0)
            {
                commandsCount--;
                string direction = Console.ReadLine();

                playerPosition.Row += directions[direction].Item1;
                playerPosition.Col += directions[direction].Item2;

                MapEdgeHandler(matrix, playerPosition, direction);

                if (matrix[playerPosition.Row, playerPosition.Col] == 'B')
                {
                    playerPosition.Row += directions[direction].Item1;
                    playerPosition.Col += directions[direction].Item2;

                    MapEdgeHandler(matrix, playerPosition, direction);
                }

                if (matrix[playerPosition.Row, playerPosition.Col] == 'T')
                {
                    playerPosition.Row -= directions[direction].Item1;
                    playerPosition.Col -= directions[direction].Item2;

                    MapEdgeHandler(matrix, playerPosition, direction);
                }

                if (matrix[playerPosition.Row, playerPosition.Col] == 'F')
                {
                    break;
                }
            }

            if (matrix[playerPosition.Row, playerPosition.Col] == 'F')
            {
                matrix[playerPosition.Row, playerPosition.Col] = 'f';
                Console.WriteLine("Player won!");
            }
            else
            {
                matrix[playerPosition.Row, playerPosition.Col] = 'f';
                Console.WriteLine("Player lost!");
            }

            Console.WriteLine(MatrixPrint(matrix));
        }

        private static void MapEdgeHandler(char[,] matrix, Vector2 playerPosition, string direction)
        {
            if (!IndexValidation(matrix, playerPosition.Row, playerPosition.Col))
            {
                if (direction == "up")
                    playerPosition.Row = matrix.GetLength(0) - 1;
                else if (direction == "down")
                    playerPosition.Row = 0;
                else if (direction == "left")
                    playerPosition.Col = matrix.GetLength(1) - 1;
                else if (direction == "right")
                    playerPosition.Col = 0;
            }
        }

        private static bool IndexValidation(char[,] matrix, int row, int col)
        {
            if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
                return true;

            return false;
        }

        private static string MatrixPrint(char[,] matrix)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    sb.Append(matrix[row, col]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    class Vector2
    {
        public Vector2(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
}
