using System;
using System.Collections.Generic;
using System.Text;

namespace P02.WallDestroyer
{
    internal class Program
    {
        static void Main()
        {
            var directions = new Dictionary<string, Tuple<int, int>> {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(+1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, +1)},
            };

            int holesMade = 1;
            int rodsHit = 0;
            bool electrocuted = false;

            int wallSize = int.Parse(Console.ReadLine());
            char[,] wall = new char[wallSize, wallSize];
            Vector2 vankoPosition = null;

            MatrixFill(wall, ref vankoPosition);

            string direction;
            while ((direction = Console.ReadLine()) != "End")
            {
                vankoPosition.Row += directions[direction].Item1;
                vankoPosition.Col += directions[direction].Item2;

                if(!IndexValidation(wall, vankoPosition.Row, vankoPosition.Col))
                {
                    vankoPosition.Row -= directions[direction].Item1;
                    vankoPosition.Col -= directions[direction].Item2;
                    continue;
                }

                if (wall[vankoPosition.Row, vankoPosition.Col] == 'C')
                {
                    electrocuted = true;
                    wall[vankoPosition.Row, vankoPosition.Col] = 'E';
                    holesMade++;
                    break;
                }

                if (wall[vankoPosition.Row, vankoPosition.Col] == 'R')
                {
                    vankoPosition.Row -= directions[direction].Item1;
                    vankoPosition.Col -= directions[direction].Item2;
                    rodsHit++;
                    Console.WriteLine("Vanko hit a rod!");
                    continue;
                }

                if(wall[vankoPosition.Row, vankoPosition.Col] == '*')
                {
                    Console.WriteLine($"The wall is already destroyed at position [{vankoPosition.Row}, {vankoPosition.Col}]!");
                    continue;
                }

                wall[vankoPosition.Row, vankoPosition.Col] = '*';
                holesMade++;
            }

            if(electrocuted)
                Console.WriteLine($"Vanko got electrocuted, but he managed to make {holesMade} hole(s).");
            else
            {
                wall[vankoPosition.Row, vankoPosition.Col] = 'V';
                Console.WriteLine($"Vanko managed to make {holesMade} hole(s) and he hit only {rodsHit} rod(s).");
            }

            Console.WriteLine(MatrixPrint(wall));
        }

        public static string MatrixPrint(char[,] matrix)
        {
            var sb = new StringBuilder();

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

        private static void MatrixFill(char[,] matrix, ref Vector2 position)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];

                    if (line[col] == 'V')
                    {
                        position = new Vector2(row, col);
                        matrix[row, col] = '*';
                    }
                }
            }
        }

        private static bool IndexValidation(char[,] matrix, int row, int col)
        {
            if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
                return true;

            return false;
        }
    }

    class Vector2
    {
        public Vector2(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
}
