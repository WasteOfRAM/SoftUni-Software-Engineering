namespace P02.Vehicles_Extension.Models
{
    using System;
    public class Vehicles
    {
        private double fuelQuantity;
        private double fuelConsumption;

        public Vehicles(double fuelQuantity, double fuelRate, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;

            if(this.TankCapacity < fuelQuantity || fuelQuantity <= 0)
                this.FuelQuantity = 0;
            else
                this.FuelQuantity = fuelQuantity;

            this.FuelConsumption = fuelRate;
        }

        public double FuelQuantity
        {
            get
            {
                return this.fuelQuantity;
            }
            protected set
            {
                this.fuelQuantity = value;
            }
        }
        public double FuelConsumption
        {
            get
            {
                return this.fuelConsumption;
            }
            protected set
            {
                this.fuelConsumption = value + this.FuelConsumptionModifier;
            }
        }
        public virtual double FuelConsumptionModifier { get; }

        public double TankCapacity { get; private set; }

        public virtual string Drive(double distance)
        {
            double litersNeeded = this.FuelConsumption * distance;

            if (litersNeeded <= this.FuelQuantity)
            {
                this.FuelQuantity -= litersNeeded;
                return $"{this.GetType().Name} travelled {distance} km";
            }

            return $"{this.GetType().Name} needs refueling";
        }

        public virtual void Refuel(double fuelAmount)
        {
            if (this.FuelQuantity + fuelAmount > this.TankCapacity)
                throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");

            if (fuelAmount <= 0)
                throw new ArgumentException("Fuel must be a positive number");

            this.FuelQuantity += fuelAmount;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:f2}";
        }
    }
}
