using System;
using System.Collections.Generic;

namespace P10.Crossroads
{
    internal class Program
    {
        static void Main()
        {
            int greenLightDuration = int.Parse(Console.ReadLine());
            int freeWindowDuration = int.Parse(Console.ReadLine());

            Queue<string> carsQueue = new Queue<string>();

            string curentCar = string.Empty;
            int hitIndex = 0;
            bool crash = false;

            int totalCarsPassed = 0;

            string command;
            while ((command = Console.ReadLine()) != "END" && !crash)
            {
                if (command != "green")
                {
                    carsQueue.Enqueue(command);
                }
                else
                {
                    int curentGreenLight = greenLightDuration;
                    int timeLeftToPass = freeWindowDuration;

                    while (curentGreenLight > 0 && carsQueue.Count > 0)
                    {
                        curentCar = carsQueue.Dequeue();
                        curentGreenLight -= curentCar.Length;

                        if (curentGreenLight > 0)
                        {
                            totalCarsPassed++;
                        }
                        else
                        {
                            timeLeftToPass += curentGreenLight;

                            if (timeLeftToPass < 0)
                            {
                                crash = true;
                                hitIndex = curentCar.Length + timeLeftToPass;
                                break;
                            }
                            else
                            {
                                totalCarsPassed++;
                            }
                        }
                    }
                }
            }

            if (crash)
            {
                Console.WriteLine("A crash happened!");
                Console.WriteLine($"{curentCar} was hit at {curentCar[hitIndex]}.");
            }
            else
            {
                Console.WriteLine("Everyone is safe.");
                Console.WriteLine($"{totalCarsPassed} total cars passed the crossroads.");
            }
        }
    }
}
