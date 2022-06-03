using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniParking
{
    internal class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int HorsePower { get; set; }
        public string RegistrationNumber { get; set; }

        public Car(string make, string model, int horsePower, string plateNumber)
        {
            this.Make = make;
            this.Model = model;
            this.HorsePower = horsePower;
            this.RegistrationNumber = plateNumber;
        }

        public override string ToString()
        {
            string result =
                $"Make: {this.Make}{Environment.NewLine}" +
                $"Model: {this.Model}{Environment.NewLine}" +
                $"HorsePower: {this.HorsePower}{Environment.NewLine}" +
                $"RegistrationNumber: {this.RegistrationNumber}";

            return result;
        }
    }
}
