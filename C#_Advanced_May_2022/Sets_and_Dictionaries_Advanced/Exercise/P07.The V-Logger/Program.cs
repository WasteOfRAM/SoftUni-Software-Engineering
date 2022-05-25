using System;
using System.Collections.Generic;
using System.Linq;

namespace P07.The_V_Logger
{
    internal class Program
    {
        static void Main()
        {
            var vLoger = new Dictionary<string, Dictionary<string, SortedSet<string>>>();

            string command;
            while ((command = Console.ReadLine()) != "Statistics")
            {
                string vlogerName = command.Split(' ')[0];
                string action = command.Split(' ')[1];

                if (action == "joined")
                {
                    if (!vLoger.ContainsKey(vlogerName))
                        vLoger[vlogerName] = new Dictionary<string, SortedSet<string>>() 
                        { 
                            {"followers", new SortedSet<string>()},
                            {"following", new SortedSet<string>()}
                        };
                }
                else
                {
                    string followed = command.Split(' ')[2];

                    if (vlogerName != followed && vLoger.ContainsKey(vlogerName) && vLoger.ContainsKey(followed))
                    {
                        vLoger[vlogerName]["following"].Add(followed);
                        vLoger[followed]["followers"].Add(vlogerName);
                    }
                }
            }

            vLoger = vLoger.OrderByDescending(followers => followers.Value["followers"].Count())
                           .ThenBy(following => following.Value["following"].Count())
                           .ToDictionary(k => k.Key, v => v.Value);

            Console.WriteLine($"The V-Logger has a total of {vLoger.Count} vloggers in its logs.");

            int vlogerIndex = 1;
            foreach (var vloger in vLoger)
            {
                Console.WriteLine($"{vlogerIndex}. {vloger.Key} : {vloger.Value["followers"].Count()} followers, {vloger.Value["following"].Count()} following");
                if (vlogerIndex == 1)
                {
                    foreach (var follower in vloger.Value["followers"])
                    {
                        Console.WriteLine($"*  {follower}");
                    } 
                }

                vlogerIndex++;
            }
        }
    }
}
