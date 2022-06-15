using System;
using System.Collections.Generic;

namespace P02.Warships
{
    internal class Program
    {
        static void Main()
        {
            int fieldSize = int.Parse(Console.ReadLine());

            char[,] field = new char[fieldSize, fieldSize];

            var attackCommands = new List<Vector2>();
            var attacksInput = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);

            Tuple<int, int>[] mineRadius =
            {
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(1, 1)
            };

            int playerOneShipsCount = 0;
            int playerTwoShipsCount = 0;
            int totalDestroyed = 0;

            foreach (var attack in attacksInput)
            {
                attackCommands.Add(new Vector2(attack));
            }

            for (int row = 0; row < field.GetLength(0); row++)
            {
                var line = Console.ReadLine().Split();
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if (char.Parse(line[col]) == '<')
                        playerOneShipsCount++;

                    if (char.Parse(line[col]) == '>')
                        playerTwoShipsCount++;

                    field[row, col] = char.Parse(line[col]);
                }
            }

            foreach (var coordinate in attackCommands)
            {
                if (IndexValidation(field, coordinate.Row, coordinate.Col))
                {
                    if (field[coordinate.Row, coordinate.Col] == '<')
                    {
                        playerOneShipsCount--;
                        totalDestroyed++;
                        field[coordinate.Row, coordinate.Col] = 'X';
                    }
                    else if (field[coordinate.Row, coordinate.Col] == '>')
                    {
                        playerTwoShipsCount--;
                        totalDestroyed++;
                        field[coordinate.Row, coordinate.Col] = 'X';
                    }
                    else if (field[coordinate.Row, coordinate.Col] == '#')
                    {
                        field[coordinate.Row, coordinate.Col] = 'X';
                        foreach (var blastRadius in mineRadius)
                        {
                            int hitRow = coordinate.Row + blastRadius.Item1;
                            int hitCol = coordinate.Col + blastRadius.Item2;

                            if (IndexValidation(field, hitRow, hitCol))
                            {
                                if (field[hitRow, hitCol] == '<')
                                {
                                    playerOneShipsCount--;
                                    totalDestroyed++;
                                }

                                if (field[hitRow, hitCol] == '>')
                                {
                                    playerTwoShipsCount--;
                                    totalDestroyed++;
                                }

                                field[hitRow, hitCol] = 'X';
                            }
                        }
                    }
                }
            }

            if (playerTwoShipsCount == 0)
            {
                Console.WriteLine($"Player One has won the game! {totalDestroyed} ships have been sunk in the battle.");
            }
            else if (playerOneShipsCount == 0)
            {
                Console.WriteLine($"Player Two has won the game! {totalDestroyed} ships have been sunk in the battle.");
            }
            else
            {
                Console.WriteLine($"It's a draw! Player One has {playerOneShipsCount} ships left. Player Two has {playerTwoShipsCount} ships left.");
            }
        }

        static bool IndexValidation(char[,] matrix, int row, int col)
        {
            if (row >= 0 && col >= 0 && row < matrix.GetLength(0) && col < matrix.GetLength(1))
                return true;

            return false;
        }
    }

    class Vector2
    {
        public Vector2(string coordinates)
        {
            this.Row = int.Parse(coordinates.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]);
            this.Col = int.Parse(coordinates.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
}
