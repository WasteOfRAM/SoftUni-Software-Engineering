using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Birthday_Celebration
{
    internal class Program
    {
        static void Main()
        {
            var guests = new Queue<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            var plates = new Stack<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            int wastedFood = 0;

            while (plates.Count > 0 && guests.Count > 0)
            {
                int plate = plates.Pop();
                int guest = guests.Dequeue();


                if (guest - plate <= 0)
                {
                    wastedFood += plate - guest;
                }
                else
                {
                    guest -= plate;
                    var tempQue = new Queue<int>();
                    tempQue.Enqueue(guest);

                    foreach (var guestInQue in guests)
                    {
                        tempQue.Enqueue(guestInQue);
                    }

                    guests = tempQue;
                }
            }

            Console.WriteLine(guests.Count == 0 ? $"Plates: {string.Join(' ', plates)}" : $"Guests: {string.Join(' ', guests)}");
            Console.WriteLine($"Wasted grams of food: {wastedFood}");
        }
    }
}
