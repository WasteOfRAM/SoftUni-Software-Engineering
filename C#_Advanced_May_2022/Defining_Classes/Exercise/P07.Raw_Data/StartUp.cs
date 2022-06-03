using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    internal class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            List<Car> cars = new List<Car>();

            for (int i = 0; i < n; i++)
            {
                string data = Console.ReadLine();
                string model = data.Split()[0];
                int engineSpeed = int.Parse(data.Split()[1]);
                int enginePower = int.Parse(data.Split()[2]);
                int cargoWeight = int.Parse(data.Split()[3]);
                string cargoType = data.Split()[4];
                double tireOnePressure = double.Parse(data.Split()[5]);
                int tireOneAge = int.Parse(data.Split()[6]);
                double tireTwoPressure = double.Parse(data.Split()[7]);
                int tireTwoAge = int.Parse(data.Split()[8]);
                double tireThreePressure = double.Parse(data.Split()[9]);
                int tireThreeAge = int.Parse(data.Split()[10]);
                double tireFourPressure = double.Parse(data.Split()[11]);
                int tireFourAge = int.Parse(data.Split()[12]);

                Engine engine = new Engine(engineSpeed, enginePower);
                Cargo cargo = new Cargo(cargoType, cargoWeight);
                Tire[] tires = new Tire[] { new Tire(tireOneAge, tireOnePressure), 
                                            new Tire(tireTwoAge, tireTwoPressure),
                                            new Tire(tireThreeAge, tireThreePressure),
                                            new Tire(tireFourAge, tireFourPressure)};

                cars.Add(new Car(model, engine, cargo, tires));
            }

            string command = Console.ReadLine();

            if (command == "fragile")
                cars = cars.Where(car => car.Cargo.Type == "fragile" && car.Tires.Any(t => t.Pressure < 1)).ToList();

            if (command == "flammable")
                cars = cars.Where(car => car.Cargo.Type == "flammable" && car.Engine.Power > 250).ToList();

            Console.WriteLine(string.Join(Environment.NewLine, cars));
        }
    }
}
