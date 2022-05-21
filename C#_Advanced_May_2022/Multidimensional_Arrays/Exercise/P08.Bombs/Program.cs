using System;
using System.Linq;

namespace P08.Bombs
{
    internal class Program
    {
        static void Main()
        {
            int matrixSize = int.Parse(Console.ReadLine());

            int[,] mineField = new int[matrixSize, matrixSize];
            MatrixFill(mineField);

            string[] bombCoordinatesInput = Console.ReadLine().Split();
            Tuple<int, int>[] bombCoordinates = new Tuple<int, int>[bombCoordinatesInput.Length];

            for (int i = 0; i < bombCoordinatesInput.Length; i++)
            {
                bombCoordinates[i] = new Tuple<int, int>(int.Parse(bombCoordinatesInput[i].Split(',', StringSplitOptions.RemoveEmptyEntries)[0]), 
                                                         int.Parse(bombCoordinatesInput[i].Split(',', StringSplitOptions.RemoveEmptyEntries)[1]));
            }

            // Bomb radius
            Tuple<int, int>[] bombRadius = { new Tuple<int, int>(-1, -1),
                                             new Tuple<int, int>(-1, 0),
                                             new Tuple<int, int>(-1, 1),
                                             new Tuple<int, int>(0, -1),
                                             new Tuple<int, int>(0, 1),
                                             new Tuple<int, int>(1, -1),
                                             new Tuple<int, int>(1, 0),
                                             new Tuple<int, int>(1, 1)};


            foreach (var bomb in bombCoordinates)
            {
                if (mineField[bomb.Item1, bomb.Item2] <= 0)
                {
                    continue;
                }

                int bombValue = mineField[bomb.Item1, bomb.Item2];
                mineField[bomb.Item1, bomb.Item2] = 0;

                foreach (var hitZone in bombRadius)
                {
                    int hitRow = bomb.Item1 + hitZone.Item1;
                    int hitCol = bomb.Item2 + hitZone.Item2;

                    if (RadiusValidation(hitRow, hitCol, matrixSize) && mineField[hitRow, hitCol] > 0)
                    { 
                        mineField[hitRow, hitCol] -= bombValue;
                    }
                }
            }

            int aliveCellsCount = 0;
            int aliveCellsSum = 0;

            foreach (var cell in mineField)
            {
                if (cell > 0)
                {
                    aliveCellsCount++;
                    aliveCellsSum += cell;
                }
            }

            Console.WriteLine($"Alive cells: {aliveCellsCount}");
            Console.WriteLine($"Sum: {aliveCellsSum}");

            MatrixPrint(mineField);
        }

        private static bool RadiusValidation(int row, int col, int matrixSize)
        {
            if (row >= 0 && row < matrixSize && col >= 0 && col < matrixSize)
            {
                return true;
            }

            return false;
        }


        private static void MatrixFill(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] matrixRow = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = matrixRow[col];
                }
            }
        }

        private static void MatrixPrint(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
