using System;
using System.Collections.Generic;

namespace P02.Bee
{
    internal class Program
    {
        static void Main()
        {
            int territorySize = int.Parse(Console.ReadLine());

            char[,] territory = new char[territorySize, territorySize];

            Vector2 beePosition = null;

            int flowersCount = 0;
            const int FLOWERS_NEEDED = 5;

            var directions = new Dictionary<string, Tuple<int, int>>
            {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, 1)},
            };

            for (int row = 0; row < territory.GetLength(0); row++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < territory.GetLength(1); col++)
                {
                    territory[row, col] = line[col];

                    if (line[col] == 'B')
                    {
                        beePosition = new Vector2(row, col);
                        territory[row, col] = '.';
                    }
                }
            }

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                beePosition.Row += directions[command].Item1;
                beePosition.Col += directions[command].Item2;

                if (!IndexValidation(territory, beePosition.Row, beePosition.Col))
                    break;

                if (territory[beePosition.Row, beePosition.Col] == 'O')
                {
                    territory[beePosition.Row, beePosition.Col] = '.';
                    beePosition.Row += directions[command].Item1;
                    beePosition.Col += directions[command].Item2;

                    if (!IndexValidation(territory, beePosition.Row, beePosition.Col))
                        break;
                }

                if (territory[beePosition.Row, beePosition.Col] == 'f')
                {
                    flowersCount++;
                    territory[beePosition.Row, beePosition.Col] = '.';
                }
            }

            if (IndexValidation(territory, beePosition.Row, beePosition.Col))
                territory[beePosition.Row, beePosition.Col] = 'B';
            else
                Console.WriteLine("The bee got lost!");

            if(flowersCount >= FLOWERS_NEEDED)
                Console.WriteLine($"Great job, the bee managed to pollinate {flowersCount} flowers!");
            else
                Console.WriteLine($"The bee couldn't pollinate the flowers, she needed {FLOWERS_NEEDED - flowersCount} flowers more");

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
