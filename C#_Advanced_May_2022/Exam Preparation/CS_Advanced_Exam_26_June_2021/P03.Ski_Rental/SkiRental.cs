using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiRental
{
    public class SkiRental
    {
        private List<Ski> data;

        public SkiRental(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            this.data = new List<Ski>();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Count => this.data.Count;

        public void Add(Ski ski)
        {
            if(this.data.Count < Capacity)
                data.Add(ski);
        }

        public bool Remove(string manufacturer, string model)
        {
            var skiToRemove = data.FirstOrDefault(ski => ski.Manufacturer == manufacturer && ski.Model == model);

            return data.Remove(skiToRemove);
        }

        public Ski GetNewestSki()
        {
            return data.OrderByDescending(ski => ski.Year).FirstOrDefault();
        }

        public Ski GetSki(string manufacturer, string model)
        {
            return data.FirstOrDefault(ski => manufacturer == ski.Manufacturer && ski.Model == model);
        }

        public string GetStatistics()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"The skis stored in {this.Name}:")
                .Append(string.Join(Environment.NewLine, data));

            return sb.ToString();
        }
    }
}
