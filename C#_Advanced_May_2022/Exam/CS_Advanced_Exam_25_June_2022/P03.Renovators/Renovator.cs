using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renovators
{
    public class Renovator
    {
        private string name;
        private string type;
        private double rate;
        private int days;
        private bool hired;

        public Renovator(string name, string type, double rate, int days)
        {
            this.name = name;
            this.type = type;
            this.rate = rate;
            this.days = days;
            this.hired = false;
        }

        public string Name { get => this.name; set => this.name = value; }
        public string Type { get => this.type; set => this.type = value; }
        public double Rate { get => this.rate; set => this.rate = value; }
        public int Days { get => this.days; set => this.days = value; }
        public bool Hired { get => this.hired; set => this.hired = value; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"-Renovator: {this.name}")
                .AppendLine($"--Specialty: {this.type}")
                .Append($"--Rate per day: {this.rate} BGN");

            return sb.ToString().TrimEnd();
        }
    }
}
