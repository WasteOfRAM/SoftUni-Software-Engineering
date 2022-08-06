namespace NavalVessels.Models
{
    using Contracts;
    using Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private readonly ICollection<string> targets;

        public Vessel(string name, double mainWeaponCaliber, double speed, double armorThicknes)
        {
            this.Name = name;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
            this.ArmorThickness = armorThicknes;
            this.targets = new List<string>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);

                this.name = value;
            }
        }

        public ICaptain Captain
        {
            get => this.captain;
            set
            {
                if(value == null)
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainName);

                this.captain = value;
            }
        }
        public double ArmorThickness { get; set; }

        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets => this.targets;

        public void Attack(IVessel target)
        {
            if (target == null)
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);

            target.ArmorThickness -= this.MainWeaponCaliber;
            if(target.ArmorThickness < 0)
                target.ArmorThickness = 0;

            this.Targets.Add(target.Name);
        }

        public abstract void RepairVessel();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"- {this.Name}")
                .AppendLine($" *Type: {this.GetType().Name}")
                .AppendLine($" *Armor thickness: {this.ArmorThickness}")
                .AppendLine($" *Main weapon caliber: {this.MainWeaponCaliber}")
                .AppendLine($" *Speed: {this.Speed} knots")
                .AppendLine($" *Targets: {(this.Targets.Count == 0 ? "None" : string.Join(", ", this.Targets))}");

            return sb.ToString();
        }
    }
}
