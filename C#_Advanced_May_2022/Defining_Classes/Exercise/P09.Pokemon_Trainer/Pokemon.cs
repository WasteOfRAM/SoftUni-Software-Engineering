using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    internal class Pokemon
    {
        private string name;
        private string element;
        private int health;

        public string Name { get => this.name; set => this.name = value; }
        public string Element { get => this.element; set => this.element = value; }
        public int Health { get => this.health; set => this.health = value; }

        public Pokemon(string name, string element, int health)
        {
            this.name = name;
            this.element = element;
            this.health = health;
        }
    }
}
