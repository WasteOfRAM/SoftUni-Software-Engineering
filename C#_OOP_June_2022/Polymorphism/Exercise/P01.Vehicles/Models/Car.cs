namespace P01.Vehicles.Models
{
    public class Car : Vehicles
    {
        private const double ACFuelConsumptionModifier = 0.9;

        public Car(double fuelQuantity, double fuelRate) 
            : base(fuelQuantity, fuelRate)
        {
            
        }

        public override double FuelConsumptionModifier => ACFuelConsumptionModifier;
    }
}
