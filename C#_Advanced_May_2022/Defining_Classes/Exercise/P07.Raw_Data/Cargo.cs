using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    internal class Cargo
    {
        private string type;
        private int weight;

        public string Type { get => this.type; set => this.type = value; }
        public int Weight { get { return this.weight; } set { this.weight = value; } }


        public Cargo(string type, int weight)
        {
            this.type = type;
            this.weight = weight;
        }
    }
}
