using System;
using System.Collections.Generic;

namespace P05.Cities_by_Continent_and_Country
{
    internal class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            var continents = new Dictionary<string, Dictionary<string, List<string>>>();

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string continent = input.Split(' ')[0];
                string country = input.Split(' ')[1];
                string city = input.Split(' ')[2];

                if(!continents.ContainsKey(continent))
                    continents[continent] = new Dictionary<string, List<string>>();

                if(!continents[continent].ContainsKey(country))
                    continents[continent][country] = new List<string>();

                continents[continent][country].Add(city);
            }

            foreach (var continent in continents)
            {
                Console.WriteLine(continent.Key + ":");
                foreach (var (country, citys) in continent.Value)
                {
                    Console.WriteLine($"{country} -> {string.Join(", ", citys)}");
                }
            }
        }
    }
}
