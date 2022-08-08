namespace CarRacing.Repositories
{
    using CarRacing.Models.Racers.Contracts;
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RacerRepository : IRepository<IRacer>
    {
        private readonly List<IRacer> models;

        public RacerRepository()
        {
            this.models = new List<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models => this.models;

        public void Add(IRacer model) => this.models.Add(model);

        public IRacer FindBy(string property) => this.models.FirstOrDefault(racer => racer.Username == property);

        public bool Remove(IRacer model) => this.models.Remove(model);
    }
}
