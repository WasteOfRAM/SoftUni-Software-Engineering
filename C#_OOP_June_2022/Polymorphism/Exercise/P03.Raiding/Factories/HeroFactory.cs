namespace P03.Raiding.Factories
{
    using System;

    using Interfaces;
    using P03.Raiding.Models;

    public abstract class HeroFactory /*: IHeroFactory*/
    {
        public static BaseHero CreateHero(string name, string type)
        {
            BaseHero hero;

            if (type == "Druid")
                hero = new Druid(name);
            else if (type == "Paladin")
                hero = new Paladin(name);
            else if (type == "Rogue")
                hero = new Rogue(name);
            else if (type == "Warrior")
                hero = new Warrior(name);
            else
                throw new ArgumentException("Invalid hero!");

            return hero;
        }
    }
}
