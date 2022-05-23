using System;
using System.Collections.Generic;
using System.Linq;

namespace P08.SoftUni_Party
{
    internal class Program
    {
        static void Main()
        {
            HashSet<string> guestList = new HashSet<string>();

            while (true)
            {
                string reservationNumber = Console.ReadLine();

                if(reservationNumber == "PARTY")
                    break;

                guestList.Add(reservationNumber);
            }

            while (true)
            {
                string arrivals = Console.ReadLine();

                if(arrivals == "END")
                {
                    Console.WriteLine(guestList.Count);
                    Console.WriteLine(string.Join(Environment.NewLine, guestList.OrderByDescending(code => char.IsDigit(code[0]))));
                    break;
                }

                if (guestList.Contains(arrivals))
                    guestList.Remove(arrivals);
            }
        }
    }
}
