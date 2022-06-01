using System;

namespace CarManufacturer
{
    public class Car
    {
        

        private string make;
        private string model;
        private int year;
        private double fuelQuantity;
        private double fuelConsumption;
        private Engine engine;
        private Tire[] tires;

        public string Make { get => this.make; set => this.make = value; }
        public string Model { get => this.model; set => this.model = value; }
        public int Year { get => this.year; set => this.year = value; }

        public double FuelQuantity
        {
            get { return fuelQuantity; }
            set { fuelQuantity = value; }
        }


        public double FuelConsumption
        {
            get { return this.fuelConsumption; }
            set { this.fuelConsumption = value; }
        }

        public Engine Engine { get => this.engine; set => this.engine = value; }
        public Tire[] Tires { get => this.tires; set => this.tires = value; }

        public Car()
        {
            this.Make = "VW";
            this.Model = "Golf";
            this.Year = 2025;
            this.FuelQuantity = 200;
            this.FuelConsumption = 10;
        }

        public Car(string make, string model, int year)
            : this()
        {
            this.make = make;
            this.model = model;
            this.year = year;
        }

        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption)
            : this(make, model, year)
        {
            this.fuelQuantity = fuelQuantity;
            this.fuelConsumption = fuelConsumption;
        }

        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption, Engine engine, Tire[] tiers)
            :this(make, model, year, fuelQuantity, fuelConsumption)
        {
            this.engine = engine;
            this.tires = tiers;
        }

        public void Drive(double distance)
        {
            double fuelNeeded = distance * (this.fuelConsumption / 100);

            if(this.fuelQuantity - fuelNeeded >= 0)
                this.fuelQuantity -= fuelNeeded;
            else
                Console.WriteLine("Not enough fuel to perform this trip!");
        }

        public string WhoAmI()
        {
            string result =
                $"Make: {this.make}\n" +
                $"Model: {this.model}\n" +
                $"Year: {this.year}\n" +
                $"HorsePowers: {this.engine.HorsePower}\n" +
                $"FuelQuantity: {this.fuelQuantity}";

            return result;
        }
    }
}
