using System;
using System.Collections.Generic;

namespace P02.Survivor
{
    internal class Program
    {
        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());

            char[][] beach = new char[rows][];

            ArrayFill(beach);

            int playerTokens = 0;
            int oponentTokens = 0;

            var directions = new Dictionary<string, Tuple<int, int>>
            {
                {"up", new Tuple<int, int>(-1, 0)},
                {"down", new Tuple<int, int>(1, 0)},
                {"left", new Tuple<int, int>(0, -1)},
                {"right", new Tuple<int, int>(0, 1)},
            };

            string command;
            while ((command = Console.ReadLine()) != "Gong")
            {
                string[] cmdSplit = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (cmdSplit[0] == "Find")
                {
                    int row = int.Parse(cmdSplit[1]);
                    int col = int.Parse(cmdSplit[2]);

                    if (IndexValidation(beach, row, col))
                    {
                        if (beach[row][col] == 'T')
                        {
                            playerTokens++;
                            beach[row][col] = '-';
                        }
                    }
                }
                else
                {
                    int row = int.Parse(cmdSplit[1]);
                    int col = int.Parse(cmdSplit[2]);
                    string direction = cmdSplit[3];

                    if(IndexValidation(beach, row, col))
                    {
                        int opPosRow = row;
                        int opPosCol = col;

                        if (beach[opPosRow][opPosCol] == 'T')
                        {
                            oponentTokens++;
                            beach[opPosRow][opPosCol] = '-';
                        }

                        for (int i = 1; i <= 3; i++)
                        {
                            opPosRow += directions[direction].Item1;
                            opPosCol += directions[direction].Item2;

                            if (IndexValidation(beach, opPosRow, opPosCol))
                            {
                                if (beach[opPosRow][opPosCol] == 'T')
                                {
                                    oponentTokens++;
                                    beach[opPosRow][opPosCol] = '-';
                                }
                            }
                        }
                    }
                }
            }

            PrintBeach(beach);
            Console.WriteLine($"Collected tokens: {playerTokens}");
            Console.WriteLine($"Opponent's tokens: {oponentTokens}");
        }

        private static void PrintBeach(char[][] beach)
        {
            for (int row = 0; row < beach.Length; row++)
            {
                Console.WriteLine(string.Join(" ", beach[row]));
            }
        }

        

        private static void ArrayFill(char[][] beach)
        {
            for (int row = 0; row < beach.Length; row++)
            {
                string line = Console.ReadLine().Replace(" ", string.Empty);
                beach[row] = line.ToCharArray();
            }
        }

        private static bool IndexValidation(char[][] beach, int row, int col)
        {
            if (row >= 0 && row < beach.Length && col >= 0 && col < beach[row].Length)
                return true;

            return false;
        }
    }
}