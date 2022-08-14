namespace PlanetWars.Repositories
{
    using Contracts;
    using Models.Planets.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;

        public PlanetRepository()
        {
            this.models = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.models.AsReadOnly();

        public void AddItem(IPlanet model) => this.models.Add(model);

        public IPlanet FindByName(string name) => this.models.FirstOrDefault(p => p.Name == name);

        public bool RemoveItem(string name)
        {
            var planet = this.models.FirstOrDefault(p => p.Name == name);

            return this.models.Remove(planet);
        }
    }
}
