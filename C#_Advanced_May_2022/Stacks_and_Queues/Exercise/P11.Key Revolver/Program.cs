using System;
using System.Collections.Generic;
using System.Linq;

namespace P11.Key_Revolver
{
    internal class Program
    {
        static void Main()
        {
            int bulletPrice = int.Parse(Console.ReadLine());
            int barelSize = int.Parse(Console.ReadLine());
            Stack<int> bullets = new Stack<int>(Console.ReadLine().Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Queue<int> locks = new Queue<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            int intelelligence = int.Parse(Console.ReadLine());

            int startingBullets = bullets.Count;
            int bulletsUsed = 0;

            while (bullets.Count > 0 && locks.Count > 0)
            {
                int curentBullet = bullets.Pop();
                bulletsUsed++;

                if (curentBullet <= locks.Peek())
                {
                    Console.WriteLine("Bang!");
                    locks.Dequeue();
                }
                else
                {
                    Console.WriteLine("Ping!");
                }

                if (bulletsUsed % barelSize == 0 && bullets.Count > 0)
                {
                    Console.WriteLine("Reloading!");
                }
            }

            int moneySpent = bulletPrice * bulletsUsed;
            int moneyEarned = intelelligence - moneySpent;

            if (locks.Count == 0)
            {
                Console.WriteLine($"{startingBullets - bulletsUsed} bullets left. Earned ${moneyEarned}");
            }
            else
            {
                Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}");
            }
        }
    }
}
