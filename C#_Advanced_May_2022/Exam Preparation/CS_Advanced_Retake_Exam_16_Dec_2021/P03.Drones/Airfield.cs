using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace Drones
{
    public class Airfield
    {
        private List<Drone> drones;
        private string name;
        private int capacity;
        private double landingStrip;

        public Airfield(string name, int capacity, double landingStrip)
        {
            this.drones = new List<Drone>();
            this.name = name;
            this.capacity = capacity;
            this.landingStrip = landingStrip;
        }

        public string Name { get => this.name; set => this.name = value; }
        public int Capacity { get => this.capacity; set => this.capacity = value; }
        public double LandingStrip { get => this.landingStrip; set => this.landingStrip = value; }
        public int Count { get => this.drones.Count; }

        public string AddDrone(Drone drone)
        {
            if (drone.Name == null || drone.Name == string.Empty || drone.Brand == null || drone.Brand == string.Empty || drone.Range < 5 || drone.Range > 15)
                return "Invalid drone.";

            if (this.drones.Count >= this.capacity)
                return "Airfield is full.";

            drones.Add(drone);
            return $"Successfully added {drone.Name} to the airfield.";
        }

        public bool RemoveDrone(string name)
        {
            var droneToRemove = this.drones.FirstOrDefault(drone => drone.Name == name);

            return this.drones.Remove(droneToRemove);
        }

        public int RemoveDroneByBrand(string brand)
        {
            return this.drones.RemoveAll(drone => drone.Brand == brand); ;
        }

        public Drone FlyDrone(string name)
        {
            var droneToFly = this.drones.FirstOrDefault(drone => drone.Name == name);

            if (droneToFly != null)
                droneToFly.Available = false;

            return droneToFly;
        }

        public List<Drone> FlyDronesByRange(int range)
        {
            return this.drones.Where(d => d.Range >= range).Select(d => { d.Available = false; return d; }).ToList();
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Drones available at {this.name}:");
            sb.Append($"{string.Join(Environment.NewLine, this.drones.Where(drone => drone.Available == true).ToList())}");

            return sb.ToString();
        }
    }
}
