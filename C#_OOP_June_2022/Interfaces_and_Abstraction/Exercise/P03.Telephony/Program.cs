using System;

namespace P03.Telephony
{
    public class Program
    {
        static void Main()
        {
            var numbers = Console.ReadLine().Split();
            var webSites = Console.ReadLine().Split();

            var smartPhone = new Smartphone();
            var stationaryPhone = new StationaryPhone();

            foreach (var number in numbers)
            {
                try
                {
                    if (number.Length == 10)
                        Console.WriteLine(smartPhone.MakeCall(number));
                    else
                        Console.WriteLine(stationaryPhone.MakeCall(number));
                    
                    
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }

            foreach (var site in webSites)
            {
                try
                {
                    Console.WriteLine(smartPhone.Browsing(site));
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }
        }
    }
}
