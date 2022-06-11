using System;
using System.Collections.Generic;
using System.Text;

namespace P02.Garden
{
    internal class Program
    {
        static void Main()
        {
            string gardenSize = Console.ReadLine();
            int rows = int.Parse(gardenSize.Split()[0]);
            int columns = int.Parse(gardenSize.Split()[1]);

            int[,] garden = new int[rows, columns];
            var seeds = new List<Tuple<int, int>>();

            string input;
            while ((input = Console.ReadLine()) != "Bloom Bloom Plow")
            {
                int seedRow = int.Parse(input.Split()[0]);
                int seedCol = int.Parse(input.Split()[1]);

                if(!IndexValidation(garden, seedRow, seedCol))
                {
                    Console.WriteLine("Invalid coordinates.");
                    continue;
                }

                seeds.Add(new Tuple<int, int>(seedRow, seedCol));
            }

            foreach (var seed in seeds)
            {
                for (int col = 0; col < garden.GetLength(1); col++)
                {
                    garden[seed.Item1, col]++;
                }

                for (int row = 0; row < garden.GetLength(0); row++)
                {
                    garden[row, seed.Item2]++;
                }

                garden[seed.Item1, seed.Item2]--;
            }

            Console.WriteLine(MatrixPrint(garden));
        }

        static bool IndexValidation(int[,] matrix, int row, int col)
        {
            if(row >= 0 && col >= 0 && row < matrix.GetLength(0) && col < matrix.GetLength(1))
                return true;

            return false;
        }

        static string MatrixPrint(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    sb.Append(matrix[row, col] + " ");
                }
                sb.ToString().TrimEnd();
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
