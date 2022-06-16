using System;
using System.Collections.Generic;

namespace P02.Super_Mario
{
    internal class Program
    {
        static void Main()
        {
            int lives = int.Parse(Console.ReadLine());
            int castelSize = int.Parse(Console.ReadLine());

            char[][] castel = new char[castelSize][];
            Vector2 marioPosition = null;

            bool dead = false;

            var directions = new Dictionary<string, Tuple<int, int>>
            {
                {"W", new Tuple<int, int>(-1, 0)},
                {"S", new Tuple<int, int>(1, 0)},
                {"A", new Tuple<int, int>(0, -1)},
                {"D", new Tuple<int, int>(0, 1)},
            };

            for (int i = 0; i < castelSize; i++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int k = 0; k < line.Length; k++)
                {
                    castel[i] = line;

                    if (line[k] == 'M')
                    {
                        marioPosition = new Vector2(i, k);
                        castel[i][k] = '-';
                    }
                }
            }

            while (true)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string direction = input[0];
                int spawnRow = int.Parse(input[1]);
                int spawnCol = int.Parse(input[2]);

                castel[spawnRow][spawnCol] = 'B';

                lives--;

                int newPositionRow = marioPosition.Row + directions[direction].Item1;
                int newPositionCol = marioPosition.Col + directions[direction].Item2;

                if (!IndexValidation(castel, newPositionRow, newPositionCol))
                {
                    if (lives <= 0)
                    {
                        dead = true;
                        castel[marioPosition.Row][marioPosition.Col] = 'X';
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                marioPosition.Row = newPositionRow;
                marioPosition.Col = newPositionCol;

                if(castel[marioPosition.Row][marioPosition.Col] == 'P')
                {
                    castel[marioPosition.Row][marioPosition.Col] = '-';
                    break;
                }
                else if (lives <= 0)
                {
                    dead = true;
                    castel[marioPosition.Row][marioPosition.Col] = 'X';
                    break;
                }
                else if (castel[marioPosition.Row][marioPosition.Col] == 'B')
                {
                    lives -= 2;

                    if(lives <= 0)
                    {
                        dead = true;
                        castel[marioPosition.Row][marioPosition.Col] = 'X';
                        break;
                    }
                    else
                    {
                        castel[marioPosition.Row][marioPosition.Col] = '-';
                    }
                }
            }

            if (dead)
            {
                Console.WriteLine($"Mario died at {marioPosition.Row};{marioPosition.Col}.");
            }
            else
            {
                Console.WriteLine($"Mario has successfully saved the princess! Lives left: {lives}");
            }

            foreach (var line in castel)
            {
                Console.WriteLine(line);
            }
        }

        static bool IndexValidation(char[][] arr, int row, int col)
        {
            if(row >= 0 && row < arr.Length && col >= 0 && col < arr[row].Length)
                return true;

            return false;
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
