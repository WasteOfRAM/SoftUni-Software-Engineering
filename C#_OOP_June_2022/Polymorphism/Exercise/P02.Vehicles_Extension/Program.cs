namespace P02.Vehicles_Extension
{
    using System;
    using Models;
    public class Program
    {
        static void Main()
        {
            string[] carInfo = Console.ReadLine().Split();
            string[] truckInfo = Console.ReadLine().Split();
            string[] busInfo = Console.ReadLine().Split();
            int commandsNum = int.Parse(Console.ReadLine());

            Vehicles car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));
            Vehicles truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));
            Vehicles bus = new Bus(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

            for (int i = 0; i < commandsNum; i++)
            {
                var commands = Console.ReadLine().Split();

                try
                {
                    if (commands[0] == "Drive")
                    {
                        if (commands[1] == "Car")
                            Console.WriteLine(car.Drive(double.Parse(commands[2])));
                        else if (commands[1] == "Truck")
                            Console.WriteLine(truck.Drive(double.Parse(commands[2])));
                        else
                            Console.WriteLine(bus.Drive(double.Parse(commands[2])));
                    }
                    else if (commands[0] == "Refuel")
                    {
                        if (commands[1] == "Car")
                            car.Refuel(double.Parse(commands[2]));
                        else if (commands[1] == "Truck")
                            truck.Refuel(double.Parse(commands[2]));
                        else
                            bus.Refuel(double.Parse(commands[2]));
                    }
                    else if (commands[0] == "DriveEmpty")
                    {
                        Bus emptyBus = bus as Bus;
                        Console.WriteLine(emptyBus.DriveEmpty(double.Parse(commands[2])));
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
            Console.WriteLine(bus);
        }
    }
}
