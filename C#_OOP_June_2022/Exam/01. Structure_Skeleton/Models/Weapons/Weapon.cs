namespace PlanetWars.Models.Weapons
{
    using Contracts;
    using PlanetWars.Utilities.Messages;
    using System;

    public abstract class Weapon : IWeapon
    {
        private int destructionLevel;

        public Weapon(int destructionLevel, double price)
        {
            this.DestructionLevel = destructionLevel;
            this.Price = price;
        }

        public double Price { get; }

        public virtual int DestructionLevel
        {
            get => this.destructionLevel;
            private set
            {
                if (value < 1)
                    throw new ArgumentException(ExceptionMessages.TooLowDestructionLevel);

                if (value > 10)
                    throw new ArgumentException(ExceptionMessages.TooHighDestructionLevel);

                this.destructionLevel = value;
            }
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
