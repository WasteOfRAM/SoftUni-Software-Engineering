using System;
using System.Collections.Generic;
using System.Text;

namespace P02.Selling
{
    internal class Program
    {
        static void Main()
        {
            int bakerySize = int.Parse(Console.ReadLine());

            char[,] bakery = new char[bakerySize, bakerySize];

            const int TARGET_MONEY = 50;
            int money = 0;

            Vector2 youAreHere = null;
            Vector2 pilarOne = null;
            Vector2 pilarTwo = null;

            var directions = new Dictionary<string, Tuple<int, int>>
            {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, 1)},
            };

            for (int row = 0; row < bakery.GetLength(0); row++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < bakery.GetLength(1); col++)
                {
                    bakery[row, col] = line[col];

                    if (line[col] == 'S')
                    {
                        youAreHere = new Vector2(row, col);
                        bakery[row, col] = '-';
                    }

                    if (line[col] == 'O')
                    {
                        if (pilarOne == null)
                            pilarOne = new Vector2(row, col);
                        else
                            pilarTwo = new Vector2(row, col);
                    }
                }
            }

            while (money < TARGET_MONEY)
            {
                string direction = Console.ReadLine();

                youAreHere.Row += directions[direction].Item1;
                youAreHere.Column += directions[direction].Item2;

                if (!IndexValidation(bakery, youAreHere.Row, youAreHere.Column))
                    break;

                if (bakery[youAreHere.Row, youAreHere.Column] == 'O')
                {
                    if (youAreHere.CompareTo(pilarOne) == 0)
                    {
                        bakery[youAreHere.Row, youAreHere.Column] = '-';
                        youAreHere.Row = pilarTwo.Row;
                        youAreHere.Column = pilarTwo.Column;
                        bakery[youAreHere.Row, youAreHere.Column] = '-';
                    }
                    else
                    {
                        bakery[youAreHere.Row, youAreHere.Column] = '-';
                        youAreHere.Row = pilarOne.Row;
                        youAreHere.Column = pilarOne.Column;
                        bakery[youAreHere.Row, youAreHere.Column] = '-';
                    }
                }
                else if (bakery[youAreHere.Row, youAreHere.Column] != '-')
                {
                    money += (int)char.GetNumericValue(bakery[youAreHere.Row, youAreHere.Column]);
                    bakery[youAreHere.Row, youAreHere.Column] = '-';
                }
            }

            if(IndexValidation(bakery, youAreHere.Row, youAreHere.Column))
            {
                bakery[youAreHere.Row, youAreHere.Column] = 'S';
                Console.WriteLine("Good news! You succeeded in collecting enough money!");
            }
            else
            {
                Console.WriteLine("Bad news, you are out of the bakery.");
            }

            Console.WriteLine($"Money: {money}");
            Console.WriteLine(MatrixPrint(bakery));
        }

        static bool IndexValidation(char[,] matrix, int row, int col)
        {
            if (row >= 0 && col >= 0 && row < matrix.GetLength(0) && col < matrix.GetLength(1))
                return true;

            return false;
        }

        static string MatrixPrint(char[,] matrix)
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

    class Vector2 : IComparable
    {
        public Vector2(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public int CompareTo(object obj)
        {
            Vector2 other = obj as Vector2;

            if (this.Row == other.Row && this.Column == other.Column)
                return 0;

            return -1;
        }
    }
}
