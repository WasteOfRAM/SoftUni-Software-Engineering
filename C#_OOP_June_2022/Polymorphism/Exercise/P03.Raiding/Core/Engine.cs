namespace P03.Raiding.Core
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using IO;
    using IO.Interfaces;
    using Models;
    using Factories;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICollection<BaseHero> party;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.party = new List<BaseHero>();
        }

        public void Start()
        {
            int partySize = int.Parse(this.reader.ReadLine());

            for (int i = 0; i < partySize; i++)
            {
                string name = this.reader.ReadLine();
                string heroType = this.reader.ReadLine();

                BaseHero hero;

                try
                {
                    hero = HeroFactory.CreateHero(name, heroType);
                }
                catch (ArgumentException ae)
                {
                    this.writer.WriteLine(ae.Message);
                    i--;
                    continue;
                }

                this.party.Add(hero);
            }

            int bossHealth = int.Parse(this.reader.ReadLine());
            int partyDamage = this.party.Sum(power => power.Power);

            foreach (var hero in this.party)
            {
                this.writer.WriteLine(hero.CastAbility());
            }

            this.writer.WriteLine(bossHealth <= partyDamage ? "Victory!" : "Defeat...");
        }
    }
}
