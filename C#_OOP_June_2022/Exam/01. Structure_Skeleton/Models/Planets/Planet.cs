namespace PlanetWars.Models.Planets
{
    using Contracts;
    using PlanetWars.Models.MilitaryUnits;
    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private readonly List<IMilitaryUnit> army;
        private readonly List<IWeapon> weapons;

        public Planet(string name, double budget)
        {
            this.Name = name;
            this.Budget = budget;
            this.army = new List<IMilitaryUnit>();
            this.weapons = new List<IWeapon>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);

                this.name = value;
            }
        }

        public double Budget
        {
            get => this.budget;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);

                this.budget = value;
            }
        }

        public double MilitaryPower => this.MilitaryPowerCalculation();

        public IReadOnlyCollection<IMilitaryUnit> Army => this.army;

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons;

        public void AddUnit(IMilitaryUnit unit)
        {
            

            this.army.Add(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public string PlanetInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Planet: {this.Name}")
                .AppendLine($"--Budget: {this.Budget} billion QUID")
                .AppendLine($"--Forces: {(this.Army.Count == 0 ? "No units" : string.Join(", ", this.Army))}")
                .AppendLine($"--Combat equipment: {(this.weapons.Count == 0 ? "No weapons" : string.Join(", ", this.weapons))}")
                .Append($"--Military Power: {this.MilitaryPower}");

            return sb.ToString();
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public void Spend(double amount)
        {
            if (this.Budget < amount)
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);

            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in this.army)
            {
                unit.IncreaseEndurance();
            }
        }

        private double MilitaryPowerCalculation()
        {
            var totalEndurence = this.army.Sum(u => u.EnduranceLevel);
            var totalDestructionLevel = this.weapons.Sum(w => w.DestructionLevel);

            double totalPower = totalEndurence + totalDestructionLevel;

            if(this.army.Any(u => u.GetType().Name == "AnonymousImpactUnit"))
            {
                totalPower *= 1.30;
            }

            if (this.weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                totalPower *= 1.45;
            }

            return Math.Round(totalPower, 3);
        }
    }
}
