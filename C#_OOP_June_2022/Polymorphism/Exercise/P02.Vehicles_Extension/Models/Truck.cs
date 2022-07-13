namespace P02.Vehicles_Extension.Models
{
    using System;
    public class Truck : Vehicles
    {
        private const double ACFuelConsumptionModifier = 1.6;
        public Truck(double fuelQuantity, double fuelRate, double tankCapacity) 
            : base(fuelQuantity, fuelRate, tankCapacity)
        {

        }

        public override double FuelConsumptionModifier => ACFuelConsumptionModifier;

        public override void Refuel(double fuelAmount)
        {
            if (this.FuelQuantity + fuelAmount > this.TankCapacity)
                throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");

            if (fuelAmount <= 0)
                throw new ArgumentException("Fuel must be a positive number");

            this.FuelQuantity += fuelAmount * 0.95;
        }
    }
}
