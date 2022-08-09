namespace Easter.Repositories
{
    using Contracts;
    using Easter.Models.Bunnies.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class BunnyRepository : IRepository<IBunny>
    {
        private readonly List<IBunny> models;

        public BunnyRepository()
        {
            this.models = new List<IBunny>();
        }

        public IReadOnlyCollection<IBunny> Models => this.models.AsReadOnly();

        public void Add(IBunny model) => this.models.Add(model);

        public IBunny FindByName(string name) => this.models.FirstOrDefault(b => b.Name == name);

        public bool Remove(IBunny model) => this.models.Remove(model);
    }
}
