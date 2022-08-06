namespace NavalVessels.Models
{
    using Contracts;
    using Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Captain : ICaptain
    {
        private string fullName;
        private readonly ICollection<IVessel> vessels;

        public Captain(string fullName)
        {
            this.FullName = fullName;
            this.vessels = new List<IVessel>();
            this.CombatExperience = 0;
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);

                this.fullName = value;
            }
        }

        public int CombatExperience { get; private set; }

        public ICollection<IVessel> Vessels => this.vessels;

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);

            this.Vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            this.CombatExperience += 10;
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.FullName} has {this.CombatExperience} combat experience and commands {this.Vessels.Count} vessels.");

            foreach (var vessel in this.vessels)
            {
                sb.AppendLine(vessel.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
