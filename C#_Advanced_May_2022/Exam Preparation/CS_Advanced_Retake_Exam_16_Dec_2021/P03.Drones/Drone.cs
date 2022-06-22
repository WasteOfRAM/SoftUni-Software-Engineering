using System.Text;

namespace Drones
{
    public class Drone
    {
        private string name;
        private string brand;
        private int range;
        private bool available;

        public Drone(string name, string brand, int range)
        {
            this.name = name;
            this.brand = brand;
            this.range = range;
            available = true;
        }

        public string Name { get => this.name; set => this.name = value; }
        public string Brand { get => this.brand; set => this.brand = value; }
        public int Range { get => this.range; set => this.range = value; }
        public bool Available { get => this.available; set => this.available = value; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Drone: {this.name}")
                .AppendLine($"Manufactured by: {this.brand}")
                .Append($"Range: {this.range} kilometers");

            return sb.ToString();
        }
    }
}
