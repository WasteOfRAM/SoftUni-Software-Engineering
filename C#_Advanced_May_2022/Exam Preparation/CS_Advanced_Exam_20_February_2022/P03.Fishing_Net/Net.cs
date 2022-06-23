using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishingNet
{
    public class Net
    {
        private List<Fish> fish;

        public Net(string material, int capacity)
        {
            Material = material;
            Capacity = capacity;

            this.fish = new List<Fish> ();
        }

        public List<Fish> Fish { get => this.fish; }
        public string Material { get; set; }
        public int Capacity { get; set; }

        public int Count { get => this.fish.Count; }

        public string AddFish(Fish fish)
        {
            if (this.fish.Count >= Capacity)
                return "Fishing net is full.";

            if (string.IsNullOrWhiteSpace(fish.FishType) || fish.Weight <= 0 || fish.Length <= 0)
                return "Invalid fish.";

            this.fish.Add(fish);
            return $"Successfully added {fish.FishType} to the fishing net.";
        }

        public bool ReleaseFish(double weight)
        {
            var fishToRemove = this.fish.FirstOrDefault(w => w.Weight == weight);

            return this.fish.Remove(fishToRemove);
        }

        public Fish GetFish(string fishType)
        {
            return this.fish.FirstOrDefault(w => w.FishType == fishType);
        }

        public Fish GetBiggestFish()
        {
            return this.fish.OrderByDescending(l => l.Length).FirstOrDefault();
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Into the {Material}:")
                .Append(string.Join(Environment.NewLine, this.fish.OrderByDescending(l => l.Length)));

            return sb.ToString();
        }
    }
}
