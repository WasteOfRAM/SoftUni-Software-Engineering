using System;
using System.Collections.Generic;
using System.Linq;

namespace CarManufacturer
{
    public class StartUp
    {
        static void Main()
        {
            List<Tire[]> tireSets = new List<Tire[]>();
            List<Engine> engines = new List<Engine>();
            List<Car> cars = new List<Car>();

            string input;
            while ((input = Console.ReadLine()) != "No more tires")
            {
                var tireSet = new Tire[]
                {
                    new Tire(int.Parse(input.Split()[0]), double.Parse(input.Split()[1])),
                    new Tire(int.Parse(input.Split()[2]), double.Parse(input.Split()[3])),
                    new Tire(int.Parse(input.Split()[4]), double.Parse(input.Split()[5])),
                    new Tire(int.Parse(input.Split()[6]), double.Parse(input.Split()[7]))
                };

                tireSets.Add(tireSet);
            }

            while ((input = Console.ReadLine()) != "Engines done")
            {
                engines.Add(new Engine(int.Parse(input.Split()[0]), double.Parse(input.Split()[1])));
            }

            while ((input = Console.ReadLine()) != "Show special")
            {
                string make = input.Split()[0];
                string model = input.Split()[1];
                int year = int.Parse(input.Split()[2]);
                double fuelQuantity = double.Parse(input.Split()[3]);
                double fuelConsumption = double.Parse(input.Split()[4]);
                int engineIndex = int.Parse(input.Split()[5]);
                int tireSetIndex = int.Parse(input.Split()[6]);

                cars.Add(new Car(make, model, year, fuelQuantity, fuelConsumption, engines[engineIndex], tireSets[tireSetIndex]));
            }

            Func<Car, bool> specialCarFilter = car => car.Year >= 2017 && car.Engine.HorsePower >= 330 && car.Tires.Sum(t => t.Pressure) >= 9 && car.Tires.Sum(t => t.Pressure) <= 10;

            cars = cars.Where(specialCarFilter).ToList();

            foreach (var car in cars)
            {
                car.Drive(20);
                Console.WriteLine(car.WhoAmI());
            }
        }
    }
}
