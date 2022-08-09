namespace Easter.Models.Bunnies
{
    using Contracts;
    using Easter.Models.Dyes.Contracts;
    using Easter.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private readonly ICollection<IDye> dyes;

        public Bunny(string name, int energy, ICollection<IDye> dyes)
        {
            this.Name = name;
            this.Energy = energy;
            this.dyes = dyes;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);

                this.name = value;
            }
        }

        public int Energy
        {
            get => this.energy;
            protected set
            {
                if (value < 0)
                    this.energy = 0;
                else
                    this.energy = value;
            }
        }

        public ICollection<IDye> Dyes => this.dyes;

        public void AddDye(IDye dye) => this.Dyes.Add(dye);

        public abstract void Work();

        public override string ToString()
        {
            int notFinishedDyes = this.Dyes.Where(d => !d.IsFinished()).ToList().Count;

            var sb = new StringBuilder();

            sb.AppendLine($"Name: {this.Name}")
                .AppendLine($"Energy: {this.Energy}")
                .Append($"Dyes: {notFinishedDyes} not finished");

            return sb.ToString();
        }
    }
}
