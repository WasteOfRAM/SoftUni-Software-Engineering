using System;
using System.Collections.Generic;

namespace P08.Traffic_Jam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int carsPassing = int.Parse(Console.ReadLine());

            Queue<string> trafficStop = new Queue<string>();

            int carsPassed = 0;

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                if (command == "green")
                {
                    for (int i = 1; i <= carsPassing; i++)
                    {
                        if (trafficStop.Count == 0)
                        {
                            break;
                        }

                        Console.WriteLine($"{trafficStop.Dequeue()} passed!");
                        carsPassed++;
                    }
                }
                else
                {
                    trafficStop.Enqueue(command);
                }
            }

            Console.WriteLine($"{carsPassed} cars passed the crossroads.");
        }
    }
}
