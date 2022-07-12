namespace P01.Vehicles.Models
{
    public class Vehicles
    {
        private double fuelQuantity;
        private double fuelConsumption;

        public Vehicles(double fuelQuantity, double fuelRate)
        {
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
            this.FuelQuantity += fuelAmount;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.FuelQuantity:f2}";
        }
    }
}
