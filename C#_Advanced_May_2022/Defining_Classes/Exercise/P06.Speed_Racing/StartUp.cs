using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    internal class StartUp
    {
        static void Main()
        {
            List<Car> cars = new List<Car>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string model = input.Split()[0];
                double fuelAmout = double.Parse(input.Split()[1]);
                double fuelRate = double.Parse(input.Split()[2]);

                cars.Add(new Car(model, fuelAmout, fuelRate));
            }

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string model = command.Split()[1];
                int distance = int.Parse(command.Split()[2]);

                cars.First(car => car.Model == model).Drive(distance);
            }

            cars.ForEach(car => car.Print());
        }
    }
}
