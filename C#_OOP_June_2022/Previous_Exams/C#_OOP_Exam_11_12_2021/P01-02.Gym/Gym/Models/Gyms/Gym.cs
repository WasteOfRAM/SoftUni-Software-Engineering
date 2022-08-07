namespace Gym.Models.Gyms
{
    using Contracts;
    using System.Collections.Generic;
    using Equipment.Contracts;
    using Athletes.Contracts;
    using System;
    using Utilities.Messages;
    using System.Linq;
    using System.Text;

    public abstract class Gym : IGym
    {
        private string name;
        private readonly ICollection<IEquipment> equipment;
        private readonly ICollection<IAthlete> athletes;

        public Gym(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.equipment = new HashSet<IEquipment>();
            this.athletes = new HashSet<IAthlete>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);

                this.name = value;
            }
        }

        public int Capacity { get; }

        public double EquipmentWeight => this.Equipment.Sum(e => e.Weight);

        public ICollection<IEquipment> Equipment => this.equipment;

        public ICollection<IAthlete> Athletes => this.athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (this.Athletes.Count >= this.Capacity)
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);

            this.Athletes.Add(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            this.Equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in this.Athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.Name} is a {this.GetType().Name}:")
                .AppendLine($"Athletes: {(this.Athletes.Count == 0 ? "No athletes" : string.Join(", ", this.Athletes.Select(a => a.FullName)))}")
                .AppendLine($"Equipment total count: {this.Equipment.Count}")
                .Append($"Equipment total weight: {this.EquipmentWeight:f2} grams");

            return sb.ToString();
        }

        public bool RemoveAthlete(IAthlete athlete) => this.Athletes.Remove(athlete);
    }
}
