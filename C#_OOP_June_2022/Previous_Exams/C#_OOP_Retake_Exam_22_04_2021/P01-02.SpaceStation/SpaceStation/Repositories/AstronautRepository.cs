﻿namespace SpaceStation.Repositories
{
    using Contracts;
    using Models.Astronauts.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly List<IAstronaut> models;

        public AstronautRepository()
        {
            this.models = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => this.models;

        public void Add(IAstronaut model)
        {
            this.models.Add(model);
        }

        public IAstronaut FindByName(string name) => this.models.FirstOrDefault(a => a.Name == name);

        public bool Remove(IAstronaut model) => this.models.Remove(model);
    }
}
