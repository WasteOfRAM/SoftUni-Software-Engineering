namespace Easter.Repositories
{
    using Contracts;
    using Easter.Models.Eggs.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class EggRepository : IRepository<IEgg>
    {
        private readonly List<IEgg> models;

        public EggRepository()
        {
            this.models = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models => this.models.AsReadOnly();

        public void Add(IEgg model) => this.models.Add(model);

        public IEgg FindByName(string name) => this.models.FirstOrDefault(e => e.Name == name);

        public bool Remove(IEgg model) => this.models.Remove(model);
    }
}
