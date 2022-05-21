using System;
using System.Linq;

namespace P06.Jagged_Array_Manipulator
{
    internal class Program
    {
        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());

            int[][] jMatrix = new int[rows][];

            JMatrixFill(jMatrix);
            Analizer(jMatrix);

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string cmd = command.Split()[0];
                int row = int.Parse(command.Split()[1]);
                int col = int.Parse(command.Split()[2]);
                int value = int.Parse((command.Split()[3]));

                if (row >= 0 && row < jMatrix.Length && col >= 0 && col < jMatrix[row].Length)
                {
                    if (cmd == "Add")
                    {
                        jMatrix[row][col] += value;
                    }
                    else
                    {
                        jMatrix[row][col] -= value;
                    }
                }
            }

            MatrixPrint(jMatrix);
        }

        private static void Analizer(int[][] jMatrix)
        {
            for (int row = 0; row < jMatrix.Length - 1; row++)
            {
                if (jMatrix[row].Length == jMatrix[row + 1].Length)
                {
                    jMatrix[row] = jMatrix[row].Select(x => x * 2).ToArray();
                    jMatrix[row + 1] = jMatrix[row + 1].Select(x => x * 2).ToArray();
                }
                else
                {
                    jMatrix[row] = jMatrix[row].Select(x => x / 2).ToArray();
                    jMatrix[row + 1] = jMatrix[row + 1].Select(x => x / 2).ToArray();
                }
            }
        }

        private static void JMatrixFill(int[][] jMatrix)
        {
            for (int row = 0; row < jMatrix.Length; row++)
            {
                jMatrix[row] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }
        }

        private static void MatrixPrint(int[][] jMatrix)
        {
            foreach (var arr in jMatrix)
            {
                Console.WriteLine(string.Join(" ", arr));
            }
        }
    }
}
