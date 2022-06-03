using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    internal class Trainer
    {
        private string name;
        private int badges;
        private List<Pokemon> pokemons;

        public string Name { get => this.name; set => this.name = value; }
        public int Badges { get => this.badges; set => this.badges = value; }
        public List<Pokemon> Pokemons { get => this.pokemons; set => this.pokemons = value; }

        public Trainer(string name)
        {
            this.name = name;
            this.badges = 0;
            this.pokemons = new List<Pokemon>();
        }

        public override string ToString()
        {
            return $"{Name} {Badges} {Pokemons.Count}";
        }
    }
}
