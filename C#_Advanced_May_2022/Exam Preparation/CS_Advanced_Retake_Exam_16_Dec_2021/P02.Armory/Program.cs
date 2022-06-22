using System;
using System.Collections.Generic;

namespace P02.Armory
{
    internal class Program
    {
        static void Main()
        {
            int armorySize = int.Parse(Console.ReadLine());

            char[,] armory = new char[armorySize, armorySize];

            Vector2 officer = null;
            Vector2 mirrorOne = null;
            Vector2 mirrorTwo = null;

            MatrixFill(armory, ref officer, ref mirrorOne, ref mirrorTwo);

            var directions = new Dictionary<string, Tuple<int, int>> {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(+1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, +1)},
            };

            int coinsSpend = 0;

            while (coinsSpend < 65)
            {
                string direction = Console.ReadLine();

                officer.Row += directions[direction].Item1;
                officer.Col += directions[direction].Item2;

                if (!IndexValidation(armory, officer.Row, officer.Col))
                    break;

                if (armory[officer.Row, officer.Col] == 'M')
                {
                    if(officer.Row == mirrorOne.Row && officer.Col == mirrorOne.Col)
                    {
                        officer.Row = mirrorTwo.Row;
                        officer.Col = mirrorTwo.Col;

                        armory[mirrorOne.Row, mirrorOne.Col] = '-';
                        armory[mirrorTwo.Row, mirrorTwo.Col] = '-';
                    }
                    else
                    {
                        officer.Row = mirrorOne.Row;
                        officer.Col = mirrorOne.Col;

                        armory[mirrorOne.Row, mirrorOne.Col] = '-';
                        armory[mirrorTwo.Row, mirrorTwo.Col] = '-';
                    }

                    continue;
                }

                if(char.IsDigit(armory[officer.Row, officer.Col]))
                {
                    //coinsSpend += armory[officer.Row, officer.Col] - '0';
                    coinsSpend += (int)char.GetNumericValue(armory[officer.Row, officer.Col]);

                    armory[officer.Row, officer.Col] = '-';
                }
            }

            if (coinsSpend < 65)
            {
                Console.WriteLine("I do not need more swords!");
            }
            else
            {
                armory[officer.Row, officer.Col] = 'A';
                Console.WriteLine("Very nice swords, I will come back for more!");
            }

            Console.WriteLine($"The king paid {coinsSpend} gold coins.");

            for (int row = 0; row < armory.GetLength(0); row++)
            {
                for (int col = 0; col < armory.GetLength(1); col++)
                {
                    Console.Write(armory[row, col]);
                }
                Console.WriteLine();
            }
        }

        private static bool IndexValidation(char[,] matrix, int row, int col)
        {
            if (row >= 0 && col >= 0 && row < matrix.GetLength(0) && col < matrix.GetLength(1))
                return true;

            return false;
        }

        private static void MatrixFill(char[,] armory, ref Vector2 officer, ref Vector2 mirrorOne, ref Vector2 mirrorTwo)
        {
            for (int row = 0; row < armory.GetLength(0); row++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < armory.GetLength(1); col++)
                {
                    armory[row, col] = line[col];

                    if (line[col] == 'A')
                    {
                        officer = new Vector2(row, col);
                        armory[row, col] = '-';
                    }

                    if (line[col] == 'M' && mirrorOne != null)
                        mirrorTwo = new Vector2(row, col);

                    if (line[col] == 'M' && mirrorOne == null)
                        mirrorOne = new Vector2(row, col);
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
