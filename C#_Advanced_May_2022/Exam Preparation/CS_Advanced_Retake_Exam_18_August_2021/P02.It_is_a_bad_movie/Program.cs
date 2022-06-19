using System;
using System.Linq;
using System.Collections.Generic;

namespace P02.It_is_a_bad_movie
{
    internal class Program
    {
        private static Vector2 armyPosition;

        static void Main()
        {
            int armyArmor = int.Parse(Console.ReadLine());
            int mapRows = int.Parse(Console.ReadLine());
            bool isDead = false;

            var directions = new Dictionary<string, Tuple<int, int>>
            {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, 1)},
            };

            char[][] map = new char[mapRows][];

            MapFill(map);

            while (true)
            {
                string commands = Console.ReadLine();
                string direction = commands.Split(' ')[0];
                int spawnRow = int.Parse(commands.Split()[1]);
                int spawnCol = int.Parse(commands.Split()[2]);

                map[spawnRow][spawnCol] = 'O';

                armyArmor--;

                if (!IndexValidation(map, armyPosition.Row + directions[direction].Item1, armyPosition.Col + directions[direction].Item2))
                {
                    if(armyArmor >= 1)
                        continue;
                    else
                    {
                        map[armyPosition.Row][armyPosition.Col] = 'X';
                        isDead = true;
                        break;
                    }
                }

                armyPosition.Row += directions[direction].Item1;
                armyPosition.Col += directions[direction].Item2;

                if (map[armyPosition.Row][armyPosition.Col] == 'M')
                {
                    map[armyPosition.Row][armyPosition.Col] = '-';
                    break;
                }
                else if (armyArmor <= 0)
                {
                    map[armyPosition.Row][armyPosition.Col] = 'X';
                    isDead = true;
                    break;
                }
                else if (map[armyPosition.Row][armyPosition.Col] == 'O')
                {
                    armyArmor -= 2;

                    if (armyArmor <= 0)
                    {
                        map[armyPosition.Row][armyPosition.Col] = 'X';
                        isDead = true;
                        break;
                    }

                    map[armyPosition.Row][armyPosition.Col] = '-';
                }
            }

            if (isDead)
            {
                Console.WriteLine($"The army was defeated at {armyPosition.Row};{armyPosition.Col}.");
            }
            else
            {
                Console.WriteLine($"The army managed to free the Middle World! Armor left: {armyArmor}");
            }

            foreach (var line in map)
            {
                Console.WriteLine(string.Join(string.Empty, line));
            }
        }

        private static bool IndexValidation(char[][] arr, int row, int col)
        {
            if(row >= 0 && row < arr.Length && col >= 0 && col < arr[row].Length)
                return true;

            return false;
        }

        private static void MapFill(char[][] map)
        {
            for (int row = 0; row < map.Length; row++)
            {
                map[row] = Console.ReadLine().ToCharArray();

                if (map[row].Contains('A'))
                {
                    int armyCol = Array.IndexOf(map[row], 'A');
                    armyPosition = new Vector2(row, armyCol);
                    map[row][armyCol] = '-';
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
