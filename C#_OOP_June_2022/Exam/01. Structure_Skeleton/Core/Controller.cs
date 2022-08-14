namespace PlanetWars.Core
{
    using Contracts;
    using Repositories.Contracts;
    using Repositories;
    using Models.Planets;
    using Models.Planets.Contracts;
    using PlanetWars.Utilities.Messages;
    using System;
    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Models.MilitaryUnits;
    using System.Linq;
    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Models.Weapons;
    using System.Text;

    public class Controller : IController
    {
        private readonly IRepository<IPlanet> planetRepository;

        public Controller()
        {
            this.planetRepository = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            var planet = this.planetRepository.FindByName(planetName);
            if (planet == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            IMilitaryUnit militaryUnit;
            if(unitTypeName == "AnonymousImpactUnit")
            {
                militaryUnit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == "SpaceForces")
            {
                militaryUnit = new SpaceForces();
            }
            else if (unitTypeName == "StormTroopers")
            {
                militaryUnit = new StormTroopers();
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            if (planet.Army.Any(u => u.GetType().Name == unitTypeName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));

            planet.Spend(militaryUnit.Cost);
            planet.AddUnit(militaryUnit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            var planet = this.planetRepository.FindByName(planetName);
            if (planet == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            if (planet.Weapons.Any(u => u.GetType().Name == weaponTypeName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));

            IWeapon weapon;
            if(weaponTypeName == "BioChemicalWeapon")
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == "SpaceMissiles")
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            var planet = this.planetRepository.FindByName(name);

            if (planet != null)
                return string.Format(OutputMessages.ExistingPlanet, name);

            planet = new Planet(name, budget);

            this.planetRepository.AddItem(planet);

            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            var sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in this.planetRepository.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var attackingPlanet = this.planetRepository.FindByName(planetOne);
            var defendingPlanet = this.planetRepository.FindByName(planetTwo);

            IPlanet winingPlanet;
            IPlanet losingPlanet;

            if(attackingPlanet.MilitaryPower == defendingPlanet.MilitaryPower)
            {
                bool tie = false;

                if(attackingPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && defendingPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    tie = true;
                }

                if (!attackingPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && !defendingPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    tie = true;
                }

                if (tie)
                {
                    attackingPlanet.Spend(attackingPlanet.Budget / 2);
                    defendingPlanet.Spend(defendingPlanet.Budget / 2);

                    return OutputMessages.NoWinner;
                }

                if(attackingPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    winingPlanet = attackingPlanet;
                    losingPlanet = defendingPlanet;
                }
                else
                {
                    winingPlanet = defendingPlanet;
                    losingPlanet = attackingPlanet;
                }
            }
            else
            {
                if(attackingPlanet.MilitaryPower > defendingPlanet.MilitaryPower)
                {
                    winingPlanet = attackingPlanet;
                    losingPlanet = defendingPlanet;
                }
                else
                {
                    winingPlanet = defendingPlanet;
                    losingPlanet = attackingPlanet;
                }
            }

            winingPlanet.Spend(winingPlanet.Budget / 2);

            winingPlanet.Profit(losingPlanet.Budget / 2);

            double losingPlanetMilitaryCost = losingPlanet.Army.Sum(u => u.Cost) + losingPlanet.Weapons.Sum(w => w.Price);

            winingPlanet.Profit(losingPlanetMilitaryCost);

            string losingPlanetName = losingPlanet.Name;

            this.planetRepository.RemoveItem(losingPlanet.Name);

            return string.Format(OutputMessages.WinnigTheWar, winingPlanet.Name, losingPlanetName);
        }

        public string SpecializeForces(string planetName)
        {
            var planet = this.planetRepository.FindByName(planetName);
            if (planet == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            if (planet.Army.Count == 0)
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);

            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
