using System;
using System.Collections.Generic;
using System.Linq;

namespace P07.Truck_Tour
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int pumpsCount = int.Parse(Console.ReadLine());

            Queue<Pump> pumps = new Queue<Pump>();

            Pump winingPump = new Pump();

            int truckPumpsReached = 0;

            for (int i = 0; i < pumpsCount; i++)
            {
                int[] pumpsData = Console.ReadLine().Split().Select(int.Parse).ToArray();

                pumps.Enqueue(new Pump(i, pumpsData[0], pumpsData[1]));
            }

            // Loop stops if the truck makes it to the last pump in queue
            while (truckPumpsReached != pumpsCount)
            {
                int truckFuel = 0;

                foreach (var pump in pumps)
                {
                    truckFuel += pump.Fuel;
                    truckPumpsReached++;

                    if (truckFuel < pump.Distance)
                    {
                        truckPumpsReached = 0;
                        break;
                    }

                    truckFuel -= pump.Distance;
                }

                winingPump = pumps.Peek();
                pumps.Enqueue(pumps.Dequeue()); 
            }

            Console.WriteLine(winingPump.Index);
        }
    }

    class Pump
    {
        public Pump()
        {
        }
        public Pump(int index, int fuel, int distance)
        {
            this.Index = index;
            this.Fuel = fuel;
            this.Distance = distance;
        }

        public int Index { get; set; }
        
        public int Fuel { get; set; } //Pump amount of fuel
        
        public int Distance { get; set; } //Pump distance to the next pump in queue
    }
}
