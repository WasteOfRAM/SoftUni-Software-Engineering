using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guild
{
    public class Guild
    {
        private List<Player> roster;

        public Guild(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.roster = new List<Player>();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }

        public int Count => this.roster.Count;

        public void AddPlayer(Player player)
        {
            if (this.Count < this.Capacity)
                this.roster.Add(player);
        }

        public bool RemovePlayer(string name)
        {
            var playerToRemove = roster.FirstOrDefault(p => p.Name == name);
            return roster.Remove(playerToRemove);
        }

        public void PromotePlayer(string name)
        {
            if (roster.FirstOrDefault(p => p.Name == name) != null)
                roster.FirstOrDefault(p => p.Name == name).Rank = "Member";
        }

        public void DemotePlayer(string name)
        {
            if (roster.FirstOrDefault(p => p.Name == name) != null)
                roster.FirstOrDefault(p => p.Name == name).Rank = "Trial";
        }

        public Player[] KickPlayersByClass(string playerClass)
        {
            Player[] removedPlayers = roster.Where(p => p.Class == playerClass).ToArray();
            roster.RemoveAll(p => p.Class == playerClass);

            return removedPlayers;
        }

        public string Report()
        {
            string result = $"Players in the guild: {this.Name}";
            foreach (var item in roster)
            {
                result += Environment.NewLine + item.ToString();
            }
            return result;
        }
    }
}
