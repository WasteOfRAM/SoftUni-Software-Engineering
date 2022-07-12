namespace P01.Vehicles.Models
{
    public class Truck : Vehicles
    {
        private const double ACFuelConsumptionModifier = 1.6;
        public Truck(double fuelQuantity, double fuelRate) 
            : base(fuelQuantity, fuelRate)
        {

        }

        public override double FuelConsumptionModifier => ACFuelConsumptionModifier;

        public override void Refuel(double fuelAmount)
        {
            base.Refuel(fuelAmount * 0.95);
        }
    }
}
