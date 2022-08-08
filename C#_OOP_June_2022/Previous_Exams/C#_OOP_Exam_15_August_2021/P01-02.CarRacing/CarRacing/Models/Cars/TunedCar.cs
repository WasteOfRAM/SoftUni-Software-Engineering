namespace CarRacing.Models.Cars
{
    using System;

    public class TunedCar : Car
    {
        private const double AvailableFuel = 65;
        private const double FuelConsumption = 7.5;

        public TunedCar(string make, string model, string vin, int horsePower) 
            : base(make, model, vin, horsePower, AvailableFuel, FuelConsumption)
        {
        }

        public override void Drive()
        {
            base.Drive();
            this.HorsePower -= (int)Math.Round(this.HorsePower * 0.03);
        }
    }
}
