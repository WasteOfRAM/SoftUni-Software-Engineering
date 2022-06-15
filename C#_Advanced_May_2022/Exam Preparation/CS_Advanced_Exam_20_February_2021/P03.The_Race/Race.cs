using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheRace
{
    public class Race
    {
        private List<Racer> data;

        public Race(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            data = new List<Racer>();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }

        public int Count => this.data.Count;

        public void Add(Racer racer)
        {
            if(data.Count < Capacity)
                data.Add(racer);
        }

        public bool Remove(string name)
        {
            var racerToRemove = data.FirstOrDefault(x => x.Name == name);

            return data.Remove(racerToRemove);
        }

        public Racer GetOldestRacer()
        {
            return data.OrderByDescending(r => r.Age).FirstOrDefault();
        }

        public Racer GetRacer(string name)
        {
            return data.FirstOrDefault(x => x.Name == name);
        }

        public Racer GetFastestRacer()
        {
            return data.OrderByDescending(r => r.Car.Speed).FirstOrDefault();
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Racers participating at {this.Name}:");
            sb.Append($"{string.Join(Environment.NewLine, this.data)}");

            return sb.ToString();
        }
    }
}
