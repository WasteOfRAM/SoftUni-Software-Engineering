using System;
using System.Collections.Generic;

namespace DefiningClasses
{
    internal class StartUp
    {
        static void Main()
        {
            int numEngines = int.Parse(Console.ReadLine());

            List<Engine> engines = new List<Engine>();
            List<Car> cars = new List<Car>();

            for (int i = 0; i < numEngines; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (input.Length == 4)
                {
                    engines.Add(new Engine(input[0], int.Parse(input[1]), int.Parse(input[2]), input[3]));
                }
                else if (input.Length == 3)
                {
                    int parsedInt;

                    if(int.TryParse(input[2],out parsedInt))
                        engines.Add(new Engine(input[0], int.Parse(input[1]), parsedInt));
                    else
                        engines.Add(new Engine(input[0], int.Parse(input[1]), input[2]));
                }
                else
                {
                    engines.Add(new Engine(input[0], int.Parse(input[1])));
                }
            }

            int numCars = int.Parse(Console.ReadLine());

            for (int i = 0; i < numCars; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = input[0];
                Engine engine = engines.Find(e => e.Model == input[1]);

                if (input.Length == 4)
                {
                    cars.Add(new Car(input[0], engine, int.Parse(input[2]), input[3]));
                }
                else if (input.Length == 3)
                {
                    int parsedInt;

                    if (int.TryParse(input[2], out parsedInt))
                        cars.Add(new Car(input[0], engine, parsedInt));
                    else
                        cars.Add(new Car(input[0], engine, input[2]));
                }
                else
                {
                    cars.Add(new Car(input[0], engine));
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, cars));
        }
    }
}
