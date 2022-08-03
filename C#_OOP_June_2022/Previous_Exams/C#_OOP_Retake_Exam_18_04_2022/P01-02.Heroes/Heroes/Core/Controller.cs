namespace Heroes.Core
{
    using Contracts;
    using Models.Heroes;
    using Models.Weapons;
    using Models.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using System;
    using System.Reflection;
    using System.Linq;
    using System.Collections.Generic;
    using Models.Map;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IHero> heroes = new HeroRepository();
        private IRepository<IWeapon> weapons = new WeaponRepository();

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (!heroes.Models.Any(h => h.Name == heroName))
                throw new InvalidOperationException($"Hero {heroName} does not exist.");

            if (!weapons.Models.Any(w => w.Name == weaponName))
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");

            IHero hero = this.heroes.Models.FirstOrDefault(n => n.Name == heroName);
            IWeapon weapon = this.weapons.Models.FirstOrDefault(n => n.Name == weaponName);

            if (hero.Weapon != null)
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            else
                hero.AddWeapon(weapon);

            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (this.heroes.Models.Any(n => n.Name == name))
                throw new InvalidOperationException($"The hero {name} already exists.");

            IHero hero = null;

            if (type == "Knight")
            {
                hero = new Knight(name, health, armour);
            }else if(type == "Barbarian")
            {
                hero = new Barbarian(name, health, armour);
            }
            else
            {
                throw new InvalidOperationException("Invalid hero type.");
            }

            this.heroes.Add(hero);

            string message;

            if (type == "Knight")
                message = $"Successfully added Sir {name} to the collection.";
            else
                message = $"Successfully added Barbarian {name} to the collection.";

            return message;
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (this.weapons.Models.Any(n => n.Name == name))
                throw new InvalidOperationException($"The weapon {name} already exists.");

            IWeapon weapon = null;

            if (type == "Claymore")
                weapon = new Claymore(name, durability);
            else if (type == "Mace")
                weapon = new Mace(name, durability);
            else
                throw new InvalidOperationException($"Invalid weapon type.");

            this.weapons.Add(weapon);

            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var hero in this.heroes.Models.OrderBy(ht => ht.GetType().Name).ThenByDescending(hh => hh.Health).ThenBy(hn => hn.Name))
            {
                sb.AppendLine(hero.ToString());
            }

            return sb.ToString().Trim();
        }

        public string StartBattle()
        {
            HashSet<IHero> validHeroses = this.heroes.Models.Where(h => h.IsAlive && h.Weapon != null).ToHashSet();
            IMap map = new Map();

            return map.Fight(validHeroses);
        }
    }
}
