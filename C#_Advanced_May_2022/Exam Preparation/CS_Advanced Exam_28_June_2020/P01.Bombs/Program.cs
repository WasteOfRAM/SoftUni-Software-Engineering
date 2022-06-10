using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Bombs
{
    public class Program
    {
        static void Main()
        {
            var effects = new Queue<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            var casings = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            // Bomb materials
            const int CHERRY = 60;
            const int DETURA = 40;
            const int SMOKE = 120; 

            // Bomb types count
            int cherryCount = 0;
            int daturaCount = 0;
            int smokeDecoy = 0;

            while (!IsPouchFull(cherryCount, daturaCount, smokeDecoy))
            {
                if (effects.Count == 0 || casings.Count == 0)
                    break;

                int ingredientsSum = effects.Peek() + casings.Peek();

                if (ingredientsSum == CHERRY)
                {
                    cherryCount++;
                    effects.Dequeue();
                    casings.Pop();
                }
                else if (ingredientsSum == DETURA)
                {
                    daturaCount++;
                    effects.Dequeue();
                    casings.Pop();
                }
                else if (ingredientsSum == SMOKE)
                {
                    smokeDecoy++;
                    effects.Dequeue();
                    casings.Pop();
                }
                else
                {
                    int newCastValue = casings.Pop() - 5;
                    casings.Push(newCastValue);
                }
            }

            if(IsPouchFull(cherryCount, daturaCount, smokeDecoy))
                Console.WriteLine("Bene! You have successfully filled the bomb pouch!");
            else
                Console.WriteLine("You don't have enough materials to fill the bomb pouch.");

            string bombEffects = effects.Count == 0 ? "empty" : string.Join(", ", effects);
            string bombCasings = casings.Count == 0 ? "empty" : string.Join(", ", casings);

            Console.WriteLine($"Bomb Effects: {bombEffects}");
            Console.WriteLine($"Bomb Casings: {bombCasings}");

            Console.WriteLine($"Cherry Bombs: {cherryCount}");
            Console.WriteLine($"Datura Bombs: {daturaCount}");
            Console.WriteLine($"Smoke Decoy Bombs: {smokeDecoy}");
        }

        static bool IsPouchFull(int cherryCount, int deturaCount, int smokeDecoy)
        {
            if(cherryCount >= 3 && deturaCount >= 3 && smokeDecoy >= 3)
                return true;

            return false;
        }
    }
}
