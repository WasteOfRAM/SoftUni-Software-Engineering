namespace P02.Vehicles_Extension.Models
{
    public class Car : Vehicles
    {
        private const double ACFuelConsumptionModifier = 0.9;

        public Car(double fuelQuantity, double fuelRate, double tankCapacity) 
            : base(fuelQuantity, fuelRate, tankCapacity)
        {
            
        }

        public override double FuelConsumptionModifier => ACFuelConsumptionModifier;
    }
}
