using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUniParking
{
    internal class Parking
    {
        private List<Car> cars;
        private int capacity;

        public List<Car> Cars { get => this.cars; set => this.cars = value; }
        public int Capacity { get => this.capacity; set => this.capacity = value; }
        public int Count { get => this.cars.Count; }

        public Parking(int capacity)
        {
            this.cars = new List<Car>();
            this.capacity = capacity;
        }

        public string AddCar(Car car)
        {
            if (this.cars.Any(curent => curent.RegistrationNumber == car.RegistrationNumber))
                return "Car with that registration number, already exists!";

            if (this.cars.Count == this.capacity)
                return "Parking is full!";
            

            this.cars.Add(car);
            return $"Successfully added new car {car.Make} {car.RegistrationNumber}";
        }

        public string RemoveCar(string plateNumber)
        {
            Car carToRemove = cars.Find(car => car.RegistrationNumber == plateNumber);

            if (carToRemove == null)
                return "Car with that registration number, doesn't exist!";


            this.cars.Remove(carToRemove);
            return $"Successfully removed {plateNumber}";
        }

        public Car GetCar(string platenumber)
        {
            return this.cars.Find(car => car.RegistrationNumber == platenumber);
        }

        public void RemoveSetOfRegistrationNumber(List<string> RegistrationNumbers)
        {
            foreach (var plateNumber in RegistrationNumbers)
            {
                RemoveCar(plateNumber);
            }
        }
    }
}
