
using System;
using System.Collections.Generic;

namespace P10.Radioactive_Mutant_Vampire_Bunnies
{
    internal class Program
    {
        static void Main()
        {
            string lairSize = Console.ReadLine();
            int rows = int.Parse(lairSize.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
            int cols = int.Parse(lairSize.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);

            char[,] lair = new char[rows, cols];

            Player player = new Player();

            //Filing the matrix and sets up the player staring position
            MatrixFill(lair, player);

            // Queues the commands
            Queue<char> commands = new Queue<char>(Console.ReadLine());


            Dictionary<char, Tuple<int, int>> directions = new Dictionary<char, Tuple<int, int>>
            {
                {'L', new Tuple<int, int>(0, -1) },
                {'R', new Tuple<int, int>(0, 1) },
                {'U', new Tuple<int, int>(-1, 0) },
                {'D', new Tuple<int, int>(1, 0) }
            };


            List<Tuple<int, int>> spreadPositions = new List<Tuple<int, int>>();


            while (!player.Dead && !player.Won)
            {
                // If there is no player input skips the player move and goes to bunny spreading
                if (commands.Count != 0)
                {
                    Tuple<int, int> direction = directions[commands.Dequeue()];

                    // If the index of the desired move is valid the player makes the move and checks if he died
                    // If index is invalid you escaped the lair
                    if (IndexValidation(player.Row + direction.Item1, player.Col + direction.Item2, lair))
                    {
                        lair[player.Row, player.Col] = '.';
                        player.Row = player.Row + direction.Item1;
                        player.Col = player.Col + direction.Item2;
                        

                        if (lair[player.Row, player.Col] == 'B')
                        {
                            player.Dead = true;
                            lair[player.Row, player.Col] = 'B';
                        }
                        else
                        {
                            lair[player.Row, player.Col] = 'P';
                        }
                    }
                    else
                    {
                        lair[player.Row, player.Col] = '.';
                        player.Won = true;
                    }
                }

                // After the player made his move stores the positions the buneys need to spred to
                BunnySpread(lair, directions, spreadPositions);

                // The Bunnys Spread!!! to the given positions and kills the player if they find him
                foreach (var position in spreadPositions)
                {
                    if (lair[position.Item1, position.Item2] == 'P')
                    {
                        player.Dead = true;
                    }

                    lair[position.Item1, position.Item2] = 'B';
                }

                spreadPositions.Clear();
            }

            MatrixPrint(lair);
            if (player.Won)
                Console.WriteLine($"won: {player.Row} {player.Col}");

            if (player.Dead)
                Console.WriteLine($"dead: {player.Row} {player.Col}");
        }

        private static void BunnySpread(char[,] lair, Dictionary<char, Tuple<int, int>> directions, List<Tuple<int, int>> spreadPositions)
        {
            for (int row = 0; row < lair.GetLength(0); row++)
            {
                for (int col = 0; col < lair.GetLength(1); col++)
                {

                    if (lair[row, col] == 'B')
                    {
                        foreach (var direction in directions)
                        {
                            if (IndexValidation(row + direction.Value.Item1, col + direction.Value.Item2, lair))
                            {
                                if (lair[row + direction.Value.Item1, col + direction.Value.Item2] != 'B')
                                {
                                    spreadPositions.Add(new Tuple<int, int>(row + direction.Value.Item1, col + direction.Value.Item2)); 
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool IndexValidation(int row, int col, char[,] lair)
        {
            if (row >= 0 && row < lair.GetLength(0) && col >= 0 && col < lair.GetLength(1))
            {
                return true;
            }

            return false;
        }

        private static void MatrixFill(char[,] matrix, Player player)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] matrixRow = Console.ReadLine().ToCharArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrixRow[col] == 'P')
                    {
                        player.Row = row;
                        player.Col = col;
                    }
                    matrix[row, col] = matrixRow[col];
                }
            }
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

    class Player
    {
        public Player()
        {

        }

        public Player(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public bool Dead { get; set; } = false;

        public bool Won { get; set; } = false;

        public int Row { get; set; }

        public int Col { get; set; }
    }
}
