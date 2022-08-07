namespace SpaceStation.Core
{
    using Contracts;
    using Repositories;
    using Repositories.Contracts;
    using SpaceStation.Models.Astronauts;
    using SpaceStation.Models.Astronauts.Contracts;
    using SpaceStation.Models.Mission;
    using SpaceStation.Models.Planets;
    using SpaceStation.Models.Planets.Contracts;
    using SpaceStation.Utilities.Messages;
    using System;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IPlanet> planets;
        private IRepository<IAstronaut> astronauts;
        private int exploredPlanets = 0;

        public Controller()
        {
            this.planets = new PlanetRepository();
            this.astronauts = new AstronautRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;
            if(type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            this.astronauts.Add(astronaut);

            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            var planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            this.planets.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var mission = new Mission();
            var planetToExplore = this.planets.FindByName(planetName);

            var suitableAstronauts = this.astronauts.Models.Where(a => a.Oxygen > 60).ToList();

            if(suitableAstronauts.Count == 0)
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);

            mission.Explore(planetToExplore, suitableAstronauts);

            this.exploredPlanets++;

            var deadAstronauts = suitableAstronauts.Where(a => a.Oxygen == 0).ToList();

            return string.Format(OutputMessages.PlanetExplored, planetName, deadAstronauts.Count);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.exploredPlanets} planets were explored!")
                .AppendLine("Astronauts info:");

            foreach (var astronaut in this.astronauts.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}")
                    .AppendLine($"Oxygen: {astronaut.Oxygen}")
                    .AppendLine($"Bag items: {(astronaut.Bag.Items.Count == 0 ? "none" : string.Join(", ", astronaut.Bag.Items))}");
            }

            return sb.ToString().Trim();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = this.astronauts.FindByName(astronautName);
            if (astronaut == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));

            this.astronauts.Remove(astronaut);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
