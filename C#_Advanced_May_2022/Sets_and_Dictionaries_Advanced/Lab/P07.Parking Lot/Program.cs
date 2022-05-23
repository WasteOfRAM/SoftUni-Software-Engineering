using System;
using System.Collections.Generic;

namespace P07.Parking_Lot
{
    internal class Program
    {
        static void Main()
        {
            HashSet<string> parkingLot = new HashSet<string>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string direction = input.Split(", ")[0];
                string plateNumber = input.Split(", ")[1];

                if(direction == "IN")
                    parkingLot.Add(plateNumber);
                else
                    parkingLot.Remove(plateNumber);
            }

            if(parkingLot.Count == 0)
                Console.WriteLine("Parking Lot is Empty");
            else
                Console.WriteLine(string.Join(Environment.NewLine, parkingLot));
        }
    }
}
