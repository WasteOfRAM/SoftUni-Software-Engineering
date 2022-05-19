using System;

namespace P07.Pascal_Triangle
{
    internal class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            long[][] triangle = new long[n][];

            for (int i = 0; i < n; i++)
            {
                triangle[i] = new long[i + 1];
            }

            triangle[0][0] = 1;

            for (int row = 1; row < n; row++)
            {
                for (int column = 0; column < triangle[row].Length; column++)
                {
                    if (column == 0)
                    {
                        triangle[row][column] = triangle[row - 1][column];
                    }
                    else if (column > triangle[row - 1].Length - 1)
                    {
                        triangle[row][column] = triangle[row - 1][column - 1];
                    }
                    else
                    {
                        triangle[row][column] = triangle[row - 1][column - 1] + triangle[row - 1][column];
                    }
                }
            }

            foreach (var arr in triangle)
            {
                Console.WriteLine(string.Join(' ', arr));
            }
        }
    }
}
