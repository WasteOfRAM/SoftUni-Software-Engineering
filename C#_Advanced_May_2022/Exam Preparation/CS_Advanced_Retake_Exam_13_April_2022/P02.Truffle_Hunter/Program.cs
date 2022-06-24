using System;
using System.Collections.Generic;
using System.Text;

namespace P02.Truffle_Hunter
{
    internal class Program
    {
        static void Main()
        {
            int forestSize = int.Parse(Console.ReadLine());
            char[,] forest = new char[forestSize, forestSize];
            MatrixFill(forest);

            var directions = new Dictionary<string, Tuple<int, int>> {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(+1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, +1)},
            };

            var playerTruffles = new Dictionary<char, int>
            {
                {'B', 0 },
                {'S', 0 },
                {'W', 0 }
            };

            int boarTruffles = 0;

            string command;
            while ((command = Console.ReadLine()) != "Stop the hunt")
            {
                int row = int.Parse(command.Split()[1]);
                int col = int.Parse(command.Split()[2]);

                if (command.Contains("Collect"))
                {
                    if (char.IsUpper(forest[row, col]))
                    {
                        playerTruffles[forest[row, col]]++;
                        forest[row, col] = '-';
                    }
                    continue;
                }

                string boarDirection = command.Split()[3];
                int eatEven = 0;

                while (row >= 0 && row < forestSize && col >= 0 && col < forestSize)
                {
                    if (char.IsUpper(forest[row, col]) && eatEven % 2 == 0)
                    {
                        forest[row, col] = '-';
                        boarTruffles++;
                    }

                    row += directions[boarDirection].Item1;
                    col += directions[boarDirection].Item2;

                    eatEven++;
                }

            }

            Console.WriteLine($"Peter manages to harvest {playerTruffles['B']} black, {playerTruffles['S']} summer, and {playerTruffles['W']} white truffles.");
            Console.WriteLine($"The wild boar has eaten {boarTruffles} truffles.");

            Console.WriteLine(MatrixPrint(forest));
        }

        public static string MatrixPrint(char[,] matrix)
        {
            var sb = new StringBuilder();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    sb.Append(matrix[row, col] + " ");
                }

                sb.ToString().TrimEnd();
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static void MatrixFill(char[,] forest)
        {
            for (int row = 0; row < forest.GetLength(0); row++)
            {
                var line = Console.ReadLine().Replace(" ", string.Empty).ToCharArray();
                for (int col = 0; col < forest.GetLength(1); col++)
                {
                    forest[row, col] = line[col];
                }
            }
        }
    }
}
