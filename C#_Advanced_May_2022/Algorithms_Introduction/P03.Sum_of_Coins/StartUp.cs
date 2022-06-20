using System;

namespace SumOfCoins
{
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var availableCoins = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var targetSum = int.Parse(Console.ReadLine());

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            var orderedCoins = coins.OrderByDescending(c => c).ToArray();
            var result = new Dictionary<int, int>();

            int index = 0;
            while (true)
            {
                if (index == orderedCoins.Length)
                    throw new InvalidOperationException("Error");

                if(targetSum - orderedCoins[index] < 0)
                {
                    index++;
                    continue;
                }

                targetSum -= orderedCoins[index];

                if (!result.ContainsKey(orderedCoins[index]))
                    result[orderedCoins[index]] = 1;
                else
                    result[orderedCoins[index]]++;

                if (targetSum == 0)
                    break;
            }

            return result;
        }
    }
}