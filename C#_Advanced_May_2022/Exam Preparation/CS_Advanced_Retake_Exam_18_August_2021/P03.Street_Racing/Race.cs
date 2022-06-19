using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreetRacing
{
    public class Race
    {
        private List<Car> participants;

        public Race(string name, string type, int laps, int capacity, int maxHorsePower)
        {
            Name = name;
            Type = type;
            Laps = laps;
            Capacity = capacity;
            MaxHorsePower = maxHorsePower;
            participants = new List<Car>();
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public int Laps { get; set; }
        public int Capacity { get; set; }
        public int MaxHorsePower { get; set; }
        public int Count => this.participants.Count;

        public void Add(Car car)
        {
            if (this.participants.Count < this.Capacity && !this.participants.Any(c => c.LicensePlate == car.LicensePlate) && car.HorsePower <= this.MaxHorsePower)
                this.participants.Add(car);
        }

        public bool Remove(string licensePlate)
        {
            var carToremove = this.participants.FirstOrDefault(car => car.LicensePlate == licensePlate);
            return this.participants.Remove(carToremove);
        }

        public Car FindParticipant(string licensePlate)
        {
            var car = this.participants.FirstOrDefault(car => car.LicensePlate == licensePlate);
            return car;
        }

        public Car GetMostPowerfulCar()
        {
            if (this.participants.Count == 0)
                return null;

            return this.participants.OrderByDescending(car => car.HorsePower).FirstOrDefault();
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Race: {Name} - Type: {Type} (Laps: {Laps})");

            sb.AppendLine(string.Join(Environment.NewLine, this.participants));

            return sb.ToString().TrimEnd();
        }
    }
}
