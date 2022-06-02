using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    internal class Car
    {
        private string model;
        private double fuelAmount;
        private double fuelRatePerKilometer;
        private double travelledDistance;

        public string Model { get => this.model; set => this.model = value; }
        public double FuelAmount { get => this.fuelAmount; set => this.fuelAmount = value; }
        public double FuelRatePerKilometer { get => this.fuelRatePerKilometer; set => this.fuelRatePerKilometer = value; }

        public Car(string model, double fuelAmount, double fuelRatePerKilometer)
        {
            this.model = model;
            this.fuelAmount = fuelAmount;
            this.fuelRatePerKilometer = fuelRatePerKilometer;

            this.travelledDistance = 0;
        }

        public void Drive(int distance)
        {
            double fuelNeeded = this.fuelRatePerKilometer * distance;

            if (fuelNeeded <= this.fuelAmount)
            {
                this.fuelAmount -= fuelNeeded;
                this.travelledDistance += distance;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }

        public void Print()
        {
            Console.WriteLine($"{this.model} {this.fuelAmount:f2} {this.travelledDistance}");
        }
    }
}
