namespace CarRacing.Models.Racers
{
    using System;
    using Contracts;
    using Cars.Contracts;
    using CarRacing.Utilities.Messages;
    using System.Text;

    public abstract class Racer : IRacer
    {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            this.Username = username;
            this.RacingBehavior = racingBehavior;
            this.DrivingExperience = drivingExperience;
            this.Car = car;
        }

        public string Username
        {
            get => this.username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);

                this.username = value;
            }
        }

        public string RacingBehavior
        {
            get => this.racingBehavior;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);

                this.racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => this.drivingExperience;
            protected set
            {
                if(value < 0 || value > 100)
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);

                this.drivingExperience = value;
            }
        }

        public ICar Car
        {
            get => this.car;
            private set
            {
                if(value == null)
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);

                this.car = value;
            }
        }

        public bool IsAvailable()
        {
            return this.Car.FuelAvailable >= this.Car.FuelConsumptionPerRace;
        }

        public virtual void Race()
        {
            this.Car.Drive();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.Username}")
                .AppendLine($"--Driving behavior: {this.RacingBehavior}")
                .AppendLine($"--Driving experience: {this.DrivingExperience}")
                .AppendLine($"--Car: {this.Car.Make} {this.Car.Model} ({this.Car.VIN})");

            return sb.ToString().TrimEnd();
        }
    }
}
