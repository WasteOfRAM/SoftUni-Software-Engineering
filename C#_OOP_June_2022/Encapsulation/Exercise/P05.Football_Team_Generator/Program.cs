using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.Football_Team_Generator
{
    public class Program
    {
        static void Main()
        {
            var teams = new List<Team>();
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                var cmdArgs = command.Split(";", StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    if (cmdArgs[0] == "Team")
                    {
                        teams.Add(new Team(cmdArgs[1]));
                    }
                    else
                    {
                        var teamName = cmdArgs[1];
                        var team = teams.FirstOrDefault(t => t.Name == teamName);

                        if (team == null)
                            throw new ArgumentException(string.Format(ExeptionMessage.TeamDoesNotExist, teamName));

                        if (cmdArgs[0] == "Rating")
                        {
                            Console.WriteLine(team);
                            continue;
                        }

                        var playerName = cmdArgs[2];

                        if (cmdArgs[0] == "Add")
                        {
                            var endurance = int.Parse(cmdArgs[3]);
                            var sprint = int.Parse(cmdArgs[4]);
                            var dribble = int.Parse(cmdArgs[5]);
                            var passing = int.Parse(cmdArgs[6]);
                            var shooting = int.Parse(cmdArgs[7]);

                            var playerStats = new Stats(endurance, sprint, dribble, passing, shooting);
                            var player = new Player(playerName, playerStats);

                            team.AddPlayer(player);
                        }
                        else if (cmdArgs[0] == "Remove")
                        {
                            team.RemovePlayer(playerName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
