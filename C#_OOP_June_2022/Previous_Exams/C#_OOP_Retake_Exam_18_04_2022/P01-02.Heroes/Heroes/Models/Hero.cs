namespace Heroes.Models
{
    using Contracts;
    using System;
    using System.Text;

    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        public Hero(string name, int health, int armour)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armour;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Hero name cannot be null or empty.");

                this.name = value;
            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Hero health cannot be below 0.");

                this.health = value;
            }
        }

        public int Armour
        {
            get
            {
                return this.armour;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Hero armour cannot be below 0.");

                this.armour = value;
            }
        }

        public IWeapon Weapon
        {
            get
            {
                return this.weapon;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentException("Weapon cannot be null.");

                this.weapon = value;
            }
        }

        public bool IsAlive => this.health > 0;

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            if (this.Armour >= points)
            {
                this.Armour -= points;
            }
            else
            {
                points -= this.Armour;
                this.Armour = 0;

                if (this.health >= points)
                {
                    this.health -= points;
                }
                else
                {
                    this.health = 0;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string weapon = this.Weapon == null ? "Unarmed" : this.Weapon.Name;

            sb.AppendLine($"{this.GetType().Name}: {this.Name}")
                .AppendLine($"--Health: {this.Health}")
                .AppendLine($"--Armour: {this.Armour}")
                .AppendLine($"--Weapon: {weapon}");

            return sb.ToString().Trim();
        }
    }
}
