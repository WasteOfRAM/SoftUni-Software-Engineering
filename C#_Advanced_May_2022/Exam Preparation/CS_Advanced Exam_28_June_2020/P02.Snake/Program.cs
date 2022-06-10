using System;
using System.Collections.Generic;

namespace P02.Snake
{
    internal class Program
    {
        static void Main()
        {
            int territorySize = int.Parse(Console.ReadLine());

            var territory = new char[territorySize, territorySize];

            Vector2 snakePosition = null;
            Vector2 burrowOne = null;
            Vector2 burrowTwo = null;

            const int FOOD_NEEDED = 10;
            int foodQuantity = 0;

            var directions = new Dictionary<string, Tuple<int, int>> {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(+1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, +1)},
            };

            for (int row = 0; row < territory.GetLength(0); row++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < territory.GetLength(1); col++)
                {
                    territory[row, col] = line[col];

                    if (line[col] == 'S')
                    {
                        snakePosition = new Vector2(row, col);
                        territory[row, col] = '.';
                    }

                    if (line[col] == 'B' && burrowOne == null)
                        burrowOne = new Vector2(row, col);
                    else if (line[col] == 'B')
                        burrowTwo = new Vector2(row, col);
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                snakePosition.Row += directions[command].Item1;
                snakePosition.Col += directions[command].Item2;

                if (!IndexValidation(territory, snakePosition.Row, snakePosition.Col))
                    break;

                if (territory[snakePosition.Row, snakePosition.Col] == '*')
                {
                    territory[snakePosition.Row, snakePosition.Col] = '.';
                    foodQuantity++;

                    if (foodQuantity == FOOD_NEEDED)
                        break;
                }
                else if (territory[snakePosition.Row, snakePosition.Col] == 'B')
                {
                    if (snakePosition.CompareTo(burrowOne) == 0)
                    {
                        territory[snakePosition.Row, snakePosition.Col] = '.';
                        snakePosition.Row = burrowTwo.Row;
                        snakePosition.Col = burrowTwo.Col;
                        territory[snakePosition.Row, snakePosition.Col] = '.';
                    }
                    else
                    {
                        territory[snakePosition.Row, snakePosition.Col] = '.';
                        snakePosition.Row = burrowOne.Row;
                        snakePosition.Col = burrowOne.Col;
                        territory[snakePosition.Row, snakePosition.Col] = '.';
                    }
                }
                else
                {
                    territory[snakePosition.Row, snakePosition.Col] = '.';
                }
            }



            if (foodQuantity == FOOD_NEEDED)
            {
                Console.WriteLine("You won! You fed the snake.");
                Console.WriteLine($"Food eaten: {foodQuantity}");
                territory[snakePosition.Row, snakePosition.Col] = 'S';
            }
            else
            {
                Console.WriteLine("Game over!");
                Console.WriteLine($"Food eaten: {foodQuantity}");
            }

            MatrixPrint(territory);
        }

        private static bool IndexValidation(char[,] matrix, int row, int col)
        {
            if (row >= 0 && col >= 0 && row < matrix.GetLength(0) && col < matrix.GetLength(1))
                return true;

            return false;
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

    class Vector2 : IComparable
    {
        public Vector2(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }

        public int CompareTo(object obj)
        {
            Vector2 other = obj as Vector2;

            if (this.Row == other.Row && this.Col == other.Col)
                return 0;

            return -1;
        }
    }
}
