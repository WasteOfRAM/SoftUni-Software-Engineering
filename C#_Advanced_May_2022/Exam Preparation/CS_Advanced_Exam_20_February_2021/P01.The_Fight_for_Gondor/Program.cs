using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.The_Fight_for_Gondor
{
    internal class Program
    {
        static void Main()
        {
            int waves = int.Parse(Console.ReadLine());

            Queue<int> plates = new Queue<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
            Stack<int> orcsWave = null;


            for (int i = 1; i <= waves; i++)
            {
                if (plates.Count == 0)
                    break;

                orcsWave = new Stack<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

                if (i % 3 == 0)
                    plates.Enqueue(int.Parse(Console.ReadLine()));

                while (plates.Count > 0 && orcsWave.Count > 0)
                {

                    int orkWarior = orcsWave.Pop();
                    int plate = plates.Dequeue();

                    if (orkWarior > plate)
                    {
                        orkWarior -= plate;
                        orcsWave.Push(orkWarior);
                    }
                    else if (orkWarior < plate)
                    {
                        plate -= orkWarior;
                        var newPlates = new Queue<int>();
                        newPlates.Enqueue(plate);
                        foreach (var item in plates)
                        {
                            newPlates.Enqueue(item);
                        }

                        plates = newPlates;
                    }
                }
            }

            if(plates.Count > 0)
            {
                Console.WriteLine("The people successfully repulsed the orc's attack.");
                Console.WriteLine($"Plates left: {string.Join(", ", plates)}");
            }
            else
            {
                Console.WriteLine("The orcs successfully destroyed the Gondor's defense.");
                Console.WriteLine($"Orcs left: {string.Join(", ", orcsWave)}");
            }


        }
    }
}
