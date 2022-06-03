using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    internal class Engine
    {
        private int speed;
        private int power;

        public int Speed { get => this.speed; set => this.speed = value; }
        public int Power { get { return this.power; } set { this.power = value; } }


        public Engine(int speed, int power)
        {
            this.speed = speed;
            this.power = power;
        }
    }
}
