using System;
using System.Collections.Generic;
using System.Text;

namespace CarManufacturer
{
    public class Engine
    {
        private int horsePower;
        private double cubicCapacity;

        public int HorsePower { get => this.horsePower; set => this.HorsePower = value; }
        public double CubicCapacity { get => this.cubicCapacity; set => this.cubicCapacity = value; }

        public Engine(int horsePower, double cubicCapacity)
        {
            this.horsePower = horsePower;
            this.cubicCapacity = cubicCapacity;
        }
    }
}
