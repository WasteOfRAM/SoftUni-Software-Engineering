using System;
using System.Collections.Generic;
using System.Linq;

namespace P09.Miner
{
    internal class Program
    {
        static void Main()
        {
            int fieldSize = int.Parse(Console.ReadLine());

            string[] commands = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            char[,] field = new char[fieldSize, fieldSize];

            Tuple<int, int> playerStart = null;
            Tuple<int, int> endOfRout = null;
            int coalCount = 0;

            MatrixFill(field, ref playerStart, ref endOfRout, ref coalCount);

            Player playerPosition = new Player(playerStart.Item1, playerStart.Item2);

            Dictionary<string, Tuple<int, int>> directions = new Dictionary<string, Tuple<int, int>>
            {
                {"left", new Tuple<int, int>(0, -1) },
                {"right", new Tuple<int, int>(0, 1) },
                {"up", new Tuple<int, int>(-1, 0) },
                {"down", new Tuple<int, int>(1, 0) }
            };

            bool foundExit = false;

            foreach (var command in commands)
            {
                var direction = directions[command];
                int directionRow = playerPosition.Row + direction.Item1;
                int directionCol = playerPosition.Col + direction.Item2;

                if (IndexValidation(directionRow, directionCol, fieldSize))
                {
                    playerPosition.Row = directionRow;
                    playerPosition.Col = directionCol;

                    if (field[playerPosition.Row, playerPosition.Col] == 'c')
                    {
                        coalCount--;

                        if (coalCount == 0)
                            break;

                        field[playerPosition.Row, playerPosition.Col] = '*';
                    }
                    else if (field[playerPosition.Row, playerPosition.Col] == 'e')
                    {
                        foundExit = true;
                        break;
                    }
                }
            }

            if (coalCount == 0)
            {
                Console.WriteLine($"You collected all coals! ({playerPosition.Row}, {playerPosition.Col})");
            }
            else if (foundExit)
            {
                Console.WriteLine($"Game over! ({playerPosition.Row}, {playerPosition.Col})");
            }
            else
            {
                Console.WriteLine($"{coalCount} coals left. ({playerPosition.Row}, {playerPosition.Col})");
            }
        }



        private static void MatrixFill(char[,] matrix, ref Tuple<int, int> playerStart, ref Tuple<int, int> endOfRout, ref int coalCount)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] matrixRow = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrixRow[col] == 's')
                    {
                        playerStart = new Tuple<int, int>(row, col);
                    }
                    else if (matrixRow[col] == 'e')
                    {
                        endOfRout = new Tuple<int, int>(row, col);
                    }
                    else if (matrixRow[col] == 'c')
                    {
                        coalCount++;
                    }

                    matrix[row, col] = matrixRow[col];
                }
            }
        }

        private static bool IndexValidation(int row, int col, int matrixSize)
        {
            if (row >= 0 && row < matrixSize && col >= 0 && col < matrixSize)
            {
                return true;
            }

            return false;
        }
    }

    class Player
    {
        public Player(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}
