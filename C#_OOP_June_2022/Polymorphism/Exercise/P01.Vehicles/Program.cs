namespace P01.Vehicles
{
    using System;
    using Models;
    public class Program
    {
        static void Main()
        {
            string[] carInfo = Console.ReadLine().Split();
            string[] truckIndo = Console.ReadLine().Split();
            int commandsNum = int.Parse(Console.ReadLine());

            var car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]));
            var truck = new Truck(double.Parse(truckIndo[1]), double.Parse(truckIndo[2]));

            for (int i = 0; i < commandsNum; i++)
            {
                var commands = Console.ReadLine().Split();

                if (commands[0] == "Drive")
                {
                    if (commands[1] == "Car")
                        Console.WriteLine(car.Drive(double.Parse(commands[2])));
                    else
                        Console.WriteLine(truck.Drive(double.Parse(commands[2])));
                }
                else
                {
                    if (commands[1] == "Car")
                        car.Refuel(double.Parse(commands[2]));
                    else
                        truck.Refuel(double.Parse(commands[2]));
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
        }
    }
}
