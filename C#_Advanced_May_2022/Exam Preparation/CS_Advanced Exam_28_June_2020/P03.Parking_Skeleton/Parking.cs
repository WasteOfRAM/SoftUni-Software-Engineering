using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Parking
    {
        private List<Car> cars;

        public Parking(string type, int capacity)
        {
            cars = new List<Car>();
            this.Type = type;
            this.Capacity = capacity;
        }

        public string Type { get; set; }
        public int Capacity { get; set; }
        public int Count { get { return this.cars.Count; } }

        public void Add(Car car)
        {
            if (cars.Count < this.Capacity)
                cars.Add(car);
        }

        public bool Remove(string manufacturer, string model)
        {
            var car = cars.Find(car => car.Manufacturer == manufacturer && car.Model == model);

            return cars.Remove(car);
        }

        public Car GetLatestCar()
        {
            if (cars.Count == 0)
                return null;

            var tempCars = new List<Car>(cars);

            return tempCars.OrderByDescending(car => car.Year).First();
        }

        public Car GetCar(string manufacturer, string model)
        {
            return cars.Find(car => car.Manufacturer == manufacturer && car.Model == model);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The cars are parked in {this.Type}:");
            foreach (var car in cars)
            {
                sb.AppendLine(car.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
