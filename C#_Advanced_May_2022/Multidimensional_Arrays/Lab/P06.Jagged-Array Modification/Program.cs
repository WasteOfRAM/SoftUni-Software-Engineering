using System;
using System.Linq;

namespace P06.Jagged_Array_Modification
{
    internal class Program
    {
        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            int[][] jArray = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                jArray[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            string commandInput;
            while ((commandInput = Console.ReadLine()) != "END")
            {
                string command = commandInput.Split(' ')[0];
                int row = int.Parse(commandInput.Split(' ')[1]);
                int column = int.Parse(commandInput.Split(' ')[2]);
                int value = int.Parse(commandInput.Split(' ')[3]);

                if (row >= 0 && row < jArray.GetLength(0)
                    && column >= 0 && column < jArray[row].Length)
                {
                    if (command == "Add")
                    {
                        jArray[row][column] += value;
                    }
                    else
                    {
                        jArray[row][column] -= value;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid coordinates");
                }
            }

            foreach (var row in jArray)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }
    }
}
