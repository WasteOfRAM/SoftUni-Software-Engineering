using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02.Beaver_at_Work
{
    internal class Program
    {
        static void Main()
        {
            int pondSize = int.Parse(Console.ReadLine());

            char[,] pond = new char[pondSize, pondSize];
            Vector2 beaver = null;
            int pondBranchesCout = 0;
            var branches = new List<char>();

            MatricFill(pond, ref beaver, ref pondBranchesCout);

            var directions = new Dictionary<string, Tuple<int, int>> {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(+1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, +1)},
            };

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                if (pondBranchesCout == 0)
                    break;

                beaver.Row += directions[command].Item1;
                beaver.Col += directions[command].Item2;

                if (!IndexValidation(pond, beaver.Row, beaver.Col))
                {
                    beaver.Row -= directions[command].Item1;
                    beaver.Col -= directions[command].Item2;

                    if (branches.Count > 0)
                        branches.RemoveAt(branches.Count - 1);

                    continue;
                }

                if (pond[beaver.Row, beaver.Col] == 'F')
                {
                    pond[beaver.Row, beaver.Col] = '-';

                    if (IndexValidation(pond, beaver.Row + directions[command].Item1, beaver.Col + directions[command].Item2))
                    {
                        if (directions[command].Item1 != 0)
                        {
                            if (directions[command].Item1 == 1)
                                beaver.Row = pond.GetLength(0) - 1;
                            else
                                beaver.Row = 0;
                        }
                        else
                        {
                            if (directions[command].Item2 == 1)
                                beaver.Col = pond.GetLength(0) - 1;
                            else
                                beaver.Col = 0;
                        }

                        if (char.IsLower(pond[beaver.Row, beaver.Col]))
                        {
                            branches.Add(pond[beaver.Row, beaver.Col]);
                            pondBranchesCout--;
                        }

                        pond[beaver.Row, beaver.Col] = '-';
                    }
                    else
                    {
                        if (directions[command].Item1 != 0)
                        {
                            if (directions[command].Item1 == 1)
                                beaver.Row = 0;
                            else
                                beaver.Row = pond.GetLength(0) - 1;
                        }
                        else
                        {
                            if (directions[command].Item2 == 1)
                                beaver.Col = 0;
                            else
                                beaver.Col = pond.GetLength(0) - 1;
                        }

                        if (char.IsLower(pond[beaver.Row, beaver.Col]))
                        {
                            branches.Add(pond[beaver.Row, beaver.Col]);
                            pondBranchesCout--;
                        }

                        pond[beaver.Row, beaver.Col] = '-';
                    }

                    continue;
                }

                if (char.IsLower(pond[beaver.Row, beaver.Col]))
                {
                    branches.Add(pond[beaver.Row, beaver.Col]);
                    pondBranchesCout--;
                    pond[beaver.Row, beaver.Col] = '-';

                    continue;
                }

                pond[beaver.Row, beaver.Col] = '-';
            }

            pond[beaver.Row, beaver.Col] = 'B';

            if (pondBranchesCout == 0)
                Console.WriteLine($"The Beaver successfully collect {branches.Count} wood branches: {string.Join(", ", branches)}.");
            else
                Console.WriteLine($"The Beaver failed to collect every wood branch. There are {pondBranchesCout} branches left.");

            Console.WriteLine(MatrixPrint(pond));
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

        private static bool IndexValidation(char[,] matrix, int row, int col)
        {
            if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
                return true;

            return false;
        }

        private static void MatricFill(char[,] matrix, ref Vector2 vector, ref int count)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().Replace(" ", string.Empty).ToCharArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];

                    if (line[col] == 'B')
                    {
                        vector = new Vector2(row, col);
                        matrix[row, col] = '-';
                    }

                    if (char.IsLower(line[col]))
                        count++;
                }
            }
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
