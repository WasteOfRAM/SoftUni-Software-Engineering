using System;

namespace P07.Knight_Game
{
    internal class Program
    {
        static void Main()
        {
            int boardSize = int.Parse(Console.ReadLine());

            char[,] board = new char[boardSize, boardSize];

            MatrixFill(board);

            int removedPices = 0;

            //Posible moves knight moves
            //row - 2 col + 1
            //row - 1 col + 2
            //row + 1 col + 2
            //row + 2 col + 1
            //row + 2 col - 1
            //row + 1 col - 2
            //row - 1 col - 2
            //row - 2 col - 1

            Tuple<int, int>[] knightMoves = { new Tuple<int, int>(-2, 1),
                                              new Tuple<int, int>(-1, 2),
                                              new Tuple<int, int>(1, 2),
                                              new Tuple<int, int>(2, 1),
                                              new Tuple<int, int>(2, -1),
                                              new Tuple<int, int>(1, -2),
                                              new Tuple<int, int>(-1, -2),
                                              new Tuple<int, int>(-2, -1)};

            while (true)
            {
                int knightMaxValidMoves = 0;
                Tuple<int, int> knightToRemove = null;

                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        int curentValidMoves = 0;
                        if (board[row, col] == 'K')
                        {
                            foreach (var move in knightMoves)
                            {
                                if (MoveValidator(row + move.Item1, col + move.Item2, boardSize) && board[row + move.Item1, col + move.Item2] == 'K')
                                {
                                    curentValidMoves++;
                                }
                            }
                        }

                        if (curentValidMoves > knightMaxValidMoves)
                        {
                            knightMaxValidMoves = curentValidMoves;
                            knightToRemove = new Tuple<int, int>(row, col);
                        }
                    }
                }

                if (knightMaxValidMoves > 0)
                {
                    removedPices++;
                    board[knightToRemove.Item1, knightToRemove.Item2] = '0';
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine(removedPices);
        }

        private static void MatrixFill(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] matrixRow = Console.ReadLine().ToCharArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = matrixRow[col];
                }
            }
        }

        private static bool MoveValidator(int row, int col, int boardSize)
        {
            if (row >= 0 && row < boardSize && col >= 0 && col < boardSize)
            {
                return true;
            }

            return false;
        }
    }
}
