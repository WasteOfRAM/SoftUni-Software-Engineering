namespace P05.Football_Team_Generator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Team
    {
        private List<Player> players;
        private string name;

        public Team(string name)
        {
            this.Name = name;
            this.players = new List<Player>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExeptionMessage.InvalidName);

                this.name = value;
            }
        }

        public int Rating
        {
            get
            {
                if(this.players.Count > 0)
                    return (int)Math.Round(players.Average(player => player.Stats.GetAverageSkill()));

                return 0;
            }
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(string player)
        {
            var playerToRemove = players.FirstOrDefault(p => p.Name == player);

            if (playerToRemove == null)
                throw new ArgumentException(string.Format(ExeptionMessage.PlayerNotInTeam, player, this.Name));

            players.Remove(playerToRemove);
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Rating}";
        }
    }
}
