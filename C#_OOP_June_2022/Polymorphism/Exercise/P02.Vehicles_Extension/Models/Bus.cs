namespace P02.Vehicles_Extension.Models
{
    public class Bus : Vehicles
    {
        private const double ACFuelConsumptionModifier = 1.4;

        public Bus(double fuelQuantity, double fuelRate, double tankCapacity) 
            : base(fuelQuantity, fuelRate, tankCapacity)
        {

        }

        public override double FuelConsumptionModifier => ACFuelConsumptionModifier;

        public string DriveEmpty(double distance)
        {
            double litersNeeded = (this.FuelConsumption - ACFuelConsumptionModifier) * distance;

            if (litersNeeded <= this.FuelQuantity)
            {
                this.FuelQuantity -= litersNeeded;
                return $"{this.GetType().Name} travelled {distance} km";
            }

            return $"{this.GetType().Name} needs refueling";
        }
    }
}
